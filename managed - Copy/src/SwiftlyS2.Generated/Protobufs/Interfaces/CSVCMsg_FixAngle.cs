
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CSVCMsg_FixAngle : ITypedProtobuf<CSVCMsg_FixAngle>
{
  static CSVCMsg_FixAngle ITypedProtobuf<CSVCMsg_FixAngle>.Wrap(nint handle, bool isManuallyAllocated) => new CSVCMsg_FixAngleImpl(handle, isManuallyAllocated);


  public bool Relative { get; set; }


  public QAngle Angle { get; set; }

}
