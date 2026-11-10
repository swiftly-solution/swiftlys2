using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.Misc;

/// <summary>
/// The atomic operations on a single cell, shared by <see cref="Atomic{T}"/> and <see cref="AtomicArray{T}"/>.
/// </summary>
internal static class AtomicOps
{
    public static T Load<T>( ref T cell )
    {
        if (!typeof(T).IsValueType)
        {
            var value = Volatile.Read(ref Unsafe.As<T, object?>(ref cell));
            return Unsafe.As<object?, T>(ref value);
        }

        if (Unsafe.SizeOf<T>() == 8)
        {
            return Unsafe.BitCast<long, T>(Volatile.Read(ref Unsafe.As<T, long>(ref cell)));
        }

        if (Unsafe.SizeOf<T>() == 4)
        {
            return Unsafe.BitCast<int, T>(Volatile.Read(ref Unsafe.As<T, int>(ref cell)));
        }

        return Interlocked.CompareExchange(ref cell, default, default);
    }

    public static bool TryCompareExchange<T>( ref T cell, ref T expected, T desired )
    {
        var seen = Interlocked.CompareExchange(ref cell, desired, expected);
        if (!typeof(T).IsValueType ? ReferenceEquals(seen, expected) : BitEquals(seen, expected))
        {
            return true;
        }

        expected = seen;
        return false;
    }

    public static T FetchAnd<T>( ref T cell, T operand ) where T : struct, IBinaryInteger<T>
    {
        if (Unsafe.SizeOf<T>() == 8)
        {
            return Unsafe.BitCast<ulong, T>(Interlocked.And(ref Unsafe.As<T, ulong>(ref cell), Unsafe.BitCast<T, ulong>(operand)));
        }

        if (Unsafe.SizeOf<T>() == 4)
        {
            return Unsafe.BitCast<uint, T>(Interlocked.And(ref Unsafe.As<T, uint>(ref cell), Unsafe.BitCast<T, uint>(operand)));
        }

        return Fetch(ref cell, operand, static ( a, b ) => a & b);
    }

    public static T FetchOr<T>( ref T cell, T operand ) where T : struct, IBinaryInteger<T>
    {
        if (Unsafe.SizeOf<T>() == 8)
        {
            return Unsafe.BitCast<ulong, T>(Interlocked.Or(ref Unsafe.As<T, ulong>(ref cell), Unsafe.BitCast<T, ulong>(operand)));
        }

        if (Unsafe.SizeOf<T>() == 4)
        {
            return Unsafe.BitCast<uint, T>(Interlocked.Or(ref Unsafe.As<T, uint>(ref cell), Unsafe.BitCast<T, uint>(operand)));
        }

        return Fetch(ref cell, operand, static ( a, b ) => a | b);
    }

    public static T FetchXor<T>( ref T cell, T operand ) where T : struct, IBinaryInteger<T> => Fetch(ref cell, operand, static ( a, b ) => a ^ b);

    /// <summary>
    /// Adds <paramref name="operand"/> and returns the new value.
    /// </summary>
    public static T Add<T>( ref T cell, T operand ) where T : struct, IBinaryInteger<T>
    {
        if (Unsafe.SizeOf<T>() == 8)
        {
            return Unsafe.BitCast<ulong, T>(Interlocked.Add(ref Unsafe.As<T, ulong>(ref cell), Unsafe.BitCast<T, ulong>(operand)));
        }

        if (Unsafe.SizeOf<T>() == 4)
        {
            return Unsafe.BitCast<uint, T>(Interlocked.Add(ref Unsafe.As<T, uint>(ref cell), Unsafe.BitCast<T, uint>(operand)));
        }

        return Fetch(ref cell, operand, static ( a, b ) => a + b) + operand;
    }

    public static T FetchAdd<T>( ref T cell, T operand ) where T : struct, IBinaryInteger<T> => Add(ref cell, operand) - operand;
    public static T Sub<T>( ref T cell, T operand ) where T : struct, IBinaryInteger<T> => Add(ref cell, T.Zero - operand);
    public static T FetchSub<T>( ref T cell, T operand ) where T : struct, IBinaryInteger<T> => Sub(ref cell, operand) + operand;
    public static T And<T>( ref T cell, T operand ) where T : struct, IBinaryInteger<T> => FetchAnd(ref cell, operand) & operand;
    public static T Or<T>( ref T cell, T operand ) where T : struct, IBinaryInteger<T> => FetchOr(ref cell, operand) | operand;
    public static T Xor<T>( ref T cell, T operand ) where T : struct, IBinaryInteger<T> => FetchXor(ref cell, operand) ^ operand;

    private static T Fetch<T>( ref T cell, T operand, Func<T, T, T> op ) where T : struct, IBinaryInteger<T>
    {
        var seen = Load(ref cell);
        while (true)
        {
            var previous = Interlocked.CompareExchange(ref cell, op(seen, operand), seen);
            if (previous == seen)
            {
                return previous;
            }

            seen = previous;
        }
    }

    private static bool BitEquals<T>( T a, T b ) =>
        MemoryMarshal.CreateReadOnlySpan(ref Unsafe.As<T, byte>(ref a), Unsafe.SizeOf<T>())
            .SequenceEqual(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.As<T, byte>(ref b), Unsafe.SizeOf<T>()));
}
