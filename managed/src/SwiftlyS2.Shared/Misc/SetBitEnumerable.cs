namespace SwiftlyS2.Shared.Misc;

/// <summary>
/// Allocation-free sequence of set bit indexes returned by <see cref="CBitVector.GetSetBits"/>.
/// </summary>
public readonly struct SetBitEnumerable : IEnumerable<int>
{
    private readonly AtomicArray<ulong>? words;

    internal SetBitEnumerable( AtomicArray<ulong> words ) => this.words = words;

    public SetBitEnumerator GetEnumerator() => new(words);

    IEnumerator<int> IEnumerable<int>.GetEnumerator() => GetEnumerator();

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
}
