using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CColorCorrectionFadeOutThinkPreContext
{
    public CColorCorrection SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CColorCorrectionFadeOutThinkPostContext
{
    public CColorCorrection SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCColorCorrectionFadeOutThinkPreDelegate(ref CColorCorrectionFadeOutThinkPreContext ctx);
public delegate void OnCColorCorrectionFadeOutThinkPostDelegate(ref CColorCorrectionFadeOutThinkPostContext ctx);

public interface ICColorCorrectionFadeOutThinkHook
{
    public event OnCColorCorrectionFadeOutThinkPreDelegate Pre;
    public event OnCColorCorrectionFadeOutThinkPostDelegate Post;

    public void Invoke(CColorCorrection schemaObject);
}