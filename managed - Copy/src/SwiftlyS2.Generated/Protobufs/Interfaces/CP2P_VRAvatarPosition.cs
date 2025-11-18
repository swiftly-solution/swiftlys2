
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CP2P_VRAvatarPosition : ITypedProtobuf<CP2P_VRAvatarPosition>
{
  static CP2P_VRAvatarPosition ITypedProtobuf<CP2P_VRAvatarPosition>.Wrap(nint handle, bool isManuallyAllocated) => new CP2P_VRAvatarPositionImpl(handle, isManuallyAllocated);


  public IProtobufRepeatedFieldSubMessageType<CP2P_VRAvatarPosition_COrientation> BodyParts { get; }


  public int HatId { get; set; }


  public int SceneId { get; set; }


  public int WorldScale { get; set; }

}
