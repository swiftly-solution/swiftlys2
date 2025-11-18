
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCLCMsg_Diagnostic : ITypedProtobuf<CCLCMsg_Diagnostic>, INetMessage<CCLCMsg_Diagnostic>, IDisposable
{
  static int INetMessage<CCLCMsg_Diagnostic>.MessageId => 37;
  
  static string INetMessage<CCLCMsg_Diagnostic>.MessageName => "CCLCMsg_Diagnostic";

  static CCLCMsg_Diagnostic ITypedProtobuf<CCLCMsg_Diagnostic>.Wrap(nint handle, bool isManuallyAllocated) => new CCLCMsg_DiagnosticImpl(handle, isManuallyAllocated);


  public CMsgSource2SystemSpecs SystemSpecs { get; }


  public CMsgSource2VProfLiteReport VprofReport { get; }


  public CMsgSource2NetworkFlowQuality DownstreamFlow { get; }


  public CMsgSource2NetworkFlowQuality UpstreamFlow { get; }


  public IProtobufRepeatedFieldSubMessageType<CMsgSource2PerfIntervalSample> PerfSamples { get; }

}
