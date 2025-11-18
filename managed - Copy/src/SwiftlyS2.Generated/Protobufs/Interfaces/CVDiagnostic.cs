
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CVDiagnostic : ITypedProtobuf<CVDiagnostic>
{
  static CVDiagnostic ITypedProtobuf<CVDiagnostic>.Wrap(nint handle, bool isManuallyAllocated) => new CVDiagnosticImpl(handle, isManuallyAllocated);


  public uint Id { get; set; }


  public uint Extended { get; set; }


  public ulong Value { get; set; }


  public string StringValue { get; set; }

}
