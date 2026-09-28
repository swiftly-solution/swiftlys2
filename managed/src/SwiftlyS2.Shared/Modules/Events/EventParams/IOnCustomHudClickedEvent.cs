using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.Events;

/// <summary>
/// Custom hud element clicked event parameters.
/// </summary>
public interface IOnCustomHudClickedEvent
{
    ///
    /// The player that clicked the custom hud element.
    /// 
    public int PlayerId { get; }

    /// <summary>
    /// The custom hud layout entity.
    /// </summary>
    public CCSCustomHudLayout CustomHudLayout { get; }

    /// <summary>
    /// The custom hud element button id.
    /// </summary>
    public string ButtonId { get; }
}