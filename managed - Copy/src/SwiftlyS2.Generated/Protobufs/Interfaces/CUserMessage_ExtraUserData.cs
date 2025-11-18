
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CUserMessage_ExtraUserData : ITypedProtobuf<CUserMessage_ExtraUserData>, INetMessage<CUserMessage_ExtraUserData>, IDisposable
{
  static int INetMessage<CUserMessage_ExtraUserData>.MessageId => 164;
  
  static string INetMessage<CUserMessage_ExtraUserData>.MessageName => "CUserMessage_ExtraUserData";

  static CUserMessage_ExtraUserData ITypedProtobuf<CUserMessage_ExtraUserData>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMessage_ExtraUserDataImpl(handle, isManuallyAllocated);


  public int Item { get; set; }


  public long Value1 { get; set; }


  public long Value2 { get; set; }


  public IProtobufRepeatedFieldValueType<byte[]> Detail1 { get; }


  public IProtobufRepeatedFieldValueType<byte[]> Detail2 { get; }

}
