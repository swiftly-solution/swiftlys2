
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_Client2GCRequestPrestigeCoin : ITypedProtobuf<CMsgGCCStrike15_v2_Client2GCRequestPrestigeCoin>
{
  static CMsgGCCStrike15_v2_Client2GCRequestPrestigeCoin ITypedProtobuf<CMsgGCCStrike15_v2_Client2GCRequestPrestigeCoin>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_Client2GCRequestPrestigeCoinImpl(handle, isManuallyAllocated);


  public uint Defindex { get; set; }


  public ulong Upgradeid { get; set; }


  public uint Hours { get; set; }


  public uint Prestigetime { get; set; }

}
