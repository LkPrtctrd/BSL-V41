using BSL.v41.Titan.DataStream;

namespace BSL.v41.Logic.Environment.LaserMessage.Sepo.Input;

public class ClientInput
{
    public long OwnSessionId { get; set; }

    public int Index { get; private set; }
    public int Type { get; private set; }
    public int X { get; private set; }
    public int Y { get; private set; }
    public bool AutoAttack { get; private set; }
    public int PinIndex { get; private set; }

    public void Decode(BitStream bitStream)
    {
        Index = bitStream.ReadPositiveIntMax32767();
        Type = bitStream.ReadPositiveIntMax15();

        X = bitStream.ReadIntMax32767();
        Y = bitStream.ReadIntMax32767();

        AutoAttack = bitStream.ReadBoolean();
        bitStream.ReadBoolean();

        switch (Type)
        {
            case 11:
                bitStream.ReadPositiveVIntMax255();
                break;
            case 9:
                PinIndex = bitStream.ReadPositiveIntMax7();
                break;
        }

        if (!AutoAttack) return;

        if (bitStream.ReadBoolean()) bitStream.ReadPositiveIntMax16383();
    }
}