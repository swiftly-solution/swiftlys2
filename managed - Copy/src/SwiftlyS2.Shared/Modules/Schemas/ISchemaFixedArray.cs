namespace SwiftlyS2.Shared.Schemas;

public interface ISchemaFixedArray<T> : ISchemaField where T : unmanaged {


  public int ElementAlignment { get; }

  public int ElementCount { get; }

  public int ElementSize { get; }

  public ref T this[int index] { get; }
}