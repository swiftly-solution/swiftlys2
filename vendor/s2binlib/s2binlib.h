/************************************************************************************
 *  S2BinLib - A static library that helps resolving memory from binary file
 *  and map to absolute memory address, targeting source 2 game engine.
 *  Copyright (C) 2025-2026  samyyc
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <https://www.gnu.org/licenses/>.
 ***********************************************************************************/

#ifndef _s2binlib_s2binlib004_h
#define _s2binlib_s2binlib004_h

#pragma once

#include <cstdint>
#include <cstddef>

#ifdef __cplusplus
extern "C"
{
#endif

#define S2BINLIB_INTERFACE_NAME "S2BINLIB004"

    /// Forward declaration
    #ifdef __cplusplus
    class S2BinLib004;
    #else
    struct S2BinLib004;
    #endif

    /// Callback function type for pattern_scan_all functions
    /// @param index The index of the current match (0-based)
    /// @param address The found address (RVA or memory address depending on the function)
    /// @param user_data User-provided data pointer
    /// @return true to stop searching, false to continue searching for more matches
    typedef bool (*PatternScanCallback)(size_t index, void* address, void* user_data);

    /// Create a new S2BinLib004 instance
    /// @param interface_name Interface name to create
    /// @return Pointer to the created instance, or nullptr on failure
    typedef S2BinLib004* (*S2CreateInterfaceFn)(const char* interface_name);

#ifdef __cplusplus
}

/// S2BinLib004 class - Interface version 004
/// This class provides access to S2BinLib functionality through a virtual table interface
class S2BinLib004
{
public:
    /// Initialize with auto-detected operating system
    /// @param game_path Path to the game directory (null-terminated C string)
    /// @param game_type Game type identifier (null-terminated C string)
    /// @return 0 on success, negative error code on failure
    virtual int Initialize(const char* game_path, const char* game_type) = 0;

    /// Initialize with explicit operating system parameter
    /// @param game_path Path to the game directory (null-terminated C string)
    /// @param game_type Game type identifier (null-terminated C string)
    /// @param os Operating system ("windows" or "linux") (null-terminated C string)
    /// @return 0 on success, negative error code on failure
    virtual int InitializeWithOs(const char* game_path, const char* game_type, const char* os) = 0;

    /// Scan for a pattern in the specified binary and return its memory address
    virtual int PatternScan(const char* binary_name, const char* pattern, void** result) = 0;

    /// Find a vtable by class name and return its memory address
    virtual int FindVtable(const char* binary_name, const char* vtable_name, void** result) = 0;

    /// Find a symbol by name and return its memory address
    virtual int FindSymbol(const char* binary_name, const char* symbol_name, void** result) = 0;

    /// Set module base address from a pointer inside the module
    virtual int SetModuleBaseFromPointer(const char* binary_name, void* pointer) = 0;

    /// Clear manually set base address for a module
    virtual int ClearModuleBaseAddress(const char* binary_name) = 0;

    /// Set a custom binary path for a specific binary and operating system
    virtual int SetCustomBinaryPath(const char* binary_name, const char* path, const char* os) = 0;

    /// Get the module base address
    virtual int GetModuleBaseAddress(const char* binary_name, void** result) const = 0;

    /// Check if a binary is already loaded
    virtual int IsBinaryLoaded(const char* binary_name) const = 0;

    /// Load a binary into memory
    virtual int LoadBinary(const char* binary_name) = 0;

    /// Get the full path to a binary file
    virtual int GetBinaryPath(const char* binary_name, char* buffer, size_t buffer_size) const = 0;

    /// Find a vtable by class name and return its relative virtual address
    virtual int FindVtableRva(const char* binary_name, const char* vtable_name, void** result) = 0;

    /// Find a vtable by mangled name and return its relative virtual address
    virtual int FindVtableMangledRva(const char* binary_name, const char* vtable_name, void** result) = 0;

    /// Find a vtable by mangled name and return its runtime memory address
    virtual int FindVtableMangled(const char* binary_name, const char* vtable_name, void** result) = 0;

    /// Find a nested vtable (2 levels) by class names and return its RVA
    virtual int FindVtableNested2Rva(const char* binary_name, const char* class1_name, const char* class2_name, void** result) = 0;

    /// Find a nested vtable (2 levels) by class names and return its memory address
    virtual int FindVtableNested2(const char* binary_name, const char* class1_name, const char* class2_name, void** result) = 0;

