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

#pragma once

#include <stdint.h>
#include <stddef.h>
#include <stdbool.h>

#ifdef __cplusplus
extern "C" {
#endif

/**
 * @file s2binlib.h
 * @brief S2BinLib Global Singleton C API
 * 
 * This header provides a thread-safe global singleton interface to S2BinLib.
 * All functions use snake_case naming and are prefixed with s2binlib_.
 * 
 * @note Error Codes:
 *   0: Success
 *  -1: Not initialized
 *  -2: Invalid parameter
 *  -3: Operation failed
 *  -4: Not found
 * -99: Mutex poisoned (internal error)
 */

// ============================================================================
// Type Definitions
// ============================================================================

/// Callback function type for pattern_scan_all functions
/// @param index The index of the current match (0-based)
/// @param address The found address (RVA or memory address depending on the function)
/// @param user_data User-provided data pointer
typedef void (*PatternScanCallback)(size_t index, void* address, void* user_data);

// ============================================================================
// Lifecycle Functions
// ============================================================================

/// Initialize the global S2BinLib002 instance with auto-detected OS
/// @param game_path Path to the game directory (null-terminated C string)
/// @param game_type Game type identifier (null-terminated C string)
/// @return 0 on success, negative error code on failure
int s2binlib_initialize(const char* game_path, const char* game_type);

/// Initialize the global S2BinLib002 instance with explicit OS
/// @param game_path Path to the game directory (null-terminated C string)
/// @param game_type Game type identifier (null-terminated C string)
/// @param os Operating system ("windows" or "linux") (null-terminated C string)
/// @return 0 on success, negative error code on failure
int s2binlib_initialize_with_os(const char* game_path, const char* game_type, const char* os);

/// Destroy the global S2BinLib002 instance
/// @return 0 on success, negative error code on failure
int s2binlib_destroy(void);

// ============================================================================
// Pattern Scanning Functions
// ============================================================================

/// Scan for a pattern in the specified binary and return its memory address
/// @param binary_name Name of the binary to scan (e.g., "server", "client")
/// @param pattern Pattern string with wildcards (e.g., "48 89 5C 24 ? 48 89 74 24 ?")
/// @param result Pointer to store the resulting address
/// @return 0 on success, negative error code on failure
int s2binlib_pattern_scan(const char* binary_name, const char* pattern, void** result);

/// Scan for a pattern and return its relative virtual address
/// @param binary_name Name of the binary to scan
/// @param pattern Pattern string with wildcards
/// @param result Pointer to store the resulting RVA
/// @return 0 on success, negative error code on failure
int s2binlib_pattern_scan_rva(const char* binary_name, const char* pattern, void** result);

/// Find all occurrences of a pattern and return their RVAs via callback
/// @param binary_name Name of the binary to scan
/// @param pattern Byte pattern to search for
/// @param callback Function pointer that will be called for each match
/// @param user_data User-provided pointer passed to each callback invocation
/// @return 0 on success, negative error code on failure
int s2binlib_pattern_scan_all_rva(const char* binary_name, const char* pattern, PatternScanCallback callback, void* user_data);

/// Find all occurrences of a pattern and return their memory addresses via callback
/// @param binary_name Name of the binary to scan
/// @param pattern Byte pattern to search for
/// @param callback Function pointer that will be called for each match
/// @param user_data User-provided pointer passed to each callback invocation
/// @return 0 on success, negative error code on failure
int s2binlib_pattern_scan_all(const char* binary_name, const char* pattern, PatternScanCallback callback, void* user_data);

// ============================================================================
// VTable Functions
// ============================================================================

/// Find a vtable by class name and return its memory address
/// @param binary_name Name of the binary to search (e.g., "server", "client")
/// @param vtable_name Class name to search for
/// @param result Pointer to store the resulting vtable address
/// @return 0 on success, negative error code on failure
int s2binlib_find_vtable(const char* binary_name, const char* vtable_name, void** result);

/// Find a vtable by class name and return its relative virtual address
/// @param binary_name Name of the binary to search
/// @param vtable_name Class name to search for
/// @param result Pointer to store the resulting vtable RVA
/// @return 0 on success, negative error code on failure
int s2binlib_find_vtable_rva(const char* binary_name, const char* vtable_name, void** result);

/// Find a vtable by mangled name and return its relative virtual address
/// @param binary_name Name of the binary to search
/// @param vtable_name Mangled RTTI name to search for
/// @param result Pointer to store the resulting vtable RVA
/// @return 0 on success, negative error code on failure
int s2binlib_find_vtable_mangled_rva(const char* binary_name, const char* vtable_name, void** result);

/// Find a vtable by mangled name and return its runtime memory address
/// @param binary_name Name of the binary to search
/// @param vtable_name Mangled RTTI name to search for
/// @param result Pointer to store the resulting vtable memory address
/// @return 0 on success, negative error code on failure
int s2binlib_find_vtable_mangled(const char* binary_name, const char* vtable_name, void** result);

/// Find a nested vtable (2 levels) by class names and return its RVA
/// @param binary_name Name of the binary to search
/// @param class1_name Outer class name
/// @param class2_name Inner/nested class name
/// @param result Pointer to store the resulting vtable RVA
/// @return 0 on success, negative error code on failure
int s2binlib_find_vtable_nested_2_rva(const char* binary_name, const char* class1_name, const char* class2_name, void** result);

/// Find a nested vtable (2 levels) by class names and return its memory address
/// @param binary_name Name of the binary to search
/// @param class1_name Outer class name
/// @param class2_name Inner/nested class name
/// @param result Pointer to store the resulting vtable memory address
/// @return 0 on success, negative error code on failure
int s2binlib_find_vtable_nested_2(const char* binary_name, const char* class1_name, const char* class2_name, void** result);

/// Get the number of virtual functions in a vtable
/// @param binary_name Name of the binary to search
/// @param vtable_name Name of the vtable/class
/// @param result Pointer to store the resulting vfunc count
/// @return 0 on success, negative error code on failure
int s2binlib_get_vtable_vfunc_count(const char* binary_name, const char* vtable_name, size_t* result);

/// Get the number of virtual functions in a vtable by RVA
/// @param binary_name Name of the binary
/// @param vtable_rva Virtual address of the vtable
/// @param result Pointer to store the resulting vfunc count
/// @return 0 on success, negative error code on failure
int s2binlib_get_vtable_vfunc_count_by_rva(const char* binary_name, uint64_t vtable_rva, size_t* result);

// ============================================================================
// Virtual Function Functions
// ============================================================================

/// Find a virtual function by vtable name and index, return RVA
/// @param binary_name Name of the binary to search
/// @param vtable_name Class name whose vtable to search for
/// @param vfunc_index Index of the virtual function in the vtable (0-based)
/// @param result Pointer to store the resulting RVA
/// @return 0 on success, negative error code on failure
int s2binlib_find_vfunc_by_vtbname_rva(const char* binary_name, const char* vtable_name, size_t vfunc_index, void** result);

/// Find a virtual function by vtable name and index, return memory address
/// @param binary_name Name of the binary to search
/// @param vtable_name Class name whose vtable to search for
/// @param vfunc_index Index of the virtual function in the vtable (0-based)
/// @param result Pointer to store the resulting memory address
/// @return 0 on success, negative error code on failure
int s2binlib_find_vfunc_by_vtbname(const char* binary_name, const char* vtable_name, size_t vfunc_index, void** result);

/// Find a virtual function by vtable pointer and index, return RVA
/// @param vtable_ptr Runtime pointer to the vtable
/// @param vfunc_index Index of the virtual function in the vtable (0-based)
/// @param result Pointer to store the resulting RVA
/// @return 0 on success, negative error code on failure
int s2binlib_find_vfunc_by_vtbptr_rva(void* vtable_ptr, size_t vfunc_index, void** result);

/// Find a virtual function by vtable pointer and index, return memory address
/// @param vtable_ptr Runtime pointer to the vtable
/// @param vfunc_index Index of the virtual function in the vtable (0-based)
/// @param result Pointer to store the resulting memory address
/// @return 0 on success, negative error code on failure
int s2binlib_find_vfunc_by_vtbptr(void* vtable_ptr, size_t vfunc_index, void** result);

// ============================================================================
// Symbol and Export Functions
// ============================================================================

/// Find a symbol by name and return its memory address
/// @param binary_name Name of the binary to search
/// @param symbol_name Symbol name to search for
/// @param result Pointer to store the resulting symbol address
/// @return 0 on success, negative error code on failure
int s2binlib_find_symbol(const char* binary_name, const char* symbol_name, void** result);

/// Find a symbol and return its relative virtual address
/// @param binary_name Name of the binary to search
/// @param symbol_name Symbol name to search for
/// @param result Pointer to store the resulting RVA
/// @return 0 on success, negative error code on failure
int s2binlib_find_symbol_rva(const char* binary_name, const char* symbol_name, void** result);

/// Find an exported symbol and return its relative virtual address
/// @param binary_name Name of the binary to search
/// @param export_name Export name to search for
/// @param result Pointer to store the resulting RVA
/// @return 0 on success, negative error code on failure
int s2binlib_find_export_rva(const char* binary_name, const char* export_name, void** result);

/// Find an exported symbol and return its runtime memory address
/// @param binary_name Name of the binary to search
/// @param export_name Export name to search for
/// @param result Pointer to store the resulting memory address
/// @return 0 on success, negative error code on failure
int s2binlib_find_export(const char* binary_name, const char* export_name, void** result);

// ============================================================================
// String Search Functions
// ============================================================================

/// Find a string in the binary and return its relative virtual address
/// @param binary_name Name of the binary to search
/// @param string String to search for
/// @param result Pointer to store the resulting RVA
/// @return 0 on success, negative error code on failure
int s2binlib_find_string_rva(const char* binary_name, const char* string, void** result);

/// Find a string in the binary and return its runtime memory address
/// @param binary_name Name of the binary to search
/// @param string String to search for
/// @param result Pointer to store the resulting memory address
/// @return 0 on success, negative error code on failure
int s2binlib_find_string(const char* binary_name, const char* string, void** result);

/// Dump all printable ASCII strings and their relative virtual addresses to a JSON file
/// @param binary_name Name of the binary to scan
/// @param output_path UTF-8 path of the JSON file to create or overwrite
/// @return 0 on success, negative error code on failure
int s2binlib_dump_strings_to_json(const char* binary_name, const char* output_path);

// ============================================================================
// Module Base Address Functions
// ============================================================================

/// Set module base address from a pointer inside the module
/// @param binary_name Name of the binary
/// @param pointer Pointer inside the specified module
/// @return 0 on success, negative error code on failure
int s2binlib_set_module_base_from_pointer(const char* binary_name, void* pointer);

/// Clear manually set base address for a module
/// @param binary_name Name of the binary
/// @return 0 on success, negative error code on failure
int s2binlib_clear_module_base_address(const char* binary_name);

/// Get the module base address
/// @param binary_name Name of the binary
/// @param result Pointer to store the resulting base address
/// @return 0 on success, negative error code on failure
int s2binlib_get_module_base_address(const char* binary_name, void** result);

// ============================================================================
// Binary Loading Functions
// ============================================================================

/// Check if a binary is already loaded
/// @param binary_name Name of the binary to check
/// @return 1 if loaded, 0 if not loaded, negative error code on failure
int s2binlib_is_binary_loaded(const char* binary_name);

/// Load a binary into memory
/// @param binary_name Name of the binary to load
/// @return 0 on success, negative error code on failure
int s2binlib_load_binary(const char* binary_name);

/// Get the full path to a binary file
/// @param binary_name Name of the binary
/// @param buffer Buffer to store the path string
/// @param buffer_size Size of the buffer
/// @return 0 on success, negative error code on failure
int s2binlib_get_binary_path(const char* binary_name, char* buffer, size_t buffer_size);

/// Set a custom binary path for a specific binary and operating system
/// @param binary_name Name of the binary
/// @param path The custom file path to the binary
/// @param os Operating system identifier ("windows" or "linux")
/// @return 0 on success, negative error code on failure
int s2binlib_set_custom_binary_path(const char* binary_name, const char* path, const char* os);

/// Unload a specific binary from memory
/// @param binary_name Name of the binary to unload
/// @return 0 on success, negative error code on failure
int s2binlib_unload_binary(const char* binary_name);

/// Unload all binaries from memory
/// @return 0 on success, negative error code on failure
int s2binlib_unload_all_binaries(void);

// ============================================================================
// Memory Read Functions
// ============================================================================

/// Read bytes from binary at a file offset
/// @param binary_name Name of the binary to read from
/// @param file_offset File offset to read from
/// @param buffer Buffer to store the read bytes
/// @param buffer_size Size of the buffer (number of bytes to read)
/// @return 0 on success, negative error code on failure
int s2binlib_read_by_file_offset(const char* binary_name, uint64_t file_offset, uint8_t* buffer, size_t buffer_size);

/// Read bytes from binary at a relative virtual address
/// @param binary_name Name of the binary to read from
/// @param rva Virtual address to read from
/// @param buffer Buffer to store the read bytes
/// @param buffer_size Size of the buffer (number of bytes to read)
/// @return 0 on success, negative error code on failure
int s2binlib_read_by_rva(const char* binary_name, uint64_t rva, uint8_t* buffer, size_t buffer_size);

/// Read bytes from binary at a runtime memory address
/// @param binary_name Name of the binary to read from
/// @param mem_address Runtime memory address to read from
/// @param buffer Buffer to store the read bytes
/// @param buffer_size Size of the buffer (number of bytes to read)
/// @return 0 on success, negative error code on failure
int s2binlib_read_by_mem_address(const char* binary_name, uint64_t mem_address, uint8_t* buffer, size_t buffer_size);

// ============================================================================
// Object Pointer Functions
// ============================================================================

/// Get the vtable name from an object pointer
/// @param object_ptr Pointer to the object
/// @param buffer Buffer to store the vtable name
/// @param buffer_size Size of the buffer
/// @return 0 on success, negative error code on failure
int s2binlib_get_object_ptr_vtable_name(const void* object_ptr, char* buffer, size_t buffer_size);

/// Check if an object pointer has a valid vtable
/// @param object_ptr Pointer to the object
/// @return 1 if has vtable, 0 if not, negative error code on failure
int s2binlib_object_ptr_has_vtable(const void* object_ptr);

/// Check if an object has a specific base class
/// @param object_ptr Pointer to the object
/// @param base_class_name Name of the base class to check
/// @return 1 if has base class, 0 if not, negative error code on failure
int s2binlib_object_ptr_has_base_class(const void* object_ptr, const char* base_class_name);

// ============================================================================
// Cross-Reference Functions
// ============================================================================

/// Dump and cache all cross-references in a binary
/// @param binary_name Name of the binary to analyze
/// @return 0 on success, negative error code on failure
int s2binlib_dump_xrefs(const char* binary_name);

/// Get the count of cached cross-references for a target RVA
/// @param binary_name Name of the binary
/// @param target_rva The target relative virtual address
/// @return Non-negative count of xrefs, negative error code on failure
int s2binlib_get_xrefs_count(const char* binary_name, void* target_rva);

/// Get cached cross-references for a target RVA into a buffer
/// @param binary_name Name of the binary
/// @param target_rva The target relative virtual address
/// @param buffer Buffer to store the xref addresses
/// @param buffer_size Size of the buffer (number of void* elements)
/// @return Non-negative count of xrefs written, negative error code on failure
int s2binlib_get_xrefs_cached(const char* binary_name, void* target_rva, void** buffer, size_t buffer_size);

/// Follow cross-reference from memory address to memory address
/// @param mem_address Runtime memory address to analyze
/// @param target_address_out Pointer to store the target address
/// @return 0 on success, negative error code on failure
int s2binlib_follow_xref_mem_to_mem(const void* mem_address, void** target_address_out);

/// Follow cross-reference from RVA to memory address
/// @param binary_name Name of the binary
/// @param rva Virtual address to analyze
/// @param target_address_out Pointer to store the target memory address
/// @return 0 on success, negative error code on failure
int s2binlib_follow_xref_rva_to_mem(const char* binary_name, uint64_t rva, void** target_address_out);

/// Follow cross-reference from RVA to RVA
/// @param binary_name Name of the binary
/// @param rva Virtual address to analyze
/// @param target_rva_out Pointer to store the target RVA
/// @return 0 on success, negative error code on failure
int s2binlib_follow_xref_rva_to_rva(const char* binary_name, uint64_t rva, uint64_t* target_rva_out);

// ============================================================================
// Function Discovery Helpers
// ============================================================================

/// Find the virtual function start that contains the given RVA (returns name/index/RVA)
/// @param binary_name Name of the binary
/// @param include_rva RVA inside the virtual function
/// @param vtable_name_out Buffer to store the vtable name
/// @param vtable_name_out_size Size of vtable_name_out buffer
/// @param vfunc_index_out Pointer to store vfunc index
/// @param vfunc_rva_out Pointer to store vfunc RVA
/// @return 0 on success, negative error code on failure
int s2binlib_find_vfunc_start_rva(
    const char* binary_name,
    uint64_t include_rva,
    char* vtable_name_out,
    size_t vtable_name_out_size,
    size_t* vfunc_index_out,
    uint64_t* vfunc_rva_out);

/// Find the virtual function start that contains the given RVA (returns name/index/memory address)
/// @param binary_name Name of the binary
/// @param include_rva RVA inside the virtual function
/// @param vtable_name_out Buffer to store the vtable name
/// @param vtable_name_out_size Size of vtable_name_out buffer
/// @param vfunc_index_out Pointer to store vfunc index
/// @param result Pointer to store the function start memory address
/// @return 0 on success, negative error code on failure
int s2binlib_find_vfunc_start(
    const char* binary_name,
    uint64_t include_rva,
    char* vtable_name_out,
    size_t vtable_name_out_size,
    size_t* vfunc_index_out,
    void** result);

/// Dump and cache all vtables in a binary
/// @param binary_name Name of the binary to analyze
/// @return 0 on success, negative error code on failure
int s2binlib_dump_vtables(const char* binary_name);

/// Find the function start RVA containing the given RVA
/// @param binary_name Name of the binary
/// @param include_rva RVA inside the function
/// @param result Pointer to store the function start RVA
/// @return 0 on success, negative error code on failure
int s2binlib_find_func_start_rva(const char* binary_name, uint64_t include_rva, uint64_t* result);

/// Find the function start memory address containing the given RVA
/// @param binary_name Name of the binary
/// @param include_rva RVA inside the function
/// @param result Pointer to store the function start memory address
/// @return 0 on success, negative error code on failure
int s2binlib_find_func_start(const char* binary_name, uint64_t include_rva, void** result);

/// Find function start RVA via a unique string reference
/// @param binary_name Name of the binary
/// @param string String to search for
/// @param result Pointer to store the function start RVA
/// @return 0 on success, negative error code on failure
int s2binlib_find_xref_func_with_string_rva(const char* binary_name, const char* string, uint64_t* result);

/// Find function start memory address via a unique string reference (xref-based)
/// @param binary_name Name of the binary
/// @param string String to search for
/// @param result Pointer to store the function start memory address
/// @return 0 on success, negative error code on failure
int s2binlib_find_xref_func_with_string(const char* binary_name, const char* string, void** result);

/// Find virtual function info via a unique string reference
/// @param binary_name Name of the binary
/// @param string String to search for
/// @param vtable_name_out Buffer to store the vtable name
/// @param vtable_name_out_size Size of vtable_name_out buffer
/// @param vfunc_index_out Pointer to store vfunc index
/// @param vfunc_rva_out Pointer to store vfunc RVA
/// @return 0 on success, negative error code on failure
int s2binlib_find_vfunc_with_string_rva(
    const char* binary_name,
    const char* string,
    char* vtable_name_out,
    size_t vtable_name_out_size,
    size_t* vfunc_index_out,
    uint64_t* vfunc_rva_out);

/// Find virtual function info via a unique string reference (memory address)
/// @param binary_name Name of the binary
/// @param string String to search for
/// @param vtable_name_out Buffer to store the vtable name
/// @param vtable_name_out_size Size of vtable_name_out buffer
/// @param vfunc_index_out Pointer to store vfunc index
/// @param result Pointer to store the function start memory address
/// @return 0 on success, negative error code on failure
int s2binlib_find_vfunc_with_string(
    const char* binary_name,
    const char* string,
    char* vtable_name_out,
    size_t vtable_name_out_size,
    size_t* vfunc_index_out,
    void** result);

/// Find function start RVA via a unique string reference
/// @param binary_name Name of the binary
/// @param string String to search for
/// @param result Pointer to store the function start RVA
/// @return 0 on success, negative error code on failure
int s2binlib_find_func_with_string_rva(const char* binary_name, const char* string, uint64_t* result);

/// Find function start memory address via a unique string reference
/// @param binary_name Name of the binary
/// @param string String to search for
/// @param result Pointer to store the function start memory address
/// @return 0 on success, negative error code on failure
int s2binlib_find_func_with_string(const char* binary_name, const char* string, void** result);

/// Find the call inside a function (by RVA) whose argument references a given xref target RVA, return the called function's RVA
/// @param binary_name Name of the binary
/// @param func_start_rva RVA of the function to search inside
/// @param target_rva RVA that must be referenced by a RIP-relative operand of an argument-setup instruction
/// @param result Pointer to store the resulting call target RVA
/// @return 0 on success, negative error code on failure
int s2binlib_find_call_with_xref_arg_rva(const char* binary_name, uint64_t func_start_rva, uint64_t target_rva, uint64_t* result);

/// Find the call inside a function (by pointer) whose argument references a given xref target pointer, return the called function's memory address
/// @param binary_name Name of the binary
/// @param func_start Memory address of the function to search inside
/// @param target Memory address that must be referenced by a RIP-relative operand of an argument-setup instruction
/// @param result Pointer to store the resulting call target memory address
/// @return 0 on success, negative error code on failure
int s2binlib_find_call_with_xref_arg(const char* binary_name, void* func_start, void* target, void** result);

/// Find the call inside a function (by RVA) whose argument loads a given string, return the called function's RVA
/// @param binary_name Name of the binary
/// @param func_start_rva RVA of the function to search inside
/// @param string String that must be loaded by an argument-setup instruction
/// @param result Pointer to store the resulting call target RVA
/// @return 0 on success, negative error code on failure
int s2binlib_find_call_with_string_arg_rva(const char* binary_name, uint64_t func_start_rva, const char* string, uint64_t* result);

/// Find the call inside a function (by pointer) whose argument loads a given string, return the called function's memory address
/// @param binary_name Name of the binary
/// @param func_start Memory address of the function to search inside
/// @param string String that must be loaded by an argument-setup instruction
/// @param result Pointer to store the resulting call target memory address
/// @return 0 on success, negative error code on failure
int s2binlib_find_call_with_string_arg(const char* binary_name, void* func_start, const char* string, void** result);

// ============================================================================
// JIT and Trampoline Functions
// ============================================================================

/// Install a JIT trampoline at a memory address
/// @param mem_address Runtime memory address where to install the trampoline
/// @param trampoline_address_out Pointer to store the trampoline address
/// @return 0 on success, negative error code on failure
int s2binlib_install_trampoline(void* mem_address, void** trampoline_address_out);

// ============================================================================
// NetworkVar Functions
// ============================================================================

/// Find the NetworkVar_StateChanged vtable index by RVA
/// @param vtable_rva Virtual address of the vtable to analyze
/// @param result Pointer to store the resulting index
/// @return 0 on success, negative error code on failure
int s2binlib_find_networkvar_vtable_statechanged_rva(uint64_t vtable_rva, uint64_t* result);

/// Find the NetworkVar_StateChanged vtable index by memory address
/// @param vtable_mem_address Runtime memory address of the vtable
/// @param result Pointer to store the resulting index
/// @return 0 on success, negative error code on failure
int s2binlib_find_networkvar_vtable_statechanged(uint64_t vtable_mem_address, uint64_t* result);

// ============================================================================
// Signature Functions
// ============================================================================

/// Make a signature from a RVA
/// @param binary_name Name of the binary
/// @param func_rva RVA of the function
/// @param result_out Buffer to store the signature
/// @param result_out_size Size of the result_out buffer
/// @return 0 on success, negative error code on failure    
int s2binlib_make_sig_rva(const char* binary_name, uint64_t func_rva, char* result_out, size_t result_out_size);

#ifdef __cplusplus
}
#endif
