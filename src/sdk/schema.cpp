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

#include "schema.h"

#include <fmt/format.h>
#include <unordered_map>

#include <core/entrypoint.h>

#include <api/interfaces/manager.h>
#include <api/shared/files.h>
#include <api/shared/jsonc.h>
#include <api/shared/plat.h>
#include <api/memory/virtual/call.h>

#include <s2binlib/s2binlib.h>

#define CBaseEntity_m_nSubclassID 0x9DC483B8C02CE796

std::unordered_map<uint64_t, SchemaField> offsets;
std::unordered_map<uint32_t, SchemaClass> classes;
std::unordered_map<uint64_t, uint64_t> inlineNetworkVarVtbs;

// Special inline classes for state changed
// These fields has vtable "CLASS::NetworkVar_FIELDNAME"
// which has the virtual function to call state changed without entity pointer

std::pair<const char*, const char*> inlineNetworkVarVtbNames[] = {
	{"sky3dparams_t", "fog"},
	{"CTriggerFan", "m_RampTimer"},
	{"CSkyCamera", "m_skyboxData"},
	{"CSkeletonInstance", "m_modelState"},
	{"CShatterGlassShardPhysics", "m_ShardDesc"},
	{"CPlayer_CameraServices", "m_audio"},
	{"CPlayer_CameraServices", "m_PlayerFog"},
	{"CPlantedC4", "m_entitySpottedState"},
	{"CPlantedC4", "m_AttributeManager"},
	{"CHostage", "m_reuseTimer"},
	{"CHostage", "m_entitySpottedState"},
	{"CGameSceneNode", "m_hParent"},
	{"CFogController", "m_fog"},
	{"CEnvWindController", "m_EnvWindShared"},
	{"CEnvWind", "m_EnvWindShared"},
	{"CEconItemView", "m_NetworkedDynamicAttributes"},
	{"CEconItemView", "m_AttributeList"},
	{"CEconEntity", "m_AttributeManager"},
	{"CCollisionProperty", "m_collisionAttribute"},
	{"CChicken", "m_AttributeManager"},
	{"CCSPlayer_ActionTrackingServices", "m_weaponPurchasesThisRound"},
	{"CCSPlayer_ActionTrackingServices", "m_weaponPurchasesThisMatch"},
	{"CCSPlayerPawn", "m_entitySpottedState"},
	{"CCSPlayerPawn", "m_EconGloves"},
	{"CCSPlayerController_ActionTrackingServices", "m_matchStats"},
	{"CCSGameRules", "m_RetakeRules"},
	{"CCSGO_TeamPreviewCharacterPosition", "m_weaponItem"},
	{"CCSGO_TeamPreviewCharacterPosition", "m_glovesItem"},
	{"CCSGO_TeamPreviewCharacterPosition", "m_agentItem"},
	{"CC4", "m_entitySpottedState"},
	{"CBodyComponentSkeletonInstance", "m_skeletonInstance"},
	{"CBodyComponentPoint", "m_sceneNode"},
	{"CBodyComponentBaseAnimGraph", "m_animationController"},
	{"CBasePlayerPawn", "m_skybox3d"},
	{"CBaseModelEntity", "m_Glow"},
	{"CBaseModelEntity", "m_Collision"},
	{"CBaseAnimGraphController", "m_animGraphNetworkedVars"},
	{"CBaseAnimGraph", "m_RagdollPose"},
	{"CAttributeContainer", "m_Item"},
};

class CNetworkVarChainer
{
public:
	CEntityInstance* m_pEntity;

private:
	uint8 pad_0000[24];

public:
	ChangeAccessorFieldPathIndex_t m_PathIndex;

private:
	uint8 pad_0024[4];
};

