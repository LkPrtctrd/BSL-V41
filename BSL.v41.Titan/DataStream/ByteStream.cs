using System.Text;
using BSL.v41.Titan.DataStream.Logic;
using BSL.v41.Titan.Mathematical;

namespace BSL.v41.Titan.DataStream;

public class ByteStream : ChecksumEncoder
{
    private int _bitIdx;

    private byte[] _buffer;
    private int _length;
    private int _offset;

    public ByteStream(int capacity)
    {
        _buffer = new byte[capacity];
    }

    public ByteStream(byte[] buffer, int length)
    {
        _length = length;
        _buffer = new byte[_length];
        Array.Copy(buffer, _buffer, _length);
    }

    public int GetLength()
    {
        return _offset < _length ? _length : _offset;
    }

    public int GetOffset()
    {
        return _offset;
    }

    public bool IsAtEnd()
    {
        return _offset >= _length;
    }

    public void Clear(int capacity)
    {
        _buffer = new byte[capacity];
    }

    public byte[] GetByteArray()
    {
        return _buffer;
    }

    public bool ReadBoolean()
    {
        if (_bitIdx == 0) ++_offset;

        var value = (_buffer[_offset - 1] & (1 << _bitIdx)) != 0;
        _bitIdx = (_bitIdx + 1) & 7;
        return value;
    }

    public byte ReadByte()
    {
        _bitIdx = 0;
        return _buffer[_offset++];
    }

    public short ReadShort()
    {
        _bitIdx = 0;

        return (short)((_buffer[_offset++] << 8) |
                       _buffer[_offset++]);
    }

    public int ReadInt()
    {
        _bitIdx = 0;

        return (_buffer[_offset++] << 24) |
               (_buffer[_offset++] << 16) |
               (_buffer[_offset++] << 8) |
               _buffer[_offset++];
    }

    public int ReadVInt()
    {
        _bitIdx = 0;
        var value = 0;
        var byteValue = _buffer[_offset++];

        if ((byteValue & 0x40) != 0)
        {
            value |= byteValue & 0x3F;

            if ((byteValue & 0x80) == 0) return (int)(value | 0xFFFFFFC0);
            value |= ((byteValue = _buffer[_offset++]) & 0x7F) << 6;

            if ((byteValue & 0x80) == 0) return (int)(value | 0xFFFFE000);
            value |= ((byteValue = _buffer[_offset++]) & 0x7F) << 13;

            if ((byteValue & 0x80) == 0) return (int)(value | 0xFFF00000);
            value |= ((byteValue = _buffer[_offset++]) & 0x7F) << 20;

            if ((byteValue & 0x80) == 0) return (int)(value | 0xF8000000);
            value |= (_buffer[_offset++] & 0x7F) << 27;
            return (int)(value | 0x80000000);
        }

        value |= byteValue & 0x3F;

        if ((byteValue & 0x80) == 0) return value;
        value |= ((byteValue = _buffer[_offset++]) & 0x7F) << 6;

        if ((byteValue & 0x80) == 0) return value;
        value |= ((byteValue = _buffer[_offset++]) & 0x7F) << 13;

        if ((byteValue & 0x80) == 0) return value;
        value |= ((byteValue = _buffer[_offset++]) & 0x7F) << 20;

        if ((byteValue & 0x80) != 0) value |= (_buffer[_offset++] & 0x7F) << 27;

        return value;
    }

    public LogicLong ReadLong()
    {
        return new LogicLong().Decode(this);
    }

    public LogicLong ReadLong(LogicLong longValue)
    {
        return longValue.Decode(this);
    }

    public long ReadVLong()
    {
        var high = ReadVInt();
        var low = ReadVInt();

        return ((long)high << 32) | (uint)low;
    }

    public int ReadBytesLength()
    {
        _bitIdx = 0;
        return (_buffer[_offset++] << 24) |
               (_buffer[_offset++] << 16) |
               (_buffer[_offset++] << 8) |
               _buffer[_offset++];
    }

    public byte[] ReadBytes(int length, int maxCapacity)
    {
        _bitIdx = 0;

        if (length <= -1)
        {
            if (length != -1)
            {
                // ignored.
            }

            return null!;
        }

        if (length > maxCapacity) return null!;
        var array = new byte[length];
        Array.Copy(_buffer, _offset, array, 0, length);
        _offset += length;
        return array;
    }

