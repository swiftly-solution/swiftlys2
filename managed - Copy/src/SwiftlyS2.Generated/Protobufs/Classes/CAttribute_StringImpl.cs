
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CAttribute_StringImpl : TypedProtobuf<CAttribute_String>, CAttribute_String
{
  public CAttribute_StringImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public string Value
  { get => Accessor.GetString("value"); set => Accessor.SetString("value", value); }

}
