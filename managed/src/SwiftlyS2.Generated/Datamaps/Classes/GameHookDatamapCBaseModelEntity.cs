using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCBaseModelEntity : IGameHookDatamapCBaseModelEntity
{
    internal readonly CBaseModelEntityProcessSceneEventsThinkHook CBaseModelEntityProcessSceneEventsThinkHook = new();
    internal readonly CBaseModelEntitySUB_DissolveIfUncarriedHook CBaseModelEntitySUB_DissolveIfUncarriedHook = new();
    internal readonly CBaseModelEntitySUB_FadeOutHook CBaseModelEntitySUB_FadeOutHook = new();
    internal readonly CBaseModelEntitySUB_PerformShadowFadeInHook CBaseModelEntitySUB_PerformShadowFadeInHook = new();
    internal readonly CBaseModelEntitySUB_PerformShadowFadeOutHook CBaseModelEntitySUB_PerformShadowFadeOutHook = new();
    internal readonly CBaseModelEntitySUB_StartFadeOutHook CBaseModelEntitySUB_StartFadeOutHook = new();
    internal readonly CBaseModelEntitySUB_StartFadeOutInstantHook CBaseModelEntitySUB_StartFadeOutInstantHook = new();
    internal readonly CBaseModelEntitySUB_StartShadowFadeInHook CBaseModelEntitySUB_StartShadowFadeInHook = new();
    internal readonly CBaseModelEntitySUB_StartShadowFadeOutHook CBaseModelEntitySUB_StartShadowFadeOutHook = new();
    internal readonly CBaseModelEntitySUB_StopShadowFadeHook CBaseModelEntitySUB_StopShadowFadeHook = new();

    public ICBaseModelEntityProcessSceneEventsThinkHook ProcessSceneEventsThink => CBaseModelEntityProcessSceneEventsThinkHook;
    public ICBaseModelEntitySUB_DissolveIfUncarriedHook SUB_DissolveIfUncarried => CBaseModelEntitySUB_DissolveIfUncarriedHook;
    public ICBaseModelEntitySUB_FadeOutHook SUB_FadeOut => CBaseModelEntitySUB_FadeOutHook;
    public ICBaseModelEntitySUB_PerformShadowFadeInHook SUB_PerformShadowFadeIn => CBaseModelEntitySUB_PerformShadowFadeInHook;
    public ICBaseModelEntitySUB_PerformShadowFadeOutHook SUB_PerformShadowFadeOut => CBaseModelEntitySUB_PerformShadowFadeOutHook;
    public ICBaseModelEntitySUB_StartFadeOutHook SUB_StartFadeOut => CBaseModelEntitySUB_StartFadeOutHook;
    public ICBaseModelEntitySUB_StartFadeOutInstantHook SUB_StartFadeOutInstant => CBaseModelEntitySUB_StartFadeOutInstantHook;
    public ICBaseModelEntitySUB_StartShadowFadeInHook SUB_StartShadowFadeIn => CBaseModelEntitySUB_StartShadowFadeInHook;
    public ICBaseModelEntitySUB_StartShadowFadeOutHook SUB_StartShadowFadeOut => CBaseModelEntitySUB_StartShadowFadeOutHook;
    public ICBaseModelEntitySUB_StopShadowFadeHook SUB_StopShadowFade => CBaseModelEntitySUB_StopShadowFadeHook;

    internal void UnregisterListeners()
    {
        CBaseModelEntityProcessSceneEventsThinkHook.UnregisterListeners();
        CBaseModelEntitySUB_DissolveIfUncarriedHook.UnregisterListeners();
        CBaseModelEntitySUB_FadeOutHook.UnregisterListeners();
        CBaseModelEntitySUB_PerformShadowFadeInHook.UnregisterListeners();
        CBaseModelEntitySUB_PerformShadowFadeOutHook.UnregisterListeners();
        CBaseModelEntitySUB_StartFadeOutHook.UnregisterListeners();
        CBaseModelEntitySUB_StartFadeOutInstantHook.UnregisterListeners();
        CBaseModelEntitySUB_StartShadowFadeInHook.UnregisterListeners();
        CBaseModelEntitySUB_StartShadowFadeOutHook.UnregisterListeners();
        CBaseModelEntitySUB_StopShadowFadeHook.UnregisterListeners();
    }
}