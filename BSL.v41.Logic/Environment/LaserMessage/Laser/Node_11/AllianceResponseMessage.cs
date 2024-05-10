using BSL.v41.Supercell.Titan.CommonUtils;

namespace BSL.v41.Logic.Environment.LaserMessage.Laser.Node_11;

public class AllianceResponseMessage : PiranhaMessage
{
    private int _response;

    public AllianceResponseMessage()
    {
        Helper.Skip();
    }

    public override void Encode()
    {
        base.Encode();

        ByteStream.WriteVInt(_response);
        ByteStream.WriteVInt(0);
    }

    public override void Destruct()
    {
        base.Destruct();
    }

    public void IsResponse(int a2)
    {
        if (GetMessageType() == 24333) _response = a2;
    }

    public override int GetMessageType()
    {
        return 24333;
    }

    public override int GetServiceNodeType()
    {
        return 11;
    }
}