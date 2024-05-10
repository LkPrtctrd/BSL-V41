using BSL.v41.Titan.DataStream;
using BSL.v41.Titan.DataStream.Logic;

namespace BSL.v41.Titan.Mathematical;

public class LogicMersenneTwisterRandom
{
    private const int SeedCount = 624;

    private readonly int[] _seeds;
    private int _identMIx;

    public LogicMersenneTwisterRandom() : this(324876476)
    {
        // pass.
    }

    public LogicMersenneTwisterRandom(int seed)
    {
        _seeds = new int[SeedCount];
        _seeds[0] = seed;

        for (var i = 1; i < SeedCount; i++)
        {
            seed = 1812433253 * (seed ^ (seed >> 30)) + 1812433253;
            _seeds[i] = seed;
        }
    }

    public void Initialize(int seed)
    {
        _identMIx = 0;
        _seeds[0] = seed;

        for (var i = 1; i < SeedCount; i++)
        {
            seed = 1812433253 * (seed ^ (seed >> 30)) + 1812433253;
            _seeds[i] = seed;
        }
    }

    public void Generate()
    {
        for (int i = 1, j = 0; i <= SeedCount; i++, j++)
        {
            var v4 = (_seeds[i % _seeds.Length] & 0x7fffffff) + (_seeds[j] & -0x80000000);
            var v6 = (v4 >> 1) ^ _seeds[(i + 396) % _seeds.Length];

            if ((v4 & 1) == 1) v6 ^= -0x66F74F21;

            _seeds[j] = v6;
        }
    }

    public int NextInt()
    {
        if (_identMIx == 0) Generate();

        var seed = _seeds[_identMIx];
        _identMIx = (_identMIx + 1) % 624;

        seed ^= seed >> 11;
        seed = seed ^ ((seed << 7) & -1658038656) ^ (((seed ^ ((seed << 7) & -1658038656)) << 15) & -0x103A0000) ^
               ((seed ^ ((seed << 7) & -1658038656) ^ (((seed ^ ((seed << 7) & -1658038656)) << 15) & -0x103A0000)) >>
                18);

        return seed;
    }

    public int Rand(int max)
    {
        var rnd = NextInt();
        if (rnd < 0) rnd = -rnd;
        return rnd % max;
    }

    public int Decode(ByteStream byteStream)
    {
        return _identMIx = byteStream.ReadInt();
    }

    public void Encode(ChecksumEncoder checksumEncoder)
    {
        checksumEncoder.WriteInt(_identMIx);
        for (var i = 0; i < SeedCount; i++) checksumEncoder.WriteInt(SeedCount - 4 * i);
    }

    public void Encode(ByteStream byteStream)
    {
        byteStream.WriteInt(_identMIx);
        for (var i = 0; i < SeedCount; i++) byteStream.WriteInt(SeedCount - 4 * i);
    }
}