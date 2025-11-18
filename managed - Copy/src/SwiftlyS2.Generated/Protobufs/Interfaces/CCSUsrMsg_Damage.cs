
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_Damage : ITypedProtobuf<CCSUsrMsg_Damage>, INetMessage<CCSUsrMsg_Damage>, IDisposable
{
  static int INetMessage<CCSUsrMsg_Damage>.MessageId => 321;
  
  static string INetMessage<CCSUsrMsg_Damage>.MessageName => "CCSUsrMsg_Damage";

  static CCSUsrMsg_Damage ITypedProtobuf<CCSUsrMsg_Damage>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_DamageImpl(handle, isManuallyAllocated);


  public int Amount { get; set; }


  public Vector InflictorWorldPos { get; set; }


  public int VictimEntindex { get; set; }

}
