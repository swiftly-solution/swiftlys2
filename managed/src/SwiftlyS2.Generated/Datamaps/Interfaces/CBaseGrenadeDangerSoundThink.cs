using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBaseGrenadeDangerSoundThinkPreContext
{
    public CBaseGrenade SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBaseGrenadeDangerSoundThinkPostContext
{
    public CBaseGrenade SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBaseGrenadeDangerSoundThinkPreDelegate(ref CBaseGrenadeDangerSoundThinkPreContext ctx);
public delegate void OnCBaseGrenadeDangerSoundThinkPostDelegate(ref CBaseGrenadeDangerSoundThinkPostContext ctx);

public interface ICBaseGrenadeDangerSoundThinkHook
{
    public event OnCBaseGrenadeDangerSoundThinkPreDelegate Pre;
    public event OnCBaseGrenadeDangerSoundThinkPostDelegate Post;

    public void Invoke(CBaseGrenade schemaObject);
}