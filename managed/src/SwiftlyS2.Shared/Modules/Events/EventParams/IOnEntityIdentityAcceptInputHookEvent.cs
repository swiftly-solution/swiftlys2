using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.Events;

/// <summary>
/// Called when the entity identity accept input hook is triggered.
/// </summary>
public interface IOnEntityIdentityAcceptInputHookEvent
{
    /// <summary>
    /// The entity identity.
    /// </summary>
    public CEntityIdentity Identity { get; }
    /// <summary>
    /// The entity instance.
    /// </summary>
    public CEntityInstance EntityInstance { get; }
    /// <summary>
    /// The name of the input being accepted.
    /// </summary>
    public string InputName { get; }
    /// <summary>
    /// The value of the input being accepted.
    /// </summary>
    public CEntityInstance? Activator { get; }
    /// <summary>
    /// The caller of the input being accepted.
    /// </summary>
    public CEntityInstance? Caller { get; }
    /// <summary>
    /// The variant value of the input being accepted.
    /// </summary>
    public CVariant VariantValue { get; }
    /// <summary>
    /// The output ID of the input being accepted.
    /// </summary>
    public int OutputId { get; }
    /// <summary>
    /// The result of the hook.
    /// </summary>
    public HookResult Result { get; set; }
}