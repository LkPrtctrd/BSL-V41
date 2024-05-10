using BSL.v41.Supercell.Titan.CommonUtils;
using BSL.v41.Titan.DataStream.Helps;

namespace BSL.v41.Logic.Environment.LaserMessage.Laser.Node_9;

public class SinglePlayerMatchRequestMessage : PiranhaMessage
{
    // public int V3 { get; set; }

    public SinglePlayerMatchRequestMessage()
    {
        Helper.Skip();
    }

    public int V1 { get; set; }
    public int V2 { get; set; }

    public override void Decode()
    {
        base.Decode();

        V1 = ByteStreamHelper.ReadDataReference(ByteStream);
        V2 = ByteStreamHelper.ReadDataReference(ByteStream);
        // V3 = ByteStream.ReadVInt();
    }

    public override void Clear()
    {
        base.Clear();

        Helper.Destructor(this);
    }

    public override int GetMessageType()
    {
        return 14118;
    }

    public override int GetServiceNodeType()
    {
        return 9;
    }
}