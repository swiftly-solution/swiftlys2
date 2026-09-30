#ifndef SW_EXTENSION_H
#define SW_EXTENSION_H

/**
 * @file sw_extension.h
 * @brief C API for SwiftlyS2 extensions.
 */

#ifndef SW_HOST
#if defined(_WIN32)
    #define SW_EXPORT __declspec(dllexport)
#else
    #define SW_EXPORT __attribute__((visibility("default")))
#endif

#ifdef __cplusplus
    #define SW_EXTERN_C extern "C"
#else
    #define SW_EXTERN_C extern
#endif

#define SW_API SW_EXTERN_C SW_EXPORT
#endif

#include <stdint.h>

#ifdef SW_HOST
typedef void** sw_ptr_out;
#else
typedef void* sw_ptr_out;
#endif

// ----- Status code -----
    /**
     * @brief Status returned by an extension API operation.
     * @details `SW_OK` indicates success; other values indicate an error.
     * See each operation for its output values and status limitations.
     */
    typedef int32_t sw_status;

    /** @brief The operation completed successfully. */
    #define SW_OK 0
    /** @brief The operation failed. */
    #define SW_EFAILED -1
    /** @brief An argument is invalid or a requested key does not exist. */
    #define SW_EINVALID_ARG -2
    /** @brief The supplied output buffer is too small. */
    #define SW_EBUFFER_TOO_SMALL -3
// -----------------------

// ----- Interfaces -----
    #define SW_IFACE_MEMORY "SwMemory00001"
// ----------------------

/** @brief Extension initialization context type. */
typedef struct sw_init_context sw_init_context;

/**
 * @brief Host callbacks available during extension initialization.
 *
 * @note The context is valid only during sw_extension_init. Do not retain it
 * for later calls.
 */
struct sw_init_context {

    /**
     * @brief Register the extension's metadata with the host.
     *
     * @param[in]   self               Initialization context, must not be NULL.
     * @param[in]   name               Extension name, must not be NULL.
     * @param[in]   version            Extension version, must not be NULL.
     * @param[in]   author             Extension author, can be NULL.
     * @param[in]   description        Extension description, can be NULL.
     *
     * @return Status code.
     * @retval      `SW_OK`            Metadata was registered.
     * @retval      `SW_EINVALID_ARG`  Name or version is NULL.
     *
     * @note Must be called during sw_extension_init to initialize the extension.
     * @note Non-NULL strings must be null-terminated. They are copied by the
     * host and may be freed after this call.
     */
    sw_status (*init)(
        const sw_init_context* self,
        const char* name,
        const char* version,
        const char* author,
        const char* description
    );

    /**
     * @brief Query a core API interface by name.
     *
     * @param[in]   self               Initialization context, must not be NULL.
     * @param[in]   iface              Null-terminated interface name, must not be NULL.
     * @param[out]  iface_out          Pointer storage, must not be NULL. Receives
     *                                 the interface, or NULL if it is not found.
     *
     * @return Status code.
     * @retval      `SW_OK`            Interface was found.
     * @retval      `SW_EINVALID_ARG`  Interface name was not found.
     *
     * @note Use the SW_IFACE_* constants in this header as interface names.
     * @note The returned interface is owned by the host and must not be freed.
     */
    sw_status (*query_interface)(
        const sw_init_context* self,
        const char* iface,
        sw_ptr_out iface_out
    );
    
};

/**
 * @brief Function pointer type for the extension entry point.
 *
 * @param[in]  ctx  Initialization context supplied by the host, must not be NULL.
 *
 * @return SW_OK on success, or a nonzero status code on failure.
 * @see sw_extension_init
 */
typedef int32_t (*sw_extension_init_fn)(const sw_init_context* ctx);
#ifndef SW_HOST
/**
 * @brief Initialize an extension when it is loaded by the host.
 *
 * @param[in]  ctx  Initialization context supplied by the host, must not be NULL.
 *
 * @return `SW_OK` on success, or a nonzero status code on failure.
 *
 * @note Must be defined and exported by the extension.
 * @note Call `ctx->init` to register the extension's metadata before returning.
 * @note The context must not be used after this function returns.
 */
