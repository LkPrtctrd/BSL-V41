using BSL.v41.Tools.LaserCsv.Data;
using BSL.v41.Tools.LaserCsv.Portal;

namespace BSL.v41.Tools.LaserCsv.Manufacturer.Laser;

public class LogicThemeData(CsvRow row, LogicDataTable table) : LogicData(row, table)
{
    private string _exportName = null!;
    private string _fileName = null!;
    private string _particleExportName = null!;
    private string _particleFileName = null!;
    private string _particleStyle = null!;
    private int _particleVariations;
    private string _themeMusic = null!;
    private bool _useInLevelSelection;

    // LogicThemeData.

    public override void CreateReferences()
    {
        _fileName = GetValue("FileName", 0);
        _exportName = GetValue("ExportName", 0);
        _particleFileName = GetValue("ParticleFileName", 0);
        _particleExportName = GetValue("ParticleExportName", 0);
        _particleStyle = GetValue("ParticleStyle", 0);
        _particleVariations = GetIntegerValue("ParticleVariations", 0);
        _themeMusic = GetValue("ThemeMusic", 0);
        _useInLevelSelection = GetBooleanValue("UseInLevelSelection", 0);
    }

    public string GetFileName()
    {
        return _fileName;
    }

    public string GetExportName()
    {
        return _exportName;
    }

    public string GetParticleFileName()
    {
        return _particleFileName;
    }

    public string GetParticleExportName()
    {
        return _particleExportName;
    }

    public string GetParticleStyle()
    {
        return _particleStyle;
    }

    public int GetParticleVariations()
    {
        return _particleVariations;
    }

    public string GetThemeMusic()
    {
        return _themeMusic;
    }

    public bool GetUseInLevelSelection()
    {
        return _useInLevelSelection;
    }
}