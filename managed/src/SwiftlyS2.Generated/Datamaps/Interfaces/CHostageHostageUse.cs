using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CHostageHostageUsePreContext
{
    public CHostage SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CHostageHostageUsePostContext
{
    public CHostage SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCHostageHostageUsePreDelegate(ref CHostageHostageUsePreContext ctx);
public delegate void OnCHostageHostageUsePostDelegate(ref CHostageHostageUsePostContext ctx);

public interface ICHostageHostageUseHook
{
    public event OnCHostageHostageUsePreDelegate Pre;
    public event OnCHostageHostageUsePostDelegate Post;

    public void Invoke(CHostage schemaObject);
}