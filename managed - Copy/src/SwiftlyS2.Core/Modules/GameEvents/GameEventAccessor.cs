using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Natives.NativeObjects;
using SwiftlyS2.Core.Players;
using SwiftlyS2.Core.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.GameEvents;

internal class GameEventAccessor : NativeHandle, IGameEventAccessor, IDisposable {
  
  public bool DontBroadcast { get; set; }
  private bool _IsValid = true;

  public GameEventAccessor(nint handle) : base(handle)
  {
  }

  public void Dispose() {
    _IsValid = false;
  }

  private void CheckIsValid() {
    if (!_IsValid) throw new InvalidOperationException("The event is already disposed.");
  }

  public void SetBool(string key, bool value) {
    CheckIsValid();
    NativeGameEvents.SetBool(Address, key, value);
  }

  public bool GetBool(string key) {
    CheckIsValid();
    return NativeGameEvents.GetBool(Address, key);
  }

  public void SetInt32(string key, int value) {
    CheckIsValid();
    NativeGameEvents.SetInt(Address, key, value);
  }

  public int GetInt32(string key) {
    CheckIsValid();
    return NativeGameEvents.GetInt(Address, key);
  }

  public void SetUInt64(string key, ulong value) {
    CheckIsValid();
    NativeGameEvents.SetUint64(Address, key, value);
  }

  public ulong GetUInt64(string key) {
    CheckIsValid();
    return NativeGameEvents.GetUint64(Address, key);
  }

  public void SetFloat(string key, float value) {
    CheckIsValid();
    NativeGameEvents.SetFloat(Address, key, value);
  }

  public float GetFloat(string key) {
    CheckIsValid();
    return NativeGameEvents.GetFloat(Address, key);
  }

  public void SetString(string key, string value) {
    CheckIsValid();
    NativeGameEvents.SetString(Address, key, value);
  }

  public string GetString(string key) {
    CheckIsValid();
    return NativeGameEvents.GetString(Address, key);
  }

  public void SetEntity<K>(string key, K value) where K : CEntityInstance {
    CheckIsValid();
    NativeGameEvents.SetEntity(Address, key, value.Address);
  }

  public K GetEntity<K>(string key) where K : CEntityInstance {
    CheckIsValid();
    return (K)K.From(NativeGameEvents.GetEntity(Address, key));
  }

  public void SetEntityIndex(string key, int value) {
    CheckIsValid();
    NativeGameEvents.SetEntityIndex(Address, key, value);
  }

  public int GetEntityIndex(string key) {
    CheckIsValid();
    return NativeGameEvents.GetEntityIndex(Address, key);
  }

  public void SetPlayerSlot(string key, int value) {
    CheckIsValid();
    NativeGameEvents.SetPlayerSlot(Address, key, value);
  }

  public int GetPlayerSlot(string key) {
    CheckIsValid();
    return NativeGameEvents.GetPlayerSlot(Address, key);
  }

  public CCSPlayerController GetPlayerController(string key) {
    CheckIsValid();
    return new CCSPlayerControllerImpl(NativeGameEvents.GetPlayerController(Address, key));
  }

  public CCSPlayerPawn GetPlayerPawn(string key) {
    CheckIsValid();
    return new CCSPlayerPawnImpl(NativeGameEvents.GetPlayerPawn(Address, key));
  }

  public IPlayer GetPlayer(string key) {
    CheckIsValid();
    return new Player(GetInt32(key));
  }

  public void SetPtr(string key, nint value) {
    CheckIsValid();
    NativeGameEvents.SetPtr(Address, key, value);
  }

  public nint GetPtr(string key) {
    CheckIsValid();
    return NativeGameEvents.GetPtr(Address, key);
  }

  public int GetPawnEntityIndex(string key) {
    CheckIsValid();
    return NativeGameEvents.GetPawnEntityIndex(Address, key);
  }

  public bool IsReliable() {
    CheckIsValid();
    return NativeGameEvents.IsReliable(Address);
  }

  public bool IsLocal() {
    CheckIsValid();
    return NativeGameEvents.IsLocal(Address);
  }

  
}
