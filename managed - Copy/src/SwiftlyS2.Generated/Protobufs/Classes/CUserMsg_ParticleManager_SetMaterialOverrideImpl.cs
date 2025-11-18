
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMsg_ParticleManager_SetMaterialOverrideImpl : TypedProtobuf<CUserMsg_ParticleManager_SetMaterialOverride>, CUserMsg_ParticleManager_SetMaterialOverride
{
  public CUserMsg_ParticleManager_SetMaterialOverrideImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public string MaterialName
  { get => Accessor.GetString("material_name"); set => Accessor.SetString("material_name", value); }


  public bool IncludeChildren
  { get => Accessor.GetBool("include_children"); set => Accessor.SetBool("include_children", value); }

}
