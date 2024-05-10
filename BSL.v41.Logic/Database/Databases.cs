using BSL.v41.Logic.Database.Account;
using BSL.v41.Logic.Database.Alliance;
using BSL.v41.Logic.Database.IntraSigned;

namespace BSL.v41.Logic.Database;

public static class Databases
{
    public static AccountDatabase AccountDatabase = null!;
    public static AllianceDatabase AllianceDatabase = null!;
    public static NotificationIntraSignedDatabase NotificationIntraSignedDatabase = null!;
}