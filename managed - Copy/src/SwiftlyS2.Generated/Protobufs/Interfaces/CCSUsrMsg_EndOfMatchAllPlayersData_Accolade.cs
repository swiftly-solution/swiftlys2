
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CCSUsrMsg_EndOfMatchAllPlayersData_Accolade : ITypedProtobuf<CCSUsrMsg_EndOfMatchAllPlayersData_Accolade>
{
  static CCSUsrMsg_EndOfMatchAllPlayersData_Accolade ITypedProtobuf<CCSUsrMsg_EndOfMatchAllPlayersData_Accolade>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_EndOfMatchAllPlayersData_AccoladeImpl(handle, isManuallyAllocated);


  public int Eaccolade { get; set; }


  public float Value { get; set; }


  public int Position { get; set; }

}
