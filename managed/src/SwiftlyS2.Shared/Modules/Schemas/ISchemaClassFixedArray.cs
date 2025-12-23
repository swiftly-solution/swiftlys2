namespace SwiftlyS2.Shared.Schemas;

public interface ISchemaClassFixedArray<T> : ISchemaField where T : class, ISchemaClass<T> {

  public int ElementAlignment { get; }

  public int ElementCount { get; }

  public int ElementSize { get; }

  public T this[int index] { get; }

}