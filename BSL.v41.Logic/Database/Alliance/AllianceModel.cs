using System.Reflection;
using Newtonsoft.Json;
using BSL.v41.Logic.Environment.LaserMessage.Sepo.Alliance;

namespace BSL.v41.Logic.Database.Alliance;

public class AllianceModel(string jsonAllianceModel = null!)
{
    private readonly string? _jsonAllianceModel = jsonAllianceModel;
    [JsonProperty] private int _allianceBadgeData; // private. Not use!

    [JsonProperty] private string? _allianceCreatedDate; // private. Not use!
    [JsonProperty] private string? _allianceDescription; // private. Not use!
    [JsonProperty] private bool _allianceFamilyType; // private. Not use!
    [JsonProperty] private long _allianceId; // private. Not use!
    [JsonProperty] private int _allianceLanguage; // private. Not use!

    [JsonProperty]
    private Dictionary<long, AllianceMemberEntry?>? _allianceMemberEntryList = new(); // private. Not use!

    [JsonProperty] private string? _allianceName; // private. Not use!
    [JsonProperty] private int _allianceRegion; // private. Not use!
    [JsonProperty] private int _allianceRequiredScore; // private. Not use!
    [JsonProperty] private int _allianceType; // private. Not use!

    public object GetFieldValueByAllianceStructureParameterFromAllianceModel(AllianceStructure allianceStructure)
    {
        object fieldValue = null!;
        {
            var fieldName = AllianceHelper.GetNormalParameterNameFromAllianceStructure(allianceStructure);
            {
                var fields = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
                {
                    foreach (var field in fields)
                        try
                        {
                            if (field.Name == fieldName) fieldValue = field.GetValue(this)!;
                        }
                        catch (Exception)
                        {
                            // ignored.
                        }
                }
            }
        }

        return fieldValue;
    }

    public void SetFieldValueByAllianceStructureParameterFromAllianceModel(AllianceStructure allianceStructure,
        object newValue)
    {
        var fieldName = AllianceHelper.GetNormalParameterNameFromAllianceStructure(allianceStructure);
        {
            var fields = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            {
                foreach (var field in fields)
                    try
                    {
                        if (field.Name != fieldName) continue;

                        if (field.FieldType == typeof(int))
                            if (newValue is long)
                                newValue = (int)newValue;

                        if (field.FieldType == typeof(long))
                            if (newValue is int)
                                newValue = (long)newValue;

                        field.SetValue(this, newValue);
                    }
                    catch (Exception)
                    {
                        // ignored.
                    }
            }
        }

        Databases.AllianceDatabase.OnAssistByAllianceInFile(this, true, _allianceId);
    }

    protected void ReplaceFieldValueByAllianceStructureParameter(AllianceModel allianceModel,
        AllianceStructure allianceStructure, object newValue)
    {
        var fieldName = AllianceHelper.GetNormalParameterNameFromAllianceStructure(allianceStructure);
        {
            var fields = allianceModel.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            {
                foreach (var field in fields)
                    try
                    {
                        if (field.Name == fieldName) field.SetValue(allianceModel, newValue);
                    }
                    catch (Exception)
                    {
                        // ignored.
                    }
            }
        }
    }

    protected bool ReplaceFieldValueByFieldName(AllianceModel allianceModel, string fieldName, object newValue)
    {
        {
            var fields = allianceModel.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            {
                foreach (var field in fields)
                    try
                    {
                        if (field.Name != fieldName) continue;
                        field.SetValue(allianceModel, newValue);

                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
            }
        }

        return false;
    }

    protected AllianceModel AbstractingAllianceModelFromRootAllianceModel(AllianceModel allianceModel,
        AllianceModel rootAllianceModel, bool thisClassAbstracted = true)
    {
        if (thisClassAbstracted)
        {
            var fieldsThis = allianceModel.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            {
                foreach (var field in fieldsThis)
                    try
                    {
                        object objectValue = null!;

                        foreach (var fieldRoot in rootAllianceModel.GetType()
                                     .GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
                            try
                            {
                                if (field.Name == fieldRoot.Name) objectValue = fieldRoot.GetValue(rootAllianceModel)!;
                            }
                            catch (Exception)
                            {
                                // ignored.
                            }

                        field.SetValue(allianceModel, objectValue);
                    }
                    catch (Exception)
                    {
                        // ignored.
                    }
            }
        }

        var newAllianceModel = new AllianceModel();
        {
            var fields = newAllianceModel.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            {
                foreach (var field in fields)
                    try
                    {
                        object objectValue = null!;

                        foreach (var fieldRoot in rootAllianceModel.GetType()
                                     .GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
                            try
                            {
                                if (field.Name == fieldRoot.Name) objectValue = fieldRoot.GetValue(rootAllianceModel)!;
                            }
                            catch (Exception)
                            {
                                // ignored.
                            }

                        field.SetValue(allianceModel, objectValue);
                    }
                    catch (Exception)
                    {
                        // ignored.
                    }
            }
        }

        return newAllianceModel;
    }

    public AllianceModel GetAllianceModel()
    {
        return this;
    }

    public long GetAllianceId()
    {
        return _allianceId;
    }

    public string AllianceModelToJson()
    {
        return JsonConvert.SerializeObject(GetAllianceModel());
    }

    public AllianceModel JsonToAllianceModel(string jsonAllianceModel = null!)
    {
        return JsonConvert.DeserializeObject<AllianceModel>(_jsonAllianceModel ?? jsonAllianceModel)!;
    }
}