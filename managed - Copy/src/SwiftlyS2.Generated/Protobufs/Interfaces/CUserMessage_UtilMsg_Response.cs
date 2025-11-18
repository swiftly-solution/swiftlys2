
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMessage_UtilMsg_Response : ITypedProtobuf<CUserMessage_UtilMsg_Response>
{
  static CUserMessage_UtilMsg_Response ITypedProtobuf<CUserMessage_UtilMsg_Response>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMessage_UtilMsg_ResponseImpl(handle, isManuallyAllocated);


  public uint Crc { get; set; }


  public int ItemCount { get; set; }


  public uint Crc2 { get; set; }


  public int ItemCount2 { get; set; }


  public IProtobufRepeatedFieldValueType<int> CrcPart { get; }


  public IProtobufRepeatedFieldValueType<int> CrcPart2 { get; }


  public int ClientTimestamp { get; set; }


  public int Platform { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CUserMessage_UtilMsg_Response_ItemDetail> Itemdetails { get; }


  public int Itemgroup { get; set; }


  public int TotalCount { get; set; }


  public int TotalCount2 { get; set; }

}
