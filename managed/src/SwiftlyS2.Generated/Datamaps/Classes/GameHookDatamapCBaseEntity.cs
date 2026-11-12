using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCBaseEntity(GameHooksService owner) : IGameHookDatamapCBaseEntity
{
    internal readonly CBaseEntityClearNavIgnoreContentsThinkHook CBaseEntityClearNavIgnoreContentsThinkHook = new(owner);
    internal readonly CBaseEntityFakeScriptThinkFuncHook CBaseEntityFakeScriptThinkFuncHook = new(owner);
    internal readonly CBaseEntitySUB_CallUseToggleHook CBaseEntitySUB_CallUseToggleHook = new(owner);
    internal readonly CBaseEntitySUB_DoNothingHook CBaseEntitySUB_DoNothingHook = new(owner);
    internal readonly CBaseEntitySUB_KillSelfHook CBaseEntitySUB_KillSelfHook = new(owner);
    internal readonly CBaseEntitySUB_RemoveHook CBaseEntitySUB_RemoveHook = new(owner);

    public ICBaseEntityClearNavIgnoreContentsThinkHook ClearNavIgnoreContentsThink => CBaseEntityClearNavIgnoreContentsThinkHook;
    public ICBaseEntityFakeScriptThinkFuncHook FakeScriptThinkFunc => CBaseEntityFakeScriptThinkFuncHook;
    public ICBaseEntitySUB_CallUseToggleHook SUB_CallUseToggle => CBaseEntitySUB_CallUseToggleHook;
    public ICBaseEntitySUB_DoNothingHook SUB_DoNothing => CBaseEntitySUB_DoNothingHook;
    public ICBaseEntitySUB_KillSelfHook SUB_KillSelf => CBaseEntitySUB_KillSelfHook;
    public ICBaseEntitySUB_RemoveHook SUB_Remove => CBaseEntitySUB_RemoveHook;

    internal void UnregisterListeners()
    {
        CBaseEntityClearNavIgnoreContentsThinkHook.UnregisterListeners();
        CBaseEntityFakeScriptThinkFuncHook.UnregisterListeners();
        CBaseEntitySUB_CallUseToggleHook.UnregisterListeners();
        CBaseEntitySUB_DoNothingHook.UnregisterListeners();
        CBaseEntitySUB_KillSelfHook.UnregisterListeners();
        CBaseEntitySUB_RemoveHook.UnregisterListeners();
    }
}