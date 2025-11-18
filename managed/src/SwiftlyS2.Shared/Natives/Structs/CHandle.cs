using System.Runtime.InteropServices;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Schemas;

namespace SwiftlyS2.Shared.Natives;

[StructLayout(LayoutKind.Sequential, Size = 4)]
public struct CHandle<T> where T : class, ISchemaClass<T>
{
    public uint Raw { get; set; }

    public CHandle( uint raw )
    {
        Raw = raw;
    }

    public T? Value {
        get {
            unsafe
            {
                if (!IsValid)
                {
                    return null;
                }
                return (T?)T.From(NativeEntitySystem.EntityHandleGet(Raw));
            }
        }
        set {
            Raw = value is null ? 0xFFFFFFFF : NativeEntitySystem.GetEntityHandleFromEntity(value.Address);
        }
    }

    public readonly uint EntityIndex => Raw & 0x7FFF;

    public readonly uint SerialNumber => (Raw >> 15) & 0x1FFFF;

    public readonly bool IsValid => NativeEntitySystem.EntityHandleIsValid(Raw);


    public static implicit operator T( CHandle<T> handle ) => handle.Value;
}