using System.Runtime.CompilerServices;
using SwiftlyS2.Shared.Misc;

namespace SwiftlyS2.Core.Services;

/// <summary>
/// Routes dispatch to only the owners registered for a given kind, instead of iterating every subscriber.
/// Writers take <see cref="Gate"/>; <see cref="Get"/> is lock-free and returns an immutable snapshot of the distinct owners (registration order).
/// <paramref name="onFirst"/> / <paramref name="onLast"/> run under <see cref="Gate"/> when a kind goes from 0 to 1 and from 1 to 0 registrations
/// (use them to install / remove a native hook). <paramref name="onFirst"/> runs before any state changes, so a throw leaves the router untouched.
/// </summary>
/// <typeparam name="TKind">An int-backed enum with contiguous values starting at 0.</typeparam>
/// <typeparam name="TOwner">The dispatch target, usually the per-plugin service.</typeparam>
internal sealed class SubscriberRouter<TKind, TOwner>( Action<TKind>? onFirst = null, Action<TKind>? onLast = null )
    where TKind : unmanaged, Enum
    where TOwner : class
{
    static SubscriberRouter()
    {
        if (Unsafe.SizeOf<TKind>() != sizeof(int)) throw new InvalidOperationException($"{typeof(TKind)} must be backed by int.");
    }

    private static int Index( TKind kind ) => Unsafe.As<TKind, int>(ref kind);

    private static readonly int KindCount = Enum.GetValues<TKind>().Length;

    private readonly List<TOwner>[] owners = [.. Enumerable.Range(0, KindCount).Select(_ => new List<TOwner>())];
    private readonly int[] externalRefs = new int[KindCount];
    private readonly AtomicArray<TOwner[]> snapshots = CreateSnapshots();

    private static AtomicArray<TOwner[]> CreateSnapshots()
    {
        var array = new AtomicArray<TOwner[]>(KindCount);
        for (var i = 0; i < KindCount; i++) array.Store(i, []);
        return array;
    }

    /// <summary>Guards registrations. Reentrant, so callbacks and callers may take it too.</summary>
    internal Lock Gate { get; } = new();

    /// <summary>Distinct owners registered for <paramref name="kind"/>. Never null, never mutated after return.</summary>
    internal TOwner[] Get( TKind kind ) => snapshots.Load(Index(kind));

    internal void Add( TKind kind, TOwner owner )
    {
        lock (Gate)
        {
            var i = Index(kind);
            if (!HasRefs(i)) onFirst?.Invoke(kind);
            owners[i].Add(owner);
            Publish(i);
        }
    }

    /// <returns>false if <paramref name="owner"/> was not registered for <paramref name="kind"/>.</returns>
    internal bool Remove( TKind kind, TOwner owner )
    {
        lock (Gate)
        {
            var i = Index(kind);
            if (!owners[i].Remove(owner)) return false;
            Publish(i);
            if (!HasRefs(i)) onLast?.Invoke(kind);
            return true;
        }
    }

    internal void AddExternal( TKind kind )
    {
        lock (Gate)
        {
            var i = Index(kind);
            if (!HasRefs(i)) onFirst?.Invoke(kind);
            externalRefs[i]++;
        }
    }

    internal void RemoveExternal( TKind kind )
    {
        lock (Gate)
        {
            var i = Index(kind);
            if (externalRefs[i] == 0) return;
            externalRefs[i]--;
            if (!HasRefs(i)) onLast?.Invoke(kind);
        }
    }

    private bool HasRefs( int i ) => owners[i].Count + externalRefs[i] > 0;

    private void Publish( int i ) => snapshots.Store(i, [.. owners[i].Distinct()]);
}
