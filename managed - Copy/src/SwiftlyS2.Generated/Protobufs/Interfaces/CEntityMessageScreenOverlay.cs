
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CEntityMessageScreenOverlay : ITypedProtobuf<CEntityMessageScreenOverlay>
{
  static CEntityMessageScreenOverlay ITypedProtobuf<CEntityMessageScreenOverlay>.Wrap(nint handle, bool isManuallyAllocated) => new CEntityMessageScreenOverlayImpl(handle, isManuallyAllocated);


  public bool StartEffect { get; set; }


  public CEntityMsg EntityMsg { get; }

}
