using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.Schemas;
using SwiftlyS2.Shared.SchemaDefinitions;
using EndReason = SwiftlyS2.Shared.Natives.RoundEndReason;
using SwiftlyS2.Core.EntitySystem;

namespace SwiftlyS2.Core.SchemaDefinitions;

internal partial class CCSGameRulesImpl : CCSGameRules
{
    public GamePhase GamePhaseEnum {
        get => (GamePhase)GamePhase;
        set => GamePhase = (int)value;
    }

    public T? FindPickerEntity<T>( CBasePlayerController controller ) where T : class, ISchemaClass<T>
    {
        var entityAddress = GameFunctions.FindPickerEntity(Address, controller.Address);
        return EntityManager.GetEntityByAddress(entityAddress) as T;
    }

    public void TerminateRound( EndReason reason, float delay )
    {
        GameFunctions.TerminateRound(Address, (uint)reason, delay, 0);
    }

    public void TerminateRound( EndReason reason, float delay, uint teamId, uint unk01 )
    {
        GameFunctions.TerminateRound(Address, (uint)reason, delay, teamId);
    }
    
    public void GoToIntermission(bool bAbortedMatch)
    {
        GameFunctions.GoToIntermission(Address, bAbortedMatch);
    }

    public ref CViewVectors GetViewVectors()
    {
        unsafe
        {
            return ref *GameFunctions.CGameRules_GetViewVectors(Address);
        }
    }
}