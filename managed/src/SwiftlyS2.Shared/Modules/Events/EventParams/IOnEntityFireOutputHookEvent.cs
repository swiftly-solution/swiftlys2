using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.Events;

/// <summary>
/// Called when the entity identity fire outputs.
/// </summary>
public interface IOnEntityFireOutputHookEvent
{
    /// <summary>
    /// The entity instance.
    /// </summary>
    public ref CEntityIOOutput EntityIO { get; }
    /// <summary>
    /// The designer name of the caller.
    /// </summary>
    public string DesignerName { get; }
    /// <summary>
    /// The name of the input being accepted.
    /// </summary>
    public string OutputName { get; }
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
    public ref CVariant<CVariantDefaultAllocator> VariantValue { get; }
    /// <summary>
    /// This delay of this IO event, in seconds.
    /// </summary>
    public float Delay { get; }
    /// <summary>
    /// The result of the hook.
    /// </summary>
    public HookResult Result { get; set; }
}