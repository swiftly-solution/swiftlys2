
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_ClientPlayerDecalSignImpl : TypedProtobuf<CMsgGCCStrike15_v2_ClientPlayerDecalSign>, CMsgGCCStrike15_v2_ClientPlayerDecalSign
{
  public CMsgGCCStrike15_v2_ClientPlayerDecalSignImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public PlayerDecalDigitalSignature Data
  { get => new PlayerDecalDigitalSignatureImpl(NativeNetMessages.GetNestedMessage(Address, "data"), false); }


  public ulong Itemid
  { get => Accessor.GetUInt64("itemid"); set => Accessor.SetUInt64("itemid", value); }

}
