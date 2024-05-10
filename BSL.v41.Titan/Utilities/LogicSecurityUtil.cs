using BSL.v41.Titan.Mathematical;

namespace BSL.v41.Titan.Utilities;

public static class LogicSecurityUtil
{
    public static int[] RandomizeNonce(int a1, int a2)
    {
        var v7 = new int[5];
        var a5 = 0;

        var logicRandom = new LogicRandom(a1);
        {
            logicRandom.SetIteratedRandomSeed(a2);
        }

        if (a2 < 1) return v7;
        do
        {
            a2--;
            a5++;
            v7[a5] ^= logicRandom.Rand(256);
        } while (a2 > 0);

        return v7;
    }

    public static void RandomizeNonce2(int a1, int a2, int a3)
    {
        var logicMersenneTwisterRandom = new LogicMersenneTwisterRandom(a1);
        var v5 = 100;

        do
        {
            _ = (byte)logicMersenneTwisterRandom.Rand(256);
            --v5;
        } while (v5 > 0);

        if (a3 < 1) return;
        {
            do
            {
                --a3;
                _ = logicMersenneTwisterRandom.Rand(256);
            } while (a3 > 0);
        }
    }
}