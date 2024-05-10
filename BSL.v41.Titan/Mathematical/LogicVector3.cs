using BSL.v41.Titan.DataStream;
using BSL.v41.Titan.DataStream.Logic;

namespace BSL.v41.Titan.Mathematical;

public class LogicVector3(int x, int y, int z)
{
    private int _x = x;
    private int _y = y;
    private int _z = z;

    public LogicVector3() : this(0, 0, 0)
    {
        // pass.
    }

    public void Set(int x, int y, int z)
    {
        _x = x;
        _y = y;
        _z = z;
    }

    public void Set(LogicVector3 vector3)
    {
        _x = vector3._x;
        _y = vector3._y;
        _z = vector3._z;
    }

    public void Set(LogicVector2 vector2, bool zToZero = false)
    {
        _x = vector2.GetX();
        _y = vector2.GetY();
        if (zToZero) _z = 0;
    }

    public void Substract(LogicVector3 vector3)
    {
        _x -= vector3._x;
        _y -= vector3._y;
        _z -= vector3._z;
    }

    public void Divide(LogicVector3 vector3)
    {
        _x /= vector3._x;
        _y /= vector3._y;
        _z /= vector3._z;
    }

    public void Destruct()
    {
        _x = 0;
        _y = 0;
        _z = 0;
    }

    public void Add(LogicVector3 vector3)
    {
        _x += vector3._x;
        _y += vector3._y;
        _z += vector3._z;
    }

    public LogicVector3 Clone()
    {
        return new LogicVector3(_x, _y, _z);
    }

    public int GetX()
    {
        return _x;
    }

    public int GetY()
    {
        return _y;
    }

    public int GetZ()
    {
        return _z;
    }

    public bool IsEqual(LogicVector3 vector3)
    {
        return _x == vector3._x && _y == vector3._y && _z == vector3._z;
    }

    public void Multiply(LogicVector3 vector3)
    {
        _x *= vector3._x;
        _y *= vector3._y;
        _z *= vector3._z;
    }

    public bool IsInArea(int minX, int minY, int maxX, int maxY)
    {
        if (_x >= minX && _y >= minY)
            return _x < minX + maxX && _y < maxY + minY;
        return false;
    }

    public int Dot(LogicVector3 vector2)
    {
        return vector2._y * vector2._x + _x * _y + vector2._y * _y;
    }

    public void Decode(ByteStream byteStream)
    {
        _x = byteStream.ReadInt();
        _y = byteStream.ReadInt();
        _z = byteStream.ReadInt();
    }

    public void Encode(ChecksumEncoder checksumEncoder)
    {
        checksumEncoder.WriteInt(_x);
        checksumEncoder.WriteInt(_y);
        checksumEncoder.WriteInt(_z);
    }

    public void Encode(ByteStream byteStream)
    {
        byteStream.WriteInt(_x);
        byteStream.WriteInt(_y);
        byteStream.WriteInt(_z);
    }
}