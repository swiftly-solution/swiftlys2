using System.Collections.Concurrent;
using SwiftlyS2.Shared.Menu;

namespace SwiftlyS2.Core.Menu;

internal sealed class MenuRendererRegistry
{
    private readonly ConcurrentDictionary<string, OwnedRenderer> renderers = new(StringComparer.OrdinalIgnoreCase);
    private readonly List<OwnedComponentRenderer> componentRenderers = [];
    private readonly Lock componentLock = new();

    public IReadOnlyList<string> RendererIds => renderers.Keys.ToList();

    public IDisposable Register( IMenuRenderer renderer, string owner )
    {
        renderers[renderer.Id] = new OwnedRenderer(renderer, owner);
        return new RendererHandle(this, renderer.Id);
    }

    public IDisposable RegisterComponentRenderer( IComponentRenderer componentRenderer, string owner )
    {
        var owned = new OwnedComponentRenderer(componentRenderer, owner);

        lock (componentLock)
        {
            componentRenderers.Add(owned);
        }

        return new ComponentHandle(this, owned);
    }

    public bool TryGet( string rendererId, out IMenuRenderer renderer )
    {
        if (renderers.TryGetValue(rendererId, out var owned))
        {
            renderer = owned.Renderer;
            return true;
        }

        renderer = null!;
        return false;
    }

    public IComponentRenderer? ResolveComponentRenderer( string rendererId, Type componentType )
    {
        lock (componentLock)
        {
            IComponentRenderer? best = null;
            var bestDepth = -1;

            foreach (var owned in componentRenderers)
            {
                if (!string.Equals(owned.ComponentRenderer.RendererId, rendererId, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                if (!owned.ComponentRenderer.ComponentType.IsAssignableFrom(componentType))
                {
                    continue;
                }

                var depth = InheritanceDepth(componentType, owned.ComponentRenderer.ComponentType);

                if (depth > bestDepth)
                {
                    bestDepth = depth;
                    best = owned.ComponentRenderer;
                }
            }

            return best;
        }
    }

    public void RemoveRenderer( string rendererId )
    {
        _ = renderers.TryRemove(rendererId, out _);
    }

    public void RemoveByOwner( string owner )
    {
        foreach (var pair in renderers)
        {
            if (string.Equals(pair.Value.Owner, owner, StringComparison.Ordinal))
            {
                _ = renderers.TryRemove(pair.Key, out _);
            }
        }

        lock (componentLock)
        {
            _ = componentRenderers.RemoveAll(owned => string.Equals(owned.Owner, owner, StringComparison.Ordinal));
        }
    }

    private void RemoveComponentRenderer( OwnedComponentRenderer owned )
    {
        lock (componentLock)
        {
            _ = componentRenderers.Remove(owned);
        }
    }

    private static int InheritanceDepth( Type derived, Type target )
    {
        if (target.IsInterface)
        {
            return 0;
        }

        var depth = 0;
        var current = derived;

        while (current is not null && current != target)
        {
            current = current.BaseType;
            depth++;
        }

        return current is null ? 0 : depth;
    }

    private readonly record struct OwnedRenderer( IMenuRenderer Renderer, string Owner );

    private sealed record OwnedComponentRenderer( IComponentRenderer ComponentRenderer, string Owner );

    private sealed class RendererHandle( MenuRendererRegistry registry, string rendererId ) : IDisposable
    {
        private bool disposed;

        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;
            registry.RemoveRenderer(rendererId);
        }
    }

    private sealed class ComponentHandle( MenuRendererRegistry registry, OwnedComponentRenderer owned ) : IDisposable
    {
        private bool disposed;

        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;
            registry.RemoveComponentRenderer(owned);
        }
    }
}

internal sealed class OwnedMenuRendererRegistry( MenuRendererRegistry registry, string owner ) : IMenuRendererRegistry
{
    private readonly List<IDisposable> handles = [];
    private readonly Lock handleLock = new();

    public IReadOnlyList<string> RendererIds => registry.RendererIds;

    public IDisposable Register( IMenuRenderer renderer )
    {
        var handle = registry.Register(renderer, owner);

        lock (handleLock)
        {
            handles.Add(handle);
        }

        return handle;
    }

    public IDisposable RegisterComponentRenderer( IComponentRenderer componentRenderer )
    {
        var handle = registry.RegisterComponentRenderer(componentRenderer, owner);

        lock (handleLock)
        {
            handles.Add(handle);
        }

        return handle;
    }

    public bool TryGet( string rendererId, out IMenuRenderer renderer ) => registry.TryGet(rendererId, out renderer);

    public IComponentRenderer? ResolveComponentRenderer( string rendererId, Type componentType )
        => registry.ResolveComponentRenderer(rendererId, componentType);

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
