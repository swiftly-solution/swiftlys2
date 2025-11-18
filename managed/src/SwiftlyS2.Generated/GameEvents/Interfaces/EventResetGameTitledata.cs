using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "reset_game_titledata"
/// reset user titledata; do not automatically write profile
/// </summary>
public interface EventResetGameTitledata : IGameEvent<EventResetGameTitledata>
{

    static EventResetGameTitledata IGameEvent<EventResetGameTitledata>.Create( nint address ) => new EventResetGameTitledataImpl(address);

    static string IGameEvent<EventResetGameTitledata>.GetName() => "reset_game_titledata";

    static uint IGameEvent<EventResetGameTitledata>.GetHash() => 0x2198EA36u;
    /// <summary>
    /// Controller id of user
    /// <br/>
    /// type: short
    /// </summary>
    public short ControllerId { get; set; }

}
