using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class CheckParametersMovementHook( GameHooksService owner ) : ICheckParametersMovementHook
{
    internal event OnCheckParametersMovementPreDelegate? _Pre;
    internal event OnCheckParametersMovementPostDelegate? _Post;

    public event OnCheckParametersMovementPreDelegate Pre
    {
        add
        {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.CheckParameters, owner);
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.CheckParameters, owner);
        }
    }

    public event OnCheckParametersMovementPostDelegate Post
    {
        add
        {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.CheckParameters, owner);
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.CheckParameters, owner);
        }
    }

    public void InvokePre(ref CheckParametersMovementPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CheckParametersMovementPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.CheckParameters, owner);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.CheckParameters, owner);
    }
}
