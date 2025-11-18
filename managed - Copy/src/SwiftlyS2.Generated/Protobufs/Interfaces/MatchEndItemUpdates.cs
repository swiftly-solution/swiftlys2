
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface MatchEndItemUpdates : ITypedProtobuf<MatchEndItemUpdates>
{
  static MatchEndItemUpdates ITypedProtobuf<MatchEndItemUpdates>.Wrap(nint handle, bool isManuallyAllocated) => new MatchEndItemUpdatesImpl(handle, isManuallyAllocated);


  public ulong ItemId { get; set; }


  public uint ItemAttrDefidx { get; set; }


  public uint ItemAttrDeltaValue { get; set; }

}
