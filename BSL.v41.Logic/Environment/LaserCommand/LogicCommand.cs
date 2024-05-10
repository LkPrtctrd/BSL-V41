using BSL.v41.Logic.Environment.LaserMessage.Sepo.Mode;
using BSL.v41.Titan.DataStream;

namespace BSL.v41.Logic.Environment.LaserCommand;

public class LogicCommand
{
    public virtual void Decode(ByteStream byteStream)
    {
        byteStream.ReadVInt();
        byteStream.ReadVInt();
        byteStream.ReadVInt();
        byteStream.ReadVInt();
    }

    public virtual void Encode(ByteStream byteStream)
    {
        byteStream.WriteVInt(1);
        byteStream.WriteVInt(1);
        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0);
    }

    public virtual bool CanExecute(LogicHomeMode logicHomeMode)
    {
        return false;
    }

    public virtual int Execute(LogicHomeMode logicHomeMode)
    {
        return 0;
    }

    public virtual int GetCommandType()
    {
        return 0;
    }
}