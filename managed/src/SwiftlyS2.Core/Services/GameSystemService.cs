using Spectre.Console;
using SwiftlyS2.Core.Events;
using SwiftlyS2.Core.Extensions;
using SwiftlyS2.Core.Misc;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Memory;

namespace SwiftlyS2.Core.Services;

internal class GameSystemService : IDisposable
{
    private delegate void BuildGameSessionManifest( nint pThis, nint pMsg );

    private readonly ISwiftlyCore core;

    private IUnmanagedFunction<BuildGameSessionManifest>? buildGameSessionManifest;
    private Guid buildGameSessionManifestGuid;

    public GameSystemService( ISwiftlyCore core )
    {
        this.core = core;

        HookBuildGameSessionManifest();
    }

    private void HookBuildGameSessionManifest()
    {
        var offset = core.GameData.GetOffset("IGameSystem::BuildGameSessionManifest");
        buildGameSessionManifest = core.Memory.GetUnmanagedFunctionByVTable<BuildGameSessionManifest>(core.Memory.GetVTableAddress(Library.Server, "CGameRulesGameSystem")!.Value, offset);
        buildGameSessionManifestGuid = buildGameSessionManifest.AddHook(next =>
        {
            return ( pThis, pMsg ) =>
            {
                next()(pThis, pMsg);

                try
                {
                    var msg = pMsg.AsRef<EventBuildGameSessionManifest_t>();
                    EventPublisher.OnPrecacheResource(msg.m_pResourceManifest);
                }
                catch (Exception e)
                {
                    if (GlobalExceptionHandler.Handle(ref e)) AnsiConsole.WriteException(e);
                }
            };
        });
    }

    public void Dispose()
    {
        buildGameSessionManifest?.RemoveHook(buildGameSessionManifestGuid);
        buildGameSessionManifest = null;
    }
}
