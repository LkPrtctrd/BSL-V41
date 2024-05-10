using BSL.v41.Logic.Database.Account;
using BSL.v41.Logic.Environment.LaserListener;
using BSL.v41.Logic.Environment.LaserMessage.Laser.Node_9;
using BSL.v41.Logic.Environment.LaserMessage.Sepo.Home.Laser.Laser;
using BSL.v41.Logic.Environment.LaserMessage.Sepo.Mode;
using BSL.v41.Titan.DataStream;

namespace BSL.v41.Logic.Environment.LaserCommand.Laser;

public class LogicSetPlayerAgeCommand : LogicCommand
{
    private int _playerAge;

    public override void Decode(ByteStream byteStream)
    {
        base.Decode(byteStream);

        _playerAge = byteStream.ReadInt();
    }

    public override bool CanExecute(LogicHomeMode logicHomeMode)
    {
        if ((bool)logicHomeMode.AccountModel
                .GetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure.UserIsAuthed) &&
            Convert.ToInt32(
                logicHomeMode.AccountModel.GetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure
                    .PlayerAge)) < 3)
            logicHomeMode.GetGameListener()!.Send(new OutOfSyncMessage());

        return _playerAge >= 3 &&
               Convert.ToInt32(
                   logicHomeMode.AccountModel.GetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure
                       .PlayerAge)) < 3;
    }

    public override int Execute(LogicHomeMode logicHomeMode)
    {
        var v1 = (Dictionary<int, IntValueEntry>)logicHomeMode.AccountModel
            .GetFieldValueByAccountStructureParameterFromAccountModel(
                AccountStructure.IntValueEntryX);

        if (v1.Count < 1) return -1;
        v1.Remove(15);

        logicHomeMode.AccountModel.SetFieldValueByAccountStructureParameterFromAccountModel(
            AccountStructure.IntValueEntryX, v1);
        logicHomeMode.AccountModel.SetFieldValueByAccountStructureParameterFromAccountModel(
            AccountStructure.PlayerAge, 999);

        return _playerAge <= 100 ? 0 : -2;
    }

    public override int GetCommandType()
    {
        return 530;
    }
}