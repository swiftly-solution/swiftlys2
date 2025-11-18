
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_VGUIMenu_KeysImpl : TypedProtobuf<CCSUsrMsg_VGUIMenu_Keys>, CCSUsrMsg_VGUIMenu_Keys
{
  public CCSUsrMsg_VGUIMenu_KeysImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public string Name
  { get => Accessor.GetString("name"); set => Accessor.SetString("name", value); }


  public string Value
  { get => Accessor.GetString("value"); set => Accessor.SetString("value", value); }

}
