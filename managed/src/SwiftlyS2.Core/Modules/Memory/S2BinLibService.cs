using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Memory;

namespace SwiftlyS2.Core.Memory;

internal sealed class S2BinLibService : IS2BinLib
{
    private static T? Ok<T>( int ret, T value ) where T : struct => ret == 0 ? value : null;

    private static nint? Addr( int ret, nint value ) => ret == 0 ? value : null;

    private static ulong? Rva( int ret, nint value ) => ret == 0 ? (ulong)value : null;

    private static byte[]? Read( int size, Func<Memory<byte>, int> read )
    {
        var buffer = new byte[size];
        return read(buffer) == 0 ? buffer : null;
    }

    public nint? GetModuleBase( string binary ) => Addr(NativeS2BinLib.GetModuleBaseAddress(binary, out var r), r);

    public bool SetModuleBase( string binary, nint pointerInsideModule ) => NativeS2BinLib.SetModuleBaseFromPointer(binary, pointerInsideModule) == 0;

    public bool ClearModuleBase( string binary ) => NativeS2BinLib.ClearModuleBaseAddress(binary) == 0;

    public bool SetCustomBinaryPath( string binary, string path, string os ) => NativeS2BinLib.SetCustomBinaryPath(binary, path, os) == 0;

    public string? GetBinaryPath( string binary ) => NativeS2BinLib.GetBinaryPath(binary, out var path) == 0 ? path : null;

    public bool IsBinaryLoaded( string binary ) => NativeS2BinLib.IsBinaryLoaded(binary) == 1;

    public bool LoadBinary( string binary ) => NativeS2BinLib.LoadBinary(binary) == 0;

    public bool UnloadBinary( string binary ) => NativeS2BinLib.UnloadBinary(binary) == 0;

    public nint? PatternScan( string binary, string pattern ) => Addr(NativeS2BinLib.PatternScan(binary, pattern, out var r), r);

    public ulong? PatternScanRva( string binary, string pattern ) => Rva(NativeS2BinLib.PatternScanRva(binary, pattern, out var r), r);

    public IReadOnlyList<nint> PatternScanAll( string binary, string pattern )
    {
        var results = new List<nint>();
        NativeS2BinLib.PatternScanAll(binary, pattern, ( _, address ) =>
        {
            results.Add(address);
            return false;
        });
        return results;
    }

    public IReadOnlyList<ulong> PatternScanAllRva( string binary, string pattern )
    {
        var results = new List<ulong>();
        NativeS2BinLib.PatternScanAllRva(binary, pattern, ( _, rva ) =>
        {
            results.Add((ulong)rva);
            return false;
        });
        return results;
    }

    public nint? FindVTable( string binary, string vtableName )
    {
        var classes = vtableName.Split("::");
        return classes.Length switch {
            1 => Addr(NativeS2BinLib.FindVtable(binary, vtableName, out var r), r),
            2 => Addr(NativeS2BinLib.FindVtableNested2(binary, classes[0], classes[1], out var r), r),
            _ => throw new ArgumentException("Vtable has too many nested classes, which is not supported for now.", nameof(vtableName))
        };
    }

    public ulong? FindVTableRva( string binary, string vtableName )
    {
        var classes = vtableName.Split("::");
        return classes.Length switch {
            1 => Rva(NativeS2BinLib.FindVtableRva(binary, vtableName, out var r), r),
            2 => Rva(NativeS2BinLib.FindVtableNested2Rva(binary, classes[0], classes[1], out var r), r),
            _ => throw new ArgumentException("Vtable has too many nested classes, which is not supported for now.", nameof(vtableName))
        };
    }

    public nint? FindVTableMangled( string binary, string mangledName ) => Addr(NativeS2BinLib.FindVtableMangled(binary, mangledName, out var r), r);

    public ulong? FindVTableMangledRva( string binary, string mangledName ) => Rva(NativeS2BinLib.FindVtableMangledRva(binary, mangledName, out var r), r);

