using SwiftlyS2.Shared;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Services;
using SwiftlyS2.Core.Extensions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.SteamAPI;
using Spectre.Console;

namespace SwiftlyS2.Core.Services;

internal class EngineService : IEngineService
{
    private readonly CommandTrackerManager _commandTrackedManager;

    public EngineService( CommandTrackerManager commandTrackedManager )
    {
        this._commandTrackedManager = commandTrackedManager;
    }

    public string? ServerIP => NativeEngineHelpers.GetIP();

    public ref CGlobalVars GlobalVars => ref NativeEngineHelpers.GetGlobalVars().AsRef<CGlobalVars>();

    public string Map => GlobalVars.MapName.Value;

    public string WorkshopId => NativeEngineHelpers.GetWorkshopId();

    public int MaxPlayers => GlobalVars.MaxClients;

    public float CurrentTime => GlobalVars.CurrentTime;

    public int TickCount => GlobalVars.TickCount;

    public void ExecuteCommand( string command )
    {
        NativeEngineHelpers.ExecuteCommand(command);
    }

    public void ExecuteCommandWithBuffer( string command, Action<string> bufferCallback )
    {
        _commandTrackedManager.EnqueueCommand(bufferCallback);
        NativeEngineHelpers.ExecuteCommand($"^wb^{command}");
    }

    public bool IsMapValid( string map )
    {
        return NativeEngineHelpers.IsMapValid(map);
    }

    public nint? FindGameSystemByName( string name )
    {
        var handle = NativeEngineHelpers.FindGameSystemByName(name);
        return handle == nint.Zero ? null : handle;
    }

    public void DispatchParticleEffect( string particleName, ParticleAttachment_t attachmentType, byte attachmentPoint, CUtlSymbolLarge attachmentName, CRecipientFilter filter, bool resetAllParticlesOnEntity = false, int splitScreenSlot = 0, CBaseEntity? entity = null )
    {
        GameFunctions.DispatchParticleEffect(
            particleName,
            (uint)attachmentType,
            entity?.Address ?? 0,
            attachmentPoint,
            attachmentName,
            resetAllParticlesOnEntity,
            splitScreenSlot,
            filter
        );
    }
}