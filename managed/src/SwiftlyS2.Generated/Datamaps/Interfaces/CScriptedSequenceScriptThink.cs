using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CScriptedSequenceScriptThinkPreContext
{
    public CScriptedSequence SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CScriptedSequenceScriptThinkPostContext
{
    public CScriptedSequence SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCScriptedSequenceScriptThinkPreDelegate(ref CScriptedSequenceScriptThinkPreContext ctx);
public delegate void OnCScriptedSequenceScriptThinkPostDelegate(ref CScriptedSequenceScriptThinkPostContext ctx);

public interface ICScriptedSequenceScriptThinkHook
{
    public event OnCScriptedSequenceScriptThinkPreDelegate Pre;
    public event OnCScriptedSequenceScriptThinkPostDelegate Post;

    public void Invoke(CScriptedSequence schemaObject);
}