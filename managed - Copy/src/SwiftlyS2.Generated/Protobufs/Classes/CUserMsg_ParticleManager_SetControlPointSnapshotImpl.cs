
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMsg_ParticleManager_SetControlPointSnapshotImpl : TypedProtobuf<CUserMsg_ParticleManager_SetControlPointSnapshot>, CUserMsg_ParticleManager_SetControlPointSnapshot
{
  public CUserMsg_ParticleManager_SetControlPointSnapshotImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int ControlPoint
  { get => Accessor.GetInt32("control_point"); set => Accessor.SetInt32("control_point", value); }


  public string SnapshotName
  { get => Accessor.GetString("snapshot_name"); set => Accessor.SetString("snapshot_name", value); }

}
