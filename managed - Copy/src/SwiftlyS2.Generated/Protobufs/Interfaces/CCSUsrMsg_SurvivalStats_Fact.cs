
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CCSUsrMsg_SurvivalStats_Fact : ITypedProtobuf<CCSUsrMsg_SurvivalStats_Fact>
{
  static CCSUsrMsg_SurvivalStats_Fact ITypedProtobuf<CCSUsrMsg_SurvivalStats_Fact>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_SurvivalStats_FactImpl(handle, isManuallyAllocated);


  public int Type { get; set; }


  public int Display { get; set; }


  public int Value { get; set; }


  public float Interestingness { get; set; }

}
