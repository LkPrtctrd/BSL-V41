namespace BSL.v41.Titan.Mathematical.Data;

public abstract class GlobalId
{
    public static int CreateGlobalId(int classId, int instanceId)
    {
        if (instanceId == 1000000) return 0;
        return 1000000 * classId + instanceId;
    }

    public static int GetClassId(int globalId)
    {
        return globalId / 1000000;
    }

    public static int GetInstanceId(int globalId)
    {
        return globalId % 1000000;
    }
}