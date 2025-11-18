
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMsg_ParticleManager_SetTextureAttributeImpl : TypedProtobuf<CUserMsg_ParticleManager_SetTextureAttribute>, CUserMsg_ParticleManager_SetTextureAttribute
{
  public CUserMsg_ParticleManager_SetTextureAttributeImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public string AttributeName
  { get => Accessor.GetString("attribute_name"); set => Accessor.SetString("attribute_name", value); }


  public string TextureName
  { get => Accessor.GetString("texture_name"); set => Accessor.SetString("texture_name", value); }

}
