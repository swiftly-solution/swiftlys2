
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_SendLastKillerDamageToClient : ITypedProtobuf<CCSUsrMsg_SendLastKillerDamageToClient>, INetMessage<CCSUsrMsg_SendLastKillerDamageToClient>, IDisposable
{
  static int INetMessage<CCSUsrMsg_SendLastKillerDamageToClient>.MessageId => 351;
  
  static string INetMessage<CCSUsrMsg_SendLastKillerDamageToClient>.MessageName => "CCSUsrMsg_SendLastKillerDamageToClient";

  static CCSUsrMsg_SendLastKillerDamageToClient ITypedProtobuf<CCSUsrMsg_SendLastKillerDamageToClient>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_SendLastKillerDamageToClientImpl(handle, isManuallyAllocated);


  public int NumHitsGiven { get; set; }


  public int DamageGiven { get; set; }


  public int NumHitsTaken { get; set; }


  public int DamageTaken { get; set; }


  public int ActualDamageGiven { get; set; }


  public int ActualDamageTaken { get; set; }

}
