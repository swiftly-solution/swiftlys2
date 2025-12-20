using Microsoft.Extensions.Logging;
using SwiftlyS2.Core.Natives;
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
    private readonly List<EntityOutputHookCallback> callbacks = [];
    private readonly Lock callbacksLock = new();
    private readonly ILoggerFactory loggerFactory;
    private readonly IContextedProfilerService profiler;

    public EntitySystemService( ILoggerFactory loggerFactory, IContextedProfilerService profiler )
    {
        this.loggerFactory = loggerFactory;
        this.profiler = profiler;
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
        return new CHandle<T> {
            Raw = NativeEntitySystem.GetEntityHandleFromEntity(entity.Address),
        };
    }

    public CCSGameRules? GetGameRules()
    {
        var handle = NativeEntitySystem.GetGameRules();
        return handle.IsValidPtr() ? new CCSGameRulesImpl(handle) : null;
    }

    public IEnumerable<CEntityInstance> GetAllEntities()
    {
        CEntityIdentity? pFirst = new CEntityIdentityImpl(NativeEntitySystem.GetFirstActiveEntity());

        for (; pFirst != null && pFirst.IsValid; pFirst = pFirst.Next)
        {
            yield return new CEntityInstanceImpl(pFirst.Address.Read<nint>());
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

    public Guid HookEntityOutput<T>( string outputName, IEntitySystemService.EntityOutputHandler callback ) where T : class, ISchemaClass<T>
    {
        var hook = new EntityOutputHookCallback(T.ClassName ?? throw new ArgumentException($"Can't hook entity output with class {typeof(T).Name}, which doesn't have a designer name"), outputName, callback, loggerFactory, profiler);
        lock (callbacksLock)
        {
            callbacks.Add(hook);
        }
        return hook.Guid;
    }

    public void UnhookEntityOutput( Guid guid )
    {
        lock (callbacksLock)
        {
            _ = callbacks.RemoveAll(callback =>
            {
                if (callback.Guid == guid)
                {
                    callback.Dispose();
                    return true;
                }
                return false;
            });
        }
    }

    public void Dispose()
    {
        lock (callbacksLock)
        {
            foreach (var callback in callbacks)
            {
                callback.Dispose();
            }
            callbacks.Clear();
        }
    }
}