using BSL.v41.Logic.Environment.LaserMessage.Sepo.Player;
using BSL.v41.Titan.DataStream.Helps;

namespace BSL.v41.Logic.Environment.LaserMessage.Laser.Node_4;

public class StartLoadingMessage : PiranhaMessage
{
    private int _gameModeVariation;
    private bool _isFriendlyMatch;
    private bool _isSpectate;
    private bool _isUnderdog;
    private int _location;
    private LogicPlayer _logicPlayer = null!;
    private List<LogicPlayer> _logicPlayerMap = null!;

    public override void Encode()
    {
        base.Encode();

        ByteStream.WriteInt(_logicPlayerMap.Count);
        ByteStream.WriteInt(_logicPlayer.GetPlayerIndex());
        ByteStream.WriteInt(_logicPlayer.GetTeamIndex());
        ByteStream.WriteInt(_logicPlayerMap.Count);

        foreach (var player in _logicPlayerMap) player.Encode(ByteStream);

        ByteStream.WriteInt(0);
        ByteStream.WriteInt(0);
        ByteStream.WriteInt(0);

        ByteStream.WriteVInt(_gameModeVariation);
        ByteStream.WriteVInt(1);
        ByteStream.WriteVInt(1);

        ByteStream.WriteBoolean(true);

        ByteStream.WriteVInt(_isSpectate);
        ByteStream.WriteVInt(0);

        ByteStreamHelper.WriteDataReference(ByteStream, _location);
        ByteStream.WriteBoolean(false); // player map.
        ByteStream.WriteBoolean(_isUnderdog);
        ByteStream.WriteBoolean(_isFriendlyMatch);
        ByteStream.WriteVInt(0);
        ByteStream.WriteVInt(0);

        ByteStream.WriteVInt(0);
        ByteStream.WriteVInt(0);
    }

    public override void Destruct()
    {
        base.Destruct();
    }

    public int GetLocation()
    {
        return _location;
    }

    public void SetLocation(int location)
    {
        _location = location;
    }

    public int GetGameModeVariation()
    {
        return _gameModeVariation;
    }

    public void SetGameModeVariation(int gameModeVariation)
    {
        _gameModeVariation = gameModeVariation;
    }

    public List<LogicPlayer> GetLogicPlayerMap()
    {
        return _logicPlayerMap;
    }

    public void SetLogicPlayerMap(List<LogicPlayer> logicPlayerMap)
    {
        _logicPlayerMap = logicPlayerMap;
    }

    public LogicPlayer GetLogicPlayer()
    {
        return _logicPlayer;
    }

    public void SetLogicPlayer(LogicPlayer logicPlayer)
    {
        _logicPlayer = logicPlayer;
    }

    public bool GetIsSpectate()
    {
        return _isSpectate;
    }

    public void SetIsSpectate(bool spectate)
    {
        _isSpectate = spectate;
    }

    public bool GetIsUnderdog()
    {
        return _isUnderdog;
    }

    public void SetIsUnderdog(bool value)
    {
        _isUnderdog = value;
    }

    public bool GetIsFriendlyMatch()
    {
        return _isFriendlyMatch;
    }

    public void SetIsFriendlyMatch(bool value)
    {
        _isFriendlyMatch = value;
    }

    public override int GetMessageType()
    {
        return 20559;
    }

    public override int GetServiceNodeType()
    {
        return 4;
    }
}