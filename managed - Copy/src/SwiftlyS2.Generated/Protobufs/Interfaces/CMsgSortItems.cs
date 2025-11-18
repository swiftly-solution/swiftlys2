
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSortItems : ITypedProtobuf<CMsgSortItems>
{
  static CMsgSortItems ITypedProtobuf<CMsgSortItems>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSortItemsImpl(handle, isManuallyAllocated);


  public uint SortType { get; set; }

}
