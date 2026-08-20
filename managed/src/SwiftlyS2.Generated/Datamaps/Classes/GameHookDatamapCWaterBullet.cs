using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCWaterBullet : IGameHookDatamapCWaterBullet
{
    internal readonly CWaterBulletBulletThinkHook CWaterBulletBulletThinkHook = new();

    public ICWaterBulletBulletThinkHook BulletThink => CWaterBulletBulletThinkHook;

    internal void UnregisterListeners()
    {
        CWaterBulletBulletThinkHook.UnregisterListeners();
    }
}