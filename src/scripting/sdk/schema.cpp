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

#include <api/interfaces/manager.h>
#include <scripting/scripting.h>

#include <public/schemasystem/schemasystem.h>
#include <public/tier1/utlstring.h>

#include <string>
#include <vector>

void Bridge_SDK_Schema_SetStateChanged(void* pEntity, uint64_t uHash)
{
    static auto schema = g_ifaceService.FetchInterface<ISDKSchema>(SDKSCHEMA_INTERFACE_VERSION);
    schema->SetStateChanged(pEntity, uHash);
}

uint32_t Bridge_SDK_Schema_FindChainOffset(const char* sClassName)
{
    static auto schema = g_ifaceService.FetchInterface<ISDKSchema>(SDKSCHEMA_INTERFACE_VERSION);
    return schema->FindChainOffset(sClassName);
}

int32_t Bridge_SDK_Schema_GetOffset(uint64_t uHash)
{
    static auto schema = g_ifaceService.FetchInterface<ISDKSchema>(SDKSCHEMA_INTERFACE_VERSION);
    return schema->GetOffset(uHash);
}

bool Bridge_SDK_Schema_IsStruct(const char* sClassName)
{
    static auto schema = g_ifaceService.FetchInterface<ISDKSchema>(SDKSCHEMA_INTERFACE_VERSION);
    return schema->IsStruct(sClassName);
}

bool Bridge_SDK_Schema_IsClassLoaded(const char* sClassName)
{
    static auto schema = g_ifaceService.FetchInterface<ISDKSchema>(SDKSCHEMA_INTERFACE_VERSION);
    return schema->IsClassLoaded(sClassName);
}

void* Bridge_SDK_Schema_GetPropPtr(void* pEntity, uint64_t uHash)
{
    static auto schema = g_ifaceService.FetchInterface<ISDKSchema>(SDKSCHEMA_INTERFACE_VERSION);
    if (!schema)
        return nullptr;

    return schema->GetPropPtr(pEntity, uHash);
}

void Bridge_SDK_Schema_WritePropPtr(void* pEntity, uint64_t uHash, void* pValue, uint32_t size)
{
    static auto schema = g_ifaceService.FetchInterface<ISDKSchema>(SDKSCHEMA_INTERFACE_VERSION);
    schema->WritePropPtr(pEntity, uHash, pValue, size);
}

void* Bridge_SDK_Schema_GetVData(void* pEntity)
{
    static auto schema = g_ifaceService.FetchInterface<ISDKSchema>(SDKSCHEMA_INTERFACE_VERSION);
    return schema->GetVData(pEntity);
}

void* Bridge_SDK_Schema_GetDatamapFunction(uint32_t uHash)
{
    static auto schema = g_ifaceService.FetchInterface<ISDKSchema>(SDKSCHEMA_INTERFACE_VERSION);
    return schema->GetDatamapFunction(uHash);
}

DEFINE_NATIVE("Schema.SetStateChanged", Bridge_SDK_Schema_SetStateChanged);
DEFINE_NATIVE("Schema.FindChainOffset", Bridge_SDK_Schema_FindChainOffset);
DEFINE_NATIVE("Schema.GetOffset", Bridge_SDK_Schema_GetOffset);
DEFINE_NATIVE("Schema.IsStruct", Bridge_SDK_Schema_IsStruct);
DEFINE_NATIVE("Schema.IsClassLoaded", Bridge_SDK_Schema_IsClassLoaded);
DEFINE_NATIVE("Schema.GetPropPtr", Bridge_SDK_Schema_GetPropPtr);
DEFINE_NATIVE("Schema.WritePropPtr", Bridge_SDK_Schema_WritePropPtr);
DEFINE_NATIVE("Schema.GetVData", Bridge_SDK_Schema_GetVData);
DEFINE_NATIVE("Schema.GetDatamapFunction", Bridge_SDK_Schema_GetDatamapFunction);

// ── Entity Schema Field Walker ──────────────────────────────────────────
// Recursively walks CSchemaClassInfo tree for an entity, outputting JSON
// with field name, type, offset, value, and nested children.