    public string ReadString(int maxCapacity = 900000)
    {
        var length = ReadBytesLength();

        if (length <= -1)
        {
            if (length != -1)
            {
                // ignored.
            }
        }
        else
        {
            if (length > maxCapacity) return null!;
            var value = Encoding.UTF8.GetString(_buffer, _offset, length);
            _offset += length;
            return value;
        }

        return null!;
    }

    public string ReadStringReference(int maxCapacity = 900000)
    {
        var length = ReadBytesLength();

        if (length <= -1) return string.Empty;
        if (length > maxCapacity) return string.Empty;
        var value = Encoding.UTF8.GetString(_buffer, _offset, length);
        _offset += length;
        return value;
    }

    public override bool WriteBoolean(bool value)
    {
        base.WriteBoolean(value);

        if (_bitIdx == 0)
        {
            EnsureCapacity(1);
            ++_offset;
        }

        if (value) _buffer[_offset - 1] |= (byte)(1 << _bitIdx);

        _bitIdx = (_bitIdx + 1) & 7;
        return value;
    }

    public override int WriteByte(byte value)
    {
        base.WriteByte(value);
        EnsureCapacity(1);

        _bitIdx = 0;

        _buffer[_offset++] = value;
        return value;
    }

    public override void WriteShort(short value)
    {
        base.WriteShort(value);
        EnsureCapacity(2);

        _bitIdx = 0;

        _buffer[_offset++] = (byte)(value >> 8);
        _buffer[_offset++] = (byte)value;
    }

    public override void WriteInt(int value)
    {
        base.WriteInt(value);
        EnsureCapacity(4);

        _bitIdx = 0;

        _buffer[_offset++] = (byte)(value >> 24);
        _buffer[_offset++] = (byte)(value >> 16);
        _buffer[_offset++] = (byte)(value >> 8);
        _buffer[_offset++] = (byte)value;
    }

    public void WriteInt(bool boolValue)
    {
        var value = boolValue ? 1 : 0;

        base.WriteInt(value);
        EnsureCapacity(4);

        _bitIdx = 0;

        _buffer[_offset++] = (byte)(value >> 24);
        _buffer[_offset++] = (byte)(value >> 16);
        _buffer[_offset++] = (byte)(value >> 8);
        _buffer[_offset++] = (byte)value;
    }

    public override int WriteVInt(int value)
    {
        base.WriteVInt(value);
        EnsureCapacity(5);

        _bitIdx = 0;

        switch (value)
        {
            case >= 0 and >= 64:
            {
                if (value >= 0x2000)
                {
                    if (value >= 0x100000)
                    {
                        if (value >= 0x8000000)
                        {
                            _buffer[_offset++] = (byte)((value & 0x3F) | 0x80);
                            _buffer[_offset++] = (byte)(((value >> 6) & 0x7F) | 0x80);
                            _buffer[_offset++] = (byte)(((value >> 13) & 0x7F) | 0x80);
                            _buffer[_offset++] = (byte)(((value >> 20) & 0x7F) | 0x80);
                            _buffer[_offset++] = (byte)((value >> 27) & 0xF);
                        }
                        else
                        {
                            _buffer[_offset++] = (byte)((value & 0x3F) | 0x80);
                            _buffer[_offset++] = (byte)(((value >> 6) & 0x7F) | 0x80);
                            _buffer[_offset++] = (byte)(((value >> 13) & 0x7F) | 0x80);
                            _buffer[_offset++] = (byte)((value >> 20) & 0x7F);
                        }
                    }
                    else
                    {
                        _buffer[_offset++] = (byte)((value & 0x3F) | 0x80);
                        _buffer[_offset++] = (byte)(((value >> 6) & 0x7F) | 0x80);
                        _buffer[_offset++] = (byte)((value >> 13) & 0x7F);
                    }
                }
                else
                {
                    _buffer[_offset++] = (byte)((value & 0x3F) | 0x80);
                    _buffer[_offset++] = (byte)((value >> 6) & 0x7F);
                }

                break;
            }
            case >= 0:
                _buffer[_offset++] = (byte)(value & 0x3F);
                break;
            case <= -0x40 and <= -0x2000:
            {
                if (value <= -0x100000)
                {
                    if (value <= -0x8000000)
                    {
                        _buffer[_offset++] = (byte)((value & 0x3F) | 0xC0);
                        _buffer[_offset++] = (byte)(((value >> 6) & 0x7F) | 0x80);
                        _buffer[_offset++] = (byte)(((value >> 13) & 0x7F) | 0x80);
                        _buffer[_offset++] = (byte)(((value >> 20) & 0x7F) | 0x80);
                        _buffer[_offset++] = (byte)((value >> 27) & 0xF);
                    }
                    else
                    {
                        _buffer[_offset++] = (byte)((value & 0x3F) | 0xC0);
                        _buffer[_offset++] = (byte)(((value >> 6) & 0x7F) | 0x80);
                        _buffer[_offset++] = (byte)(((value >> 13) & 0x7F) | 0x80);
                        _buffer[_offset++] = (byte)((value >> 20) & 0x7F);
                    }
                }
                else
                {
                    _buffer[_offset++] = (byte)((value & 0x3F) | 0xC0);
                    _buffer[_offset++] = (byte)(((value >> 6) & 0x7F) | 0x80);
                    _buffer[_offset++] = (byte)((value >> 13) & 0x7F);
                }

                break;
            }
            case <= -0x40:
                _buffer[_offset++] = (byte)((value & 0x3F) | 0xC0);
                _buffer[_offset++] = (byte)((value >> 6) & 0x7F);
                break;
            default:
                _buffer[_offset++] = (byte)((value & 0x3F) | 0x40);
                break;
        }

        return value;
    }

