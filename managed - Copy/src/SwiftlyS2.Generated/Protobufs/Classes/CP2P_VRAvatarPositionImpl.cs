
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CP2P_VRAvatarPositionImpl : TypedProtobuf<CP2P_VRAvatarPosition>, CP2P_VRAvatarPosition
{
  public CP2P_VRAvatarPositionImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public IProtobufRepeatedFieldSubMessageType<CP2P_VRAvatarPosition_COrientation> BodyParts
  { get => new ProtobufRepeatedFieldSubMessageType<CP2P_VRAvatarPosition_COrientation>(Accessor, "body_parts"); }


  public int HatId
  { get => Accessor.GetInt32("hat_id"); set => Accessor.SetInt32("hat_id", value); }


  public int SceneId
  { get => Accessor.GetInt32("scene_id"); set => Accessor.SetInt32("scene_id", value); }


  public int WorldScale
  { get => Accessor.GetInt32("world_scale"); set => Accessor.SetInt32("world_scale", value); }

}
