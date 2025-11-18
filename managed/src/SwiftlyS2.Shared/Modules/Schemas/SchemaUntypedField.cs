using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Shared.Schemas;

public class SchemaUntypedField : INativeHandle, ISchemaClass<SchemaUntypedField>
{
    public bool IsValid => throw new NotImplementedException();
    static int ISchemaClass<SchemaUntypedField>.Size => throw new NotImplementedException();

    public SchemaUntypedField( nint handle )
    {
        Address = handle;
    }

    public static SchemaUntypedField From( nint handle )
    {
        return new SchemaUntypedField(handle);
    }

    public nint Address { get; }
}
