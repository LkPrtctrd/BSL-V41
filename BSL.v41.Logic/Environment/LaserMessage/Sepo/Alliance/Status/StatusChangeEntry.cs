using BSL.v41.Logic.Database.Account;
using BSL.v41.Titan.DataStream;

namespace BSL.v41.Logic.Environment.LaserMessage.Sepo.Alliance.Status;

public class StatusChangeEntry
{
    public AccountModel AccountModel { get; set; } = null!;

    public void Encode(ByteStream byteStream)
    {
        byteStream.WriteLong(AccountModel.GetAccountId());
        byteStream.WriteVInt(
            AccountModel.GetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure.PlayerStatus), true);
    }
}