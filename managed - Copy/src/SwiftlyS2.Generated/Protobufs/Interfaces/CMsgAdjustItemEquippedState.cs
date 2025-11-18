
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgAdjustItemEquippedState : ITypedProtobuf<CMsgAdjustItemEquippedState>
{
  static CMsgAdjustItemEquippedState ITypedProtobuf<CMsgAdjustItemEquippedState>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgAdjustItemEquippedStateImpl(handle, isManuallyAllocated);


  public ulong ItemId { get; set; }


  public uint NewClass { get; set; }


  public uint NewSlot { get; set; }


  public bool Swap { get; set; }

}
