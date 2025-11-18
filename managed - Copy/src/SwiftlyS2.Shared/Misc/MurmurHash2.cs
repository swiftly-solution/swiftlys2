using System.Text;

namespace SwiftlyS2.Shared.Misc;

public static class MurmurHash2
{
    /// <summary>
    /// Compute MurmurHash2 (32-bit) of a byte array with an optional seed.
    /// </summary>
    public static uint Hash(byte[] data, uint seed = 0x31415926)
    {
        const uint m = 0x5bd1e995;
        const int r = 24;

        uint length = (uint)data.Length;
        uint h = seed ^ length;

        int index = 0;
        while (length >= 4)
        {
            uint k = BitConverter.ToUInt32(data, index);

            k *= m;
            k ^= k >> r;
            k *= m;

            h *= m;
            h ^= k;

            index += 4;
            length -= 4;
        }

        switch (length)
        {
            case 3:
                h ^= (uint)(data[index + 2] << 16);
                goto case 2;
            case 2:
                h ^= (uint)(data[index + 1] << 8);
                goto case 1;
            case 1:
                h ^= data[index];
                h *= m;
                break;
        }

        h ^= h >> 13;
        h *= m;
        h ^= h >> 15;

        return h;
    }

    /// <summary>
    /// Convenience method for strings (UTF8).
    /// </summary>
    public static uint HashString(string text, uint seed = 0x31415926)
    {
        return Hash(Encoding.UTF8.GetBytes(text), seed);
    }

    /// <summary>
    /// Convert a string to lowercase and then hash it.
    /// </summary>
    public static uint HashStringLowercase(string text, uint seed = 0x31415926)
    {
        return Hash(Encoding.UTF8.GetBytes(text.ToLower()), seed);
    }
}