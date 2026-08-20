using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CFogControllerSetLerpValuesPreContext
{
    public CFogController SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CFogControllerSetLerpValuesPostContext
{
    public CFogController SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCFogControllerSetLerpValuesPreDelegate(ref CFogControllerSetLerpValuesPreContext ctx);
public delegate void OnCFogControllerSetLerpValuesPostDelegate(ref CFogControllerSetLerpValuesPostContext ctx);

public interface ICFogControllerSetLerpValuesHook
{
    public event OnCFogControllerSetLerpValuesPreDelegate Pre;
    public event OnCFogControllerSetLerpValuesPostDelegate Post;

    public void Invoke(CFogController schemaObject);
}