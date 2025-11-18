
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMessage_NotifyResponseFound_CriteriaImpl : TypedProtobuf<CUserMessage_NotifyResponseFound_Criteria>, CUserMessage_NotifyResponseFound_Criteria
{
  public CUserMessage_NotifyResponseFound_CriteriaImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint NameSymbol
  { get => Accessor.GetUInt32("name_symbol"); set => Accessor.SetUInt32("name_symbol", value); }


  public string Value
  { get => Accessor.GetString("value"); set => Accessor.SetString("value", value); }

}
