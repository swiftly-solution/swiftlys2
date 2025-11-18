
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSVCMsg_SoundsImpl : NetMessage<CSVCMsg_Sounds>, CSVCMsg_Sounds
{
  public CSVCMsg_SoundsImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public bool ReliableSound
  { get => Accessor.GetBool("reliable_sound"); set => Accessor.SetBool("reliable_sound", value); }


  public IProtobufRepeatedFieldSubMessageType<CSVCMsg_Sounds_sounddata_t> Sounds
  { get => new ProtobufRepeatedFieldSubMessageType<CSVCMsg_Sounds_sounddata_t>(Accessor, "sounds"); }

}