static inline int  PeekI32(const void* p) { int v; memcpy(&v, p, 4); return v; }
static inline unsigned PeekU32(const void* p) { unsigned v; memcpy(&v, p, 4); return v; }
static inline float PeekF32(const void* p) { float v; memcpy(&v, p, 4); return v; }
static inline long long PeekI64(const void* p) { long long v; memcpy(&v, p, 8); return v; }
static inline void* PeekPtr(const void* p) { void* v; memcpy(&v, p, sizeof(v)); return v; }

// ── JSON string escaping with UTF-8 → \uXXXX ──────────────────────────
static std::string JsonEscape(const std::string& s) {
    std::string out;
    for (size_t i = 0; i < s.size(); ) {
        unsigned char c = (unsigned char)s[i];
        if (c == '"' || c == '\\') { out += '\\'; out += (char)c; i++; }
        else if (c < 0x20) {                                        // control character → \u00XX
            char e[8]; snprintf(e, sizeof(e), "\\u%04x", c); out += e; i++;
        } else if (c >= 0x80) {
            // Check first byte to determine UTF-8 sequence length
            int len = 0; unsigned cp = 0;
            if      (c < 0xE0) { len = 2; cp = c & 0x1F; }         // 2-byte sequence (Latin, Greek, etc.)
            else if (c < 0xF0) { len = 3; cp = c & 0x0F; }         // 3-byte sequence (CJK, etc.)
            else if (c < 0xF8) { len = 4; cp = c & 0x07; }         // 4-byte sequence (emoji, etc.)

            if (len >= 2 && i + len <= s.size()) {
                bool ok = true;
                for (int j = 1; ok && j < len; j++) {
                    unsigned char nc = (unsigned char)s[i + j];
                    if ((nc >> 6) != 2) ok = false;                 // continuation byte must start with bits '10'
                    else cp = (cp << 6) | (nc & 0x3F);
                }
                if (ok) {
                    if (cp <= 0xFFFF) { char e[8]; snprintf(e,sizeof(e),"\\u%04x",cp); out += e; }
                    else {
                        // Encode as UTF-16 surrogate pair (U+D800..U+DFFF)
                        unsigned hi = 0xD800 + ((cp - 0x10000) >> 10);
                        unsigned lo = 0xDC00 + ((cp - 0x10000) & 0x3FF);
                        char e[14]; snprintf(e,sizeof(e),"\\u%04x\\u%04x",hi,lo); out += e;
                    }
                    i += len; continue;
                }
            }
            // Invalid UTF-8 byte — escape it as-is
            char e[8]; snprintf(e,sizeof(e),"\\u%04x",c); out += e; i++;
        } else { out += s[i++]; }
    }
    return out;
}

// ── Type name lookup ────────────────────────────────────────────────────
static const char* GetTypeName(CSchemaType* pType) {
    auto cat = pType->m_eTypeCategory;
    if (cat == SCHEMA_TYPE_DECLARED_CLASS) {
        auto* dc = (CSchemaType_DeclaredClass*)pType;
        return dc->m_pClassInfo ? dc->m_pClassInfo->m_pszName : "class";
    }
    if (cat == SCHEMA_TYPE_POINTER) return "ptr";
    if (cat == SCHEMA_TYPE_ATOMIC) {
        if (pType->m_eAtomicCategory == SCHEMA_ATOMIC_COLLECTION_OF_T) return "CUtlVector";
        if (pType->m_eAtomicCategory == SCHEMA_ATOMIC_T) return "CHandle";
        auto* at = (CSchemaType_Atomic*)pType;
        return at->m_pAtomicInfo ? at->m_pAtomicInfo->m_pszName : "atomic";
    }
    if (cat == SCHEMA_TYPE_FIXED_ARRAY) return "fixedarray";
    if (cat == SCHEMA_TYPE_DECLARED_ENUM) return "enum";
    if (cat == SCHEMA_TYPE_BUILTIN) {
        static const char* kBuiltin[] = { "bool","int8","uint8","int16","uint16","int32","uint32","int64","uint64","float","double" };
        int idx = (int)((CSchemaType_Builtin*)pType)->m_eBuiltinType;
        return (idx >= 0 && idx < 10) ? kBuiltin[idx] : "builtin";
    }
    return "?";
}

