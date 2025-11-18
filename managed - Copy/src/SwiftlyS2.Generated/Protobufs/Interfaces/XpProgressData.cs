
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface XpProgressData : ITypedProtobuf<XpProgressData>
{
  static XpProgressData ITypedProtobuf<XpProgressData>.Wrap(nint handle, bool isManuallyAllocated) => new XpProgressDataImpl(handle, isManuallyAllocated);


  public uint XpPoints { get; set; }


  public int XpCategory { get; set; }

}
