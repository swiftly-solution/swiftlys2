using SwiftlyS2.Shared.Schemas;
using SwiftlyS2.Core.Extensions;

namespace SwiftlyS2.Core.Schemas;

internal class SchemaFixedArray<T> : SchemaField, ISchemaFixedArray<T> where T : unmanaged {


  public int ElementAlignment { get; set; }

  public int ElementCount { get; set; }

  public int ElementSize { get; set; }

  public SchemaFixedArray(nint handle, ulong hash, int elementCount, int elementSize, int elementAlignment) : base(handle, hash) {
  
    ElementAlignment = elementAlignment;
    ElementCount = elementCount;
    ElementSize = elementSize;
  }

  public ref T this[int index] {
    get {
      return ref _Handle.AsRef<T>(FieldOffset + index * ElementSize);
    }
  }

}