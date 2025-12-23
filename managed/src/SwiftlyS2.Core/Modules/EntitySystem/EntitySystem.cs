
using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Events;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.Schemas;
using SwiftlyS2.Core.Extensions;
using SwiftlyS2.Shared.Profiler;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.EntitySystem;
using SwiftlyS2.Core.SchemaDefinitions;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.EntitySystem;

internal class EntitySystemService : IEntitySystemService, IDisposable
{
    private readonly ILoggerFactory loggerFactory;
    private readonly IContextedProfilerService profiler;
    private readonly IEventSubscriber eventSubscriber;

    [Obsolete("Use outputHooks instead.")]
    private readonly ConcurrentDictionary<Guid, EntityOutputHookCallback> outputCallbacks = new();
    private readonly ConcurrentDictionary<Guid, EventDelegates.OnEntityFireOutputHookEvent> outputHooks = new();
    private readonly ConcurrentDictionary<Guid, EventDelegates.OnEntityIdentityAcceptInputHook> inputHooks = new();

    private volatile bool disposed;

    public EntitySystemService( IEventSubscriber eventSubscriber, ILoggerFactory loggerFactory, IContextedProfilerService profiler )
    {
        this.loggerFactory = loggerFactory;
        this.profiler = profiler;
        this.eventSubscriber = eventSubscriber;
        this.disposed = false;
    }

    public T CreateEntity<T>() where T : class, ISchemaClass<T>
    {
        return string.IsNullOrWhiteSpace(T.ClassName)
            ? throw new ArgumentException($"Can't create entity with class {typeof(T).Name}, which doesn't have a designer name.")
            : CreateEntityByDesignerName<T>(T.ClassName);
    }

    public T CreateEntityByDesignerName<T>( string designerName ) where T : ISchemaClass<T>
    {
        var handle = NativeEntitySystem.CreateEntityByName(designerName);
        return handle == nint.Zero
            ? throw new ArgumentException($"Failed to create entity by designer name: {designerName}, probably invalid designer name.")
            : T.From(handle);
    }

    public CHandle<T> GetRefEHandle<T>( T entity ) where T : class, ISchemaClass<T>
    {
        return new CHandle<T> { Raw = NativeEntitySystem.GetEntityHandleFromEntity(entity.Address) };
    }

    public CCSGameRules? GetGameRules()
    {
        var handle = NativeEntitySystem.GetGameRules();
        return handle.IsValidPtr() ? new CCSGameRulesImpl(handle) : null;
    }

    public IEnumerable<CEntityInstance> GetAllEntities()
    {
        CEntityIdentity? pFirst = new CEntityIdentityImpl(NativeEntitySystem.GetFirstActiveEntity());

        while (pFirst != null && pFirst.IsValid)
        {
            yield return new CEntityInstanceImpl(pFirst.Address.Read<nint>());
            pFirst = pFirst.Next;
        }
    }

    public IEnumerable<T> GetAllEntitiesByClass<T>() where T : class, ISchemaClass<T>
    {
        return string.IsNullOrWhiteSpace(T.ClassName)
            ? throw new ArgumentException($"Can't get entities with class {typeof(T).Name}, which doesn't have a designer name")
            : GetAllEntities().Where(( entity ) => entity.Entity?.DesignerName == T.ClassName).Select(( entity ) => T.From(entity.Address));
    }

    public IEnumerable<T> GetAllEntitiesByDesignerName<T>( string designerName ) where T : class, ISchemaClass<T>
    {
        return GetAllEntities()
            .Where(entity => entity.Entity?.DesignerName == designerName)
            .Select(entity => T.From(entity.Address));
    }

    public T? GetEntityByIndex<T>( uint index ) where T : class, ISchemaClass<T>
    {
        var handle = NativeEntitySystem.GetEntityByIndex(index);
        return handle == nint.Zero ? null : T.From(handle);
    }

    [Obsolete("Use HookEntityOutput(string outputName, Action<IOnEntityFireOutputHookEvent> callback) instead.")]
    public Guid HookEntityOutput<T>( string outputName, IEntitySystemService.EntityOutputHandler callback ) where T : class, ISchemaClass<T>
    {
        var hook = new EntityOutputHookCallback(T.ClassName ?? throw new ArgumentException($"Can't hook entity output with class {typeof(T).Name}, which doesn't have a designer name"), outputName, callback, loggerFactory, profiler);
        _ = outputCallbacks.TryAdd(hook.Guid, hook);
        return hook.Guid;
    }

