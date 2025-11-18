
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMsg_ParticleManager_ClearModellistOverrideImpl : TypedProtobuf<CUserMsg_ParticleManager_ClearModellistOverride>, CUserMsg_ParticleManager_ClearModellistOverride
{
  public CUserMsg_ParticleManager_ClearModellistOverrideImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Groupid
  { get => Accessor.GetUInt32("groupid"); set => Accessor.SetUInt32("groupid", value); }

}
