using BSL.v41.Titan.Mathematical.Data;

namespace BSL.v41.Titan.DataStream;

public class BitStream(byte[] buffer, int length)
{
    private int _bitOffset;

    private int _offset;

    public BitStream(int length) : this(new byte[length], length)
    {
        // pass.
    }

    public byte ReadBit()
    {
        if (_offset > buffer.Length)
            return 0;

        var value = (byte)((buffer[_offset] >> _bitOffset) & 1);
        _bitOffset++;

        if (_bitOffset != 8) return value;
        _bitOffset = 0;
        _offset += 1;

        return value;
    }

    public byte[] ReadBytes(int lengthBi)
    {
        var data = new List<byte>();
        {
            for (var i = 0; i < lengthBi;)
            {
                byte value = 0;
                for (var p = 0; p < 8 && i < lengthBi; p++, i++) value |= (byte)(ReadBit() << p);
                data.Add(value);
            }
        }

        return data.ToArray();
    }

    public int ReadInt(int bits)
    {
        return (2 * ReadPositiveInt(1) - 1) * ReadPositiveInt(bits);
    }

    public int ReadIntMax1()
    {
        return ReadInt(1);
    }

    public int ReadIntMax3()
    {
        return ReadInt(2);
    }

    public int ReadIntMax7()
    {
        return ReadInt(3);
    }

    public int ReadIntMax15()
    {
        return ReadInt(4);
    }

    public int ReadIntMax31()
    {
        return ReadInt(5);
    }

    public int ReadIntMax63()
    {
        return ReadInt(6);
    }

    public int ReadIntMax127()
    {
        return ReadInt(7);
    }

    public int ReadIntMax255()
    {
        return ReadInt(8);
    }

    public int ReadIntMax511()
    {
        return ReadInt(9);
    }

    public int ReadIntMax1023()
    {
        return ReadInt(10);
    }

    public int ReadIntMax2047()
    {
        return ReadInt(11);
    }

    public int ReadIntMax4095()
    {
        return ReadInt(12);
    }

    public int ReadIntMax16383()
    {
        return ReadInt(14);
    }

    public int ReadIntMax32767()
    {
        return ReadInt(15);
    }

    public int ReadIntMax65535()
    {
        return ReadInt(16);
    }

    public int ReadPositiveInt(int bitsCount)
    {
        var bytes = ReadBytes(bitsCount);
        return bytes.Length switch
        {
            1 => bytes[0],
            2 => BitConverter.ToUInt16(bytes),
            3 => (bytes[0] << 16) | (bytes[1] << 8) | bytes[2],
            4 => BitConverter.ToInt32(bytes),
            _ => -1
        };
    }

    public int ReadPositiveIntMax1()
    {
        return ReadPositiveInt(1);
    }

    public int ReadPositiveIntMax3()
    {
        return ReadPositiveInt(2);
    }

    public int ReadPositiveIntMax7()
    {
        return ReadPositiveInt(3);
    }

    public int ReadPositiveIntMax15()
    {
        return ReadPositiveInt(4);
    }

    public int ReadPositiveIntMax31()
    {
        return ReadPositiveInt(5);
    }

    public int ReadPositiveIntMax63()
    {
        return ReadPositiveInt(6);
    }

    public int ReadPositiveIntMax127()
    {
        return ReadPositiveInt(7);
    }

    public int ReadPositiveIntMax255()
    {
        return ReadPositiveInt(8);
    }

    public int ReadPositiveIntMax511()
    {
        return ReadPositiveInt(9);
    }

    public int ReadPositiveIntMax1023()
    {
        return ReadPositiveInt(10);
    }

    public int ReadPositiveIntMax2047()
    {
        return ReadPositiveInt(11);
    }

    public int ReadPositiveIntMax4095()
    {
        return ReadPositiveInt(12);
    }

    public int ReadPositiveIntMax8191()
    {
        return ReadPositiveInt(13);
    }

