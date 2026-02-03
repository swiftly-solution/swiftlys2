using System;
using System.Diagnostics;
using System.Reflection;
using SwiftlyS2.Core.SchemaDefinitions;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.EntitySystem;

internal static class EntityManager
{

    private static CEntityInstance?[] _Entities = new CEntityInstance?[1 << 15];
    private static List<uint> _ActiveEntityIndices = new();
    private static Dictionary<nint, uint> _PtrToIndex = new();
    private static CEntityInstanceImpl _Dummy = new(0);
    private static readonly ReaderWriterLockSlim _rw = new(LockRecursionPolicy.NoRecursion);

    public static CEntityInstance OnEntityCreated( nint entityPtr )
    {
        Stopwatch sw = Stopwatch.StartNew();
        _Dummy.DangerousSetHandle(entityPtr);
        var index = _Dummy.Index;
        var entity = ClassConvertor.ConvertEntityByDesignerName(entityPtr, _Dummy.DesignerName);
        _rw.EnterWriteLock();
        try
        {
            _Entities[index] = entity;
            _ActiveEntityIndices.Add(index);
            _PtrToIndex.Add(entityPtr, index);
            Console.WriteLine($"[EntityManager] Created entity: Index={index}, DesignerName={_Dummy.DesignerName}, time: {sw.ElapsedTicks * 1000000F / Stopwatch.Frequency} μs");
            return entity;
        }
        finally
        {
            _rw.ExitWriteLock();
        }
    }

    public static CEntityInstance? GetEntityByIndex( uint index )
    {
        _rw.EnterReadLock();
        try
        {
            return _Entities[index];
        }
        finally
        {
            _rw.ExitReadLock();
        }
    }

    public static CEntityInstance? GetEntityByAddress(nint address)
    {
        _rw.EnterReadLock();
        try
        {
            if (!_PtrToIndex.ContainsKey(address))
            {
                return null;
            }
            return _Entities[_PtrToIndex[address]]!;
        } finally
        {
            _rw.ExitReadLock();
        }
    }

    public static void OnEntityDeleted(nint entityPtr)
    {
        Stopwatch sw = Stopwatch.StartNew();
        _rw.EnterWriteLock();
        try
        {
            if (!_PtrToIndex.ContainsKey(entityPtr))
            {
                return;
            }
            var index = _PtrToIndex[entityPtr];
            _Entities[index] = null;
            _ = _ActiveEntityIndices.Remove(index);
            _ = _PtrToIndex.Remove(entityPtr);
            Console.WriteLine($"[EntityManager] Deleted entity: Index={index}, time: {sw.ElapsedTicks * 1000000F / Stopwatch.Frequency} μs");
        }
        finally
        {
            _rw.ExitWriteLock();
        }
    }

    public static IEnumerable<CEntityInstance> GetAllEntities()
    {

        _rw.EnterReadLock();
        try
        {
            return _ActiveEntityIndices.Select(index => _Entities[index]!);
        } finally
        {
            _rw.ExitReadLock();
        }
    }

    public static bool IsAddressValid(nint address)
    {
        _rw.EnterReadLock();
        try
        {
            return _PtrToIndex.ContainsKey(address);
        } finally
        {
            _rw.ExitReadLock();
        }
    }

}
