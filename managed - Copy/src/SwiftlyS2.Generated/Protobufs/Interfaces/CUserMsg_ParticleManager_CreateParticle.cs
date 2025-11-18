
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMsg_ParticleManager_CreateParticle : ITypedProtobuf<CUserMsg_ParticleManager_CreateParticle>
{
  static CUserMsg_ParticleManager_CreateParticle ITypedProtobuf<CUserMsg_ParticleManager_CreateParticle>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMsg_ParticleManager_CreateParticleImpl(handle, isManuallyAllocated);


  public ulong ParticleNameIndex { get; set; }


  public int AttachType { get; set; }


  public uint EntityHandle { get; set; }


  public uint EntityHandleForModifiers { get; set; }


  public bool ApplyVoiceBanRules { get; set; }


  public int TeamBehavior { get; set; }


  public string ControlPointConfiguration { get; set; }


  public bool Cluster { get; set; }


  public float EndcapTime { get; set; }


  public Vector AggregationPosition { get; set; }

}
