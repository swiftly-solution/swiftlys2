
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_VoteStartImpl : NetMessage<CCSUsrMsg_VoteStart>, CCSUsrMsg_VoteStart
{
  public CCSUsrMsg_VoteStartImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int Team
  { get => Accessor.GetInt32("team"); set => Accessor.SetInt32("team", value); }


  public int PlayerSlot
  { get => Accessor.GetInt32("player_slot"); set => Accessor.SetInt32("player_slot", value); }


  public int VoteType
  { get => Accessor.GetInt32("vote_type"); set => Accessor.SetInt32("vote_type", value); }


  public string DispStr
  { get => Accessor.GetString("disp_str"); set => Accessor.SetString("disp_str", value); }


  public string DetailsStr
  { get => Accessor.GetString("details_str"); set => Accessor.SetString("details_str", value); }


  public string OtherTeamStr
  { get => Accessor.GetString("other_team_str"); set => Accessor.SetString("other_team_str", value); }


  public bool IsYesNoVote
  { get => Accessor.GetBool("is_yes_no_vote"); set => Accessor.SetBool("is_yes_no_vote", value); }


  public int PlayerSlotTarget
  { get => Accessor.GetInt32("player_slot_target"); set => Accessor.SetInt32("player_slot_target", value); }

}
