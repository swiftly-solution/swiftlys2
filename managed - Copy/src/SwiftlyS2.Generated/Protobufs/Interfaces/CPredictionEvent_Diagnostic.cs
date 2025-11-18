
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPredictionEvent_Diagnostic : ITypedProtobuf<CPredictionEvent_Diagnostic>
{
  static CPredictionEvent_Diagnostic ITypedProtobuf<CPredictionEvent_Diagnostic>.Wrap(nint handle, bool isManuallyAllocated) => new CPredictionEvent_DiagnosticImpl(handle, isManuallyAllocated);


  public uint Id { get; set; }


  public uint RequestedSync { get; set; }


  public uint RequestedPlayerIndex { get; set; }


  public IProtobufRepeatedFieldValueType<uint> ExecutionSync { get; }

}
