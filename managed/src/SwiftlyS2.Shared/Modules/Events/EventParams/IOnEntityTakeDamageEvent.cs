using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.Events;


/// <summary>
/// Called when an entity takes damage.
/// </summary>
public interface IOnEntityTakeDamageEvent
{
    /// <summary>
    /// The entity that took damage.
    /// </summary>
    public CEntityInstance Entity { get; }

    /// <summary>
    /// The damage info.
    /// </summary>
    public ref CTakeDamageInfo Info { get; }

    /// <summary>
    /// The damage result.
    /// </summary>
    public ref CTakeDamageResult DamageResult { get; }

    /// <summary>
    /// If return <see cref="HookResult.Stop"/>, the damage will not be applied.
    /// </summary>
    public HookResult Result { get; set; }
}