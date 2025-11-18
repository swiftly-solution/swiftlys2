
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCLCMsg_VoiceDataImpl : NetMessage<CCLCMsg_VoiceData>, CCLCMsg_VoiceData
{
  public CCLCMsg_VoiceDataImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public CMsgVoiceAudio Audio
  { get => new CMsgVoiceAudioImpl(NativeNetMessages.GetNestedMessage(Address, "audio"), false); }


  public ulong Xuid
  { get => Accessor.GetUInt64("xuid"); set => Accessor.SetUInt64("xuid", value); }


  public uint Tick
  { get => Accessor.GetUInt32("tick"); set => Accessor.SetUInt32("tick", value); }

}
