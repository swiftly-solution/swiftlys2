
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CSVCMsg_CreateStringTable : ITypedProtobuf<CSVCMsg_CreateStringTable>, INetMessage<CSVCMsg_CreateStringTable>, IDisposable
{
  static int INetMessage<CSVCMsg_CreateStringTable>.MessageId => 44;
  
  static string INetMessage<CSVCMsg_CreateStringTable>.MessageName => "CSVCMsg_CreateStringTable";

  static CSVCMsg_CreateStringTable ITypedProtobuf<CSVCMsg_CreateStringTable>.Wrap(nint handle, bool isManuallyAllocated) => new CSVCMsg_CreateStringTableImpl(handle, isManuallyAllocated);


  public string Name { get; set; }


  public int NumEntries { get; set; }


  public bool UserDataFixedSize { get; set; }


  public int UserDataSize { get; set; }


  public int UserDataSizeBits { get; set; }


  public int Flags { get; set; }


  public byte[] StringData { get; set; }


  public int UncompressedSize { get; set; }


  public bool DataCompressed { get; set; }


  public bool UsingVarintBitcounts { get; set; }

}