void CSDKSchema::Load()
{
	auto schemaSystem = g_ifaceService.FetchInterface<CSchemaSystem>(SCHEMASYSTEM_INTERFACE_VERSION);
	auto logger = g_ifaceService.FetchInterface<ILogger>(LOGGER_INTERFACE_VERSION);

	json sdkJson;

	logger->Info("SDK", "Loading inline network var vtables...\n");

	for (auto& name : inlineNetworkVarVtbNames) {
		void* vtable;
		int result = s2binlib_find_vtable_nested_2("server", name.first, (std::string("NetworkVar_") + name.second).c_str(), &vtable);
		if (result == 0) {
			uint64_t index;
			result = s2binlib_find_networkvar_vtable_statechanged((uint64_t)vtable, &index);
			if (result == 0) {
				inlineNetworkVarVtbs[(uint64_t)vtable] = index;
				logger->Info("SDK", fmt::format("Loaded vfunc '{}::{}->StateChanged' => {}.\n", name.first, name.second, index));
			}
			else {
				logger->Error("SDK", fmt::format("Failed to find inline network var vtable state changed: {}, error: {}\n", name.first, name.second, result));
			}
		}
		else {
			logger->Error("SDK", fmt::format("Failed to find inline network var vtable: {}::{}, error: {}\n", name.first, name.second, result));
		}
	}

	logger->Info("SDK", fmt::format("Loaded {} inline network var vtables.\n", inlineNetworkVarVtbs.size()));

	logger->Info("SDK", "Loading SDK classes...\n");

	auto gts = schemaSystem->GlobalTypeScope();

	int classes_count = gts->m_DeclaredClasses.m_Map.Count();

	FOR_EACH_MAP(gts->m_DeclaredClasses.m_Map, iter)
	{
		ReadClasses(gts->m_DeclaredClasses.m_Map.Element(iter), sdkJson);
	}

	for (int i = 0; i < schemaSystem->m_TypeScopes.GetNumStrings(); i++)
	{
		auto ts = schemaSystem->m_TypeScopes[i];

		classes_count += ts->m_DeclaredClasses.m_Map.Count();

		FOR_EACH_MAP(ts->m_DeclaredClasses.m_Map, iter)
		{
			ReadClasses(ts->m_DeclaredClasses.m_Map.Element(iter), sdkJson);
		}
	}

	logger->Info("SDK", fmt::format("Finished loading {} SDK classes ({} fields).\n", classes_count, offsets.size()));

	logger->Info("SDK", "Loading SDK enums...\n");

	int enums_count = gts->m_DeclaredEnums.m_Map.Count();

	FOR_EACH_MAP(gts->m_DeclaredEnums.m_Map, iter)
	{
		ReadEnums(gts->m_DeclaredEnums.m_Map.Element(iter), sdkJson);
	}

	for (int i = 0; i < schemaSystem->m_TypeScopes.GetNumStrings(); i++)
	{
		auto ts = schemaSystem->m_TypeScopes[i];

		enums_count += ts->m_DeclaredEnums.m_Map.Count();

		FOR_EACH_MAP(ts->m_DeclaredEnums.m_Map, iter)
		{
			ReadEnums(ts->m_DeclaredEnums.m_Map.Element(iter), sdkJson);
		}
	}

	logger->Info("SDK", fmt::format("Finished loading {} SDK enums.\n", enums_count));

	schemaSystem->PrintSchemaStats("");

	WriteJSON(g_SwiftlyCore.GetCorePath() + "gamedata/cs2/sdk.json", sdkJson);
}

void CSDKSchema::SetStateChanged(void* pEntity, const char* sClassName, const char* sMemberName)
{
	uint32_t class_hash = hash_32_fnv1a_const(sClassName);
	uint64_t fieldHash = ((uint64_t)(class_hash) << 32 | hash_32_fnv1a_const(sMemberName));

	SetStateChanged(pEntity, fieldHash);
}

