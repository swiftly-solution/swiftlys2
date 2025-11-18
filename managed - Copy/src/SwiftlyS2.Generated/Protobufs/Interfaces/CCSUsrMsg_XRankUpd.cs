
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_XRankUpd : ITypedProtobuf<CCSUsrMsg_XRankUpd>, INetMessage<CCSUsrMsg_XRankUpd>, IDisposable
{
  static int INetMessage<CCSUsrMsg_XRankUpd>.MessageId => 341;
  
  static string INetMessage<CCSUsrMsg_XRankUpd>.MessageName => "CCSUsrMsg_XRankUpd";

  static CCSUsrMsg_XRankUpd ITypedProtobuf<CCSUsrMsg_XRankUpd>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_XRankUpdImpl(handle, isManuallyAllocated);


  public int ModeIdx { get; set; }


  public int Controller { get; set; }


  public int Ranking { get; set; }

}
