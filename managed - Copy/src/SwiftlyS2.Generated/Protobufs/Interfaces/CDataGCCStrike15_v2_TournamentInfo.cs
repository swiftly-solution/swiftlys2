
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CDataGCCStrike15_v2_TournamentInfo : ITypedProtobuf<CDataGCCStrike15_v2_TournamentInfo>
{
  static CDataGCCStrike15_v2_TournamentInfo ITypedProtobuf<CDataGCCStrike15_v2_TournamentInfo>.Wrap(nint handle, bool isManuallyAllocated) => new CDataGCCStrike15_v2_TournamentInfoImpl(handle, isManuallyAllocated);


  public IProtobufRepeatedFieldSubMessageType<CDataGCCStrike15_v2_TournamentSection> Sections { get; }


  public TournamentEvent TournamentEvent { get; }


  public IProtobufRepeatedFieldSubMessageType<TournamentTeam> TournamentTeams { get; }

}
