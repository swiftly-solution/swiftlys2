
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgTEFireBullets_ExtraImpl : TypedProtobuf<CMsgTEFireBullets_Extra>, CMsgTEFireBullets_Extra
{
  public CMsgTEFireBullets_ExtraImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public QAngle AimPunch
  { get => Accessor.GetQAngle("aim_punch"); set => Accessor.SetQAngle("aim_punch", value); }


  public int AttackTickCount
  { get => Accessor.GetInt32("attack_tick_count"); set => Accessor.SetInt32("attack_tick_count", value); }


  public float AttackTickFrac
  { get => Accessor.GetFloat("attack_tick_frac"); set => Accessor.SetFloat("attack_tick_frac", value); }


  public int RenderTickCount
  { get => Accessor.GetInt32("render_tick_count"); set => Accessor.SetInt32("render_tick_count", value); }


  public float RenderTickFrac
  { get => Accessor.GetFloat("render_tick_frac"); set => Accessor.SetFloat("render_tick_frac", value); }


  public float InaccuracyMove
  { get => Accessor.GetFloat("inaccuracy_move"); set => Accessor.SetFloat("inaccuracy_move", value); }


  public float InaccuracyAir
  { get => Accessor.GetFloat("inaccuracy_air"); set => Accessor.SetFloat("inaccuracy_air", value); }


  public int Type
  { get => Accessor.GetInt32("type"); set => Accessor.SetInt32("type", value); }

}
