using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBarnLightThink_SetNextQueuedLightStylePreContext
{
    public CBarnLight SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBarnLightThink_SetNextQueuedLightStylePostContext
{
    public CBarnLight SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBarnLightThink_SetNextQueuedLightStylePreDelegate(ref CBarnLightThink_SetNextQueuedLightStylePreContext ctx);
public delegate void OnCBarnLightThink_SetNextQueuedLightStylePostDelegate(ref CBarnLightThink_SetNextQueuedLightStylePostContext ctx);

public interface ICBarnLightThink_SetNextQueuedLightStyleHook
{
    public event OnCBarnLightThink_SetNextQueuedLightStylePreDelegate Pre;
    public event OnCBarnLightThink_SetNextQueuedLightStylePostDelegate Post;

    public void Invoke(CBarnLight schemaObject);
}