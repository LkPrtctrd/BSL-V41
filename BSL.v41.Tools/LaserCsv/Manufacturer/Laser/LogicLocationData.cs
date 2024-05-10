using BSL.v41.Tools.LaserCsv.Data;
using BSL.v41.Tools.LaserCsv.Portal;

namespace BSL.v41.Tools.LaserCsv.Manufacturer.Laser;

public class LogicLocationData(CsvRow row, LogicDataTable table) : LogicData(row, table)
{
    private string _bgPrefix = null!;
    private string _campaignGroundScw = null!;
    private string _communityCredit = null!;
    private bool _disabled;
    private string _environmentScw = null!;
    private string _gameModeVariation = null!;
    private string _groundScw = null!;
    private string _iconSwf = null!;
    private string _locationTheme = null!;
    private string _map = null!;
    private string _music = null!;

    // LogicLocationData.

    public override void CreateReferences()
    {
        _disabled = GetBooleanValue("Disabled", 0);
        _bgPrefix = GetValue("BgPrefix", 0);
        _locationTheme = GetValue("LocationTheme", 0);
        _groundScw = GetValue("GroundSCW", 0);
        _campaignGroundScw = GetValue("CampaignGroundSCW", 0);
        _environmentScw = GetValue("EnvironmentSCW", 0);
        _iconSwf = GetValue("IconSWF", 0);
        _gameModeVariation = GetValue("GameModeVariation", 0);
        _map = GetValue("Map", 0);
        _music = GetValue("Music", 0);
        _communityCredit = GetValue("CommunityCredit", 0);
    }

    public bool GetDisabled()
    {
        return _disabled;
    }

    public string GetBgPrefix()
    {
        return _bgPrefix;
    }

    public string GetLocationTheme()
    {
        return _locationTheme;
    }

    public string GetGroundScw()
    {
        return _groundScw;
    }

    public string GetCampaignGroundScw()
    {
        return _campaignGroundScw;
    }

    public string GetEnvironmentScw()
    {
        return _environmentScw;
    }

    public string GetIconSwf()
    {
        return _iconSwf;
    }

    public string GetGameModeVariation()
    {
        return _gameModeVariation;
    }

    public string GetMusic()
    {
        return _music;
    }

    public string GetCommunityCredit()
    {
        return _communityCredit;
    }

    public string GetMap()
    {
        return _map;
    }
}