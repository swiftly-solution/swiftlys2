
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMsg_ParticleManager_SetParticleNamedValueContext_EHandleContext : ITypedProtobuf<CUserMsg_ParticleManager_SetParticleNamedValueContext_EHandleContext>
{
  static CUserMsg_ParticleManager_SetParticleNamedValueContext_EHandleContext ITypedProtobuf<CUserMsg_ParticleManager_SetParticleNamedValueContext_EHandleContext>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMsg_ParticleManager_SetParticleNamedValueContext_EHandleContextImpl(handle, isManuallyAllocated);


  public uint ValueNameHash { get; set; }


  public uint EntIndex { get; set; }

}
