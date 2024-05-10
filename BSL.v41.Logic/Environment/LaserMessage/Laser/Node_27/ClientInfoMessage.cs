using BSL.v41.Supercell.Titan.CommonUtils;

namespace BSL.v41.Logic.Environment.LaserMessage.Laser.Node_27;

public class ClientInfoMessage : PiranhaMessage
{
    private string _type = null!;

    public ClientInfoMessage()
    {
        Helper.Skip();
    }

    public override void Decode()
    {
        base.Decode();

        _type = ByteStream.ReadString(512);
    }

    public override void Clear()
    {
        base.Clear();
    }

    public new string GetType()
    {
        return _type;
    }

    public void SetType(string type)
    {
        _type = type;
    }

    public override int GetMessageType()
    {
        return 10177;
    }

    public override int GetServiceNodeType()
    {
        return 27;
    }
}