using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Shared.Schemas;

public class SchemaUntypedField : INativeHandle, ISchemaClass<SchemaUntypedField>
{

    private nint _handle;

    public bool IsValid => throw new NotImplementedException();
    static int ISchemaClass<SchemaUntypedField>.Size => throw new NotImplementedException();
    static string? ISchemaClass<SchemaUntypedField>.ClassName => throw new NotImplementedException();

    public SchemaUntypedField( nint handle )
    {
        _handle = handle;
    }

    public static SchemaUntypedField From( nint handle )
    {
        return new SchemaUntypedField(handle);
    }

    public nint Address => _handle;
}
