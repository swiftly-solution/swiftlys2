
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSVCMsg_VoiceInitImpl : NetMessage<CSVCMsg_VoiceInit>, CSVCMsg_VoiceInit
{
  public CSVCMsg_VoiceInitImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int Quality
  { get => Accessor.GetInt32("quality"); set => Accessor.SetInt32("quality", value); }


  public string Codec
  { get => Accessor.GetString("codec"); set => Accessor.SetString("codec", value); }


  public int Version
  { get => Accessor.GetInt32("version"); set => Accessor.SetInt32("version", value); }

}
