
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMsg_ParticleManager_SetParticleClusterGrowthImpl : TypedProtobuf<CUserMsg_ParticleManager_SetParticleClusterGrowth>, CUserMsg_ParticleManager_SetParticleClusterGrowth
{
  public CUserMsg_ParticleManager_SetParticleClusterGrowthImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public float Duration
  { get => Accessor.GetFloat("duration"); set => Accessor.SetFloat("duration", value); }


  public Vector Origin
  { get => Accessor.GetVector("origin"); set => Accessor.SetVector("origin", value); }

}