    /// Get the number of virtual functions in a vtable
    virtual int GetVtableVfuncCount(const char* binary_name, const char* vtable_name, size_t* result) = 0;

    /// Get the number of virtual functions in a vtable by RVA
    virtual int GetVtableVfuncCountByRva(const char* binary_name, uint64_t vtable_rva, size_t* result) = 0;

    /// Scan for a pattern and return its relative virtual address
    virtual int PatternScanRva(const char* binary_name, const char* pattern, void** result) = 0;

    /// Find all occurrences of a pattern and return their RVAs via callback
    virtual int PatternScanAllRva(const char* binary_name, const char* pattern, PatternScanCallback callback, void* user_data) = 0;

    /// Find all occurrences of a pattern and return their memory addresses via callback
    virtual int PatternScanAll(const char* binary_name, const char* pattern, PatternScanCallback callback, void* user_data) = 0;

    /// Find an exported symbol and return its relative virtual address
    virtual int FindExportRva(const char* binary_name, const char* export_name, void** result) = 0;

    /// Find an exported symbol and return its runtime memory address
    virtual int FindExport(const char* binary_name, const char* export_name, void** result) = 0;

    /// Find a symbol and return its relative virtual address
    virtual int FindSymbolRva(const char* binary_name, const char* symbol_name, void** result) = 0;

    /// Read bytes from binary at a file offset
    virtual int ReadByFileOffset(const char* binary_name, uint64_t file_offset, uint8_t* buffer, size_t buffer_size) = 0;

    /// Read bytes from binary at a relative virtual address
    virtual int ReadByRva(const char* binary_name, uint64_t rva, uint8_t* buffer, size_t buffer_size) = 0;

    /// Read bytes from binary at a runtime memory address
    virtual int ReadByMemAddress(const char* binary_name, uint64_t mem_address, uint8_t* buffer, size_t buffer_size) = 0;

    /// Find a virtual function by vtable name and index, return RVA
    virtual int FindVfuncByVtbnameRva(const char* binary_name, const char* vtable_name, size_t vfunc_index, void** result) = 0;

    /// Find a virtual function by vtable name and index, return memory address
    virtual int FindVfuncByVtbname(const char* binary_name, const char* vtable_name, size_t vfunc_index, void** result) = 0;

    /// Find a virtual function by vtable pointer and index, return RVA
    virtual int FindVfuncByVtbptrRva(void* vtable_ptr, size_t vfunc_index, void** result) const = 0;

    /// Find a virtual function by vtable pointer and index, return memory address
    virtual int FindVfuncByVtbptr(void* vtable_ptr, size_t vfunc_index, void** result) const = 0;

    /// Get the vtable name from an object pointer
    virtual int GetObjectPtrVtableName(const void* object_ptr, char* buffer, size_t buffer_size) const = 0;

    /// Check if an object pointer has a valid vtable
    virtual int ObjectPtrHasVtable(const void* object_ptr) const = 0;

    /// Check if an object has a specific base class
    virtual int ObjectPtrHasBaseClass(const void* object_ptr, const char* base_class_name) const = 0;

    /// Find a string in the binary and return its relative virtual address
    virtual int FindStringRva(const char* binary_name, const char* string, void** result) = 0;

    /// Find a string in the binary and return its runtime memory address
    virtual int FindString(const char* binary_name, const char* string, void** result) = 0;

    /// Dump and cache all cross-references in a binary
    virtual int DumpXrefs(const char* binary_name) = 0;

    /// Get the count of cached cross-references for a target RVA
    virtual int GetXrefsCount(const char* binary_name, void* target_rva) const = 0;

    /// Get cached cross-references for a target RVA into a buffer
    virtual int GetXrefsCached(const char* binary_name, void* target_rva, void** buffer, size_t buffer_size) const = 0;

    /// Unload a specific binary from memory
    virtual int UnloadBinary(const char* binary_name) = 0;

    /// Unload all binaries from memory
    virtual int UnloadAllBinaries() = 0;

    /// Install a JIT trampoline at a memory address
    virtual int InstallTrampoline(void* mem_address, void** trampoline_address_out) = 0;

    /// Follow cross-reference from memory address to memory address
    virtual int FollowXrefMemToMem(const void* mem_address, void** target_address_out) const = 0;

