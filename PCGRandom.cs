using static System.Environment;
using static System.DateTime;

/// <summary>
/// Quickly written by pt300 and modified afterwards by guitarxhero.
/// </summary>
public class PCGRandom
{
    ulong state;
    ulong inc;

    public PCGRandom() :
        this((ulong)TickCount, (ulong)(TickCount * Now.Ticks)) { }

    public PCGRandom(ulong seed, ulong seq)
    {
        state = 0U;
        inc = (seq << 1) | 1;
        pcg32_random();
        state += seed;
        pcg32_random();
    }

    public uint pcg32_random()
    {
        ulong oldstate = state;
        state = oldstate * 6364136223846793005ul + inc;
        uint xorshifted = (uint)((oldstate >> 18) ^ oldstate) >> 27;
        uint rot = (uint)(oldstate >> 59);
        return (xorshifted >> (int)rot) | (xorshifted << ((-(int)rot) & 31));
    }

    public int Next(uint MaxValue)
    {
        uint threshold = (uint)((0 - (ulong)MaxValue) % MaxValue);
        for (;;)
        {
            uint r = pcg32_random();
            if (r >= threshold)
                return (int)(r % MaxValue);
        }
    }
}