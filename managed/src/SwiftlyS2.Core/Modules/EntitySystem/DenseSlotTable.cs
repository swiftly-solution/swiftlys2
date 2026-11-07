namespace SwiftlyS2.Core.EntitySystem;

/// <summary>
/// Items looked up by slot index or by native address, also kept packed in a dense array so listing all of them is a single copy.
/// Add and remove are O(1) (swap-remove, so the listing order is not stable). An address maps to one slot at a time.
/// Not thread-safe: callers must lock.
/// </summary>
internal sealed class DenseSlotTable<T> where T : class
{
    private readonly T?[] bySlot;
    private readonly int[] positionOf;
    private readonly T[] dense;
    private readonly uint[] slotAt;
    private readonly nint[] addressAt;
    private readonly Dictionary<nint, uint> slotOfAddress = [];

    public DenseSlotTable( int capacity )
    {
        bySlot = new T?[capacity];
        positionOf = new int[capacity];
        dense = new T[capacity];
        slotAt = new uint[capacity];
        addressAt = new nint[capacity];
    }

    public int Count { get; private set; }

    /// <summary>
    /// Returns the item in <paramref name="slot"/>, or null. Throws <see cref="IndexOutOfRangeException"/> past the capacity.
    /// </summary>
    public T? Get( uint slot ) => bySlot[slot];

    /// <summary>
    /// Returns the item registered under <paramref name="address"/>, or null.
    /// </summary>
    public T? GetByAddress( nint address ) => slotOfAddress.TryGetValue(address, out var slot) ? bySlot[slot] : null;

    public bool ContainsAddress( nint address ) => slotOfAddress.ContainsKey(address);

    /// <summary>
    /// Puts <paramref name="item"/> in <paramref name="slot"/> under <paramref name="address"/>.
    /// Whatever was in the slot is replaced, and so is whichever other slot held the same address.
    /// Throws <see cref="IndexOutOfRangeException"/> past the capacity, before changing anything.
    /// </summary>
    public void Set( uint slot, nint address, T item )
    {
        var occupied = bySlot[slot] is not null;

        if (slotOfAddress.TryGetValue(address, out var holder) && holder != slot)
        {
            RemoveSlot(holder);
        }

        if (occupied)
        {
            var position = positionOf[slot];
            _ = slotOfAddress.Remove(addressAt[position]);
            dense[position] = item;
            addressAt[position] = address;
        }
        else
        {
            dense[Count] = item;
            slotAt[Count] = slot;
            addressAt[Count] = address;
            positionOf[slot] = Count++;
        }

        bySlot[slot] = item;
        slotOfAddress[address] = slot;
    }

    /// <summary>
    /// Removes the item registered under <paramref name="address"/>.
    /// </summary>
    /// <returns>Whether there was one.</returns>
    public bool Remove( nint address )
    {
        if (!slotOfAddress.TryGetValue(address, out var slot))
        {
            return false;
        }

        RemoveSlot(slot);
        return true;
    }

    /// <summary>
    /// Copies every item into a new list.
    /// </summary>
    public List<T> ToList()
    {
        var result = new List<T>(Count);
        System.Runtime.InteropServices.CollectionsMarshal.SetCount(result, Count);
        dense.AsSpan(0, Count).CopyTo(System.Runtime.InteropServices.CollectionsMarshal.AsSpan(result));
        return result;
    }

    private void RemoveSlot( uint slot )
    {
        var position = positionOf[slot];
        _ = slotOfAddress.Remove(addressAt[position]);

        var last = --Count;
        var movedSlot = slotAt[last];
        dense[position] = dense[last];
        slotAt[position] = movedSlot;
        addressAt[position] = addressAt[last];
        positionOf[movedSlot] = position;
        dense[last] = null!;
        bySlot[slot] = null;
    }
}
