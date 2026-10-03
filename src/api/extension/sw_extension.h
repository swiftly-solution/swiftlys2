#ifndef SW_EXTENSION_H
#define SW_EXTENSION_H

/**
 * @file sw_extension.h
 * @brief C API for SwiftlyS2 extensions.
 * @note Outside core->init, status-returning operations return SW_E_INVALID_ARG when a
 * required interface, input pointer, or output pointer is NULL. Optional
 * pointers are identified in each operation. Invalid arguments leave outputs
 * unchanged. All core->init operations require active initialization and
 * return SW_E_INVALID_OPERATION afterward for a valid self, even if other
 * arguments are invalid.
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
     * @note `SW_OK` indicates success; other values indicate an error.
     * See each operation for its output values and status limitations.
     */
    typedef int32_t sw_status;

    /** @brief The operation completed successfully. */
    #define SW_OK 0
    /** @brief The operation failed. */
    #define SW_E_FAILED -1
    /** @brief An argument is invalid or a requested key does not exist. */
    #define SW_E_INVALID_ARG -2
    /** @brief The supplied output buffer is too small. */
    #define SW_E_BUFFER_TOO_SMALL -3
    /** @brief This function is not allowed to call at current state. */
    #define SW_E_INVALID_OPERATION -4
// -----------------------

// ----- Interfaces -----
    #define SW_IFACE_CORE_00001 "SwCore00001"
    #define SW_IFACE_CORE SW_IFACE_CORE_00001
// ----------------------

/** @brief Extension initialization context type. */
typedef struct sw_init_context sw_init_context;

/** @brief sw_core forward declaration. */
typedef struct sw_core_00001 sw_core_00001;
/** @brief Extension core API. */
typedef sw_core_00001 sw_core;

/**
 * @brief Host callbacks available during extension initialization.
 *
 * @note The context is valid only during sw_extension_init. Do not retain it
 * for later calls.
 */
struct sw_init_context {

