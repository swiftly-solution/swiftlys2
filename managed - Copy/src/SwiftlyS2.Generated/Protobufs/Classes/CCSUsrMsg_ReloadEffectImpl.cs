
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_ReloadEffectImpl : NetMessage<CCSUsrMsg_ReloadEffect>, CCSUsrMsg_ReloadEffect
{
  public CCSUsrMsg_ReloadEffectImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int Entidx
  { get => Accessor.GetInt32("entidx"); set => Accessor.SetInt32("entidx", value); }


  public int Actanim
  { get => Accessor.GetInt32("actanim"); set => Accessor.SetInt32("actanim", value); }


  public float OriginX
  { get => Accessor.GetFloat("origin_x"); set => Accessor.SetFloat("origin_x", value); }


  public float OriginY
  { get => Accessor.GetFloat("origin_y"); set => Accessor.SetFloat("origin_y", value); }


  public float OriginZ
  { get => Accessor.GetFloat("origin_z"); set => Accessor.SetFloat("origin_z", value); }

}
