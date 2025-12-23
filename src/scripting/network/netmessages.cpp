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

#include <api/interfaces/manager.h>
#include <scripting/scripting.h>

#include <public/engine/igameeventsystem.h>
#include <public/networksystem/inetworkmessages.h>

#include <api/sdk/recipientfilter.h>

#ifdef GetMessage
#undef GetMessage
#endif

#define GETCHECK_FIELD(return_value)                                                                                                                                                                                                                           \
    if (!msg)                                                                                                                                                                                                                                                  \
        return return_value;                                                                                                                                                                                                                                   \
    const google::protobuf::FieldDescriptor* field = msg->GetDescriptor()->FindFieldByName(fieldName);                                                                                                                                                         \
    if (!field)                                                                                                                                                                                                                                                \
    {                                                                                                                                                                                                                                                          \
        return return_value;                                                                                                                                                                                                                                   \
    }

#define GETCHECK_FIELD_VOID()                                                                                                                                                                                                                                  \
    if (!msg)                                                                                                                                                                                                                                                  \
        return;                                                                                                                                                                                                                                                \
    const google::protobuf::FieldDescriptor* field = msg->GetDescriptor()->FindFieldByName(fieldName);                                                                                                                                                         \
    if (!field)                                                                                                                                                                                                                                                \
    {                                                                                                                                                                                                                                                          \
        return;                                                                                                                                                                                                                                                \
    }

#define CHECK_FIELD_NOT_REPEATED_VOID()                                                                                                                                                                                                                        \
    if (field->label() == google::protobuf::FieldDescriptor::LABEL_REPEATED)                                                                                                                                                                                   \
    {                                                                                                                                                                                                                                                          \
        return;                                                                                                                                                                                                                                                \
    }

#define CHECK_FIELD_NOT_REPEATED(return_value)                                                                                                                                                                                                                 \
    if (field->label() == google::protobuf::FieldDescriptor::LABEL_REPEATED)                                                                                                                                                                                   \
    {                                                                                                                                                                                                                                                          \
        return return_value;                                                                                                                                                                                                                                   \
    }

#define CHECK_FIELD_TYPE_VOID(type)                                                                                                                                                                                                                            \
    if (field->cpp_type() != google::protobuf::FieldDescriptor::CPPTYPE_##type)                                                                                                                                                                                \
    {                                                                                                                                                                                                                                                          \
        return;                                                                                                                                                                                                                                                \
    }

#define CHECK_FIELD_TYPE(type, return_value)                                                                                                                                                                                                                   \
    if (field->cpp_type() != google::protobuf::FieldDescriptor::CPPTYPE_##type)                                                                                                                                                                                \
    {                                                                                                                                                                                                                                                          \
        return return_value;                                                                                                                                                                                                                                   \
    }

#define CHECK_FIELD_TYPE2_VOID(type1, type2)                                                                                                                                                                                                                   \
    google::protobuf::FieldDescriptor::CppType fieldType = field->cpp_type();                                                                                                                                                                                  \
    if (fieldType != google::protobuf::FieldDescriptor::CPPTYPE_##type1 && fieldType != google::protobuf::FieldDescriptor::CPPTYPE_##type2)                                                                                                                    \
    {                                                                                                                                                                                                                                                          \
        return;                                                                                                                                                                                                                                                \
    }

#define CHECK_FIELD_TYPE2(type1, type2, return_value)                                                                                                                                                                                                          \
    google::protobuf::FieldDescriptor::CppType fieldType = field->cpp_type();                                                                                                                                                                                  \
    if (fieldType != google::protobuf::FieldDescriptor::CPPTYPE_##type1 && fieldType != google::protobuf::FieldDescriptor::CPPTYPE_##type2)                                                                                                                    \
    {                                                                                                                                                                                                                                                          \
        return return_value;                                                                                                                                                                                                                                   \
    }

#define CHECK_FIELD_REPEATED_VOID()                                                                                                                                                                                                                            \
    if (field->label() != google::protobuf::FieldDescriptor::LABEL_REPEATED)                                                                                                                                                                                   \
    {                                                                                                                                                                                                                                                          \
        return;                                                                                                                                                                                                                                                \
    }

#define CHECK_FIELD_REPEATED(return_value)                                                                                                                                                                                                                     \
    if (field->label() != google::protobuf::FieldDescriptor::LABEL_REPEATED)                                                                                                                                                                                   \
    {                                                                                                                                                                                                                                                          \
        return return_value;                                                                                                                                                                                                                                   \
    }

#define CHECK_REPEATED_ELEMENT_VOID(idx)                                                                                                                                                                                                                       \
    int elemCount = msg->GetReflection()->FieldSize(*msg, field);                                                                                                                                                                                              \
    if (elemCount == 0 || idx >= elemCount || idx < 0)                                                                                                                                                                                                         \
    {                                                                                                                                                                                                                                                          \
        return;                                                                                                                                                                                                                                                \
    }

#define CHECK_REPEATED_ELEMENT(idx, return_value)                                                                                                                                                                                                              \
    int elemCount = msg->GetReflection()->FieldSize(*msg, field);                                                                                                                                                                                              \
    if (elemCount == 0 || idx >= elemCount || idx < 0)                                                                                                                                                                                                         \
    {                                                                                                                                                                                                                                                          \
        return return_value;                                                                                                                                                                                                                                   \
    }

extern INetworkMessages* networkMessages;

void* Bridge_NetMessages_AllocateNetMessageByID(int msgid)
{
    auto netmsg = networkMessages->FindNetworkMessageById(msgid);
    if (!netmsg)
    {
        return nullptr;
    }
    return netmsg->AllocateMessage()->ToPB<google::protobuf::Message>();
}

void* Bridge_NetMessages_AllocateNetMessageByPartialName(const char* name)
{
    auto netmsg = networkMessages->FindNetworkMessagePartial(name);
    if (!netmsg)
    {
        return nullptr;
    }
    return netmsg->AllocateMessage()->ToPB<google::protobuf::Message>();
}

void Bridge_NetMessages_DeallocateNetMessage(void* msg)
{
    if (!msg)
    {
        return;
    }
    delete (CNetMessagePB<google::protobuf::Message>*)msg;
}

bool Bridge_NetMessages_HasField(void* pmsg, const char* fieldName)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD(false);
    CHECK_FIELD_NOT_REPEATED(false);

    return msg->GetReflection()->HasField(*msg, field);
}

int Bridge_NetMessages_GetInt32(void* pmsg, const char* fieldName)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD(0);
    CHECK_FIELD_NOT_REPEATED(0);

    if (field->cpp_type() == google::protobuf::FieldDescriptor::CPPTYPE_ENUM)
    {
        return msg->GetReflection()->GetEnum(*msg, field)->number();
    }
    else
    {
        return msg->GetReflection()->GetInt32(*msg, field);
    }
}

int Bridge_NetMessages_GetRepeatedInt32(void* pmsg, const char* fieldName, int index)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD(0);
    CHECK_FIELD_REPEATED(0);
    CHECK_REPEATED_ELEMENT(index, 0);

    if (field->cpp_type() == google::protobuf::FieldDescriptor::CPPTYPE_ENUM)
    {
        return msg->GetReflection()->GetRepeatedEnum(*msg, field, index)->number();
    }
    else
    {
        return msg->GetReflection()->GetRepeatedInt32(*msg, field, index);
    }
}

void Bridge_NetMessages_SetInt32(void* pmsg, const char* fieldName, int value)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD_VOID();
    CHECK_FIELD_NOT_REPEATED_VOID();

    if (field->cpp_type() == google::protobuf::FieldDescriptor::CPPTYPE_ENUM)
    {
        const google::protobuf::EnumValueDescriptor* pEnumValue = field->enum_type()->FindValueByNumber(value);
        if (!pEnumValue)
        {
            return;
        }

        msg->GetReflection()->SetEnum(msg, field, pEnumValue);
    }
    else
    {
        msg->GetReflection()->SetInt32(msg, field, value);
    }
}

