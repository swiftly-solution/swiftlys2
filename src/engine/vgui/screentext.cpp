/************************************************************************************************
 * SwiftlyS2 is a scripting framework for Source2-based games.
 * Copyright (C) 2023-2026 Swiftly Solution SRL via Sava Andrei-Sebastian and it's contributors
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 ************************************************************************************************/

#include "screentext.h"

#include <api/interfaces/manager.h>

#include <api/memory/virtual/call.h>

#include <public/entity2/entitykeyvalues.h>
#include <public/mathlib/vector.h>

 /**
  * @todo: implement block transmit for other players using player manager & object
  * @todo: try to find a better impl because this one is fine, but the text looks like you have 20fps and still counting on my fingers
  */

#define BOTTOM -4.8
#define TOP_FROM_BOTTOM 10.13
#define LEFT -9.28
#define RIGHT_FROM_LEFT 18.5

class CNetworkedQuantizedFloat
{
public:
    float m_Value;
    uint16_t m_nEncoder;
    bool m_bUnflattened;
};

void CScreenText::Create(Color color, const std::string& font, int size, bool drawBackground, bool isMenu)
{
    m_col = color;
    m_font = font;
    m_size = size;
    m_drawBackground = drawBackground;
    m_isMenu = isMenu;

    static auto entitysystem = g_ifaceService.FetchInterface<IEntitySystem>(ENTITYSYSTEM_INTERFACE_VERSION);

    pScreenEntity.Set((CEntityInstance*)(entitysystem->CreateEntityByName("point_worldtext")));
    if (!pScreenEntity) return;

    CEntityKeyValues* pMenuKV = new CEntityKeyValues();

    pMenuKV->SetBool("enabled", true);
    pMenuKV->SetFloat("world_units_per_pixel", (0.25f / 1050) * size);
    pMenuKV->SetInt("justify_horizontal", 0);
    pMenuKV->SetInt("justify_vertical", 2);
    pMenuKV->SetInt("reorient_mode", 0);
    pMenuKV->SetInt("fullbright", 1);
    pMenuKV->SetFloat("font_size", size);
    pMenuKV->SetString("font_name", font.c_str());
    pMenuKV->SetColor("color", color);

    if (drawBackground) {
        pMenuKV->SetBool("draw_background", true);

        if (isMenu) {
            pMenuKV->SetFloat("background_border_width", 0.2);
            pMenuKV->SetFloat("background_border_height", 0.15);
        }
        else {
            static auto config = g_ifaceService.FetchInterface<IConfiguration>(CONFIGURATION_INTERFACE_VERSION);
            if (double* width = std::get_if<double>(&config->GetValue("core.VGUI.TextBackground.PaddingX")))
                pMenuKV->SetFloat("background_border_width", *width);
            if (double* height = std::get_if<double>(&config->GetValue("core.VGUI.TextBackground.PaddingY")))
                pMenuKV->SetFloat("background_border_height", *height);
        }

        pMenuKV->SetFloat("background_away_units", 0.04);
        pMenuKV->SetFloat("background_world_to_uv", 0.05);
    }

    entitysystem->Spawn(pScreenEntity.Get(), pMenuKV);
}

void CScreenText::SetText(const std::string& text)
{
    m_text = text;

    if (!pScreenEntity.IsValid()) return;
    if (!pScreenEntity) return;

    static auto entitysystem = g_ifaceService.FetchInterface<IEntitySystem>(ENTITYSYSTEM_INTERFACE_VERSION);
    entitysystem->AcceptInput(pScreenEntity.Get(), "SetMessage", nullptr, nullptr, text.c_str(), 0);
    entitysystem->AcceptInput(pScreenEntity.Get(), "Enable", nullptr, nullptr, "", 0);
}

void CScreenText::SetPosition(float posX, float posY)
{
    m_posX = posX;
    m_posY = posY;

    UpdatePosition();
}

