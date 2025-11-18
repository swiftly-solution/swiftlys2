
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMsg_ParticleManager_SetTextureAttribute : ITypedProtobuf<CUserMsg_ParticleManager_SetTextureAttribute>
{
  static CUserMsg_ParticleManager_SetTextureAttribute ITypedProtobuf<CUserMsg_ParticleManager_SetTextureAttribute>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMsg_ParticleManager_SetTextureAttributeImpl(handle, isManuallyAllocated);


  public string AttributeName { get; set; }


  public string TextureName { get; set; }

}
