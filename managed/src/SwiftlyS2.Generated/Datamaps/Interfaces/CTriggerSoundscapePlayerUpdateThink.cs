using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CTriggerSoundscapePlayerUpdateThinkPreContext
{
    public CTriggerSoundscape SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CTriggerSoundscapePlayerUpdateThinkPostContext
{
    public CTriggerSoundscape SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCTriggerSoundscapePlayerUpdateThinkPreDelegate(ref CTriggerSoundscapePlayerUpdateThinkPreContext ctx);
public delegate void OnCTriggerSoundscapePlayerUpdateThinkPostDelegate(ref CTriggerSoundscapePlayerUpdateThinkPostContext ctx);

public interface ICTriggerSoundscapePlayerUpdateThinkHook
{
    public event OnCTriggerSoundscapePlayerUpdateThinkPreDelegate Pre;
    public event OnCTriggerSoundscapePlayerUpdateThinkPostDelegate Post;

    public void Invoke(CTriggerSoundscape schemaObject);
}