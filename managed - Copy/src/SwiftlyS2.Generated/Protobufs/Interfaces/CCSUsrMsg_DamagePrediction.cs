
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_DamagePrediction : ITypedProtobuf<CCSUsrMsg_DamagePrediction>, INetMessage<CCSUsrMsg_DamagePrediction>, IDisposable
{
  static int INetMessage<CCSUsrMsg_DamagePrediction>.MessageId => 386;
  
  static string INetMessage<CCSUsrMsg_DamagePrediction>.MessageName => "CCSUsrMsg_DamagePrediction";

  static CCSUsrMsg_DamagePrediction ITypedProtobuf<CCSUsrMsg_DamagePrediction>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_DamagePredictionImpl(handle, isManuallyAllocated);


  public int CommandNum { get; set; }


  public int PelletIdx { get; set; }


  public int VictimSlot { get; set; }


  public int VictimStartingHealth { get; set; }


  public int VictimDamage { get; set; }


  public Vector ShootPos { get; set; }


  public QAngle ShootDir { get; set; }


  public QAngle AimPunch { get; set; }

}