    public int? GetVTableFuncCount( string binary, string vtableName ) => Ok(NativeS2BinLib.GetVtableVfuncCount(binary, vtableName, out var count), count);

    public int? GetVTableFuncCount( string binary, ulong vtableRva ) => Ok(NativeS2BinLib.GetVtableVfuncCountByRva(binary, vtableRva, out var count), count);

    public nint? FindVFunc( string binary, string vtableName, int index ) => Addr(NativeS2BinLib.FindVfuncByVtbname(binary, vtableName, index, out var r), r);

    public ulong? FindVFuncRva( string binary, string vtableName, int index ) => Rva(NativeS2BinLib.FindVfuncByVtbnameRva(binary, vtableName, index, out var r), r);

    public nint? FindVFunc( nint vtable, int index ) => Addr(NativeS2BinLib.FindVfuncByVtbptr(vtable, index, out var r), r);

    public ulong? FindVFuncRva( nint vtable, int index ) => Rva(NativeS2BinLib.FindVfuncByVtbptrRva(vtable, index, out var r), r);

    public S2VFunc? FindVFuncContaining( string binary, ulong rva ) =>
        Ok(NativeS2BinLib.FindVfuncStart(binary, rva, out var name, out var index, out var address), new S2VFunc(name, index, address));

    public S2VFuncRva? FindVFuncContainingRva( string binary, ulong rva ) =>
        Ok(NativeS2BinLib.FindVfuncStartRva(binary, rva, out var name, out var index, out var funcRva), new S2VFuncRva(name, index, funcRva));

    public S2VFunc? FindVFuncWithString( string binary, string value ) =>
        Ok(NativeS2BinLib.FindVfuncWithString(binary, value, out var name, out var index, out var address), new S2VFunc(name, index, address));

    public S2VFuncRva? FindVFuncWithStringRva( string binary, string value ) =>
        Ok(NativeS2BinLib.FindVfuncWithStringRva(binary, value, out var name, out var index, out var funcRva), new S2VFuncRva(name, index, funcRva));

    public int? FindNetworkVarStateChangedIndex( ulong vtableAddress ) => Ok(NativeS2BinLib.FindNetworkvarVtableStatechanged(vtableAddress, out var r), (int)r);

    public int? FindNetworkVarStateChangedIndexRva( ulong vtableRva ) => Ok(NativeS2BinLib.FindNetworkvarVtableStatechangedRva(vtableRva, out var r), (int)r);

    public bool DumpVTables( string binary ) => NativeS2BinLib.DumpVtables(binary) == 0;

    public string? GetObjectPtrVtableName( nint objectPtr ) =>
        NativeS2BinLib.GetObjectPtrVtableName(objectPtr, out var name) == 0 && name != string.Empty ? name : null;

    public bool ObjectPtrHasVtable( nint objectPtr ) => NativeS2BinLib.ObjectPtrHasVtable(objectPtr) == 1;

    public bool ObjectPtrHasBaseClass( nint objectPtr, string baseClassName ) => NativeS2BinLib.ObjectPtrHasBaseClass(objectPtr, baseClassName) == 1;

    public nint? FindSymbol( string binary, string symbolName ) => Addr(NativeS2BinLib.FindSymbol(binary, symbolName, out var r), r);

    public ulong? FindSymbolRva( string binary, string symbolName ) => Rva(NativeS2BinLib.FindSymbolRva(binary, symbolName, out var r), r);

    public nint? FindExport( string binary, string exportName ) => Addr(NativeS2BinLib.FindExport(binary, exportName, out var r), r);

    public ulong? FindExportRva( string binary, string exportName ) => Rva(NativeS2BinLib.FindExportRva(binary, exportName, out var r), r);

    public nint? FindString( string binary, string value ) => Addr(NativeS2BinLib.FindString(binary, value, out var r), r);

    public ulong? FindStringRva( string binary, string value ) => Rva(NativeS2BinLib.FindStringRva(binary, value, out var r), r);

