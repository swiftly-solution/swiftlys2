
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CMsgVDebugGameSessionIDEvent : ITypedProtobuf<CMsgVDebugGameSessionIDEvent>, INetMessage<CMsgVDebugGameSessionIDEvent>, IDisposable
{
  static int INetMessage<CMsgVDebugGameSessionIDEvent>.MessageId => 200;
  
  static string INetMessage<CMsgVDebugGameSessionIDEvent>.MessageName => "CMsgVDebugGameSessionIDEvent";

  static CMsgVDebugGameSessionIDEvent ITypedProtobuf<CMsgVDebugGameSessionIDEvent>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgVDebugGameSessionIDEventImpl(handle, isManuallyAllocated);


  public int Clientid { get; set; }


  public string Gamesessionid { get; set; }

}
