
using System.Collections.Concurrent;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.Schemas;
using SwiftlyS2.Shared.EntitySystem;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Core.Events;
using SwiftlyS2.Shared.Misc;

namespace SwiftlyS2.Core.EntitySystem;

internal class EntitySystemService : IEntitySystemService, IDisposable
{
    public static CCSGameRulesProxy? cachedGameRulesProxy;
    private readonly IGameHooks gameHooks;

    private readonly ConcurrentDictionary<Guid, OnFireOutputEntityPreDelegate> outputHooks = new();
    private readonly ConcurrentDictionary<Guid, OnAcceptInputEntityPreDelegate> inputHooks = new();
    private volatile bool disposed;

    public EntitySystemService( IGameHooks gameHooks )
    {
        this.gameHooks = gameHooks;
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
        return CreateEntity<T>(-1);
    }

    public T CreateEntity<T>( int forcedIndex ) where T : class, ISchemaClass<T>
    {
        ThrowIfEntitySystemInvalid();
        return string.IsNullOrWhiteSpace(T.ClassName)
            ? throw new ArgumentException($"Can't create entity with class {typeof(T).Name}, which doesn't have a designer name.")
            : CreateEntityByDesignerName<T>(T.ClassName, forcedIndex);
    }

    public T CreateEntityByDesignerName<T>( string designerName ) where T : class, ISchemaClass<T>
    {
        return CreateEntityByDesignerName<T>(designerName, -1);
    }

    public T CreateEntityByDesignerName<T>( string designerName, int forcedIndex ) where T : class, ISchemaClass<T>
    {
        return (CreateEntityByDesignerName(designerName, forcedIndex) as T)!;
    }

    public CEntityInstance CreateEntityByDesignerName( string designerName, int forcedIndex )
    {
        ThrowIfEntitySystemInvalid();
        var handle = GameFunctions.CreateEntityByName(designerName, forcedIndex);
        if (handle == nint.Zero) throw new ArgumentException($"Failed to create entity by designer name: {designerName}, probably invalid designer name or the forced index is already used.");

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
        if (cachedGameRulesProxy != null && cachedGameRulesProxy.IsValidEntity)
        {
            return cachedGameRulesProxy.GameRules;
        }
        cachedGameRulesProxy = null;

        if (GetAllEntitiesByClass<CCSGameRulesProxy>().FirstOrDefault() is CCSGameRulesProxy proxy)
        {
            cachedGameRulesProxy = proxy;
            return proxy.GameRules;
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
            .Where(entity => entity.DesignerName == designerName)
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
        void handler( ref FireOutputEntityPreContext ctx )
        {
            if (outputName == "*" || outputName.Equals(ctx.Params.OutputName, StringComparison.OrdinalIgnoreCase))
            {
                if (ctx.Params.DesignerName.Equals(className, StringComparison.OrdinalIgnoreCase))
                {
                    unsafe
                    {
                        var @e = new OnEntityFireOutputHookEvent {
                            _entityIO = ctx.Params._entityIO,
                            _variant = ctx.Params._variant,
                            DesignerName = ctx.Params.DesignerName,
                            OutputName = ctx.Params.OutputName,
                            Activator = ctx.Params.Activator,
                            Caller = ctx.Params.Caller,
                            Delay = ctx.Params.Delay,
                            Result = HookResult.Continue
                        };
                        callback(@e);
                        ctx.SetHookResult(@e.Result);
                    }
                }
            }
        }

        var guid = Guid.NewGuid();
        _ = outputHooks.TryAdd(guid, handler);
        gameHooks.Entities.FireOutput.Pre += handler;

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
        void handler( ref FireOutputEntityPreContext ctx )
        {
            if (outputName == "*" || outputName.Equals(ctx.Params.OutputName, StringComparison.OrdinalIgnoreCase))
            {
                if (designerName == "*" || ctx.Params.DesignerName.Equals(designerName, StringComparison.OrdinalIgnoreCase))
                {
                    unsafe
                    {
                        var @e = new OnEntityFireOutputHookEvent {
                            _entityIO = ctx.Params._entityIO,
                            _variant = ctx.Params._variant,
                            DesignerName = ctx.Params.DesignerName,
                            OutputName = ctx.Params.OutputName,
                            Activator = ctx.Params.Activator,
                            Caller = ctx.Params.Caller,
                            Delay = ctx.Params.Delay,
                            Result = HookResult.Continue
                        };
                        callback(@e);
                        ctx.SetHookResult(@e.Result);
                    }
                }
            }
        }

        var guid = Guid.NewGuid();
        _ = outputHooks.TryAdd(guid, handler);
        gameHooks.Entities.FireOutput.Pre += handler;

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
        void handler( ref AcceptInputEntityPreContext ctx )
        {
            if (inputName == "*" || inputName.Equals(ctx.Params.InputName, StringComparison.OrdinalIgnoreCase))
            {
                if (ctx.Params.DesignerName.Equals(className, StringComparison.OrdinalIgnoreCase))
                {
                    unsafe
                    {
                        var @e = new OnEntityIdentityAcceptInputHookEvent {
                            Identity = ctx.Params.Identity,
                            EntityInstance = ctx.Params.EntityInstance,
                            DesignerName = ctx.Params.DesignerName,
                            InputName = ctx.Params.InputName,
                            Activator = ctx.Params.Activator,
                            Caller = ctx.Params.Caller,
                            _variant = ctx.Params._variant,
                            OutputId = ctx.Params.OutputId,
                            Result = HookResult.Continue
                        };
                        callback(@e);
                        ctx.SetHookResult(@e.Result);
                    }
                }
            }
        }

        var guid = Guid.NewGuid();
        _ = inputHooks.TryAdd(guid, handler);
        gameHooks.Entities.AcceptInput.Pre += handler;

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
        void handler( ref AcceptInputEntityPreContext ctx )
        {
            if (inputName == "*" || inputName.Equals(ctx.Params.InputName, StringComparison.OrdinalIgnoreCase))
            {
                if (designerName == "*" || ctx.Params.DesignerName.Equals(designerName, StringComparison.OrdinalIgnoreCase))
                {
                    unsafe
                    {
                        var @e = new OnEntityIdentityAcceptInputHookEvent {
                            Identity = ctx.Params.Identity,
                            EntityInstance = ctx.Params.EntityInstance,
                            DesignerName = ctx.Params.DesignerName,
                            InputName = ctx.Params.InputName,
                            Activator = ctx.Params.Activator,
                            Caller = ctx.Params.Caller,
                            _variant = ctx.Params._variant,
                            OutputId = ctx.Params.OutputId,
                            Result = HookResult.Continue
                        };
                        callback(@e);
                        ctx.SetHookResult(@e.Result);
                    }
                }
            }
        }

        var guid = Guid.NewGuid();
        _ = inputHooks.TryAdd(guid, handler);
        gameHooks.Entities.AcceptInput.Pre += handler;

        return guid;
    }

    public bool UnhookEntityOutput( Guid guid )
    {
        if (outputHooks.TryRemove(guid, out var handler))
        {
            gameHooks.Entities.FireOutput.Pre -= handler;
            return true;
        }
        return false;
    }

    public bool UnhookEntityInput( Guid guid )
    {
        if (inputHooks.TryRemove(guid, out var handler))
        {
            gameHooks.Entities.AcceptInput.Pre -= handler;
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
            gameHooks.Entities.FireOutput.Pre -= handler;
        }
        outputHooks.Clear();

        foreach (var handler in inputHooks.Values)
        {
            gameHooks.Entities.AcceptInput.Pre -= handler;
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
