
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_XpUpdate : ITypedProtobuf<CCSUsrMsg_XpUpdate>, INetMessage<CCSUsrMsg_XpUpdate>, IDisposable
{
  static int INetMessage<CCSUsrMsg_XpUpdate>.MessageId => 365;
  
  static string INetMessage<CCSUsrMsg_XpUpdate>.MessageName => "CCSUsrMsg_XpUpdate";

  static CCSUsrMsg_XpUpdate ITypedProtobuf<CCSUsrMsg_XpUpdate>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_XpUpdateImpl(handle, isManuallyAllocated);


  public CMsgGCCstrike15_v2_GC2ServerNotifyXPRewarded Data { get; }

}
