
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CUserMessageSayTextChannel : ITypedProtobuf<CUserMessageSayTextChannel>, INetMessage<CUserMessageSayTextChannel>, IDisposable
{
  static int INetMessage<CUserMessageSayTextChannel>.MessageId => 119;
  
  static string INetMessage<CUserMessageSayTextChannel>.MessageName => "CUserMessageSayTextChannel";

  static CUserMessageSayTextChannel ITypedProtobuf<CUserMessageSayTextChannel>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMessageSayTextChannelImpl(handle, isManuallyAllocated);


  public int Player { get; set; }


  public int Channel { get; set; }


  public string Text { get; set; }

}
