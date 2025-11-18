
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CP2P_VoiceImpl : TypedProtobuf<CP2P_Voice>, CP2P_Voice
{
  public CP2P_VoiceImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public CMsgVoiceAudio Audio
  { get => new CMsgVoiceAudioImpl(NativeNetMessages.GetNestedMessage(Address, "audio"), false); }


  public uint BroadcastGroup
  { get => Accessor.GetUInt32("broadcast_group"); set => Accessor.SetUInt32("broadcast_group", value); }

}
