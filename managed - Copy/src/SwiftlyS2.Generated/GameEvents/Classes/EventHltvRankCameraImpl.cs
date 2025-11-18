using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "hltv_rank_camera"
/// a camera ranking
/// </summary>
internal class EventHltvRankCameraImpl : GameEvent<EventHltvRankCamera>, EventHltvRankCamera
{

  public EventHltvRankCameraImpl(nint address) : base(address)
  {
  }

  // fixed camera index
  public byte Index
  { get => (byte)Accessor.GetInt32("index"); set => Accessor.SetInt32("index", value); }

  // ranking, how interesting is this camera view
  public float Rank
  { get => Accessor.GetFloat("rank"); set => Accessor.SetFloat("rank", value); }

  // best/closest target entity
  public int Target
  { get => Accessor.GetPlayerSlot("target"); set => Accessor.SetPlayerSlot("target", value); }
}
