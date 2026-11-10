using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class TakeDamageEntityHook : ITakeDamageEntityHook
{
    private OnTakeDamageEntityPreDelegate? _Pre;
    private OnTakeDamageEntityPostDelegate? _Post;

    public event OnTakeDamageEntityPreDelegate Pre
    {
        add
        {
            GameHooksPublisher.UpdateListener(HookListener.TakeDamage, ref _Pre, value, add: true);
        }
        remove
        {
            GameHooksPublisher.UpdateListener(HookListener.TakeDamage, ref _Pre, value, add: false);
        }
    }

    public event OnTakeDamageEntityPostDelegate Post
    {
        add
        {
            GameHooksPublisher.UpdateListener(HookListener.TakeDamage, ref _Post, value, add: true);
        }
        remove
        {
            GameHooksPublisher.UpdateListener(HookListener.TakeDamage, ref _Post, value, add: false);
        }
    }

    public void InvokePre( ref TakeDamageEntityPreContext ctx ) => Volatile.Read(ref _Pre)?.Invoke(ref ctx);
    public void InvokePost( ref TakeDamageEntityPostContext ctx ) => Volatile.Read(ref _Post)?.Invoke(ref ctx);

    public void UnregisterListeners()
    {
        GameHooksPublisher.ClearListeners(HookListener.TakeDamage, ref _Pre);
        GameHooksPublisher.ClearListeners(HookListener.TakeDamage, ref _Post);
    }
}
