using BSL.v41.Titan.Mathematical.Massive;

namespace BSL.v41.Tools.LaserCsv.Portal;

public class CsvNode
{
    private readonly string _fileName = null!;
    private CsvTable _table = null!;

    public CsvNode(string[] lines, string fileName)
    {
        try
        {
            _fileName = fileName;
            Load(lines);
        }
        catch
        {
            // ignored.
        }
    }

    public void Load(string[] lines)
    {
        _table = new CsvTable(this, lines.Length);

        if (lines.Length < 2) return;
        var columnNames = ParseLine(lines[0]);
        var columnTypes = ParseLine(lines[1]);

        for (var i = 0; i < columnNames.Count; i++) _table.AddColumn(columnNames[i]);

        for (var i = 0; i < columnTypes.Count; i++)
        {
            var type = columnTypes[i];
            var columnType = -1;

            if (!string.IsNullOrEmpty(type))
            {
                if (string.Equals(type, "string", StringComparison.InvariantCultureIgnoreCase))
                    columnType = 0;
                else if (string.Equals(type, "int", StringComparison.InvariantCultureIgnoreCase))
                    columnType = 1;
                else if (string.Equals(type, "boolean", StringComparison.InvariantCultureIgnoreCase))
                    columnType = 2;
            }

            _table.AddColumnType(columnType);
        }

        _table.ValidateColumnTypes();

        if (lines.Length <= 2) return;
        {
            for (var i = 2; i < lines.Length; i++)
            {
                var values = ParseLine(lines[i]);

                if (values.Count <= 0) continue;
                if (!string.IsNullOrEmpty(values[0])) _table.CreateRow();

                for (var j = 0; j < values.Count; j++) _table.AddAndConvertValue(values[j], j);
            }
        }
    }

    public static LogicArrayList<string> ParseLine(string line)
    {
        var inQuote = false;
        var readField = string.Empty;

        var fields = new LogicArrayList<string>();

        for (var i = 0; i < line.Length; i++)
        {
            var currentChar = line[i];

            switch (currentChar)
            {
                case '"' when inQuote:
                {
                    if (i + 1 < line.Length && line[i + 1] == '"')
                        readField += currentChar;
                    else
                        inQuote = false;

                    break;
                }
                case '"':
                    inQuote = true;
                    break;
                case ',' when !inQuote:
                    fields.Add(readField);
                    readField = string.Empty;
                    break;
                default:
                    readField += currentChar;
                    break;
            }
        }

        fields.Add(readField);

        return fields;
    }

    public string GetFileName()
    {
        return _fileName;
    }

    public CsvTable GetTable()
    {
        return _table;
    }
}