
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class TournamentPlayerImpl : TypedProtobuf<TournamentPlayer>, TournamentPlayer
{
  public TournamentPlayerImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint AccountId
  { get => Accessor.GetUInt32("account_id"); set => Accessor.SetUInt32("account_id", value); }


  public string PlayerNick
  { get => Accessor.GetString("player_nick"); set => Accessor.SetString("player_nick", value); }


  public string PlayerName
  { get => Accessor.GetString("player_name"); set => Accessor.SetString("player_name", value); }


  public uint PlayerDob
  { get => Accessor.GetUInt32("player_dob"); set => Accessor.SetUInt32("player_dob", value); }


  public string PlayerFlag
  { get => Accessor.GetString("player_flag"); set => Accessor.SetString("player_flag", value); }


  public string PlayerLocation
  { get => Accessor.GetString("player_location"); set => Accessor.SetString("player_location", value); }


  public string PlayerDesc
  { get => Accessor.GetString("player_desc"); set => Accessor.SetString("player_desc", value); }

}
