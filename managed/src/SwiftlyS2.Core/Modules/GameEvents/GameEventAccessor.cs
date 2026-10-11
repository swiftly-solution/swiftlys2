using System.Runtime.CompilerServices;
using SwiftlyS2.Core.EntitySystem;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Natives.NativeObjects;
using SwiftlyS2.Core.Players;
using SwiftlyS2.Core.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.GameEvents;

internal enum GameEventValueKind
{
    Bool,
    Int32,
    UInt64,
    Float,
    Ptr,
    Entity,
    EntityIndex,
    PlayerSlot,
    PlayerController,
    PlayerPawn,
    PawnEntityIndex
}

internal class GameEventAccessor : NativeHandle, IGameEventAccessor, IDisposable
{

    public bool DontBroadcast { get; set; }
    private bool _IsValid = true;

    public GameEventAccessor( nint handle ) : base(handle)
    {
    }

    public void Dispose()
    {
        _IsValid = false;
    }

    private void CheckIsValid()
    {
        if (!_IsValid) throw new InvalidOperationException("The event is already disposed.");
        if (Address == 0) throw new InvalidOperationException("The event is invalid.");
    }

    public void SetBool( string key, bool value )
    {
        CheckIsValid();
        Write(key, GameEventValueKind.Bool, value);
    }

    public bool GetBool( string key )
    {
        CheckIsValid();
        return Read<bool>(key, GameEventValueKind.Bool);
    }

    public void SetInt32( string key, int value )
    {
        CheckIsValid();
        Write(key, GameEventValueKind.Int32, value);
    }

    public int GetInt32( string key )
    {
        CheckIsValid();
        return Read<int>(key, GameEventValueKind.Int32);
    }

    public void SetUInt64( string key, ulong value )
    {
        CheckIsValid();
        Write(key, GameEventValueKind.UInt64, value);
    }

    public ulong GetUInt64( string key )
    {
        CheckIsValid();
        return Read<ulong>(key, GameEventValueKind.UInt64);
    }

    public void SetFloat( string key, float value )
    {
        CheckIsValid();
        Write(key, GameEventValueKind.Float, value);
    }

    public float GetFloat( string key )
    {
        CheckIsValid();
        return Read<float>(key, GameEventValueKind.Float);
    }

    public void SetString( string key, string value )
    {
        CheckIsValid();
        NativeGameEvents.SetString(Address, key, value);
    }

    public string GetString( string key )
    {
        CheckIsValid();
        return NativeGameEvents.GetString(Address, key);
    }

    public void SetEntity<K>( string key, K value ) where K : CEntityInstance
    {
        CheckIsValid();
        Write(key, GameEventValueKind.Entity, value.Address);
    }

    public K GetEntity<K>( string key ) where K : CEntityInstance
    {
        CheckIsValid();
        return (K)K.From(Read<nint>(key, GameEventValueKind.Entity));
    }

    public void SetEntityIndex( string key, int value )
    {
        CheckIsValid();
        Write(key, GameEventValueKind.EntityIndex, value);
    }

    public int GetEntityIndex( string key )
    {
        CheckIsValid();
        return Read<int>(key, GameEventValueKind.EntityIndex);
    }

    public void SetPlayerSlot( string key, int value )
    {
        CheckIsValid();
        Write(key, GameEventValueKind.PlayerSlot, value);
    }

    public int GetPlayerSlot( string key )
    {
        CheckIsValid();
        return Read<int>(key, GameEventValueKind.PlayerSlot);
    }

    public CCSPlayerController GetPlayerController( string key )
    {
        CheckIsValid();
        var controllerPtr = Read<nint>(key, GameEventValueKind.PlayerController);
        return EntityManager.GetEntityByAddress(controllerPtr) as CCSPlayerControllerImpl ?? new CCSPlayerControllerImpl(controllerPtr);
    }

    public CCSPlayerPawn GetPlayerPawn( string key )
    {
        CheckIsValid();
        var pawnPtr = Read<nint>(key, GameEventValueKind.PlayerPawn);
        return EntityManager.GetEntityByAddress(pawnPtr) as CCSPlayerPawnImpl ?? new CCSPlayerPawnImpl(pawnPtr);
    }

    public IPlayer? GetPlayer( string key )
    {
        CheckIsValid();

        var playerid = GetInt32(key);
        return PlayerManagerService.PlayerObjects.TryGetValue(playerid, out var player) ? player : null;
    }

    public void SetPtr( string key, nint value )
    {
        CheckIsValid();
        Write(key, GameEventValueKind.Ptr, value);
    }

    public nint GetPtr( string key )
    {
        CheckIsValid();
        return Read<nint>(key, GameEventValueKind.Ptr);
    }

    public int GetPawnEntityIndex( string key )
    {
        CheckIsValid();
        return Read<int>(key, GameEventValueKind.PawnEntityIndex);
    }

    private unsafe T Read<T>( string key, GameEventValueKind kind ) where T : unmanaged
    {
        T value = default;
        _ = NativeGameEvents.GetValue(Address, key, (int)kind, (nint)(&value));
        return value;
    }

    private unsafe void Write<T>( string key, GameEventValueKind kind, T value ) where T : unmanaged
    {
        _ = NativeGameEvents.SetValue(Address, key, (int)kind, (nint)(&value));
    }

    public bool IsReliable()
    {
        CheckIsValid();
        return NativeGameEvents.IsReliable(Address);
    }

    public bool IsLocal()
    {
        CheckIsValid();
        return NativeGameEvents.IsLocal(Address);
    }


}