    /// Follow cross-reference from RVA to memory address
    virtual int FollowXrefRvaToMem(const char* binary_name, uint64_t rva, void** target_address_out) = 0;

    /// Follow cross-reference from RVA to RVA
    virtual int FollowXrefRvaToRva(const char* binary_name, uint64_t rva, uint64_t* target_rva_out) = 0;

    /// Dump vtables and cache metadata
    virtual int DumpVtables(const char* binary_name) = 0;

    /// Find the virtual function start that contains the given RVA (returns name/index/RVA)
    virtual int FindVfuncStartRva(const char* binary_name, uint64_t include_rva, char* vtable_name_out, size_t vtable_name_out_size, size_t* vfunc_index_out, uint64_t* vfunc_rva_out) = 0;

    /// Find the virtual function start that contains the given RVA (returns name/index/memory address)
    virtual int FindVfuncStart(const char* binary_name, uint64_t include_rva, char* vtable_name_out, size_t vtable_name_out_size, size_t* vfunc_index_out, void** result) = 0;

    /// Find the function start RVA that contains the given RVA (xref-based)
    virtual int FindXrefFuncStartRva(const char* binary_name, uint64_t include_rva, uint64_t* result) = 0;

    /// Find the function start memory address that contains the given RVA (xref-based)
    virtual int FindXrefFuncStart(const char* binary_name, uint64_t include_rva, void** result) = 0;

    /// Find function start memory address via a unique string reference (xref-based)
    virtual int FindXrefFuncWithString(const char* binary_name, const char* string, void** result) = 0;

    /// Find function start RVA that contains the given RVA
    virtual int FindFuncStartRva(const char* binary_name, uint64_t include_rva, uint64_t* result) = 0;

    /// Find function start memory address that contains the given RVA
    virtual int FindFuncStart(const char* binary_name, uint64_t include_rva, void** result) = 0;

    /// Find function start RVA by a referenced string
    virtual int FindXrefFuncWithStringRva(const char* binary_name, const char* string, uint64_t* result) = 0;

    /// Find virtual function info by a referenced string
    virtual int FindVfuncWithStringRva(const char* binary_name, const char* string, char* vtable_name_out, size_t vtable_name_out_size, size_t* vfunc_index_out, uint64_t* vfunc_rva_out) = 0;

    /// Find virtual function info by a referenced string (memory address)
    virtual int FindVfuncWithString(const char* binary_name, const char* string, char* vtable_name_out, size_t vtable_name_out_size, size_t* vfunc_index_out, void** result) = 0;

    /// Find function start RVA by a referenced string
    virtual int FindFuncWithStringRva(const char* binary_name, const char* string, uint64_t* result) = 0;

    /// Find function start memory address by a referenced string
    virtual int FindFuncWithString(const char* binary_name, const char* string, void** result) = 0;

    /// Find the NetworkVar_StateChanged vtable index by RVA
    virtual int FindNetworkvarVtableStatechangedRva(uint64_t vtable_rva, uint64_t* result) const = 0;

    /// Find the NetworkVar_StateChanged vtable index by memory address
    virtual int FindNetworkvarVtableStatechanged(uint64_t vtable_mem_address, uint64_t* result) const = 0;

    /// Make a signature from a RVA
    virtual int MakeSigRva(const char* binary_name, uint64_t func_rva, char* result_out, size_t result_out_size) = 0;

    /// Destroy the S2BinLib004 instance
    virtual int Destroy() = 0;

    /// Find the call inside a function (by RVA) whose argument references a given xref target RVA, return the called function's RVA
    virtual int FindCallWithXrefArgRva(const char* binary_name, uint64_t func_start_rva, uint64_t target_rva, uint64_t* result) = 0;

    /// Find the call inside a function (by pointer) whose argument references a given xref target pointer, return the called function's memory address
    virtual int FindCallWithXrefArg(const char* binary_name, void* func_start, void* target, void** result) = 0;

    /// Find the call inside a function (by RVA) whose argument loads a given string, return the called function's RVA
    virtual int FindCallWithStringArgRva(const char* binary_name, uint64_t func_start_rva, const char* string, uint64_t* result) = 0;

    /// Find the call inside a function (by pointer) whose argument loads a given string, return the called function's memory address
    virtual int FindCallWithStringArg(const char* binary_name, void* func_start, const char* string, void** result) = 0;
};

#endif // __cplusplus

#endif // _s2binlib_s2binlib004_h