void CSDKSchema::SetStateChanged(void* pEntity, uint64_t uHash)
{
	auto fieldData = offsets.find(uHash);
	if (fieldData == offsets.end()) return;

	auto& fieldInfo = fieldData->second;
	if (!fieldInfo.m_bNetworked) return;
	auto logger = g_ifaceService.FetchInterface<ILogger>(LOGGER_INTERFACE_VERSION);

	auto uncheckedNetworkVar = reinterpret_cast<NetworkVar*>(pEntity);
	auto it = inlineNetworkVarVtbs.find(uncheckedNetworkVar->pVtable());
	if (it != inlineNetworkVarVtbs.end()) {
		auto index = it->second;
		uncheckedNetworkVar->StateChanged(index, NetworkStateChangedData(fieldInfo.m_uOffset));
		return;
	}

	if (fieldInfo.m_bChainer) {
		CNetworkVarChainer* pChainer = (CNetworkVarChainer*)((uintptr_t)pEntity + fieldInfo.m_nChainerOffset);

		CEntityInstance* pEntity = pChainer->m_pEntity;
		if (pEntity)
			pEntity->NetworkStateChanged(NetworkStateChangedData(fieldInfo.m_uOffset, -1, pChainer->m_PathIndex));
	}
	else if (fieldInfo.m_bIsStruct) {
		logger->Error("SDK", fmt::format("State changed is called on an unsupported field (hash={}), please report this to the developer.\n", uHash));
		// NetworkStateChangedData data(fieldInfo.m_uOffset);
		// CALL_VIRTUAL(void, WIN_LINUX(27, 28), pEntity, &data);
	}
	else {
		reinterpret_cast<CEntityInstance*>(pEntity)->NetworkStateChanged(NetworkStateChangedData(fieldInfo.m_uOffset));
	}
}

int32_t CSDKSchema::FindChainOffset(const char* sClassName)
{
	return GetOffset(sClassName, "__m_pChainEntity");
}

int32_t CSDKSchema::GetOffset(const char* sClassName, const char* sMemberName)
{
	uint32_t class_hash = hash_32_fnv1a_const(sClassName);
	uint64_t fieldHash = ((uint64_t)(class_hash) << 32 | hash_32_fnv1a_const(sMemberName));
	return GetOffset(fieldHash);
}

int32_t CSDKSchema::GetOffset(uint64_t uHash)
{
	auto it = offsets.find(uHash);
	if (it == offsets.end()) return 0;
	else return it->second.m_uOffset;
}

bool CSDKSchema::IsStruct(const char* sClassName)
{
	auto it = classes.find(hash_32_fnv1a_const(sClassName));
	if (it == classes.end()) return false;
	return it->second.m_bIsStruct;
}

bool CSDKSchema::IsClassLoaded(const char* sClassName)
{
	return classes.contains(hash_32_fnv1a_const(sClassName));
}

void* CSDKSchema::GetPropPtr(void* pEntity, const char* sClassName, const char* sMemberName)
{
	uint32_t class_hash = hash_32_fnv1a_const(sClassName);
	uint64_t fieldHash = ((uint64_t)(class_hash) << 32 | hash_32_fnv1a_const(sMemberName));

	return GetPropPtr(pEntity, fieldHash);
}

void* CSDKSchema::GetPropPtr(void* pEntity, uint64_t uHash)
{
	auto it = offsets.find(uHash);
	if (it == offsets.end()) return nullptr;

	auto& fieldInfo = it->second;
	return reinterpret_cast<void*>((uintptr_t)pEntity + fieldInfo.m_uOffset);
}

void CSDKSchema::WritePropPtr(void* pEntity, const char* sClassName, const char* sMemberName, void* pValue, uint32_t size)
{
	uint32_t class_hash = hash_32_fnv1a_const(sClassName);
	uint64_t fieldHash = ((uint64_t)(class_hash) << 32 | hash_32_fnv1a_const(sMemberName));

	WritePropPtr(pEntity, fieldHash, pValue, size);
}

void CSDKSchema::WritePropPtr(void* pEntity, uint64_t uHash, void* pValue, uint32_t size)
{
	void* propPtr = GetPropPtr(pEntity, uHash);
	if (!propPtr) return;

	Plat_WriteMemory(propPtr, (uint8_t*)pValue, size);
}

void* CSDKSchema::GetVData(void* pEntity)
{
	void* subclassPtr = GetPropPtr(pEntity, CBaseEntity_m_nSubclassID);
	return *(void**)((uintptr_t)subclassPtr + 4);
}