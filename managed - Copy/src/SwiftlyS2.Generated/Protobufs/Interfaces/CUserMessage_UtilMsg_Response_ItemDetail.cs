
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMessage_UtilMsg_Response_ItemDetail : ITypedProtobuf<CUserMessage_UtilMsg_Response_ItemDetail>
{
  static CUserMessage_UtilMsg_Response_ItemDetail ITypedProtobuf<CUserMessage_UtilMsg_Response_ItemDetail>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMessage_UtilMsg_Response_ItemDetailImpl(handle, isManuallyAllocated);


  public int Index { get; set; }


  public int Hash { get; set; }


  public int Crc { get; set; }


  public string Name { get; set; }

}