void Bridge_NetMessages_SetRepeatedInt32(void* pmsg, const char* fieldName, int index, int value)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD_VOID();
    CHECK_FIELD_REPEATED_VOID();
    CHECK_REPEATED_ELEMENT_VOID(index);

    if (field->cpp_type() == google::protobuf::FieldDescriptor::CPPTYPE_ENUM)
    {
        const google::protobuf::EnumValueDescriptor* pEnumValue = field->enum_type()->FindValueByNumber(value);
        if (!pEnumValue)
        {
            return;
        }

        msg->GetReflection()->SetRepeatedEnum(msg, field, index, pEnumValue);
    }
    else
    {
        msg->GetReflection()->SetRepeatedInt32(msg, field, index, value);
    }
}

void Bridge_NetMessages_AddInt32(void* pmsg, const char* fieldName, int value)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD_VOID();
    CHECK_FIELD_REPEATED_VOID();

    if (field->cpp_type() == google::protobuf::FieldDescriptor::CPPTYPE_ENUM)
    {
        const google::protobuf::EnumValueDescriptor* pEnumValue = field->enum_type()->FindValueByNumber(value);
        if (!pEnumValue)
        {
            return;
        }

        msg->GetReflection()->AddEnum(msg, field, pEnumValue);
    }
    else
    {
        msg->GetReflection()->AddInt32(msg, field, value);
    }
}

int64_t Bridge_NetMessages_GetInt64(void* pmsg, const char* fieldName)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD(0);
    CHECK_FIELD_NOT_REPEATED(0);
    return msg->GetReflection()->GetInt64(*msg, field);
}

int64_t Bridge_NetMessages_GetRepeatedInt64(void* pmsg, const char* fieldName, int index)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD(0);
    CHECK_FIELD_REPEATED(0);
    CHECK_REPEATED_ELEMENT(index, 0);
    return msg->GetReflection()->GetRepeatedInt64(*msg, field, index);
}

void Bridge_NetMessages_SetInt64(void* pmsg, const char* fieldName, int64_t value)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD_VOID();
    CHECK_FIELD_NOT_REPEATED_VOID();
    msg->GetReflection()->SetInt64(msg, field, value);
}

void Bridge_NetMessages_SetRepeatedInt64(void* pmsg, const char* fieldName, int index, int64_t value)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD_VOID();
    CHECK_FIELD_REPEATED_VOID();
    CHECK_REPEATED_ELEMENT_VOID(index);
    msg->GetReflection()->SetRepeatedInt64(msg, field, index, value);
}

void Bridge_NetMessages_AddInt64(void* pmsg, const char* fieldName, int64_t value)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD_VOID();
    CHECK_FIELD_REPEATED_VOID();
    msg->GetReflection()->AddInt64(msg, field, value);
}

uint32_t Bridge_NetMessages_GetUInt32(void* pmsg, const char* fieldName)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD(0);
    CHECK_FIELD_NOT_REPEATED(0);
    return msg->GetReflection()->GetUInt32(*msg, field);
}

uint32_t Bridge_NetMessages_GetRepeatedUInt32(void* pmsg, const char* fieldName, int index)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD(0);
    CHECK_FIELD_REPEATED(0);
    CHECK_REPEATED_ELEMENT(index, 0);
    return msg->GetReflection()->GetRepeatedUInt32(*msg, field, index);
}

void Bridge_NetMessages_SetUInt32(void* pmsg, const char* fieldName, uint32_t value)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD_VOID();
    CHECK_FIELD_NOT_REPEATED_VOID();
    msg->GetReflection()->SetUInt32(msg, field, value);
}

