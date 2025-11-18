
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_PlayerDecalDigitalSignatureImpl : NetMessage<CCSUsrMsg_PlayerDecalDigitalSignature>, CCSUsrMsg_PlayerDecalDigitalSignature
{
  public CCSUsrMsg_PlayerDecalDigitalSignatureImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public PlayerDecalDigitalSignature Data
  { get => new PlayerDecalDigitalSignatureImpl(NativeNetMessages.GetNestedMessage(Address, "data"), false); }

}
