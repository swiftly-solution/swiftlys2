
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMsg_ParticleManager_UpdateFan : ITypedProtobuf<CUserMsg_ParticleManager_UpdateFan>
{
  static CUserMsg_ParticleManager_UpdateFan ITypedProtobuf<CUserMsg_ParticleManager_UpdateFan>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMsg_ParticleManager_UpdateFanImpl(handle, isManuallyAllocated);


  public bool Active { get; set; }


  public Vector FanOrigin { get; set; }


  public Vector FanOriginOffset { get; set; }


  public Vector FanDirection { get; set; }


  public float FanRampRatio { get; set; }


  public Vector BoundsMins { get; set; }


  public Vector BoundsMaxs { get; set; }

}
