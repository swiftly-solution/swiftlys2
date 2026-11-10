using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class CategorizePositionMovementHook( GameHooksService owner ) : ICategorizePositionMovementHook
{
    internal event OnCategorizePositionMovementPreDelegate? _Pre;
    internal event OnCategorizePositionMovementPostDelegate? _Post;

    public event OnCategorizePositionMovementPreDelegate Pre
    {
        add
        {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.CategorizePosition, owner);
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.CategorizePosition, owner);
        }
    }

    public event OnCategorizePositionMovementPostDelegate Post
    {
        add
        {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.CategorizePosition, owner);
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.CategorizePosition, owner);
        }
    }

    public void InvokePre(ref CategorizePositionMovementPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CategorizePositionMovementPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.CategorizePosition, owner);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.CategorizePosition, owner);
    }
}
