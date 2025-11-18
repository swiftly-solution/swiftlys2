using System.Runtime.InteropServices;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.Schemas;

namespace SwiftlyS2.Shared.Natives;

[StructLayout(LayoutKind.Sequential, Size = 4)]
public struct CHandle<T> where T : class, ISchemaClass<T>
{
  private uint _index;

  public uint Raw
  {
    get => _index;
    set => _index = value;
  }

  public CHandle(uint raw)
  {
    _index = raw;
  }

  public T? Value
  {
    get
    {
      unsafe
      {
        if (!IsValid)
        {
          return null;
        }
        return (T?)T.From(NativeEntitySystem.EntityHandleGet(_index));
      }
    }
    set
    {
      _index = value is null ? 0xFFFFFFFF : NativeEntitySystem.GetEntityHandleFromEntity(value.Address);
    }
  }

  public readonly uint EntityIndex => _index & 0x7FFF;

  public readonly uint SerialNumber => (_index >> 15) & 0x1FFFF;

  public readonly bool IsValid => NativeEntitySystem.EntityHandleIsValid(_index);


  public static implicit operator T(CHandle<T> handle) => handle.Value;
}