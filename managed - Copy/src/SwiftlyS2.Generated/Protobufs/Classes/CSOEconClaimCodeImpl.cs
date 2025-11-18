
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSOEconClaimCodeImpl : TypedProtobuf<CSOEconClaimCode>, CSOEconClaimCode
{
  public CSOEconClaimCodeImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint AccountId
  { get => Accessor.GetUInt32("account_id"); set => Accessor.SetUInt32("account_id", value); }


  public uint CodeType
  { get => Accessor.GetUInt32("code_type"); set => Accessor.SetUInt32("code_type", value); }


  public uint TimeAcquired
  { get => Accessor.GetUInt32("time_acquired"); set => Accessor.SetUInt32("time_acquired", value); }


  public string Code
  { get => Accessor.GetString("code"); set => Accessor.SetString("code", value); }

}
