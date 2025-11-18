using SwiftlyS2.Core.Schemas;

namespace SwiftlyS2.Shared.Schemas;

public interface ISchemaFixedString : ISchemaFixedArray<byte>, IFormattable {

  public string Value { get; set; }

}