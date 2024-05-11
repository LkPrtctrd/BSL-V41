using Newtonsoft.Json;
using BSL.v41.Logic.Database;
using BSL.v41.Logic.Environment.LaserCommand.Laser;
using BSL.v41.Logic.Environment.LaserListener;
using BSL.v41.Logic.Environment.LaserMessage.Laser.Node_9;
using BSL.v41.Logic.Environment.LaserMessage.Sepo.Event;
using BSL.v41.Logic.Environment.LaserMessage.Sepo.Home.Laser;
using BSL.v41.Supercell.Titan.CommonUtils;
using BSL.v41.Supercell.Titan.CommonUtils.Utils;
using BSL.v41.Titan.Utilities;
using BSL.v41.Titan.Utilities.Sepo;
using BSL.v41.Tools.LaserCsv;
using BSL.v41.Tools.LaserCsv.Manufacturer.Laser;

namespace BSL.v41.Logic.Dynamic;

public static class DynamicServerParameters
{
    public static Dictionary<int, EventData> Event1DataMassive { get; private set; } = null!;
    public static Dictionary<int, EventData> Event2DataMassive { get; private set; } = null!;

    public static long DefaultSecTimeForUpdate { get; set; }
    public static string PathToJson { get; set; } = null!;

    public static void InitializeRotateEvents()
    {
        Event1DataMassive = new Dictionary<int, EventData>();
        Event2DataMassive = new Dictionary<int, EventData>();

        var json = File.ReadAllText(PathToJson);
        dynamic data = JsonConvert.DeserializeObject(json)!;

        foreach (var slot in data.slots)
        {
            int slotNumber = slot.slot;
            string gameMode = slot.game_modes[0];

            var dataTable = LogicDataTables.GetAllDataFromCsvById(CsvHelperTable.Locations.GetId());
            {
                new Random().Shuffle(dataTable);

                var allowedLocations = dataTable.Cast<LogicLocationData?>()
                    .Count(location => location!.GetGameModeVariation() == gameMode);
                if (allowedLocations <= 1) continue;
                var locationSetted = false;

                while (!locationSetted)
                    foreach (var locateData in dataTable)
                    {
                        if (locationSetted) break;

                        var locate = (LogicLocationData)locateData;

                        if (!Helper.GetChanceByPercentage(40) || locate.GetGameModeVariation() != gameMode ||
                            locate.GetDisabled()) continue;

                        Event1DataMassive.Add(slotNumber - 1, new EventData(
                            (int)new TimeHelper(LogicTimeUtil.GetTimestamp()).AddSeconds(
                                (int)DefaultSecTimeForUpdate),
                            slotNumber, locate.GetInstanceId(), 0));
                        Event2DataMassive.Add(slotNumber - 1, new EventData(
                            (int)new TimeHelper(LogicTimeUtil.GetTimestamp()).AddSeconds(
                                (int)DefaultSecTimeForUpdate),
                            slotNumber, locate.GetInstanceId(), 0, true));

                        locationSetted = true;
                    }
            }
        }
    }

    public static void EventRotator()
    {
        new Thread(() =>
        {
            while (true)
            {
                Thread.Sleep((int)DefaultSecTimeForUpdate * 1000);

                var json = File.ReadAllText(PathToJson);
                dynamic data = JsonConvert.DeserializeObject(json)!;

                foreach (var slot in data.slots)
                {
                    int slotNumber = slot.slot;
                    string gameMode = slot.game_modes[new Random().Next(0, slot.game_modes.Count)];

                    var dataTable = LogicDataTables.GetAllDataFromCsvById(CsvHelperTable.Locations.GetId());
                    {
                        new Random().Shuffle(dataTable);

                        var allowedLocations = dataTable.Cast<LogicLocationData?>()
                            .Count(location => location!.GetGameModeVariation() == gameMode);
                        if (allowedLocations <= 1) continue;
                        var locationSetted = false;

                        while (!locationSetted)
                            foreach (var locateData in dataTable)
                            {
                                if (locationSetted) break;

                                var locate = (LogicLocationData)locateData;

                                if (!Helper.GetChanceByPercentage(40) || locate.GetGameModeVariation() != gameMode ||
                                    locate.GetDisabled())
                                    continue;

                                Event1DataMassive[slotNumber - 1] = new EventData(
                                    (int)new TimeHelper(LogicTimeUtil.GetTimestamp()).AddSeconds(
                                        (int)DefaultSecTimeForUpdate),
                                    slotNumber, locate.GetInstanceId(), 0);
                                Event2DataMassive[slotNumber - 1] = new EventData(
                                    (int)new TimeHelper(LogicTimeUtil.GetTimestamp()).AddSeconds(
                                        (int)DefaultSecTimeForUpdate),
                                    slotNumber, locate.GetInstanceId(), 0, true);

                                locationSetted = true;
                            }
                    }
                }

                foreach (var gameListener in IdentifierListener.GetLogicGameListeners())
                {
                    var v1 = new LogicConfData(Databases.AccountDatabase.LoadAccount(gameListener.Key));

                    var availableServerCommandMessage = new AvailableServerCommandMessage();
                    {
                        var logicDayChangedCommand = new LogicDayChangedCommand(v1);
                        {
                            availableServerCommandMessage.SetServerCommand(logicDayChangedCommand);
                        }
                    }

                    gameListener.Value.Send(availableServerCommandMessage);
                }
            }
        }).Start();
    }

public static void ShopRotator(string timeZoneId)
{
    new Thread(() =>
    {
        while (true)
        {
            var localTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow,
                TimeZoneInfo.FindSystemTimeZoneById(timeZoneId));

            if (localTime.Hour != 11) continue;

            for (var i = 1; i <= Databases.AccountDatabase.GetMaxAccountId(); i++)
            {
                var accountModel = Databases.AccountDatabase.LoadAccount(i);
                // todo.
            }
        }
    }).Start();
}



}
