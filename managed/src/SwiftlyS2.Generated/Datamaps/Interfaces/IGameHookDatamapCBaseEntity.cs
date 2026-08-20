namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookDatamapCBaseEntity
{
    public ICBaseEntityClearNavIgnoreContentsThinkHook ClearNavIgnoreContentsThink { get; }
    public ICBaseEntityFakeScriptThinkFuncHook FakeScriptThinkFunc { get; }
    public ICBaseEntitySUB_CallUseToggleHook SUB_CallUseToggle { get; }
    public ICBaseEntitySUB_DoNothingHook SUB_DoNothing { get; }
    public ICBaseEntitySUB_KillSelfHook SUB_KillSelf { get; }
    public ICBaseEntitySUB_RemoveHook SUB_Remove { get; }
}