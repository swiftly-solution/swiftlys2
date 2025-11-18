namespace SwiftlyS2.Shared.Convars;

public interface IConVar<T> {
  /// <summary>
  /// The value of the convar.
  /// When setting, if the convar can be replicated, it will automatically replicate to all clients.
  /// Also, setting value with this method will internally put it into a set queue,
  /// Which means that for some special case ( e.g. setting sv_enablebunnyhopping inside a hook ) it won't work,
  /// in such cases you should use the SetInternal method instead.
  /// </summary>
  T Value { get; set; }

  /// <summary>
  /// The max value of the convar.
  /// 
  /// <exception cref="InvalidOperationException">Thrown when the convar is not a min/max type or doesn't have a max value.</exception>
  /// </summary>
  T MaxValue { get; set; }

  /// <summary>
  /// The min value of the convar.
  /// 
  /// <exception cref="InvalidOperationException">Thrown when the convar is not a min/max type or doesn't have a min value.</exception>
  /// </summary>
  T MinValue { get; set; }

  /// <summary>
  /// The default value of the convar.
  /// </summary>
  T DefaultValue { get; set; }

  /// <summary>
  /// Whether the convar has a default value.
  /// </summary>
  bool HasDefaultValue { get; }

  /// <summary>
  /// Whether the convar has a min value.
  /// </summary>
  bool HasMinValue { get; }

  /// <summary>
  /// Whether the convar has a max value.
  /// </summary>
  bool HasMaxValue { get; }

  /// <summary>
  /// The flags of the convar.
  /// </summary>
  ConvarFlags Flags { get; set; }


  /// <summary>
  /// Internally set the value of the convar.
  /// Won't replicate the change to clients.
  /// </summary>
  /// <param name="value">The value to set.</param>
  void SetInternal(T value);

  /// <summary>
  /// Replicate the value of the convar to specified client.
  /// </summary>
  /// <param name="clientId">The client id to replicate to.</param>
  void ReplicateToClient(int clientId, T value);

  /// <summary>
  /// Query the value of the convar from specified client.
  /// </summary>
  /// <param name="clientId"></param>
  /// <param name="callback">The action to execute with the value.</param>
  void QueryClient(int clientId, Action<string> callback);

  /// <summary>
  /// Try to get the min value of the convar.
  /// </summary>
  /// <param name="minValue">The min value of the convar.</param>
  /// <returns>True if the min value is found, false otherwise.</returns>
  bool TryGetMinValue(out T minValue);

  /// <summary>
  /// Try to get the max value of the convar.
  /// </summary>
  /// <param name="maxValue">The max value of the convar.</param>
  /// <returns>True if the max value is found, false otherwise.</returns>
  bool TryGetMaxValue(out T maxValue);

  /// <summary>
  /// Try to get the default value of the convar.
  /// </summary>
  /// <param name="defaultValue">The default value of the convar.</param>
  /// <returns>True if the default value is found, false otherwise.</returns>
  bool TryGetDefaultValue(out T defaultValue);
}