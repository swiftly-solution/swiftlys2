
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSOAccountItemPersonalStoreImpl : TypedProtobuf<CSOAccountItemPersonalStore>, CSOAccountItemPersonalStore
{
  public CSOAccountItemPersonalStoreImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint GenerationTime
  { get => Accessor.GetUInt32("generation_time"); set => Accessor.SetUInt32("generation_time", value); }


  public uint RedeemableBalance
  { get => Accessor.GetUInt32("redeemable_balance"); set => Accessor.SetUInt32("redeemable_balance", value); }


  public IProtobufRepeatedFieldValueType<ulong> Items
  { get => new ProtobufRepeatedFieldValueType<ulong>(Accessor, "items"); }

}
