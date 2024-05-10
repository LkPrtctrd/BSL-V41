using BSL.v41.Titan.Mathematical.Massive;

namespace BSL.v41.Tools.LaserCsv.Portal;

public class CsvColumn
{
    private readonly LogicArrayList<byte> _boolValue;
    private readonly LogicArrayList<int> _intValue;
    private readonly LogicArrayList<string> _stringValue;

    public CsvColumn(int type, int size)
    {
        ColumnType = type;

        _intValue = new LogicArrayList<int>();
        _boolValue = new LogicArrayList<byte>();
        _stringValue = new LogicArrayList<string>();

        switch (type)
        {
            case -1:
            case 0:
            {
                _stringValue.EnsureCapacity(size);
                break;
            }

            case 1:
            {
                _intValue.EnsureCapacity(size);
                break;
            }

            case 2:
            {
                _boolValue.EnsureCapacity(size);
                break;
            }
        }
    }

    public int ColumnType { get; }

    public void AddEmptyValue()
    {
        switch (ColumnType)
        {
            case -1:
            case 0:
            {
                _stringValue.Add(string.Empty);
                break;
            }

            case 1:
            {
                _intValue.Add(0x7fffffff);
                break;
            }

            case 2:
            {
                _boolValue.Add(2);
                break;
            }
        }
    }

    public void AddBooleanValue(bool value)
    {
        _boolValue.Add((byte)(value ? 1 : 0));
    }

    public void AddIntegerValue(int value)
    {
        _intValue.Add(value);
    }

    public void AddStringValue(string value)
    {
        _stringValue.Add(value);
    }

    public int GetArraySize(int startOffset, int endOffset)
    {
        switch (ColumnType)
        {
            default:
            {
                for (var i = endOffset - 1; i + 1 > startOffset; i--)
                    if (_stringValue[i].Length > 0)
                        return i - startOffset + 1;

                break;
            }
            case 1:
            {
                for (var i = endOffset - 1; i + 1 > startOffset; i--)
                    if (_intValue[i] != 0x7fffffff)
                        return i - startOffset + 1;

                break;
            }

            case 2:
            {
                for (var i = endOffset - 1; i + 1 > startOffset; i--)
                    if (_boolValue[i] != 2)
                        return i - startOffset + 1;

                break;
            }
        }

        return 0;
    }

    public bool GetBooleanValue(int index)
    {
        return _boolValue[index] == 1;
    }

    public int GetIntegerValue(int index)
    {
        return _intValue[index];
    }

    public string GetStringValue(int index)
    {
        return _stringValue[index];
    }

    public int GetSize()
    {
        switch (ColumnType)
        {
            case -1:
            case 0:
            {
                return _stringValue.Count;
            }

            case 1:
            {
                return _intValue.Count;
            }

            case 2:
            {
                return _boolValue.Count;
            }
        }

        return 0;
    }
}