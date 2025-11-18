
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_ProcessSpottedEntityUpdate_SpottedEntityUpdateImpl : TypedProtobuf<CCSUsrMsg_ProcessSpottedEntityUpdate_SpottedEntityUpdate>, CCSUsrMsg_ProcessSpottedEntityUpdate_SpottedEntityUpdate
{
  public CCSUsrMsg_ProcessSpottedEntityUpdate_SpottedEntityUpdateImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int EntityIdx
  { get => Accessor.GetInt32("entity_idx"); set => Accessor.SetInt32("entity_idx", value); }


  public int ClassId
  { get => Accessor.GetInt32("class_id"); set => Accessor.SetInt32("class_id", value); }


  public int OriginX
  { get => Accessor.GetInt32("origin_x"); set => Accessor.SetInt32("origin_x", value); }


  public int OriginY
  { get => Accessor.GetInt32("origin_y"); set => Accessor.SetInt32("origin_y", value); }


  public int OriginZ
  { get => Accessor.GetInt32("origin_z"); set => Accessor.SetInt32("origin_z", value); }


  public int AngleY
  { get => Accessor.GetInt32("angle_y"); set => Accessor.SetInt32("angle_y", value); }


  public bool Defuser
  { get => Accessor.GetBool("defuser"); set => Accessor.SetBool("defuser", value); }


  public bool PlayerHasDefuser
  { get => Accessor.GetBool("player_has_defuser"); set => Accessor.SetBool("player_has_defuser", value); }


  public bool PlayerHasC4
  { get => Accessor.GetBool("player_has_c4"); set => Accessor.SetBool("player_has_c4", value); }

}
