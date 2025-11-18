
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgUseItemImpl : TypedProtobuf<CMsgUseItem>, CMsgUseItem
{
  public CMsgUseItemImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong ItemId
  { get => Accessor.GetUInt64("item_id"); set => Accessor.SetUInt64("item_id", value); }


  public ulong TargetSteamId
  { get => Accessor.GetUInt64("target_steam_id"); set => Accessor.SetUInt64("target_steam_id", value); }


  public IProtobufRepeatedFieldValueType<uint> GiftPotentialTargets
  { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "gift__potential_targets"); }


  public uint DuelClassLock
  { get => Accessor.GetUInt32("duel__class_lock"); set => Accessor.SetUInt32("duel__class_lock", value); }


  public ulong InitiatorSteamId
  { get => Accessor.GetUInt64("initiator_steam_id"); set => Accessor.SetUInt64("initiator_steam_id", value); }

}