SW_API int32_t sw_extension_init(const sw_init_context* ctx);
#endif

/** @brief Memory API interface type queried with SW_IFACE_MEMORY. */
typedef struct sw_memory sw_memory;

/**
 * @brief Opaque handle to a function or virtual function hook.
 *
 * @note Release a handle exactly once with the matching unhook_address or
 * unhook_vtable callback. The handle is invalid after that call.
 */
typedef void* sw_hook_handle;

/**
 * @brief Shared pointers, memory allocation, hooks, and address lookup APIs.
 *
 * @note Obtain this interface with sw_init_context::query_interface using
 * SW_IFACE_MEMORY. Pass that interface pointer as self to each callback.
 * @note Required pointers must be valid and non-NULL. The callbacks do not
 * generally validate them or return SW_EINVALID_ARG for NULL pointers.
 */
struct sw_memory {

    /**
     * @brief Get a pointer from the host's shared pointer registry.
     *
     * @param[in]   self                Memory interface, must not be NULL.
     * @param[in]   key                 Null-terminated key, must not be NULL.
     * @param[out]  shared_pointer_out  Pointer storage, must not be NULL.
     *                                  Receives the stored value on success;
     *                                  unchanged if the key does not exist.
     *
     * @return Status code.
     * @retval      `SW_OK`             Key exists; its value may be NULL.
     * @retval      `SW_EINVALID_ARG`   Key does not exist.
     *
     * @note The registry does not transfer ownership of the pointed-to object.
     */
    sw_status (*get_shared_pointer)(
        const sw_memory* self,
        const char* key,
        sw_ptr_out shared_pointer_out
    );

    /**
     * @brief Insert or replace a pointer in the host's shared pointer registry.
     *
     * @param[in]   self            Memory interface, must not be NULL.
     * @param[in]   key             Null-terminated key, must not be NULL.
     * @param[in]   shared_pointer  Value to store, can be NULL.
     *
     * @return Status code.
     * @retval      `SW_OK`         Value was stored.
     *
     * @note The key is copied. The pointed-to object is not copied or owned by
     * the registry, and replacing an entry does not free its previous value.
     */
    sw_status (*set_shared_pointer)(
        const sw_memory* self,
        const char* key,
        void* shared_pointer
    );

    /**
     * @brief Check whether a key exists in the shared pointer registry.
     *
     * @param[in]   self        Memory interface, must not be NULL.
     * @param[in]   key         Null-terminated key, must not be NULL.
     * @param[out]  result_out  Result storage, must not be NULL. Receives 1 if
     *                          the key exists, or 0 otherwise.
     *
     * @return Status code.
     * @retval      `SW_OK`     Result was written.
     *
     * @note A key with a NULL value still exists.
     */
    sw_status (*has_shared_pointer)(
        const sw_memory* self,
        const char* key,
        int32_t* result_out
    );

    /**
     * @brief Remove a key from the shared pointer registry.
     *
     * @param[in]   self               Memory interface, must not be NULL.
     * @param[in]   key                Null-terminated key, must not be NULL.
     *
     * @return Status code.
     * @retval      `SW_OK`            Entry was removed.
     * @retval      `SW_EINVALID_ARG`  Key does not exist.
     *
     * @note Removing an entry does not free the pointed-to object.
     */
    sw_status (*remove_shared_pointer)(
        const sw_memory* self,
        const char* key
    );

    /**
     * @brief Allocate memory through the game's allocator.
     *
     * @param[in]   self          Memory interface, must not be NULL.
     * @param[in]   size          Number of bytes to allocate.
     * @param[out]  pointer_out   Pointer storage, must not be NULL. Receives
     *                            the allocation, or NULL on failure.
     *
     * @return Status code.
     * @retval      `SW_OK`       Allocation returned a non-NULL pointer.
     * @retval      `SW_EFAILED`  Allocator returned NULL.
     *
     * @note Memory is not initialized. Release it with this interface's free
     * callback, or resize it with resize.
     */
    sw_status (*alloc)(
        const sw_memory* self,
        uint64_t size,
        sw_ptr_out pointer_out
    );

