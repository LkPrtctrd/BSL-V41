using BSL.v41.General.Cloud;
using BSL.v41.Logic.Environment;
using BSL.v41.Logic.Environment.LaserMessage;
using BSL.v41.Logic.Environment.LaserMessage.Laser.Node_27;
using BSL.v41.Titan.DataStream;

namespace BSL.v41.General.Networking.LaserUdp;

public class UdpPacketHandler(long sessionId, ByteStream byteStream)
{
    public void Receive()
    {
        var type = byteStream.ReadVInt();
        var length = byteStream.ReadVInt();
        var data = byteStream.ReadBytes(length, 2048);

        var message = LogicLaserMessageFactory.CreateMessageByType(type);
        if (message == null) return;

        message.ByteStream.SetByteArray(data, length);
        message.Decode();

        HandleMessage(message);
    }

    private void HandleMessage(PiranhaMessage piranhaMessage)
    {
        if (piranhaMessage.GetMessageType() != 10555) return;

        while (((ClientInputMessage)piranhaMessage).ClientInputManager!.GetOverrideGroup()
               .TryDequeue(out var clientInput))
        {
            clientInput.OwnSessionId = sessionId;
            InteractiveModule.LogicBattleModeServersMassive![sessionId].HandleClientInput(clientInput);
        }
    }
}