
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMsg_ParticleManager_UpdateEntityPosition : ITypedProtobuf<CUserMsg_ParticleManager_UpdateEntityPosition>
{
  static CUserMsg_ParticleManager_UpdateEntityPosition ITypedProtobuf<CUserMsg_ParticleManager_UpdateEntityPosition>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMsg_ParticleManager_UpdateEntityPositionImpl(handle, isManuallyAllocated);


  public uint EntityHandle { get; set; }


  public Vector Position { get; set; }

}
