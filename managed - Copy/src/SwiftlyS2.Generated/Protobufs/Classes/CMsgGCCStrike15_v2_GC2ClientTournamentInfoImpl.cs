
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_GC2ClientTournamentInfoImpl : TypedProtobuf<CMsgGCCStrike15_v2_GC2ClientTournamentInfo>, CMsgGCCStrike15_v2_GC2ClientTournamentInfo
{
  public CMsgGCCStrike15_v2_GC2ClientTournamentInfoImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Eventid
  { get => Accessor.GetUInt32("eventid"); set => Accessor.SetUInt32("eventid", value); }


  public uint Stageid
  { get => Accessor.GetUInt32("stageid"); set => Accessor.SetUInt32("stageid", value); }


  public uint GameType
  { get => Accessor.GetUInt32("game_type"); set => Accessor.SetUInt32("game_type", value); }


  public IProtobufRepeatedFieldValueType<uint> Teamids
  { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "teamids"); }

}