    public int ReadPositiveIntMax16383()
    {
        return ReadPositiveInt(14);
    }

    public int ReadPositiveIntMax32767()
    {
        return ReadPositiveInt(15);
    }

    public bool ReadBoolean()
    {
        return ReadPositiveInt(1) == 1;
    }

    public int ReadPositiveVIntMax255()
    {
        var v2 = ReadPositiveInt(3) + 1;
        return ReadPositiveInt(v2);
    }

    public void cCEdxBAg4w1Rh27DLv02OknR1k6t92VCrH18rijj7ji8TPvZWg(byte data)
    {
        if (_bitOffset == 0)
        {
            _offset += 1;
            S71to3wZjEB2CasFkrDBclddvzPnFAzIr7iXz96X8X66lmskjG(0xFF);
        }

        int value = buffer[_offset - 1];
        value &= ~(1 << _bitOffset);
        value |= data << _bitOffset;
        buffer[_offset - 1] = (byte)value;
        _bitOffset = (_bitOffset + 1) % 8;
    }

    public void S71to3wZjEB2CasFkrDBclddvzPnFAzIr7iXz96X8X66lmskjG(byte value)
    {
        Sub_ReKec789(1);
        _bitOffset = 0;
        buffer[_offset - 1] = value;
    }

    public void WajpDe7aAqLJgeHUDpTF8Qhb7J6b52sLhabQYm49amc1Dj7ts6(byte[] bits, long count)
    {
        var position = 0;
        for (long i = 0; i < count;)
        {
            for (var p = 0; p < 8 && i < count; i++, p++)
            {
                var value = (byte)((bits[position] >> p) & 1);
                cCEdxBAg4w1Rh27DLv02OknR1k6t92VCrH18rijj7ji8TPvZWg(value);
            }

            position++;
        }
    }

    public void WriteInt(int value, int bits)
    {
        var val = value;

        if (val <= -1)
        {
            WritePositiveInt(0, 1);
            val = -value;
        }
        else
        {
            WritePositiveInt(1, 1);
            val = value;
        }

        WritePositiveInt(val, bits);
    }

    public void WriteIntMax1(int value)
    {
        WriteInt(value, 1);
    }

    public void WriteIntMax3(int value)
    {
        WriteInt(value, 2);
    }

    public void WriteIntMax7(int value)
    {
        WriteInt(value, 3);
    }

    public void WriteIntMax15(int value)
    {
        WriteInt(value, 4);
    }

    public void WriteIntMax31(int value)
    {
        WriteInt(value, 5);
    }

    public void WriteIntMax63(int value)
    {
        WriteInt(value, 6);
    }

    public void WriteIntMax127(int value)
    {
        WriteInt(value, 7);
    }

    public void WriteIntMax255(int value)
    {
        WriteInt(value, 8);
    }

    public void WriteIntMax511(int value)
    {
        WriteInt(value, 9);
    }

    public void WriteIntMax1023(int value)
    {
        WriteInt(value, 10);
    }

    public void WriteIntMax2047(int value)
    {
        WriteInt(value, 11);
    }

    public void WriteIntMax4095(int value)
    {
        WriteInt(value, 12);
    }

    public void WriteIntMax16383(int value)
    {
        WriteInt(value, 14);
    }

    public void WriteIntMax32767(int value)
    {
        WriteInt(value, 15);
    }

    public void WriteIntMax65535(int value)
    {
        WriteInt(value, 16);
    }

    public void WritePositiveInt(int value, int bits)
    {
        WajpDe7aAqLJgeHUDpTF8Qhb7J6b52sLhabQYm49amc1Dj7ts6(BitConverter.GetBytes(value), bits);
    }

    public int WritePositiveIntMax1(int value)
    {
        WritePositiveInt(value, 1);
        return value;
    }

    public int WritePositiveIntMax3(int value)
    {
        WritePositiveInt(value, 2);
        return value;
    }

