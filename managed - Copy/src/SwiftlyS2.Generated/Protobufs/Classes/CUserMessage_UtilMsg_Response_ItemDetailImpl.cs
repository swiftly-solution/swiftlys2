
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMessage_UtilMsg_Response_ItemDetailImpl : TypedProtobuf<CUserMessage_UtilMsg_Response_ItemDetail>, CUserMessage_UtilMsg_Response_ItemDetail
{
  public CUserMessage_UtilMsg_Response_ItemDetailImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int Index
  { get => Accessor.GetInt32("index"); set => Accessor.SetInt32("index", value); }


  public int Hash
  { get => Accessor.GetInt32("hash"); set => Accessor.SetInt32("hash", value); }


  public int Crc
  { get => Accessor.GetInt32("crc"); set => Accessor.SetInt32("crc", value); }


  public string Name
  { get => Accessor.GetString("name"); set => Accessor.SetString("name", value); }

}
