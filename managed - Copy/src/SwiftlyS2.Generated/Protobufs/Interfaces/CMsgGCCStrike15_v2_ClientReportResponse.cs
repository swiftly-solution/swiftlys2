
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_ClientReportResponse : ITypedProtobuf<CMsgGCCStrike15_v2_ClientReportResponse>
{
  static CMsgGCCStrike15_v2_ClientReportResponse ITypedProtobuf<CMsgGCCStrike15_v2_ClientReportResponse>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_ClientReportResponseImpl(handle, isManuallyAllocated);


  public ulong ConfirmationId { get; set; }


  public uint AccountId { get; set; }


  public uint ServerIp { get; set; }


  public uint ResponseType { get; set; }


  public uint ResponseResult { get; set; }


  public uint Tokens { get; set; }

}
