namespace BSL.v41.Supercell.Titan.CommonUtils.Utils;

public enum AllianceRoleHelperTable
{
    NonMember = 0,
    Member = 1,
    Leader = 2,
    Elder = 3,
    CoLeader = 4
}

public static class AllianceRoleHelperTableExtensions
{
    public static string GetCsvName(this AllianceRoleHelperTable allianceRoleHelperTable)
    {
        return Enum.GetName(typeof(AllianceRoleHelperTable), allianceRoleHelperTable)!;
    }
}