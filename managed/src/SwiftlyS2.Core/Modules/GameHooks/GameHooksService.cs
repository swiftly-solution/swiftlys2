using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHooksService : IGameHooks
{
    internal readonly GameHookItems ItemsHook = new();
    internal readonly GameHookMovement MovementHook = new();
    internal readonly GameHookPawn PawnHook = new();
    internal readonly GameHookWeapons WeaponsHook = new();
    internal readonly GameHookController ControllerHook = new();

    public IGameHookItems Items => ItemsHook;
    public IGameHookMovement Movement => MovementHook;
    public IGameHookPawn Pawn => PawnHook;
    public IGameHookWeapons Weapons => WeaponsHook;
    public IGameHookController Controller => ControllerHook;
}
