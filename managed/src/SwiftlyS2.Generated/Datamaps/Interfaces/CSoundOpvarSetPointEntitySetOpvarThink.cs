using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CSoundOpvarSetPointEntitySetOpvarThinkPreContext
{
    public CSoundOpvarSetPointEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CSoundOpvarSetPointEntitySetOpvarThinkPostContext
{
    public CSoundOpvarSetPointEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCSoundOpvarSetPointEntitySetOpvarThinkPreDelegate(ref CSoundOpvarSetPointEntitySetOpvarThinkPreContext ctx);
public delegate void OnCSoundOpvarSetPointEntitySetOpvarThinkPostDelegate(ref CSoundOpvarSetPointEntitySetOpvarThinkPostContext ctx);

public interface ICSoundOpvarSetPointEntitySetOpvarThinkHook
{
    public event OnCSoundOpvarSetPointEntitySetOpvarThinkPreDelegate Pre;
    public event OnCSoundOpvarSetPointEntitySetOpvarThinkPostDelegate Post;

    public void Invoke(CSoundOpvarSetPointEntity schemaObject);
}