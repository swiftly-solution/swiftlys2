
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CSVCMsg_VoiceInit : ITypedProtobuf<CSVCMsg_VoiceInit>, INetMessage<CSVCMsg_VoiceInit>, IDisposable
{
  static int INetMessage<CSVCMsg_VoiceInit>.MessageId => 46;
  
  static string INetMessage<CSVCMsg_VoiceInit>.MessageName => "CSVCMsg_VoiceInit";

  static CSVCMsg_VoiceInit ITypedProtobuf<CSVCMsg_VoiceInit>.Wrap(nint handle, bool isManuallyAllocated) => new CSVCMsg_VoiceInitImpl(handle, isManuallyAllocated);


  public int Quality { get; set; }


  public string Codec { get; set; }


  public int Version { get; set; }

}
