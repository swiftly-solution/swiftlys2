using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Shared.Schemas;

public interface ISchemaField : INativeHandle {


}

/// <summary>
/// Describes a single field in an entity's schema, optionally with nested children
/// for declared classes, fixed arrays, and collection elements.
/// </summary>
public record EntityFieldInfo
{
    /// <summary>Field name, or "[N]" for collection/array elements.</summary>
    public required string Name { get; init; }
    /// <summary>Schema type name (e.g. "int32", "Vector", "CUtlVector", "CCSPlayerController").</summary>
    public required string Type { get; init; }
    /// <summary>Byte offset from the entity base pointer.</summary>
    public required int Offset { get; init; }
    /// <summary>Formatted value string. Format depends on Type: scalars are numeric,
    /// vectors are space-separated floats, strings are raw text, handles are hex.</summary>
    public required string Value { get; init; }
    /// <summary>Nesting depth (0 = top-level entity field).</summary>
    public int Depth { get; init; }
    /// <summary>Nested child fields (empty if the field has no sub-fields).</summary>
    public IReadOnlyList<EntityFieldInfo> Children { get; init; } = [];
}