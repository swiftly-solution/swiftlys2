namespace SwiftlyS2.Shared.Memory;

/// <summary>
/// A virtual function found by S2BinLib, addressed at runtime.
/// </summary>
/// <param name="VTableName">Class name of the vtable that owns the function.</param>
/// <param name="Index">Index of the function inside the vtable.</param>
/// <param name="Address">Runtime memory address of the function.</param>
public readonly record struct S2VFunc( string VTableName, int Index, nint Address );

/// <summary>
/// A virtual function found by S2BinLib, addressed relative to the module base.
/// </summary>
/// <param name="VTableName">Class name of the vtable that owns the function.</param>
/// <param name="Index">Index of the function inside the vtable.</param>
/// <param name="Rva">Relative virtual address of the function.</param>
public readonly record struct S2VFuncRva( string VTableName, int Index, ulong Rva );

/// <summary>
/// Binary analysis helpers (pattern scan, vtables, symbols, xrefs...) backed by S2BinLib.
/// <para>
/// <c>binary</c> is the module name, e.g. <c>"server"</c> or <c>"engine2"</c>.
/// Methods return <c>null</c>, <c>false</c> or an empty list when nothing was found or the lookup failed.
/// Methods ending in <c>Rva</c> work with offsets relative to the module base, the others with runtime addresses.
/// </para>
/// </summary>
public interface IS2BinLib
{
    /// <summary>Get the runtime base address of a module.</summary>
    public nint? GetModuleBase( string binary );

    /// <summary>Set the module base from any pointer that lives inside the module.</summary>
    public bool SetModuleBase( string binary, nint pointerInsideModule );

    /// <summary>Clear a module base that was set manually.</summary>
    public bool ClearModuleBase( string binary );

    /// <summary>Use a custom file for a binary on the given OS (<c>"windows"</c> or <c>"linux"</c>).</summary>
    public bool SetCustomBinaryPath( string binary, string path, string os );

    /// <summary>Get the file path of a binary.</summary>
    public string? GetBinaryPath( string binary );

    /// <summary>Check whether a binary is loaded in S2BinLib.</summary>
    public bool IsBinaryLoaded( string binary );

    /// <summary>Load a binary into S2BinLib.</summary>
    public bool LoadBinary( string binary );

    /// <summary>Unload a binary from S2BinLib.</summary>
    public bool UnloadBinary( string binary );

    /// <summary>Scan for an ida-style pattern and return its address.</summary>
    public nint? PatternScan( string binary, string pattern );

    /// <summary>Scan for an ida-style pattern and return its RVA.</summary>
    public ulong? PatternScanRva( string binary, string pattern );

    /// <summary>Find every match of an ida-style pattern.</summary>
    public IReadOnlyList<nint> PatternScanAll( string binary, string pattern );

    /// <summary>Find the RVA of every match of an ida-style pattern.</summary>
    public IReadOnlyList<ulong> PatternScanAllRva( string binary, string pattern );

    /// <summary>
    /// Find a vtable by class name. Use <c>"Outer::Inner"</c> for nested classes (2 levels max).
    /// </summary>
    public nint? FindVTable( string binary, string vtableName );

    /// <inheritdoc cref="FindVTable"/>
    public ulong? FindVTableRva( string binary, string vtableName );

    /// <summary>Find a vtable by its mangled name.</summary>
    public nint? FindVTableMangled( string binary, string mangledName );

    /// <inheritdoc cref="FindVTableMangled"/>
    public ulong? FindVTableMangledRva( string binary, string mangledName );

    /// <summary>Get the number of virtual functions of a vtable.</summary>
    public int? GetVTableFuncCount( string binary, string vtableName );

    /// <summary>Get the number of virtual functions of the vtable located at <paramref name="vtableRva"/>.</summary>
    public int? GetVTableFuncCount( string binary, ulong vtableRva );

    /// <summary>Find a virtual function by vtable name and index.</summary>
    public nint? FindVFunc( string binary, string vtableName, int index );

    /// <inheritdoc cref="FindVFunc(string, string, int)"/>
    public ulong? FindVFuncRva( string binary, string vtableName, int index );

    /// <summary>Find a virtual function by vtable pointer and index.</summary>
    public nint? FindVFunc( nint vtable, int index );

    /// <inheritdoc cref="FindVFunc(nint, int)"/>
    public ulong? FindVFuncRva( nint vtable, int index );

    /// <summary>Find the virtual function that contains the given RVA.</summary>
    public S2VFunc? FindVFuncContaining( string binary, ulong rva );

    /// <inheritdoc cref="FindVFuncContaining"/>
    public S2VFuncRva? FindVFuncContainingRva( string binary, ulong rva );

    /// <summary>Find the virtual function that references the given string.</summary>
    public S2VFunc? FindVFuncWithString( string binary, string value );

    /// <inheritdoc cref="FindVFuncWithString"/>
    public S2VFuncRva? FindVFuncWithStringRva( string binary, string value );

