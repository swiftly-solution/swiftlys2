
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CSVCMsg_TempEntities : ITypedProtobuf<CSVCMsg_TempEntities>
{
  static CSVCMsg_TempEntities ITypedProtobuf<CSVCMsg_TempEntities>.Wrap(nint handle, bool isManuallyAllocated) => new CSVCMsg_TempEntitiesImpl(handle, isManuallyAllocated);


  public bool Reliable { get; set; }


  public int NumEntries { get; set; }


  public byte[] EntityData { get; set; }

}
