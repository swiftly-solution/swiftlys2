using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CChickenChickenTouchPreContext
{
    public CChicken SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CChickenChickenTouchPostContext
{
    public CChicken SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCChickenChickenTouchPreDelegate(ref CChickenChickenTouchPreContext ctx);
public delegate void OnCChickenChickenTouchPostDelegate(ref CChickenChickenTouchPostContext ctx);

public interface ICChickenChickenTouchHook
{
    public event OnCChickenChickenTouchPreDelegate Pre;
    public event OnCChickenChickenTouchPostDelegate Post;

    public void Invoke(CChicken schemaObject);
}