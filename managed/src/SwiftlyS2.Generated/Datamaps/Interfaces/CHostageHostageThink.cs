using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CHostageHostageThinkPreContext
{
    public CHostage SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CHostageHostageThinkPostContext
{
    public CHostage SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCHostageHostageThinkPreDelegate(ref CHostageHostageThinkPreContext ctx);
public delegate void OnCHostageHostageThinkPostDelegate(ref CHostageHostageThinkPostContext ctx);

public interface ICHostageHostageThinkHook
{
    public event OnCHostageHostageThinkPreDelegate Pre;
    public event OnCHostageHostageThinkPostDelegate Post;

    public void Invoke(CHostage schemaObject);
}