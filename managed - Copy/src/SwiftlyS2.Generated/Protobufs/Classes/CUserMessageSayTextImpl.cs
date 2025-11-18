
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMessageSayTextImpl : NetMessage<CUserMessageSayText>, CUserMessageSayText
{
  public CUserMessageSayTextImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int Playerindex
  { get => Accessor.GetInt32("playerindex"); set => Accessor.SetInt32("playerindex", value); }


  public string Text
  { get => Accessor.GetString("text"); set => Accessor.SetString("text", value); }


  public bool Chat
  { get => Accessor.GetBool("chat"); set => Accessor.SetBool("chat", value); }

}
