using SwiftlyS2.Core.Natives;

namespace SwiftlyS2.Core.SchemaDefinitions;

internal partial class CCSPlayerControllerImpl
{
    public void Respawn()
    {
        var pawn = PlayerPawn;
        if (pawn is { IsValid: false }) return;

        SetPawn(pawn.Value!);
        GameFunctions.CCSPlayerController_Respawn(Address);
    }
}