    public byte[]? ReadByFileOffset( string binary, ulong fileOffset, int size ) =>
        Read(size, buffer => NativeS2BinLib.ReadByFileOffset(binary, fileOffset, buffer.Span));

    public byte[]? ReadByRva( string binary, ulong rva, int size ) =>
        Read(size, buffer => NativeS2BinLib.ReadByRva(binary, rva, buffer.Span));

    public byte[]? ReadByAddress( string binary, ulong address, int size ) =>
        Read(size, buffer => NativeS2BinLib.ReadByMemAddress(binary, address, buffer.Span));

    public bool DumpXrefs( string binary ) => NativeS2BinLib.DumpXrefs(binary) == 0;

    public IReadOnlyList<nint> GetXrefs( string binary, ulong targetRva )
    {
        var count = NativeS2BinLib.GetXrefsCount(binary, (nint)targetRva);
        if (count <= 0)
        {
            return [];
        }

        var buffer = new nint[count];
        var ret = NativeS2BinLib.GetXrefsCached(binary, (nint)targetRva, buffer);
        return ret < 0 ? [] : buffer;
    }

    public nint? FollowXref( nint address ) => Addr(NativeS2BinLib.FollowXrefMemToMem(address, out var r), r);

    public nint? FollowXrefRvaToAddress( string binary, ulong rva ) => Addr(NativeS2BinLib.FollowXrefRvaToMem(binary, rva, out var r), r);

    public ulong? FollowXrefRva( string binary, ulong rva ) => Ok(NativeS2BinLib.FollowXrefRvaToRva(binary, rva, out var r), r);

    public nint? FindFuncStart( string binary, ulong rva ) => Addr(NativeS2BinLib.FindFuncStart(binary, rva, out var r), r);

    public ulong? FindFuncStartRva( string binary, ulong rva ) => Ok(NativeS2BinLib.FindFuncStartRva(binary, rva, out var r), r);

    public nint? FindXrefFuncStart( string binary, ulong rva ) => Addr(NativeS2BinLib.FindXrefFuncStart(binary, rva, out var r), r);

    public ulong? FindXrefFuncStartRva( string binary, ulong rva ) => Ok(NativeS2BinLib.FindXrefFuncStartRva(binary, rva, out var r), r);

    public nint? FindFuncWithString( string binary, string value ) => Addr(NativeS2BinLib.FindFuncWithString(binary, value, out var r), r);

    public ulong? FindFuncWithStringRva( string binary, string value ) => Ok(NativeS2BinLib.FindFuncWithStringRva(binary, value, out var r), r);

    public nint? FindXrefFuncWithString( string binary, string value ) => Addr(NativeS2BinLib.FindXrefFuncWithString(binary, value, out var r), r);

    public ulong? FindXrefFuncWithStringRva( string binary, string value ) => Ok(NativeS2BinLib.FindXrefFuncWithStringRva(binary, value, out var r), r);

    public nint? FindCallWithXrefArg( string binary, nint funcStart, nint target ) => Addr(NativeS2BinLib.FindCallWithXrefArg(binary, funcStart, target, out var r), r);

    public ulong? FindCallWithXrefArgRva( string binary, ulong funcStartRva, ulong targetRva ) => Ok(NativeS2BinLib.FindCallWithXrefArgRva(binary, funcStartRva, targetRva, out var r), r);

    public nint? FindCallWithStringArg( string binary, nint funcStart, string value ) => Addr(NativeS2BinLib.FindCallWithStringArg(binary, funcStart, value, out var r), r);

    public ulong? FindCallWithStringArgRva( string binary, ulong funcStartRva, string value ) => Ok(NativeS2BinLib.FindCallWithStringArgRva(binary, funcStartRva, value, out var r), r);

    public string? MakeSignature( string binary, ulong funcRva ) => NativeS2BinLib.MakeSigRva(binary, funcRva, out var signature) == 0 ? signature : null;

    public nint? InstallTrampoline( nint address ) => Addr(NativeS2BinLib.InstallTrampoline(address, out var r), r);
}
