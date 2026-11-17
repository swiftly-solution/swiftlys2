using System.Numerics;

namespace SwiftlyS2.Shared.Misc;

/// <summary>
/// A thread-safe cell, the counterpart of C++ <c>std::atomic&lt;T&gt;</c>.
/// Every operation is atomic and acts as a full memory barrier, except <see cref="Load"/> on 4- and 8-byte types, which is an acquire load.
/// For reference types (e.g. <see cref="string"/>) the cell holds a reference: reads and writes are atomic, and
/// <see cref="CompareExchange"/> / <see cref="TryCompareExchange"/> compare by reference, not by value.
/// </summary>
/// <typeparam name="T">The type to store: a primitive, an enum, or a reference type.</typeparam>
public sealed class Atomic<T>
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
    public T Load() => AtomicOps.Load(ref cell);

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
    public bool TryCompareExchange( ref T expected, T desired ) => AtomicOps.TryCompareExchange(ref cell, ref expected, desired);

    /// <summary>
    /// Reads the current value, like the C++ <c>operator T</c>.
    /// </summary>
    public static implicit operator T( Atomic<T> atomic ) => atomic.Load();

    public override string ToString() => Load()?.ToString() ?? string.Empty;

    internal ref T Cell => ref cell;
}

/// <summary>
/// Arithmetic and bitwise operations for <see cref="Atomic{T}"/> over integer types.
/// </summary>
public static class AtomicExtensions
{
    public static T FetchAdd<T>( this Atomic<T> atomic, T operand ) where T : struct, IBinaryInteger<T> => AtomicOps.FetchAdd(ref atomic.Cell, operand);
    public static T FetchSub<T>( this Atomic<T> atomic, T operand ) where T : struct, IBinaryInteger<T> => AtomicOps.FetchSub(ref atomic.Cell, operand);
    public static T FetchXor<T>( this Atomic<T> atomic, T operand ) where T : struct, IBinaryInteger<T> => AtomicOps.FetchXor(ref atomic.Cell, operand);
    public static T FetchAnd<T>( this Atomic<T> atomic, T operand ) where T : struct, IBinaryInteger<T> => AtomicOps.FetchAnd(ref atomic.Cell, operand);
    public static T FetchOr<T>( this Atomic<T> atomic, T operand ) where T : struct, IBinaryInteger<T> => AtomicOps.FetchOr(ref atomic.Cell, operand);

    public static T Add<T>( this Atomic<T> atomic, T operand ) where T : struct, IBinaryInteger<T> => AtomicOps.Add(ref atomic.Cell, operand);
    public static T Sub<T>( this Atomic<T> atomic, T operand ) where T : struct, IBinaryInteger<T> => AtomicOps.Sub(ref atomic.Cell, operand);
    public static T And<T>( this Atomic<T> atomic, T operand ) where T : struct, IBinaryInteger<T> => AtomicOps.And(ref atomic.Cell, operand);
    public static T Or<T>( this Atomic<T> atomic, T operand ) where T : struct, IBinaryInteger<T> => AtomicOps.Or(ref atomic.Cell, operand);
    public static T Xor<T>( this Atomic<T> atomic, T operand ) where T : struct, IBinaryInteger<T> => AtomicOps.Xor(ref atomic.Cell, operand);

    public static T Increment<T>( this Atomic<T> atomic ) where T : struct, IBinaryInteger<T> => AtomicOps.Add(ref atomic.Cell, T.One);
    public static T Decrement<T>( this Atomic<T> atomic ) where T : struct, IBinaryInteger<T> => AtomicOps.Sub(ref atomic.Cell, T.One);
}
