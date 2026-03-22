using System.Runtime.InteropServices;
using SwiftlyS2.Core.EntitySystem;
using SwiftlyS2.Core.Natives;
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
    public readonly bool IsValid => NativeEntitySystem.EntityHandleIsValid(Raw);

    public T? Value {
        readonly get {
            unsafe
            {
                if (!IsValid) return null;

                var entityHandleGet = NativeEntitySystem.EntityHandleGet(Raw);
                return EntityManager.GetEntityByAddress(entityHandleGet) is T entity ? entity : T.From(entityHandleGet);
            }
        }
        set {
            Raw = value is null ? 0xFFFFFFFF : NativeEntitySystem.GetEntityHandleFromEntity(value.Address);
        }
    }

    public static CHandle<T> Invalid => new(0xFFFFFFFF);

    public static bool operator ==( CHandle<T> left, CHandle<T> right ) => left.Equals(right);
    public static bool operator !=( CHandle<T> left, CHandle<T> right ) => !left.Equals(right);
    public static implicit operator T( CHandle<T> handle ) => handle.Value ?? throw new InvalidOperationException("Entity handle is invalid or entity does not exist.");

    public readonly bool Equals( CHandle<T>? other ) => other.HasValue && other.Value.Raw == this.Raw;
    public override readonly bool Equals( object? obj ) => obj is CHandle<T> v && Equals(v);
    public override readonly int GetHashCode() => this.Raw.GetHashCode();
    public override readonly string ToString() => this.IsValid ? $"CHandle<{typeof(T).Name}>[{this.EntityIndex}] SerialNumber:{this.SerialNumber}" : $"CHandle<{typeof(T).Name}> invalid";
}