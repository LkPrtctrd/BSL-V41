namespace BSL.v41.Supercell.Titan.CommonUtils.Utils;

public enum ObjectIndexHelperTable
{
    BlueTeamIndexType,
    RedTeamIndexType,
    ForAllRedTeamIndexType
}

public static class ObjectIndexHelperTableExtensions
{
    public static int GetIndex(this ObjectIndexHelperTable objectIndexHelperTable, int ownIndex)
    {
        return objectIndexHelperTable switch
        {
            ObjectIndexHelperTable.BlueTeamIndexType => ownIndex + 16 * 0,
            ObjectIndexHelperTable.RedTeamIndexType => ownIndex + 16 * 1,
            ObjectIndexHelperTable.ForAllRedTeamIndexType => 16 * 10,
            _ => throw new ArgumentOutOfRangeException(nameof(objectIndexHelperTable), objectIndexHelperTable, null)
        };
    }

    public static int GetIndex(int teamVariation, int ownIndex)
    {
        return ownIndex + 16 * teamVariation;
    }

    public static int GetOwnIndex(int teamVariation, int index)
    {
        return index - 16 * teamVariation;
    }
}