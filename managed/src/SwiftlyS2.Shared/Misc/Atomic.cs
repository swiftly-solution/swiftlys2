using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.Misc;

/// <summary>
/// A thread-safe cell, the counterpart of C++ <c>std::atomic&lt;T&gt;</c>.
/// Every operation is atomic and acts as a full memory barrier.
/// </summary>
/// <typeparam name="T">The value type to store.</typeparam>
public sealed class Atomic<T> where T : struct
{
    private T cell;

    public Atomic( T initialValue = default )
    {
        cell = initialValue;
    }

    /// <summary>
    /// Gets or sets the current value (<c>load</c> / <c>store</c>).
    /// </summary>
    public T Value {
        get => Load();
        set => Store(value);
    }

    /// <summary>
    /// Reads the current value.
    /// </summary>
    public T Load() => Interlocked.CompareExchange(ref cell, default, default);

    /// <summary>
    /// Overwrites the current value.
    /// </summary>
    public void Store( T newValue ) => Interlocked.Exchange(ref cell, newValue);

    /// <summary>
    /// Stores <paramref name="newValue"/> and returns the previous value.
    /// </summary>
    public T Exchange( T newValue ) => Interlocked.Exchange(ref cell, newValue);

    /// <summary>
    /// Stores <paramref name="newValue"/> only if the current value equals <paramref name="comparand"/>.
    /// </summary>
    public T CompareExchange( T newValue, T comparand ) => Interlocked.CompareExchange(ref cell, newValue, comparand);

    /// <summary>
    /// <c>compare_exchange_strong</c>: stores <paramref name="desired"/> only if the current value equals <paramref name="expected"/>.
    /// On failure <paramref name="expected"/> is overwritten with the value that was actually found.
    /// </summary>
    public bool TryCompareExchange( ref T expected, T desired )
    {
        var seen = Interlocked.CompareExchange(ref cell, desired, expected);
        if (BitEquals(seen, expected))
        {
            return true;
        }

        expected = seen;
        return false;
    }

    /// <summary>
    /// Reads the current value, like the C++ <c>operator T</c>.
    /// </summary>
    public static implicit operator T( Atomic<T> atomic ) => atomic.Load();

    public override string ToString() => Load().ToString() ?? string.Empty;

    private static bool BitEquals( T a, T b ) =>
        MemoryMarshal.CreateReadOnlySpan(ref Unsafe.As<T, byte>(ref a), Unsafe.SizeOf<T>())
            .SequenceEqual(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.As<T, byte>(ref b), Unsafe.SizeOf<T>()));
}

/// <summary>
/// Arithmetic and bitwise operations for <see cref="Atomic{T}"/> over integer types.
/// </summary>
public static class AtomicExtensions
{
    public static T FetchAdd<T>( this Atomic<T> atomic, T operand ) where T : struct, IBinaryInteger<T> => Fetch(atomic, operand, static ( a, b ) => a + b);
    public static T FetchSub<T>( this Atomic<T> atomic, T operand ) where T : struct, IBinaryInteger<T> => Fetch(atomic, operand, static ( a, b ) => a - b);
    public static T FetchAnd<T>( this Atomic<T> atomic, T operand ) where T : struct, IBinaryInteger<T> => Fetch(atomic, operand, static ( a, b ) => a & b);
    public static T FetchOr<T>( this Atomic<T> atomic, T operand ) where T : struct, IBinaryInteger<T> => Fetch(atomic, operand, static ( a, b ) => a | b);
    public static T FetchXor<T>( this Atomic<T> atomic, T operand ) where T : struct, IBinaryInteger<T> => Fetch(atomic, operand, static ( a, b ) => a ^ b);

    public static T Add<T>( this Atomic<T> atomic, T operand ) where T : struct, IBinaryInteger<T> => atomic.FetchAdd(operand) + operand;
    public static T Sub<T>( this Atomic<T> atomic, T operand ) where T : struct, IBinaryInteger<T> => atomic.FetchSub(operand) - operand;
    public static T And<T>( this Atomic<T> atomic, T operand ) where T : struct, IBinaryInteger<T> => atomic.FetchAnd(operand) & operand;
    public static T Or<T>( this Atomic<T> atomic, T operand ) where T : struct, IBinaryInteger<T> => atomic.FetchOr(operand) | operand;
    public static T Xor<T>( this Atomic<T> atomic, T operand ) where T : struct, IBinaryInteger<T> => atomic.FetchXor(operand) ^ operand;

    public static T Increment<T>( this Atomic<T> atomic ) where T : struct, IBinaryInteger<T> => atomic.Add(T.One);
    public static T Decrement<T>( this Atomic<T> atomic ) where T : struct, IBinaryInteger<T> => atomic.Sub(T.One);

    private static T Fetch<T>( Atomic<T> atomic, T operand, Func<T, T, T> op ) where T : struct, IBinaryInteger<T>
    {
        var seen = atomic.Load();
        while (true)
        {
            var previous = atomic.CompareExchange(op(seen, operand), seen);
            if (previous == seen)
            {
                return previous;
            }

            seen = previous;
        }
    }
}
