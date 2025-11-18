
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_HintText : ITypedProtobuf<CCSUsrMsg_HintText>, INetMessage<CCSUsrMsg_HintText>, IDisposable
{
  static int INetMessage<CCSUsrMsg_HintText>.MessageId => 323;
  
  static string INetMessage<CCSUsrMsg_HintText>.MessageName => "CCSUsrMsg_HintText";

  static CCSUsrMsg_HintText ITypedProtobuf<CCSUsrMsg_HintText>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_HintTextImpl(handle, isManuallyAllocated);


  public string Message { get; set; }

}
