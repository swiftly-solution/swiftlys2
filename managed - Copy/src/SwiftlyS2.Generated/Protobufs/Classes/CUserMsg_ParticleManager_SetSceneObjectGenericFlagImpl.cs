
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMsg_ParticleManager_SetSceneObjectGenericFlagImpl : TypedProtobuf<CUserMsg_ParticleManager_SetSceneObjectGenericFlag>, CUserMsg_ParticleManager_SetSceneObjectGenericFlag
{
  public CUserMsg_ParticleManager_SetSceneObjectGenericFlagImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public bool FlagValue
  { get => Accessor.GetBool("flag_value"); set => Accessor.SetBool("flag_value", value); }

}
