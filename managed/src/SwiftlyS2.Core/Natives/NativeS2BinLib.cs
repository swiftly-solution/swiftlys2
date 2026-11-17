#pragma warning disable CS0649
#pragma warning disable CS0169

using System.Runtime.InteropServices;
using SwiftlyS2.Core.Misc;

namespace SwiftlyS2.Core.Natives;

/// <summary>
/// Managed binding for the native S2BinLib004 instance (vendor/s2binlib/s2binlib.h).
/// Every method returns the native status code (0 = success, negative = error)
/// and hands results back through `out` parameters, mirroring the header.
/// "Rva" variants return offsets relative to the module base, the others return runtime addresses.
/// </summary>
internal static class NativeS2BinLib
{
    private static readonly nint _self = GlobalState.S2BinLibPtr;

    private unsafe static readonly S2BinLibVTable* _vtable = _self == 0 ? throw new InvalidOperationException("S2BinLib instance was not provided by the native core.") : *(S2BinLibVTable**)_self;

    private sealed class ScanState
    {
        public required Func<int, nint, bool> Callback;
        public Exception? Error;
    }

    [UnmanagedCallersOnly]
    private static byte OnPatternMatch(nuint index, nint address, nint userData)
    {
        var state = (ScanState)GCHandle.FromIntPtr(userData).Target!;
        try
        {
            return state.Callback((int)index, address) ? (byte)1 : (byte)0;
        }
        catch (Exception e)
        {
            state.Error = e;
            return 1;
        }
    }

    public unsafe static int Initialize(string gamePath, string gameType)
    {
        using var gamePathStr = new ScopedCString(gamePath);
        using var gameTypeStr = new ScopedCString(gameType);
        fixed (byte* gamePathBufferPtr = gamePathStr)
        {
            fixed (byte* gameTypeBufferPtr = gameTypeStr)
            {
                var ret = _vtable->Initialize(_self, gamePathBufferPtr, gameTypeBufferPtr);
                return ret;
            }
        }
    }

    public unsafe static int InitializeWithOs(string gamePath, string gameType, string os)
    {
        using var gamePathStr = new ScopedCString(gamePath);
        using var gameTypeStr = new ScopedCString(gameType);
        using var osStr = new ScopedCString(os);
        fixed (byte* gamePathBufferPtr = gamePathStr)
        {
            fixed (byte* gameTypeBufferPtr = gameTypeStr)
            {
                fixed (byte* osBufferPtr = osStr)
                {
                    var ret = _vtable->InitializeWithOs(_self, gamePathBufferPtr, gameTypeBufferPtr, osBufferPtr);
                    return ret;
                }
            }
        }
    }

