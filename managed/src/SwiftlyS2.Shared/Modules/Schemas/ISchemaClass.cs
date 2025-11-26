using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Shared.Schemas;

public interface ISchemaClass : INativeHandle
{
    /// <summary>
    /// Convert this handle to another type.
    /// </summary>
    /// <typeparam name="K">The type to convert to.</typeparam>
    /// <returns>The converted handle.</returns>
    public K As<K>() where K : ISchemaClass<K>
    {
        return K.From(Address);
    }
}

public interface ISchemaClass<T> : ISchemaField, ISchemaClass, INativeHandle where T : ISchemaClass<T>
{
    internal static abstract T From( nint handle );
    internal static abstract int Size { get; }
    internal static abstract string? ClassName { get; }
}