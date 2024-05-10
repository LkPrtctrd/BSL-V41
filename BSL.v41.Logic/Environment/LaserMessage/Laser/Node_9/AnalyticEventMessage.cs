using BSL.v41.Logic.Environment.LaserMessage.Sepo;

namespace BSL.v41.Logic.Environment.LaserMessage.Laser.Node_9;

public class AnalyticEventMessage : PiranhaMessage
{
    private AnalyticEvent _analyticEvent;

    public AnalyticEventMessage()
    {
        _analyticEvent = new AnalyticEvent(ByteStream);
    }

    public override void Decode()
    {
        base.Decode();

        _analyticEvent.Decode();
    }

    public override void Clear()
    {
        base.Clear();

        _analyticEvent = null!;
    }

    public AnalyticEvent GetAnalyticEvent()
    {
        return _analyticEvent;
    }

    public override int GetMessageType()
    {
        return 10110;
    }

    public override int GetServiceNodeType()
    {
        return 9;
    }
}