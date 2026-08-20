using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CChickenChickenUsePreContext
{
    public CChicken SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CChickenChickenUsePostContext
{
    public CChicken SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCChickenChickenUsePreDelegate(ref CChickenChickenUsePreContext ctx);
public delegate void OnCChickenChickenUsePostDelegate(ref CChickenChickenUsePostContext ctx);

public interface ICChickenChickenUseHook
{
    public event OnCChickenChickenUsePreDelegate Pre;
    public event OnCChickenChickenUsePostDelegate Post;

    public void Invoke(CChicken schemaObject);
}