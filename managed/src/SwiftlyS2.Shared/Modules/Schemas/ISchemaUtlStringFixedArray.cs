namespace SwiftlyS2.Shared.Schemas;

public interface ISchemaUtlStringFixedArray : ISchemaFixedArray<nint>
{
    public new string this[int index] { get; set; }
}