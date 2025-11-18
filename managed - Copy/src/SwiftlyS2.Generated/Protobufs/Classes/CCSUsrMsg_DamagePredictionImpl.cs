
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_DamagePredictionImpl : NetMessage<CCSUsrMsg_DamagePrediction>, CCSUsrMsg_DamagePrediction
{
  public CCSUsrMsg_DamagePredictionImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int CommandNum
  { get => Accessor.GetInt32("command_num"); set => Accessor.SetInt32("command_num", value); }


  public int PelletIdx
  { get => Accessor.GetInt32("pellet_idx"); set => Accessor.SetInt32("pellet_idx", value); }


  public int VictimSlot
  { get => Accessor.GetInt32("victim_slot"); set => Accessor.SetInt32("victim_slot", value); }


  public int VictimStartingHealth
  { get => Accessor.GetInt32("victim_starting_health"); set => Accessor.SetInt32("victim_starting_health", value); }


  public int VictimDamage
  { get => Accessor.GetInt32("victim_damage"); set => Accessor.SetInt32("victim_damage", value); }


  public Vector ShootPos
  { get => Accessor.GetVector("shoot_pos"); set => Accessor.SetVector("shoot_pos", value); }


  public QAngle ShootDir
  { get => Accessor.GetQAngle("shoot_dir"); set => Accessor.SetQAngle("shoot_dir", value); }


  public QAngle AimPunch
  { get => Accessor.GetQAngle("aim_punch"); set => Accessor.SetQAngle("aim_punch", value); }

}
