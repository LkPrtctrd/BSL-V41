using BSL.v41.Titan.DataStream.Logic;
using BSL.v41.Titan.Mathematical;
using BSL.v41.Titan.Mathematical.Data;

namespace BSL.v41.Titan.DataStream.Helps;

public static class ByteStreamHelper
{
    public static LogicLong DecodeLogicLong(ByteStream byteStream)
    {
        var high = byteStream.ReadVInt();
        var low = byteStream.ReadVInt();

        return new LogicLong(high, low);
    }

    public static LogicLong EncodeLogicLong(ChecksumEncoder checksumEncoder, LogicLong value)
    {
        checksumEncoder.WriteVInt(value.GetHigherInt());
        checksumEncoder.WriteVInt(value.GetLowerInt());

        return value;
    }

    public static LogicLong EncodeLogicLong(ByteStream byteStream, LogicLong value)
    {
        byteStream.WriteVInt(value.GetHigherInt());
        byteStream.WriteVInt(value.GetLowerInt());

        return value;
    }

    public static int ReadDataReference(ByteStream byteStream)
    {
        var classId = byteStream.ReadVInt();
        var instanceId = byteStream.ReadVInt();

        return classId + instanceId == 0 ? 0 : GlobalId.CreateGlobalId(classId, instanceId);
    }

    public static int WriteDataReference(ChecksumEncoder checksumEncoder, int globalId)
    {
        checksumEncoder.WriteVInt(GlobalId.GetClassId(globalId));
        checksumEncoder.WriteVInt(GlobalId.GetInstanceId(globalId));

        return globalId;
    }

    public static int WriteDataReference(ByteStream byteStream, int globalId)
    {
        if (globalId > 0)
        {
            byteStream.WriteVInt(GlobalId.GetClassId(globalId));
            byteStream.WriteVInt(GlobalId.GetInstanceId(globalId));
        }
        else
        {
            byteStream.WriteVInt(0);
        }

        return globalId;
    }

    public static int WriteDataReference(ByteStream byteStream, object globalIdO)
    {
        var globalId = Convert.ToInt32(globalIdO);

        if (globalId > 0)
        {
            byteStream.WriteVInt(GlobalId.GetClassId(globalId));
            byteStream.WriteVInt(GlobalId.GetInstanceId(globalId));
        }
        else
        {
            byteStream.WriteVInt(0);
        }

        return globalId;
    }

    public static int WriteDataReference(ByteStream byteStream, int classId, int instanceId)
    {
        if (classId > 0)
        {
            byteStream.WriteVInt(classId);
            byteStream.WriteVInt(instanceId);
        }
        else
        {
            byteStream.WriteVInt(0);
        }

        return instanceId;
    }

    public static int WriteDataReference(BitStream bitStream, int globalId)
    {
        bitStream.WritePositiveIntMax31(GlobalId.GetClassId(globalId));
        bitStream.WritePositiveIntMax511(GlobalId.GetInstanceId(globalId));

        return globalId;
    }

    public static void WriteObjectRunningId(BitStream bitStream, int globalId)
    {
        bitStream.WritePositiveVIntMax65535(GlobalId.GetInstanceId(globalId));
    }
}