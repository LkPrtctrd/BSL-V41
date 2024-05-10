using BSL.v41.Supercell.Titan.CommonUtils;

namespace BSL.v41.Logic.Environment.LaserMessage.Laser.Node_9;

public class ChangeAvatarNameMessage : PiranhaMessage
{
    private string _name = null!;
    private bool _nameSetByUser;

    public ChangeAvatarNameMessage()
    {
        Helper.Skip();
    }

    public override void Decode()
    {
        base.Decode();

        _name = ByteStream.ReadString();
        _nameSetByUser = ByteStream.ReadBoolean();
    }

    public override void Clear()
    {
        base.Clear();

        Helper.Destructor(this);
    }

    public string GetName()
    {
        return _name;
    }

    public bool GetNameSetByUser()
    {
        return _nameSetByUser;
    }

    public override int GetMessageType()
    {
        return 10212;
    }

    public override int GetServiceNodeType()
    {
        return 9;
    }
}