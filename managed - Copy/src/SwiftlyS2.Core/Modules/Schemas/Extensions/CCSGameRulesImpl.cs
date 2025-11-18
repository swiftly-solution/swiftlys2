using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.Schemas;

namespace SwiftlyS2.Core.SchemaDefinitions;

internal partial class CCSGameRulesImpl : CCSGameRules
{
    public T? FindPickerEntity<T>( CBasePlayerController controller ) where T : ISchemaClass<T>
    {
        CBaseEntity ent = new CBaseEntityImpl(GameFunctions.FindPickerEntity(Address, controller.Address));
        return ent.As<T>();
    }

    public void TerminateRound( RoundEndReason reason, float delay )
    {
        GameFunctions.TerminateRound(Address, (uint)reason, delay);
    }
}