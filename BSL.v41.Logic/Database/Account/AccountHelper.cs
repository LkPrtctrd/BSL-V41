using System.Text.RegularExpressions;
using BSL.v41.Supercell.Titan.CommonUtils;

namespace BSL.v41.Logic.Database.Account;

public static partial class AccountHelper
{
    public static string GetNormalParameterNameFromAccountStructure(AccountStructure accountStructure)
    {
        return "_" + Helper.ConvertStringToUnderscore(Enum.GetName(typeof(AccountStructure), accountStructure)!);
    }

    public static string GetNormalParameterNameToAccountModelByCamelCaseFromAccountStructure(string parameter)
    {
        if (!MyRegex().IsMatch(parameter)) return parameter;
        return char.ToLower(parameter[0]) + parameter[1..];
    }

    [GeneratedRegex("[a-z]+[A-Za-z]*")]
    private static partial Regex MyRegex();
}