using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class ProcessUsercmdsHook : IProcessUsercmdsHook
{
    private OnProcessUsercmdsPreDelegate? _Pre;
    private OnProcessUsercmdsPostDelegate? _Post;

    public event OnProcessUsercmdsPreDelegate Pre
    {
        add
        {
            GameHooksPublisher.UpdateListener(HookListener.ProcessUsercmds, ref _Pre, value, add: true);
        }
        remove
        {
            GameHooksPublisher.UpdateListener(HookListener.ProcessUsercmds, ref _Pre, value, add: false);
        }
    }

    public event OnProcessUsercmdsPostDelegate Post
    {
        add
        {
            GameHooksPublisher.UpdateListener(HookListener.ProcessUsercmds, ref _Post, value, add: true);
        }
        remove
        {
            GameHooksPublisher.UpdateListener(HookListener.ProcessUsercmds, ref _Post, value, add: false);
        }
    }

    public void InvokePre(ref ProcessUsercmdsPreContext ctx) => Volatile.Read(ref _Pre)?.Invoke(ref ctx);
    public void InvokePost(ref ProcessUsercmdsPostContext ctx) => Volatile.Read(ref _Post)?.Invoke(ref ctx);

    public void UnregisterListeners()
    {
        GameHooksPublisher.ClearListeners(HookListener.ProcessUsercmds, ref _Pre);
        GameHooksPublisher.ClearListeners(HookListener.ProcessUsercmds, ref _Post);
    }
}
