using BSL.v41.Titan.Mathematical;

namespace BSL.v41.Titan.Utilities;

public static class LogicGamePlayUtil
{
    public static int GetDistanceBetween(int a1, int a2, int a3, int a4)
    {
        return LogicMath.Sqrt((a3 - a1) * (a3 - a1) + (a4 - a2) * (a4 - a2));
    }

    public static int GetDistanceSquaredBetween(int a1, int a2, int a3, int a4)
    {
        return (a3 - a1) * (a3 - a1) + (a4 - a2) * (a4 - a2);
    }

    public static float TweenCubicEaseIn(float a1, float a2, float a3)
    {
        return a2 + a1 * a1 * a1 * (a3 - a2);
    }

    public static float WeaponSpreadToAngleRad(int a1)
    {
        return (float)((float)a1 * 0.008);
    }

    public static bool IsTargetedJumpCharge(int chargeType)
    {
        var v1 = (uint)(chargeType - 2);

        if (v1 <= 9)
            return ((0x213u >> (int)v1) & 1) != 0;

        return false;
    }

    public static bool IsJumpCharge(int chargeType)
    {
        var v1 = (uint)(chargeType - 2);

        if (v1 <= 9)
            return ((0x293u >> (int)v1) & 1) != 0;

        return false;
    }

    public static int GetPlayerCountWithGameModeVariation(int gameMode)
    {
        switch (gameMode)
        {
            case 0:
            case 5:
            case 2:
            case 3:
                return 6;
            case 6:
                return 10;
            case 7:
                return 6;
            case 8:
                return 6;
            case 9:
                return 6;
            case 10:
                return 1;
            case 11:
                return 6;
            case 12:
            case 13:
                return 10;
            case 14:
            case 15:
                return 6;
            case 16:
                return 6;
            case 17:
                return 6;
            case 18:
                return 1;
            case 19:
            case 20:
            case 21:
            case 22:
            case 23:
                return 6;
            case 24:
                return 2;
            case 25:
            case 26:
            case 27:
                return 6;
        }

        return -1;
    }

    public static int Lerp(int a1, int a2, int a3)
    {
        var v3 = (long)(274877907 * (a3 * a2 + (1000 - a3) * a1)) >> 32;
        return (int)((v3 >> 6) + ((uint)v3 >> 31));
    }

    public static int LerpAngle(int a1, int a2, int a3)
    {
        var v5 = LogicMath.NormalizeAngle360(a1);
        var v6 = LogicMath.NormalizeAngle360(a2);

        if (v5 - v6 < 181)
        {
            if (v5 - v6 < -180)
                v5 += 360;
        }
        else
        {
            v5 -= 360;
        }

        var v7 = (int)((274877907L * (v6 * a3 + v5 * (1000 - a3))) >> 32);
        return LogicMath.NormalizeAngle360((int)((v7 >> 6) + ((uint)v7 >> 31)));
    }
}