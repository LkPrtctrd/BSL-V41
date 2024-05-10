using BSL.v41.Supercell.Titan.CommonUtils;
using BSL.v41.Titan.DataStream;

namespace BSL.v41.Logic.Environment.LaserMessage.Laser.Node_4;

public class VisionUpdateMessage : PiranhaMessage
{
    public bool BrawlTvMode;
    public bool SkipIntro;
    public int Tick, Spectators, InputCounter;

    public VisionUpdateMessage()
    {
        Helper.Skip();
    }

    private BitStream? BitStream { get; set; }

    public override void Encode()
    {
        base.Encode();

        ByteStream.WriteVInt(SkipIntro ? Tick <= 120 ? 120 : Tick : Tick);

        ByteStream.WriteVInt(InputCounter);
        ByteStream.WriteVInt(0);

        ByteStream.WriteVInt(Spectators);
        ByteStream.WriteBoolean(BrawlTvMode);

        ByteStream.WriteBoolean(false);

        ByteStream.WriteBytes(BitStream!.GetByteArray(), BitStream!.GetLength());
    }

    public override void Destruct()
    {
        base.Destruct();
    }

    public void SetVisionBitStream(BitStream bitStream)
    {
        BitStream = bitStream;
    }

    public override int GetMessageType()
    {
        return 24109;
    }

    public override int GetServiceNodeType()
    {
        return 4;
    }
}