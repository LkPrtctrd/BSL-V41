using Newtonsoft.Json;
using BSL.v41.Titan.DataStream;
using BSL.v41.Titan.Utilities;

namespace BSL.v41.Logic.Environment.LaserNotification.Laser.Own;

[method: JsonConstructor]
[JsonObject]
public class BaseNotification(string text, bool read, int time)
{
    public bool IsRead = read;
    public string Text = text;
    public int Time = time;
    public int Type = -1;

    public virtual void Encode(ByteStream byteStream)
    {
        byteStream.WriteInt(1);
        byteStream.WriteBoolean(IsRead);
        byteStream.WriteInt(LogicTimeUtil.GetTimestamp() - Time);
        byteStream.WriteString(Text);
    }

    public virtual void Decode(ByteStream byteStream)
    {
        _ = byteStream.ReadInt();
        IsRead = byteStream.ReadBoolean();
        Time = byteStream.ReadInt();
        Text = byteStream.ReadString(99999);
    }

    public virtual void Destruct()
    {
    }

    public virtual int GetNotificationType()
    {
        return Type;
    }
}