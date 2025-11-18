
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSGOInputHistoryEntryPBImpl : TypedProtobuf<CSGOInputHistoryEntryPB>, CSGOInputHistoryEntryPB
{
  public CSGOInputHistoryEntryPBImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public QAngle ViewAngles
  { get => Accessor.GetQAngle("view_angles"); set => Accessor.SetQAngle("view_angles", value); }


  public int RenderTickCount
  { get => Accessor.GetInt32("render_tick_count"); set => Accessor.SetInt32("render_tick_count", value); }


  public float RenderTickFraction
  { get => Accessor.GetFloat("render_tick_fraction"); set => Accessor.SetFloat("render_tick_fraction", value); }


  public int PlayerTickCount
  { get => Accessor.GetInt32("player_tick_count"); set => Accessor.SetInt32("player_tick_count", value); }


  public float PlayerTickFraction
  { get => Accessor.GetFloat("player_tick_fraction"); set => Accessor.SetFloat("player_tick_fraction", value); }


  public CSGOInterpolationInfoPB_CL ClInterp
  { get => new CSGOInterpolationInfoPB_CLImpl(NativeNetMessages.GetNestedMessage(Address, "cl_interp"), false); }


  public CSGOInterpolationInfoPB SvInterp0
  { get => new CSGOInterpolationInfoPBImpl(NativeNetMessages.GetNestedMessage(Address, "sv_interp0"), false); }


  public CSGOInterpolationInfoPB SvInterp1
  { get => new CSGOInterpolationInfoPBImpl(NativeNetMessages.GetNestedMessage(Address, "sv_interp1"), false); }


  public CSGOInterpolationInfoPB PlayerInterp
  { get => new CSGOInterpolationInfoPBImpl(NativeNetMessages.GetNestedMessage(Address, "player_interp"), false); }


  public int FrameNumber
  { get => Accessor.GetInt32("frame_number"); set => Accessor.SetInt32("frame_number", value); }


  public int TargetEntIndex
  { get => Accessor.GetInt32("target_ent_index"); set => Accessor.SetInt32("target_ent_index", value); }


  public Vector ShootPosition
  { get => Accessor.GetVector("shoot_position"); set => Accessor.SetVector("shoot_position", value); }


  public Vector TargetHeadPosCheck
  { get => Accessor.GetVector("target_head_pos_check"); set => Accessor.SetVector("target_head_pos_check", value); }


  public Vector TargetAbsPosCheck
  { get => Accessor.GetVector("target_abs_pos_check"); set => Accessor.SetVector("target_abs_pos_check", value); }


  public QAngle TargetAbsAngCheck
  { get => Accessor.GetQAngle("target_abs_ang_check"); set => Accessor.SetQAngle("target_abs_ang_check", value); }

}
