
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMsg_ParticleManager_UpdateEntityPositionImpl : TypedProtobuf<CUserMsg_ParticleManager_UpdateEntityPosition>, CUserMsg_ParticleManager_UpdateEntityPosition
{
  public CUserMsg_ParticleManager_UpdateEntityPositionImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint EntityHandle
  { get => Accessor.GetUInt32("entity_handle"); set => Accessor.SetUInt32("entity_handle", value); }


  public Vector Position
  { get => Accessor.GetVector("position"); set => Accessor.SetVector("position", value); }

}
