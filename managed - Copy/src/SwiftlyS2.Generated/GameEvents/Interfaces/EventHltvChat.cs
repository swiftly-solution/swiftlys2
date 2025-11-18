using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "hltv_chat"
/// a HLTV chat msg sent by spectators
/// </summary>
public interface EventHltvChat : IGameEvent<EventHltvChat> {

  static EventHltvChat IGameEvent<EventHltvChat>.Create(nint address) => new EventHltvChatImpl(address);

  static string IGameEvent<EventHltvChat>.GetName() => "hltv_chat";

  static uint IGameEvent<EventHltvChat>.GetHash() => 0x91E5A35Au;
  /// <summary>
  /// type: string
  /// </summary>
  string Text { get; set; }

  /// <summary>
  /// steam id
  /// <br/>
  /// type: uint64
  /// </summary>
  ulong SteamID { get; set; }

}
