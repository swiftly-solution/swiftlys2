using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBaseModelEntitySUB_DissolveIfUncarriedPreContext
{
    public CBaseModelEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBaseModelEntitySUB_DissolveIfUncarriedPostContext
{
    public CBaseModelEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBaseModelEntitySUB_DissolveIfUncarriedPreDelegate(ref CBaseModelEntitySUB_DissolveIfUncarriedPreContext ctx);
public delegate void OnCBaseModelEntitySUB_DissolveIfUncarriedPostDelegate(ref CBaseModelEntitySUB_DissolveIfUncarriedPostContext ctx);

public interface ICBaseModelEntitySUB_DissolveIfUncarriedHook
{
    public event OnCBaseModelEntitySUB_DissolveIfUncarriedPreDelegate Pre;
    public event OnCBaseModelEntitySUB_DissolveIfUncarriedPostDelegate Post;

    public void Invoke(CBaseModelEntity schemaObject);
}