using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Scheduler;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.EntitySystem;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Core.EntitySystem;
using SwiftlyS2.Core.Schemas;
using SwiftlyS2.Core.Extensions;

namespace SwiftlyS2.Core.SchemaDefinitions;

internal partial class CEntityInstanceImpl : CEntityInstance, IEquatable<CEntityInstance>
{
    private CEntityIdentityImpl _identity = new(0);

    public uint Index => Identity?.EntityHandle.EntityIndex ?? uint.MaxValue;
    public string DesignerName => Identity?.DesignerName ?? string.Empty;

    public CEntityIdentity? Identity {
        get {
            _EntityOffset ??= Schema.GetOffset(0xB6DD442EA8A45978);

            var ptr = _Handle.Read<nint>(_EntityOffset!.Value);
            if (ptr == nint.Zero)
            {
                return null;
            }

            _identity.DangerousSetHandle(ptr);
            return _identity;
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
        NativePlayer.ShouldBlockTransmitEntity(playerId, (int)Index, !transmitting);
    }

    public void SetTransmitState( bool transmitting )
    {
        ThrowIfInvalidEntity();
        NativePlayerManager.ShouldBlockTransmitEntity((int)Index, !transmitting);
    }

    public bool IsTransmitting( int playerId )
    {
        ThrowIfInvalidEntity();
        return !NativePlayer.IsTransmitEntityBlocked(playerId, (int)Index);
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