void Bridge_NetMessages_SetRepeatedUInt32(void* pmsg, const char* fieldName, int index, uint32_t value)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD_VOID();
    CHECK_FIELD_REPEATED_VOID();
    CHECK_REPEATED_ELEMENT_VOID(index);
    msg->GetReflection()->SetRepeatedUInt32(msg, field, index, value);
}

void Bridge_NetMessages_AddUInt32(void* pmsg, const char* fieldName, uint32_t value)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD_VOID();
    CHECK_FIELD_REPEATED_VOID();
    msg->GetReflection()->AddUInt32(msg, field, value);
}

uint64_t Bridge_NetMessages_GetUInt64(void* pmsg, const char* fieldName)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD(0);
    CHECK_FIELD_NOT_REPEATED(0);
    return msg->GetReflection()->GetUInt64(*msg, field);
}

uint64_t Bridge_NetMessages_GetRepeatedUInt64(void* pmsg, const char* fieldName, int index)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD(0);
    CHECK_FIELD_REPEATED(0);
    CHECK_REPEATED_ELEMENT(index, 0);
    return msg->GetReflection()->GetRepeatedUInt64(*msg, field, index);
}

void Bridge_NetMessages_SetUInt64(void* pmsg, const char* fieldName, uint64_t value)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD_VOID();
    CHECK_FIELD_NOT_REPEATED_VOID();
    msg->GetReflection()->SetUInt64(msg, field, value);
}

void Bridge_NetMessages_SetRepeatedUInt64(void* pmsg, const char* fieldName, int index, uint64_t value)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD_VOID();
    CHECK_FIELD_REPEATED_VOID();
    CHECK_REPEATED_ELEMENT_VOID(index);
    msg->GetReflection()->SetRepeatedUInt64(msg, field, index, value);
}

void Bridge_NetMessages_AddUInt64(void* pmsg, const char* fieldName, uint64_t value)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD_VOID();
    CHECK_FIELD_REPEATED_VOID();
    msg->GetReflection()->AddUInt64(msg, field, value);
}

bool Bridge_NetMessages_GetBool(void* pmsg, const char* fieldName)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD(false);
    CHECK_FIELD_NOT_REPEATED(false);
    return msg->GetReflection()->GetBool(*msg, field);
}

bool Bridge_NetMessages_GetRepeatedBool(void* pmsg, const char* fieldName, int index)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD(false);
    CHECK_FIELD_REPEATED(false);
    CHECK_REPEATED_ELEMENT(index, false);
    return msg->GetReflection()->GetRepeatedBool(*msg, field, index);
}

void Bridge_NetMessages_SetBool(void* pmsg, const char* fieldName, bool value)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD_VOID();
    CHECK_FIELD_NOT_REPEATED_VOID();
    msg->GetReflection()->SetBool(msg, field, value);
}

void Bridge_NetMessages_SetRepeatedBool(void* pmsg, const char* fieldName, int index, bool value)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD_VOID();
    CHECK_FIELD_REPEATED_VOID();
    CHECK_REPEATED_ELEMENT_VOID(index);
    msg->GetReflection()->SetRepeatedBool(msg, field, index, value);
}

void Bridge_NetMessages_AddBool(void* pmsg, const char* fieldName, bool value)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD_VOID();
    CHECK_FIELD_REPEATED_VOID();
    msg->GetReflection()->AddBool(msg, field, value);
}

float Bridge_NetMessages_GetFloat(void* pmsg, const char* fieldName)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD(0.0f);
    CHECK_FIELD_NOT_REPEATED(0.0f);
    return msg->GetReflection()->GetFloat(*msg, field);
}

float Bridge_NetMessages_GetRepeatedFloat(void* pmsg, const char* fieldName, int index)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD(0.0f);
    CHECK_FIELD_REPEATED(0.0f);
    CHECK_REPEATED_ELEMENT(index, 0.0f);
    return msg->GetReflection()->GetRepeatedFloat(*msg, field, index);
}

void Bridge_NetMessages_SetFloat(void* pmsg, const char* fieldName, float value)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD_VOID();
    CHECK_FIELD_NOT_REPEATED_VOID();
    msg->GetReflection()->SetFloat(msg, field, value);
}

void Bridge_NetMessages_SetRepeatedFloat(void* pmsg, const char* fieldName, int index, float value)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD_VOID();
    CHECK_FIELD_REPEATED_VOID();
    CHECK_REPEATED_ELEMENT_VOID(index);
    msg->GetReflection()->SetRepeatedFloat(msg, field, index, value);
}

void Bridge_NetMessages_AddFloat(void* pmsg, const char* fieldName, float value)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD_VOID();
    CHECK_FIELD_REPEATED_VOID();
    msg->GetReflection()->AddFloat(msg, field, value);
}

double Bridge_NetMessages_GetDouble(void* pmsg, const char* fieldName)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD(0.0);
    CHECK_FIELD_NOT_REPEATED(0.0);
    return msg->GetReflection()->GetDouble(*msg, field);
}

double Bridge_NetMessages_GetRepeatedDouble(void* pmsg, const char* fieldName, int index)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD(0.0);
    CHECK_FIELD_REPEATED(0.0);
    CHECK_REPEATED_ELEMENT(index, 0.0);
    return msg->GetReflection()->GetRepeatedDouble(*msg, field, index);
}

void Bridge_NetMessages_SetDouble(void* pmsg, const char* fieldName, double value)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD_VOID();
    CHECK_FIELD_NOT_REPEATED_VOID();
    msg->GetReflection()->SetDouble(msg, field, value);
}

void Bridge_NetMessages_SetRepeatedDouble(void* pmsg, const char* fieldName, int index, double value)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD_VOID();
    CHECK_FIELD_REPEATED_VOID();
    CHECK_REPEATED_ELEMENT_VOID(index);
    msg->GetReflection()->SetRepeatedDouble(msg, field, index, value);
}