// ── Value formatting ────────────────────────────────────────────────────
static std::string FormatAtomicValue(void* addr, CSchemaType_Atomic* pType) {
    if (!pType->m_pAtomicInfo) return "?";
    const char* n = pType->m_pAtomicInfo->m_pszName; if (!n) return "?";
    char b[256];

    // Vector / Color types — read known layout
    if (!strcmp(n, "Vector")) { float* v = (float*)addr; snprintf(b,sizeof(b),"%f %f %f",(double)v[0],(double)v[1],(double)v[2]); return b; }
    if (!strcmp(n, "Vector2D")) { float* v = (float*)addr; snprintf(b,sizeof(b),"%f %f",(double)v[0],(double)v[1]); return b; }
    if (!strcmp(n, "Vector4D")) { float* v = (float*)addr; snprintf(b,sizeof(b),"%f %f %f %f",(double)v[0],(double)v[1],(double)v[2],(double)v[3]); return b; }
    if (!strcmp(n, "QAngle")) { float* v = (float*)addr; snprintf(b,sizeof(b),"%f %f %f",(double)v[0],(double)v[1],(double)v[2]); return b; }
    if (!strcmp(n, "Color")) { uint8_t* c = (uint8_t*)addr; snprintf(b,sizeof(b),"%d %d %d %d",c[0],c[1],c[2],c[3]); return b; }
    // String types
    if (!strcmp(n, "CUtlString")) { const char* s = *(const char**)addr; return s ? "\""+std::string(s)+"\"" : "\"\""; }
    if (!strcmp(n, "CUtlStringToken")) { snprintf(b,sizeof(b),"hash=%u",PeekU32(addr)); return b; }
    // Numeric scalars
    if (!strcmp(n, "float32")) { snprintf(b,sizeof(b),"%f",(double)PeekF32(addr)); return b; }
    if (!strcmp(n, "float64")) { double v; memcpy(&v,addr,8); snprintf(b,sizeof(b),"%g",v); return b; }
    if (!strcmp(n, "int32"))  { snprintf(b,sizeof(b),"%d",PeekI32(addr)); return b; }
    if (!strcmp(n, "uint32")) { snprintf(b,sizeof(b),"%u",PeekU32(addr)); return b; }
    if (!strcmp(n, "int64"))  { snprintf(b,sizeof(b),"%lld",PeekI64(addr)); return b; }
    if (!strcmp(n, "uint64")) { unsigned long long v; memcpy(&v,addr,8); snprintf(b,sizeof(b),"%llu",v); return b; }
    if (!strcmp(n, "bool"))   { return PeekI32(addr) ? "true" : "false"; }
    // Entity handles
    if (!strcmp(n, "CEntityIndex")) { snprintf(b,sizeof(b),"#%d",PeekI32(addr)); return b; }
    if (!strcmp(n, "CEntityHandle") || !strcmp(n, "CHandle")) { unsigned r = PeekU32(addr); snprintf(b,sizeof(b),"#%u (e=%u s=%u)",r,r&0x3FFF,r>>14); return b; }
    if (!strcmp(n, "CNetworkedQuantizedFloat")) { snprintf(b,sizeof(b),"%f",(double)PeekF32(addr)); return b; }
    snprintf(b,sizeof(b),"?(%s)",n); return b;
}

static std::string FormatBuiltinValue(void* addr, CSchemaType_Builtin* bt) {
    char b[64];
    switch (bt->m_eBuiltinType) {
    case SCHEMA_BUILTIN_TYPE_BOOL:    return PeekI32(addr) ? "true" : "false";
    case SCHEMA_BUILTIN_TYPE_CHAR:    case SCHEMA_BUILTIN_TYPE_INT8:  snprintf(b,sizeof(b),"%d",*(int8_t*)addr); break;
    case SCHEMA_BUILTIN_TYPE_UINT8:   snprintf(b,sizeof(b),"%u",*(uint8_t*)addr); break;
    case SCHEMA_BUILTIN_TYPE_INT16:   snprintf(b,sizeof(b),"%d",*(int16_t*)addr); break;
    case SCHEMA_BUILTIN_TYPE_UINT16:  snprintf(b,sizeof(b),"%u",*(uint16_t*)addr); break;
    case SCHEMA_BUILTIN_TYPE_INT32:   snprintf(b,sizeof(b),"%d",PeekI32(addr)); break;
    case SCHEMA_BUILTIN_TYPE_UINT32:  snprintf(b,sizeof(b),"%u",PeekU32(addr)); break;
    case SCHEMA_BUILTIN_TYPE_INT64:   snprintf(b,sizeof(b),"%lld",PeekI64(addr)); break;
    case SCHEMA_BUILTIN_TYPE_UINT64:  { unsigned long long v; memcpy(&v,addr,8); snprintf(b,sizeof(b),"%llu",v); break; }
    case SCHEMA_BUILTIN_TYPE_FLOAT32: snprintf(b,sizeof(b),"%f",(double)PeekF32(addr)); break;
    case SCHEMA_BUILTIN_TYPE_FLOAT64: { double v; memcpy(&v,addr,8); snprintf(b,sizeof(b),"%g",v); break; }
    default: return "?";
    }
    return b;
}

