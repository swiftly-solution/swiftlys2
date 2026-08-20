using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CColorCorrectionFadeInThinkPreContext
{
    public CColorCorrection SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CColorCorrectionFadeInThinkPostContext
{
    public CColorCorrection SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCColorCorrectionFadeInThinkPreDelegate(ref CColorCorrectionFadeInThinkPreContext ctx);
public delegate void OnCColorCorrectionFadeInThinkPostDelegate(ref CColorCorrectionFadeInThinkPostContext ctx);

public interface ICColorCorrectionFadeInThinkHook
{
    public event OnCColorCorrectionFadeInThinkPreDelegate Pre;
    public event OnCColorCorrectionFadeInThinkPostDelegate Post;

    public void Invoke(CColorCorrection schemaObject);
}