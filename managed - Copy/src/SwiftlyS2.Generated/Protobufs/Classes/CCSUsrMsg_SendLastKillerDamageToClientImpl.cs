
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_SendLastKillerDamageToClientImpl : NetMessage<CCSUsrMsg_SendLastKillerDamageToClient>, CCSUsrMsg_SendLastKillerDamageToClient
{
  public CCSUsrMsg_SendLastKillerDamageToClientImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int NumHitsGiven
  { get => Accessor.GetInt32("num_hits_given"); set => Accessor.SetInt32("num_hits_given", value); }


  public int DamageGiven
  { get => Accessor.GetInt32("damage_given"); set => Accessor.SetInt32("damage_given", value); }


  public int NumHitsTaken
  { get => Accessor.GetInt32("num_hits_taken"); set => Accessor.SetInt32("num_hits_taken", value); }


  public int DamageTaken
  { get => Accessor.GetInt32("damage_taken"); set => Accessor.SetInt32("damage_taken", value); }


  public int ActualDamageGiven
  { get => Accessor.GetInt32("actual_damage_given"); set => Accessor.SetInt32("actual_damage_given", value); }


  public int ActualDamageTaken
  { get => Accessor.GetInt32("actual_damage_taken"); set => Accessor.SetInt32("actual_damage_taken", value); }

}
