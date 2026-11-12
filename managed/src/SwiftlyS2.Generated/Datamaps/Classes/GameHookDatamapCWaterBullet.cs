using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCWaterBullet(GameHooksService owner) : IGameHookDatamapCWaterBullet
{
    internal readonly CWaterBulletBulletThinkHook CWaterBulletBulletThinkHook = new(owner);

    public ICWaterBulletBulletThinkHook BulletThink => CWaterBulletBulletThinkHook;

    internal void UnregisterListeners()
    {
        CWaterBulletBulletThinkHook.UnregisterListeners();
    }
}