    /**
     * @brief Release memory allocated through the game's allocator.
     *
     * @param[in]   self     Memory interface, must not be NULL.
     * @param[in]   pointer  Allocation returned by alloc or resize, or NULL.
     *
     * @return Status code.
     * @retval      `SW_OK`  Free request was processed.
     */
    sw_status (*free)(
        const sw_memory* self,
        void* pointer
    );

    /**
     * @brief Resize an allocation through the game's allocator.
     *
     * @param[in]   self         Memory interface, must not be NULL.
     * @param[in]   pointer      Live allocation returned by alloc or resize,
     *                           or NULL to allocate a new block.
     * @param[in]   new_size     New size in bytes.
     * @param[out]  pointer_out  Pointer storage, must not be NULL. Receives
     *                           the allocator's result, which may be NULL.
     *
     * @return Status code.
     * @retval      `SW_OK`      Resize request was processed, even if the
     *                           allocator returned NULL.
     *
     * @note For a nonzero new_size, check pointer_out for allocation failure
     * before replacing the original pointer. A successful resize may move
     * the allocation and invalidate the original pointer.
     * @note Zero-size behavior is determined by the game's allocator.
     */
    sw_status (*resize)(
        const sw_memory* self,
        void* pointer,
        uint64_t new_size,
        sw_ptr_out pointer_out
    );

    /**
     * @brief Create and enable a hook at a function address.
     *
     * @param[in]   self           Memory interface, must not be NULL.
     * @param[in]   address        Function address to hook, must not be NULL.
     * @param[in]   hook_callback  Replacement function, must not be NULL.
     * @param[out]  handle_out     Handle storage, must not be NULL. Receives
     *                             the hook handle on success, or NULL on failure.
     * @param[out]  original_out   Pointer storage, must not be NULL. Receives
     *                             a trampoline for calling the original function
     *                             on success, or NULL on failure.
     *
     * @return Status code.
     * @retval      `SW_OK`        A non-NULL original trampoline was created.
     * @retval      `SW_EFAILED`   Original trampoline is NULL; hook was not enabled.
     *
     * @note The callback and original function must use the target function's
     * signature and calling convention.
     * @note The original trampoline is checked before enabling the hook.
     * A failed hook is destroyed and both outputs are set to NULL.
     * @note Release the handle with unhook_address. The original trampoline
     * must not be used after the hook is removed.
     */
    sw_status (*hook_address)(
        const sw_memory* self,
        void* address,
        void* hook_callback,
        sw_hook_handle* handle_out,
        sw_ptr_out original_out
    );

    /**
     * @brief Disable a function address hook.
     *
     * @param[in]   self     Memory interface, must not be NULL.
     * @param[in]   handle   Live handle returned by hook_address, must not be NULL.
     *
     * @return Status code.
     * @retval      `SW_OK`  Hook was disabled and destroyed.
     *
     * @note The handle and its original trampoline are invalid after this call.
     */
    sw_status (*unhook_address)(
        const sw_memory* self,
        const sw_hook_handle handle
    );

    /**
     * @brief Create and enable a hook for a virtual function table entry.
     *
     * @param[in]   self           Memory interface, must not be NULL.
     * @param[in]   vtable         Virtual function table address, must not be NULL.
     * @param[in]   offset         Zero-based entry index in the table, not a byte
     *                             offset. Must refer to a valid entry.
     * @param[in]   hook_callback  Replacement function, must not be NULL.
     * @param[out]  handle_out     Handle storage, must not be NULL. Receives
     *                             the hook handle on success, or NULL on failure.
     * @param[out]  original_out   Pointer storage, must not be NULL. Receives
     *                             a trampoline for calling the original function
     *                             on success, or NULL on failure.
     *
     * @return Status code.
     * @retval      `SW_OK`        A non-NULL original trampoline was created.
     * @retval      `SW_EFAILED`   Original trampoline is NULL; hook was not enabled.
     *
     * @note Pass the vtable address itself, not an object instance.
     * @note The callback and original function must use the target function's
     * signature and calling convention, including its instance argument.
     * @note The original trampoline is checked before enabling the hook.
     * A failed hook is destroyed and both outputs are set to NULL.
     * @note Release the handle with unhook_vtable. The original trampoline
     * must not be used after the hook is removed.
     */
    sw_status (*hook_vtable)(
        const sw_memory* self,
        void* vtable,
        uint32_t offset,
        void* hook_callback,
        sw_hook_handle* handle_out,
        sw_ptr_out original_out
    );

