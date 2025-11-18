
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCLCMsg_VoiceData : ITypedProtobuf<CCLCMsg_VoiceData>, INetMessage<CCLCMsg_VoiceData>, IDisposable
{
  static int INetMessage<CCLCMsg_VoiceData>.MessageId => 22;
  
  static string INetMessage<CCLCMsg_VoiceData>.MessageName => "CCLCMsg_VoiceData";

  static CCLCMsg_VoiceData ITypedProtobuf<CCLCMsg_VoiceData>.Wrap(nint handle, bool isManuallyAllocated) => new CCLCMsg_VoiceDataImpl(handle, isManuallyAllocated);


  public CMsgVoiceAudio Audio { get; }


  public ulong Xuid { get; set; }


  public uint Tick { get; set; }

}
