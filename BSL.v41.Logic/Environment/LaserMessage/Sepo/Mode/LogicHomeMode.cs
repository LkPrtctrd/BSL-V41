using BSL.v41.Logic.Database.Account;
using BSL.v41.Logic.Environment.LaserCommand;
using BSL.v41.Logic.Environment.LaserListener;
using BSL.v41.Logic.Environment.LaserMessage.Laser.Node_9;
using BSL.v41.Logic.Environment.LaserMessage.Sepo.Avatar;
using BSL.v41.Logic.Environment.LaserMessage.Sepo.Home;

namespace BSL.v41.Logic.Environment.LaserMessage.Sepo.Mode;

public class LogicHomeMode
{
    private LogicClientAvatar _logicClientAvatar = null!;
    private LogicClientHome _logicClientHome = null!;
    private LogicGameListener? _logicGameListener;
    public AccountModel AccountModel { get; set; } = null!;

    public void EndClientTurnReceived(int tick, int checksum, IEnumerable<LogicCommand> commands,
        LogicGameListener? logicGameListener = null)
    {
        logicGameListener ??= _logicGameListener;
        commands = commands.Where(command => command.CanExecute(this));

        foreach (var outOfSyncMessage in from command in commands
                 where command.Execute(this) != 0
                 select new OutOfSyncMessage()) logicGameListener!.Send(outOfSyncMessage);
    }

    public LogicGameListener? GetGameListener()
    {
        return _logicGameListener;
    }

    public void SetGameListener(LogicGameListener? logicGameListener)
    {
        _logicGameListener = logicGameListener;
    }

    public LogicClientHome GetHome()
    {
        return _logicClientHome;
    }

    public void SetHome(LogicClientHome logicClientHome)
    {
        _logicClientHome = logicClientHome;
    }

    public LogicClientAvatar GetPlayerAvatar()
    {
        return _logicClientAvatar;
    }

    public void SetHomeOwnerAvatar(LogicClientAvatar logicClientAvatar)
    {
        _logicClientAvatar = logicClientAvatar;
    }
}