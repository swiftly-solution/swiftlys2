
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSGOUserCmdPBImpl : TypedProtobuf<CSGOUserCmdPB>, CSGOUserCmdPB
{
  public CSGOUserCmdPBImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public CBaseUserCmdPB Base
  { get => new CBaseUserCmdPBImpl(NativeNetMessages.GetNestedMessage(Address, "base"), false); }


  public IProtobufRepeatedFieldSubMessageType<CSGOInputHistoryEntryPB> InputHistory
  { get => new ProtobufRepeatedFieldSubMessageType<CSGOInputHistoryEntryPB>(Accessor, "input_history"); }


  public int Attack1StartHistoryIndex
  { get => Accessor.GetInt32("attack1_start_history_index"); set => Accessor.SetInt32("attack1_start_history_index", value); }


  public int Attack2StartHistoryIndex
  { get => Accessor.GetInt32("attack2_start_history_index"); set => Accessor.SetInt32("attack2_start_history_index", value); }


  public bool LeftHandDesired
  { get => Accessor.GetBool("left_hand_desired"); set => Accessor.SetBool("left_hand_desired", value); }


  public bool IsPredictingBodyShotFx
  { get => Accessor.GetBool("is_predicting_body_shot_fx"); set => Accessor.SetBool("is_predicting_body_shot_fx", value); }


  public bool IsPredictingHeadShotFx
  { get => Accessor.GetBool("is_predicting_head_shot_fx"); set => Accessor.SetBool("is_predicting_head_shot_fx", value); }


  public bool IsPredictingKillRagdolls
  { get => Accessor.GetBool("is_predicting_kill_ragdolls"); set => Accessor.SetBool("is_predicting_kill_ragdolls", value); }

}
