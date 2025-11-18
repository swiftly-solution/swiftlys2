
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_HudMsg : ITypedProtobuf<CCSUsrMsg_HudMsg>, INetMessage<CCSUsrMsg_HudMsg>, IDisposable
{
  static int INetMessage<CCSUsrMsg_HudMsg>.MessageId => 308;
  
  static string INetMessage<CCSUsrMsg_HudMsg>.MessageName => "CCSUsrMsg_HudMsg";

  static CCSUsrMsg_HudMsg ITypedProtobuf<CCSUsrMsg_HudMsg>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_HudMsgImpl(handle, isManuallyAllocated);


  public int Channel { get; set; }


  public Vector2D Pos { get; set; }


  public Color Clr1 { get; set; }


  public Color Clr2 { get; set; }


  public int Effect { get; set; }


  public float FadeInTime { get; set; }


  public float FadeOutTime { get; set; }


  public float HoldTime { get; set; }


  public float FxTime { get; set; }


  public string Text { get; set; }

}
