
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CSVCMsg_GameEvent_key_t : ITypedProtobuf<CSVCMsg_GameEvent_key_t>
{
  static CSVCMsg_GameEvent_key_t ITypedProtobuf<CSVCMsg_GameEvent_key_t>.Wrap(nint handle, bool isManuallyAllocated) => new CSVCMsg_GameEvent_key_tImpl(handle, isManuallyAllocated);


  public int Type { get; set; }


  public string ValString { get; set; }


  public float ValFloat { get; set; }


  public int ValLong { get; set; }


  public int ValShort { get; set; }


  public int ValByte { get; set; }


  public bool ValBool { get; set; }


  public ulong ValUint64 { get; set; }

}
