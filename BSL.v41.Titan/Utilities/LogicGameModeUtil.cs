using BSL.v41.Titan.Utilities.Structs;

namespace BSL.v41.Titan.Utilities;

public static class LogicGameModeUtil
{
    public static bool HasMoreThanTwoTeams(int variation)
    {
        variation -= 6;
        bool result;

        if (variation <= 9)
            result = ((0x309u >> variation) & 1) != 0;
        else
            result = false;

        return result;
    }

    public static bool IsBattleRoyale(int variation)
    {
        return variation is 6 or 9;
    }

    public static int GetRespawnSeconds(int variation)
    {
        switch (variation)
        {
            case 0:
            case 2:
                return 3;
            case 3:
                return 3;
            case 15:
                return 5;
            default:
                return 3;
        }
    }

    public static bool HasSpawnProtectionInTheStart(int variation)
    {
        variation -= 6;
        bool result;

        if (variation <= 9)
            result = ((0x309u >> variation) & 1) != 0;
        else
            result = false;

        return result;
    }

    public static bool HasTimerAndCanEndBeforeTimerRunsOut(int variation)
    {
        int result;

        if (variation <= 0x1D)
            result = (int)((0x3EEB09A5u >> variation) & 1);
        else
            result = 0;

        return result > 0;
    }

    public static bool RoundResetsWhenObjectiveIsMissing(int variation)
    {
        int result; // r0

        var v1 = variation - 5; // r0

        if (v1 <= 0x12)
            result = (((int)new Stru707Fc().rOffset + 1) >> v1) & 1;
        else result = 0;

        return result > 0;
    }

    public static bool IsTileOnPoisonArea(int tick, int xTile, int yTile)
    {
        var poisons = (tick - 500) / 100;

        if (tick <= 500) return false;
        return xTile <= poisons || xTile >= 59 - poisons || yTile <= poisons || yTile >= 59 - poisons;
    }

    public static bool IsCoop(int variation)
    {
        bool result;

        if (variation != 8)
            result = ((variation - 10) & 0xFFFFFFF7) == 0;
        else return true;

        return result;
    }

    public static bool PlayersCollectBountyStars(int variation)
    {
        return variation is 3 or 15;
    }

    public static bool HasTwoTeams(int variation)
    {
        return variation != 6 && variation != 8 && variation != 15;
    }

    public static bool HasTwoBases(int variation)
    {
        return variation is 2 or 11;
    }

    public static bool ModeHasCarryables(int variation)
    {
        return variation is 16;
    }

    public static bool PlayersDropPowerCubesOnDeath(int variation)
    {
        variation -= 6;
        var result = false;

        if (variation <= 8)
            result = ((0x109u >> variation) & 1) != 0;

        return result;
    }

    public static bool PlayersCollectBolts(int variation)
    {
        return variation == 11;
    }

    public static bool PlayersCollectPowerCubes(int variation)
    {
        variation -= 6;

        if (variation <= 8)
            return ((0x119 >> variation) & 1) != 0;

        return false;
    }

    public static bool IsBigGameBoss(int variation, bool isBoss)
    {
        return variation == 7 && isBoss;
    }
}