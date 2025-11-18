
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface TournamentMatchSetup : ITypedProtobuf<TournamentMatchSetup>
{
  static TournamentMatchSetup ITypedProtobuf<TournamentMatchSetup>.Wrap(nint handle, bool isManuallyAllocated) => new TournamentMatchSetupImpl(handle, isManuallyAllocated);


  public int EventId { get; set; }


  public int TeamIdCt { get; set; }


  public int TeamIdT { get; set; }


  public int EventStageId { get; set; }

}
