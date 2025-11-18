
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSteam_Voice_EncodingImpl : TypedProtobuf<CSteam_Voice_Encoding>, CSteam_Voice_Encoding
{
  public CSteam_Voice_EncodingImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public byte[] VoiceData
  { get => Accessor.GetBytes("voice_data"); set => Accessor.SetBytes("voice_data", value); }

}
