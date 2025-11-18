
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_SendPlayerLoadoutImpl : NetMessage<CCSUsrMsg_SendPlayerLoadout>, CCSUsrMsg_SendPlayerLoadout
{
  public CCSUsrMsg_SendPlayerLoadoutImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public IProtobufRepeatedFieldSubMessageType<CCSUsrMsg_SendPlayerLoadout_LoadoutItem> Loadout
  { get => new ProtobufRepeatedFieldSubMessageType<CCSUsrMsg_SendPlayerLoadout_LoadoutItem>(Accessor, "loadout"); }


  public int Playerslot
  { get => Accessor.GetInt32("playerslot"); set => Accessor.SetInt32("playerslot", value); }

}