void Bridge_NetMessages_AddDouble(void* pmsg, const char* fieldName, double value)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD_VOID();
    CHECK_FIELD_REPEATED_VOID();
    msg->GetReflection()->AddDouble(msg, field, value);
}

int Bridge_NetMessages_GetString(char* out, void* pmsg, const char* fieldName)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD(0);
    CHECK_FIELD_NOT_REPEATED(0);

    static std::string s;
    s = msg->GetReflection()->GetString(*msg, field);
    if (out != nullptr)
    {
        strcpy(out, s.c_str());
    }

    return s.size();
}

int Bridge_NetMessages_GetRepeatedString(char* out, void* pmsg, const char* fieldName, int index)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD(0);
    CHECK_FIELD_REPEATED(0);
    CHECK_REPEATED_ELEMENT(index, 0);

    static std::string s;
    s = msg->GetReflection()->GetRepeatedString(*msg, field, index);
    if (out != nullptr)
    {
        strcpy(out, s.c_str());
    }

    return s.size();
}

void Bridge_NetMessages_SetString(void* pmsg, const char* fieldName, const char* value)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD_VOID();
    CHECK_FIELD_NOT_REPEATED_VOID();
    msg->GetReflection()->SetString(msg, field, value);
}

void Bridge_NetMessages_SetRepeatedString(void* pmsg, const char* fieldName, int index, const char* value)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD_VOID();
    CHECK_FIELD_REPEATED_VOID();
    CHECK_REPEATED_ELEMENT_VOID(index);
    msg->GetReflection()->SetRepeatedString(msg, field, index, value);
}

void Bridge_NetMessages_AddString(void* pmsg, const char* fieldName, const char* value)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD_VOID();
    CHECK_FIELD_REPEATED_VOID();
    msg->GetReflection()->AddString(msg, field, value);
}

Vector2D Bridge_NetMessages_GetVector2D(void* pmsg, const char* fieldName)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    Vector2D vec{ 0.0f, 0.0f };
    GETCHECK_FIELD(vec);
    CHECK_FIELD_NOT_REPEATED(vec);

    const CMsgVector2D* msgVec2d = (const CMsgVector2D*)&msg->GetReflection()->GetMessage(*msg, field);
    vec.x = msgVec2d->x();
    vec.y = msgVec2d->y();
    return vec;
}

Vector2D Bridge_NetMessages_GetRepeatedVector2D(void* pmsg, const char* fieldName, int index)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;

    Vector2D vec{ 0.0f, 0.0f };
    GETCHECK_FIELD(vec);
    CHECK_FIELD_REPEATED(vec);
    CHECK_REPEATED_ELEMENT(index, vec);

    const CMsgVector2D* msgVec2d = (const CMsgVector2D*)&msg->GetReflection()->GetRepeatedMessage(*msg, field, index);
    vec.x = msgVec2d->x();
    vec.y = msgVec2d->y();
    return vec;
}

void Bridge_NetMessages_SetVector2D(void* pmsg, const char* fieldName, Vector2D value)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD_VOID();
    CHECK_FIELD_NOT_REPEATED_VOID();

    CMsgVector2D* msgVec2d = (CMsgVector2D*)msg->GetReflection()->MutableMessage(msg, field);
    msgVec2d->set_x(value.x);
    msgVec2d->set_y(value.y);
}

void Bridge_NetMessages_SetRepeatedVector2D(void* pmsg, const char* fieldName, int index, Vector2D value)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD_VOID();
    CHECK_FIELD_REPEATED_VOID();
    CHECK_REPEATED_ELEMENT_VOID(index);

    CMsgVector2D* msgVec2d = (CMsgVector2D*)msg->GetReflection()->MutableRepeatedMessage(msg, field, index);
    msgVec2d->set_x(value.x);
    msgVec2d->set_y(value.y);
}

void Bridge_NetMessages_AddVector2D(void* pmsg, const char* fieldName, Vector2D value)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD_VOID();
    CHECK_FIELD_REPEATED_VOID();

    CMsgVector2D* msgVec2d = (CMsgVector2D*)msg->GetReflection()->AddMessage(msg, field);
    msgVec2d->set_x(value.x);
    msgVec2d->set_y(value.y);
}

Vector Bridge_NetMessages_GetVector(void* pmsg, const char* fieldName)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    Vector vec{ 0.0f, 0.0f, 0.0f };
    GETCHECK_FIELD(vec);
    CHECK_FIELD_NOT_REPEATED(vec);

    const CMsgVector* msgVec = (const CMsgVector*)&msg->GetReflection()->GetMessage(*msg, field);
    vec.x = msgVec->x();
    vec.y = msgVec->y();
    vec.z = msgVec->z();
    return vec;
}

Vector Bridge_NetMessages_GetRepeatedVector(void* pmsg, const char* fieldName, int index)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;

    Vector vec{ 0.0f, 0.0f, 0.0f };
    GETCHECK_FIELD(vec);
    CHECK_FIELD_REPEATED(vec);
    CHECK_REPEATED_ELEMENT(index, vec);

    const CMsgVector* msgVec = (const CMsgVector*)&msg->GetReflection()->GetRepeatedMessage(*msg, field, index);
    vec.x = msgVec->x();
    vec.y = msgVec->y();
    vec.z = msgVec->z();
    return vec;
}

void Bridge_NetMessages_SetVector(void* pmsg, const char* fieldName, Vector value)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD_VOID();
    CHECK_FIELD_NOT_REPEATED_VOID();

    CMsgVector* msgVec = (CMsgVector*)msg->GetReflection()->MutableMessage(msg, field);
    msgVec->set_x(value.x);
    msgVec->set_y(value.y);
    msgVec->set_z(value.z);
}

