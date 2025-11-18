
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_GCToClientChatImpl : TypedProtobuf<CMsgGCCStrike15_v2_GCToClientChat>, CMsgGCCStrike15_v2_GCToClientChat
{
  public CMsgGCCStrike15_v2_GCToClientChatImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint AccountId
  { get => Accessor.GetUInt32("account_id"); set => Accessor.SetUInt32("account_id", value); }


  public string Text
  { get => Accessor.GetString("text"); set => Accessor.SetString("text", value); }

}
