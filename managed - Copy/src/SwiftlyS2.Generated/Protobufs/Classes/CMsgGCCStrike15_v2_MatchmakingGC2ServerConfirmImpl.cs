
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_MatchmakingGC2ServerConfirmImpl : TypedProtobuf<CMsgGCCStrike15_v2_MatchmakingGC2ServerConfirm>, CMsgGCCStrike15_v2_MatchmakingGC2ServerConfirm
{
  public CMsgGCCStrike15_v2_MatchmakingGC2ServerConfirmImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Token
  { get => Accessor.GetUInt32("token"); set => Accessor.SetUInt32("token", value); }


  public uint Stamp
  { get => Accessor.GetUInt32("stamp"); set => Accessor.SetUInt32("stamp", value); }


  public ulong Exchange
  { get => Accessor.GetUInt64("exchange"); set => Accessor.SetUInt64("exchange", value); }


  public uint Retry
  { get => Accessor.GetUInt32("retry"); set => Accessor.SetUInt32("retry", value); }

}
