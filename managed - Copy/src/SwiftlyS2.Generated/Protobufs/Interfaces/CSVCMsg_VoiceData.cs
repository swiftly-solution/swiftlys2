
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CSVCMsg_VoiceData : ITypedProtobuf<CSVCMsg_VoiceData>, INetMessage<CSVCMsg_VoiceData>, IDisposable
{
  static int INetMessage<CSVCMsg_VoiceData>.MessageId => 47;
  
  static string INetMessage<CSVCMsg_VoiceData>.MessageName => "CSVCMsg_VoiceData";

  static CSVCMsg_VoiceData ITypedProtobuf<CSVCMsg_VoiceData>.Wrap(nint handle, bool isManuallyAllocated) => new CSVCMsg_VoiceDataImpl(handle, isManuallyAllocated);


  public CMsgVoiceAudio Audio { get; }


  public int Client { get; set; }


  public bool Proximity { get; set; }


  public ulong Xuid { get; set; }


  public int AudibleMask { get; set; }


  public uint Tick { get; set; }


  public int Passthrough { get; set; }

}
