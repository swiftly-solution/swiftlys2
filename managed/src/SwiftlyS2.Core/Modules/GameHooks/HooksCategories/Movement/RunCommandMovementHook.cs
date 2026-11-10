using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class RunCommandMovementHook : IRunCommandMovementHook
{
    private OnRunCommandMovementPreDelegate? _Pre;
    private OnRunCommandMovementPostDelegate? _Post;

    public event OnRunCommandMovementPreDelegate Pre
    {
        add => GameHooksPublisher.UpdateListener(HookListener.RunCommand, ref _Pre, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.RunCommand, ref _Pre, value, add: false);
    }

    public event OnRunCommandMovementPostDelegate Post
    {
        add => GameHooksPublisher.UpdateListener(HookListener.RunCommand, ref _Post, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.RunCommand, ref _Post, value, add: false);
    }

    public void InvokePre(ref RunCommandMovementPreContext ctx) => Volatile.Read(ref _Pre)?.Invoke(ref ctx);
    public void InvokePost(ref RunCommandMovementPostContext ctx) => Volatile.Read(ref _Post)?.Invoke(ref ctx);

    public bool HasPreListeners => Volatile.Read(ref _Pre) != null;
    public bool HasPostListeners => Volatile.Read(ref _Post) != null;

    public void UnregisterListeners()
    {
        GameHooksPublisher.ClearListeners(HookListener.RunCommand, ref _Pre);
        GameHooksPublisher.ClearListeners(HookListener.RunCommand, ref _Post);
    }
}
