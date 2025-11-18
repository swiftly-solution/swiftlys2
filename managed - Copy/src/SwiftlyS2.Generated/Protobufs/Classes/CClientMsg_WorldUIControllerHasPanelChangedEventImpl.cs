
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CClientMsg_WorldUIControllerHasPanelChangedEventImpl : TypedProtobuf<CClientMsg_WorldUIControllerHasPanelChangedEvent>, CClientMsg_WorldUIControllerHasPanelChangedEvent
{
  public CClientMsg_WorldUIControllerHasPanelChangedEventImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public bool HasPanel
  { get => Accessor.GetBool("has_panel"); set => Accessor.SetBool("has_panel", value); }


  public uint ClientEhandle
  { get => Accessor.GetUInt32("client_ehandle"); set => Accessor.SetUInt32("client_ehandle", value); }


  public uint LiteralHandType
  { get => Accessor.GetUInt32("literal_hand_type"); set => Accessor.SetUInt32("literal_hand_type", value); }

}
