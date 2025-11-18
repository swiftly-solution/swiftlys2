
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CBaseUserCmdPBImpl : TypedProtobuf<CBaseUserCmdPB>, CBaseUserCmdPB
{
  public CBaseUserCmdPBImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int LegacyCommandNumber
  { get => Accessor.GetInt32("legacy_command_number"); set => Accessor.SetInt32("legacy_command_number", value); }


  public int ClientTick
  { get => Accessor.GetInt32("client_tick"); set => Accessor.SetInt32("client_tick", value); }


  public uint PredictionOffsetTicksX256
  { get => Accessor.GetUInt32("prediction_offset_ticks_x256"); set => Accessor.SetUInt32("prediction_offset_ticks_x256", value); }


  public CInButtonStatePB ButtonsPb
  { get => new CInButtonStatePBImpl(NativeNetMessages.GetNestedMessage(Address, "buttons_pb"), false); }


  public QAngle Viewangles
  { get => Accessor.GetQAngle("viewangles"); set => Accessor.SetQAngle("viewangles", value); }


  public float Forwardmove
  { get => Accessor.GetFloat("forwardmove"); set => Accessor.SetFloat("forwardmove", value); }


  public float Leftmove
  { get => Accessor.GetFloat("leftmove"); set => Accessor.SetFloat("leftmove", value); }


  public float Upmove
  { get => Accessor.GetFloat("upmove"); set => Accessor.SetFloat("upmove", value); }


  public int Impulse
  { get => Accessor.GetInt32("impulse"); set => Accessor.SetInt32("impulse", value); }


  public int Weaponselect
  { get => Accessor.GetInt32("weaponselect"); set => Accessor.SetInt32("weaponselect", value); }


  public int RandomSeed
  { get => Accessor.GetInt32("random_seed"); set => Accessor.SetInt32("random_seed", value); }


  public int Mousedx
  { get => Accessor.GetInt32("mousedx"); set => Accessor.SetInt32("mousedx", value); }


  public int Mousedy
  { get => Accessor.GetInt32("mousedy"); set => Accessor.SetInt32("mousedy", value); }


  public uint PawnEntityHandle
  { get => Accessor.GetUInt32("pawn_entity_handle"); set => Accessor.SetUInt32("pawn_entity_handle", value); }


  public IProtobufRepeatedFieldSubMessageType<CSubtickMoveStep> SubtickMoves
  { get => new ProtobufRepeatedFieldSubMessageType<CSubtickMoveStep>(Accessor, "subtick_moves"); }


  public byte[] MoveCrc
  { get => Accessor.GetBytes("move_crc"); set => Accessor.SetBytes("move_crc", value); }


  public uint ConsumedServerAngleChanges
  { get => Accessor.GetUInt32("consumed_server_angle_changes"); set => Accessor.SetUInt32("consumed_server_angle_changes", value); }


  public int CmdFlags
  { get => Accessor.GetInt32("cmd_flags"); set => Accessor.SetInt32("cmd_flags", value); }

}
