using System.Collections.Concurrent;
using SwiftlyS2.Shared.Menu;

namespace SwiftlyS2.Core.Menu;

internal sealed class MenuActionRegistry
{
    private readonly ConcurrentDictionary<MenuActionId, OwnedAction> actions = new();

    public IDisposable Register( MenuActionDescriptor descriptor, string owner )
    {
        actions[descriptor.Id] = new OwnedAction(descriptor, owner);
        return new Handle(this, descriptor.Id);
    }

    public bool Unregister( MenuActionId id )
    {
        return actions.TryRemove(id, out _);
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
        return actions.Values
            .Where(owned => string.Equals(owned.Descriptor.Id.Scope, scope, StringComparison.OrdinalIgnoreCase))
            .Select(owned => owned.Descriptor)
            .OrderBy(descriptor => descriptor.Order)
            .ToList();
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
