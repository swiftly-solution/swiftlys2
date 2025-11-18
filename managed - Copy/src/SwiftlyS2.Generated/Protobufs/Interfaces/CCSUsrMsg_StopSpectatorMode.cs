
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_StopSpectatorMode : ITypedProtobuf<CCSUsrMsg_StopSpectatorMode>, INetMessage<CCSUsrMsg_StopSpectatorMode>, IDisposable
{
  static int INetMessage<CCSUsrMsg_StopSpectatorMode>.MessageId => 329;
  
  static string INetMessage<CCSUsrMsg_StopSpectatorMode>.MessageName => "CCSUsrMsg_StopSpectatorMode";

  static CCSUsrMsg_StopSpectatorMode ITypedProtobuf<CCSUsrMsg_StopSpectatorMode>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_StopSpectatorModeImpl(handle, isManuallyAllocated);


  public int Dummy { get; set; }

}
