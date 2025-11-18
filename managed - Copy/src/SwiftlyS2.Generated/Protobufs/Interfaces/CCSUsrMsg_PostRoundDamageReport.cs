
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_PostRoundDamageReport : ITypedProtobuf<CCSUsrMsg_PostRoundDamageReport>, INetMessage<CCSUsrMsg_PostRoundDamageReport>, IDisposable
{
  static int INetMessage<CCSUsrMsg_PostRoundDamageReport>.MessageId => 376;
  
  static string INetMessage<CCSUsrMsg_PostRoundDamageReport>.MessageName => "CCSUsrMsg_PostRoundDamageReport";

  static CCSUsrMsg_PostRoundDamageReport ITypedProtobuf<CCSUsrMsg_PostRoundDamageReport>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_PostRoundDamageReportImpl(handle, isManuallyAllocated);


  public ulong OtherXuid { get; set; }


  public int GivenKillType { get; set; }


  public int GivenHealthRemoved { get; set; }


  public int GivenNumHits { get; set; }


  public int TakenKillType { get; set; }


  public int TakenHealthRemoved { get; set; }


  public int TakenNumHits { get; set; }

}
