
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CUserMessageSendAudio : ITypedProtobuf<CUserMessageSendAudio>, INetMessage<CUserMessageSendAudio>, IDisposable
{
  static int INetMessage<CUserMessageSendAudio>.MessageId => 130;
  
  static string INetMessage<CUserMessageSendAudio>.MessageName => "CUserMessageSendAudio";

  static CUserMessageSendAudio ITypedProtobuf<CUserMessageSendAudio>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMessageSendAudioImpl(handle, isManuallyAllocated);


  public string Soundname { get; set; }


  public bool Stop { get; set; }

}
