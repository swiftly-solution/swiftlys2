/************************************************************************************************
 *  SwiftlyS2 is a scripting framework for Source2-based games.
 *  Copyright (C) 2023-2026 Swiftly Solution SRL via Sava Andrei-Sebastian and it's contributors
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
 ************************************************************************************************/

#include "memory_impl_00001.h"

#include <api/interfaces/interfaces.h>
#include <core/entrypoint.h>
#include <memory/gamedata/manager.h>

SwMemoryImpl00001* from_self(const sw_memory_00001* self)
{
    return reinterpret_cast<SwMemoryImpl00001*>(const_cast<sw_memory_00001*>(self));
}

sw_status get_shared_pointer(
    const sw_memory_00001* self,
    const char* key,
    sw_ptr_out shared_pointer_out
)
{
    if (!g_pExtensionHost->HasSharedPointer(key))
        return SW_E_INVALID_ARG;
    
    *shared_pointer_out = g_pExtensionHost->GetSharedPointer(key);
    return SW_OK;
}

sw_status set_shared_pointer(
    const sw_memory_00001* self,
    const char* key,
    void* shared_pointer
)
{
    g_pExtensionHost->SetSharedPointer(key, shared_pointer);
    return SW_OK;
}

sw_status has_shared_pointer(
    const sw_memory_00001* self,
    const char* key,
    int32_t* result_out
)
{
    *result_out = g_pExtensionHost->HasSharedPointer(key) ? 1 : 0;
    return SW_OK;
}

sw_status remove_shared_pointer(
    const sw_memory_00001* self,
    const char* key
)
{
    if (!g_pExtensionHost->HasSharedPointer(key))
        return SW_E_INVALID_ARG;
    g_pExtensionHost->RemoveSharedPointer(key);
    return SW_OK;
}

sw_status alloc(
    const sw_memory_00001* self,
    uint64_t size,
    void** pointer_out
)
{
    *pointer_out = g_pMemoryAllocator->Alloc(size);
    if (*pointer_out == 0) return SW_E_FAILED;
    return SW_OK;
}


sw_status free(
    const sw_memory_00001* self,
    void* pointer
)
{
    g_pMemoryAllocator->Free(pointer);
    return SW_OK;
}

sw_status resize(
    const sw_memory_00001* self,
    void* pointer,
    uint64_t new_size,
    void** pointer_out
)
{
    *pointer_out = g_pMemoryAllocator->Resize(pointer, new_size);
    return SW_OK;
}

sw_status hook_address(
    const sw_memory_00001* self,
    void* address,
    void* hook_callback,
    sw_hook_handle* handle_out,
    void** original_out
)
{
    *handle_out = nullptr;
    *original_out = nullptr;

    auto hook = g_pHooksManager->CreateFunctionHook();
    hook->SetHookFunction(address, hook_callback);

    void* original = hook->GetOriginal();
    if (!original)
    {
        g_pHooksManager->DestroyFunctionHook(hook);
        return SW_E_FAILED;
    }

    hook->Enable();

    *handle_out = hook;
    *original_out = original;
    return SW_OK;
}

sw_status unhook_address(
    const sw_memory_00001* self,
    const sw_hook_handle handle
)
{
    IFunctionHook* hook = reinterpret_cast<IFunctionHook*>(const_cast<sw_hook_handle>(handle));
    
    hook->Disable();
    g_pHooksManager->DestroyFunctionHook(hook);
    return SW_OK;
}

sw_status hook_vtable(
    const sw_memory_00001* self,
    void* vtable,
    uint32_t offset,
    void* hook_callback,
    sw_hook_handle* handle_out,
    void** original_out
)
{
    *handle_out = nullptr;
    *original_out = nullptr;

    IVFunctionHook* hook = g_pHooksManager->CreateVFunctionHook();
    hook->SetHookFunction(vtable, offset, hook_callback, true);

    void* original = hook->GetOriginal();
    if (!original)
    {
        g_pHooksManager->DestroyVFunctionHook(hook);
        return SW_E_FAILED;
    }

    hook->Enable();

    *handle_out = hook;
    *original_out = original;
    return SW_OK;
}

sw_status unhook_vtable(
    const sw_memory_00001* self,
    const sw_hook_handle handle
)
{
    IVFunctionHook* hook = reinterpret_cast<IVFunctionHook*>(const_cast<sw_hook_handle>(handle));
    
    hook->Disable();
    g_pHooksManager->DestroyVFunctionHook(hook);
    return SW_OK;
}

sw_status gamedata_resolve_signature(
    const sw_memory_00001* self,
    const char* name,
    void** address_out
)
{
    if (!g_pGameDataManager->GetSignatures()->Exists(name))
        return SW_E_INVALID_ARG;

    void* result = g_pGameDataManager->GetSignatures()->Fetch(name);
    *address_out = result;
    if (!result)
        return SW_E_FAILED;
    
    return SW_OK;
}

sw_status gamedata_get_offset(
    const sw_memory_00001* self,
    const char* name,
    uint32_t* offset_out
)
{
    if (!g_pGameDataManager->GetOffsets()->Exists(name))
        return SW_E_INVALID_ARG;

    uint32_t result = g_pGameDataManager->GetOffsets()->Fetch(name);
    *offset_out = result;
    
    return SW_OK;
}

sw_status resolve_signature(
    const sw_memory_00001* self,
    const char* library,
    const char* pattern,
    void** address_out
)
{
    auto err = g_pS2BinLib->PatternScan(library, pattern, address_out);
    if (err)
        return SW_E_FAILED;
    if (!*address_out)
        return SW_E_FAILED;

    return SW_OK;
}

sw_status find_vtable(
    const sw_memory_00001* self,
    const char* library,
    const char* vtable,
    void** address_out
)
{
    auto err = g_pS2BinLib->FindVtable(library, vtable, address_out);

    if (err)
        return SW_E_FAILED;
    if (!*address_out)
        return SW_E_FAILED;
    
    return SW_OK;
}

sw_status get_valve_interface(
    const sw_memory_00001* self,
    const char* interface_name,
    sw_ptr_out interface_out
)
{
    if (!self || !interface_name || !interface_out)
        return SW_E_INVALID_ARG;

    *interface_out = g_SwiftlyCore.GetInterface(interface_name);
    if (!*interface_out)
        return SW_E_FAILED;

    return SW_OK;
}

SwMemoryImpl00001 g_SwMemoryImpl00001{
    .api = {
        .get_shared_pointer = get_shared_pointer,
        .set_shared_pointer = set_shared_pointer,
        .has_shared_pointer = has_shared_pointer,
        .remove_shared_pointer = remove_shared_pointer,
        .alloc = alloc,
        .free = free,
        .resize = resize,
        .hook_address = hook_address,
        .unhook_address = unhook_address,
        .hook_vtable = hook_vtable,
        .unhook_vtable = unhook_vtable,
        .gamedata_resolve_signature = gamedata_resolve_signature,
        .gamedata_get_offset = gamedata_get_offset,
        .resolve_signature = resolve_signature,
        .find_vtable = find_vtable,
        .get_valve_interface = get_valve_interface
    }
};