// Reads value at addr for pType, sets isNested if children should be expanded
static void FormatFieldValue(void* addr, CSchemaType* pType, bool& isNested, std::string& out) {
    if (!addr) { out = "[null]"; return; }
    auto cat = pType->m_eTypeCategory;

    if (cat == SCHEMA_TYPE_BUILTIN) { out = FormatBuiltinValue(addr, (CSchemaType_Builtin*)pType); return; }
    if (cat == SCHEMA_TYPE_POINTER) { void* p = PeekPtr(addr); char b[32]; snprintf(b,sizeof(b),p?"0x%p":"nullptr",p); out = b; return; }
    if (cat == SCHEMA_TYPE_ATOMIC) {
        auto a = pType->m_eAtomicCategory;
        if (a == SCHEMA_ATOMIC_PLAIN || a == SCHEMA_ATOMIC_T) { out = FormatAtomicValue(addr, (CSchemaType_Atomic*)pType); return; }
    }
    if (cat == SCHEMA_TYPE_DECLARED_CLASS) {
        auto* dc = (CSchemaType_DeclaredClass*)pType;
        out = dc->m_pClassInfo ? dc->m_pClassInfo->m_pszName : "class";
        isNested = true; return;
    }
    if (cat == SCHEMA_TYPE_DECLARED_ENUM) {
        auto* de = (CSchemaType_DeclaredEnum*)pType;
        if (de->m_pEnumInfo)
            for (int i = 0; i < de->m_pEnumInfo->m_nEnumeratorCount; i++)
                if (de->m_pEnumInfo->m_pEnumerators[i].m_nValue == PeekI32(addr))
                    { out = de->m_pEnumInfo->m_pEnumerators[i].m_pszName; return; }
        char b[32]; snprintf(b,sizeof(b),"%d",PeekI32(addr)); out = b; return;
    }
    if (cat == SCHEMA_TYPE_FIXED_ARRAY) {
        auto* fa = (CSchemaType_FixedArray*)pType;
        // Char arrays are treated as strings
        if (fa->m_pElementType->m_eTypeCategory == SCHEMA_TYPE_BUILTIN &&
            ((CSchemaType_Builtin*)fa->m_pElementType)->m_eBuiltinType == SCHEMA_BUILTIN_TYPE_CHAR)
            { out = "\"" + std::string((char*)addr) + "\""; return; }
        isNested = true; return;
    }
    out = "?";
}

// Helper: append one element of a collection/fixed-array as JSON
static void WalkSchemaFields(void* pEntity, CSchemaClassInfo* pSchema, std::string& json, int depth);

static void AppendElement(void* elem, CSchemaType* elemType, int index, int offset, std::string& json, int depth) {
    std::string val; bool nest = false;
    FormatFieldValue(elem, elemType, nest, val);
    json += "{\"name\":\"[" + std::to_string(index) + "]\",\"type\":\"elem\",\"offset\":" + std::to_string(offset) + ",\"value\":\"" + JsonEscape(val) + "\",\"children\":";
    if (nest && depth < 4 && elemType->m_eTypeCategory == SCHEMA_TYPE_DECLARED_CLASS) {
        auto* dc = (CSchemaType_DeclaredClass*)elemType;
        json += "[";
        if (dc->m_pClassInfo) WalkSchemaFields(elem, dc->m_pClassInfo, json, depth + 1);
        if (!json.empty() && json.back() == ',') json.pop_back();
        json += "]";
    } else json += "[]";
    json += "},";
}

