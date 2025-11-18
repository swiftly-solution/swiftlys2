
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgPlayerBulletHitImpl : TypedProtobuf<CMsgPlayerBulletHit>, CMsgPlayerBulletHit
{
  public CMsgPlayerBulletHitImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int AttackerSlot
  { get => Accessor.GetInt32("attacker_slot"); set => Accessor.SetInt32("attacker_slot", value); }


  public int VictimSlot
  { get => Accessor.GetInt32("victim_slot"); set => Accessor.SetInt32("victim_slot", value); }


  public Vector VictimPos
  { get => Accessor.GetVector("victim_pos"); set => Accessor.SetVector("victim_pos", value); }


  public int HitGroup
  { get => Accessor.GetInt32("hit_group"); set => Accessor.SetInt32("hit_group", value); }


  public int Damage
  { get => Accessor.GetInt32("damage"); set => Accessor.SetInt32("damage", value); }


  public int PenetrationCount
  { get => Accessor.GetInt32("penetration_count"); set => Accessor.SetInt32("penetration_count", value); }


  public bool IsKill
  { get => Accessor.GetBool("is_kill"); set => Accessor.SetBool("is_kill", value); }

}
