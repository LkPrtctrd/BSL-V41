namespace BSL.v41.Supercell.Titan.CommonUtils.Utils;

public enum ObjectTypeHelperTable
{
    Character,
    Projectile,
    AreaEffect,
    Item
}

public static class ObjectTypeHelperTableExtensions
{
    public static int GetObjectType(this ObjectTypeHelperTable objectTypeHelperTable)
    {
        return objectTypeHelperTable switch
        {
            ObjectTypeHelperTable.Character => 1,
            ObjectTypeHelperTable.Projectile => 2,
            ObjectTypeHelperTable.AreaEffect => 3,
            ObjectTypeHelperTable.Item => 4,
            _ => throw new ArgumentOutOfRangeException(nameof(objectTypeHelperTable), objectTypeHelperTable, null)
        };
    }
}