
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsgPreMatchSayTextImpl : TypedProtobuf<CCSUsrMsgPreMatchSayText>, CCSUsrMsgPreMatchSayText
{
  public CCSUsrMsgPreMatchSayTextImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint AccountId
  { get => Accessor.GetUInt32("account_id"); set => Accessor.SetUInt32("account_id", value); }


  public string Text
  { get => Accessor.GetString("text"); set => Accessor.SetString("text", value); }


  public bool AllChat
  { get => Accessor.GetBool("all_chat"); set => Accessor.SetBool("all_chat", value); }

}
