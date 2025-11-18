using SwiftlyS2.Shared.Schemas;

namespace SwiftlyS2.Shared.Natives;

/// <summary>
/// Native handle from swiftly c++ native side, either allocated or borrowed from game.
/// </summary>
public interface INativeHandle
{

  /// <summary>
  /// Return whether a handle is valid. 
  /// Still might be dangerous for some pointer that borrowed from game instead of allocated by ourselves.
  /// </summary>

  public bool IsValid { get; }

  /// <summary>
  /// Dangerous method to get the memory address of the object
  /// </summary>
  /// <returns>The raw handle.</returns>
  public unsafe IntPtr Address { get; }

}