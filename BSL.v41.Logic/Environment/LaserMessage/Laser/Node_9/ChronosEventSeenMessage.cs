using BSL.v41.Supercell.Titan.CommonUtils;

namespace BSL.v41.Logic.Environment.LaserMessage.Laser.Node_9;

public class ChronosEventSeenMessage : PiranhaMessage
{
    private int _event;

    public ChronosEventSeenMessage()
    {
        Helper.Skip();
    }

    public override void Decode()
    {
        base.Decode();

        _event = ByteStream.ReadVInt();
    }

    public override void Clear()
    {
        base.Clear();

        Helper.Destructor(this);
    }

    public int GetEvent()
    {
        return _event;
    }

    public void SetEvent(int @event)
    {
        _event = @event;
    }

    public override int GetMessageType()
    {
        return 14166;
    }

    public override int GetServiceNodeType()
    {
        return 9;
    }
}