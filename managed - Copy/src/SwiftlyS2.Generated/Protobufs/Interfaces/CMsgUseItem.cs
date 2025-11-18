
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgUseItem : ITypedProtobuf<CMsgUseItem>
{
  static CMsgUseItem ITypedProtobuf<CMsgUseItem>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgUseItemImpl(handle, isManuallyAllocated);


  public ulong ItemId { get; set; }


  public ulong TargetSteamId { get; set; }


  public IProtobufRepeatedFieldValueType<uint> GiftPotentialTargets { get; }


  public uint DuelClassLock { get; set; }


  public ulong InitiatorSteamId { get; set; }

}
