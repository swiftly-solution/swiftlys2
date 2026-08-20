using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CPointValueRemapperUpdateThinkPreContext
{
    public CPointValueRemapper SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CPointValueRemapperUpdateThinkPostContext
{
    public CPointValueRemapper SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCPointValueRemapperUpdateThinkPreDelegate(ref CPointValueRemapperUpdateThinkPreContext ctx);
public delegate void OnCPointValueRemapperUpdateThinkPostDelegate(ref CPointValueRemapperUpdateThinkPostContext ctx);

public interface ICPointValueRemapperUpdateThinkHook
{
    public event OnCPointValueRemapperUpdateThinkPreDelegate Pre;
    public event OnCPointValueRemapperUpdateThinkPostDelegate Post;

    public void Invoke(CPointValueRemapper schemaObject);
}