void Bridge_NetMessages_SetRepeatedVector(void* pmsg, const char* fieldName, int index, Vector value)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD_VOID();
    CHECK_FIELD_REPEATED_VOID();
    CHECK_REPEATED_ELEMENT_VOID(index);

    CMsgVector* msgVec = (CMsgVector*)msg->GetReflection()->MutableRepeatedMessage(msg, field, index);
    msgVec->set_x(value.x);
    msgVec->set_y(value.y);
    msgVec->set_z(value.z);
}

void Bridge_NetMessages_AddVector(void* pmsg, const char* fieldName, Vector value)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD_VOID();
    CHECK_FIELD_REPEATED_VOID();

    CMsgVector* msgVec = (CMsgVector*)msg->GetReflection()->AddMessage(msg, field);
    msgVec->set_x(value.x);
    msgVec->set_y(value.y);
    msgVec->set_z(value.z);
}

Color Bridge_NetMessages_GetColor(void* pmsg, const char* fieldName)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    Color color{ 255, 255, 255, 255 };
    GETCHECK_FIELD(color);
    CHECK_FIELD_NOT_REPEATED(color);

    const CMsgRGBA* msgColor = (const CMsgRGBA*)&msg->GetReflection()->GetMessage(*msg, field);
    color.SetColor(msgColor->r(), msgColor->g(), msgColor->b(), msgColor->a());
    return color;
}

Color Bridge_NetMessages_GetRepeatedColor(void* pmsg, const char* fieldName, int index)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;

    Color color{ 255, 255, 255, 255 };
    GETCHECK_FIELD(color);
    CHECK_FIELD_REPEATED(color);
    CHECK_REPEATED_ELEMENT(index, color);

    const CMsgRGBA* msgColor = (const CMsgRGBA*)&msg->GetReflection()->GetRepeatedMessage(*msg, field, index);
    color.SetColor(msgColor->r(), msgColor->g(), msgColor->b(), msgColor->a());
    return color;
}

void Bridge_NetMessages_SetColor(void* pmsg, const char* fieldName, Color value)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD_VOID();
    CHECK_FIELD_NOT_REPEATED_VOID();

    CMsgRGBA* msgColor = (CMsgRGBA*)msg->GetReflection()->MutableMessage(msg, field);
    msgColor->set_r(value.r());
    msgColor->set_g(value.g());
    msgColor->set_b(value.b());
    msgColor->set_a(value.a());
}

void Bridge_NetMessages_SetRepeatedColor(void* pmsg, const char* fieldName, int index, Color value)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD_VOID();
    CHECK_FIELD_REPEATED_VOID();
    CHECK_REPEATED_ELEMENT_VOID(index);

    CMsgRGBA* msgColor = (CMsgRGBA*)msg->GetReflection()->MutableRepeatedMessage(msg, field, index);
    msgColor->set_r(value.r());
    msgColor->set_g(value.g());
    msgColor->set_b(value.b());
    msgColor->set_a(value.a());
}

void Bridge_NetMessages_AddColor(void* pmsg, const char* fieldName, Color value)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD_VOID();
    CHECK_FIELD_REPEATED_VOID();

    CMsgRGBA* msgColor = (CMsgRGBA*)msg->GetReflection()->AddMessage(msg, field);
    msgColor->set_r(value.r());
    msgColor->set_g(value.g());
    msgColor->set_b(value.b());
    msgColor->set_a(value.a());
}

QAngle Bridge_NetMessages_GetQAngle(void* pmsg, const char* fieldName)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    QAngle angle{ 0.0f, 0.0f, 0.0f };
    GETCHECK_FIELD(angle);
    CHECK_FIELD_NOT_REPEATED(angle);

    const CMsgQAngle* msgAngle = (const CMsgQAngle*)&msg->GetReflection()->GetMessage(*msg, field);
    angle.x = msgAngle->x();
    angle.y = msgAngle->y();
    angle.z = msgAngle->z();
    return angle;
}

QAngle Bridge_NetMessages_GetRepeatedQAngle(void* pmsg, const char* fieldName, int index)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;

    QAngle angle{ 0.0f, 0.0f, 0.0f };
    GETCHECK_FIELD(angle);
    CHECK_FIELD_REPEATED(angle);
    CHECK_REPEATED_ELEMENT(index, angle);

    const CMsgQAngle* msgAngle = (const CMsgQAngle*)&msg->GetReflection()->GetRepeatedMessage(*msg, field, index);
    angle.x = msgAngle->x();
    angle.y = msgAngle->y();
    angle.z = msgAngle->z();
    return angle;
}

void Bridge_NetMessages_SetQAngle(void* pmsg, const char* fieldName, QAngle value)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD_VOID();
    CHECK_FIELD_NOT_REPEATED_VOID();

    CMsgQAngle* msgAngle = (CMsgQAngle*)msg->GetReflection()->MutableMessage(msg, field);
    msgAngle->set_x(value.x);
    msgAngle->set_y(value.y);
    msgAngle->set_z(value.z);
}

void Bridge_NetMessages_SetRepeatedQAngle(void* pmsg, const char* fieldName, int index, QAngle value)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD_VOID();
    CHECK_FIELD_REPEATED_VOID();
    CHECK_REPEATED_ELEMENT_VOID(index);

    CMsgQAngle* msgAngle = (CMsgQAngle*)msg->GetReflection()->MutableRepeatedMessage(msg, field, index);
    msgAngle->set_x(value.x);
    msgAngle->set_y(value.y);
    msgAngle->set_z(value.z);
}

void Bridge_NetMessages_AddQAngle(void* pmsg, const char* fieldName, QAngle value)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD_VOID();
    CHECK_FIELD_REPEATED_VOID();

    CMsgQAngle* msgAngle = (CMsgQAngle*)msg->GetReflection()->AddMessage(msg, field);
    msgAngle->set_x(value.x);
    msgAngle->set_y(value.y);
    msgAngle->set_z(value.z);
}

