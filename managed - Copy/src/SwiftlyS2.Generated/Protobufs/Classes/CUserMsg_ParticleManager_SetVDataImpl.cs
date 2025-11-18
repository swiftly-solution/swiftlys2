
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMsg_ParticleManager_SetVDataImpl : TypedProtobuf<CUserMsg_ParticleManager_SetVData>, CUserMsg_ParticleManager_SetVData
{
  public CUserMsg_ParticleManager_SetVDataImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public string VdataName
  { get => Accessor.GetString("vdata_name"); set => Accessor.SetString("vdata_name", value); }

}
