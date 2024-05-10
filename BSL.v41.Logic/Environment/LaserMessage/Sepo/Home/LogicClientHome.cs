using BSL.v41.Logic.Database;
using BSL.v41.Logic.Database.Account;
using BSL.v41.Logic.Environment.LaserMessage.Sepo.Home.Laser;
using BSL.v41.Titan.DataStream;
using BSL.v41.Titan.DataStream.Helps;

namespace BSL.v41.Logic.Environment.LaserMessage.Sepo.Home;

public class LogicClientHome(AccountModel accountModel)
{
    public void Encode(ByteStream byteStream)
    {
        var v1 = Databases.NotificationIntraSignedDatabase.GetAppendix(Convert.ToInt64(
            accountModel.GetFieldValueByAccountStructureParameterFromAccountModel(
                AccountStructure.NotificationFactoryIntraSignedId))); // object;

        new LogicDailyData(accountModel).Encode(byteStream);
        new LogicConfData(accountModel).Encode(byteStream);

        byteStream.WriteLong(accountModel.GetAccountId());

        byteStream.WriteVInt(v1.FreeTextNotifications.Count);
        {
            foreach (var notification in v1.FreeTextNotifications)
            {
                byteStream.WriteVInt(notification.Value.GetNotificationType());
                notification.Value.Encode(byteStream);
            }
        }

        byteStream.WriteVInt(-1);
        byteStream.WriteBoolean(false);
        byteStream.WriteVInt(0);

        var v101 = byteStream.WriteVInt(0);
        {
            for (var i = 0; i < v101; i++) ByteStreamHelper.WriteDataReference(byteStream, 0);
        }

        byteStream.WriteVInt(0);
    }
}