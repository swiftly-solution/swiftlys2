using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CTriggerHurtRadiationThinkPreContext
{
    public CTriggerHurt SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CTriggerHurtRadiationThinkPostContext
{
    public CTriggerHurt SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCTriggerHurtRadiationThinkPreDelegate(ref CTriggerHurtRadiationThinkPreContext ctx);
public delegate void OnCTriggerHurtRadiationThinkPostDelegate(ref CTriggerHurtRadiationThinkPostContext ctx);

public interface ICTriggerHurtRadiationThinkHook
{
    public event OnCTriggerHurtRadiationThinkPreDelegate Pre;
    public event OnCTriggerHurtRadiationThinkPostDelegate Post;

    public void Invoke(CTriggerHurt schemaObject);
}