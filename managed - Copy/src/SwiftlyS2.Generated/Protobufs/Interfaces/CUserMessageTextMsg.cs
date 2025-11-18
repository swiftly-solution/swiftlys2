
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CUserMessageTextMsg : ITypedProtobuf<CUserMessageTextMsg>, INetMessage<CUserMessageTextMsg>, IDisposable
{
  static int INetMessage<CUserMessageTextMsg>.MessageId => 124;
  
  static string INetMessage<CUserMessageTextMsg>.MessageName => "CUserMessageTextMsg";

  static CUserMessageTextMsg ITypedProtobuf<CUserMessageTextMsg>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMessageTextMsgImpl(handle, isManuallyAllocated);


  public uint Dest { get; set; }


  public IProtobufRepeatedFieldValueType<string> Param { get; }

}
