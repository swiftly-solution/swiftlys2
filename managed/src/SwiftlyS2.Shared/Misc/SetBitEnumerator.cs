using System.Numerics;

namespace SwiftlyS2.Shared.Misc;

/// <summary>
/// Struct enumerator over the set bits of a <see cref="CBitVector"/>, in ascending order.
/// </summary>
public struct SetBitEnumerator : IEnumerator<int>
{
    private readonly AtomicArray<ulong>? words;
    private int q;
    private ulong word;

    internal SetBitEnumerator( AtomicArray<ulong>? words )
    {
        this.words = words;
        q = -1;
    }

    public int Current { get; private set; }

    object System.Collections.IEnumerator.Current => Current;

    public bool MoveNext()
    {
        if (words is null)
        {
            return false;
        }

        while (word == 0)
        {
            var next = q + 1;
            var offset = next < words.Length ? NextNonEmpty(words.Peek(next)) : -1;
            if (offset < 0)
            {
                q = words.Length;
                return false;
            }

            q = next + offset;
            word = words.Load(q);
        }

        Current = (q << 6) + BitOperations.TrailingZeroCount(word);
        word &= word - 1;
        return true;
    }

    private static int NextNonEmpty( ReadOnlySpan<ulong> span )
    {
        var probe = Math.Min(span.Length, 8);
        for (var i = 0; i < probe; i++)
        {
            if (span[i] != 0)
            {
                return i;
            }
        }

        if (span.Length == probe)
        {
            return -1;
        }

        var rest = span[probe..].IndexOfAnyExcept(0UL);
        return rest < 0 ? -1 : probe + rest;
    }

    public void Reset() => throw new NotSupportedException();

    public void Dispose()
    {
    }
}
