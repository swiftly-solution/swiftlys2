using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CTriggerActiveWeaponDetectActiveWeaponThinkPreContext
{
    public CTriggerActiveWeaponDetect SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CTriggerActiveWeaponDetectActiveWeaponThinkPostContext
{
    public CTriggerActiveWeaponDetect SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCTriggerActiveWeaponDetectActiveWeaponThinkPreDelegate(ref CTriggerActiveWeaponDetectActiveWeaponThinkPreContext ctx);
public delegate void OnCTriggerActiveWeaponDetectActiveWeaponThinkPostDelegate(ref CTriggerActiveWeaponDetectActiveWeaponThinkPostContext ctx);

public interface ICTriggerActiveWeaponDetectActiveWeaponThinkHook
{
    public event OnCTriggerActiveWeaponDetectActiveWeaponThinkPreDelegate Pre;
    public event OnCTriggerActiveWeaponDetectActiveWeaponThinkPostDelegate Post;

    public void Invoke(CTriggerActiveWeaponDetect schemaObject);
}