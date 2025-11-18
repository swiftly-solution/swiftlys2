using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEventDefinitions;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "hltv_chat"
/// a HLTV chat msg sent by spectators
/// </summary>
internal class EventHltvChatImpl : GameEvent<EventHltvChat>, EventHltvChat
{

    public EventHltvChatImpl( nint address ) : base(address)
    {
    }

    public string Text { get => Accessor.GetString("text"); set => Accessor.SetString("text", value); }

    // steam id
    public ulong SteamID { get => Accessor.GetUInt64("steamID"); set => Accessor.SetUInt64("steamID", value); }
}
