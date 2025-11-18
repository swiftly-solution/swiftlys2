
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMsg_ParticleManager_UpdateParticleEnt : ITypedProtobuf<CUserMsg_ParticleManager_UpdateParticleEnt>
{
  static CUserMsg_ParticleManager_UpdateParticleEnt ITypedProtobuf<CUserMsg_ParticleManager_UpdateParticleEnt>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMsg_ParticleManager_UpdateParticleEntImpl(handle, isManuallyAllocated);


  public int ControlPoint { get; set; }


  public uint EntityHandle { get; set; }


  public int AttachType { get; set; }


  public int Attachment { get; set; }


  public Vector FallbackPosition { get; set; }


  public bool IncludeWearables { get; set; }


  public Vector OffsetPosition { get; set; }


  public QAngle OffsetAngles { get; set; }

}
