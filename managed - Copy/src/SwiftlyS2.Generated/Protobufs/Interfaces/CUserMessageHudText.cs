
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CUserMessageHudText : ITypedProtobuf<CUserMessageHudText>, INetMessage<CUserMessageHudText>, IDisposable
{
  static int INetMessage<CUserMessageHudText>.MessageId => 111;
  
  static string INetMessage<CUserMessageHudText>.MessageName => "CUserMessageHudText";

  static CUserMessageHudText ITypedProtobuf<CUserMessageHudText>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMessageHudTextImpl(handle, isManuallyAllocated);


  public string Message { get; set; }

}
