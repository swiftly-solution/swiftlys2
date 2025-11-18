
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMsg_ParticleManager_ParticleCanFreezeImpl : TypedProtobuf<CUserMsg_ParticleManager_ParticleCanFreeze>, CUserMsg_ParticleManager_ParticleCanFreeze
{
  public CUserMsg_ParticleManager_ParticleCanFreezeImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public bool CanFreeze
  { get => Accessor.GetBool("can_freeze"); set => Accessor.SetBool("can_freeze", value); }

}
