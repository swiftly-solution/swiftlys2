
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_GameTitle : ITypedProtobuf<CCSUsrMsg_GameTitle>, INetMessage<CCSUsrMsg_GameTitle>, IDisposable
{
  static int INetMessage<CCSUsrMsg_GameTitle>.MessageId => 310;
  
  static string INetMessage<CCSUsrMsg_GameTitle>.MessageName => "CCSUsrMsg_GameTitle";

  static CCSUsrMsg_GameTitle ITypedProtobuf<CCSUsrMsg_GameTitle>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_GameTitleImpl(handle, isManuallyAllocated);


  public int Dummy { get; set; }

}
