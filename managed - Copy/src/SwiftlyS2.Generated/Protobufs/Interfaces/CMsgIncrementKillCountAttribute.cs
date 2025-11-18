
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgIncrementKillCountAttribute : ITypedProtobuf<CMsgIncrementKillCountAttribute>
{
  static CMsgIncrementKillCountAttribute ITypedProtobuf<CMsgIncrementKillCountAttribute>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgIncrementKillCountAttributeImpl(handle, isManuallyAllocated);


  public uint KillerAccountId { get; set; }


  public uint VictimAccountId { get; set; }


  public ulong ItemId { get; set; }


  public uint EventType { get; set; }


  public uint Amount { get; set; }

}
