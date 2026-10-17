using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GetItemInLoadoutHook : IGetItemInLoadoutHook
{
    internal event OnGetItemInLoadoutPreDelegate? _Pre;
    internal event OnGetItemInLoadoutPostDelegate? _Post;

    public event OnGetItemInLoadoutPreDelegate Pre
    {
        add
        {
            ThrowIfFollowingServerGuidelines();
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.GetItemInLoadout);
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.GetItemInLoadout);
        }
    }

    public event OnGetItemInLoadoutPostDelegate Post
    {
        add
        {
            ThrowIfFollowingServerGuidelines();
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.GetItemInLoadout);
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.GetItemInLoadout);
        }
    }

    public void InvokePre(ref GetItemInLoadoutPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref GetItemInLoadoutPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.GetItemInLoadout);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.GetItemInLoadout);
    }

    private static void ThrowIfFollowingServerGuidelines()
    {
        if (NativeServerHelpers.IsFollowingServerGuidelines())
        {
            throw new InvalidOperationException("Cannot hook GetItemInLoadout while \"FollowCS2ServerGuidelines\" is enabled.\n\tTo use this operation, disable the option in core.jsonc.");
        }
    }
}
