using System.Numerics;
using BSL.v41.Titan.DataStream;
using BSL.v41.Titan.DataStream.Logic;

namespace BSL.v41.Titan.Mathematical;

public class LogicVector2(int x, int y)
{
    private int _x = x;
    private int _y = y;

    public LogicVector2() : this(0, 0)
    {
        // pass.
    }

    public LogicVector2(Vector2 vector2) : this((int)vector2.X, (int)vector2.Y)
    {
        // pass.
    }

    public void Destruct()
    {
        _x = 0;
        _y = 0;
    }

    public void Add(LogicVector2 vector2)
    {
        _x += vector2._x;
        _y += vector2._y;
    }

    public void Add(int x, int y)
    {
        _x += x;
        _y += y;
    }

    public LogicVector2 Clone()
    {
        return new LogicVector2(_x, _y);
    }

    public int Dot(LogicVector2 vector2)
    {
        return _x * vector2._x + _y * vector2._y;
    }

    public int GetAngle()
    {
        return LogicMath.GetAngle(_x, _y);
    }

    public int GetAngleBetween(int x, int y)
    {
        return LogicMath.GetAngleBetween(LogicMath.GetAngle(_x, _y), LogicMath.GetAngle(x, y));
    }

    public int GetDistance(LogicVector2 vector2)
    {
        var x = _x - vector2._x;
        var distance = 0x7FFFFFFF;

        if ((uint)(x + 46340) > 92680) return LogicMath.Sqrt(distance);
        var y = _y - vector2._y;

        if ((uint)(y + 46340) > 92680) return LogicMath.Sqrt(distance);
        var distanceX = x * x;
        var distanceY = y * y;

        if ((uint)distanceY < (distanceX ^ 0x7FFFFFFFu)) distance = distanceX + distanceY;

        return LogicMath.Sqrt(distance);
    }

    public int GetDistanceSquared(LogicVector2 vector2)
    {
        var x = _x - vector2._x;
        var distance = 0x7FFFFFFF;

        if ((uint)(x + 46340) > 92680) return distance;
        var y = _y - vector2._y;

        if ((uint)(y + 46340) > 92680) return distance;
        var distanceX = x * x;
        var distanceY = y * y;

        if ((uint)distanceY < (distanceX ^ 0x7FFFFFFFu)) distance = distanceX + distanceY;

        return distance;
    }

    public int GetDistanceSquaredTo(int x, int y)
    {
        var distance = 0x7FFFFFFF;

        x -= _x;

        if ((uint)(x + 46340) > 92680) return distance;
        y -= _y;

        if ((uint)(y + 46340) > 92680) return distance;
        var distanceX = x * x;
        var distanceY = y * y;

        if ((uint)distanceY < (distanceX ^ 0x7FFFFFFFu)) distance = distanceX + distanceY;

        return distance;
    }

    public int GetLength()
    {
        var length = 0x7FFFFFFF;

        if ((uint)(46340 - _x) > 92680) return LogicMath.Sqrt(length);
        if ((uint)(46340 - _y) > 92680) return LogicMath.Sqrt(length);

        var lengthX = _x * _x;
        var lengthY = _y * _y;

        if ((uint)lengthY < (lengthX ^ 0x7FFFFFFFu)) length = lengthX + lengthY;

        return LogicMath.Sqrt(length);
    }

    public int GetLengthSquared()
    {
        var length = 0x7FFFFFFF;

        if ((uint)(46340 - _x) > 92680) return length;
        if ((uint)(46340 - _y) > 92680) return length;

        var lengthX = _x * _x;
        var lengthY = _y * _y;

        if ((uint)lengthY < (lengthX ^ 0x7FFFFFFFu)) length = lengthX + lengthY;

        return length;
    }

    public bool IsEqual(LogicVector2 vector2)
    {
        return _x == vector2._x && _y == vector2._y;
    }

    public bool IsInArea(int minX, int minY, int maxX, int maxY)
    {
        if (_x >= minX && _y >= minY)
            return _x < minX + maxX && _y < maxY + minY;
        return false;
    }

    public void Multiply(LogicVector2 vector2)
    {
        _x *= vector2._x;
        _y *= vector2._y;
    }

    public int Normalize(int value)
    {
        var length = GetLength();
        if (LogicMath.Abs(length) == 0) return length;

        _x = _x * value / length;
        _y = _y * value / length;

        return length;
    }

    public void Rotate(int degrees)
    {
        _x = LogicMath.GetRotatedX(_x, _y, degrees);
        _y = LogicMath.GetRotatedY(_x, _y, degrees);
    }

    public int GetX()
    {
        return _x;
    }

    public int GetY()
    {
        return _y;
    }

    public void Set(int x, int y)
    {
        _x = x;
        _y = y;
    }

    public void Set(LogicVector2 vector2)
    {
        _x = vector2._x;
        _y = vector2._y;
    }

    public void Set(Vector2 vector2)
    {
        _x = (int)vector2.X;
        _y = (int)vector2.Y;
    }

    public void Substract(LogicVector2 vector2)
    {
        _x -= vector2._x;
        _y -= vector2._y;
    }

    public void Decode(ByteStream byteStream)
    {
        _x = byteStream.ReadInt();
        _y = byteStream.ReadInt();
    }

    public void Encode(ChecksumEncoder checksumEncoder)
    {
        checksumEncoder.WriteInt(_x);
        checksumEncoder.WriteInt(_y);
    }

    public void Encode(ByteStream byteStream)
    {
        byteStream.WriteInt(_x);
        byteStream.WriteInt(_y);
    }

    public override string ToString()
    {
        return "LogicVector2(" + _x + "," + _y + ")";
    }
}