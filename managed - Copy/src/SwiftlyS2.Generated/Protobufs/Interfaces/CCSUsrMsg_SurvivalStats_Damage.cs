
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CCSUsrMsg_SurvivalStats_Damage : ITypedProtobuf<CCSUsrMsg_SurvivalStats_Damage>
{
  static CCSUsrMsg_SurvivalStats_Damage ITypedProtobuf<CCSUsrMsg_SurvivalStats_Damage>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_SurvivalStats_DamageImpl(handle, isManuallyAllocated);


  public ulong Xuid { get; set; }


  public int To { get; set; }


  public int ToHits { get; set; }


  public int From { get; set; }


  public int FromHits { get; set; }

}
