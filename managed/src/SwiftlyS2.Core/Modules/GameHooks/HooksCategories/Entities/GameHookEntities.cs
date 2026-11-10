using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookEntities( GameHooksService owner ) : IGameHookEntities
{
    internal readonly TakeDamageEntityHook TakeDamageHook = new(owner);
    internal readonly StartTouchEntityHook StartTouchHook = new(owner);
    internal readonly TouchEntityHook TouchHook = new(owner);
    internal readonly EndTouchEntityHook EndTouchHook = new(owner);
    internal readonly AcceptInputEntityHook AcceptInputHook = new(owner);
    internal readonly FireOutputEntityHook FireOutputHook = new(owner);

    public ITakeDamageEntityHook TakeDamage => TakeDamageHook;
    public IStartTouchEntityHook StartTouch => StartTouchHook;
    public ITouchEntityHook Touch => TouchHook;
    public IEndTouchEntityHook EndTouch => EndTouchHook;
    public IAcceptInputEntityHook AcceptInput => AcceptInputHook;
    public IFireOutputEntityHook FireOutput => FireOutputHook;
}
