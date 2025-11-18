
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CBaseUserCmdPB : ITypedProtobuf<CBaseUserCmdPB>
{
  static CBaseUserCmdPB ITypedProtobuf<CBaseUserCmdPB>.Wrap(nint handle, bool isManuallyAllocated) => new CBaseUserCmdPBImpl(handle, isManuallyAllocated);


  public int LegacyCommandNumber { get; set; }


  public int ClientTick { get; set; }


  public uint PredictionOffsetTicksX256 { get; set; }


  public CInButtonStatePB ButtonsPb { get; }


  public QAngle Viewangles { get; set; }


  public float Forwardmove { get; set; }


  public float Leftmove { get; set; }


  public float Upmove { get; set; }


  public int Impulse { get; set; }


  public int Weaponselect { get; set; }


  public int RandomSeed { get; set; }


  public int Mousedx { get; set; }


  public int Mousedy { get; set; }


  public uint PawnEntityHandle { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CSubtickMoveStep> SubtickMoves { get; }


  public byte[] MoveCrc { get; set; }


  public uint ConsumedServerAngleChanges { get; set; }


  public int CmdFlags { get; set; }

}
