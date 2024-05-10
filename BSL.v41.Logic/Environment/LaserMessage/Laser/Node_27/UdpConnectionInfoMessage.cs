using BSL.v41.Supercell.Titan.CommonUtils;

namespace BSL.v41.Logic.Environment.LaserMessage.Laser.Node_27;

public class UdpConnectionInfoMessage(int serverPort, string serverDomain, long sessionId) : PiranhaMessage
{
    public UdpConnectionInfoMessage() : this(0, "", 0)
    {
        Helper.Skip();
    }

    public override void Encode()
    {
        base.Encode();

        ByteStream.WriteVInt(serverPort);
        ByteStream.WriteString(serverDomain);
        ByteStream.WriteInt(10);
        ByteStream.WriteLong(sessionId);
        ByteStream.WriteShort(0);
        ByteStream.WriteInt(0);
    }

    public override void Destruct()
    {
        base.Destruct();
    }

    public override int GetMessageType()
    {
        return 24112;
    }

    public override int GetServiceNodeType()
    {
        return 27;
    }
}