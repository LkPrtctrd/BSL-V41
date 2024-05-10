using BSL.v41.Supercell.Titan.CommonUtils;
using BSL.v41.Titan.DataStream.Helps;

namespace BSL.v41.Logic.Environment.LaserMessage.Laser.Node_9;

public class MatchmakeRequestMessage : PiranhaMessage
{
    private int _brawler;
    private int _eventSlot;

    public MatchmakeRequestMessage()
    {
        Helper.Skip();
    }

    public override void Decode()
    {
        base.Decode();

        _brawler = ByteStreamHelper.ReadDataReference(ByteStream);
        ByteStream.ReadVInt();
        ByteStream.ReadVInt();
        _eventSlot = ByteStream.ReadVInt(); 
        ByteStream.ReadVInt();
    }

    public override void Clear()
    {
        base.Clear();
    }

    public int GetBrawler()
    {
        return _brawler;
    }

    public void SetBrawler(int brawler)
    {
        _brawler = brawler;
    }

    public int GetEventSlot()
    {
        return _eventSlot;
    }

    public void SetEventSlot(int eventSlot)
    {
        _eventSlot = eventSlot;
    }

    public override int GetMessageType()
    {
        return 14103;
    }

    public override int GetServiceNodeType()
    {
        return 9;
    }
}