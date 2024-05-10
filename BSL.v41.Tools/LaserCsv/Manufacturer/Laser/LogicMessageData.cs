using BSL.v41.Tools.LaserCsv.Data;
using BSL.v41.Tools.LaserCsv.Portal;

namespace BSL.v41.Tools.LaserCsv.Manufacturer.Laser;

public class LogicMessageData(CsvRow row, LogicDataTable table) : LogicData(row, table)
{
    private bool _ageGated;
    private string _bubbleOverrideTid = null!;
    private bool _disabled;
    private string _exportName = null!;
    private string _fileName = null!;
    private int _messageType;
    private int _quickEmojiType;
    private int _sortPriority;

    // LogicMessageData.

    public override void CreateReferences()
    {
        _bubbleOverrideTid = GetValue("BubbleOverrideTID", 0);
        _disabled = GetBooleanValue("Disabled", 0);
        _messageType = GetIntegerValue("MessageType", 0);
        _fileName = GetValue("FileName", 0);
        _exportName = GetValue("ExportName", 0);
        _quickEmojiType = GetIntegerValue("QuickEmojiType", 0);
        _sortPriority = GetIntegerValue("SortPriority", 0);
        _ageGated = GetBooleanValue("AgeGated", 0);
    }

    public string GetBubbleOverrideTid()
    {
        return _bubbleOverrideTid;
    }

    public bool GetDisabled()
    {
        return _disabled;
    }

    public int GetMessageType()
    {
        return _messageType;
    }

    public string GetFileName()
    {
        return _fileName;
    }

    public string GetExportName()
    {
        return _exportName;
    }

    public int GetQuickEmojiType()
    {
        return _quickEmojiType;
    }

    public int GetSortPriority()
    {
        return _sortPriority;
    }

    public bool GetAgeGated()
    {
        return _ageGated;
    }
}