using BSL.v41.Supercell.Titan.CommonUtils;

namespace BSL.v41.Logic.Environment.LaserMessage.Laser.Node_9;

public class MatchMakingStatusMessage : PiranhaMessage
{
    private int _foundPlayers;
    private int _maxFounds;
    private int _seconds;
    private bool _showTips;

    public MatchMakingStatusMessage()
    {
        Helper.Skip();
    }

    public override void Encode()
    {
        base.Encode();

        ByteStream.WriteInt(_seconds);
        ByteStream.WriteInt(_foundPlayers);
        ByteStream.WriteInt(_maxFounds);
        ByteStream.WriteInt(0);
        ByteStream.WriteInt(0);
        ByteStream.WriteBoolean(_showTips);
    }

    public override void Destruct()
    {
        base.Destruct();
    }

    public int GetSeconds()
    {
        return _seconds;
    }

    public void SetSeconds(int seconds)
    {
        _seconds = seconds;
    }

    public int GetFoundPlayers()
    {
        return _foundPlayers;
    }

    public void SetFoundPlayers(int foundPlayers)
    {
        _foundPlayers = foundPlayers;
    }

    public int GetMaxFounds()
    {
        return _maxFounds;
    }

    public void SetMaxFounds(int maxFounds)
    {
        _maxFounds = maxFounds;
    }

    public bool GetShowTips()
    {
        return _showTips;
    }

    public void SetShowTips(bool showTips)
    {
        _showTips = showTips;
    }

    public override int GetMessageType()
    {
        return 20405;
    }

    public override int GetServiceNodeType()
    {
        return 9;
    }
}