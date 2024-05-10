using BSL.v41.Titan.Mathematical;

namespace BSL.v41.Tools.LaserCsv.Portal;

public class CsvRow(CsvTable table)
{
    private readonly int _rowOffset = table.GetColumnRowCount();

    public int GetBiggestArraySize(string column)
    {
        return GetColumnIndexByName(column) == -1 ? 0 : table.GetArraySizeAt(this, GetColumnIndexByName(column));
    }

    public int GetBiggestArraySize()
    {
        var columnCount = table.GetColumnCount();
        var maxSize = 0;

        for (var i = 0; i < columnCount; i++) maxSize = LogicMath.Max(table.GetArraySizeAt(this, i), maxSize);

        return maxSize;
    }

    public int GetColumnCount()
    {
        return table.GetColumnCount();
    }

    public int GetColumnIndexByName(string name)
    {
        return table.GetColumnIndexByName(name);
    }

    public bool GetBooleanValue(string columnName, int index)
    {
        return table.GetBooleanValue(columnName, _rowOffset + index);
    }

    public bool GetBooleanValueAt(int columnIndex, int index)
    {
        return table.GetBooleanValueAt(columnIndex, _rowOffset + index);
    }

    public bool GetClampedBooleanValue(string columnName, int index)
    {
        var columnIndex = GetColumnIndexByName(columnName);

        if (columnIndex == -1) return false;
        var arraySize = table.GetArraySizeAt(this, columnIndex);

        if (index >= arraySize || arraySize < 1) index = LogicMath.Max(arraySize - 1, 0);

        return table.GetBooleanValueAt(columnIndex, _rowOffset + index);
    }

    public int GetIntegerValue(string columnName, int index)
    {
        return table.GetIntegerValue(columnName, _rowOffset + index);
    }

    public int GetIntegerValueAt(int columnIndex, int index)
    {
        return table.GetIntegerValueAt(columnIndex, _rowOffset + index);
    }

    public int GetClampedIntegerValue(string columnName, int index)
    {
        var columnIndex = GetColumnIndexByName(columnName);

        if (columnIndex == -1) return 0;
        var arraySize = table.GetArraySizeAt(this, columnIndex);

        if (index >= arraySize || arraySize < 1) index = LogicMath.Max(arraySize - 1, 0);

        return table.GetIntegerValueAt(columnIndex, _rowOffset + index);
    }

    public string GetValue(string columnName, int index)
    {
        return table.GetValue(columnName, _rowOffset + index);
    }

    public string GetValueAt(int columnIndex, int index)
    {
        return table.GetValueAt(columnIndex, _rowOffset + index);
    }

    public string GetClampedValue(string columnName, int index)
    {
        var columnIndex = GetColumnIndexByName(columnName);
        if (columnIndex == -1) return string.Empty;

        var arraySize = table.GetArraySizeAt(this, columnIndex);
        if (index >= arraySize || arraySize < 1) index = LogicMath.Max(arraySize - 1, 0);

        return table.GetValueAt(columnIndex, _rowOffset + index);
    }

    public string GetName()
    {
        return table.GetValueAt(0, _rowOffset);
    }

    public int GetRowOffset()
    {
        return _rowOffset;
    }
}