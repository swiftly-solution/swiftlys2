
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_RawAudioImpl : NetMessage<CCSUsrMsg_RawAudio>, CCSUsrMsg_RawAudio
{
  public CCSUsrMsg_RawAudioImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int Pitch
  { get => Accessor.GetInt32("pitch"); set => Accessor.SetInt32("pitch", value); }


  public int Entidx
  { get => Accessor.GetInt32("entidx"); set => Accessor.SetInt32("entidx", value); }


  public float Duration
  { get => Accessor.GetFloat("duration"); set => Accessor.SetFloat("duration", value); }


  public string VoiceFilename
  { get => Accessor.GetString("voice_filename"); set => Accessor.SetString("voice_filename", value); }

}
