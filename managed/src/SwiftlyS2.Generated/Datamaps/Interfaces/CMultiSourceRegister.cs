using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CMultiSourceRegisterPreContext
{
    public CMultiSource SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CMultiSourceRegisterPostContext
{
    public CMultiSource SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCMultiSourceRegisterPreDelegate(ref CMultiSourceRegisterPreContext ctx);
public delegate void OnCMultiSourceRegisterPostDelegate(ref CMultiSourceRegisterPostContext ctx);

public interface ICMultiSourceRegisterHook
{
    public event OnCMultiSourceRegisterPreDelegate Pre;
    public event OnCMultiSourceRegisterPostDelegate Post;

    public void Invoke(CMultiSource schemaObject);
}