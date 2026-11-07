using System.Numerics;

namespace SwiftlyS2.Shared.Misc;

/// <summary>
/// A fixed-size array whose elements are each a thread-safe cell, like <see cref="Atomic{T}"/> but stored contiguously.
/// Same rules as <see cref="Atomic{T}"/> apply to every element; indexes out of range throw <see cref="IndexOutOfRangeException"/>.
/// </summary>
/// <typeparam name="T">The value type to store.</typeparam>
public sealed class AtomicArray<T> where T : struct
{
    private readonly T[] items;

    /// <summary>
    /// Creates an array of <paramref name="length"/> default-initialized cells.
    /// </summary>
    public AtomicArray( int length )
    {
        ArgumentOutOfRangeException.ThrowIfNegative(length);
        items = new T[length];
    }

    public int Length => items.Length;

    /// <summary>
    /// Reads the element at <paramref name="index"/>.
    /// </summary>
    public T Load( int index ) => AtomicOps.Load(ref items[index]);

    /// <summary>
    /// Overwrites the element at <paramref name="index"/>.
    /// </summary>
    public void Store( int index, T newValue ) => Interlocked.Exchange(ref items[index], newValue);

    /// <summary>
    /// Stores <paramref name="newValue"/> at <paramref name="index"/> and returns the previous value.
    /// </summary>
    public T Exchange( int index, T newValue ) => Interlocked.Exchange(ref items[index], newValue);

    /// <summary>
    /// Stores <paramref name="newValue"/> only if the element equals <paramref name="comparand"/>, and returns the previous value.
    /// </summary>
    public T CompareExchange( int index, T newValue, T comparand ) => Interlocked.CompareExchange(ref items[index], newValue, comparand);

    /// <summary>
    /// <c>compare_exchange_strong</c> on one element, see <see cref="Atomic{T}.TryCompareExchange"/>.
    /// </summary>
    public bool TryCompareExchange( int index, ref T expected, T desired ) => AtomicOps.TryCompareExchange(ref items[index], ref expected, desired);

    internal ref T Cell( int index ) => ref items[index];

    /// <summary>
    /// Plain, non-atomic view of the elements from <paramref name="start"/>, for vectorized scans only.
    /// Re-read anything you act on with <see cref="Load"/>.
    /// </summary>
    internal ReadOnlySpan<T> Peek( int start ) => items.AsSpan(start);
}

/// <summary>
/// Arithmetic and bitwise operations for <see cref="AtomicArray{T}"/> over integer types.
/// </summary>
public static class AtomicArrayExtensions
{
    public static T FetchAdd<T>( this AtomicArray<T> array, int index, T operand ) where T : struct, IBinaryInteger<T> => AtomicOps.FetchAdd(ref array.Cell(index), operand);
    public static T FetchSub<T>( this AtomicArray<T> array, int index, T operand ) where T : struct, IBinaryInteger<T> => AtomicOps.FetchSub(ref array.Cell(index), operand);
    public static T FetchXor<T>( this AtomicArray<T> array, int index, T operand ) where T : struct, IBinaryInteger<T> => AtomicOps.FetchXor(ref array.Cell(index), operand);
    public static T FetchAnd<T>( this AtomicArray<T> array, int index, T operand ) where T : struct, IBinaryInteger<T> => AtomicOps.FetchAnd(ref array.Cell(index), operand);
    public static T FetchOr<T>( this AtomicArray<T> array, int index, T operand ) where T : struct, IBinaryInteger<T> => AtomicOps.FetchOr(ref array.Cell(index), operand);

    public static T Add<T>( this AtomicArray<T> array, int index, T operand ) where T : struct, IBinaryInteger<T> => AtomicOps.Add(ref array.Cell(index), operand);
    public static T Sub<T>( this AtomicArray<T> array, int index, T operand ) where T : struct, IBinaryInteger<T> => AtomicOps.Sub(ref array.Cell(index), operand);
    public static T And<T>( this AtomicArray<T> array, int index, T operand ) where T : struct, IBinaryInteger<T> => AtomicOps.And(ref array.Cell(index), operand);
    public static T Or<T>( this AtomicArray<T> array, int index, T operand ) where T : struct, IBinaryInteger<T> => AtomicOps.Or(ref array.Cell(index), operand);
    public static T Xor<T>( this AtomicArray<T> array, int index, T operand ) where T : struct, IBinaryInteger<T> => AtomicOps.Xor(ref array.Cell(index), operand);

    public static T Increment<T>( this AtomicArray<T> array, int index ) where T : struct, IBinaryInteger<T> => AtomicOps.Add(ref array.Cell(index), T.One);
    public static T Decrement<T>( this AtomicArray<T> array, int index ) where T : struct, IBinaryInteger<T> => AtomicOps.Sub(ref array.Cell(index), T.One);
}
