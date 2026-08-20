using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CSoundEventSphereEntitySoundEventSphereThinkPreContext
{
    public CSoundEventSphereEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CSoundEventSphereEntitySoundEventSphereThinkPostContext
{
    public CSoundEventSphereEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCSoundEventSphereEntitySoundEventSphereThinkPreDelegate(ref CSoundEventSphereEntitySoundEventSphereThinkPreContext ctx);
public delegate void OnCSoundEventSphereEntitySoundEventSphereThinkPostDelegate(ref CSoundEventSphereEntitySoundEventSphereThinkPostContext ctx);

public interface ICSoundEventSphereEntitySoundEventSphereThinkHook
{
    public event OnCSoundEventSphereEntitySoundEventSphereThinkPreDelegate Pre;
    public event OnCSoundEventSphereEntitySoundEventSphereThinkPostDelegate Post;

    public void Invoke(CSoundEventSphereEntity schemaObject);
}