void CScreenText::UpdatePosition()
{
    if (!m_player) return;
    if (m_player->IsFakeClient()) return;
    if (!pScreenEntity.IsValid()) return;
    if (!pScreenEntity) return;

    auto pawn = m_player->GetPlayerPawn();
    if (!pawn) return;

    static auto schema = g_ifaceService.FetchInterface<ISDKSchema>(SDKSCHEMA_INTERFACE_VERSION);

    if (*(uint32_t*)schema->GetPropPtr(pawn, 11368356186166639856) == 2) { // CBaseEntity::m_lifeState
        auto controller = m_player->GetController();
        if (!controller) return;
        if (*(bool*)schema->GetPropPtr(controller, 2948968942928542819)) return; // CCSPlayerController::m_bControllingBot

        auto& observerServices = *(void**)schema->GetPropPtr(pawn, 14568842447348147577); // CBasePlayerPawn::m_pObserverServices
        if (!observerServices) return;

        CHandle<CEntityInstance>& observerTarget = *(CHandle<CEntityInstance>*)schema->GetPropPtr(observerServices, 1590106406667131980); // CPlayer_ObserverServices::m_hObserverTarget
        if (!observerTarget) return;

        auto& observerController = *(CHandle<CEntityInstance>*)schema->GetPropPtr(observerTarget.Get(), 15634397247676853836); // CCSPlayerPawnBase::m_hOriginalController
        if (!observerController) return;

        CHandle<CEntityInstance>& pawnHandle = *(CHandle<CEntityInstance>*)schema->GetPropPtr(observerController, 2948968946114051708); // CCSPlayerController::m_hPlayerPawn
        if (!pawnHandle) return;
        pawn = (void*)(pawnHandle.Get());
    }

    if (!pawn) return;

    static auto gamedata = g_ifaceService.FetchInterface<IGameDataManager>(GAMEDATA_INTERFACE_VERSION);

    QAngle& eyeAngles = *(QAngle*)schema->GetPropPtr(pawn, 14366846385912177324); // CCSPlayerPawn::m_angEyeAngles
    Vector fwd, right, up;
    AngleVectors(eyeAngles, &fwd, &right, &up);

    Vector eyePos(0.0, 0.0, 0.0);

    float fwdOffset = 7;
    float rightOffset = (LEFT + (m_posX * RIGHT_FROM_LEFT));
    float upOffset = (BOTTOM + (m_posY * TOP_FROM_BOTTOM));

    eyePos += fwd * fwdOffset;
    eyePos += right * rightOffset;
    eyePos += up * upOffset;

    QAngle ang(0, eyeAngles.y + 270, 90 - eyeAngles.x);

    void*& bodyComponent = *(void**)schema->GetPropPtr(pawn, 11368356189195133893); // CBaseEntity::m_CBodyComponent
    if (!bodyComponent) return;

    void*& sceneNode = *(void**)schema->GetPropPtr(bodyComponent, 5688829619060421781); // CBodyComponent::m_pSceneNode
    if (!sceneNode) return;

    void* viewOffset = schema->GetPropPtr(pawn, 5870523440453878603); // CBaseModelEntity::m_vecViewOffset
    if (!viewOffset) return;

    CNetworkedQuantizedFloat& viewOffsetZ = *(CNetworkedQuantizedFloat*)schema->GetPropPtr(viewOffset, 1697243212355959693); // CNetworkViewOffsetVector::m_vecZ

    void* velocity = schema->GetPropPtr(pawn, 11368356187783788690); // CBaseEntity::m_vecVelocity
    if (!velocity) return;

    CNetworkedQuantizedFloat& velocityX = *(CNetworkedQuantizedFloat*)schema->GetPropPtr(velocity, 7191597421563705447); // CNetworkedQuantizedFloat::m_vecX
    CNetworkedQuantizedFloat& velocityY = *(CNetworkedQuantizedFloat*)schema->GetPropPtr(velocity, 7191597421546927828); // CNetworkedQuantizedFloat::m_vecY
    CNetworkedQuantizedFloat& velocityZ = *(CNetworkedQuantizedFloat*)schema->GetPropPtr(velocity, 7191597421597260685); // CNetworkedQuantizedFloat::m_vecZ

    eyePos += *(Vector*)schema->GetPropPtr(sceneNode, 15655952205019933413) + Vector(0, 0, viewOffsetZ.m_Value);

    Vector vel(velocityX.m_Value, velocityY.m_Value, velocityZ.m_Value);

    static int iTeleportOffset = gamedata->GetOffsets()->Fetch("CBaseEntity::Teleport");
    CALL_VIRTUAL(void, iTeleportOffset, pScreenEntity.Get(), &eyePos, &ang, &vel);
}

void CScreenText::SetPlayer(IPlayer* player)
{
    m_player = player;
}

void CScreenText::SetColor(Color color)
{
    m_col = color;

    if (!pScreenEntity.IsValid()) return;
    if (!pScreenEntity) return;

    static auto schema = g_ifaceService.FetchInterface<ISDKSchema>(SDKSCHEMA_INTERFACE_VERSION);
    Color& col = *(Color*)schema->GetPropPtr(pScreenEntity.Get(), "CPointWorldText", "m_Color");
    col = color;
}

bool CScreenText::IsValidEntity()
{
    return pScreenEntity.IsValid();
}

void CScreenText::RegenerateText(bool recreate)
{
    if (recreate) {
        static auto entitysystem = g_ifaceService.FetchInterface<IEntitySystem>(ENTITYSYSTEM_INTERFACE_VERSION);
        if (pScreenEntity.IsValid()) entitysystem->Despawn(pScreenEntity.Get());

        Create(m_col, m_font, m_size, m_drawBackground, m_isMenu);
        SetText(m_text);
        SetPosition(m_posX, m_posY);
    }
    else {
        SetPosition(m_posX, m_posY);
    }
}

IPlayer* CScreenText::GetPlayer()
{
    return m_player;
}

int CScreenText::GetEntityIndex()
{
    if (!pScreenEntity.IsValid()) return 0;
    if (!pScreenEntity) return 0;

    return pScreenEntity->GetEntityIndex().Get();
}

bool CScreenText::IsRenderingTo(CHandle<CEntityInstance> renderingTo)
{
    return renderingTo == pRenderingTo;
}

void CScreenText::SetRenderingTo(CEntityInstance* ent)
{
    pRenderingTo.Set(ent);
}

void CScreenText::ResetSpawnState()
{
    pScreenEntity.Term();
}