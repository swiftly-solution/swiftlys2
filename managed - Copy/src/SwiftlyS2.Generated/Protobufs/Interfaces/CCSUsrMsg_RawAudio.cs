
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_RawAudio : ITypedProtobuf<CCSUsrMsg_RawAudio>, INetMessage<CCSUsrMsg_RawAudio>, IDisposable
{
  static int INetMessage<CCSUsrMsg_RawAudio>.MessageId => 318;
  
  static string INetMessage<CCSUsrMsg_RawAudio>.MessageName => "CCSUsrMsg_RawAudio";

  static CCSUsrMsg_RawAudio ITypedProtobuf<CCSUsrMsg_RawAudio>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_RawAudioImpl(handle, isManuallyAllocated);


  public int Pitch { get; set; }


  public int Entidx { get; set; }


  public float Duration { get; set; }


  public string VoiceFilename { get; set; }

}
