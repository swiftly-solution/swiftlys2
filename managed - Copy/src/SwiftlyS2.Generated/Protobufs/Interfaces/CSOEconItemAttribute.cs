
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CSOEconItemAttribute : ITypedProtobuf<CSOEconItemAttribute>
{
  static CSOEconItemAttribute ITypedProtobuf<CSOEconItemAttribute>.Wrap(nint handle, bool isManuallyAllocated) => new CSOEconItemAttributeImpl(handle, isManuallyAllocated);


  public uint DefIndex { get; set; }


  public uint Value { get; set; }


  public byte[] ValueBytes { get; set; }

}
