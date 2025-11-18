namespace SwiftlyS2.Shared.Misc;

internal static class BitFieldHelper
{
    public static int GetBits( ref byte data, int index, int bitCount )
    {
        if (index < 0 || index + bitCount > 8) throw new ArgumentOutOfRangeException();
        var mask = (1 << bitCount) - 1;
        return (data >> index) & mask;
    }

    public static void SetBits( ref byte data, int index, int bitCount, int value )
    {
        if (index < 0 || index + bitCount > 8) throw new ArgumentOutOfRangeException();
        var mask = ((1 << bitCount) - 1) << index;
        data = (byte)((data & ~mask) | ((value << index) & mask));
    }

    public static int GetBits( ref int data, int index, int bitCount )
    {
        if (index < 0 || index + bitCount > 32) throw new ArgumentOutOfRangeException();
        var mask = (1 << bitCount) - 1;
        return (data >> index) & mask;
    }

    public static void SetBits( ref int data, int index, int bitCount, int value )
    {
        if (index < 0 || index + bitCount > 32) throw new ArgumentOutOfRangeException();
        var mask = ((1 << bitCount) - 1) << index;
        data = (data & ~mask) | ((value << index) & mask);
    }

    public static long GetBits( ref long data, int index, int bitCount )
    {
        if (index < 0 || index + bitCount > 64) throw new ArgumentOutOfRangeException();
        var mask = (1L << bitCount) - 1;
        return (data >> index) & mask;
    }

    public static void SetBits( ref long data, int index, int bitCount, long value )
    {
        if (index < 0 || index + bitCount > 64) throw new ArgumentOutOfRangeException();
        var mask = ((1L << bitCount) - 1) << index;
        data = (data & ~mask) | ((value << index) & mask);
    }

    public static bool GetBit( ref byte data, int index ) => GetBits(ref data, index, 1) != 0;
    public static void SetBit( ref byte data, int index, bool value ) => SetBits(ref data, index, 1, value ? 1 : 0);

    public static bool GetBit( ref int data, int index ) => GetBits(ref data, index, 1) != 0;
    public static void SetBit( ref int data, int index, bool value ) => SetBits(ref data, index, 1, value ? 1 : 0);

    public static bool GetBit( ref long data, int index ) => GetBits(ref data, index, 1) != 0;
    public static void SetBit( ref long data, int index, bool value ) => SetBits(ref data, index, 1, value ? 1 : 0);
}