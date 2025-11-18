
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgModifyItemAttribute : ITypedProtobuf<CMsgModifyItemAttribute>
{
  static CMsgModifyItemAttribute ITypedProtobuf<CMsgModifyItemAttribute>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgModifyItemAttributeImpl(handle, isManuallyAllocated);


  public ulong ItemId { get; set; }


  public uint AttrDefidx { get; set; }


  public uint AttrValue { get; set; }

}
