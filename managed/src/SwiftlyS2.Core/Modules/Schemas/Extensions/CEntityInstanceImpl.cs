using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Scheduler;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.EntitySystem;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Core.EntitySystem;

namespace SwiftlyS2.Core.SchemaDefinitions;


internal partial class CEntityInstanceImpl : CEntityInstance, IEquatable<CEntityInstance>
{
    private const uint MAX_EDICTS = 1 << 14;

    public uint Index => Entity?.EntityHandle.EntityIndex ?? uint.MaxValue;
    public string DesignerName => Entity?.DesignerName ?? string.Empty;

    public CEntityIdentity? Identity {
        get {
            return Entity;
        }
    }

    public new bool IsValid => base.IsValid && IsValidEntity;

    public bool IsValidEntity => EntityManager.IsAddressValid(Address);

    private void ThrowIfInvalidEntity()
    {
        if (!IsValidEntity)
        {
            throw new InvalidOperationException("The entity instance is no longer valid.");
        }
    }

    public unsafe void AcceptInput<T>( string input, T? value, CEntityInstance? activator = null, CEntityInstance? caller = null, int outputID = 0 )
    {
        NativeBinding.ThrowIfNonMainThread();
        ThrowIfInvalidEntity();

        using var variant = new CVariant<CVariantDefaultAllocator>(value);

        GameFunctions.CEntityInstance_AcceptInput(Address, input, activator?.Address ?? nint.Zero, caller?.Address ?? nint.Zero, (nint)(&variant), outputID);
    }

    public Task AcceptInputAsync<T>( string input, T? value, CEntityInstance? activator = null, CEntityInstance? caller = null, int outputID = 0 )
    {
        return SchedulerManager.QueueOrNow(() => AcceptInput(input, value, activator, caller, outputID));
    }

    public unsafe void AddEntityIOEvent<T>( string input, T? value, CEntityInstance? activator = null, CEntityInstance? caller = null, float delay = 0f )
    {
        NativeBinding.ThrowIfNonMainThread();
        ThrowIfInvalidEntity();

        using var variant = new CVariant<CVariantDefaultAllocator>(value);

        GameFunctions.AddEntityIOEvent(Address, input, activator?.Address ?? nint.Zero, caller?.Address ?? nint.Zero, (nint)(&variant), delay);
    }

    public Task AddEntityIOEventAsync<T>( string input, T? value, CEntityInstance? activator = null, CEntityInstance? caller = null, float delay = 0f )
    {
        return SchedulerManager.QueueOrNow(() => AddEntityIOEvent(input, value, activator, caller, delay));
    }

    public void SetTransmitState( bool transmitting, int playerId )
    {
        ThrowIfInvalidEntity();
        var index = Index;
        if (index < 0 || index >= MAX_EDICTS)
        {
            throw new InvalidOperationException($"Cannot modify the transmit state of non-networked entity ({DesignerName}[{index}]) as it's not being transmitted to players.");
        }
        NativePlayer.ShouldBlockTransmitEntity(playerId, (int)index, !transmitting);
    }

    public void SetTransmitState( bool transmitting )
    {
        ThrowIfInvalidEntity();
        var index = Index;
        if (index < 0 || index >= MAX_EDICTS)
        {
            throw new InvalidOperationException($"Cannot modify the transmit state of non-networked entity ({DesignerName}[{index}]) as it's not being transmitted to players.");
        }

        NativePlayerManager.ShouldBlockTransmitEntity((int)index, !transmitting);
    }

    public bool IsTransmitting( int playerId )
    {
        ThrowIfInvalidEntity();
        var index = Index;
        if (index < 0 || index >= MAX_EDICTS)
        {
            throw new InvalidOperationException($"Cannot read the transmit state of non-networked entity ({DesignerName}[{index}]) as it's not being transmitted to players.");
        }

        return !NativePlayer.IsTransmitEntityBlocked(playerId, (int)index);
    }

    public void DispatchSpawn( CEntityKeyValues? entityKV = null )
    {
        ThrowIfInvalidEntity();
        NativeEntitySystem.Spawn(Address, entityKV?.Address ?? nint.Zero);
    }

    public Task DispatchSpawnAsync( CEntityKeyValues? entityKV = null )
    {
        return SchedulerManager.QueueOrNow(() => DispatchSpawn(entityKV));
    }

    public void Despawn()
    {
        ThrowIfInvalidEntity();
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
        return obj is CEntityInstance v && Equals(v);
    }

    public override int GetHashCode()
    {
        return this.Address.GetHashCode();
    }

}