    /**
     * @brief Disable a virtual function hook.
     *
     * @param[in]   self     Memory interface, must not be NULL.
     * @param[in]   handle   Live handle returned by hook_vtable, must not be NULL.
     *
     * @return Status code.
     * @retval      `SW_OK`  Hook was disabled and destroyed.
     *
     * @note The handle and its original trampoline are invalid after this call.
     */
    sw_status (*unhook_vtable)(
        const sw_memory* self,
        const sw_hook_handle handle
    );

    /**
     * @brief Look up a signature address from the host's loaded gamedata.
     *
     * @param[in]   self               Memory interface, must not be NULL.
     * @param[in]   name               Null-terminated gamedata signature name,
     *                                 must not be NULL.
     * @param[out]  address_out        Pointer storage, must not be NULL. Receives the
     *                                 stored address when the name exists; otherwise
     *                                 unchanged.
     *
     * @return Status code.
     * @retval      `SW_OK`            A non-NULL address was found.
     * @retval      `SW_EINVALID_ARG`  Signature name does not exist.
     * @retval      `SW_EFAILED`       Stored address is NULL.
     */
    sw_status (*gamedata_resolve_signature)(
        const sw_memory* self,
        const char* name,
        sw_ptr_out address_out
    );

    /**
     * @brief Look up an offset from the host's loaded gamedata.
     *
     * @param[in]   self               Memory interface, must not be NULL.
     * @param[in]   name               Null-terminated gamedata offset name,
     *                                 must not be NULL.
     * @param[out]  offset_out         Offset storage, must not be NULL. Receives the
     *                                 stored offset when the name exists; otherwise
     *                                 unchanged.
     *
     * @return Status code.
     * @retval      `SW_OK`            A nonzero offset was found.
     * @retval      `SW_EINVALID_ARG`  Offset name does not exist.
     */
    sw_status (*gamedata_get_offset)(
        const sw_memory* self,
        const char* name,
        uint32_t* offset_out
    );

    /**
     * @brief Scan a library for a signature and resolve its runtime address.
     *
     * @param[in]   self          Memory interface, must not be NULL.
     * @param[in]   library       Null-terminated binary name, e.g. "server" or "engine2".
     *                            must not be NULL.
     * @param[in]   pattern       Null-terminated pattern accepted by S2BinLib,
     *                            must not be NULL.
     * @param[out]  address_out   Pointer storage, must not be NULL. Receives
     *                            the matched address on success.
     *
     * @return Status code.
     * @retval      `SW_OK`       A non-NULL address was resolved.
     * @retval      `SW_EFAILED`  Scan failed or no address was resolved.
     *
     * @note The output is only valid on success.
     */
    sw_status (*resolve_signature)(
        const sw_memory* self,
        const char* library,
        const char* pattern,
        sw_ptr_out address_out
    );

    /**
     * @brief Find a class's virtual function table in a library.
     *
     * @param[in]   self          Memory interface, must not be NULL.
     * @param[in]   library       Null-terminated binary name, e.g. "server" or "engine2".
     *                            must not be NULL.
     * @param[in]   vtable        Null-terminated class name used to identify
     *                            the virtual function table, must not be NULL.
     * @param[out]  address_out   Pointer storage, must not be NULL. Receives
     *                            the table's runtime address on success.
     *
     * @return Status code.
     * @retval      `SW_OK`       A non-NULL table address was resolved.
     * @retval      `SW_EFAILED`  Lookup failed or no address was resolved.
     *
     * @note The output is only valid on success. The table belongs to the
     * library and must not be freed.
     * @see hook_vtable
     */
    sw_status (*find_vtable)(
        const sw_memory* self,
        const char* library,
        const char* vtable,
        sw_ptr_out address_out
    );
    
};

#endif
