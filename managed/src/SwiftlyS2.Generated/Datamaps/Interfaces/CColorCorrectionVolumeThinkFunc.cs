using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CColorCorrectionVolumeThinkFuncPreContext
{
    public CColorCorrectionVolume SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CColorCorrectionVolumeThinkFuncPostContext
{
    public CColorCorrectionVolume SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCColorCorrectionVolumeThinkFuncPreDelegate(ref CColorCorrectionVolumeThinkFuncPreContext ctx);
public delegate void OnCColorCorrectionVolumeThinkFuncPostDelegate(ref CColorCorrectionVolumeThinkFuncPostContext ctx);

public interface ICColorCorrectionVolumeThinkFuncHook
{
    public event OnCColorCorrectionVolumeThinkFuncPreDelegate Pre;
    public event OnCColorCorrectionVolumeThinkFuncPostDelegate Post;

    public void Invoke(CColorCorrectionVolume schemaObject);
}