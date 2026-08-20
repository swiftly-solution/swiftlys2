using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CSoundOpvarSetBoxEntitySetOpvarThinkPreContext
{
    public CSoundOpvarSetBoxEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CSoundOpvarSetBoxEntitySetOpvarThinkPostContext
{
    public CSoundOpvarSetBoxEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCSoundOpvarSetBoxEntitySetOpvarThinkPreDelegate(ref CSoundOpvarSetBoxEntitySetOpvarThinkPreContext ctx);
public delegate void OnCSoundOpvarSetBoxEntitySetOpvarThinkPostDelegate(ref CSoundOpvarSetBoxEntitySetOpvarThinkPostContext ctx);

public interface ICSoundOpvarSetBoxEntitySetOpvarThinkHook
{
    public event OnCSoundOpvarSetBoxEntitySetOpvarThinkPreDelegate Pre;
    public event OnCSoundOpvarSetBoxEntitySetOpvarThinkPostDelegate Post;

    public void Invoke(CSoundOpvarSetBoxEntity schemaObject);
}