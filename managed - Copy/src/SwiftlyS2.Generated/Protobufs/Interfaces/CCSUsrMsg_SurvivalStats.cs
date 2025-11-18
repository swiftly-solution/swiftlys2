
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_SurvivalStats : ITypedProtobuf<CCSUsrMsg_SurvivalStats>, INetMessage<CCSUsrMsg_SurvivalStats>, IDisposable
{
  static int INetMessage<CCSUsrMsg_SurvivalStats>.MessageId => 373;
  
  static string INetMessage<CCSUsrMsg_SurvivalStats>.MessageName => "CCSUsrMsg_SurvivalStats";

  static CCSUsrMsg_SurvivalStats ITypedProtobuf<CCSUsrMsg_SurvivalStats>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_SurvivalStatsImpl(handle, isManuallyAllocated);


  public ulong Xuid { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CCSUsrMsg_SurvivalStats_Fact> Facts { get; }


  public IProtobufRepeatedFieldSubMessageType<CCSUsrMsg_SurvivalStats_Placement> Users { get; }


  public IProtobufRepeatedFieldSubMessageType<CCSUsrMsg_SurvivalStats_Damage> Damages { get; }


  public int Ticknumber { get; set; }

}
