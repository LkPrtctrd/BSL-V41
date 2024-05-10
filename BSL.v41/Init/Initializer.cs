using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json.Linq;
using BSL.v41.General.Cloud;
using BSL.v41.General.NetIsland;
using BSL.v41.Logic.Database;
using BSL.v41.Logic.Database.Account;
using BSL.v41.Logic.Database.Alliance;
using BSL.v41.Logic.Database.IntraSigned;
using BSL.v41.Logic.Dynamic;
using BSL.v41.StaticService.Laser;
using BSL.v41.Supercell.Titan.CommonUtils.Utils;
using BSL.v41.Supercell.Titan.CommonUtils.Сensorship;
using BSL.v41.Tools.LaserCsv;
using BSL.v41.Tools.LaserCsv.Manufacturer.Laser;
using BSL.v41.Tools.LaserData;

namespace BSL.v41.Init;

public abstract class Initializer
{
    protected static void Start()
    {
        Saver.SearchTimeFactor = 1;
        Databases.AccountDatabase = new AccountDatabase("SaveBase/account_database.json");
        Databases.AllianceDatabase = new AllianceDatabase("SaveBase/alliance_database.json");
        Databases.NotificationIntraSignedDatabase =
            new NotificationIntraSignedDatabase("SaveBase/notification_inta_signed_database.json");
        Fingerprint.Patch = "SaveBase/Assets/fingerprint.json";
        LogicDataTables.CreateReferences("SaveBase/Assets/csv_logic/");
        ProfanityManager.Initialize("SaveBase/profanity.txt");
        GlobalStaticCloud.PragmaAndServerEnvironment = "dev";
        DynamicServerParameters.PathToJson = "SaveBase/event_structure.json";
        DynamicServerParameters.DefaultSecTimeForUpdate = 30;
        DynamicServerParameters.InitializeRotateEvents();
        DynamicServerParameters.EventRotator();
        DynamicServerParameters.ShopRotator();

        var ownMatchmakingManager = new OwnMatchmakingManager();
        {
            ownMatchmakingManager.Initialize();
        }

        Saver.OwnMatchmakingManager = ownMatchmakingManager;

        InitMaps();
    }

    [SuppressMessage("ReSharper.DPA", "DPA0002: Excessive memory allocations in SOH",
        MessageId = "type: BSL.v41.Tools.LaserCsv.Data.LogicData[]; size: 231MB")]
    private static void InitMaps()
    {
        var obj = File.ReadAllText("SaveBase/map_structure.json");
        var mapDataList = JArray.Parse(obj);

        foreach (var jToken in mapDataList)
        {
            var mapData = (JObject)jToken;
            var name = "";
            {
                foreach (var data in LogicDataTables.GetAllDataFromCsvById(CsvHelperTable.Locations.GetId()))
                {
                    var d1 = (LogicLocationData)data;
                    if (mapData.GetValue("Map")!.ToString() == d1.GetMap()) name = d1.GetLocationTheme();
                }
            }

            var a1 = LogicDataTables.GetLocationThemeByName(name);
            if (a1 != null) 
                Saver.MapStructureDictionary.Add(mapData.GetValue("Map")!.ToString(),
                    [a1.GetMapHeight(), a1.GetMapWidth(), mapData.GetValue("Data")!.ToString()]);
        }
    }
}