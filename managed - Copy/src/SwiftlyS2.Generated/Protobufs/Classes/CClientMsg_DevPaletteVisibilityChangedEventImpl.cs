
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CClientMsg_DevPaletteVisibilityChangedEventImpl : TypedProtobuf<CClientMsg_DevPaletteVisibilityChangedEvent>, CClientMsg_DevPaletteVisibilityChangedEvent
{
  public CClientMsg_DevPaletteVisibilityChangedEventImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public bool Visible
  { get => Accessor.GetBool("visible"); set => Accessor.SetBool("visible", value); }

}
