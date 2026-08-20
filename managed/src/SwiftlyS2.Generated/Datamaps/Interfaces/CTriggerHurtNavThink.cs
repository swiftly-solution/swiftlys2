using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CTriggerHurtNavThinkPreContext
{
    public CTriggerHurt SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CTriggerHurtNavThinkPostContext
{
    public CTriggerHurt SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCTriggerHurtNavThinkPreDelegate(ref CTriggerHurtNavThinkPreContext ctx);
public delegate void OnCTriggerHurtNavThinkPostDelegate(ref CTriggerHurtNavThinkPostContext ctx);

public interface ICTriggerHurtNavThinkHook
{
    public event OnCTriggerHurtNavThinkPreDelegate Pre;
    public event OnCTriggerHurtNavThinkPostDelegate Post;

    public void Invoke(CTriggerHurt schemaObject);
}