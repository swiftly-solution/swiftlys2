using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Core.Scheduler;
using SwiftlyS2.Shared.Services;
using SwiftlyS2.Core.Extensions;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.Services;

internal class EngineService : IEngineService
{
    public EngineService()
    {
    }

    public string? ServerIP => NativeEngineHelpers.GetIP();

    public ref CGlobalVars GlobalVars {
        get {
            var globalVarsPtr = NativeEngineHelpers.GetGlobalVars();
            if (globalVarsPtr == nint.Zero) throw new InvalidOperationException("GlobalVars is null.");

            return ref globalVarsPtr.AsRef<CGlobalVars>();
        }
    }

    public string Map => GlobalVars.MapName.Value;

    public string WorkshopId => NativeEngineHelpers.GetWorkshopId();

    public int MaxPlayers => GlobalVars.MaxClients;

    public float CurrentTime => GlobalVars.CurrentTime;

    public int TickCount => GlobalVars.TickCount;

    public void ExecuteCommand( string command )
    {
        NativeEngineHelpers.ExecuteCommand(command);
    }

    public Task ExecuteCommandAsync( string command )
    {
        return SchedulerManager.QueueOrNow(() => ExecuteCommand(command));
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

    public void DispatchParticleEffect(
        string particleName,
        ParticleAttachment_t attachmentType,
        byte attachmentPoint,
        CUtlSymbolLarge attachmentName,
        CRecipientFilter filter,
        bool resetAllParticlesOnEntity = false,
        int splitScreenSlot = 0,
        CBaseEntity? entity = null
    )
    {
        NativeBinding.ThrowIfNonMainThread();
        GameFunctions.DispatchParticleEffect(particleName, (uint)attachmentType, entity?.Address ?? 0, attachmentPoint, attachmentName, resetAllParticlesOnEntity, splitScreenSlot, filter);
    }

    public Task DispatchParticleEffectAsync(
        string particleName,
        ParticleAttachment_t attachmentType,
        byte attachmentPoint,
        CUtlSymbolLarge attachmentName,
        CRecipientFilter filter,
        bool resetAllParticlesOnEntity = false,
        int splitScreenSlot = 0,
        CBaseEntity? entity = null
    )
    {
        return SchedulerManager.QueueOrNow(() => DispatchParticleEffect(particleName, attachmentType, attachmentPoint, attachmentName, filter, resetAllParticlesOnEntity, splitScreenSlot, entity));
    }
}