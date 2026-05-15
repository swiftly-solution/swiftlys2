using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Shared.Schemas;

public interface ISchemaField : INativeHandle {


}

public record EntityFieldInfo
{
    public required string Name { get; init; }
    public required string Type { get; init; }
    public required int Offset { get; init; }
    public required string Value { get; init; }
    public List<EntityFieldInfo> Children { get; init; } = [];
}