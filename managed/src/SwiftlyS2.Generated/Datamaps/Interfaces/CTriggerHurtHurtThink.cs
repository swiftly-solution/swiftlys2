using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CTriggerHurtHurtThinkPreContext
{
    public CTriggerHurt SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CTriggerHurtHurtThinkPostContext
{
    public CTriggerHurt SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCTriggerHurtHurtThinkPreDelegate(ref CTriggerHurtHurtThinkPreContext ctx);
public delegate void OnCTriggerHurtHurtThinkPostDelegate(ref CTriggerHurtHurtThinkPostContext ctx);

public interface ICTriggerHurtHurtThinkHook
{
    public event OnCTriggerHurtHurtThinkPreDelegate Pre;
    public event OnCTriggerHurtHurtThinkPostDelegate Post;

    public void Invoke(CTriggerHurt schemaObject);
}