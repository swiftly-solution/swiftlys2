
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_VGUIMenuImpl : NetMessage<CCSUsrMsg_VGUIMenu>, CCSUsrMsg_VGUIMenu
{
  public CCSUsrMsg_VGUIMenuImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public string Name
  { get => Accessor.GetString("name"); set => Accessor.SetString("name", value); }


  public bool Show
  { get => Accessor.GetBool("show"); set => Accessor.SetBool("show", value); }


  public IProtobufRepeatedFieldSubMessageType<CCSUsrMsg_VGUIMenu_Keys> Keys
  { get => new ProtobufRepeatedFieldSubMessageType<CCSUsrMsg_VGUIMenu_Keys>(Accessor, "keys"); }

}
