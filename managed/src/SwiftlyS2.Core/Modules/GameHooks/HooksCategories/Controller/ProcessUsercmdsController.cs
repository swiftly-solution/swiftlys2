using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class ProcessUsercmdsHook( GameHooksService owner ) : IProcessUsercmdsHook
{
    private event OnProcessUsercmdsPreDelegate? _Pre;
    private event OnProcessUsercmdsPostDelegate? _Post;

    public event OnProcessUsercmdsPreDelegate Pre
    {
        add
        {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.ProcessUsercmds, owner);
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.ProcessUsercmds, owner);
        }
    }

    public event OnProcessUsercmdsPostDelegate Post
    {
        add
        {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.ProcessUsercmds, owner);
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.ProcessUsercmds, owner);
        }
    }

    public void InvokePre(ref ProcessUsercmdsPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref ProcessUsercmdsPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.ProcessUsercmds, owner);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.ProcessUsercmds, owner);
    }
}
