/******************************************************************
 *
 * This feature is inspired from CounterStrikeSharp.
 * You can find the source code used by us in the following files:
 *
 * https://github.com/roflmuffin/CounterStrikeSharp/blob/a87bd25b48ff1407a71cfdce3222f5f55c8a2e0b/src/core/managers/voice_manager.h#L26
 * https://github.com/roflmuffin/CounterStrikeSharp/blob/a87bd25b48ff1407a71cfdce3222f5f55c8a2e0b/src/core/managers/voice_manager.cpp#L30
 *
 ******************************************************************/

using System.Globalization;
using SwiftlyS2.Core.Events;
using SwiftlyS2.Core.Extensions;
using SwiftlyS2.Core.Players;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Memory;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.Services;

internal class CoreVoiceService : IDisposable
{
    private delegate byte SetClientListening( nint pThis, int receiver, int sender, byte listen );
    private delegate void ClientCommand( nint pThis, int slot, nint args );
    private delegate void ClientVoice( nint pThis, int slot );

    private readonly ISwiftlyCore core;

    private IUnmanagedFunction<SetClientListening>? setClientListening;
    private Guid setClientListeningGuid;
    private IUnmanagedFunction<ClientCommand>? clientCommand;
    private Guid clientCommandGuid;
    private IUnmanagedFunction<ClientVoice>? clientVoice;
    private Guid clientVoiceGuid;

    public CoreVoiceService( ISwiftlyCore core )
    {
        this.core = core;

        HookSetClientListening();
        HookClientCommand();
        HookClientVoice();
    }

    private static bool ResolveListen( Player receiver, Player sender, bool listen )
    {
        var senderFlags = sender.VoiceFlags;
        var receiverFlags = receiver.VoiceFlags;
        var listenOverride = receiver.GetListenOverride(sender.Slot);

        if (receiver.IsMutedBySelf(sender.Slot) || senderFlags.HasFlag(VoiceFlagValue.Muted) || listenOverride == ListenOverride.Mute)
        {
            return false;
        }

        if (listenOverride == ListenOverride.Hear || senderFlags.HasFlag(VoiceFlagValue.All) || receiverFlags.HasFlag(VoiceFlagValue.ListenAll))
        {
            return true;
        }

        if (senderFlags.HasFlag(VoiceFlagValue.Team) || receiverFlags.HasFlag(VoiceFlagValue.ListenTeam))
        {
            var senderController = sender.Controller;
            var receiverController = receiver.Controller;
            if (senderController.IsValid && receiverController.IsValid)
            {
                return senderController.TeamNum == receiverController.TeamNum;
            }
        }

        return listen;
    }

    private void HookSetClientListening()
    {
        var offset = core.GameData.GetOffset("IVEngineServer2::SetClientListening");
        setClientListening = core.Memory.GetUnmanagedFunctionByVTable<SetClientListening>(core.Memory.GetVTableAddress(Library.Engine, "CEngineServer")!.Value, offset);
        setClientListeningGuid = setClientListening.AddHook(next =>
        {
            return ( pThis, receiverSlot, senderSlot, listen ) =>
            {
                var receiver = PlayerManagerService.GetPlayerObject(receiverSlot);
                var sender = PlayerManagerService.GetPlayerObject(senderSlot);
                if (receiver is not null && sender is not null)
                {
                    listen = ResolveListen(receiver, sender, listen != 0) ? (byte)1 : (byte)0;
                }

                return next()(pThis, receiverSlot, senderSlot, listen);
            };
        });
    }

    private void HookClientCommand()
    {
        var offset = core.GameData.GetOffset("IServerGameClients::ClientCommand");
        clientCommand = core.Memory.GetUnmanagedFunctionByVTable<ClientCommand>(core.Memory.GetVTableAddress(Library.Server, "CSource2GameClients")!.Value, offset);
        clientCommandGuid = clientCommand.AddHook(next =>
        {
            return ( pThis, slot, args ) =>
            {
                unsafe
                {
                    if (args != nint.Zero && PlayerManagerService.GetPlayerObject(slot) is { } receiver)
                    {
                        ref var command = ref args.AsRef<CCommand>();
                        if (command.ArgC > 1 && command.Arg(0) == "vban" &&
                            uint.TryParse(command.Arg(1), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var mask))
                        {
                            receiver.SetSelfMutesLowDWord(mask);
                        }
                    }
                }

                next()(pThis, slot, args);
            };
        });
    }

    private void HookClientVoice()
    {
        var offset = core.GameData.GetOffset("IServerGameClients::ClientVoice");
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
        setClientListening?.RemoveHook(setClientListeningGuid);
        clientCommand?.RemoveHook(clientCommandGuid);
        clientVoice?.RemoveHook(clientVoiceGuid);
        setClientListening = null;
        clientCommand = null;
        clientVoice = null;
    }
}
