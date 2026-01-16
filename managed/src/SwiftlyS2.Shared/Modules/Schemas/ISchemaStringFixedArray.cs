namespace SwiftlyS2.Shared.Schemas;

public interface ISchemaStringFixedArray : ISchemaFixedArray<nint>
{

    public new string this[int index] { get; set; }
}