using SwiftlyS2.Core.Extensions;
using SwiftlyS2.Shared.Schemas;

namespace SwiftlyS2.Core.Schemas;

internal class SchemaUtlStringFixedArray( nint handle, ulong hash, int elementCount, int elementSize, int elementAlignment ) : SchemaFixedArray<nint>(handle, hash, elementCount, elementSize, elementAlignment), ISchemaUtlStringFixedArray
{
    public new string this[int index] {
        get {
            return Schema.GetCUtlString(base[index].Read<nint>());
        }
        set {
            Schema.SetCUtlString(base[index], 0, value);
        }
    }
}