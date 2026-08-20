using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBarnLightThink_ApplyLightStylesToTargetsPreContext
{
    public CBarnLight SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBarnLightThink_ApplyLightStylesToTargetsPostContext
{
    public CBarnLight SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBarnLightThink_ApplyLightStylesToTargetsPreDelegate(ref CBarnLightThink_ApplyLightStylesToTargetsPreContext ctx);
public delegate void OnCBarnLightThink_ApplyLightStylesToTargetsPostDelegate(ref CBarnLightThink_ApplyLightStylesToTargetsPostContext ctx);

public interface ICBarnLightThink_ApplyLightStylesToTargetsHook
{
    public event OnCBarnLightThink_ApplyLightStylesToTargetsPreDelegate Pre;
    public event OnCBarnLightThink_ApplyLightStylesToTargetsPostDelegate Post;

    public void Invoke(CBarnLight schemaObject);
}