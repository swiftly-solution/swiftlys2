using System.Reflection.Metadata;
using SwiftlyS2.Shared.Schemas;

namespace SwiftlyS2.Core.Schemas;

internal class SchemaClassFixedArray<T> : SchemaField, ISchemaClassFixedArray<T> where T : class, ISchemaClass<T>
{
    public int ElementAlignment { get; set; }

    public int ElementCount { get; set; }

    public int ElementSize { get; set; }

    public SchemaClassFixedArray( nint handle, ulong hash, int elementCount, int elementSize, int elementAlignment ) :
        base(handle, hash)
    {
        ElementAlignment = elementAlignment;
        ElementCount = elementCount;
        ElementSize = elementSize;
    }

    public T this[ int index ] => T.From(_Handle + FieldOffset + index * ElementSize);
}