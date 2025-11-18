
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CEntityMessageFixAngle : ITypedProtobuf<CEntityMessageFixAngle>
{
  static CEntityMessageFixAngle ITypedProtobuf<CEntityMessageFixAngle>.Wrap(nint handle, bool isManuallyAllocated) => new CEntityMessageFixAngleImpl(handle, isManuallyAllocated);


  public bool Relative { get; set; }


  public QAngle Angle { get; set; }


  public CEntityMsg EntityMsg { get; }

}
