
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CSOEconItemLeagueViewPass : ITypedProtobuf<CSOEconItemLeagueViewPass>
{
  static CSOEconItemLeagueViewPass ITypedProtobuf<CSOEconItemLeagueViewPass>.Wrap(nint handle, bool isManuallyAllocated) => new CSOEconItemLeagueViewPassImpl(handle, isManuallyAllocated);


  public uint AccountId { get; set; }


  public uint LeagueId { get; set; }


  public uint Admin { get; set; }


  public uint Itemindex { get; set; }

}
