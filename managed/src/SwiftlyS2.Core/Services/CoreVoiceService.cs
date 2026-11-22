/******************************************************************
 *
 * This feature is inspired from CounterStrikeSharp.
 * You can find the source code used by us in the following files:
 *
 * https://github.com/roflmuffin/CounterStrikeSharp/blob/a87bd25b48ff1407a71cfdce3222f5f55c8a2e0b/src/core/managers/voice_manager.h#L26
 * https://github.com/roflmuffin/CounterStrikeSharp/blob/a87bd25b48ff1407a71cfdce3222f5f55c8a2e0b/src/core/managers/voice_manager.cpp#L30
 *
 ******************************************************************/

using SwiftlyS2.Core.Events;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Memory;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.Services;

internal class CoreVoiceService : IDisposable
{
    private delegate void ClientVoice( nint pThis, int slot );

    private readonly ISwiftlyCore core;

    private IUnmanagedFunction<ClientVoice>? clientVoice;
    private Guid clientVoiceGuid;

    public CoreVoiceService( ISwiftlyCore core )
    {
        this.core = core;

        HookClientVoice();
    }

    private void HookClientVoice()
    {
        var offset = core.GameData.GetOffset("CSource2GameClients::ClientVoice");
        clientVoice = core.Memory.GetUnmanagedFunctionByVTable<ClientVoice>(core.Memory.GetVTableAddress(Library.Server, "CSource2GameClients")!.Value, offset);
        clientVoiceGuid = clientVoice.AddHook(next =>
        {
            return ( pThis, slot ) =>
            {
                next()(pThis, slot);
                EventPublisher.InvokeOnClientVoice(slot);
            };
        });
    }

    public void Dispose()
    {
        clientVoice?.RemoveHook(clientVoiceGuid);
        clientVoice = null;
    }
}