int Bridge_NetMessages_GetBytes(uint8_t* out, void* pmsg, const char* fieldName)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD(0);
    CHECK_FIELD_NOT_REPEATED(0);

    static std::string s;
    s = msg->GetReflection()->GetString(*msg, field);
    if (out != nullptr)
    {
        std::memcpy(out, s.data(), s.size());
    }

    return s.size();
}

int Bridge_NetMessages_GetRepeatedBytes(uint8_t* out, void* pmsg, const char* fieldName, int index)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD(0);
    CHECK_FIELD_REPEATED(0);
    CHECK_REPEATED_ELEMENT(index, 0);

    static std::string s;
    s = msg->GetReflection()->GetRepeatedString(*msg, field, index);
    if (out != nullptr)
    {
        std::memcpy(out, s.data(), s.size());
    }

    return s.size();
}

void Bridge_NetMessages_SetBytes(void* pmsg, const char* fieldName, char* value, int valueLength)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD_VOID();
    CHECK_FIELD_NOT_REPEATED_VOID();

    std::string s(value, (size_t)valueLength);
    msg->GetReflection()->SetString(msg, field, s);
}

void Bridge_NetMessages_SetRepeatedBytes(void* pmsg, const char* fieldName, int index, char* value, int valueLength)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD_VOID();
    CHECK_FIELD_REPEATED_VOID();
    CHECK_REPEATED_ELEMENT_VOID(index);

    std::string s(value, (size_t)valueLength);
    msg->GetReflection()->SetRepeatedString(msg, field, index, s);
}

void Bridge_NetMessages_AddBytes(void* pmsg, const char* fieldName, char* value, int valueLength)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD_VOID();
    CHECK_FIELD_REPEATED_VOID();

    std::string s(value, (size_t)valueLength);
    msg->GetReflection()->AddString(msg, field, s);
}

void* Bridge_NetMessages_GetNestedMessage(void* pmsg, const char* fieldName)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD(nullptr);
    CHECK_FIELD_NOT_REPEATED(nullptr);
    return (void*)msg->GetReflection()->MutableMessage(msg, field);
}

void* Bridge_NetMessages_GetRepeatedNestedMessage(void* pmsg, const char* fieldName, int index)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;

    GETCHECK_FIELD(nullptr);
    CHECK_FIELD_REPEATED(nullptr);
    CHECK_REPEATED_ELEMENT(index, nullptr);

    return (void*)msg->GetReflection()->MutableRepeatedMessage(msg, field, index);
}

void* Bridge_NetMessages_AddNestedMessage(void* pmsg, const char* fieldName)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD(nullptr);
    CHECK_FIELD_REPEATED(nullptr);

    return (void*)msg->GetReflection()->AddMessage(msg, field);
}

int Bridge_NetMessages_GetRepeatedFieldSize(void* pmsg, const char* fieldName)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;

    GETCHECK_FIELD(0);
    return msg->GetReflection()->FieldSize(*msg, field);
}

void Bridge_NetMessages_ClearRepeatedField(void* pmsg, const char* fieldName)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;

    GETCHECK_FIELD_VOID();
    CHECK_FIELD_REPEATED_VOID();
    msg->GetReflection()->ClearField(msg, field);
}

void Bridge_NetMessages_Clear(void* pmsg)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    msg->Clear();
}

extern bool bypassPostEventAbstractHook;

void Bridge_NetMessages_SendMessage(void* pmsg, int msgid, int playerid)
{
    CNetMessagePB<google::protobuf::Message>* msg = (CNetMessagePB<google::protobuf::Message>*)pmsg;

    static auto gameEventSystem = g_ifaceService.FetchInterface<IGameEventSystem>(GAMEEVENTSYSTEM_INTERFACE_VERSION);

    auto netmsg = networkMessages->FindNetworkMessageById(msgid);
    if (!netmsg)
    {
        return;
    }

    bypassPostEventAbstractHook = true;

    CSingleRecipientFilter filter(playerid);
    gameEventSystem->PostEventAbstract(-1, false, &filter, netmsg, msg, 0);

    bypassPostEventAbstractHook = false;
}

void Bridge_NetMessages_SendMessageToPlayers(void* pmsg, int msgid, uint64_t playermask)
{
    CNetMessagePB<google::protobuf::Message>* msg = (CNetMessagePB<google::protobuf::Message>*)pmsg;
    static auto gameEventSystem = g_ifaceService.FetchInterface<IGameEventSystem>(GAMEEVENTSYSTEM_INTERFACE_VERSION);

    auto netmsg = networkMessages->FindNetworkMessageById(msgid);
    if (!netmsg)
    {
        return;
    }

    bypassPostEventAbstractHook = true;

    CRecipientFilter filter;
    auto& recipients = filter.GetRecipients();

    // because recipients are only 64, we can cast the base array pointer from being base[0] and base[1],
    // each having 4 bytes, to a single base with 8 bytes
    *(uint64_t*)(recipients.Base()) = playermask;

    gameEventSystem->PostEventAbstract(-1, false, &filter, netmsg, msg, 0);

    bypassPostEventAbstractHook = false;
}

uint64_t Bridge_NetMessages_AddNetMessageServerHook(void* callback_ptr)
{
    auto netmessages = g_ifaceService.FetchInterface<INetMessages>(NETMESSAGES_INTERFACE_VERSION);

    return netmessages->AddServerMessageSendCallback([callback_ptr](uint64_t* clients, int messageid, void* msg) { return ((int (*)(uint64_t*, int, void*))callback_ptr)(clients, messageid, msg); });
}

