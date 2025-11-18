
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSortItemsImpl : TypedProtobuf<CMsgSortItems>, CMsgSortItems
{
  public CMsgSortItemsImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint SortType
  { get => Accessor.GetUInt32("sort_type"); set => Accessor.SetUInt32("sort_type", value); }

}
