using BSL.v41.Logic.Database.Account;
using BSL.v41.Titan.DataStream;
using BSL.v41.Titan.DataStream.Helps;
using BSL.v41.Titan.Mathematical;

namespace BSL.v41.Logic.Environment.LaserMessage.Sepo.Avatar;

public class LogicClientAvatar(AccountModel accountModel)
{
    public void Encode(ByteStream byteStream)
    {
        ByteStreamHelper.EncodeLogicLong(byteStream, new LogicLong(0, Convert.ToInt32(accountModel.GetAccountId())));
        ByteStreamHelper.EncodeLogicLong(byteStream, new LogicLong(0, Convert.ToInt32(accountModel.GetAccountId())));
        ByteStreamHelper.EncodeLogicLong(byteStream, new LogicLong(0,
            Convert.ToInt16(
                accountModel.GetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure.HomeId))));

        byteStream.WriteStringReference(
            accountModel.GetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure.AvatarName)
                .ToString()!);
        byteStream.WriteBoolean(
            (bool)accountModel
                .GetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure.NameSetByUser));

        byteStream.WriteInt(0);

        byteStream.WriteVInt(15);

        byteStream.WriteVInt(2 + 1);

        ByteStreamHelper.WriteDataReference(byteStream, 23, 0);
        byteStream.WriteVInt(1);

        ByteStreamHelper.WriteDataReference(byteStream, 5, 8);
        byteStream.WriteVInt(100);

        ByteStreamHelper.WriteDataReference(byteStream, 5, 10);
        byteStream.WriteVInt(0);

        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0);

        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0);
        byteStream.WriteVInt(2);

    }
}