    public int WritePositiveIntMax7(int value)
    {
        WritePositiveInt(value, 3);
        return value;
    }

    public int WritePositiveIntMax15(int value)
    {
        WritePositiveInt(value, 4);
        return value;
    }

    public int WritePositiveIntMax31(int value)
    {
        WritePositiveInt(value, 5);
        return value;
    }

    public int WritePositiveIntMax63(int value)
    {
        WritePositiveInt(value, 6);
        return value;
    }

    public int WritePositiveIntMax127(int value)
    {
        WritePositiveInt(value, 7);
        return value;
    }

    public int WritePositiveIntMax255(int value)
    {
        WritePositiveInt(value, 8);
        return value;
    }

    public int WritePositiveIntMax511(int value)
    {
        WritePositiveInt(value, 9);
        return value;
    }

    public int WritePositiveIntMax1023(int value)
    {
        WritePositiveInt(value, 10);
        return value;
    }

    public int WritePositiveIntMax2047(int value)
    {
        WritePositiveInt(value, 11);
        return value;
    }

    public int WritePositiveIntMax4095(int value)
    {
        WritePositiveInt(value, 12);
        return value;
    }

    public int WritePositiveIntMax8191(int value)
    {
        WritePositiveInt(value, 13);
        return value;
    }

    public int WritePositiveIntMax16383(int value)
    {
        WritePositiveInt(value, 14);
        return value;
    }

    public int WritePositiveIntMax32767(int value)
    {
        WritePositiveInt(value, 15);
        return value;
    }

    public int WritePositiveIntMax65535(int value)
    {
        WritePositiveInt(value, 16);
        return value;
    }

    public int WritePositiveIntMax131071(int value)
    {
        WritePositiveInt(value, 17);
        return value;
    }

    public int WritePositiveIntMax262143(int value)
    {
        WritePositiveInt(value, 18);
        return value;
    }

    public int WritePositiveIntMax524287(int value)
    {
        WritePositiveInt(value, 19);
        return value;
    }

    public int WritePositiveIntMax1048575(int value)
    {
        WritePositiveInt(value, 20);
        return value;
    }

    public int WritePositiveIntMax2097151(int value)
    {
        WritePositiveInt(value, 21);
        return value;
    }

    public int WritePositiveIntMax4194303(int value)
    {
        WritePositiveInt(value, 22);
        return value;
    }

    public int WritePositiveIntMax8388607(int value)
    {
        WritePositiveInt(value, 23);
        return value;
    }

    public int WritePositiveIntMax16777215(int value)
    {
        WritePositiveInt(value, 24);
        return value;
    }

    public int WritePositiveIntMax33554431(int value)
    {
        WritePositiveInt(value, 25);
        return value;
    }

    public int WritePositiveIntMax67108863(int value)
    {
        WritePositiveInt(value, 26);
        return value;
    }

    public int WritePositiveIntMax134217727(int value)
    {
        WritePositiveInt(value, 27);
        return value;
    }

    public bool WriteBoolean(bool value)
    {
        WritePositiveInt(value ? 1 : 0, 1);
        return value;
    }

    public bool WriteBoolean(int value)
    {
        WritePositiveInt(value > 0 ? 1 : 0, 1);
        return value > 0;
    }

    public void WritePositiveVInt(int value, int bits)
    {
        var v3 = 1;
        if (value != 0)
        {
            if (value < 1)
            {
                v3 = 0;
            }
            else
            {
                var v8 = value;
                v3 = 0;
                do
                {
                    v3 += 1;
                    v8 >>= 1;
                } while (v8 != 0);
            }
        }

        WritePositiveInt(v3 - 1, bits);
        WritePositiveInt(value, v3);
    }

    public int WritePositiveVIntMax255(int value)
    {
        WritePositiveVInt(value, 3);
        return value;
    }

    public int WritePositiveVIntMax65535(int value)
    {
        WritePositiveVInt(value, 4);
        return value;
    }

