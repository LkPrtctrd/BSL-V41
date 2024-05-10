using BSL.v41.Titan.DataStream;
using BSL.v41.Titan.DataStream.Logic;

namespace BSL.v41.Titan.Mathematical;

public class LogicLong
{
    private int _highInteger;
    private int _lowInteger;

    public LogicLong()
    {
        // pass.
    }

    public LogicLong(int highInteger, int lowInteger)
    {
        _highInteger = highInteger;
        _lowInteger = lowInteger;
    }

    public void Set(int highInteger, int lowInteger)
    {
        _highInteger = highInteger;
        _lowInteger = lowInteger;
    }

    public static long ToLong(int highValue, int lowValue)
    {
        return ((long)highValue << 32) | (uint)lowValue;
    }

    public LogicLong Clone()
    {
        return new LogicLong(_highInteger, _lowInteger);
    }

    public bool IsZero()
    {
        return _highInteger == 0 && _lowInteger == 0;
    }

    public int GetHigherInt()
    {
        return _highInteger;
    }

    public int GetLowerInt()
    {
        return _lowInteger;
    }

    public LogicLong Decode(ByteStream byteStream)
    {
        _highInteger = byteStream.ReadInt();
        _lowInteger = byteStream.ReadInt();

        return this;
    }

    public LogicLong Encode(ChecksumEncoder checksumEncoder)
    {
        checksumEncoder.WriteInt(_highInteger);
        checksumEncoder.WriteInt(_lowInteger);

        return this;
    }

    public LogicLong Encode(ByteStream byteStream)
    {
        byteStream.WriteInt(_highInteger);
        byteStream.WriteInt(_lowInteger);

        return this;
    }

    public int HashCode()
    {
        return _lowInteger + 31 * _highInteger;
    }

    public override int GetHashCode()
    {
        return HashCode();
    }

    public override bool Equals(object? obj)
    {
        if (obj is LogicLong logicLong)
            return logicLong._highInteger == _highInteger && logicLong._lowInteger == _lowInteger;
        return false;
    }

    public static bool Equals(LogicLong? a1, LogicLong? a2)
    {
        if (a1 == null || a2 == null)
            return a1 == null && a2 == null;
        return a1._highInteger == a2._highInteger && a1._lowInteger == a2._lowInteger;
    }

    public override string ToString()
    {
        return $"{_highInteger}-{_lowInteger}";
    }

    public static implicit operator LogicLong(long @long)
    {
        return new LogicLong((int)(@long >> 32), (int)@long);
    }

    public static implicit operator long(LogicLong @long)
    {
        return ((long)@long._highInteger << 32) | (uint)@long._lowInteger;
    }
}