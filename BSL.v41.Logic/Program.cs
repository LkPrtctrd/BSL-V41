using BSL.v41.Logic.LHelp;
using BSL.v41.Titan.Graphic;

namespace BSL.v41.Logic;

public static class Program
{
    public static void Main(string[] args)
    {
        ConsoleLogger.WriteTextWithPrefix(ConsoleLogger.Prefixes.Cmd, "Emulation of 'Logic' has been launched.");
        {
            StaticOnGetService.OnGetMessagesHelper = new OnGetMessagesHelper();
            StaticOnGetService.OnGetMessagesHelper.OnLoad();
        }
    }
}