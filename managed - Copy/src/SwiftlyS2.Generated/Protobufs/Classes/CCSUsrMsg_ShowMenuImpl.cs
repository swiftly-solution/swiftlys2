
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_ShowMenuImpl : NetMessage<CCSUsrMsg_ShowMenu>, CCSUsrMsg_ShowMenu
{
  public CCSUsrMsg_ShowMenuImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int BitsValidSlots
  { get => Accessor.GetInt32("bits_valid_slots"); set => Accessor.SetInt32("bits_valid_slots", value); }


  public int DisplayTime
  { get => Accessor.GetInt32("display_time"); set => Accessor.SetInt32("display_time", value); }


  public string MenuString
  { get => Accessor.GetString("menu_string"); set => Accessor.SetString("menu_string", value); }

}
