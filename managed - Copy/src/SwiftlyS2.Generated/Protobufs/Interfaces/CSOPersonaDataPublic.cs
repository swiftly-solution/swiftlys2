
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CSOPersonaDataPublic : ITypedProtobuf<CSOPersonaDataPublic>
{
  static CSOPersonaDataPublic ITypedProtobuf<CSOPersonaDataPublic>.Wrap(nint handle, bool isManuallyAllocated) => new CSOPersonaDataPublicImpl(handle, isManuallyAllocated);


  public int PlayerLevel { get; set; }


  public PlayerCommendationInfo Commendation { get; }


  public bool ElevatedState { get; set; }


  public uint XpTrailTimestampRefresh { get; set; }


  public uint XpTrailLevel { get; set; }

}
