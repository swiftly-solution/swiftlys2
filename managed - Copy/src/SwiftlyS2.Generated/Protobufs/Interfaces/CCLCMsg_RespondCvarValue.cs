
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCLCMsg_RespondCvarValue : ITypedProtobuf<CCLCMsg_RespondCvarValue>, INetMessage<CCLCMsg_RespondCvarValue>, IDisposable
{
  static int INetMessage<CCLCMsg_RespondCvarValue>.MessageId => 25;
  
  static string INetMessage<CCLCMsg_RespondCvarValue>.MessageName => "CCLCMsg_RespondCvarValue";

  static CCLCMsg_RespondCvarValue ITypedProtobuf<CCLCMsg_RespondCvarValue>.Wrap(nint handle, bool isManuallyAllocated) => new CCLCMsg_RespondCvarValueImpl(handle, isManuallyAllocated);


  public int Cookie { get; set; }


  public int StatusCode { get; set; }


  public string Name { get; set; }


  public string Value { get; set; }

}
