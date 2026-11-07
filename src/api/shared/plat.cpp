#include "plat.h"
#include <api/shared/profiler.h>
#include <cstring>

#include <safetyhook/safetyhook.hpp>


void Plat_WriteMemory(void* pPatchAddress, uint8_t* pPatch, int iPatchSize)
{
    PROFILE_FUNCTION();

    if (!pPatchAddress || !pPatch || iPatchSize <= 0)
        return;

    auto protect = safetyhook::unprotect(reinterpret_cast<uint8_t*>(pPatchAddress), iPatchSize);
    memcpy(pPatchAddress, pPatch, iPatchSize);
}