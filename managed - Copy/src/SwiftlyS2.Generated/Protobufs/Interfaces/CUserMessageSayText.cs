
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CUserMessageSayText : ITypedProtobuf<CUserMessageSayText>, INetMessage<CUserMessageSayText>, IDisposable
{
  static int INetMessage<CUserMessageSayText>.MessageId => 117;
  
  static string INetMessage<CUserMessageSayText>.MessageName => "CUserMessageSayText";

  static CUserMessageSayText ITypedProtobuf<CUserMessageSayText>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMessageSayTextImpl(handle, isManuallyAllocated);


  public int Playerindex { get; set; }


  public string Text { get; set; }


  public bool Chat { get; set; }

}
