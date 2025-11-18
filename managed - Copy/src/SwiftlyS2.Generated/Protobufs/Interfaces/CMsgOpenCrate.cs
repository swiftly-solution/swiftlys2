
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgOpenCrate : ITypedProtobuf<CMsgOpenCrate>
{
  static CMsgOpenCrate ITypedProtobuf<CMsgOpenCrate>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgOpenCrateImpl(handle, isManuallyAllocated);


  public ulong ToolItemId { get; set; }


  public ulong SubjectItemId { get; set; }


  public bool ForRental { get; set; }


  public uint PointsRemaining { get; set; }

}
