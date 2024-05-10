using BSL.v41.Tools.LaserCsv.Data;
using BSL.v41.Tools.LaserCsv.Portal;

namespace BSL.v41.Tools.LaserCsv.Manufacturer.Laser;

public class LogicAllianceRoleData(CsvRow row, LogicDataTable table) : LogicData(row, table)
{
    private bool _canAcceptJoinRequest;
    private bool _canBePromotedToLeader;
    private bool _canChangeAllianceSettings;
    private bool _canInvite;
    private bool _canKick;
    private bool _canSendMail;
    private int _level;
    private int _promoteSkill;

    // LogicAllianceRoleData.

    public override void CreateReferences()
    {
        _level = GetIntegerValue("Level", 0);
        _canInvite = GetBooleanValue("CanInvite", 0);
        _canSendMail = GetBooleanValue("CanSendMail", 0);
        _canChangeAllianceSettings = GetBooleanValue("CanChangeAllianceSettings", 0);
        _canAcceptJoinRequest = GetBooleanValue("CanAcceptJoinRequest", 0);
        _canKick = GetBooleanValue("CanKick", 0);
        _canBePromotedToLeader = GetBooleanValue("CanBePromotedToLeader", 0);
        _promoteSkill = GetIntegerValue("PromoteSkill", 0);
    }

    public int GetLevel()
    {
        return _level;
    }

    public bool GetCanInvite()
    {
        return _canInvite;
    }

    public bool GetCanSendMail()
    {
        return _canSendMail;
    }

    public bool GetCanChangeAllianceSettings()
    {
        return _canChangeAllianceSettings;
    }

    public bool GetCanAcceptJoinRequest()
    {
        return _canAcceptJoinRequest;
    }

    public bool GetCanKick()
    {
        return _canKick;
    }

    public bool GetCanBePromotedToLeader()
    {
        return _canBePromotedToLeader;
    }

    public int GetPromoteSkill()
    {
        return _promoteSkill;
    }
}