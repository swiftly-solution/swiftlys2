
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_UpdateScreenHealthBarImpl : NetMessage<CCSUsrMsg_UpdateScreenHealthBar>, CCSUsrMsg_UpdateScreenHealthBar
{
  public CCSUsrMsg_UpdateScreenHealthBarImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int Entidx
  { get => Accessor.GetInt32("entidx"); set => Accessor.SetInt32("entidx", value); }


  public float HealthratioOld
  { get => Accessor.GetFloat("healthratio_old"); set => Accessor.SetFloat("healthratio_old", value); }


  public float HealthratioNew
  { get => Accessor.GetFloat("healthratio_new"); set => Accessor.SetFloat("healthratio_new", value); }


  public int Style
  { get => Accessor.GetInt32("style"); set => Accessor.SetInt32("style", value); }

}
