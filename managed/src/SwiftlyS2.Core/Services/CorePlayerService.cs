using SwiftlyS2.Core.Events;
using SwiftlyS2.Core.Players;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Memory;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SteamAPI;

namespace SwiftlyS2.Core.Services;

internal class CorePlayerService : IDisposable
{
    private delegate void ClientSettingsChanged( nint pThis, int slot );

    private readonly ISwiftlyCore core;

    private IUnmanagedFunction<ClientSettingsChanged>? clientSettingsChanged;
    private Guid clientSettingsChangedGuid;
    private readonly Guid firstSpawnHookGuid;
    private Callback<ValidateAuthTicketResponse_t>? authTicketResponse;

    public CorePlayerService( ISwiftlyCore core )
    {
        this.core = core;

        HookClientSettingsChanged();
        core.Event.OnSteamAPIActivated += OnSteamAPIActivated;

        firstSpawnHookGuid = core.GameEvent.HookPost<EventPlayerSpawn>(@event =>
        {
            if (@event.UserIdPlayer is Player player) player.IsFirstSpawn = false;
            return HookResult.Continue;
        });
    }

    private void HookClientSettingsChanged()
    {
        var offset = core.GameData.GetOffset("IServerGameClients::ClientSettingsChanged");
        clientSettingsChanged = core.Memory.GetUnmanagedFunctionByVTable<ClientSettingsChanged>(core.Memory.GetVTableAddress(Library.Server, "CSource2GameClients")!.Value, offset);
        clientSettingsChangedGuid = clientSettingsChanged.AddHook(next =>
        {
            return ( pThis, slot ) =>
            {
                next()(pThis, slot);
                EventPublisher.InvokeOnClientSettingsChanged(slot);
            };
        });
    }

    private void OnSteamAPIActivated()
    {
        authTicketResponse?.Dispose();
        authTicketResponse = Callback<ValidateAuthTicketResponse_t>.Create(OnValidateAuthTicket);
    }

    private static void OnValidateAuthTicket( ValidateAuthTicketResponse_t response )
    {
        var steamId = response.m_SteamID.GetSteamID64();
        foreach (var obj in PlayerManagerService.PlayerObjects.Values)
        {
            if (obj is Player player && player.UnauthorizedSteamID == steamId)
            {
                player.ChangeAuthorizationState(response.m_eAuthSessionResponse == EAuthSessionResponse.k_EAuthSessionResponseOK);
                return;
            }
        }
    }

    public void Dispose()
    {
        core.Event.OnSteamAPIActivated -= OnSteamAPIActivated;
        authTicketResponse?.Dispose();
        authTicketResponse = null;
        core.GameEvent.Unhook(firstSpawnHookGuid);
        clientSettingsChanged?.RemoveHook(clientSettingsChangedGuid);
        clientSettingsChanged = null;
    }
}
