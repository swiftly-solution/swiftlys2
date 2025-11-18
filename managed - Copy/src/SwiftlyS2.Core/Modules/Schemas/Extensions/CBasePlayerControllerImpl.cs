using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.SchemaDefinitions;

internal partial class CBasePlayerControllerImpl : CBasePlayerController
{
    public void SetPawn(CBasePlayerPawn? pawn)
    {
        nint? handle = pawn?.Address;
        GameFunctions.SetPlayerControllerPawn(Address, handle ?? IntPtr.Zero, true, false, false, false);
    }
}