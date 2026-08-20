using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBaseGrenadeTumbleThinkPreContext
{
    public CBaseGrenade SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBaseGrenadeTumbleThinkPostContext
{
    public CBaseGrenade SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBaseGrenadeTumbleThinkPreDelegate(ref CBaseGrenadeTumbleThinkPreContext ctx);
public delegate void OnCBaseGrenadeTumbleThinkPostDelegate(ref CBaseGrenadeTumbleThinkPostContext ctx);

public interface ICBaseGrenadeTumbleThinkHook
{
    public event OnCBaseGrenadeTumbleThinkPreDelegate Pre;
    public event OnCBaseGrenadeTumbleThinkPostDelegate Post;

    public void Invoke(CBaseGrenade schemaObject);
}