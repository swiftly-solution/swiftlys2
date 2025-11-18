using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.SchemaDefinitions;

internal partial class CBasePlayerPawnImpl : CBasePlayerPawn
{
    public void CommitSuicide(bool explode, bool force)
    {
        GameFunctions.PawnCommitSuicide(Address, explode, force);
    }
}