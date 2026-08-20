using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CCSPlayerPawnCheckStuffThinkPreContext
{
    public CCSPlayerPawn SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CCSPlayerPawnCheckStuffThinkPostContext
{
    public CCSPlayerPawn SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCCSPlayerPawnCheckStuffThinkPreDelegate(ref CCSPlayerPawnCheckStuffThinkPreContext ctx);
public delegate void OnCCSPlayerPawnCheckStuffThinkPostDelegate(ref CCSPlayerPawnCheckStuffThinkPostContext ctx);

public interface ICCSPlayerPawnCheckStuffThinkHook
{
    public event OnCCSPlayerPawnCheckStuffThinkPreDelegate Pre;
    public event OnCCSPlayerPawnCheckStuffThinkPostDelegate Post;

    public void Invoke(CCSPlayerPawn schemaObject);
}