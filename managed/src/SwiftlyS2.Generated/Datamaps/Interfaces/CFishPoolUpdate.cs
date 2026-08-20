using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CFishPoolUpdatePreContext
{
    public CFishPool SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CFishPoolUpdatePostContext
{
    public CFishPool SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCFishPoolUpdatePreDelegate(ref CFishPoolUpdatePreContext ctx);
public delegate void OnCFishPoolUpdatePostDelegate(ref CFishPoolUpdatePostContext ctx);

public interface ICFishPoolUpdateHook
{
    public event OnCFishPoolUpdatePreDelegate Pre;
    public event OnCFishPoolUpdatePostDelegate Post;

    public void Invoke(CFishPool schemaObject);
}