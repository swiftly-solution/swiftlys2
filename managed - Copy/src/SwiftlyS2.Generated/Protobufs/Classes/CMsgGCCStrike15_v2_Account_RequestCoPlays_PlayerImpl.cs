
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_Account_RequestCoPlays_PlayerImpl : TypedProtobuf<CMsgGCCStrike15_v2_Account_RequestCoPlays_Player>, CMsgGCCStrike15_v2_Account_RequestCoPlays_Player
{
  public CMsgGCCStrike15_v2_Account_RequestCoPlays_PlayerImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Accountid
  { get => Accessor.GetUInt32("accountid"); set => Accessor.SetUInt32("accountid", value); }


  public uint Rtcoplay
  { get => Accessor.GetUInt32("rtcoplay"); set => Accessor.SetUInt32("rtcoplay", value); }


  public bool Online
  { get => Accessor.GetBool("online"); set => Accessor.SetBool("online", value); }

}
