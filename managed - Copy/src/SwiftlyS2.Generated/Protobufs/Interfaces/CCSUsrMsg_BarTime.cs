
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_BarTime : ITypedProtobuf<CCSUsrMsg_BarTime>, INetMessage<CCSUsrMsg_BarTime>, IDisposable
{
  static int INetMessage<CCSUsrMsg_BarTime>.MessageId => 355;
  
  static string INetMessage<CCSUsrMsg_BarTime>.MessageName => "CCSUsrMsg_BarTime";

  static CCSUsrMsg_BarTime ITypedProtobuf<CCSUsrMsg_BarTime>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_BarTimeImpl(handle, isManuallyAllocated);


  public string Time { get; set; }

}
