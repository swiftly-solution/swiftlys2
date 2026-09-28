using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CSoundOpvarSetDomeEntitySetOpvarThinkPreContext
{
    public CSoundOpvarSetDomeEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CSoundOpvarSetDomeEntitySetOpvarThinkPostContext
{
    public CSoundOpvarSetDomeEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCSoundOpvarSetDomeEntitySetOpvarThinkPreDelegate(ref CSoundOpvarSetDomeEntitySetOpvarThinkPreContext ctx);
public delegate void OnCSoundOpvarSetDomeEntitySetOpvarThinkPostDelegate(ref CSoundOpvarSetDomeEntitySetOpvarThinkPostContext ctx);

public interface ICSoundOpvarSetDomeEntitySetOpvarThinkHook
{
    public event OnCSoundOpvarSetDomeEntitySetOpvarThinkPreDelegate Pre;
    public event OnCSoundOpvarSetDomeEntitySetOpvarThinkPostDelegate Post;

    public void Invoke(CSoundOpvarSetDomeEntity schemaObject);
}