
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMsg_ParticleManager_SetParticleNamedValueContext_EHandleContextImpl : TypedProtobuf<CUserMsg_ParticleManager_SetParticleNamedValueContext_EHandleContext>, CUserMsg_ParticleManager_SetParticleNamedValueContext_EHandleContext
{
  public CUserMsg_ParticleManager_SetParticleNamedValueContext_EHandleContextImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint ValueNameHash
  { get => Accessor.GetUInt32("value_name_hash"); set => Accessor.SetUInt32("value_name_hash", value); }


  public uint EntIndex
  { get => Accessor.GetUInt32("ent_index"); set => Accessor.SetUInt32("ent_index", value); }

}
