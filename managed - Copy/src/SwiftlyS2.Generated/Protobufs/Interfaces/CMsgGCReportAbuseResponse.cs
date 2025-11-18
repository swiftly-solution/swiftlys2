
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCReportAbuseResponse : ITypedProtobuf<CMsgGCReportAbuseResponse>
{
  static CMsgGCReportAbuseResponse ITypedProtobuf<CMsgGCReportAbuseResponse>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCReportAbuseResponseImpl(handle, isManuallyAllocated);


  public ulong TargetSteamId { get; set; }


  public uint Result { get; set; }


  public string ErrorMessage { get; set; }

}
