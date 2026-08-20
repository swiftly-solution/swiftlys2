using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CTriggerFanPushThinkPreContext
{
    public CTriggerFan SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CTriggerFanPushThinkPostContext
{
    public CTriggerFan SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCTriggerFanPushThinkPreDelegate(ref CTriggerFanPushThinkPreContext ctx);
public delegate void OnCTriggerFanPushThinkPostDelegate(ref CTriggerFanPushThinkPostContext ctx);

public interface ICTriggerFanPushThinkHook
{
    public event OnCTriggerFanPushThinkPreDelegate Pre;
    public event OnCTriggerFanPushThinkPostDelegate Post;

    public void Invoke(CTriggerFan schemaObject);
}