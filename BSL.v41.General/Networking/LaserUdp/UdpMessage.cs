using System.Net.Sockets;
using BSL.v41.General.Cloud;
using BSL.v41.Logic.Environment.LaserMessage;
using BSL.v41.Titan.DataStream;

namespace BSL.v41.General.Networking.LaserUdp;

public class UdpMessage
{
    public PiranhaMessage PiranhaMessage { get; set; } = null!;
    public ByteStream ByteStream { get; set; } = new(1024);

    public void Send(long sessionId, int port)
    {
        try
        {
            PiranhaMessage.Encode();

            var stream = new ByteStream(1024);
            {
                stream.WriteLong(sessionId);
                stream.WriteShort(0);
                stream.WriteVInt(PiranhaMessage.GetMessageType());
                stream.WriteVInt(ByteStream.GetLength());
                stream.WriteBytesWithoutLength(ByteStream.GetByteArray(), ByteStream.GetLength());
            }

            Saver.UdpLaserSocketListeners[port]!.UdpAdministrator!.SendTo(stream.GetByteArray(), stream.GetLength(),
                SocketFlags.None, Saver.UdpLaserSocketListeners[port]!.Sessions![sessionId]);
        }
        catch
        {
            // ignored.
        }
    }
}