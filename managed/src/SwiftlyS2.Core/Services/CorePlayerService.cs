using SwiftlyS2.Core.Events;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Memory;

namespace SwiftlyS2.Core.Services;

internal class CorePlayerService : IDisposable
{
    private delegate void ClientSettingsChanged( nint pThis, int slot );

    private readonly ISwiftlyCore core;

    private IUnmanagedFunction<ClientSettingsChanged>? clientSettingsChanged;
    private Guid clientSettingsChangedGuid;

    public CorePlayerService( ISwiftlyCore core )
    {
        this.core = core;

        HookClientSettingsChanged();
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

    public void Dispose()
    {
        clientSettingsChanged?.RemoveHook(clientSettingsChangedGuid);
        clientSettingsChanged = null;
    }
}
