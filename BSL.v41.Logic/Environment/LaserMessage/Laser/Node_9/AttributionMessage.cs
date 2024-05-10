using BSL.v41.Supercell.Titan.CommonUtils;

namespace BSL.v41.Logic.Environment.LaserMessage.Laser.Node_9;

public class AttributionMessage : PiranhaMessage
{
    public readonly List<string> StringReferenceOpenList = [];

    public AttributionMessage()
    {
        Helper.Skip();
    }

    public override void Decode()
    {
        StringReferenceOpenList.Add(ByteStream.ReadStringReference(1024));
        StringReferenceOpenList.Add(ByteStream.ReadStringReference(1024));
        StringReferenceOpenList.Add(ByteStream.ReadStringReference(1024));
        StringReferenceOpenList.Add(ByteStream.ReadStringReference(1024));
        StringReferenceOpenList.Add(ByteStream.ReadStringReference(1024));
        StringReferenceOpenList.Add(ByteStream.ReadStringReference(1024));
        StringReferenceOpenList.Add(ByteStream.ReadStringReference(1024));
        StringReferenceOpenList.Add(ByteStream.ReadStringReference(1024));
        StringReferenceOpenList.Add(ByteStream.ReadStringReference(1024));
        StringReferenceOpenList.Add(ByteStream.ReadStringReference(1024));
    }

    public override int GetMessageType()
    {
        return 30000;
    }

    public override int GetServiceNodeType()
    {
        return 9;
    }
}