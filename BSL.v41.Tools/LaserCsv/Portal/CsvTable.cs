using BSL.v41.Titan.Graphic;
using BSL.v41.Titan.Mathematical.Massive;

namespace BSL.v41.Tools.LaserCsv.Portal;

public class CsvTable(CsvNode node, int size)
{
    private readonly LogicArrayList<CsvColumn> _columnList = new();
    private readonly LogicArrayList<string> _columnNameList = new();
    private readonly LogicArrayList<CsvRow> _rowList = new();

    public void AddAndConvertValue(string value, int columnIndex)
    {
        var column = _columnList[columnIndex];

        if (!string.IsNullOrEmpty(value))
            switch (column.ColumnType)
            {
                case -1:
                case 0:
                {
                    column.AddStringValue(value);
                    break;
                }

                case 1:
                {
                    column.AddIntegerValue(int.Parse(value));
                    break;
                }

                case 2:
                {
                    if (bool.TryParse(value, out var booleanValue)) column.AddBooleanValue(booleanValue);

                    break;
                }
            }
        else
            column.AddEmptyValue();
    }

    public void AddColumn(string name)
    {
        _columnNameList.Add(name);
    }

    public void AddColumnType(int type)
    {
        _columnList.Add(new CsvColumn(type, size));
    }

    public void AddRow(CsvRow row)
    {
        _rowList.Add(row);
    }

    public void ColumnNamesLoaded()
    {
        _columnList.EnsureCapacity(_columnNameList.Count);
    }

    public void CreateRow()
    {
        _rowList.Add(new CsvRow(this));
    }

    public int GetArraySizeAt(CsvRow row, int columnIdx)
    {
        if (_rowList.Count <= 0) return 0;

        var rowIdx = _rowList.IndexOf(row);
        if (rowIdx == -1) return 0;

        var column = _columnList[columnIdx];

        return column.GetArraySize(_rowList[rowIdx].GetRowOffset(),
            rowIdx + 1 >= _rowList.Count ? column.GetSize() : _rowList[rowIdx + 1].GetRowOffset());
    }

    public string GetColumnName(int idx)
    {
        return _columnNameList[idx];
    }

    public int GetColumnIndexByName(string name)
    {
        return _columnNameList.IndexOf(name);
    }

    public int GetColumnCount()
    {
        return _columnNameList.Count;
    }

    public int GetColumnRowCount()
    {
        return _columnList[0].GetSize();
    }

    public int GetColumnTypeCount()
    {
        return _columnList.Count;
    }

    public string GetFileName()
    {
        return node.GetFileName();
    }

    public bool GetBooleanValue(string name, int index)
    {
        return GetBooleanValueAt(_columnNameList.IndexOf(name), index);
    }

    public bool GetBooleanValueAt(int columnIndex, int index)
    {
        return columnIndex != -1 && _columnList[columnIndex].GetBooleanValue(index);
    }

    public int GetIntegerValue(string name, int index)
    {
        return GetIntegerValueAt(_columnNameList.IndexOf(name), index);
    }

    public int GetIntegerValueAt(int columnIndex, int index)
    {
        if (columnIndex == -1) return 0;
        var value = _columnList[columnIndex].GetIntegerValue(index);

        if (value == 0x7fffffff) value = 0;

        return value;
    }

    public string GetValue(string name, int index)
    {
        return GetValueAt(_columnNameList.IndexOf(name), index);
    }

    public string GetValueAt(int columnIndex, int index)
    {
        return columnIndex != -1 ? _columnList[columnIndex].GetStringValue(index) : string.Empty;
    }

    public CsvRow GetRowAt(int index)
    {
        return _rowList[index];
    }

    public CsvColumn GetCsvColumn(int index)
    {
        return _columnList[index];
    }

    public int GetRowCount()
    {
        return _rowList.Count;
    }

    public void ValidateColumnTypes()
    {
        if (_columnNameList.Count != _columnList.Count)
            ConsoleLogger.WriteTextWithPrefix(ConsoleLogger.Prefixes.Warn,
                $"Column name count {_columnNameList.Count}, column type count {_columnList.Count}, file {GetFileName()}.");
    }
}