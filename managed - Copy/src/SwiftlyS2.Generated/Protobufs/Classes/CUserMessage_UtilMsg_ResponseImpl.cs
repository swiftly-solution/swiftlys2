
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMessage_UtilMsg_ResponseImpl : TypedProtobuf<CUserMessage_UtilMsg_Response>, CUserMessage_UtilMsg_Response
{
  public CUserMessage_UtilMsg_ResponseImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Crc
  { get => Accessor.GetUInt32("crc"); set => Accessor.SetUInt32("crc", value); }


  public int ItemCount
  { get => Accessor.GetInt32("item_count"); set => Accessor.SetInt32("item_count", value); }


  public uint Crc2
  { get => Accessor.GetUInt32("crc2"); set => Accessor.SetUInt32("crc2", value); }


  public int ItemCount2
  { get => Accessor.GetInt32("item_count2"); set => Accessor.SetInt32("item_count2", value); }


  public IProtobufRepeatedFieldValueType<int> CrcPart
  { get => new ProtobufRepeatedFieldValueType<int>(Accessor, "crc_part"); }


  public IProtobufRepeatedFieldValueType<int> CrcPart2
  { get => new ProtobufRepeatedFieldValueType<int>(Accessor, "crc_part2"); }


  public int ClientTimestamp
  { get => Accessor.GetInt32("client_timestamp"); set => Accessor.SetInt32("client_timestamp", value); }


  public int Platform
  { get => Accessor.GetInt32("platform"); set => Accessor.SetInt32("platform", value); }


  public IProtobufRepeatedFieldSubMessageType<CUserMessage_UtilMsg_Response_ItemDetail> Itemdetails
  { get => new ProtobufRepeatedFieldSubMessageType<CUserMessage_UtilMsg_Response_ItemDetail>(Accessor, "itemdetails"); }


  public int Itemgroup
  { get => Accessor.GetInt32("itemgroup"); set => Accessor.SetInt32("itemgroup", value); }


  public int TotalCount
  { get => Accessor.GetInt32("total_count"); set => Accessor.SetInt32("total_count", value); }


  public int TotalCount2
  { get => Accessor.GetInt32("total_count2"); set => Accessor.SetInt32("total_count2", value); }

}
