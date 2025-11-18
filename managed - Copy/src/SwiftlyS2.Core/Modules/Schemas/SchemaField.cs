using SwiftlyS2.Core.Natives.NativeObjects;
using SwiftlyS2.Shared.Schemas;

namespace SwiftlyS2.Core.Schemas;

internal abstract class SchemaField : NativeHandle, ISchemaField {

  public nint FieldOffset { get; set; }

  private ulong _hash { get; set; } = 0;

  public SchemaField(nint handle, ulong hash) : base(handle) {
    FieldOffset = Schema.GetOffset(hash);
    _hash = hash;
  }


}
