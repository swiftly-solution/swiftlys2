using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CAmbientGenericRampThinkPreContext
{
    public CAmbientGeneric SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CAmbientGenericRampThinkPostContext
{
    public CAmbientGeneric SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCAmbientGenericRampThinkPreDelegate(ref CAmbientGenericRampThinkPreContext ctx);
public delegate void OnCAmbientGenericRampThinkPostDelegate(ref CAmbientGenericRampThinkPostContext ctx);

public interface ICAmbientGenericRampThinkHook
{
    public event OnCAmbientGenericRampThinkPreDelegate Pre;
    public event OnCAmbientGenericRampThinkPostDelegate Post;

    public void Invoke(CAmbientGeneric schemaObject);
}