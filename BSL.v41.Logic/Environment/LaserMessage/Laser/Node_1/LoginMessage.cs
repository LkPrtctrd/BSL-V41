using BSL.v41.Supercell.Titan.CommonUtils;
using BSL.v41.Titan.DataStream.Helps;
using BSL.v41.Titan.Mathematical;
using BSL.v41.Titan.Message;

namespace BSL.v41.Logic.Environment.LaserMessage.Laser.Node_1;

public class LoginMessage : PiranhaMessage
{
    private long _accountId;
    private int _clientBuild;
    private int _clientMajorVersion;
    private int _clientMinor;
    private string? _clientVersion;
    private string? _device;
    private int _iMei;
    private bool _isAdvertisingEnabled;
    private bool _isAndroid;
    private string? _oSVersion;
    private string? _passToken;
    private string? _preferredDeviceLanguage;
    private int _preferredLanguage;
    private string? _resourceSha;

    public LoginMessage()
    {
        Helper.Skip();
    }

    public override void Decode()
    {
        base.Decode();

        _accountId = new LogicLong().Decode(ByteStream);
        _passToken = ByteStream.ReadString(1024 * 5);
        _clientMajorVersion = ByteStream.ReadInt();
        _clientBuild = ByteStream.ReadInt();
        _clientMinor = ByteStream.ReadInt();
        _resourceSha = ByteStream.ReadString(1024);
        _device = ByteStream.ReadString(1024);
        _preferredLanguage = ByteStreamHelper.ReadDataReference(ByteStream);
        _preferredDeviceLanguage = ByteStream.ReadString(1024);
        _oSVersion = ByteStream.ReadString(1024);
        _isAndroid = ByteStream.ReadBoolean();
        ByteStream.ReadStringReference(1024);
        ByteStream.ReadStringReference(1024);
        _isAdvertisingEnabled = ByteStream.ReadBoolean();
        ByteStream.ReadString(1024);
        _iMei = ByteStream.ReadInt();
        ByteStream.ReadVInt();
        _clientVersion = ByteStream.ReadStringReference(1024);
    }

    public override void Clear()
    {
        base.Clear();

        Helper.Destructor(this);
    }

    public long GetAccountId()
    {
        return _accountId;
    }

    public string GetPassToken()
    {
        return _passToken!;
    }

    public int GetClientMajorVersion()
    {
        return _clientMajorVersion;
    }

    public int GetClientBuild()
    {
        return _clientBuild;
    }

    public int GetClientMinor()
    {
        return _clientMinor;
    }

    public string GetResourceSha()
    {
        return _resourceSha!;
    }

    public string GetDevice()
    {
        return _device!;
    }

    public int GetPreferredLanguage()
    {
        return _preferredLanguage;
    }

    public string GetPreferredDeviceLanguage()
    {
        return _preferredDeviceLanguage!;
    }

    public string GetOsVersion()
    {
        return _oSVersion!;
    }

    public bool GetIsAndroid()
    {
        return _isAndroid;
    }

    public bool GetIsAdvertisingEnabled()
    {
        return _isAdvertisingEnabled;
    }

    public int GetImei()
    {
        return _iMei;
    }

    public string GetClientVersion()
    {
        return _clientVersion!;
    }

    public override int GetMessageType()
    {
        return TitanLoginMessage.GetMessageType();
    }

    public override int GetServiceNodeType()
    {
        return TitanLoginMessage.GetServiceNodeType();
    }
}