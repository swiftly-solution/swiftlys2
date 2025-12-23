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

#ifndef src_api_engine_convars_convars_h
#define src_api_engine_convars_convars_h

#include <string>
#include <functional>
#include <variant>
#include <optional>

#include <public/tier1/convar.h>
#include <public/mathlib/vector2d.h>
#include <public/mathlib/vector.h>
#include <public/mathlib/vector4d.h>

using ConvarValue = std::variant<int16_t, uint16_t, int32_t, uint32_t, int64_t, uint64_t, bool, float, double, Color, Vector2D, Vector, Vector4D, QAngle, std::string>;

class IConvarManager
{
public:
    virtual void Initialize() = 0;
    virtual void Shutdown() = 0;

    virtual void QueryClientConvar(int playerid, std::string cvar_name) = 0;
    virtual int AddQueryClientCvarCallback(std::function<void(int, std::string, std::string)> callback) = 0;
    virtual void RemoveQueryClientCvarCallback(int callback_id) = 0;
    virtual void OnClientQueryCvar(int playerid, std::string cvar_name, std::string cvar_value) = 0;

    virtual void CreateConvar(std::string cvar_name, EConVarType type, uint64_t flags, const char* help_message, ConvarValue defaultValue, std::optional<ConvarValue> minValue = std::nullopt, std::optional<ConvarValue> maxValue = std::nullopt) = 0;
    virtual void DeleteConvar(std::string cvar_name) = 0;
    virtual bool ExistsConvar(std::string cvar_name) = 0;
    virtual EConVarType GetConvarType(std::string cvar_name) = 0;

    virtual void* GetConvarDataAddress(std::string cvar_name) = 0;
    virtual ConvarValue GetConvarValue(std::string cvar_name) = 0;

    virtual void SetConvarValue(std::string cvar_name, ConvarValue value) = 0;
    virtual void SetClientConvar(int playerid, const std::string& cvar_name, const std::string& value) = 0;

    virtual void AddFlags(std::string cvar_name, uint64_t flags) = 0;
    virtual void RemoveFlags(std::string cvar_name, uint64_t flags) = 0;
    virtual void ClearFlags(std::string cvar_name) = 0;
    virtual uint64_t GetFlags(std::string cvar_name) = 0;

    virtual uint64_t AddGlobalChangeListener(std::function<void(const char*, int, const char*, const char*)> callback) = 0;
    virtual void RemoveGlobalChangeListener(uint64_t callback_id) = 0;

    virtual uint64_t AddConvarCreatedListener(std::function<void(const char*)> callback) = 0;
    virtual void RemoveConvarCreatedListener(uint64_t callback_id) = 0;

    virtual uint64_t AddConCommandCreatedListener(std::function<void(const char*)> callback) = 0;
    virtual void RemoveConCommandCreatedListener(uint64_t callback_id) = 0;
};

#endif