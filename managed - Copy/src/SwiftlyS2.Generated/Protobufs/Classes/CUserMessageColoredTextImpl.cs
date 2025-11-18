
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMessageColoredTextImpl : NetMessage<CUserMessageColoredText>, CUserMessageColoredText
{
  public CUserMessageColoredTextImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public uint Color
  { get => Accessor.GetUInt32("color"); set => Accessor.SetUInt32("color", value); }


  public string Text
  { get => Accessor.GetString("text"); set => Accessor.SetString("text", value); }


  public bool Reset
  { get => Accessor.GetBool("reset"); set => Accessor.SetBool("reset", value); }


  public int ContextPlayerSlot
  { get => Accessor.GetInt32("context_player_slot"); set => Accessor.SetInt32("context_player_slot", value); }


  public int ContextValue
  { get => Accessor.GetInt32("context_value"); set => Accessor.SetInt32("context_value", value); }


  public int ContextTeamId
  { get => Accessor.GetInt32("context_team_id"); set => Accessor.SetInt32("context_team_id", value); }

}
