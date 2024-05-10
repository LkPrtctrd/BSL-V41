using BSL.v41.Supercell.Titan.CommonUtils;
using BSL.v41.Titan.DataStream;

namespace BSL.v41.Logic.Environment.LaserMessage;

public abstract class PiranhaMessage
{
    private int _messageVersion = -1;
    private int _proxySessionId;
    public ByteStream ByteStream { get; } = new(128);

    public virtual int GetProxySessionId()
    {
        return _proxySessionId;
    }

    public virtual int SetProxySessionId(int proxySessionId)
    {
        _proxySessionId = proxySessionId;
        return _proxySessionId;
    }

    public bool IsClientToServerMessage()
    {
        return GetMessageType() is >= 10000 and < 20000 or 30000;
    }

    public bool IsServerToClientMessage()
    {
        return GetMessageType() is >= 20000 and < 30000 or 40000;
    }

    public virtual string GetMessageTypeName()
    {
        return Helper.GetPacketNameByType(GetMessageType())!;
    }

    public virtual int GetMessageVersion()
    {
        return _messageVersion < 1 ? GetMessageType() == 20104 ? 1 : 0 : _messageVersion;
    }

    public void SetMessageVersion(int newArg)
    {
        _messageVersion = newArg;
    }

    public virtual int GetEncodingLength()
    {
        return ByteStream.GetLength();
    }

    public virtual byte[] GetMessageBytes()
    {
        return ByteStream.GetByteArray();
    }

    public virtual void Encode()
    {
    }

    public virtual void Decode()
    {
    }

    public virtual void Clear()
    {
        ByteStream.Clear(GetEncodingLength());
    }

    public virtual void Destruct()
    {
        ByteStream.Destruct();
    }

    public abstract int GetMessageType();
    public abstract int GetServiceNodeType();
}