    /// <summary>Find the vtable index of <c>NetworkStateChanged</c> for the vtable at <paramref name="vtableAddress"/>.</summary>
    public int? FindNetworkVarStateChangedIndex( ulong vtableAddress );

    /// <summary>Find the vtable index of <c>NetworkStateChanged</c> for the vtable at <paramref name="vtableRva"/>.</summary>
    public int? FindNetworkVarStateChangedIndexRva( ulong vtableRva );

    /// <summary>Cache vtable metadata of a binary. Needed before vtable-based lookups on some binaries.</summary>
    public bool DumpVTables( string binary );

    /// <summary>Get the vtable class name of an object. Returns <c>null</c> when the object has no vtable.</summary>
    public string? GetObjectPtrVtableName( nint objectPtr );

    /// <summary>Check whether an object has a vtable.</summary>
    public bool ObjectPtrHasVtable( nint objectPtr );

    /// <summary>Check whether an object derives from the given class.</summary>
    public bool ObjectPtrHasBaseClass( nint objectPtr, string baseClassName );

    public nint? FindSymbol( string binary, string symbolName );

    public ulong? FindSymbolRva( string binary, string symbolName );

    public nint? FindExport( string binary, string exportName );

    public ulong? FindExportRva( string binary, string exportName );

    /// <summary>Find a string literal inside the binary.</summary>
    public nint? FindString( string binary, string value );

    /// <inheritdoc cref="FindString"/>
    public ulong? FindStringRva( string binary, string value );

    /// <summary>Read <paramref name="size"/> bytes from the binary file at a file offset.</summary>
    public byte[]? ReadByFileOffset( string binary, ulong fileOffset, int size );

    /// <summary>Read <paramref name="size"/> bytes from the binary at an RVA.</summary>
    public byte[]? ReadByRva( string binary, ulong rva, int size );

    /// <summary>Read <paramref name="size"/> bytes from the binary at a runtime address.</summary>
    public byte[]? ReadByAddress( string binary, ulong address, int size );

    /// <summary>Cache every cross-reference of a binary. Needed before the xref-based lookups.</summary>
    public bool DumpXrefs( string binary );

    /// <summary>Get the cached cross-references to <paramref name="targetRva"/>.</summary>
    public IReadOnlyList<nint> GetXrefs( string binary, ulong targetRva );

    /// <summary>Follow the cross-reference at a runtime address and return its target address.</summary>
    public nint? FollowXref( nint address );

    /// <summary>Follow the cross-reference at an RVA and return its target address.</summary>
    public nint? FollowXrefRvaToAddress( string binary, ulong rva );

    /// <summary>Follow the cross-reference at an RVA and return its target RVA.</summary>
    public ulong? FollowXrefRva( string binary, ulong rva );

    /// <summary>Find the start of the function that contains the given RVA.</summary>
    public nint? FindFuncStart( string binary, ulong rva );

    /// <inheritdoc cref="FindFuncStart"/>
    public ulong? FindFuncStartRva( string binary, ulong rva );

    /// <summary>Find the start of the function that contains the given RVA, using cross-references.</summary>
    public nint? FindXrefFuncStart( string binary, ulong rva );

    /// <inheritdoc cref="FindXrefFuncStart"/>
    public ulong? FindXrefFuncStartRva( string binary, ulong rva );

    /// <summary>Find the function that references the given string.</summary>
    public nint? FindFuncWithString( string binary, string value );

    /// <inheritdoc cref="FindFuncWithString"/>
    public ulong? FindFuncWithStringRva( string binary, string value );

    /// <summary>Find the function that references the given string, using cross-references.</summary>
    public nint? FindXrefFuncWithString( string binary, string value );

    /// <inheritdoc cref="FindXrefFuncWithString"/>
    public ulong? FindXrefFuncWithStringRva( string binary, string value );

    /// <summary>Find the call inside <paramref name="funcStart"/> that has an argument referencing <paramref name="target"/> and return the called function.</summary>
    public nint? FindCallWithXrefArg( string binary, nint funcStart, nint target );

    /// <inheritdoc cref="FindCallWithXrefArg"/>
    public ulong? FindCallWithXrefArgRva( string binary, ulong funcStartRva, ulong targetRva );

    /// <summary>Find the call inside <paramref name="funcStart"/> that has the given string as argument and return the called function.</summary>
    public nint? FindCallWithStringArg( string binary, nint funcStart, string value );

    /// <inheritdoc cref="FindCallWithStringArg"/>
    public ulong? FindCallWithStringArgRva( string binary, ulong funcStartRva, string value );

    /// <summary>Build an ida-style signature for the function at <paramref name="funcRva"/>.</summary>
    public string? MakeSignature( string binary, ulong funcRva );

    /// <summary>Install a JIT trampoline at a runtime address and return the trampoline address.</summary>
    public nint? InstallTrampoline( nint address );
}
