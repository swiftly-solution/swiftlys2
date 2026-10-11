using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using SwiftlyS2.Core.Events;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Memory;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.SteamAPI;

namespace SwiftlyS2.Core.Services;

internal class CoreHookService : IDisposable
{
    private readonly ISwiftlyCore core;
    private readonly ILogger<CoreHookService> logger;
    public CoreHookService( ILogger<CoreHookService> logger, ISwiftlyCore core )
    {
        this.logger = logger;
        this.core = core;

        HookExecuteCommand();
        HookSteamServerAPIActivated();
    }

    /*
        Original function in engine2.dll: __int64 sub_1C0CD0(__int64 a1, int a2, unsigned int a3, ...)
        This is a variadic function, but we only need the first two variable arguments (v55, v57)

        __int64 sub_1C0CD0(__int64 a1, int a2, unsigned int a3, ...)
        {
            ...

            va_list va; // [rsp+D28h] [rbp+D28h]
            __int64 v55; // [rsp+E28h] [rbp+D28h] BYREF
            va_list va1; // [rsp+E28h] [rbp+D28h]

            ...

            va_start(va1, a3);
            va_start(va, a3);
            v55 = va_arg(va1, _QWORD);
            v57 = va_arg(va1, _QWORD);

            ...
        }

        So we model it as a fixed 5-parameter function for interop purposes
    */
    private delegate nint ExecuteCommand( nint a1, int a2, uint a3, nint a4, nint a5 );
    private delegate void SteamServerAPIActivated( nint pServer );

    private IUnmanagedFunction<ExecuteCommand>? executeCommand;
    private Guid executeCommandGuid;
    private IUnmanagedFunction<SteamServerAPIActivated>? steamServerAPIActivated;
    private Guid steamServerAPIActivatedGuid;

    internal void HookExecuteCommand()
    {
        var address = core.GameData.GetSignature("Cmd_ExecuteCommand");

        logger.LogInformation("Hooking Cmd_ExecuteCommand at {Address:X}", address);

        executeCommand = core.Memory.GetUnmanagedFunctionByAddress<ExecuteCommand>(address);
        executeCommandGuid = executeCommand.AddHook(( next ) =>
        {
            return ( a1, a2, a3, a4, a5 ) =>
            {
                unsafe
                {
                    if (a5 != nint.Zero)
                    {
                        ref var command = ref Unsafe.AsRef<CCommand>((void*)a5);
                        var @eventPre = new OnCommandExecuteHookEvent(ref command, HookMode.Pre);
                        EventPublisher.InvokeOnCommandExecuteHook(@eventPre);

                        if (@eventPre.Result == HookResult.Stop || @eventPre.Result == HookResult.CancelOriginal)
                        {
                            return 0;
                        }

                        var result = next()(a1, a2, a3, a4, a5);

                        var @eventPost = new OnCommandExecuteHookEvent(ref command, HookMode.Post);
                        EventPublisher.InvokeOnCommandExecuteHook(@eventPost);
                        return result;
                    }
                    return next()(a1, a2, a3, a4, a5);
                }
            };
        });
    }

    internal void UnhookExecuteCommand()
    {
        if (executeCommand == null) return;
        executeCommand.RemoveHook(executeCommandGuid);
        executeCommand = null;
    }

    internal void HookSteamServerAPIActivated()
    {
        var offset = core.GameData.GetOffset("IServerGameDLL::GameServerSteamAPIActivated");
        steamServerAPIActivated = core.Memory.GetUnmanagedFunctionByVTable<SteamServerAPIActivated>(core.Memory.GetVTableAddress(Library.Server, "CSource2Server")!.Value, offset);
        logger.LogInformation("Hooking IServerGameDLL::GameServerSteamAPIActivated at {Address:X}", steamServerAPIActivated.Address);
        steamServerAPIActivatedGuid = steamServerAPIActivated.AddHook(next =>
        {
            return ( pServer ) =>
            {
                if (!CSteamGameServerAPIContext.Init())
                {
                    logger.LogError("Failed to initialize Steamworks GameServer API context.");
                    return;
                }

                EventPublisher.InvokeOnSteamAPIActivatedHook();
                next()(pServer);
            };
        });
    }

    internal void UnhookSteamServerAPIActivated()
    {
        if (steamServerAPIActivated == null) return;
        steamServerAPIActivated.RemoveHook(steamServerAPIActivatedGuid);
        steamServerAPIActivated = null;
    }

    public void Dispose()
    {
        UnhookExecuteCommand();
        UnhookSteamServerAPIActivated();
    }
}
