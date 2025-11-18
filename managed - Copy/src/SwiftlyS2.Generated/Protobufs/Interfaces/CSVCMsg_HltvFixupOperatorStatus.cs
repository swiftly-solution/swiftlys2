
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CSVCMsg_HltvFixupOperatorStatus : ITypedProtobuf<CSVCMsg_HltvFixupOperatorStatus>, INetMessage<CSVCMsg_HltvFixupOperatorStatus>, IDisposable
{
  static int INetMessage<CSVCMsg_HltvFixupOperatorStatus>.MessageId => 75;
  
  static string INetMessage<CSVCMsg_HltvFixupOperatorStatus>.MessageName => "CSVCMsg_HltvFixupOperatorStatus";

  static CSVCMsg_HltvFixupOperatorStatus ITypedProtobuf<CSVCMsg_HltvFixupOperatorStatus>.Wrap(nint handle, bool isManuallyAllocated) => new CSVCMsg_HltvFixupOperatorStatusImpl(handle, isManuallyAllocated);


  public uint Mode { get; set; }


  public string OverrideOperatorName { get; set; }

}
