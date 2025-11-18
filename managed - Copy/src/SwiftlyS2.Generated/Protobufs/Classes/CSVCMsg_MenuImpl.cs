
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSVCMsg_MenuImpl : NetMessage<CSVCMsg_Menu>, CSVCMsg_Menu
{
  public CSVCMsg_MenuImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int DialogType
  { get => Accessor.GetInt32("dialog_type"); set => Accessor.SetInt32("dialog_type", value); }


  public byte[] MenuKeyValues
  { get => Accessor.GetBytes("menu_key_values"); set => Accessor.SetBytes("menu_key_values", value); }

}
