using BSL.v41.Supercell.Titan.CommonUtils;

namespace BSL.v41.Logic.Environment.LaserMessage.Laser.Node_1;

public class AccountIdentifiersMessage : PiranhaMessage
{
    private string _identifier = null!;

    public AccountIdentifiersMessage()
    {
        Helper.Skip();
    }

    public override void Decode()
    {
        base.Decode();

        _identifier = ByteStream.ReadString(2048);
    }

    public override void Clear()
    {
        base.Clear();

        Helper.Destructor(this);
    }

    public string GetIdentifier()
    {
        return _identifier;
    }

    public void SetIdentifier(string identifier)
    {
        _identifier = identifier;
    }

    public override int GetMessageType()
    {
        return 10111;
    }

    public override int GetServiceNodeType()
    {
        return 1;
    }
}