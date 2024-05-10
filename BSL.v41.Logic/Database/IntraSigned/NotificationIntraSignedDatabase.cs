using Newtonsoft.Json;
using BSL.v41.Logic.Database.Account;
using BSL.v41.Logic.Environment.LaserNotification;
using BSL.v41.Titan.Graphic;

namespace BSL.v41.Logic.Database.IntraSigned;

public class NotificationIntraSignedDatabase(string filePath)
{
    public void Debug()
    {
        ConsoleLogger.WriteTextWithPrefix(ConsoleLogger.Prefixes.Start,
            $"Database-element started! Information: element name: {GetType().Name}; " +
            $"database buffer length: {LoadFromFile().Count}.");
    }

    public void AddAppendix(long iSId, NotificationFactory notificationFactory)
    {
        var v1 = LoadFromFile();
        {
            v1.Add(iSId, notificationFactory);
        }

        SaveToFile(v1);
    }

    public void ReplaceAppendix(NotificationFactory notificationFactory)
    {
        var v1 = LoadFromFile();
        {
            v1[Convert.ToInt64(
                    notificationFactory.AccountModel.GetFieldValueByAccountStructureParameterFromAccountModel(
                        AccountStructure.NotificationFactoryIntraSignedId))] =
                notificationFactory;
        }

        SaveToFile(v1);
    }

    public NotificationFactory GetAppendix(long iSId)
    {
        return LoadFromFile()[iSId];
    }

    private void SaveToFile(Dictionary<long, NotificationFactory> notificationFactories)
    {
        File.WriteAllText(filePath, JsonConvert.SerializeObject(notificationFactories));
    }

    private Dictionary<long, NotificationFactory> LoadFromFile()
    {
        return !File.Exists(filePath)
            ? new Dictionary<long, NotificationFactory>()
            : JsonConvert.DeserializeObject<Dictionary<long, NotificationFactory>>(File.ReadAllText(filePath))!;
    }
}