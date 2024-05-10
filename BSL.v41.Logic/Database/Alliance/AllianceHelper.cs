using System.Text.RegularExpressions;
using BSL.v41.Supercell.Titan.CommonUtils;

namespace BSL.v41.Logic.Database.Alliance;

public partial class AllianceHelper
{
    public static string GetNormalParameterNameFromAllianceStructure(AllianceStructure allianceStructure)
    {
        return "_" + Helper.ConvertStringToUnderscore(Enum.GetName(typeof(AllianceStructure), allianceStructure)!);
    }

    public static string GetNormalParameterNameToAllianceModelByCamelCaseFromAllianceStructure(string parameter)
    {
        if (!MyRegex().IsMatch(parameter)) return parameter;
        return char.ToLower(parameter[0]) + parameter[1..];
    }

    [GeneratedRegex("[a-z]+[A-Za-z]*")]
    private static partial Regex MyRegex();
}