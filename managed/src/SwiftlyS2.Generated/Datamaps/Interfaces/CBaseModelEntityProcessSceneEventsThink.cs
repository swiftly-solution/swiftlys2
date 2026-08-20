using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBaseModelEntityProcessSceneEventsThinkPreContext
{
    public CBaseModelEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBaseModelEntityProcessSceneEventsThinkPostContext
{
    public CBaseModelEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBaseModelEntityProcessSceneEventsThinkPreDelegate(ref CBaseModelEntityProcessSceneEventsThinkPreContext ctx);
public delegate void OnCBaseModelEntityProcessSceneEventsThinkPostDelegate(ref CBaseModelEntityProcessSceneEventsThinkPostContext ctx);

public interface ICBaseModelEntityProcessSceneEventsThinkHook
{
    public event OnCBaseModelEntityProcessSceneEventsThinkPreDelegate Pre;
    public event OnCBaseModelEntityProcessSceneEventsThinkPostDelegate Post;

    public void Invoke(CBaseModelEntity schemaObject);
}