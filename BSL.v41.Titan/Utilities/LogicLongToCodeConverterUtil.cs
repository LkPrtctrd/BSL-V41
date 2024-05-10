using BSL.v41.Titan.Mathematical;

namespace BSL.v41.Titan.Utilities;

public class LogicLongToCodeConverterUtil(string hashTag, string conversionChars)
{
    public string ToCode(LogicLong logicLong)
    {
        var highValue = logicLong.GetHigherInt();

        if (highValue < 256) return hashTag + Convert(((long)logicLong.GetLowerInt() << 8) | (uint)highValue);

        return null!;
    }

    public LogicLong ToId(string code)
    {
        if (code.Length >= 14) return new LogicLong(-1, -1);
        var idCode = code[1..];
        var id = ConvertCode(idCode);

        return id != -1 ? new LogicLong((int)(id % 256), (int)((id >> 8) & 0x7FFFFFFF)) : new LogicLong(-1, -1);
    }

    private long ConvertCode(string code)
    {
        var id = 0;
        var conversionCharsCount = conversionChars.Length;
        var codeCharsCount = code.Length;

        for (var i = 0; i < codeCharsCount; i++)
        {
            var charIndex = conversionChars.IndexOf(code[i]);

            if (charIndex == -1)
            {
                id = -1;
                break;
            }

            id = id * conversionCharsCount + charIndex;
        }

        return id;
    }

    private string Convert(long value)
    {
        var code = new char[12];

        if (value <= -1) return null!;
        var conversionCharsCount = conversionChars.Length;

        for (var i = 11; i >= 0; i--)
        {
            code[i] = conversionChars[(int)(value % conversionCharsCount)];
            value /= conversionCharsCount;

            if (value == 0) return new string(code, i, 12 - i);
        }

        return new string(code);
    }
}