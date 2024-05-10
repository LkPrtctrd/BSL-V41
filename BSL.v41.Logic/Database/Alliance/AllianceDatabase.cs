using Newtonsoft.Json;
using BSL.v41.Logic.Environment.LaserMessage.Sepo.Alliance;
using BSL.v41.Titan.Graphic;

namespace BSL.v41.Logic.Database.Alliance;

public class AllianceDatabase : AllianceModel
{
    private readonly string? _filePath;
    private Dictionary<long, AllianceModel> _onRamBase;

    public AllianceDatabase(string? filePath, bool loadByBackup = false)
    {
        _filePath = filePath;
        _onRamBase = new Dictionary<long, AllianceModel>();
    }

    public void CyclingLoadDataFromFile(int perSec)
    {
        Task.Run(() =>
        {
            while (true)
            {
                Thread.Sleep(perSec * 1000);
                {
                    LoadDataFromFile(true);
                }
            }
        });
    }

    public void LoadDataFromFile(bool reloaded = false)
    {
        try
        {
            var data = File.ReadAllText(_filePath!);
            {
                _onRamBase = ConvertJsonToDictionary(data);
            }

            ConsoleLogger.WriteTextWithPrefix(!reloaded ? ConsoleLogger.Prefixes.Start : ConsoleLogger.Prefixes.Load,
                $"Database-element {(reloaded ? "reloaded" : "started")}! Information: element name: {GetType().Name}; " +
                $"database buffer length: {_onRamBase.Count}.");
        }
        catch (Exception exception)
        {
            ConsoleLogger.WriteTextWithPrefix(ConsoleLogger.Prefixes.Error, exception);
        }
    }

    public AllianceModel CreateAlliance(AllianceModel allianceModel, long allianceId = -1)
    {
        allianceId = allianceId < 0 ? GetNewAllianceId() : allianceId;
        {
            try
            {
                OnAssistByAllianceInFile(allianceModel, false, allianceId);
                {
                    allianceModel.SetFieldValueByAllianceStructureParameterFromAllianceModel(
                        AllianceStructure.AllianceId,
                        allianceId);
                }
            }
            catch
            {
                // ignored.
            }
        }

        return allianceModel;
    }

    public AllianceModel LoadAlliance(long id)
    {
        try
        {
            return _onRamBase[id];
        }
        catch (Exception)
        {
            // ignored.
        }

        return null!;
    }

    public AllianceModel SetAllianceParameter(long allianceId, AllianceStructure allianceStructure, object value)
    {
        var allianceModel = LoadAlliance(allianceId);
        if (allianceModel == null!) return null!;

        var parameterName = AllianceHelper.GetNormalParameterNameFromAllianceStructure(allianceStructure);
        {
            if (allianceModel != null!) ReplaceFieldValueByFieldName(allianceModel, parameterName, value);
        }

        return OnAssistByAllianceInFile(allianceModel!, true, allianceId);
    }

    public object GetAllianceParameter(long allianceId, AllianceStructure allianceStructure)
    {
        var allianceModel = LoadAlliance(allianceId);
        {
            if (allianceModel != null!)
                return allianceModel.GetFieldValueByAllianceStructureParameterFromAllianceModel(allianceStructure);
        }

        return null!;
    }

    public AllianceModel OnAssistByAllianceInFile(AllianceModel allianceModel, bool wasReplace, long allianceId = -1)
    {
        allianceId = Convert.ToInt64(allianceId < 0
            ? allianceModel.GetFieldValueByAllianceStructureParameterFromAllianceModel(AllianceStructure.AllianceId)
            : allianceId);

        var jsonData = File.ReadAllText(_filePath!);
        {
            var allianceData = JsonConvert.DeserializeObject<Dictionary<long, AllianceModel>>(jsonData);
            {
                var newAllianceModel = new AllianceModel();
                {
                    switch (wasReplace)
                    {
                        case true:
                            _onRamBase[allianceId] = allianceModel;
                            allianceData![allianceId] = allianceModel;
                        {
                            AbstractingAllianceModelFromRootAllianceModel(newAllianceModel, allianceModel);
                        }
                            break;
                        case false:
                            _onRamBase.Add(allianceId, allianceModel);
                        {
                            allianceData!.Add(Convert.ToInt32(allianceId.ToString()), allianceModel);
                        }
                            break;
                    }
                }

                if (allianceData == null!) return allianceModel;

                File.WriteAllText(_filePath!, JsonConvert.SerializeObject(allianceData, Formatting.Indented));
                return newAllianceModel;
            }
        }
    }

    public string ConvertDictionaryToJson(Dictionary<long, AllianceModel>? onRamBase = null!)
    {
        return JsonConvert.SerializeObject(onRamBase ?? _onRamBase);
    }

    public Dictionary<long, AllianceModel> ConvertJsonToDictionary(string json, bool localUs = false)
    {
        return localUs ? _onRamBase : JsonConvert.DeserializeObject<Dictionary<long, AllianceModel>>(json)!;
    }

    public long GetMaxAllianceId()
    {
        return _onRamBase.Count == 0 ? 0 : _onRamBase.Keys.Max();
    }

    public long GetNewAllianceId()
    {
        return GetMaxAllianceId() + 1;
    }

    public List<AllianceModel> GetRandomAllianceModels(int maxRequiredTrophies)
    {
        var setAlliances = new List<AllianceModel>();
        {
            setAlliances.AddRange(from allianceModel in _onRamBase
                let c = (Dictionary<long, AllianceMemberEntry>)
                    allianceModel.Value.GetFieldValueByAllianceStructureParameterFromAllianceModel(
                        AllianceStructure.AllianceMemberEntryList)
                where c.Count > 0 &&
                      (int)allianceModel.Value.GetFieldValueByAllianceStructureParameterFromAllianceModel(
                          AllianceStructure.AllianceRequiredScore) <= maxRequiredTrophies
                select allianceModel.Value);
        }

        return setAlliances;
    }

    public List<AllianceModel> GetAllianceModelsByName(string name)
    {
        return (from allianceModel in _onRamBase
            let deltaName =
                allianceModel.Value
                    .GetFieldValueByAllianceStructureParameterFromAllianceModel(AllianceStructure.AllianceName)
                    .ToString()!
            where deltaName.Contains(name, StringComparison.CurrentCultureIgnoreCase)
            where ((Dictionary<long, AllianceMemberEntry>)
                allianceModel.Value.GetFieldValueByAllianceStructureParameterFromAllianceModel(
                    AllianceStructure.AllianceMemberEntryList)).Count > 0
            select allianceModel.Value).ToList();
    }
}