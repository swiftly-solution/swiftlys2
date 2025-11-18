
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_ClientPartyJoinRelayImpl : TypedProtobuf<CMsgGCCStrike15_v2_ClientPartyJoinRelay>, CMsgGCCStrike15_v2_ClientPartyJoinRelay
{
  public CMsgGCCStrike15_v2_ClientPartyJoinRelayImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Accountid
  { get => Accessor.GetUInt32("accountid"); set => Accessor.SetUInt32("accountid", value); }


  public ulong Lobbyid
  { get => Accessor.GetUInt64("lobbyid"); set => Accessor.SetUInt64("lobbyid", value); }

}
