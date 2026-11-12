using System.Numerics;

namespace SwiftlyS2.Shared.Misc;

/// <summary>
/// A fixed-size, thread-safe bit vector made of 64-bit words.
/// This is not part of the game data types. Please use CBitVec for the game variant instead.
/// </summary>
public sealed class CBitVector
{
    private readonly AtomicArray<ulong> words;
    private readonly Atomic<int> count = new();

    /// <summary>
    /// Number of addressable bits.
    /// </summary>
    public int Length { get; }

    /// <summary>
    /// Number of bits currently set, maintained incrementally so reading it is O(1).
    /// </summary>
    public int Count => Math.Max(0, count.Value);

    /// <summary>
    /// Number of 64-bit words backing the vector.
    /// </summary>
    public int QWordCount => words.Length;

    /// <summary>
    /// Creates a vector with every bit cleared.
    /// </summary>
    /// <param name="length">Number of bits, must not be negative.</param>
    public CBitVector( int length )
    {
        ArgumentOutOfRangeException.ThrowIfNegative(length);
        Length = length;
        words = new AtomicArray<ulong>((length + 63) >> 6);
    }

    /// <summary>
    /// Returns whether <paramref name="bit"/> is set.
    /// </summary>
    public bool IsBitSet( int bit ) => (uint)bit < (uint)Length && (words.Load(bit >> 6) & (1UL << (bit & 63))) != 0;

    /// <summary>
    /// Yields the index of every set bit in ascending order.
    /// </summary>
    public SetBitEnumerable GetSetBits() => new(words);

    /// <summary>
    /// Sets <paramref name="bit"/>.
    /// </summary>
    /// <returns>Whether the bit was already set.</returns>
    public bool Set( int bit )
    {
        if ((uint)bit >= (uint)Length)
        {
            return false;
        }

        var mask = 1UL << (bit & 63);
        var wasSet = (words.FetchOr(bit >> 6, mask) & mask) != 0;
        if (!wasSet)
        {
            _ = count.Increment();
        }

        return wasSet;
    }

    /// <summary>
    /// Clears <paramref name="bit"/>.
    /// </summary>
    /// <returns>Whether the bit was set before.</returns>
    public bool Clear( int bit )
    {
        if ((uint)bit >= (uint)Length)
        {
            return false;
        }

        var mask = 1UL << (bit & 63);
        var wasSet = (words.FetchAnd(bit >> 6, ~mask) & mask) != 0;
        if (wasSet)
        {
            _ = count.Decrement();
        }

        return wasSet;
    }

    /// <summary>
    /// Reads a whole 64-bit word, or 0 if <paramref name="qword"/> is out of range.
    /// </summary>
    public ulong GetQWord( int qword ) => (uint)qword < (uint)words.Length ? words.Load(qword) : 0;

    /// <summary>
    /// Overwrites one 32-bit half of a 64-bit word (dword 0 is the low half of qword 0) and leaves the other half untouched.
    /// </summary>
    public void SetDWord( int dword, uint value )
    {
        if ((uint)dword >= (uint)words.Length * 2)
        {
            return;
        }

        var index = dword >> 1;
        var shift = (dword & 1) * 32;
        var keep = ~(0xFFFFFFFFUL << shift);
        var bits = (ulong)value << shift;
        var valid = index == words.Length - 1 && (Length & 63) != 0 ? (1UL << (Length & 63)) - 1 : ~0UL;

        var expected = words.Load(index);
        while (true)
        {
            var desired = ((expected & keep) | bits) & valid;
            if (words.TryCompareExchange(index, ref expected, desired))
            {
                Track(expected, desired);
                return;
            }
        }
    }

    /// <summary>
    /// Clears every bit.
    /// </summary>
    public void ClearAll()
    {
        for (var i = 0; i < words.Length; i++)
        {
            Track(words.Exchange(i, 0UL), 0UL);
        }
    }

    /// <summary>
    /// Sets every bit in <see cref="Length"/>. Padding bits of the last word stay 0.
    /// </summary>
    public void SetAll()
    {
        for (var i = 0; i < words.Length; i++)
        {
            var value = i == words.Length - 1 && (Length & 63) != 0 ? (1UL << (Length & 63)) - 1 : ~0UL;
            Track(words.Exchange(i, value), value);
        }
    }

    /// <summary>
    /// Copies every word of <paramref name="source"/> into this vector.
    /// </summary>
    public void CopyFrom( CBitVector source )
    {
        ThrowIfLengthMismatch(source);
        for (var i = 0; i < words.Length; i++)
        {
            var value = source.GetQWord(i);
            Track(words.Exchange(i, value), value);
        }
    }

    /// <summary>
    /// Clears every bit of this vector that is set in <paramref name="filter"/> (<c>this &amp;= ~filter</c>).
    /// </summary>
    public void Filter( CBitVector filter )
    {
        ThrowIfLengthMismatch(filter);
        for (var i = 0; i < words.Length; i++)
        {
            var mask = filter.GetQWord(i);
            if (mask != 0)
            {
                var before = words.FetchAnd(i, ~mask);
                Track(before, before & ~mask);
            }
        }
    }

    /// <summary>
    /// Applies the change of one word to <see cref="Count"/>.
    /// </summary>
    private void Track( ulong before, ulong after )
    {
        var delta = BitOperations.PopCount(after) - BitOperations.PopCount(before);
        if (delta != 0)
        {
            _ = count.Add(delta);
        }
    }

    private void ThrowIfLengthMismatch( CBitVector other )
    {
        ArgumentNullException.ThrowIfNull(other);
        if (other.Length != Length)
        {
            throw new ArgumentException($"Length mismatch: {other.Length} != {Length}.", nameof(other));
        }
    }
}
