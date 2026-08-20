using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CChickenChickenThinkPreContext
{
    public CChicken SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CChickenChickenThinkPostContext
{
    public CChicken SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCChickenChickenThinkPreDelegate(ref CChickenChickenThinkPreContext ctx);
public delegate void OnCChickenChickenThinkPostDelegate(ref CChickenChickenThinkPostContext ctx);

public interface ICChickenChickenThinkHook
{
    public event OnCChickenChickenThinkPreDelegate Pre;
    public event OnCChickenChickenThinkPostDelegate Post;

    public void Invoke(CChicken schemaObject);
}