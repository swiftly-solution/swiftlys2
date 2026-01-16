using SwiftlyS2.Core.Extensions;
using SwiftlyS2.Shared.Schemas;

namespace SwiftlyS2.Core.Schemas;

internal class SchemaStringFixedArray( nint handle, ulong hash, int elementCount, int elementSize, int elementAlignment ) : SchemaFixedArray<nint>(handle, hash, elementCount, elementSize, elementAlignment), ISchemaStringFixedArray
{
    public new string this[int index] {
        get {
            return Schema.GetString(base[index].Read<nint>());
        }
        set {
            Schema.SetString(base[index], 0, value);
        }
    }
}