    public bool WriteVInt(bool boolValue)
    {
        WriteVInt(boolValue ? 1 : 0);
        return boolValue;
    }

    public int WriteVInt(object objValue, bool unkLen)
    {
        var value = Convert.ToInt32(objValue);
        WriteVInt(value);
        return value;
    }

    public void WriteIntToByteArray(int value)
    {
        EnsureCapacity(4);
        _bitIdx = 0;

        _buffer[_offset++] = (byte)(value >> 24);
        _buffer[_offset++] = (byte)(value >> 16);
        _buffer[_offset++] = (byte)(value >> 8);
        _buffer[_offset++] = (byte)value;
    }

    public override void WriteBytes(byte[]? value, int length)
    {
        base.WriteBytes(value, length);

        if (value == null!)
        {
            WriteIntToByteArray(-1);
        }
        else
        {
            EnsureCapacity(length + 4);
            WriteIntToByteArray(length);

            Array.Copy(value, 0, _buffer, _offset, length);

            _offset += length;
        }
    }

    public void WriteBytesWithoutLength(byte[] value, int length)
    {
        base.WriteBytes(value, length);

        if (value == null!) return;
        EnsureCapacity(length);
        Array.Copy(value, 0, _buffer, _offset, length);
        _offset += length;
    }

    public override void WriteString(string? value)
    {
        base.WriteString(value);

        if (value == null!)
        {
            WriteIntToByteArray(-1);
        }
        else
        {
            var bytes = Encoding.UTF8.GetBytes(value);

            EnsureCapacity(bytes.Length + 4);
            WriteIntToByteArray(bytes.Length);

            var data = Encoding.UTF8.GetBytes(value);
            Buffer.BlockCopy(data, 0, _buffer, _offset, bytes.Length);

            _offset += bytes.Length;
        }
    }

    public void WriteString(object value, bool unkLen)
    {
        WriteString(value.ToString());
    }

    public override void WriteStringReference(string value)
    {
        base.WriteStringReference(value);

        if (value != null!)
        {
            var bytes = Encoding.UTF8.GetBytes(value);

            EnsureCapacity(bytes.Length + 4);
            WriteIntToByteArray(bytes.Length);

            var data = Encoding.UTF8.GetBytes(value);
            Buffer.BlockCopy(data, 0, _buffer, _offset, bytes.Length);

            _offset += bytes.Length;
        }
        else
        {
            WriteIntToByteArray(0);
        }
    }

    public void SetByteArray(byte[] buffer, int length)
    {
        _offset = 0;
        _bitIdx = 0;
        _buffer = buffer;
        _length = length;
    }

    public void ResetOffset()
    {
        _offset = 0;
        _bitIdx = 0;
    }

    public void SetOffset(int offset)
    {
        _offset = offset;
        _bitIdx = 0;
    }

    public byte[] RemoveByteArray()
    {
        var byteArray = _buffer;
        _buffer = null!;
        return byteArray;
    }

    public void EnsureCapacity(int capacity)
    {
        var bufferLength = _buffer.Length;

        if (_offset + capacity <= bufferLength) return;
        var tmpBuffer = new byte[_buffer.Length + capacity + 100];
        Array.Copy(_buffer, tmpBuffer, bufferLength);
        _buffer = tmpBuffer;
    }

    public void Destruct()
    {
        _buffer = null!;
        _bitIdx = 0;
        _length = 0;
        _offset = 0;
    }
}