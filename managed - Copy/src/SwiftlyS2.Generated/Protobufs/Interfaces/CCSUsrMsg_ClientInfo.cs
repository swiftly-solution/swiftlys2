
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_ClientInfo : ITypedProtobuf<CCSUsrMsg_ClientInfo>, INetMessage<CCSUsrMsg_ClientInfo>, IDisposable
{
  static int INetMessage<CCSUsrMsg_ClientInfo>.MessageId => 339;
  
  static string INetMessage<CCSUsrMsg_ClientInfo>.MessageName => "CCSUsrMsg_ClientInfo";

  static CCSUsrMsg_ClientInfo ITypedProtobuf<CCSUsrMsg_ClientInfo>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_ClientInfoImpl(handle, isManuallyAllocated);


  public int Dummy { get; set; }

}
