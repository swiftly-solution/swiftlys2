
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_HudTextImpl : NetMessage<CCSUsrMsg_HudText>, CCSUsrMsg_HudText
{
  public CCSUsrMsg_HudTextImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public string Text
  { get => Accessor.GetString("text"); set => Accessor.SetString("text", value); }

}
