
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CDataGCCStrike15_v2_TournamentInfoImpl : TypedProtobuf<CDataGCCStrike15_v2_TournamentInfo>, CDataGCCStrike15_v2_TournamentInfo
{
  public CDataGCCStrike15_v2_TournamentInfoImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public IProtobufRepeatedFieldSubMessageType<CDataGCCStrike15_v2_TournamentSection> Sections
  { get => new ProtobufRepeatedFieldSubMessageType<CDataGCCStrike15_v2_TournamentSection>(Accessor, "sections"); }


  public TournamentEvent TournamentEvent
  { get => new TournamentEventImpl(NativeNetMessages.GetNestedMessage(Address, "tournament_event"), false); }


  public IProtobufRepeatedFieldSubMessageType<TournamentTeam> TournamentTeams
  { get => new ProtobufRepeatedFieldSubMessageType<TournamentTeam>(Accessor, "tournament_teams"); }

}