    /**
     * @brief Query the core by it's version.
     *
     * @param[in]   self               Initialization context, must not be NULL.
     * @param[in]   iface              Pass in the `SW_IFACE_CORE`.
     * @param[out]  sw_core_out        Pointer storage, must not be NULL. Receives
     *                                 the `sw_core` interface.
     *
     * @return Status code.
     * @retval      `SW_E_INVALID_ARG`  Self, iface, or output storage is NULL.
     * @retval      `SW_OK`             Interface was found.
     * @retval      `SW_E_FAILED`       Interface was not found. `sw_core_out` is set to NULL.
     *
     * @note Use the SW_IFACE_CORE for the iface parameter.
     * @note The returned interface is owned by the host and must not be freed.
     */
    sw_status (*core)(
        const sw_init_context* self,
        const char* iface,
        sw_core** sw_core_out
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
 * @note Call `core->init->set_info` to register the extension's metadata before returning.
 * @note The context must not be used after this function returns.
 */
SW_API int32_t sw_extension_init(const sw_init_context* ctx);
#endif

/**
 * @brief Extension lifecycle callback invoked with the registered user data.
 * @param[in] user_data User data supplied when registering the callback.
 */
typedef void (*sw_extension_callback_fn)(void* user_data);

/** @brief Initialization API interface type. */
typedef struct sw_init_00001 sw_init_00001;

/**
 * @brief Extension metadata registration and hot reload state APIs.
 *
 * @note Obtain this interface from sw_core::init and pass it as self to each
 * callback. Call these callbacks only during sw_extension_init. After it returns,
 * all operations on this interface return SW_E_INVALID_OPERATION for a valid self.
 * The interface remains owned by the host until the extension is unloaded.
 */
struct sw_init_00001 {

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
     * @retval      `SW_OK`                   Metadata was registered.
     * @retval      `SW_E_INVALID_ARG`        Self, name, or version is NULL.
     * @retval      `SW_E_INVALID_OPERATION`  Initialization has finished.
     *
     * @note Must be called during sw_extension_init to initialize the extension.
     * @note Non-NULL strings must be null-terminated. They are copied by the
     * host and may be freed after this call.
     */
    sw_status (*set_info)(
        const sw_init_00001* self,
        const char* name,
        const char* version,
        const char* author,
        const char* description
    );

    /**
     * @brief Whether this initialization is hot reloaded manually.
     *
     * @param[in]   self             Initialization interface, must not be NULL.
     * @param[out]  hotreloaded_out  Result storage, must not be NULL. Receives 1
     *                               for a manual hot reload, or 0 otherwise.
     *
     * @return Status code.
     * @retval      `SW_OK`                   Result was written.
     * @retval      `SW_E_INVALID_ARG`        Self or output storage is NULL.
     * @retval      `SW_E_INVALID_OPERATION`  Initialization has finished.
     */
    sw_status (*is_hotreloaded)(
        const sw_init_00001* self,
        int32_t* hotreloaded_out
    );

    /**
     * @brief Register a callback invoked before this extension is unloaded.
     *
     * @param[in] self       Initialization interface, must not be NULL.
     * @param[in] callback   Callback to invoke, or NULL to clear it.
     * @param[in] user_data  User data passed to the callback, can be NULL.
     *
     * @return Status code.
     * @retval      `SW_OK`                   Callback was registered.
     * @retval      `SW_E_INVALID_ARG`        Self is NULL.
     * @retval      `SW_E_INVALID_OPERATION`  Initialization has finished.
     *
     * @note Called while the core API and extension library are still valid,
     * including cleanup after failed initialization. Replacing the callback
     * replaces its user data. The host does not own user_data.
     */
    sw_status (*set_unload_callback)(
        const sw_init_00001* self,
        sw_extension_callback_fn callback,
        void* user_data
    );

    /**
     * @brief Register a callback invoked when the loaded extension set changes.
     *
     * @param[in] self       Initialization interface, must not be NULL.
     * @param[in] callback   Callback to invoke, or NULL to clear it.
     * @param[in] user_data  User data passed to the callback, can be NULL.
     *
     * @return Status code.
     * @retval      `SW_OK`                   Callback was registered.
     * @retval      `SW_E_INVALID_ARG`        Self is NULL.
     * @retval      `SW_E_INVALID_OPERATION`  Initialization has finished.
     *
     * @note Invoked once after the initial loading pass, then on all remaining
     * extensions after each successful load or unload. New extensions are
     * included after their initialization has returned. Failed loads and
     * unloads do not trigger it. The host does not own user_data.
     * @note Loading or unloading extensions during a lifecycle callback fails.
     */
    sw_status (*set_on_all_extensions_loaded_callback)(
        const sw_init_00001* self,
        sw_extension_callback_fn callback,
        void* user_data
    );

};

/** @brief Extension initialization API. */
typedef sw_init_00001 sw_init;

/** @brief Shared pointer API interface type. */
typedef struct sw_shared_00001 sw_shared_00001;

/** @brief Shared pointer registry APIs. */
struct sw_shared_00001 {

    /**
     * @brief Get a pointer from the host's shared pointer registry.
     *
     * @param[in]   self                Shared pointer interface, must not be NULL.
     * @param[in]   key                 Null-terminated key, must not be NULL.
     * @param[out]  shared_pointer_out  Pointer storage, must not be NULL.
     *                                  Receives the stored value on success;
     *                                  unchanged if the key does not exist.
     *
     * @return Status code.
     * @retval      `SW_OK`             Key exists; its value may be NULL.
     * @retval      `SW_E_INVALID_ARG`  A required pointer is NULL or the key does not exist.
     *
     * @note The registry does not transfer ownership of the pointed-to object.
     */
    sw_status (*get_shared_pointer)(
        const sw_shared_00001* self,
        const char* key,
        sw_ptr_out shared_pointer_out
    );

    /**
     * @brief Insert or replace a pointer in the host's shared pointer registry.
     *
     * @param[in]   self            Shared pointer interface, must not be NULL.
     * @param[in]   key             Null-terminated key, must not be NULL.
     * @param[in]   shared_pointer  Value to store, can be NULL.
     *
     * @return Status code.
     * @retval      `SW_E_INVALID_ARG`  Self or key is NULL.
     * @retval      `SW_OK`             Value was stored.
     *
     * @note The key is copied. The pointed-to object is not copied or owned by
     * the registry, and replacing an entry does not free its previous value.
     */
    sw_status (*set_shared_pointer)(
        const sw_shared_00001* self,
        const char* key,
        void* shared_pointer
    );

    /**
     * @brief Check whether a key exists in the shared pointer registry.
     *
     * @param[in]   self        Shared pointer interface, must not be NULL.
     * @param[in]   key         Null-terminated key, must not be NULL.
     * @param[out]  result_out  Result storage, must not be NULL. Receives 1 if
     *                          the key exists, or 0 otherwise.
     *
     * @return Status code.
     * @retval      `SW_E_INVALID_ARG`  Self, key, or output storage is NULL.
     * @retval      `SW_OK`             Result was written.
     *
     * @note A key with a NULL value still exists.
     */
    sw_status (*has_shared_pointer)(
        const sw_shared_00001* self,
        const char* key,
        int32_t* result_out
    );

    /**
     * @brief Remove a key from the shared pointer registry.
     *
     * @param[in]   self               Shared pointer interface, must not be NULL.
     * @param[in]   key                Null-terminated key, must not be NULL.
     *
     * @return Status code.
     * @retval      `SW_OK`             Entry was removed.
     * @retval      `SW_E_INVALID_ARG`  Self or key is NULL, or the key does not exist.
     *
     * @note Removing an entry does not free the pointed-to object.
     */
    sw_status (*remove_shared_pointer)(
        const sw_shared_00001* self,
        const char* key
    );

};

/** @brief Extension shared pointer API. */
typedef sw_shared_00001 sw_shared;

/** @brief Memory API interface type. */
typedef struct sw_memory_00001 sw_memory_00001;

/**
 * @brief Opaque handle to a function or virtual function hook.
 *
 * @note Release a handle exactly once with the matching unhook_address or
 * unhook_vtable callback. The handle is invalid after that call.
 */
typedef void* sw_hook_handle;

/**
 * @brief Memory allocation, hooks, and address lookup APIs.
 */
struct sw_memory_00001 {

    /**
     * @brief Allocate memory through the game's allocator.
     *
     * @param[in]   self          Memory interface, must not be NULL.
     * @param[in]   size          Number of bytes to allocate.
     * @param[out]  pointer_out   Pointer storage, must not be NULL. Receives
     *                            the allocation, or NULL on failure.
     *
     * @return Status code.
     * @retval      `SW_E_INVALID_ARG`  Self or output storage is NULL.
     * @retval      `SW_OK`             Allocation returned a non-NULL pointer.
     * @retval      `SW_E_FAILED`       Allocator returned NULL.
     *
     * @note Memory is not initialized. Release it with this interface's free
     * callback, or resize it with resize.
     */
    sw_status (*alloc)(
        const sw_memory_00001* self,
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
     * @retval      `SW_E_INVALID_ARG`  Self is NULL.
     * @retval      `SW_OK`             Free request was processed.
     */
    sw_status (*free)(
        const sw_memory_00001* self,
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
     * @retval      `SW_E_INVALID_ARG`  Self or output storage is NULL.
     * @retval      `SW_OK`             Resize request was processed, even if the
     *                           allocator returned NULL.
     *
     * @note For a nonzero new_size, check pointer_out for allocation failure
     * before replacing the original pointer. A successful resize may move
     * the allocation and invalidate the original pointer.
     * @note Zero-size behavior is determined by the game's allocator.
     */
    sw_status (*resize)(
        const sw_memory_00001* self,
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
     * @retval      `SW_E_INVALID_ARG`  A required pointer is NULL.
     * @retval      `SW_OK`             A non-NULL original trampoline was created.
     * @retval      `SW_E_FAILED`       Original trampoline is NULL; hook was not enabled.
     *
     * @note The callback and original function must use the target function's
     * signature and calling convention.
     * @note The original trampoline is checked before enabling the hook.
     * A failed hook is destroyed and both outputs are set to NULL.
     * @note Release the handle with unhook_address. The original trampoline
     * must not be used after the hook is removed.
     */
    sw_status (*hook_address)(
        const sw_memory_00001* self,
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
     * @retval      `SW_E_INVALID_ARG`  Self or handle is NULL.
     * @retval      `SW_OK`             Hook was disabled and destroyed.
     *
     * @note The handle and its original trampoline are invalid after this call.
     */
    sw_status (*unhook_address)(
        const sw_memory_00001* self,
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
     * @retval      `SW_E_INVALID_ARG`  A required pointer is NULL or the index exceeds INT_MAX.
     * @retval      `SW_OK`             A non-NULL original trampoline was created.
     * @retval      `SW_E_FAILED`       Original trampoline is NULL; hook was not enabled.
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
        const sw_memory_00001* self,
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
     * @retval      `SW_E_INVALID_ARG`  Self or handle is NULL.
     * @retval      `SW_OK`             Hook was disabled and destroyed.
     *
     * @note The handle and its original trampoline are invalid after this call.
     */
    sw_status (*unhook_vtable)(
        const sw_memory_00001* self,
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
     * @retval      `SW_OK`             A non-NULL address was found.
     * @retval      `SW_E_INVALID_ARG`  A required pointer is NULL or the name does not exist.
     * @retval      `SW_E_FAILED`       Stored address is NULL.
     */
    sw_status (*gamedata_resolve_signature)(
        const sw_memory_00001* self,
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
     * @retval      `SW_OK`             An offset was found, including zero.
     * @retval      `SW_E_INVALID_ARG`  A required pointer is NULL or the name does not exist.
     */
    sw_status (*gamedata_get_offset)(
        const sw_memory_00001* self,
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
     * @retval      `SW_E_INVALID_ARG`  A required pointer is NULL.
     * @retval      `SW_OK`             A non-NULL address was resolved.
     * @retval      `SW_EFAILED`        Scan failed or no address was resolved.
     *
     * @note The output is only valid on success.
     */
    sw_status (*resolve_signature)(
        const sw_memory_00001* self,
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
     * @retval      `SW_E_INVALID_ARG`  A required pointer is NULL.
     * @retval      `SW_OK`             A non-NULL table address was resolved.
     * @retval      `SW_EFAILED`        Lookup failed or no address was resolved.
     *
     * @note The output is only valid on success. The table belongs to the
     * library and must not be freed.
     * @see hook_vtable
     */
    sw_status (*find_vtable)(
        const sw_memory_00001* self,
        const char* library,
        const char* vtable,
        sw_ptr_out address_out
    );

    /**
     * @brief Look up a Valve interface.
     *
     * @param[in]   self            Memory interface, must not be NULL.
     * @param[in]   interface_name  Null-terminated interface version name,
     *                              must not be NULL.
     * @param[out]  interface_out   Pointer storage, must not be NULL. Receives
     *                              the interface, or NULL if it was not found.
     *
     * @retval      `SW_OK`             A non-NULL interface was found.
     * @retval      `SW_E_FAILED`       Interface lookup failed.
     * @retval      `SW_E_INVALID_ARG`  A required pointer is NULL.
     *
     * @note The returned interface belongs to the game and must not be freed.
     */
    sw_status (*get_valve_interface)(
        const sw_memory_00001* self,
        const char* interface_name,
        sw_ptr_out interface_out
    );

};

typedef sw_memory_00001 sw_memory;

/** @brief Schema API interface type. */
typedef struct sw_schema_00001 sw_schema_00001;

/** @brief Schema field lookup and network state change APIs. */
struct sw_schema_00001 {

    /**
     * @brief Look up a schema field's byte offset within its declaring class.
     *
     * @param[in]   self        Schema interface, must not be NULL.
     * @param[in]   class_name  Null-terminated schema class name, must not be NULL.
     * @param[in]   field_name  Null-terminated schema field name, must not be NULL.
     * @param[out]  offset_out  Result storage, must not be NULL. Receives the
     *                          byte offset on success; unchanged on failure.
     *
     * @retval      `SW_OK`             Field was found; its offset may be zero.
     * @retval      `SW_E_INVALID_ARG`  A required pointer is NULL or the class or field does not exist.
     *
     * @note Class and field names are case-sensitive. For inherited fields,
     * use the class that declares the field.
     */
    sw_status (*get_schema_offset)(
        const sw_schema_00001* self,
        const char* class_name,
        const char* field_name,
        uint32_t* offset_out
    );

    /**
     * @brief Notify the game that a schema field's value has changed.
     *
     * @param[in]  self        Schema interface, must not be NULL.
     * @param[in]  object      Live instance of the declaring class, must not be NULL.
     * @param[in]  class_name  Null-terminated schema class name, must not be NULL.
     * @param[in]  field_name  Null-terminated schema field name, must not be NULL.
     *
     * @retval      `SW_OK`             The field exists and the notification was submitted.
     * @retval      `SW_E_INVALID_ARG`  A required pointer is NULL or the class or field does not exist.
     *
     * @note Call after modifying the field, on the game thread. Names are
     * case-sensitive; use the class that declares the field.
     */
    sw_status (*set_state_changed)(
        const sw_schema_00001* self,
        void* object,
        const char* class_name,
        const char* field_name
    );
};

/** @brief Extension schema API. */
typedef sw_schema_00001 sw_schema;

struct sw_core_00001 {
    /** @brief Extension metadata registration APIs. */
    sw_init_00001* init;
    /** @brief Memory allocation, hooks, and address lookup APIs. */
    sw_memory_00001* memory;
    /** @brief Schema field lookup and network state change APIs. */
    sw_schema_00001* schema;
    /** @brief Shared pointer registry API. */
    sw_shared_00001* shared;
};

#endif