    public unsafe static int PatternScan(string binary, string pattern, out nint address)
    {
        using var binaryStr = new ScopedCString(binary);
        using var patternStr = new ScopedCString(pattern);
        nint addressValue = 0;
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            fixed (byte* patternBufferPtr = patternStr)
            {
                var ret = _vtable->PatternScan(_self, binaryBufferPtr, patternBufferPtr, &addressValue);
                address = addressValue;
                return ret;
            }
        }
    }

    public unsafe static int FindVtable(string binary, string vtableName, out nint address)
    {
        using var binaryStr = new ScopedCString(binary);
        using var vtableNameStr = new ScopedCString(vtableName);
        nint addressValue = 0;
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            fixed (byte* vtableNameBufferPtr = vtableNameStr)
            {
                var ret = _vtable->FindVtable(_self, binaryBufferPtr, vtableNameBufferPtr, &addressValue);
                address = addressValue;
                return ret;
            }
        }
    }

    public unsafe static int FindSymbol(string binary, string symbolName, out nint address)
    {
        using var binaryStr = new ScopedCString(binary);
        using var symbolNameStr = new ScopedCString(symbolName);
        nint addressValue = 0;
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            fixed (byte* symbolNameBufferPtr = symbolNameStr)
            {
                var ret = _vtable->FindSymbol(_self, binaryBufferPtr, symbolNameBufferPtr, &addressValue);
                address = addressValue;
                return ret;
            }
        }
    }

    public unsafe static int SetModuleBaseFromPointer(string binary, nint pointer)
    {
        using var binaryStr = new ScopedCString(binary);
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            var ret = _vtable->SetModuleBaseFromPointer(_self, binaryBufferPtr, pointer);
            return ret;
        }
    }

    public unsafe static int ClearModuleBaseAddress(string binary)
    {
        using var binaryStr = new ScopedCString(binary);
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            var ret = _vtable->ClearModuleBaseAddress(_self, binaryBufferPtr);
            return ret;
        }
    }

    public unsafe static int SetCustomBinaryPath(string binary, string path, string os)
    {
        using var binaryStr = new ScopedCString(binary);
        using var pathStr = new ScopedCString(path);
        using var osStr = new ScopedCString(os);
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            fixed (byte* pathBufferPtr = pathStr)
            {
                fixed (byte* osBufferPtr = osStr)
                {
                    var ret = _vtable->SetCustomBinaryPath(_self, binaryBufferPtr, pathBufferPtr, osBufferPtr);
                    return ret;
                }
            }
        }
    }

    public unsafe static int GetModuleBaseAddress(string binary, out nint baseAddress)
    {
        using var binaryStr = new ScopedCString(binary);
        nint baseAddressValue = 0;
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            var ret = _vtable->GetModuleBaseAddress(_self, binaryBufferPtr, &baseAddressValue);
            baseAddress = baseAddressValue;
            return ret;
        }
    }

    /// <summary>
    /// Returns 1 if loaded, 0 if not, negative on error.
    /// </summary>
    public unsafe static int IsBinaryLoaded(string binary)
    {
        using var binaryStr = new ScopedCString(binary);
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            var ret = _vtable->IsBinaryLoaded(_self, binaryBufferPtr);
            return ret;
        }
    }

    public unsafe static int LoadBinary(string binary)
    {
        using var binaryStr = new ScopedCString(binary);
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            var ret = _vtable->LoadBinary(_self, binaryBufferPtr);
            return ret;
        }
    }

    public unsafe static int GetBinaryPath(string binary, out string path)
    {
        using var binaryStr = new ScopedCString(binary);
        var pathBuffer = stackalloc byte[4096];
        pathBuffer[0] = 0;
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            var ret = _vtable->GetBinaryPath(_self, binaryBufferPtr, pathBuffer, 4096);
            path = ret == 0 ? Marshal.PtrToStringUTF8((nint)pathBuffer) ?? string.Empty : string.Empty;
            return ret;
        }
    }

    public unsafe static int FindVtableRva(string binary, string vtableName, out nint rva)
    {
        using var binaryStr = new ScopedCString(binary);
        using var vtableNameStr = new ScopedCString(vtableName);
        nint rvaValue = 0;
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            fixed (byte* vtableNameBufferPtr = vtableNameStr)
            {
                var ret = _vtable->FindVtableRva(_self, binaryBufferPtr, vtableNameBufferPtr, &rvaValue);
                rva = rvaValue;
                return ret;
            }
        }
    }

    public unsafe static int FindVtableMangledRva(string binary, string vtableName, out nint rva)
    {
        using var binaryStr = new ScopedCString(binary);
        using var vtableNameStr = new ScopedCString(vtableName);
        nint rvaValue = 0;
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            fixed (byte* vtableNameBufferPtr = vtableNameStr)
            {
                var ret = _vtable->FindVtableMangledRva(_self, binaryBufferPtr, vtableNameBufferPtr, &rvaValue);
                rva = rvaValue;
                return ret;
            }
        }
    }

    public unsafe static int FindVtableMangled(string binary, string vtableName, out nint address)
    {
        using var binaryStr = new ScopedCString(binary);
        using var vtableNameStr = new ScopedCString(vtableName);
        nint addressValue = 0;
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            fixed (byte* vtableNameBufferPtr = vtableNameStr)
            {
                var ret = _vtable->FindVtableMangled(_self, binaryBufferPtr, vtableNameBufferPtr, &addressValue);
                address = addressValue;
                return ret;
            }
        }
    }

    public unsafe static int FindVtableNested2Rva(string binary, string class1, string class2, out nint rva)
    {
        using var binaryStr = new ScopedCString(binary);
        using var class1Str = new ScopedCString(class1);
        using var class2Str = new ScopedCString(class2);
        nint rvaValue = 0;
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            fixed (byte* class1BufferPtr = class1Str)
            {
                fixed (byte* class2BufferPtr = class2Str)
                {
                    var ret = _vtable->FindVtableNested2Rva(_self, binaryBufferPtr, class1BufferPtr, class2BufferPtr, &rvaValue);
                    rva = rvaValue;
                    return ret;
                }
            }
        }
    }

    public unsafe static int FindVtableNested2(string binary, string class1, string class2, out nint address)
    {
        using var binaryStr = new ScopedCString(binary);
        using var class1Str = new ScopedCString(class1);
        using var class2Str = new ScopedCString(class2);
        nint addressValue = 0;
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            fixed (byte* class1BufferPtr = class1Str)
            {
                fixed (byte* class2BufferPtr = class2Str)
                {
                    var ret = _vtable->FindVtableNested2(_self, binaryBufferPtr, class1BufferPtr, class2BufferPtr, &addressValue);
                    address = addressValue;
                    return ret;
                }
            }
        }
    }

    public unsafe static int GetVtableVfuncCount(string binary, string vtableName, out int count)
    {
        using var binaryStr = new ScopedCString(binary);
        using var vtableNameStr = new ScopedCString(vtableName);
        nuint countValue = 0;
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            fixed (byte* vtableNameBufferPtr = vtableNameStr)
            {
                var ret = _vtable->GetVtableVfuncCount(_self, binaryBufferPtr, vtableNameBufferPtr, &countValue);
                count = (int)countValue;
                return ret;
            }
        }
    }

    public unsafe static int GetVtableVfuncCountByRva(string binary, ulong vtableRva, out int count)
    {
        using var binaryStr = new ScopedCString(binary);
        nuint countValue = 0;
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            var ret = _vtable->GetVtableVfuncCountByRva(_self, binaryBufferPtr, vtableRva, &countValue);
            count = (int)countValue;
            return ret;
        }
    }

    public unsafe static int PatternScanRva(string binary, string pattern, out nint rva)
    {
        using var binaryStr = new ScopedCString(binary);
        using var patternStr = new ScopedCString(pattern);
        nint rvaValue = 0;
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            fixed (byte* patternBufferPtr = patternStr)
            {
                var ret = _vtable->PatternScanRva(_self, binaryBufferPtr, patternBufferPtr, &rvaValue);
                rva = rvaValue;
                return ret;
            }
        }
    }

    /// <summary>
    /// Callback is (matchIndex, rva); return true to stop scanning.
    /// </summary>
    public unsafe static int PatternScanAllRva(string binary, string pattern, Func<int, nint, bool> callback)
    {
        using var binaryStr = new ScopedCString(binary);
        using var patternStr = new ScopedCString(pattern);
        var state = new ScanState { Callback = callback };
        var handle = GCHandle.Alloc(state);
        try
        {
            fixed (byte* binaryBufferPtr = binaryStr)
            {
                fixed (byte* patternBufferPtr = patternStr)
                {
                    var ret = _vtable->PatternScanAllRva(_self, binaryBufferPtr, patternBufferPtr, (nint)(delegate* unmanaged<nuint, nint, nint, byte>)&OnPatternMatch, GCHandle.ToIntPtr(handle));
                    if (state.Error is { } error)
                    {
                        throw new InvalidOperationException("Pattern scan callback threw.", error);
                    }
                    return ret;
                }
            }
        }
        finally
        {
            handle.Free();
        }
    }

    /// <summary>
    /// Callback is (matchIndex, address); return true to stop scanning.
    /// </summary>
    public unsafe static int PatternScanAll(string binary, string pattern, Func<int, nint, bool> callback)
    {
        using var binaryStr = new ScopedCString(binary);
        using var patternStr = new ScopedCString(pattern);
        var state = new ScanState { Callback = callback };
        var handle = GCHandle.Alloc(state);
        try
        {
            fixed (byte* binaryBufferPtr = binaryStr)
            {
                fixed (byte* patternBufferPtr = patternStr)
                {
                    var ret = _vtable->PatternScanAll(_self, binaryBufferPtr, patternBufferPtr, (nint)(delegate* unmanaged<nuint, nint, nint, byte>)&OnPatternMatch, GCHandle.ToIntPtr(handle));
                    if (state.Error is { } error)
                    {
                        throw new InvalidOperationException("Pattern scan callback threw.", error);
                    }
                    return ret;
                }
            }
        }
        finally
        {
            handle.Free();
        }
    }

    public unsafe static int FindExportRva(string binary, string exportName, out nint rva)
    {
        using var binaryStr = new ScopedCString(binary);
        using var exportNameStr = new ScopedCString(exportName);
        nint rvaValue = 0;
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            fixed (byte* exportNameBufferPtr = exportNameStr)
            {
                var ret = _vtable->FindExportRva(_self, binaryBufferPtr, exportNameBufferPtr, &rvaValue);
                rva = rvaValue;
                return ret;
            }
        }
    }

    public unsafe static int FindExport(string binary, string exportName, out nint address)
    {
        using var binaryStr = new ScopedCString(binary);
        using var exportNameStr = new ScopedCString(exportName);
        nint addressValue = 0;
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            fixed (byte* exportNameBufferPtr = exportNameStr)
            {
                var ret = _vtable->FindExport(_self, binaryBufferPtr, exportNameBufferPtr, &addressValue);
                address = addressValue;
                return ret;
            }
        }
    }

    public unsafe static int FindSymbolRva(string binary, string symbolName, out nint rva)
    {
        using var binaryStr = new ScopedCString(binary);
        using var symbolNameStr = new ScopedCString(symbolName);
        nint rvaValue = 0;
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            fixed (byte* symbolNameBufferPtr = symbolNameStr)
            {
                var ret = _vtable->FindSymbolRva(_self, binaryBufferPtr, symbolNameBufferPtr, &rvaValue);
                rva = rvaValue;
                return ret;
            }
        }
    }

    public unsafe static int ReadByFileOffset(string binary, ulong fileOffset, Span<byte> buffer)
    {
        using var binaryStr = new ScopedCString(binary);
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            fixed (byte* bufferBufferPtr = buffer)
            {
                var ret = _vtable->ReadByFileOffset(_self, binaryBufferPtr, fileOffset, bufferBufferPtr, (nuint)buffer.Length);
                return ret;
            }
        }
    }

    public unsafe static int ReadByRva(string binary, ulong rva, Span<byte> buffer)
    {
        using var binaryStr = new ScopedCString(binary);
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            fixed (byte* bufferBufferPtr = buffer)
            {
                var ret = _vtable->ReadByRva(_self, binaryBufferPtr, rva, bufferBufferPtr, (nuint)buffer.Length);
                return ret;
            }
        }
    }

    public unsafe static int ReadByMemAddress(string binary, ulong memAddress, Span<byte> buffer)
    {
        using var binaryStr = new ScopedCString(binary);
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            fixed (byte* bufferBufferPtr = buffer)
            {
                var ret = _vtable->ReadByMemAddress(_self, binaryBufferPtr, memAddress, bufferBufferPtr, (nuint)buffer.Length);
                return ret;
            }
        }
    }

    public unsafe static int FindVfuncByVtbnameRva(string binary, string vtableName, int vfuncIndex, out nint rva)
    {
        using var binaryStr = new ScopedCString(binary);
        using var vtableNameStr = new ScopedCString(vtableName);
        nint rvaValue = 0;
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            fixed (byte* vtableNameBufferPtr = vtableNameStr)
            {
                var ret = _vtable->FindVfuncByVtbnameRva(_self, binaryBufferPtr, vtableNameBufferPtr, (nuint)vfuncIndex, &rvaValue);
                rva = rvaValue;
                return ret;
            }
        }
    }

    public unsafe static int FindVfuncByVtbname(string binary, string vtableName, int vfuncIndex, out nint address)
    {
        using var binaryStr = new ScopedCString(binary);
        using var vtableNameStr = new ScopedCString(vtableName);
        nint addressValue = 0;
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            fixed (byte* vtableNameBufferPtr = vtableNameStr)
            {
                var ret = _vtable->FindVfuncByVtbname(_self, binaryBufferPtr, vtableNameBufferPtr, (nuint)vfuncIndex, &addressValue);
                address = addressValue;
                return ret;
            }
        }
    }

    public unsafe static int FindVfuncByVtbptrRva(nint vtablePtr, int vfuncIndex, out nint rva)
    {
        nint rvaValue = 0;
        var ret = _vtable->FindVfuncByVtbptrRva(_self, vtablePtr, (nuint)vfuncIndex, &rvaValue);
        rva = rvaValue;
        return ret;
    }

    public unsafe static int FindVfuncByVtbptr(nint vtablePtr, int vfuncIndex, out nint address)
    {
        nint addressValue = 0;
        var ret = _vtable->FindVfuncByVtbptr(_self, vtablePtr, (nuint)vfuncIndex, &addressValue);
        address = addressValue;
        return ret;
    }

    public unsafe static int GetObjectPtrVtableName(nint objectPtr, out string vtableName)
    {
        var vtableNameBuffer = stackalloc byte[1024];
        vtableNameBuffer[0] = 0;
        var ret = _vtable->GetObjectPtrVtableName(_self, objectPtr, vtableNameBuffer, 1024);
        vtableName = ret == 0 ? Marshal.PtrToStringUTF8((nint)vtableNameBuffer) ?? string.Empty : string.Empty;
        return ret;
    }

    /// <summary>
    /// Returns 1 if the object has a vtable, 0 if not, negative on error.
    /// </summary>
    public unsafe static int ObjectPtrHasVtable(nint objectPtr)
    {
        var ret = _vtable->ObjectPtrHasVtable(_self, objectPtr);
        return ret;
    }

    /// <summary>
    /// Returns 1 if the object derives from the class, 0 if not, negative on error.
    /// </summary>
    public unsafe static int ObjectPtrHasBaseClass(nint objectPtr, string baseClassName)
    {
        using var baseClassNameStr = new ScopedCString(baseClassName);
        fixed (byte* baseClassNameBufferPtr = baseClassNameStr)
        {
            var ret = _vtable->ObjectPtrHasBaseClass(_self, objectPtr, baseClassNameBufferPtr);
            return ret;
        }
    }

    public unsafe static int FindStringRva(string binary, string value, out nint rva)
    {
        using var binaryStr = new ScopedCString(binary);
        using var valueStr = new ScopedCString(value);
        nint rvaValue = 0;
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            fixed (byte* valueBufferPtr = valueStr)
            {
                var ret = _vtable->FindStringRva(_self, binaryBufferPtr, valueBufferPtr, &rvaValue);
                rva = rvaValue;
                return ret;
            }
        }
    }

    public unsafe static int FindString(string binary, string value, out nint address)
    {
        using var binaryStr = new ScopedCString(binary);
        using var valueStr = new ScopedCString(value);
        nint addressValue = 0;
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            fixed (byte* valueBufferPtr = valueStr)
            {
                var ret = _vtable->FindString(_self, binaryBufferPtr, valueBufferPtr, &addressValue);
                address = addressValue;
                return ret;
            }
        }
    }

    public unsafe static int DumpXrefs(string binary)
    {
        using var binaryStr = new ScopedCString(binary);
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            var ret = _vtable->DumpXrefs(_self, binaryBufferPtr);
            return ret;
        }
    }

    /// <summary>
    /// Returns the number of cached xrefs to targetRva, negative on error.
    /// </summary>
    public unsafe static int GetXrefsCount(string binary, nint targetRva)
    {
        using var binaryStr = new ScopedCString(binary);
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            var ret = _vtable->GetXrefsCount(_self, binaryBufferPtr, targetRva);
            return ret;
        }
    }

    public unsafe static int GetXrefsCached(string binary, nint targetRva, Span<nint> buffer)
    {
        using var binaryStr = new ScopedCString(binary);
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            fixed (nint* bufferBufferPtr = buffer)
            {
                var ret = _vtable->GetXrefsCached(_self, binaryBufferPtr, targetRva, bufferBufferPtr, (nuint)buffer.Length);
                return ret;
            }
        }
    }

    public unsafe static int UnloadBinary(string binary)
    {
        using var binaryStr = new ScopedCString(binary);
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            var ret = _vtable->UnloadBinary(_self, binaryBufferPtr);
            return ret;
        }
    }

    public unsafe static int UnloadAllBinaries()
    {
        var ret = _vtable->UnloadAllBinaries(_self);
        return ret;
    }

    public unsafe static int InstallTrampoline(nint memAddress, out nint trampolineAddress)
    {
        nint trampolineAddressValue = 0;
        var ret = _vtable->InstallTrampoline(_self, memAddress, &trampolineAddressValue);
        trampolineAddress = trampolineAddressValue;
        return ret;
    }

    public unsafe static int FollowXrefMemToMem(nint memAddress, out nint targetAddress)
    {
        nint targetAddressValue = 0;
        var ret = _vtable->FollowXrefMemToMem(_self, memAddress, &targetAddressValue);
        targetAddress = targetAddressValue;
        return ret;
    }

    public unsafe static int FollowXrefRvaToMem(string binary, ulong rva, out nint targetAddress)
    {
        using var binaryStr = new ScopedCString(binary);
        nint targetAddressValue = 0;
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            var ret = _vtable->FollowXrefRvaToMem(_self, binaryBufferPtr, rva, &targetAddressValue);
            targetAddress = targetAddressValue;
            return ret;
        }
    }

    public unsafe static int FollowXrefRvaToRva(string binary, ulong rva, out ulong targetRva)
    {
        using var binaryStr = new ScopedCString(binary);
        ulong targetRvaValue = 0;
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            var ret = _vtable->FollowXrefRvaToRva(_self, binaryBufferPtr, rva, &targetRvaValue);
            targetRva = targetRvaValue;
            return ret;
        }
    }

    public unsafe static int DumpVtables(string binary)
    {
        using var binaryStr = new ScopedCString(binary);
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            var ret = _vtable->DumpVtables(_self, binaryBufferPtr);
            return ret;
        }
    }

    public unsafe static int FindVfuncStartRva(string binary, ulong includeRva, out string vtableName, out int vfuncIndex, out ulong vfuncRva)
    {
        using var binaryStr = new ScopedCString(binary);
        var vtableNameBuffer = stackalloc byte[1024];
        vtableNameBuffer[0] = 0;
        nuint vfuncIndexValue = 0;
        ulong vfuncRvaValue = 0;
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            var ret = _vtable->FindVfuncStartRva(_self, binaryBufferPtr, includeRva, vtableNameBuffer, 1024, &vfuncIndexValue, &vfuncRvaValue);
            vtableName = ret == 0 ? Marshal.PtrToStringUTF8((nint)vtableNameBuffer) ?? string.Empty : string.Empty;
            vfuncIndex = (int)vfuncIndexValue;
            vfuncRva = vfuncRvaValue;
            return ret;
        }
    }

    public unsafe static int FindVfuncStart(string binary, ulong includeRva, out string vtableName, out int vfuncIndex, out nint address)
    {
        using var binaryStr = new ScopedCString(binary);
        var vtableNameBuffer = stackalloc byte[1024];
        vtableNameBuffer[0] = 0;
        nuint vfuncIndexValue = 0;
        nint addressValue = 0;
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            var ret = _vtable->FindVfuncStart(_self, binaryBufferPtr, includeRva, vtableNameBuffer, 1024, &vfuncIndexValue, &addressValue);
            vtableName = ret == 0 ? Marshal.PtrToStringUTF8((nint)vtableNameBuffer) ?? string.Empty : string.Empty;
            vfuncIndex = (int)vfuncIndexValue;
            address = addressValue;
            return ret;
        }
    }

    public unsafe static int FindXrefFuncStartRva(string binary, ulong includeRva, out ulong funcRva)
    {
        using var binaryStr = new ScopedCString(binary);
        ulong funcRvaValue = 0;
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            var ret = _vtable->FindXrefFuncStartRva(_self, binaryBufferPtr, includeRva, &funcRvaValue);
            funcRva = funcRvaValue;
            return ret;
        }
    }

    public unsafe static int FindXrefFuncStart(string binary, ulong includeRva, out nint address)
    {
        using var binaryStr = new ScopedCString(binary);
        nint addressValue = 0;
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            var ret = _vtable->FindXrefFuncStart(_self, binaryBufferPtr, includeRva, &addressValue);
            address = addressValue;
            return ret;
        }
    }

    public unsafe static int FindXrefFuncWithString(string binary, string value, out nint address)
    {
        using var binaryStr = new ScopedCString(binary);
        using var valueStr = new ScopedCString(value);
        nint addressValue = 0;
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            fixed (byte* valueBufferPtr = valueStr)
            {
                var ret = _vtable->FindXrefFuncWithString(_self, binaryBufferPtr, valueBufferPtr, &addressValue);
                address = addressValue;
                return ret;
            }
        }
    }

    public unsafe static int FindFuncStartRva(string binary, ulong includeRva, out ulong funcRva)
    {
        using var binaryStr = new ScopedCString(binary);
        ulong funcRvaValue = 0;
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            var ret = _vtable->FindFuncStartRva(_self, binaryBufferPtr, includeRva, &funcRvaValue);
            funcRva = funcRvaValue;
            return ret;
        }
    }

    public unsafe static int FindFuncStart(string binary, ulong includeRva, out nint address)
    {
        using var binaryStr = new ScopedCString(binary);
        nint addressValue = 0;
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            var ret = _vtable->FindFuncStart(_self, binaryBufferPtr, includeRva, &addressValue);
            address = addressValue;
            return ret;
        }
    }

    public unsafe static int FindXrefFuncWithStringRva(string binary, string value, out ulong funcRva)
    {
        using var binaryStr = new ScopedCString(binary);
        using var valueStr = new ScopedCString(value);
        ulong funcRvaValue = 0;
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            fixed (byte* valueBufferPtr = valueStr)
            {
                var ret = _vtable->FindXrefFuncWithStringRva(_self, binaryBufferPtr, valueBufferPtr, &funcRvaValue);
                funcRva = funcRvaValue;
                return ret;
            }
        }
    }

    public unsafe static int FindVfuncWithStringRva(string binary, string value, out string vtableName, out int vfuncIndex, out ulong vfuncRva)
    {
        using var binaryStr = new ScopedCString(binary);
        using var valueStr = new ScopedCString(value);
        var vtableNameBuffer = stackalloc byte[1024];
        vtableNameBuffer[0] = 0;
        nuint vfuncIndexValue = 0;
        ulong vfuncRvaValue = 0;
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            fixed (byte* valueBufferPtr = valueStr)
            {
                var ret = _vtable->FindVfuncWithStringRva(_self, binaryBufferPtr, valueBufferPtr, vtableNameBuffer, 1024, &vfuncIndexValue, &vfuncRvaValue);
                vtableName = ret == 0 ? Marshal.PtrToStringUTF8((nint)vtableNameBuffer) ?? string.Empty : string.Empty;
                vfuncIndex = (int)vfuncIndexValue;
                vfuncRva = vfuncRvaValue;
                return ret;
            }
        }
    }

    public unsafe static int FindVfuncWithString(string binary, string value, out string vtableName, out int vfuncIndex, out nint address)
    {
        using var binaryStr = new ScopedCString(binary);
        using var valueStr = new ScopedCString(value);
        var vtableNameBuffer = stackalloc byte[1024];
        vtableNameBuffer[0] = 0;
        nuint vfuncIndexValue = 0;
        nint addressValue = 0;
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            fixed (byte* valueBufferPtr = valueStr)
            {
                var ret = _vtable->FindVfuncWithString(_self, binaryBufferPtr, valueBufferPtr, vtableNameBuffer, 1024, &vfuncIndexValue, &addressValue);
                vtableName = ret == 0 ? Marshal.PtrToStringUTF8((nint)vtableNameBuffer) ?? string.Empty : string.Empty;
                vfuncIndex = (int)vfuncIndexValue;
                address = addressValue;
                return ret;
            }
        }
    }

    public unsafe static int FindFuncWithStringRva(string binary, string value, out ulong funcRva)
    {
        using var binaryStr = new ScopedCString(binary);
        using var valueStr = new ScopedCString(value);
        ulong funcRvaValue = 0;
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            fixed (byte* valueBufferPtr = valueStr)
            {
                var ret = _vtable->FindFuncWithStringRva(_self, binaryBufferPtr, valueBufferPtr, &funcRvaValue);
                funcRva = funcRvaValue;
                return ret;
            }
        }
    }

    public unsafe static int FindFuncWithString(string binary, string value, out nint address)
    {
        using var binaryStr = new ScopedCString(binary);
        using var valueStr = new ScopedCString(value);
        nint addressValue = 0;
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            fixed (byte* valueBufferPtr = valueStr)
            {
                var ret = _vtable->FindFuncWithString(_self, binaryBufferPtr, valueBufferPtr, &addressValue);
                address = addressValue;
                return ret;
            }
        }
    }

    public unsafe static int FindNetworkvarVtableStatechangedRva(ulong vtableRva, out ulong index)
    {
        ulong indexValue = 0;
        var ret = _vtable->FindNetworkvarVtableStatechangedRva(_self, vtableRva, &indexValue);
        index = indexValue;
        return ret;
    }

    public unsafe static int FindNetworkvarVtableStatechanged(ulong vtableMemAddress, out ulong index)
    {
        ulong indexValue = 0;
        var ret = _vtable->FindNetworkvarVtableStatechanged(_self, vtableMemAddress, &indexValue);
        index = indexValue;
        return ret;
    }

    public unsafe static int MakeSigRva(string binary, ulong funcRva, out string signature)
    {
        using var binaryStr = new ScopedCString(binary);
        var signatureBuffer = stackalloc byte[1024];
        signatureBuffer[0] = 0;
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            var ret = _vtable->MakeSigRva(_self, binaryBufferPtr, funcRva, signatureBuffer, 1024);
            signature = ret == 0 ? Marshal.PtrToStringUTF8((nint)signatureBuffer) ?? string.Empty : string.Empty;
            return ret;
        }
    }

    /// <summary>
    /// Destroys the native instance. The core owns it: do not call this unless you own the lifetime.
    /// </summary>
    public unsafe static int Destroy()
    {
        var ret = _vtable->Destroy(_self);
        return ret;
    }

    public unsafe static int FindCallWithXrefArgRva(string binary, ulong funcStartRva, ulong targetRva, out ulong calleeRva)
    {
        using var binaryStr = new ScopedCString(binary);
        ulong calleeRvaValue = 0;
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            var ret = _vtable->FindCallWithXrefArgRva(_self, binaryBufferPtr, funcStartRva, targetRva, &calleeRvaValue);
            calleeRva = calleeRvaValue;
            return ret;
        }
    }

    public unsafe static int FindCallWithXrefArg(string binary, nint funcStart, nint target, out nint calleeAddress)
    {
        using var binaryStr = new ScopedCString(binary);
        nint calleeAddressValue = 0;
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            var ret = _vtable->FindCallWithXrefArg(_self, binaryBufferPtr, funcStart, target, &calleeAddressValue);
            calleeAddress = calleeAddressValue;
            return ret;
        }
    }

    public unsafe static int FindCallWithStringArgRva(string binary, ulong funcStartRva, string value, out ulong calleeRva)
    {
        using var binaryStr = new ScopedCString(binary);
        using var valueStr = new ScopedCString(value);
        ulong calleeRvaValue = 0;
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            fixed (byte* valueBufferPtr = valueStr)
            {
                var ret = _vtable->FindCallWithStringArgRva(_self, binaryBufferPtr, funcStartRva, valueBufferPtr, &calleeRvaValue);
                calleeRva = calleeRvaValue;
                return ret;
            }
        }
    }

    public unsafe static int FindCallWithStringArg(string binary, nint funcStart, string value, out nint calleeAddress)
    {
        using var binaryStr = new ScopedCString(binary);
        using var valueStr = new ScopedCString(value);
        nint calleeAddressValue = 0;
        fixed (byte* binaryBufferPtr = binaryStr)
        {
            fixed (byte* valueBufferPtr = valueStr)
            {
                var ret = _vtable->FindCallWithStringArg(_self, binaryBufferPtr, funcStart, valueBufferPtr, &calleeAddressValue);
                calleeAddress = calleeAddressValue;
                return ret;
            }
        }
    }
}
