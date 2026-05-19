using System.Runtime.InteropServices;
using SwiftlyS2.Core.EntitySystem;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.Schemas;

namespace SwiftlyS2.Shared.Natives;

public interface ICHandle
{
    public uint Raw { get; }
}

[StructLayout(LayoutKind.Sequential, Size = 4)]
public struct CHandle<T>( uint raw ) : ICHandle where T : class, ISchemaClass<T>
{
    public uint Raw { get; set; } = raw;
    public readonly uint EntityIndex => Raw & 0x7FFF;
    public readonly uint SerialNumber => (Raw >> 15) & 0x1FFFF;
    public readonly bool IsValid {
        get {
            if (Raw == 0xFFFFFFFF) return false;
            var ent = EntityManager.GetEntityByIndex(EntityIndex);
            if (ent == null || ent.Entity == null) return false;
            return ent.Entity.EntityHandle.Raw == Raw;
        }
    }

    public T? Value {
        readonly get {
            unsafe
            {
                if (!IsValid) return null;

                var ent = EntityManager.GetEntityByIndex(EntityIndex);
                if (ent == null) return null;

                return ent is T entity ? entity : T.From(ent.Address);
            }
        }
        set {
            if (value == null) Raw = 0xFFFFFFFF;
            else if (value is not CEntityInstance ent) throw new InvalidOperationException($"Value must be of type {typeof(T).Name} which implements CEntityInstance.");
            else Raw = ent.Entity == null ? 0xFFFFFFFF : ent.Entity.EntityHandle.Raw;
        }
    }

    public static CHandle<T> Invalid => new(0xFFFFFFFF);

    public static CHandle<T> FromPackedInt( int packed_handle )
    {
        if (packed_handle == 0xFFFFFF) return new();

        var index = (uint)(packed_handle & 0x3FFF);
        var serial = (packed_handle >> 14) & 0x3FF;

        var ent = EntityManager.GetEntityByIndex(index);
        if (ent == null || ent.Entity == null) return new();

        var entHandle = ent.Entity.EntityHandle;
        if ((entHandle.SerialNumber & 0x3FF) != serial) return new();

        return new CHandle<T> { Raw = entHandle.Raw };
    }

    public readonly int ToPackedInt()
    {
        return !IsValid ? 0xFFFFFF : (int)EntityIndex | ((int)(SerialNumber & 0x3FF) << 14);
    }

    public static bool operator ==( CHandle<T> left, CHandle<T> right ) => left.Equals(right);
    public static bool operator !=( CHandle<T> left, CHandle<T> right ) => !left.Equals(right);
    public static implicit operator T( CHandle<T> handle ) => handle.Value ?? throw new InvalidOperationException("Entity handle is invalid or entity does not exist.");

    public readonly bool Equals( CHandle<T>? other ) => other.HasValue && other.Value.Raw == this.Raw;
    public override readonly bool Equals( object? obj ) => obj is CHandle<T> v && Equals(v);
    public override readonly int GetHashCode() => this.Raw.GetHashCode();
    public override readonly string ToString() => this.IsValid ? $"CHandle<{typeof(T).Name}>[{this.EntityIndex}] SerialNumber:{this.SerialNumber}" : $"CHandle<{typeof(T).Name}> invalid";
}
