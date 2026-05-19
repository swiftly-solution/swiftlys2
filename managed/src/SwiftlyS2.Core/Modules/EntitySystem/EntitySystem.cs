
using System.Collections.Concurrent;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Events;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.Schemas;
using SwiftlyS2.Shared.EntitySystem;
using SwiftlyS2.Core.SchemaDefinitions;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.EntitySystem;

internal class EntitySystemService : IEntitySystemService, IDisposable
{
    private CCSGameRulesProxy? cachedGameRulesProxy;
    private CCSGameRules? cachedGameRules;
    private readonly IEventSubscriber eventSubscriber;

    private readonly ConcurrentDictionary<Guid, EventDelegates.OnEntityFireOutputHookEvent> outputHooks = new();
    private readonly ConcurrentDictionary<Guid, EventDelegates.OnEntityIdentityAcceptInputHook> inputHooks = new();
    private volatile bool disposed;

    public EntitySystemService( IEventSubscriber eventSubscriber )
    {
        this.eventSubscriber = eventSubscriber;
        this.disposed = false;
    }

    private static void ThrowIfEntitySystemInvalid()
    {
        if (!NativeEntitySystem.IsValid())
        {
            throw new InvalidOperationException("Entity system is not valid at this moment.");
        }
    }

    public T CreateEntity<T>() where T : class, ISchemaClass<T>
    {
        ThrowIfEntitySystemInvalid();
        return string.IsNullOrWhiteSpace(T.ClassName)
            ? throw new ArgumentException($"Can't create entity with class {typeof(T).Name}, which doesn't have a designer name.")
            : CreateEntityByDesignerName<T>(T.ClassName);
    }

    public T CreateEntityByDesignerName<T>( string designerName ) where T : class, ISchemaClass<T>
    {
        return (CreateEntityByDesignerName(designerName) as T)!;
    }

    public CEntityInstance CreateEntityByDesignerName( string designerName )
    {
        ThrowIfEntitySystemInvalid();
        var handle = NativeEntitySystem.CreateEntityByName(designerName);
        if (handle == nint.Zero) throw new ArgumentException($"Failed to create entity by designer name: {designerName}, probably invalid designer name.");

        var entity = EntityManager.OnEntityCreated(handle);
        return entity;
    }

    public CHandle<T> GetRefEHandle<T>( T entity ) where T : class, ISchemaClass<T>
    {
        ThrowIfEntitySystemInvalid();
        return new CHandle<T> { Value = entity };
    }

    public CCSGameRules? GetGameRules()
    {
        ThrowIfEntitySystemInvalid();
        if (cachedGameRules != null && cachedGameRulesProxy != null && cachedGameRulesProxy.IsValidEntity)
        {
            return cachedGameRules;
        }
        cachedGameRules = null;
        cachedGameRulesProxy = null;

        if (GetAllEntitiesByClass<CCSGameRulesProxy>().FirstOrDefault() is CCSGameRulesProxy proxy)
        {
            cachedGameRulesProxy = proxy;
            cachedGameRules = cachedGameRulesProxy.GameRules;
            return cachedGameRules;
        }
        return null;
    }

    public IEnumerable<CEntityInstance> GetAllEntities()
    {
        return EntityManager.GetAllEntities();
    }

    public IEnumerable<T> GetAllEntitiesByClass<T>() where T : class, ISchemaClass<T>
    {
        return GetAllEntities().OfType<T>();
    }

    public IEnumerable<T> GetAllEntitiesByDesignerName<T>( string designerName ) where T : class, ISchemaClass<T>
    {
        return GetAllEntities()
            .Where(entity => entity.Entity?.DesignerName == designerName)
            .Select(entity => (entity as T)!);
    }

    public T? GetEntityByIndex<T>( uint index ) where T : class, ISchemaClass<T>
    {
        var ent = GetEntityByIndex(index);

        return ent == null
            ? null
            : ent is T e
            ? e
            : throw new InvalidOperationException($"Invalid entity type. Requested: {typeof(T).Name}, Actual: {ent!.GetType().Name}.");
    }

    public CEntityInstance? GetEntityByIndex( uint index )
    {
        return EntityManager.GetEntityByIndex(index);
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
        if (outputHooks.TryRemove(guid, out var handler))
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

    public T? GetEntityByAddress<T>( nint address ) where T : class, ISchemaClass<T>
    {
        var ent = GetEntityByAddress(address);
        return ent == null
            ? null
            : ent is T e
            ? e
            : throw new InvalidOperationException($"Invalid entity type. Requested: {typeof(T).Name}, Actual: {ent!.GetType().Name}.");
    }

    public CEntityInstance? GetEntityByAddress( nint address )
    {
        return EntityManager.GetEntityByAddress(address);
    }

    ~EntitySystemService()
    {
        Dispose();
    }
}
