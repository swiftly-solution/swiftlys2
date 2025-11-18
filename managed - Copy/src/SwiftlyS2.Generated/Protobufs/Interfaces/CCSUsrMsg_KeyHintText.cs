
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_KeyHintText : ITypedProtobuf<CCSUsrMsg_KeyHintText>, INetMessage<CCSUsrMsg_KeyHintText>, IDisposable
{
  static int INetMessage<CCSUsrMsg_KeyHintText>.MessageId => 324;
  
  static string INetMessage<CCSUsrMsg_KeyHintText>.MessageName => "CCSUsrMsg_KeyHintText";

  static CCSUsrMsg_KeyHintText ITypedProtobuf<CCSUsrMsg_KeyHintText>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_KeyHintTextImpl(handle, isManuallyAllocated);


  public IProtobufRepeatedFieldValueType<string> Messages { get; }

}