    public Guid HookEntityOutput<T>( string outputName, IEntitySystemService.EntityOutputEventHandler callback ) where T : class, ISchemaClass<T>
    {
        if (T.ClassName == null)
        {
            throw new ArgumentException($"Can't hook entity output with class {typeof(T).Name}, which doesn't have a designer name.");
        }
        if (string.IsNullOrWhiteSpace(outputName))
        {
            throw new ArgumentException("Output name cannot be null or empty.");
        }

        var className = T.ClassName;
        outputName = outputName.Trim();
        void handler( IOnEntityFireOutputHookEvent @event )
        {
            if (outputName == "*" || outputName.Equals(@event.OutputName, StringComparison.OrdinalIgnoreCase))
            {
                if (@event.DesignerName.Equals(className, StringComparison.OrdinalIgnoreCase))
                {
                    callback(@event);
                }
            }
        }

        var guid = Guid.NewGuid();
        _ = outputHooks.TryAdd(guid, handler);
        eventSubscriber.OnEntityFireOutputHook += handler;

        return guid;
    }

    public Guid HookEntityOutput( string designerName, string outputName, IEntitySystemService.EntityOutputEventHandler callback )
    {
        if (string.IsNullOrWhiteSpace(designerName))
        {
            throw new ArgumentException("Designer name cannot be null or empty.");
        }
        if (string.IsNullOrWhiteSpace(outputName))
        {
            throw new ArgumentException("Output name cannot be null or empty.");
        }

        designerName = designerName.Trim();
        outputName = outputName.Trim();
        void handler( IOnEntityFireOutputHookEvent @event )
        {
            if (outputName == "*" || outputName.Equals(@event.OutputName, StringComparison.OrdinalIgnoreCase))
            {
                if (designerName == "*" || @event.DesignerName.Equals(designerName, StringComparison.OrdinalIgnoreCase))
                {
                    callback(@event);
                }
            }
        }

        var guid = Guid.NewGuid();
        _ = outputHooks.TryAdd(guid, handler);
        eventSubscriber.OnEntityFireOutputHook += handler;

        return guid;
    }

    public Guid HookEntityInput<T>( string inputName, IEntitySystemService.EntityInputEventHandler callback ) where T : class, ISchemaClass<T>
    {
        if (T.ClassName == null)
        {
            throw new ArgumentException($"Can't hook entity input with class {typeof(T).Name}, which doesn't have a designer name.");
        }
        if (string.IsNullOrWhiteSpace(inputName))
        {
            throw new ArgumentException("Input name cannot be null or empty.");
        }

        var className = T.ClassName;
        inputName = inputName.Trim();
        void handler( IOnEntityIdentityAcceptInputHookEvent @event )
        {
            if (inputName == "*" || inputName.Equals(@event.InputName, StringComparison.OrdinalIgnoreCase))
            {
                if (@event.DesignerName.Equals(className, StringComparison.OrdinalIgnoreCase))
                {
                    callback(@event);
                }
            }
        }

        var guid = Guid.NewGuid();
        _ = inputHooks.TryAdd(guid, handler);
        eventSubscriber.OnEntityIdentityAcceptInputHook += handler;

        return guid;
    }

    public Guid HookEntityInput( string designerName, string inputName, IEntitySystemService.EntityInputEventHandler callback )
    {
        if (string.IsNullOrWhiteSpace(designerName))
        {
            throw new ArgumentException("Designer name cannot be null or empty.");
        }
        if (string.IsNullOrWhiteSpace(inputName))
        {
            throw new ArgumentException("Input name cannot be null or empty.");
        }

        designerName = designerName.Trim();
        inputName = inputName.Trim();
        void handler( IOnEntityIdentityAcceptInputHookEvent @event )
        {
            if (inputName == "*" || inputName.Equals(@event.InputName, StringComparison.OrdinalIgnoreCase))
            {
                if (designerName == "*" || @event.DesignerName.Equals(designerName, StringComparison.OrdinalIgnoreCase))
                {
                    callback(@event);
                }
            }
        }

        var guid = Guid.NewGuid();
        _ = inputHooks.TryAdd(guid, handler);
        eventSubscriber.OnEntityIdentityAcceptInputHook += handler;

        return guid;
    }

    public bool UnhookEntityOutput( Guid guid )
    {
        if (outputCallbacks.TryRemove(guid, out var callback))
        {
            callback.Dispose();
            return true;
        }
        else if (outputHooks.TryRemove(guid, out var handler))
        {
            eventSubscriber.OnEntityFireOutputHook -= handler;
            return true;
        }
        return false;
    }

    public bool UnhookEntityInput( Guid guid )
    {
        if (inputHooks.TryRemove(guid, out var handler))
        {
            eventSubscriber.OnEntityIdentityAcceptInputHook -= handler;
            return true;
        }
        return false;
    }

    public void Dispose()
    {
        if (disposed)
        {
            return;
        }
        disposed = true;

        foreach (var callback in outputCallbacks.Values)
        {
            callback.Dispose();
        }
        outputCallbacks.Clear();

        foreach (var handler in outputHooks.Values)
        {
            eventSubscriber.OnEntityFireOutputHook -= handler;
        }
        outputHooks.Clear();

        foreach (var handler in inputHooks.Values)
        {
            eventSubscriber.OnEntityIdentityAcceptInputHook -= handler;
        }
        inputHooks.Clear();

        GC.SuppressFinalize(this);
    }

    ~EntitySystemService()
    {
        Dispose();
    }
}