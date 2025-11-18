
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CCLCMsg_HltvFixupOperatorTick : ITypedProtobuf<CCLCMsg_HltvFixupOperatorTick>
{
  static CCLCMsg_HltvFixupOperatorTick ITypedProtobuf<CCLCMsg_HltvFixupOperatorTick>.Wrap(nint handle, bool isManuallyAllocated) => new CCLCMsg_HltvFixupOperatorTickImpl(handle, isManuallyAllocated);


  public int Tick { get; set; }


  public byte[] PropsData { get; set; }


  public Vector Origin { get; set; }


  public QAngle EyeAngles { get; set; }


  public int ObserverMode { get; set; }


  public bool CameramanScoreboard { get; set; }


  public int ObserverTarget { get; set; }


  public Vector ViewOffset { get; set; }

}
