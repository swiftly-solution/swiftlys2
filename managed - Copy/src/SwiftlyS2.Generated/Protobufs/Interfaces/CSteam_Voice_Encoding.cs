
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CSteam_Voice_Encoding : ITypedProtobuf<CSteam_Voice_Encoding>
{
  static CSteam_Voice_Encoding ITypedProtobuf<CSteam_Voice_Encoding>.Wrap(nint handle, bool isManuallyAllocated) => new CSteam_Voice_EncodingImpl(handle, isManuallyAllocated);


  public byte[] VoiceData { get; set; }

}
