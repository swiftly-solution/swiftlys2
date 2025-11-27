using SwiftlyS2.Core.EntitySystem;
using SwiftlyS2.Shared.EntitySystem;

namespace SwiftlyS2.Shared.SchemaDefinitions;

public partial interface CEntityInstance
{


  /// <summary>
  /// The index of the entity.
  /// </summary>
  public uint Index { get; }

  /// <summary>
  /// The designer name of the entity.
  /// </summary>
  public string DesignerName { get; }

  /// <summary>
  /// Fire an input to the entity.
  /// </summary>
  /// <typeparam name="T">Param type. Support bool, int, uint, long, ulong, float, double, string, Vector, Vector2D, Vector4D, QAngle, Color</typeparam>
  /// <param name="input">Input name.</param>
  /// <param name="value">Input value.</param>
  /// <param name="activator">Activator entity. Nullable.</param>
  /// <param name="caller">Caller entity. Nullable.</param>
  /// <param name="outputID">Output ID.</param>
  public void AcceptInput<T>( string input, T value, CEntityInstance? activator = null, CEntityInstance? caller = null, int outputID = 0 );

  /// <summary>
  /// Add an entity IO event to the entity.
  /// </summary>
  /// <typeparam name="T">Param type. Support bool, int, uint, long, ulong, float, double, string, Vector, Vector2D, Vector4D, QAngle, Color</typeparam>
  /// <param name="input">Input name.</param>
  /// <param name="value">Input value.</param>
  /// <param name="activator">Activator entity. Nullable.</param>
  /// <param name="caller">Caller entity. Nullable.</param>
  /// <param name="delay">Delay in seconds.</param>x
  public void AddEntityIOEvent<T>( string input, T value, CEntityInstance? activator = null, CEntityInstance? caller = null, float delay = 0f );

  /// <summary>
  /// Dispatch a spawn event to the entity.
  /// </summary>
  /// <param name="entityKV">Entity key values. Nullable.</param>
  public void DispatchSpawn( CEntityKeyValues? entityKV = null );

  /// <summary>
  /// Set the transmit state of the entity for one player.
  /// </summary>
  /// <param name="transmitting">Whether the entity should be transmitting.</param>
  /// <param name="playerId">The player ID to set the transmit state for.</param>
  public void SetTransmitState( bool transmitting, int playerId );

  /// <summary>
  /// Set the global transmit state of the entity.
  /// </summary>
  /// <param name="transmitting">Whether the entity should be transmitting.</param>
  public void SetTransmitState( bool transmitting );

  /// <summary>
  /// Check if the entity is transmitting for one player.
  /// </summary>
  /// <param name="playerId">The player ID to check the transmit state for.</param>
  public bool IsTransmitting( int playerId );

  /// <summary>
  /// Despawn the entity.
  public void Despawn();
}