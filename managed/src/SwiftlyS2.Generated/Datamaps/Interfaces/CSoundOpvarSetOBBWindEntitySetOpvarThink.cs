using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CSoundOpvarSetOBBWindEntitySetOpvarThinkPreContext
{
    public CSoundOpvarSetOBBWindEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CSoundOpvarSetOBBWindEntitySetOpvarThinkPostContext
{
    public CSoundOpvarSetOBBWindEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCSoundOpvarSetOBBWindEntitySetOpvarThinkPreDelegate(ref CSoundOpvarSetOBBWindEntitySetOpvarThinkPreContext ctx);
public delegate void OnCSoundOpvarSetOBBWindEntitySetOpvarThinkPostDelegate(ref CSoundOpvarSetOBBWindEntitySetOpvarThinkPostContext ctx);

public interface ICSoundOpvarSetOBBWindEntitySetOpvarThinkHook
{
    public event OnCSoundOpvarSetOBBWindEntitySetOpvarThinkPreDelegate Pre;
    public event OnCSoundOpvarSetOBBWindEntitySetOpvarThinkPostDelegate Post;

    public void Invoke(CSoundOpvarSetOBBWindEntity schemaObject);
}