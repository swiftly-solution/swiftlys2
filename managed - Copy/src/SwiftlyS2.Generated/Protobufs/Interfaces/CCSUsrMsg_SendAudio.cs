
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_SendAudio : ITypedProtobuf<CCSUsrMsg_SendAudio>, INetMessage<CCSUsrMsg_SendAudio>, IDisposable
{
  static int INetMessage<CCSUsrMsg_SendAudio>.MessageId => 317;
  
  static string INetMessage<CCSUsrMsg_SendAudio>.MessageName => "CCSUsrMsg_SendAudio";

  static CCSUsrMsg_SendAudio ITypedProtobuf<CCSUsrMsg_SendAudio>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_SendAudioImpl(handle, isManuallyAllocated);


  public string RadioSound { get; set; }

}
