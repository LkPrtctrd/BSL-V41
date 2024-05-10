using BSL.v41.Tools.LaserCsv.Data;
using BSL.v41.Tools.LaserCsv.Portal;

namespace BSL.v41.Tools.LaserCsv.Manufacturer.Laser;

public class LogicSkinData(CsvRow row, LogicDataTable table) : LogicData(row, table)
{
    private string _blueSpecular = null!;
    private string _blueTexture = null!;
    private int _campaign;
    private string _communityCredit = null!;
    private string _conf = null!;
    private int _costGems;
    private int _costLegendaryTrophies;
    private string _features = null!;
    private string _materialsFile = null!;
    private int _obtainType;
    private int _obtainTypeCn;
    private string _petSkin = null!;
    private string _petSkin2 = null!;
    private string _redSpecular = null!;
    private string _redTexture = null!;
    private string _shopTid = null!;

    // LogicSkinData.

    public override void CreateReferences()
    {
        _conf = GetValue("Conf", 0);
        _campaign = GetIntegerValue("Campaign", 0);
        _obtainType = GetIntegerValue("ObtainType", 0);
        _obtainTypeCn = GetIntegerValue("ObtainTypeCN", 0);
        _petSkin = GetValue("PetSkin", 0);
        _petSkin2 = GetValue("PetSkin2", 0);
        _costLegendaryTrophies = GetIntegerValue("CostLegendaryTrophies", 0);
        _costGems = GetIntegerValue("CostGems", 0);
        _shopTid = GetValue("ShopTID", 0);
        _features = GetValue("Features", 0);
        _communityCredit = GetValue("CommunityCredit", 0);
        _materialsFile = GetValue("MaterialsFile", 0);
        _blueTexture = GetValue("BlueTexture", 0);
        _redTexture = GetValue("RedTexture", 0);
        _blueSpecular = GetValue("BlueSpecular", 0);
        _redSpecular = GetValue("RedSpecular", 0);
    }

    public string GetConf()
    {
        return _conf;
    }

    public int GetCampaign()
    {
        return _campaign;
    }

    public int GetObtainType()
    {
        return _obtainType;
    }

    public int GetObtainTypeCn()
    {
        return _obtainTypeCn;
    }

    public string GetPetSkin()
    {
        return _petSkin;
    }

    public string GetPetSkin2()
    {
        return _petSkin2;
    }

    public int GetCostLegendaryTrophies()
    {
        return _costLegendaryTrophies;
    }

    public int GetCostGems()
    {
        return _costGems;
    }

    public string GetShopTid()
    {
        return _shopTid;
    }

    public string GetFeatures()
    {
        return _features;
    }

    public string GetCommunityCredit()
    {
        return _communityCredit;
    }

    public string GetMaterialsFile()
    {
        return _materialsFile;
    }

    public string GetBlueTexture()
    {
        return _blueTexture;
    }

    public string GetRedTexture()
    {
        return _redTexture;
    }

    public string GetBlueSpecular()
    {
        return _blueSpecular;
    }

    public string GetRedSpecular()
    {
        return _redSpecular;
    }
}