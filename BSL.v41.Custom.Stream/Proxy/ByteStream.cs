using System.Reflection;
using Newtonsoft.Json;
using BSL.v41.Custom.Stream.Proxy.Laser;
using BSL.v41.Titan.Mathematical.Data;

namespace BSL.v41.Custom.Stream.Proxy;

public class ByteStream
{
    private readonly List<DataField> _data = [];

    public void WriteString(string value)
    {
        var dataField = new DataField
        {
            FieldType = MethodBase.GetCurrentMethod()!.Name,
            FieldValue = value
        };

        _data.Add(dataField);
    }

    public int WriteVInt(int value)
    {
        var dataField = new DataField
        {
            FieldType = MethodBase.GetCurrentMethod()!.Name,
            FieldValue = value.ToString()
        };

        _data.Add(dataField);
        return value;
    }

    public void WriteInt(int value)
    {
        var dataField = new DataField
        {
            FieldType = MethodBase.GetCurrentMethod()!.Name,
            FieldValue = value.ToString()
        };

        _data.Add(dataField);
    }

    public void WriteLong(int value)
    {
        var dataField = new DataField
        {
            FieldType = MethodBase.GetCurrentMethod()!.Name,
            FieldValue = value.ToString()
        };

        _data.Add(dataField);
    }

    public void WriteBoolean(bool value)
    {
        var dataField = new DataField
        {
            FieldType = MethodBase.GetCurrentMethod()!.Name,
            FieldValue = value.ToString()
        };

        _data.Add(dataField);
    }

    public void WriteLogicLong(int value)
    {
        WriteVInt(0);
        WriteVInt(value);
    }

    public void WriteDataReference(int classId, int instanceId)
    {
        WriteVInt(classId);
        WriteVInt(instanceId);
    }

    public void WriteDataReference(int globalId)
    {
        WriteVInt(GlobalId.GetClassId(globalId));
        WriteVInt(GlobalId.GetInstanceId(globalId));
    }

    public string ToJson()
    {
        return JsonConvert.SerializeObject(_data);
    }
}