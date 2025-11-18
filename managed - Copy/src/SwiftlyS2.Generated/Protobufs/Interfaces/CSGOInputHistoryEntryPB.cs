
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CSGOInputHistoryEntryPB : ITypedProtobuf<CSGOInputHistoryEntryPB>
{
  static CSGOInputHistoryEntryPB ITypedProtobuf<CSGOInputHistoryEntryPB>.Wrap(nint handle, bool isManuallyAllocated) => new CSGOInputHistoryEntryPBImpl(handle, isManuallyAllocated);


  public QAngle ViewAngles { get; set; }


  public int RenderTickCount { get; set; }


  public float RenderTickFraction { get; set; }


  public int PlayerTickCount { get; set; }


  public float PlayerTickFraction { get; set; }


  public CSGOInterpolationInfoPB_CL ClInterp { get; }


  public CSGOInterpolationInfoPB SvInterp0 { get; }


  public CSGOInterpolationInfoPB SvInterp1 { get; }


  public CSGOInterpolationInfoPB PlayerInterp { get; }


  public int FrameNumber { get; set; }


  public int TargetEntIndex { get; set; }


  public Vector ShootPosition { get; set; }


  public Vector TargetHeadPosCheck { get; set; }


  public Vector TargetAbsPosCheck { get; set; }


  public QAngle TargetAbsAngCheck { get; set; }

}
