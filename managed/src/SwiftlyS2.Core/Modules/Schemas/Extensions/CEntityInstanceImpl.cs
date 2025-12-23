using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Scheduler;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.EntitySystem;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.SchemaDefinitions;

internal partial class CEntityInstanceImpl : CEntityInstance, IEquatable<CEntityInstance>
{
    public uint Index => Entity?.EntityHandle.EntityIndex ?? uint.MaxValue;
    public string DesignerName => Entity?.DesignerName ?? string.Empty;

    public unsafe void AcceptInput<T>( string input, T? value, CEntityInstance? activator = null, CEntityInstance? caller = null, int outputID = 0 )
    {
        NativeBinding.ThrowIfNonMainThread();

        var variant = new CVariant<CVariantDefaultAllocator>(value);

        NativeEntitySystem.AcceptInput(Address, input, activator?.Address ?? nint.Zero, caller?.Address ?? nint.Zero, new nint(&variant), outputID);
    }

    public Task AcceptInputAsync<T>( string input, T? value, CEntityInstance? activator = null, CEntityInstance? caller = null, int outputID = 0 )
    {
        return SchedulerManager.QueueOrNow(() => AcceptInput(input, value, activator, caller, outputID));
    }

    public unsafe void AddEntityIOEvent<T>( string input, T? value, CEntityInstance? activator = null, CEntityInstance? caller = null, float delay = 0f )
    {
        NativeBinding.ThrowIfNonMainThread();

        var variant = new CVariant<CVariantDefaultAllocator>(value);

        NativeEntitySystem.AddEntityIOEvent(Address, input, activator?.Address ?? nint.Zero, caller?.Address ?? nint.Zero, (nint)(&variant), delay);
    }

    public Task AddEntityIOEventAsync<T>( string input, T? value, CEntityInstance? activator = null, CEntityInstance? caller = null, float delay = 0f )
    {
        return SchedulerManager.QueueOrNow(() => AddEntityIOEvent(input, value, activator, caller, delay));
    }

    public void SetTransmitState( bool transmitting, int playerId )
    {
        NativePlayer.ShouldBlockTransmitEntity(playerId, (int)Index, !transmitting);
    }

    public void SetTransmitState( bool transmitting )
    {
        NativePlayerManager.ShouldBlockTransmitEntity((int)Index, !transmitting);
    }

    public bool IsTransmitting( int playerId )
    {
        return !NativePlayer.IsTransmitEntityBlocked(playerId, (int)Index);
    }

    public void DispatchSpawn( CEntityKeyValues? entityKV = null )
    {
        NativeEntitySystem.Spawn(Address, entityKV?.Address ?? nint.Zero);
    }

    public Task DispatchSpawnAsync( CEntityKeyValues? entityKV = null )
    {
        return SchedulerManager.QueueOrNow(() => DispatchSpawn(entityKV));
    }

    public void Despawn()
    {
        NativeEntitySystem.Despawn(Address);
    }

    public Task DespawnAsync()
    {
        return SchedulerManager.QueueOrNow(Despawn);
    }

    public bool Equals( CEntityInstance? other )
    {
        return other != null && this.Address.Equals(other.Address);
    }

    public override string ToString()
    {
        return this.IsValid ? $"{this.DesignerName}[{this.Index}]" : "invalid";
    }

    public override bool Equals( object? obj )
    {
        return obj is CEntityInstance v && this.Equals(v);
    }

    public override int GetHashCode()
    {
        return this.Address.GetHashCode();
    }

}