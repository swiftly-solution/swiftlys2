using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBaseEntityClearNavIgnoreContentsThinkPreContext
{
    public CBaseEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBaseEntityClearNavIgnoreContentsThinkPostContext
{
    public CBaseEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBaseEntityClearNavIgnoreContentsThinkPreDelegate(ref CBaseEntityClearNavIgnoreContentsThinkPreContext ctx);
public delegate void OnCBaseEntityClearNavIgnoreContentsThinkPostDelegate(ref CBaseEntityClearNavIgnoreContentsThinkPostContext ctx);

public interface ICBaseEntityClearNavIgnoreContentsThinkHook
{
    public event OnCBaseEntityClearNavIgnoreContentsThinkPreDelegate Pre;
    public event OnCBaseEntityClearNavIgnoreContentsThinkPostDelegate Post;

    public void Invoke(CBaseEntity schemaObject);
}