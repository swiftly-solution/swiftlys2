using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBarnLightThink_LightStyleEventPreContext
{
    public CBarnLight SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBarnLightThink_LightStyleEventPostContext
{
    public CBarnLight SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBarnLightThink_LightStyleEventPreDelegate(ref CBarnLightThink_LightStyleEventPreContext ctx);
public delegate void OnCBarnLightThink_LightStyleEventPostDelegate(ref CBarnLightThink_LightStyleEventPostContext ctx);

public interface ICBarnLightThink_LightStyleEventHook
{
    public event OnCBarnLightThink_LightStyleEventPreDelegate Pre;
    public event OnCBarnLightThink_LightStyleEventPostDelegate Post;

    public void Invoke(CBarnLight schemaObject);
}