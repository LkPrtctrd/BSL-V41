using BSL.v41.Supercell.Titan.CommonUtils;
using BSL.v41.Titan.Mathematical;

namespace BSL.v41.Logic.Environment.LaserMessage.Laser.Node_1;

public class LoginOkMessage : PiranhaMessage
{
    private string? _accountCreatedDate;
    private long _accountId;
    private int _contentVersion;
    private int _daysSinceStartedPlaying;
    private string? _facebookId;
    private string? _gamecenterId;
    private long _homeId;
    private string? _kunlunId;
    private string? _loginCountry;
    private string? _passToken;
    private int _playTimeSeconds;
    private int _secondsUntilAccountDeletion;
    private int _serverBuild;
    private string? _serverEnvironment;
    private int _serverMajorVersion;
    private string? _serverTime;
    private int _sessionCount;
    private int _startupCooldownSeconds;
    private int _tier;

    public LoginOkMessage()
    {
        Helper.Skip();
    }

    public override void Encode()
    {
        base.Encode();

        new LogicLong(0, (int)_accountId).Encode(ByteStream);
        new LogicLong(0, (int)_homeId).Encode(ByteStream);
        ByteStream.WriteString(_passToken);
        ByteStream.WriteString(_facebookId);
        ByteStream.WriteString(_gamecenterId);
        ByteStream.WriteInt(_serverMajorVersion);
        ByteStream.WriteInt(_serverBuild);
        ByteStream.WriteInt(_contentVersion);
        ByteStream.WriteString(_serverEnvironment);
        ByteStream.WriteInt(_sessionCount);
        ByteStream.WriteInt(_playTimeSeconds);
        ByteStream.WriteInt(_daysSinceStartedPlaying);
        ByteStream.WriteString(_serverTime);
        ByteStream.WriteString(_accountCreatedDate);
        ByteStream.WriteString(null);
        ByteStream.WriteInt(_startupCooldownSeconds);
        ByteStream.WriteString(null);
        ByteStream.WriteString(_loginCountry);
        ByteStream.WriteString(_kunlunId);
        ByteStream.WriteInt(_tier);
        ByteStream.WriteInt(0);
        ByteStream.WriteInt(_secondsUntilAccountDeletion);
        ByteStream.WriteInt(1);
        ByteStream.WriteString(null);
        ByteStream.WriteInt(1);
        ByteStream.WriteInt(1);
        ByteStream.WriteBoolean(true);
    }

    public override void Destruct()
    {
        base.Destruct();
    }

    public void SetAccountId(long accountId)
    {
        _accountId = accountId;
    }

    public void SetHomeId(long homeId)
    {
        _homeId = homeId;
    }

    public void SetPassToken(string passToken)
    {
        _passToken = passToken;
    }

    public void SetFacebookId(string facebookId)
    {
        _facebookId = facebookId;
    }

    public void SetGamecenterId(string gamecenterId)
    {
        _gamecenterId = gamecenterId;
    }

    public void SetServerMajorVersion(int majorVersion)
    {
        _serverMajorVersion = majorVersion;
    }

    public void SetServerBuild(int serverBuild)
    {
        _serverBuild = serverBuild;
    }

    public void SetContentVersion(int contentVersion)
    {
        _contentVersion = contentVersion;
    }

    public void SetServerEnvironment(string serverEnvironment)
    {
        _serverEnvironment = serverEnvironment;
    }

    public void SetSessionCount(int sessionCount)
    {
        _sessionCount = sessionCount;
    }

    public void SetPlayTimeSeconds(int playTimeSeconds)
    {
        _playTimeSeconds = playTimeSeconds;
    }

    public void SetDaysSinceStartedPlaying(int daysSinceStartedPlaying)
    {
        _daysSinceStartedPlaying = daysSinceStartedPlaying;
    }

    public void SetServerTime(string serverTime)
    {
        _serverTime = serverTime;
    }

    public void SetAccountCreatedDate(string accountCreatedDate)
    {
        _accountCreatedDate = accountCreatedDate;
    }

    public void SetStartupCooldownSeconds(int startupCooldownSeconds)
    {
        _startupCooldownSeconds = startupCooldownSeconds;
    }

    public void SetLoginCountry(string loginCountry)
    {
        _loginCountry = loginCountry;
    }

    public void SetKunlunId(string kunlunId)
    {
        _kunlunId = kunlunId;
    }

    public void SetTier(int tier)
    {
        _tier = tier;
    }

    public void SetSecondsUntilAccountDeletion(int secondsUntilAccountDeletion)
    {
        _secondsUntilAccountDeletion = secondsUntilAccountDeletion;
    }

    public override int GetMessageType()
    {
        return 20104;
    }

    public override int GetServiceNodeType()
    {
        return 1;
    }
}