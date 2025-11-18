
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_ClientPerfReport_Entry : ITypedProtobuf<CMsgGCCStrike15_v2_ClientPerfReport_Entry>
{
  static CMsgGCCStrike15_v2_ClientPerfReport_Entry ITypedProtobuf<CMsgGCCStrike15_v2_ClientPerfReport_Entry>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_ClientPerfReport_EntryImpl(handle, isManuallyAllocated);


  public uint Perfcounter { get; set; }


  public uint Length { get; set; }


  public byte[] Reference { get; set; }


  public byte[] Actual { get; set; }


  public uint Sourceid { get; set; }


  public uint Status { get; set; }

}
