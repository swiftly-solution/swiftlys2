using SwiftlyS2.Shared.Schemas;

namespace SwiftlyS2.Shared.Memory;

public interface IMemoryService
{

  /// <summary>
  /// Get an unmanaged function by its address.
  /// </summary>
  /// <typeparam name="TDelegate">The delegate type of the unmanaged function.</typeparam>
  /// <param name="address">The address of the unmanaged function.</param>
  /// <returns>The unmanaged function.</returns>
  IUnmanagedFunction<TDelegate> GetUnmanagedFunctionByAddress<TDelegate>(nint address) where TDelegate : Delegate;

  /// <summary>
  /// Get an unmanaged function by its vtable address and index.
  /// </summary>
  /// <typeparam name="TDelegate"></typeparam>
  /// <param name="pVTable">The address of the vtable.</param>
  /// <param name="index">The index of the function in the vtable.</param>
  /// <returns>The unmanaged function.</returns>
  IUnmanagedFunction<TDelegate> GetUnmanagedFunctionByVTable<TDelegate>(nint pVTable, int index) where TDelegate : Delegate;

  /// <summary>
  /// Get an unmanaged memory block by its address.
  /// </summary>
  /// <param name="address">The address from which to create the Unmanaged Memory wrapper.</param>
  IUnmanagedMemory GetUnmanagedMemoryByAddress(nint address);

  /// <summary>
  /// Get the address of an valve or swiftly native interface by its name.
  /// </summary>
  /// <param name="name">The name of the interface.</param>
  /// <returns>The address of the interface. Return null if not found.</returns>
  nint? GetInterfaceByName(string name);

  /// <summary>
  /// Get the address of a ida-style signature.
  /// </summary>
  /// <param name="library">The library of that signature belongs to.</param>
  /// <param name="signature">The signature of the function.</param>
  /// <returns>The address of the function. Return null if not found.</returns>
  nint? GetAddressBySignature(string library, string signature);

  /// <summary>
  /// Get the address of a vtable by its name.
  /// </summary>
  /// <param name="library">The library of that vtable belongs to.</param>
  /// <param name="vtableName">The name of the vtable.</param>
  /// <returns>The address of the vtable. Return null if not found.</returns>
  nint? GetVTableAddress(string library, string vtableName);

  /// <summary>
  /// Resolve the address of a xref signature.
  /// </summary>
  /// <param name="xrefAddress">The address of the xref.</param>
  /// <returns>The resolved address.</returns>
  nint ResolveXrefAddress(nint xrefAddress);

  /// <summary>
  /// Get the vtable name of an object pointer.
  /// </summary>
  /// <param name="address">The address of the object pointer.</param>
  /// <returns>The vtable name. Return null if not found.</returns>
  string? GetObjectPtrVtableName(nint address);
  
  /// <summary>
  /// Check if an object pointer has a vtable.
  /// </summary>
  /// <param name="address">The address of the object pointer.</param>
  /// <returns>True if the object pointer has a vtable, false otherwise.</returns>
  bool ObjectPtrHasVtable(nint address);
  
  /// <summary>
  /// Check if an object pointer has a base class.
  /// </summary>
  /// <param name="address">The address of the object pointer.</param>
  /// <param name="baseClassName">The name of the base class.</param>
  /// <returns>True if the object pointer has the base class, false otherwise.</returns>
  bool ObjectPtrHasBaseClass(nint address, string baseClassName);

  /// <summary>
  /// Convert a raw address to a schema class.
  /// </summary>
  /// <typeparam name="T">The schema class type.</typeparam>
  /// <param name="address">The address of the schema class.</param>
  /// <returns>The schema class.</returns>
  T ToSchemaClass<T>(nint address) where T : class, ISchemaClass<T>;

}