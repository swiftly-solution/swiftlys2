namespace SwiftlyS2.Shared.Memory;

public interface IUnmanagedFunction<TDelegate> where TDelegate : Delegate {

  /// <summary>
  /// The address of the unmanaged function.
  /// </summary>
  nint Address { get; }


  /// <summary>
  /// The unhooked original function delegate.
  /// Call this if you don't want your invocation to be hooked.
  /// </summary>
  TDelegate CallOriginal { get; }

  /// <summary>
  /// The delegate that directly call to the address.
  /// Might be hooked by other plugins or core.
  /// </summary>
  TDelegate Call { get; }

  /// <summary>
  /// Hook a native function at the specified address with a managed callback.
  /// The <paramref name="callbackBuilder"/> receives the pointer to the "next" function in the chain
  /// (previous callback if any, or the original function pointer if this is the first callback),
  /// and must return a delegate matching the native function signature with proper calling convention.
  /// </summary>
  /// <param name="callbackBuilder">Builder that receives the next function pointer and returns the managed callback.</param>
  /// <returns>a guid for the hook.</returns>
  Guid AddHook(Func<Func<TDelegate>, TDelegate> callbackBuilder);

  /// <summary>
  /// Unhook a hook by its id.
  /// </summary>
  /// <param name="id">The id of the hook to unhook.</param>
  void RemoveHook(Guid id);
}
