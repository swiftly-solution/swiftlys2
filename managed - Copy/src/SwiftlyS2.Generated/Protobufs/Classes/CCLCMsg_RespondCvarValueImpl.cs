
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCLCMsg_RespondCvarValueImpl : NetMessage<CCLCMsg_RespondCvarValue>, CCLCMsg_RespondCvarValue
{
  public CCLCMsg_RespondCvarValueImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int Cookie
  { get => Accessor.GetInt32("cookie"); set => Accessor.SetInt32("cookie", value); }


  public int StatusCode
  { get => Accessor.GetInt32("status_code"); set => Accessor.SetInt32("status_code", value); }


  public string Name
  { get => Accessor.GetString("name"); set => Accessor.SetString("name", value); }


  public string Value
  { get => Accessor.GetString("value"); set => Accessor.SetString("value", value); }

}
