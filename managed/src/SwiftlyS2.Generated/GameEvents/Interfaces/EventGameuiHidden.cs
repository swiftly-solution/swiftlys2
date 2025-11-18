using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "gameui_hidden"
/// </summary>
public interface EventGameuiHidden : IGameEvent<EventGameuiHidden>
{

    static EventGameuiHidden IGameEvent<EventGameuiHidden>.Create( nint address ) => new EventGameuiHiddenImpl(address);

    static string IGameEvent<EventGameuiHidden>.GetName() => "gameui_hidden";

    static uint IGameEvent<EventGameuiHidden>.GetHash() => 0xB938FB5Eu;
}
