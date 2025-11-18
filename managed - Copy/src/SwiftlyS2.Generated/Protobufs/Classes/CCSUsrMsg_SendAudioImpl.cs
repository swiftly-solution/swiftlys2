
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_SendAudioImpl : NetMessage<CCSUsrMsg_SendAudio>, CCSUsrMsg_SendAudio
{
  public CCSUsrMsg_SendAudioImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public string RadioSound
  { get => Accessor.GetString("radio_sound"); set => Accessor.SetString("radio_sound", value); }

}
