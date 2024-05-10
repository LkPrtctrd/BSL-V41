using Newtonsoft.Json.Linq;
using BSL.v41.Titan.DataStream;
using BSL.v41.Titan.DataStream.Helps;
using BSL.v41.Titan.DataStream.Logic;

namespace BSL.v41.Tools.LaserCsv.Data;

public class LogicDataSlot(LogicData data, int count = 0)
{
    public LogicData GetData()
    {
        return data;
    }

    public LogicDataSlot Clone()
    {
        return new LogicDataSlot(data, count);
    }

    public void Encode(ChecksumEncoder encoder)
    {
        ByteStreamHelper.WriteDataReference(encoder, data.GetGlobalId());
        encoder.WriteVInt(count);
    }

    public void Decode(ByteStream stream)
    {
        data = LogicDataTables.GetDataById(ByteStreamHelper.ReadDataReference(stream));
        count = stream.ReadVInt();
    }

    public int GetCount()
    {
        return count;
    }

    public void SetCount(int countD)
    {
        count = countD;
    }

    public void ReadFromJson(JObject obj)
    {
        var id = (int)obj["data"]!;

        data = LogicDataTables.GetDataById(id);
        count = (int)obj["count"]!;
    }

    public JObject WriteToJson()
    {
        var obj = new JObject();
        {
            obj["data"] = data.GetGlobalId();
            obj["count"] = count;
        }

        return obj;
    }
}