
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSVCMsg_CreateStringTableImpl : NetMessage<CSVCMsg_CreateStringTable>, CSVCMsg_CreateStringTable
{
  public CSVCMsg_CreateStringTableImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public string Name
  { get => Accessor.GetString("name"); set => Accessor.SetString("name", value); }


  public int NumEntries
  { get => Accessor.GetInt32("num_entries"); set => Accessor.SetInt32("num_entries", value); }


  public bool UserDataFixedSize
  { get => Accessor.GetBool("user_data_fixed_size"); set => Accessor.SetBool("user_data_fixed_size", value); }


  public int UserDataSize
  { get => Accessor.GetInt32("user_data_size"); set => Accessor.SetInt32("user_data_size", value); }


  public int UserDataSizeBits
  { get => Accessor.GetInt32("user_data_size_bits"); set => Accessor.SetInt32("user_data_size_bits", value); }


  public int Flags
  { get => Accessor.GetInt32("flags"); set => Accessor.SetInt32("flags", value); }


  public byte[] StringData
  { get => Accessor.GetBytes("string_data"); set => Accessor.SetBytes("string_data", value); }


  public int UncompressedSize
  { get => Accessor.GetInt32("uncompressed_size"); set => Accessor.SetInt32("uncompressed_size", value); }


  public bool DataCompressed
  { get => Accessor.GetBool("data_compressed"); set => Accessor.SetBool("data_compressed", value); }


  public bool UsingVarintBitcounts
  { get => Accessor.GetBool("using_varint_bitcounts"); set => Accessor.SetBool("using_varint_bitcounts", value); }

}