void Bridge_NetMessages_RemoveNetMessageServerHook(uint64_t callbackID)
{
    auto netmessages = g_ifaceService.FetchInterface<INetMessages>(NETMESSAGES_INTERFACE_VERSION);
    netmessages->RemoveServerMessageSendCallback(callbackID);
}

uint64_t Bridge_NetMessages_AddNetMessageClientHook(void* callback_ptr)
{
    auto netmessages = g_ifaceService.FetchInterface<INetMessages>(NETMESSAGES_INTERFACE_VERSION);

    return netmessages->AddClientMessageSendCallback([callback_ptr](int playerid, int messageid, void* msg) { return ((int (*)(int, int, void*))callback_ptr)(playerid, messageid, msg); });
}

void Bridge_NetMessages_RemoveNetMessageClientHook(uint64_t callbackID)
{
    auto netmessages = g_ifaceService.FetchInterface<INetMessages>(NETMESSAGES_INTERFACE_VERSION);
    netmessages->RemoveClientMessageSendCallback(callbackID);
}

uint64_t Bridge_NetMessages_AddNetMessageServerHookInternal(void* callback_ptr)
{
    auto netmessages = g_ifaceService.FetchInterface<INetMessages>(NETMESSAGES_INTERFACE_VERSION);

    return netmessages->AddServerMessageInternalSendCallback([callback_ptr](int playerid, int messageid, void* msg) { return ((int (*)(int, int, void*))callback_ptr)(playerid, messageid, msg); });
}

void Bridge_NetMessages_RemoveNetMessageServerHookInternal(uint64_t callbackID)
{
    auto netmessages = g_ifaceService.FetchInterface<INetMessages>(NETMESSAGES_INTERFACE_VERSION);
    netmessages->RemoveServerMessageInternalSendCallback(callbackID);
}