    public void WritePositiveVIntOftenZero(int v1, int v2)
    {
        if (v1 == 0)
        {
            WritePositiveInt(1, 1);
            return;
        }

        WritePositiveInt(0, 1);
        WritePositiveVInt(v1, v2);
    }

    public int WritePositiveVIntMax255OftenZero(int value)
    {
        if (value == 0)
        {
            WritePositiveInt(1, 1);
            return value;
        }

        WritePositiveInt(0, 1);
        WritePositiveVInt(value, 3);

        return value;
    }

    public int WritePositiveVIntMax65535OftenZero(int value)
    {
        if (value == 0)
        {
            WritePositiveInt(1, 1);
            return value;
        }

        WritePositiveInt(0, 1);
        WritePositiveVInt(value, 4);
        return value;
    }

    public int Sub_364FF0(int globalId) // old versions technologies.
    {
        WritePositiveIntMax16383(GlobalId.GetInstanceId(globalId));
        return globalId;
    }

    public void Sub_436E70(int a1, int a2 = 0) // old versions technologies.
    {
        if (a2 == 0) a2 = a1;

        if (a2 > 3)
        {
            if (a2 > 7)
            {
                if (a2 > 15)
                {
                    if (a2 > 31)
                    {
                        if (a2 > 63)
                        {
                            if (a2 > 127)
                            {
                                if (a2 > 255)
                                {
                                    if (a2 >= 512)
                                    {
                                        if (a2 >= 1024)
                                        {
                                            if (a2 >= 2048)
                                            {
                                                if (a2 >= 4096)
                                                {
                                                    if (a2 >= 0x2000)
                                                    {
                                                        if (a2 >= 0x4000)
                                                        {
                                                            if (a2 >= 0x8000)
                                                            {
                                                                WritePositiveInt(15, 4);
                                                                WritePositiveInt(a2, 16);
                                                                return;
                                                            }

                                                            WritePositiveInt(14, 4);
                                                            WritePositiveInt(a1, 15);
                                                            return;
                                                        }

                                                        WritePositiveInt(13, 4);
                                                        WritePositiveInt(a1, 14);
                                                        return;
                                                    }

                                                    WritePositiveInt(12, 4);
                                                    WritePositiveInt(a1, 13);
                                                    return;
                                                }

                                                WritePositiveInt(11, 4);
                                                WritePositiveInt(a1, 12);
                                                return;
                                            }

                                            WritePositiveInt(10, 4);
                                            WritePositiveInt(a1, 11);
                                            return;
                                        }

                                        WritePositiveInt(9, 4);
                                        WritePositiveInt(a1, 10);
                                        return;
                                    }

                                    WritePositiveInt(8, 4);
                                    WritePositiveInt(a1, 9);
                                    return;
                                }

                                WritePositiveInt(7, 4);
                                WritePositiveInt(a1, 8);
                                return;
                            }

                            WritePositiveInt(6, 4);
                            WritePositiveInt(a1, 7);
                            return;
                        }

                        WritePositiveInt(5, 4);
                        WritePositiveInt(a1, 6);
                        return;
                    }

                    WritePositiveInt(4, 4);
                    WritePositiveInt(a1, 5);
                    return;
                }

                WritePositiveInt(3, 4);
                WritePositiveInt(a1, 4);
                return;
            }

            WritePositiveInt(2, 4);
            WritePositiveInt(a1, 3);
        }
        else
        {
            WritePositiveInt(1, 4);
            WritePositiveInt(a1, 2);
        }
    }

    public void Sub_ReKec789(int capacity)
    {
        var bufferLength = buffer.Length;

        if (_offset + capacity <= bufferLength) return;
        var tmpBuffer = new byte[buffer.Length + capacity];
        Array.Copy(buffer, 0, tmpBuffer, 0, bufferLength);
        buffer = tmpBuffer;
    }

    public byte[] GetByteArray()
    {
        return buffer;
    }

    public int GetLength()
    {
        return length > _offset ? length : _offset;
    }
}