static void WalkSchemaFields(void* pEntity, CSchemaClassInfo* pSchema, std::string& json, int depth) {
    if (!pEntity || !pSchema || depth > 4) return;

    for (int i = 0; i < pSchema->m_nFieldCount; i++) {
        auto& field = pSchema->m_pFields[i];
        auto* pType = field.m_pType; if (!pType) continue;
        void* fieldPtr = (uint8_t*)pEntity + field.m_nSingleInheritanceOffset;

        std::string val; bool isNested = false;
        FormatFieldValue(fieldPtr, pType, isNested, val);

        json += "{\"name\":\"" + JsonEscape(field.m_pszName ? field.m_pszName : "") + "\"";
        json += ",\"type\":\"" + JsonEscape(GetTypeName(pType)) + "\"";
        json += ",\"offset\":" + std::to_string(field.m_nSingleInheritanceOffset);
        json += ",\"value\":\"" + JsonEscape(val) + "\"";
        json += ",\"children\":";

        if (isNested && depth < 4) {
            size_t start = json.size(); json += "[";
            auto cat = pType->m_eTypeCategory;

            if (cat == SCHEMA_TYPE_DECLARED_CLASS) {
                auto* dc = (CSchemaType_DeclaredClass*)pType;
                if (dc->m_pClassInfo) {
                    WalkSchemaFields(fieldPtr, dc->m_pClassInfo, json, depth + 1);
                    for (int b = 0; b < dc->m_pClassInfo->m_nBaseClassCount; b++)
                        WalkSchemaFields(fieldPtr, dc->m_pClassInfo->m_pBaseClasses[b].m_pClass, json, depth + 1);
                }
            } else if (cat == SCHEMA_TYPE_ATOMIC && pType->m_eAtomicCategory == SCHEMA_ATOMIC_COLLECTION_OF_T) {
                auto* col = (CSchemaType_Atomic_CollectionOfT*)pType;
                int count = (int)(intptr_t)col->m_pfnManipulator(SCHEMA_COLLECTION_MANIPULATOR_ACTION_GET_COUNT, fieldPtr, 0, 0);
                for (int ei = 0; ei < count && ei < 50; ei++)
                    AppendElement((void*)col->m_pfnManipulator(SCHEMA_COLLECTION_MANIPULATOR_ACTION_GET_ELEMENT_CONST, fieldPtr, ei, 0), col->m_pTemplateType, ei, 0, json, depth + 1);
            } else if (cat == SCHEMA_TYPE_FIXED_ARRAY) {
                auto* fa = (CSchemaType_FixedArray*)pType;
                for (int ei = 0; ei < fa->m_nElementCount && ei < 50; ei++)
                    AppendElement((uint8_t*)fieldPtr + fa->m_nElementSize * ei, fa->m_pElementType, ei, (int)(fa->m_nElementSize * ei), json, depth + 1);
            }

            if (json.size() == start + 1) json += "]";
            else { if (json.back() == ',') json.pop_back(); json += "]"; }
        } else json += "[]";
        json += "},";
    }
    for (int i = 0; i < pSchema->m_nBaseClassCount; i++)
        WalkSchemaFields(pEntity, pSchema->m_pBaseClasses[i].m_pClass, json, depth);
}

char* Bridge_SDK_Schema_GetEntityFields(int* outSize, void* pEntity, const char* className)
{
    static auto memory = g_ifaceService.FetchInterface<IMemoryAllocator>(MEMORYALLOCATOR_INTERFACE_VERSION);
    if (!pEntity || !className || !className[0]) { *outSize = 2; char* o = (char*)memory->Alloc(3); memcpy(o,"[]",2); o[2]=0; return o; }

    static auto ss = g_ifaceService.FetchInterface<ISchemaSystem>(SCHEMASYSTEM_INTERFACE_VERSION);
    auto* scope = ss ? ss->FindTypeScopeForModule("server.dll") : nullptr;
    auto* pSchema = scope ? scope->FindDeclaredClass(className).Get() : nullptr;

    if (!pSchema) { *outSize = 2; char* o = (char*)memory->Alloc(3); memcpy(o,"[]",2); o[2]=0; return o; }

    std::string json = "[";
    WalkSchemaFields(pEntity, pSchema, json, 0);
    if (json.size() > 1 && json.back() == ',') json.pop_back();
    json += "]";

    int len = (int)json.size(); *outSize = len;
    char* o = (char*)memory->Alloc(len + 1);
    memory->Copy(o, (void*)json.data(), len);
    o[len] = 0;
    return o;
}

DEFINE_NATIVE("Schema.GetEntityFields", Bridge_SDK_Schema_GetEntityFields);