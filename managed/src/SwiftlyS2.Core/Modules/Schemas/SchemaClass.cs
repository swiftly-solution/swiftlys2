namespace SwiftlyS2.Core.Schemas;

internal abstract class SchemaClass : SchemaField
{
    public SchemaClass( nint handle ) : base(handle, 0)
    {
    }

    public SchemaClass( nint handle, ulong hash ) : base(handle, hash)
    {
    }

}