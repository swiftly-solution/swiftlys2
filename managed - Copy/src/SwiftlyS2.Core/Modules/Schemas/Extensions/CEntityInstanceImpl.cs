using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Schemas;
using SwiftlyS2.Shared.EntitySystem;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.Schemas;

namespace SwiftlyS2.Core.SchemaDefinitions;

internal partial class CEntityInstanceImpl : CEntityInstance {

  public uint Index => Entity?.EntityHandle.EntityIndex ?? uint.MaxValue;

  public string DesignerName => Entity?.DesignerName ?? string.Empty;

  public void AcceptInput<T>(string input, T value, CEntityInstance? activator = null, CEntityInstance? caller = null, int outputID = 0) {
    switch (value) {
      case bool boolValue:
        NativeEntitySystem.AcceptInputBool(Address, input, activator?.Address ?? nint.Zero, caller?.Address ?? nint.Zero, boolValue, outputID);
        break;
      case int intValue:
        NativeEntitySystem.AcceptInputInt32(Address, input, activator?.Address ?? nint.Zero, caller?.Address ?? nint.Zero, intValue, outputID);
        break;
      case uint uintValue:
        NativeEntitySystem.AcceptInputUInt32(Address, input, activator?.Address ?? nint.Zero, caller?.Address ?? nint.Zero, uintValue, outputID);
        break;
      case long longValue:
        NativeEntitySystem.AcceptInputInt64(Address, input, activator?.Address ?? nint.Zero, caller?.Address ?? nint.Zero, longValue, outputID);
        break;
      case ulong ulongValue:
        NativeEntitySystem.AcceptInputUInt64(Address, input, activator?.Address ?? nint.Zero, caller?.Address ?? nint.Zero, ulongValue, outputID);
        break;
      case float floatValue:
        NativeEntitySystem.AcceptInputFloat(Address, input, activator?.Address ?? nint.Zero, caller?.Address ?? nint.Zero, floatValue, outputID);
        break;
      case double doubleValue:
        NativeEntitySystem.AcceptInputDouble(Address, input, activator?.Address ?? nint.Zero, caller?.Address ?? nint.Zero, doubleValue, outputID);
        break;
      case string stringValue:
        NativeEntitySystem.AcceptInputString(Address, input, activator?.Address ?? nint.Zero, caller?.Address ?? nint.Zero, stringValue, outputID);
        break;
      default:
        throw new InvalidOperationException($"Unsupported type: {typeof(T).Name}");
    }
  }

  public void AddEntityIOEvent<T>(string input, T value, CEntityInstance? activator = null, CEntityInstance? caller = null, float delay = 0f) {
    switch (value) {
      case bool boolValue:
        NativeEntitySystem.AddEntityIOEventBool(Address, input, activator?.Address ?? nint.Zero, caller?.Address ?? nint.Zero, boolValue, delay);
        break;
      case int intValue:
        NativeEntitySystem.AddEntityIOEventInt32(Address, input, activator?.Address ?? nint.Zero, caller?.Address ?? nint.Zero, intValue, delay);
        break;
      case uint uintValue:
        NativeEntitySystem.AddEntityIOEventUInt32(Address, input, activator?.Address ?? nint.Zero, caller?.Address ?? nint.Zero, uintValue, delay);
        break;
      case long longValue:
        NativeEntitySystem.AddEntityIOEventInt64(Address, input, activator?.Address ?? nint.Zero, caller?.Address ?? nint.Zero, longValue, delay);
        break;
      case ulong ulongValue:
        NativeEntitySystem.AddEntityIOEventUInt64(Address, input, activator?.Address ?? nint.Zero, caller?.Address ?? nint.Zero, ulongValue, delay);
        break;
      case float floatValue:
        NativeEntitySystem.AddEntityIOEventFloat(Address, input, activator?.Address ?? nint.Zero, caller?.Address ?? nint.Zero, floatValue, delay);
        break;
      case double doubleValue:
        NativeEntitySystem.AddEntityIOEventDouble(Address, input, activator?.Address ?? nint.Zero, caller?.Address ?? nint.Zero, doubleValue, delay);
        break;
      case string stringValue:
        NativeEntitySystem.AddEntityIOEventString(Address, input, activator?.Address ?? nint.Zero, caller?.Address ?? nint.Zero, stringValue, delay);
        break;
      default:
        throw new InvalidOperationException($"Unsupported type: {typeof(T).Name}");
    }
  }

  public void SetTransmitState( bool transmitting , int playerId ) {
    NativePlayer.ShouldBlockTransmitEntity(playerId, (int)Index, transmitting);

  }

  public void SetTransmitState( bool transmitting )
  {
    NativePlayerManager.ShouldBlockTransmitEntity((int)Index, transmitting);
  }
  
  public bool IsTransmitting( int playerId )
  {
    return NativePlayer.IsTransmitEntityBlocked(playerId, (int)Index);
  }

  public void DispatchSpawn(CEntityKeyValues? entityKV = null) {
    NativeEntitySystem.Spawn(Address, entityKV?.Address ?? nint.Zero);
  }

  public void Despawn() {
    NativeEntitySystem.Despawn(Address);
  }
}