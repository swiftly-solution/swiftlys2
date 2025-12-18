using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Players;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.SchemaDefinitions;

internal partial class CBasePlayerControllerImpl : CBasePlayerController
{
    public void SetPawn( CBasePlayerPawn? pawn )
    {
        nint? handle = pawn?.Address;
        GameFunctions.SetPlayerControllerPawn(Address, handle ?? IntPtr.Zero, true, false, false, false);
    }

    public IPlayer? ToPlayer()
    {
        if (!IsValid) return null;
        var player = new Player((int)(Index - 1));
        if (player is not { IsValid: true } || !NativePlayerManager.IsPlayerOnline(player.PlayerID)) return null;
        return player;
    }
}