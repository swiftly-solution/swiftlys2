
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_DamageImpl : NetMessage<CCSUsrMsg_Damage>, CCSUsrMsg_Damage
{
  public CCSUsrMsg_DamageImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int Amount
  { get => Accessor.GetInt32("amount"); set => Accessor.SetInt32("amount", value); }


  public Vector InflictorWorldPos
  { get => Accessor.GetVector("inflictor_world_pos"); set => Accessor.SetVector("inflictor_world_pos", value); }


  public int VictimEntindex
  { get => Accessor.GetInt32("victim_entindex"); set => Accessor.SetInt32("victim_entindex", value); }

}
