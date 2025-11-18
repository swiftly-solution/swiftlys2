
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_Client2GCRequestPrestigeCoinImpl : TypedProtobuf<CMsgGCCStrike15_v2_Client2GCRequestPrestigeCoin>, CMsgGCCStrike15_v2_Client2GCRequestPrestigeCoin
{
  public CMsgGCCStrike15_v2_Client2GCRequestPrestigeCoinImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Defindex
  { get => Accessor.GetUInt32("defindex"); set => Accessor.SetUInt32("defindex", value); }


  public ulong Upgradeid
  { get => Accessor.GetUInt64("upgradeid"); set => Accessor.SetUInt64("upgradeid", value); }


  public uint Hours
  { get => Accessor.GetUInt32("hours"); set => Accessor.SetUInt32("hours", value); }


  public uint Prestigetime
  { get => Accessor.GetUInt32("prestigetime"); set => Accessor.SetUInt32("prestigetime", value); }

}
