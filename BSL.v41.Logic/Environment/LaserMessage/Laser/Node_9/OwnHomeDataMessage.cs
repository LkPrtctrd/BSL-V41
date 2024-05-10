using BSL.v41.Logic.Database.Account;
using BSL.v41.Logic.Environment.LaserMessage.Sepo.Avatar;
using BSL.v41.Logic.Environment.LaserMessage.Sepo.Home;
using BSL.v41.Supercell.Titan.CommonUtils;
using BSL.v41.Titan.DataStream.Helps;

namespace BSL.v41.Logic.Environment.LaserMessage.Laser.Node_9;

public class OwnHomeDataMessage(AccountModel accountModel) : PiranhaMessage
{
    public OwnHomeDataMessage() : this(new AccountModel())
    {
        Helper.Skip();
    }

    public override void Encode()
    {
        base.Encode();

        ByteStream.WriteVInt(0);
        ByteStream.WriteVInt(0);

        new LogicClientHome(accountModel).Encode(ByteStream);
        new LogicClientAvatar(accountModel).Encode(ByteStream);
        ByteStream.WriteVInt(0); // currentTimeInSecondsSinceEpoch
    }

    public override int GetMessageType()
    {
        return 24101;
    }

    public override int GetServiceNodeType()
    {
        return 9;
    }
}