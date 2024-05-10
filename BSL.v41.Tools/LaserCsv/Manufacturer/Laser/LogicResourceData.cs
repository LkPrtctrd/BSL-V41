using BSL.v41.Tools.LaserCsv.Data;
using BSL.v41.Tools.LaserCsv.Portal;

namespace BSL.v41.Tools.LaserCsv.Manufacturer.Laser;

public class LogicResourceData(CsvRow row, LogicDataTable table) : LogicData(row, table)
{
    private int _cap;
    private string _collectEffect = null!;
    private string _iconSwf = null!;
    private bool _premiumCurrency;
    private string _rarity = null!;
    private int _textBlue;
    private int _textGreen;
    private int _textRed;
    private string _type = null!;

    // LogicResourceData.

    public override void CreateReferences()
    {
        _iconSwf = GetValue("IconSWF", 0);
        _collectEffect = GetValue("CollectEffect", 0);
        _type = GetValue("Type", 0);
        _rarity = GetValue("Rarity", 0);
        _premiumCurrency = GetBooleanValue("PremiumCurrency", 0);
        _textRed = GetIntegerValue("TextRed", 0);
        _textGreen = GetIntegerValue("TextGreen", 0);
        _textBlue = GetIntegerValue("TextBlue", 0);
        _cap = GetIntegerValue("Cap", 0);
    }

    public string GetIconSwf()
    {
        return _iconSwf;
    }

    public string GetCollectEffect()
    {
        return _collectEffect;
    }

    public new string GetType()
    {
        return _type;
    }

    public string GetRarity()
    {
        return _rarity;
    }

    public bool GetPremiumCurrency()
    {
        return _premiumCurrency;
    }

    public int GetTextRed()
    {
        return _textRed;
    }

    public int GetTextGreen()
    {
        return _textGreen;
    }

    public int GetTextBlue()
    {
        return _textBlue;
    }

    public int GetCap()
    {
        return _cap;
    }
}