DEFINE_NATIVE("NetMessages.AllocateNetMessageByID", Bridge_NetMessages_AllocateNetMessageByID);
DEFINE_NATIVE("NetMessages.AllocateNetMessageByPartialName", Bridge_NetMessages_AllocateNetMessageByPartialName);
DEFINE_NATIVE("NetMessages.DeallocateNetMessage", Bridge_NetMessages_DeallocateNetMessage);
DEFINE_NATIVE("NetMessages.HasField", Bridge_NetMessages_HasField);
DEFINE_NATIVE("NetMessages.GetInt32", Bridge_NetMessages_GetInt32);
DEFINE_NATIVE("NetMessages.GetRepeatedInt32", Bridge_NetMessages_GetRepeatedInt32);
DEFINE_NATIVE("NetMessages.SetInt32", Bridge_NetMessages_SetInt32);
DEFINE_NATIVE("NetMessages.SetRepeatedInt32", Bridge_NetMessages_SetRepeatedInt32);
DEFINE_NATIVE("NetMessages.AddInt32", Bridge_NetMessages_AddInt32);
DEFINE_NATIVE("NetMessages.GetInt64", Bridge_NetMessages_GetInt64);
DEFINE_NATIVE("NetMessages.GetRepeatedInt64", Bridge_NetMessages_GetRepeatedInt64);
DEFINE_NATIVE("NetMessages.SetInt64", Bridge_NetMessages_SetInt64);
DEFINE_NATIVE("NetMessages.SetRepeatedInt64", Bridge_NetMessages_SetRepeatedInt64);
DEFINE_NATIVE("NetMessages.AddInt64", Bridge_NetMessages_AddInt64);
DEFINE_NATIVE("NetMessages.GetUInt32", Bridge_NetMessages_GetUInt32);
DEFINE_NATIVE("NetMessages.GetRepeatedUInt32", Bridge_NetMessages_GetRepeatedUInt32);
DEFINE_NATIVE("NetMessages.SetUInt32", Bridge_NetMessages_SetUInt32);
DEFINE_NATIVE("NetMessages.SetRepeatedUInt32", Bridge_NetMessages_SetRepeatedUInt32);
DEFINE_NATIVE("NetMessages.AddUInt32", Bridge_NetMessages_AddUInt32);
DEFINE_NATIVE("NetMessages.GetUInt64", Bridge_NetMessages_GetUInt64);
DEFINE_NATIVE("NetMessages.GetRepeatedUInt64", Bridge_NetMessages_GetRepeatedUInt64);
DEFINE_NATIVE("NetMessages.SetUInt64", Bridge_NetMessages_SetUInt64);
DEFINE_NATIVE("NetMessages.SetRepeatedUInt64", Bridge_NetMessages_SetRepeatedUInt64);
DEFINE_NATIVE("NetMessages.AddUInt64", Bridge_NetMessages_AddUInt64);
DEFINE_NATIVE("NetMessages.GetBool", Bridge_NetMessages_GetBool);
DEFINE_NATIVE("NetMessages.GetRepeatedBool", Bridge_NetMessages_GetRepeatedBool);
DEFINE_NATIVE("NetMessages.SetBool", Bridge_NetMessages_SetBool);
DEFINE_NATIVE("NetMessages.SetRepeatedBool", Bridge_NetMessages_SetRepeatedBool);
DEFINE_NATIVE("NetMessages.AddBool", Bridge_NetMessages_AddBool);
DEFINE_NATIVE("NetMessages.GetFloat", Bridge_NetMessages_GetFloat);
DEFINE_NATIVE("NetMessages.GetRepeatedFloat", Bridge_NetMessages_GetRepeatedFloat);
DEFINE_NATIVE("NetMessages.SetFloat", Bridge_NetMessages_SetFloat);
DEFINE_NATIVE("NetMessages.SetRepeatedFloat", Bridge_NetMessages_SetRepeatedFloat);
DEFINE_NATIVE("NetMessages.AddFloat", Bridge_NetMessages_AddFloat);
DEFINE_NATIVE("NetMessages.GetDouble", Bridge_NetMessages_GetDouble);
DEFINE_NATIVE("NetMessages.GetRepeatedDouble", Bridge_NetMessages_GetRepeatedDouble);
DEFINE_NATIVE("NetMessages.SetDouble", Bridge_NetMessages_SetDouble);
DEFINE_NATIVE("NetMessages.SetRepeatedDouble", Bridge_NetMessages_SetRepeatedDouble);
DEFINE_NATIVE("NetMessages.AddDouble", Bridge_NetMessages_AddDouble);
DEFINE_NATIVE("NetMessages.GetString", Bridge_NetMessages_GetString);
DEFINE_NATIVE("NetMessages.GetRepeatedString", Bridge_NetMessages_GetRepeatedString);
DEFINE_NATIVE("NetMessages.SetString", Bridge_NetMessages_SetString);
DEFINE_NATIVE("NetMessages.SetRepeatedString", Bridge_NetMessages_SetRepeatedString);
DEFINE_NATIVE("NetMessages.AddString", Bridge_NetMessages_AddString);
DEFINE_NATIVE("NetMessages.GetVector2D", Bridge_NetMessages_GetVector2D);
DEFINE_NATIVE("NetMessages.GetRepeatedVector2D", Bridge_NetMessages_GetRepeatedVector2D);
DEFINE_NATIVE("NetMessages.SetVector2D", Bridge_NetMessages_SetVector2D);
DEFINE_NATIVE("NetMessages.SetRepeatedVector2D", Bridge_NetMessages_SetRepeatedVector2D);
DEFINE_NATIVE("NetMessages.AddVector2D", Bridge_NetMessages_AddVector2D);
DEFINE_NATIVE("NetMessages.GetVector", Bridge_NetMessages_GetVector);
DEFINE_NATIVE("NetMessages.GetRepeatedVector", Bridge_NetMessages_GetRepeatedVector);
DEFINE_NATIVE("NetMessages.SetVector", Bridge_NetMessages_SetVector);
DEFINE_NATIVE("NetMessages.SetRepeatedVector", Bridge_NetMessages_SetRepeatedVector);
DEFINE_NATIVE("NetMessages.AddVector", Bridge_NetMessages_AddVector);
DEFINE_NATIVE("NetMessages.GetColor", Bridge_NetMessages_GetColor);
DEFINE_NATIVE("NetMessages.GetRepeatedColor", Bridge_NetMessages_GetRepeatedColor);
DEFINE_NATIVE("NetMessages.SetColor", Bridge_NetMessages_SetColor);
DEFINE_NATIVE("NetMessages.SetRepeatedColor", Bridge_NetMessages_SetRepeatedColor);
DEFINE_NATIVE("NetMessages.AddColor", Bridge_NetMessages_AddColor);
DEFINE_NATIVE("NetMessages.GetQAngle", Bridge_NetMessages_GetQAngle);
DEFINE_NATIVE("NetMessages.GetRepeatedQAngle", Bridge_NetMessages_GetRepeatedQAngle);
DEFINE_NATIVE("NetMessages.SetQAngle", Bridge_NetMessages_SetQAngle);
DEFINE_NATIVE("NetMessages.SetRepeatedQAngle", Bridge_NetMessages_SetRepeatedQAngle);
DEFINE_NATIVE("NetMessages.AddQAngle", Bridge_NetMessages_AddQAngle);
DEFINE_NATIVE("NetMessages.GetBytes", Bridge_NetMessages_GetBytes);
DEFINE_NATIVE("NetMessages.GetRepeatedBytes", Bridge_NetMessages_GetRepeatedBytes);
DEFINE_NATIVE("NetMessages.SetBytes", Bridge_NetMessages_SetBytes);
DEFINE_NATIVE("NetMessages.SetRepeatedBytes", Bridge_NetMessages_SetRepeatedBytes);
DEFINE_NATIVE("NetMessages.AddBytes", Bridge_NetMessages_AddBytes);
DEFINE_NATIVE("NetMessages.GetNestedMessage", Bridge_NetMessages_GetNestedMessage);
DEFINE_NATIVE("NetMessages.GetRepeatedNestedMessage", Bridge_NetMessages_GetRepeatedNestedMessage);
DEFINE_NATIVE("NetMessages.AddNestedMessage", Bridge_NetMessages_AddNestedMessage);
DEFINE_NATIVE("NetMessages.GetRepeatedFieldSize", Bridge_NetMessages_GetRepeatedFieldSize);
DEFINE_NATIVE("NetMessages.ClearRepeatedField", Bridge_NetMessages_ClearRepeatedField);
DEFINE_NATIVE("NetMessages.Clear", Bridge_NetMessages_Clear);
DEFINE_NATIVE("NetMessages.SendMessage", Bridge_NetMessages_SendMessage);
DEFINE_NATIVE("NetMessages.SendMessageToPlayers", Bridge_NetMessages_SendMessageToPlayers);
DEFINE_NATIVE("NetMessages.AddNetMessageServerHook", Bridge_NetMessages_AddNetMessageServerHook);
DEFINE_NATIVE("NetMessages.RemoveNetMessageServerHook", Bridge_NetMessages_RemoveNetMessageServerHook);
DEFINE_NATIVE("NetMessages.AddNetMessageClientHook", Bridge_NetMessages_AddNetMessageClientHook);
DEFINE_NATIVE("NetMessages.RemoveNetMessageClientHook", Bridge_NetMessages_RemoveNetMessageClientHook);
DEFINE_NATIVE("NetMessages.AddNetMessageServerHookInternal", Bridge_NetMessages_AddNetMessageServerHookInternal);
DEFINE_NATIVE("NetMessages.RemoveNetMessageServerHookInternal", Bridge_NetMessages_RemoveNetMessageServerHookInternal);