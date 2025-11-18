
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CDataGCCStrike15_v2_TournamentSection : ITypedProtobuf<CDataGCCStrike15_v2_TournamentSection>
{
  static CDataGCCStrike15_v2_TournamentSection ITypedProtobuf<CDataGCCStrike15_v2_TournamentSection>.Wrap(nint handle, bool isManuallyAllocated) => new CDataGCCStrike15_v2_TournamentSectionImpl(handle, isManuallyAllocated);


  public uint Sectionid { get; set; }


  public string Name { get; set; }


  public string Desc { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CDataGCCStrike15_v2_TournamentGroup> Groups { get; }

}
