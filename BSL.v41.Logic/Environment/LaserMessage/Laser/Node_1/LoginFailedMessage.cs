using BSL.v41.Supercell.Titan.CommonUtils;
using BSL.v41.Titan.Message;

namespace BSL.v41.Logic.Environment.LaserMessage.Laser.Node_1;

public class LoginFailedMessage : PiranhaMessage
{
    private string _contentUrl = null!;
    private int _errorCode;
    private string _reason = null!;
    private string _redirectDomain = null!;
    private string _removeResourceFingerprintData = null!;
    private int _secondsUntilMaintenanceEnd;
    private bool _showContactSupportForBan;
    private string _updateUrl = null!;

    public LoginFailedMessage()
    {
        Helper.Skip();
    }

    public override void Encode()
    {
        base.Encode();

        ByteStream.WriteInt(_errorCode);
        ByteStream.WriteString(_removeResourceFingerprintData);
        ByteStream.WriteString(_redirectDomain);
        ByteStream.WriteString(_contentUrl);
        ByteStream.WriteString(_updateUrl);
        ByteStream.WriteString(_reason);
        ByteStream.WriteInt(_secondsUntilMaintenanceEnd);
        ByteStream.WriteBoolean(_showContactSupportForBan);
    }

    public override void Destruct()
    {
        base.Destruct();
    }

    public int GetErrorCode()
    {
        return _errorCode;
    }

    public string GetRemoveResourceFingerprintData()
    {
        return _removeResourceFingerprintData;
    }

    public string GetRedirectDomain()
    {
        return _redirectDomain;
    }

    public string GetContentUrl()
    {
        return _contentUrl;
    }

    public string GetUpdateUrl()
    {
        return _updateUrl;
    }

    public string GetReason()
    {
        return _reason;
    }

    public int GetSecondsUntilMaintenanceEnd()
    {
        return _secondsUntilMaintenanceEnd;
    }

    public bool IsShowContactSupportForBan()
    {
        return _showContactSupportForBan;
    }

    public void SetErrorCode(int errorCode)
    {
        _errorCode = errorCode;
    }

    public void SetRemoveResourceFingerprintData(string removeResourceFingerprintData)
    {
        _removeResourceFingerprintData = removeResourceFingerprintData;
    }

    public void SetRedirectDomain(string redirectDomain)
    {
        _redirectDomain = redirectDomain;
    }

    public void SetContentUrl(string contentUrl)
    {
        _contentUrl = contentUrl;
    }

    public void SetUpdateUrl(string updateUrl)
    {
        _updateUrl = updateUrl;
    }

    public void SetReason(string reason)
    {
        _reason = reason;
    }

    public void SetSecondsUntilMaintenanceEnd(int secondsUntilMaintenanceEnd)
    {
        _secondsUntilMaintenanceEnd = secondsUntilMaintenanceEnd;
    }

    public void SetShowContactSupportForBan(bool showContactSupportForBan)
    {
        _showContactSupportForBan = showContactSupportForBan;
    }

    public override int GetMessageType()
    {
        return TitanLoginFailedMessage.GetMessageType();
    }

    public override int GetServiceNodeType()
    {
        return TitanLoginFailedMessage.GetServiceNodeType();
    }
}