
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSVCMsg_VoiceDataImpl : NetMessage<CSVCMsg_VoiceData>, CSVCMsg_VoiceData
{
  public CSVCMsg_VoiceDataImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public CMsgVoiceAudio Audio
  { get => new CMsgVoiceAudioImpl(NativeNetMessages.GetNestedMessage(Address, "audio"), false); }


  public int Client
  { get => Accessor.GetInt32("client"); set => Accessor.SetInt32("client", value); }


  public bool Proximity
  { get => Accessor.GetBool("proximity"); set => Accessor.SetBool("proximity", value); }


  public ulong Xuid
  { get => Accessor.GetUInt64("xuid"); set => Accessor.SetUInt64("xuid", value); }


  public int AudibleMask
  { get => Accessor.GetInt32("audible_mask"); set => Accessor.SetInt32("audible_mask", value); }


  public uint Tick
  { get => Accessor.GetUInt32("tick"); set => Accessor.SetUInt32("tick", value); }


  public int Passthrough
  { get => Accessor.GetInt32("passthrough"); set => Accessor.SetInt32("passthrough", value); }

}
