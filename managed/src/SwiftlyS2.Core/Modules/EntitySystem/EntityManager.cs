using SwiftlyS2.Core.SchemaDefinitions;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.EntitySystem;

internal static class EntityManager
{
    private static readonly DenseSlotTable<CEntityInstance> _Entities = new(1 << 15);
    private static readonly CEntityInstanceImpl _Dummy = new(0);
    private static readonly Lock _lock = new();

    public static CEntityInstance OnEntityCreated( nint entityPtr )
    {
        var ent = GetEntityByAddress(entityPtr);
        if (ent != null) return ent;

        _Dummy.DangerousSetHandle(entityPtr);
        var index = _Dummy.Index;
        var designerName = _Dummy.DesignerName;
        var entity = ClassConvertor.ConvertEntityByDesignerName(entityPtr, designerName);
        lock (_lock)
        {
            try
            {
                _Entities.Set(index, entityPtr, entity);
                return entity;
            }
            catch
            {
                return entity;
            }
        }
    }

    public static CEntityInstance? GetEntityByIndex( uint index )
    {
        lock (_lock)
        {
            try
            {
                return _Entities.Get(index);
            }
            catch
            {
                return null;
            }
        }
    }

    public static CEntityInstance? GetEntityByAddress( nint address )
    {
        if (address == 0) return null;

        lock (_lock)
        {
            return _Entities.GetByAddress(address);
        }
    }

    public static void OnEntityDeleted( nint entityPtr )
    {
        if (entityPtr == 0) return;

        lock (_lock)
        {
            _ = _Entities.Remove(entityPtr);
        }
    }

    public static IEnumerable<CEntityInstance> GetAllEntities()
    {
        lock (_lock)
        {
            return _Entities.ToList();
        }
    }

    public static bool IsAddressValid( nint address )
    {
        if (address == 0) return false;

        lock (_lock)
        {
            return _Entities.ContainsAddress(address);
        }
    }

}
