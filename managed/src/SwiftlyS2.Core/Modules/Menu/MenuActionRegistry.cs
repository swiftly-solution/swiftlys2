using System.Collections.Concurrent;
using SwiftlyS2.Shared.Menu;

namespace SwiftlyS2.Core.Menu;

internal sealed class MenuActionRegistry
{
    private readonly ConcurrentDictionary<MenuActionId, OwnedAction> actions = new();
    private readonly ConcurrentDictionary<string, IReadOnlyList<MenuActionDescriptor>> scopes = new(StringComparer.OrdinalIgnoreCase);

    private int version;
    public int Version => Volatile.Read(ref version);

    public IDisposable Register( MenuActionDescriptor descriptor, string owner )
    {
        actions[descriptor.Id] = new OwnedAction(descriptor, owner);
        Changed();
        return new Handle(this, descriptor.Id);
    }

    public bool Unregister( MenuActionId id )
    {
        var removed = actions.TryRemove(id, out _);

        if (removed)
        {
            Changed();
        }

        return removed;
    }

    public bool TryGet( MenuActionId id, out MenuActionDescriptor descriptor )
    {
        if (actions.TryGetValue(id, out var owned))
        {
            descriptor = owned.Descriptor;
            return true;
        }

        descriptor = null!;
        return false;
    }

    public IReadOnlyList<MenuActionDescriptor> GetScope( string scope )
    {
        if (scopes.TryGetValue(scope, out var cached))
        {
            return cached;
        }

        var seen = Version;
        var built = actions.Values
            .Where(owned => string.Equals(owned.Descriptor.Id.Scope, scope, StringComparison.OrdinalIgnoreCase))
            .Select(owned => owned.Descriptor)
            .OrderBy(descriptor => descriptor.Order)
            .ToList();

        _ = scopes.TryAdd(scope, built);

        if (seen != Version)
        {
            _ = scopes.TryRemove(scope, out _);
        }

        return built;
    }

    public void RemoveByOwner( string owner )
    {
        foreach (var pair in actions)
        {
            if (string.Equals(pair.Value.Owner, owner, StringComparison.Ordinal))
            {
                _ = actions.TryRemove(pair.Key, out _);
            }
        }

        Changed();
    }

    private void Changed()
    {
        _ = Interlocked.Increment(ref version);
        scopes.Clear();
    }

    private readonly record struct OwnedAction( MenuActionDescriptor Descriptor, string Owner );

    private sealed class Handle( MenuActionRegistry registry, MenuActionId id ) : IDisposable
    {
        private bool disposed;

        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;
            _ = registry.Unregister(id);
        }
    }
}

internal sealed class OwnedMenuActionRegistry( MenuActionRegistry registry, string owner ) : IMenuActionRegistry
{
    private readonly List<IDisposable> handles = [];
    private readonly Lock handleLock = new();

    public IDisposable Register( MenuActionDescriptor descriptor )
    {
        var handle = registry.Register(descriptor, owner);

        lock (handleLock)
        {
            handles.Add(handle);
        }

        return handle;
    }

    public bool Unregister( MenuActionId id ) => registry.Unregister(id);

    public bool TryGet( MenuActionId id, out MenuActionDescriptor descriptor ) => registry.TryGet(id, out descriptor);

    public IReadOnlyList<MenuActionDescriptor> GetScope( string scope ) => registry.GetScope(scope);

    public void ReleaseAll()
    {
        lock (handleLock)
        {
            foreach (var handle in handles)
            {
                handle.Dispose();
            }

            handles.Clear();
        }

        registry.RemoveByOwner(owner);
    }
}
