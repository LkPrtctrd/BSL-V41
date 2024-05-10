using BSL.v41.Logic.Environment.LaserMessage.Sepo.Mode;
using BSL.v41.Logic.Environment.LaserNotification.Laser.Own;
using BSL.v41.Titan.DataStream;

namespace BSL.v41.Logic.Environment.LaserCommand.Laser;

public class LogicAddNotificationCommand(BaseNotification notification) : LogicCommand
{
    public override void Encode(ByteStream byteStream)
    {
        byteStream.WriteVInt(1);
        {
            byteStream.WriteVInt(notification.GetNotificationType());
            {
                notification.Encode(byteStream);
            }
        }
    }

    public override bool CanExecute(LogicHomeMode logicHomeMode)
    {
        return false;
    }

    public override void Decode(ByteStream byteStream)
    {
        byteStream.ReadVInt();
        {
            byteStream.ReadVInt();
            {
                notification.Decode(byteStream);
            }
        }
    }

    public override int Execute(LogicHomeMode logicHomeMode)
    {
        return 0;
    }

    public override int GetCommandType()
    {
        return 206;
    }
}