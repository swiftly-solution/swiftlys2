
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_ClientAccountBalanceImpl : TypedProtobuf<CMsgGCCStrike15_v2_ClientAccountBalance>, CMsgGCCStrike15_v2_ClientAccountBalance
{
  public CMsgGCCStrike15_v2_ClientAccountBalanceImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong Amount
  { get => Accessor.GetUInt64("amount"); set => Accessor.SetUInt64("amount", value); }


  public string Url
  { get => Accessor.GetString("url"); set => Accessor.SetString("url", value); }

}
