
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_PlayerDecalDigitalSignature : ITypedProtobuf<CCSUsrMsg_PlayerDecalDigitalSignature>, INetMessage<CCSUsrMsg_PlayerDecalDigitalSignature>, IDisposable
{
  static int INetMessage<CCSUsrMsg_PlayerDecalDigitalSignature>.MessageId => 368;
  
  static string INetMessage<CCSUsrMsg_PlayerDecalDigitalSignature>.MessageName => "CCSUsrMsg_PlayerDecalDigitalSignature";

  static CCSUsrMsg_PlayerDecalDigitalSignature ITypedProtobuf<CCSUsrMsg_PlayerDecalDigitalSignature>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_PlayerDecalDigitalSignatureImpl(handle, isManuallyAllocated);


  public PlayerDecalDigitalSignature Data { get; }

}
