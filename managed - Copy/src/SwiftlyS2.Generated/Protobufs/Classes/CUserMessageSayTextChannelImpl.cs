
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMessageSayTextChannelImpl : NetMessage<CUserMessageSayTextChannel>, CUserMessageSayTextChannel
{
  public CUserMessageSayTextChannelImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int Player
  { get => Accessor.GetInt32("player"); set => Accessor.SetInt32("player", value); }


  public int Channel
  { get => Accessor.GetInt32("channel"); set => Accessor.SetInt32("channel", value); }


  public string Text
  { get => Accessor.GetString("text"); set => Accessor.SetString("text", value); }

}
