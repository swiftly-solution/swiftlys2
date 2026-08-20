namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookDatamapCBaseModelEntity
{
    public ICBaseModelEntityProcessSceneEventsThinkHook ProcessSceneEventsThink { get; }
    public ICBaseModelEntitySUB_DissolveIfUncarriedHook SUB_DissolveIfUncarried { get; }
    public ICBaseModelEntitySUB_FadeOutHook SUB_FadeOut { get; }
    public ICBaseModelEntitySUB_PerformShadowFadeInHook SUB_PerformShadowFadeIn { get; }
    public ICBaseModelEntitySUB_PerformShadowFadeOutHook SUB_PerformShadowFadeOut { get; }
    public ICBaseModelEntitySUB_StartFadeOutHook SUB_StartFadeOut { get; }
    public ICBaseModelEntitySUB_StartFadeOutInstantHook SUB_StartFadeOutInstant { get; }
    public ICBaseModelEntitySUB_StartShadowFadeInHook SUB_StartShadowFadeIn { get; }
    public ICBaseModelEntitySUB_StartShadowFadeOutHook SUB_StartShadowFadeOut { get; }
    public ICBaseModelEntitySUB_StopShadowFadeHook SUB_StopShadowFade { get; }
}