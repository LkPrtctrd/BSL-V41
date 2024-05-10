using System.Reflection;
using BSL.v41.Titan.Mathematical.Data;
using BSL.v41.Tools.LaserCsv.Manufacturer;
using BSL.v41.Tools.LaserCsv.Portal;

namespace BSL.v41.Tools.LaserCsv.Data;

public class LogicData(CsvRow row, LogicDataTable table)
{
    private const int VillageType = 0;
    private readonly int _globalId = GlobalId.CreateGlobalId(table.GetTableIndex(), table.GetItemCount());

    public virtual void CreateReferences()
    {
        // CreateReferences.
    }

    public int GetArraySize(string column)
    {
        return row.GetBiggestArraySize();
    }

    public int GetDataType()
    {
        return table.GetTableIndex();
    }

    public int GetGlobalId()
    {
        return _globalId;
    }

    public int GetClassId()
    {
        return GlobalId.GetClassId(_globalId);
    }

    public int GetInstanceId()
    {
        return GlobalId.GetInstanceId(_globalId);
    }

    public int GetColumnIndex(string name)
    {
        return row.GetColumnIndexByName(name);
    }

    protected string GetDebuggerName()
    {
        return row.GetName() + " (" + table.GetTableName() + ")";
    }

    protected bool GetBooleanValue(string columnName, int index)
    {
        return row.GetBooleanValue(columnName, index);
    }

    protected bool GetClampedBooleanValue(string columnName, int index)
    {
        return row.GetClampedBooleanValue(columnName, index);
    }

    protected int GetIntegerValue(string columnName, int index)
    {
        return row.GetIntegerValue(columnName, index);
    }

    protected int GetClampedIntegerValue(string columnName, int index)
    {
        return row.GetClampedIntegerValue(columnName, index);
    }

    protected string GetValue(string columnName, int index)
    {
        return row.GetValue(columnName, index);
    }

    public string GetClampedValue(string columnName, int index)
    {
        return row.GetClampedValue(columnName, index);
    }

    public string GetName()
    {
        return row.GetName();
    }

    public void AutoLoadData()
    {
        foreach (var propertyInfo in GetType()
                     .GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
        {
            if (!propertyInfo.CanWrite) continue;
            if (propertyInfo.PropertyType.IsArray)
            {
                var elementType = propertyInfo.PropertyType.GetElementType();

                if (elementType == typeof(int))
                {
                    var arraySize = row.GetBiggestArraySize();
                    var array = new int[arraySize];

                    for (var i = 0; i < arraySize; i++) array[i] = GetIntegerValue(propertyInfo.Name, i);

                    propertyInfo.SetValue(this, array);
                }
                else if (elementType == typeof(bool))
                {
                    var arraySize = row.GetBiggestArraySize();
                    var array = new bool[arraySize];

                    for (var i = 0; i < arraySize; i++) array[i] = GetBooleanValue(propertyInfo.Name, i);

                    propertyInfo.SetValue(this, array);
                }
                else if (elementType == typeof(string))
                {
                    var arraySize = row.GetBiggestArraySize();
                    var array = new string[arraySize];

                    for (var i = 0; i < arraySize; i++) array[i] = GetValue(propertyInfo.Name, i);

                    propertyInfo.SetValue(this, array);
                }
            }
            else if (propertyInfo.PropertyType == typeof(LogicData) ||
                     propertyInfo.PropertyType.BaseType == typeof(LogicData))
            {
                ((LogicData)propertyInfo.GetValue(this)!).AutoLoadData();
            }
            else
            {
                var elementType = propertyInfo.PropertyType;
                {
                    if (elementType == typeof(bool))
                        propertyInfo.SetValue(this, GetBooleanValue(propertyInfo.Name, 0));
                    else if (elementType == typeof(int))
                        propertyInfo.SetValue(this, GetIntegerValue(propertyInfo.Name, 0));
                    else if (propertyInfo.PropertyType == typeof(string))
                        propertyInfo.SetValue(this, GetValue(propertyInfo.Name, 0));
                }
            }
        }
    }

    public int GetVillageType()
    {
        return VillageType;
    }
}