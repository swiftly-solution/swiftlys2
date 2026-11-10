using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GetItemInLoadoutHook : IGetItemInLoadoutHook
{
    private OnGetItemInLoadoutPreDelegate? _Pre;
    private OnGetItemInLoadoutPostDelegate? _Post;

    public event OnGetItemInLoadoutPreDelegate Pre
    {
        add
        {
            ThrowIfFollowingServerGuidelines();
            GameHooksPublisher.UpdateListener(HookListener.GetItemInLoadout, ref _Pre, value, add: true);
        }
        remove
        {
            GameHooksPublisher.UpdateListener(HookListener.GetItemInLoadout, ref _Pre, value, add: false);
        }
    }

    public event OnGetItemInLoadoutPostDelegate Post
    {
        add
        {
            ThrowIfFollowingServerGuidelines();
            GameHooksPublisher.UpdateListener(HookListener.GetItemInLoadout, ref _Post, value, add: true);
        }
        remove
        {
            GameHooksPublisher.UpdateListener(HookListener.GetItemInLoadout, ref _Post, value, add: false);
        }
    }

    public void InvokePre(ref GetItemInLoadoutPreContext ctx) => Volatile.Read(ref _Pre)?.Invoke(ref ctx);
    public void InvokePost(ref GetItemInLoadoutPostContext ctx) => Volatile.Read(ref _Post)?.Invoke(ref ctx);

    public void UnregisterListeners()
    {
        GameHooksPublisher.ClearListeners(HookListener.GetItemInLoadout, ref _Pre);
        GameHooksPublisher.ClearListeners(HookListener.GetItemInLoadout, ref _Post);
    }

    private static void ThrowIfFollowingServerGuidelines()
    {
        if (NativeServerHelpers.IsFollowingServerGuidelines())
        {
            throw new InvalidOperationException("Cannot hook GetItemInLoadout while \"FollowCS2ServerGuidelines\" is enabled.\n\tTo use this operation, disable the option in core.jsonc.");
        }
    }
}
