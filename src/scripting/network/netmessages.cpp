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

#include <api/interfaces/interfaces.h>
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

static char* Bridge_NetMessages_CopyString(const std::string& value, int* size)
{
    int outSize = static_cast<int>(value.size());
    *size = outSize;

    char* out = (char*)g_pMemoryAllocator->Alloc(outSize + 1);
    g_pMemoryAllocator->Copy(out, (void*)value.c_str(), outSize);
    out[outSize] = '\0';
    return out;
}

void* Bridge_NetMessages_AllocateNetMessageByID(int msgid)
{
    auto netmsg = g_pGameNetworkMessages->FindNetworkMessageById(msgid);
    if (!netmsg)
    {
        return nullptr;
    }
    return netmsg->AllocateMessage()->ToPB<google::protobuf::Message>();
}

void* Bridge_NetMessages_AllocateNetMessageByPartialName(const char* name)
{
    auto netmsg = g_pGameNetworkMessages->FindNetworkMessagePartial(name);
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

enum ProtobufValueKind
{
    ProtobufValueKind_Int32,
    ProtobufValueKind_Int64,
    ProtobufValueKind_UInt32,
    ProtobufValueKind_UInt64,
    ProtobufValueKind_Bool,
    ProtobufValueKind_Float,
    ProtobufValueKind_Double,
    ProtobufValueKind_Vector2D,
    ProtobufValueKind_Vector,
    ProtobufValueKind_Color,
    ProtobufValueKind_QAngle
};

using google::protobuf::FieldDescriptor;

static const FieldDescriptor* FindField(google::protobuf::Message* msg, const char* fieldName, int index, bool adding = false)
{
    if (!msg)
        return nullptr;

    const FieldDescriptor* field = msg->GetDescriptor()->FindFieldByName(fieldName);
    if (!field)
        return nullptr;

    bool repeated = field->label() == FieldDescriptor::LABEL_REPEATED;
    if (adding)
        return repeated ? field : nullptr;

    if (index < 0)
        return repeated ? nullptr : field;

    if (!repeated || index >= msg->GetReflection()->FieldSize(*msg, field))
        return nullptr;

    return field;
}

static bool KindMatchesField(int kind, const FieldDescriptor* field)
{
    FieldDescriptor::CppType type = field->cpp_type();
    switch (kind)
    {
        case ProtobufValueKind_Int32: return type == FieldDescriptor::CPPTYPE_INT32 || type == FieldDescriptor::CPPTYPE_ENUM;
        case ProtobufValueKind_Int64: return type == FieldDescriptor::CPPTYPE_INT64;
        case ProtobufValueKind_UInt32: return type == FieldDescriptor::CPPTYPE_UINT32;
        case ProtobufValueKind_UInt64: return type == FieldDescriptor::CPPTYPE_UINT64;
        case ProtobufValueKind_Bool: return type == FieldDescriptor::CPPTYPE_BOOL;
        case ProtobufValueKind_Float: return type == FieldDescriptor::CPPTYPE_FLOAT;
        case ProtobufValueKind_Double: return type == FieldDescriptor::CPPTYPE_DOUBLE;
        case ProtobufValueKind_Vector2D: return type == FieldDescriptor::CPPTYPE_MESSAGE && field->message_type()->name() == "CMsgVector2D";
        case ProtobufValueKind_Vector: return type == FieldDescriptor::CPPTYPE_MESSAGE && field->message_type()->name() == "CMsgVector";
        case ProtobufValueKind_Color: return type == FieldDescriptor::CPPTYPE_MESSAGE && field->message_type()->name() == "CMsgRGBA";
        case ProtobufValueKind_QAngle: return type == FieldDescriptor::CPPTYPE_MESSAGE && field->message_type()->name() == "CMsgQAngle";
        default: return false;
    }
}

static void WriteDefaultValue(int kind, void* out)
{
    switch (kind)
    {
        case ProtobufValueKind_Int32: *(int32_t*)out = 0; break;
        case ProtobufValueKind_Int64: *(int64_t*)out = 0; break;
        case ProtobufValueKind_UInt32: *(uint32_t*)out = 0; break;
        case ProtobufValueKind_UInt64: *(uint64_t*)out = 0; break;
        case ProtobufValueKind_Bool: *(bool*)out = false; break;
        case ProtobufValueKind_Float: *(float*)out = 0.0f; break;
        case ProtobufValueKind_Double: *(double*)out = 0.0; break;
        case ProtobufValueKind_Vector2D: *(Vector2D*)out = Vector2D{ 0.0f, 0.0f }; break;
        case ProtobufValueKind_Vector: *(Vector*)out = Vector{ 0.0f, 0.0f, 0.0f }; break;
        case ProtobufValueKind_Color: *(Color*)out = Color{ 255, 255, 255, 255 }; break;
        case ProtobufValueKind_QAngle: *(QAngle*)out = QAngle{ 0.0f, 0.0f, 0.0f }; break;
    }
}

static void ReadMessageValue(int kind, const google::protobuf::Message& message, void* out)
{
    switch (kind)
    {
        case ProtobufValueKind_Vector2D:
        {
            auto& msgVec2d = (const CMsgVector2D&)message;
            *(Vector2D*)out = Vector2D{ msgVec2d.x(), msgVec2d.y() };
            break;
        }
        case ProtobufValueKind_Vector:
        {
            auto& msgVec = (const CMsgVector&)message;
            *(Vector*)out = Vector{ msgVec.x(), msgVec.y(), msgVec.z() };
            break;
        }
        case ProtobufValueKind_Color:
        {
            auto& msgColor = (const CMsgRGBA&)message;
            ((Color*)out)->SetColor(msgColor.r(), msgColor.g(), msgColor.b(), msgColor.a());
            break;
        }
        case ProtobufValueKind_QAngle:
        {
            auto& msgAngle = (const CMsgQAngle&)message;
            *(QAngle*)out = QAngle{ msgAngle.x(), msgAngle.y(), msgAngle.z() };
            break;
        }
    }
}

static void WriteMessageValue(int kind, google::protobuf::Message* message, const void* value)
{
    switch (kind)
    {
        case ProtobufValueKind_Vector2D:
        {
            auto* msgVec2d = (CMsgVector2D*)message;
            msgVec2d->set_x(((const Vector2D*)value)->x);
            msgVec2d->set_y(((const Vector2D*)value)->y);
            break;
        }
        case ProtobufValueKind_Vector:
        {
            auto* msgVec = (CMsgVector*)message;
            msgVec->set_x(((const Vector*)value)->x);
            msgVec->set_y(((const Vector*)value)->y);
            msgVec->set_z(((const Vector*)value)->z);
            break;
        }
        case ProtobufValueKind_Color:
        {
            auto* msgColor = (CMsgRGBA*)message;
            msgColor->set_r(((const Color*)value)->r());
            msgColor->set_g(((const Color*)value)->g());
            msgColor->set_b(((const Color*)value)->b());
            msgColor->set_a(((const Color*)value)->a());
            break;
        }
        case ProtobufValueKind_QAngle:
        {
            auto* msgAngle = (CMsgQAngle*)message;
            msgAngle->set_x(((const QAngle*)value)->x);
            msgAngle->set_y(((const QAngle*)value)->y);
            msgAngle->set_z(((const QAngle*)value)->z);
            break;
        }
    }
}

static bool IsMessageKind(int kind)
{
    return kind == ProtobufValueKind_Vector2D || kind == ProtobufValueKind_Vector || kind == ProtobufValueKind_Color || kind == ProtobufValueKind_QAngle;
}

bool Bridge_NetMessages_HasField(void* pmsg, const char* fieldName)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    GETCHECK_FIELD(false);
    CHECK_FIELD_NOT_REPEATED(false);

    return msg->GetReflection()->HasField(*msg, field);
}

bool Bridge_NetMessages_GetValue(void* pmsg, const char* fieldName, int index, int kind, void* out)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;
    WriteDefaultValue(kind, out);

    const FieldDescriptor* field = FindField(msg, fieldName, index);
    if (!field || !KindMatchesField(kind, field))
        return false;

    const google::protobuf::Reflection* reflection = msg->GetReflection();
    bool repeated = index >= 0;

#define GET_VALUE(type, Name) *(type*)out = repeated ? reflection->GetRepeated##Name(*msg, field, index) : reflection->Get##Name(*msg, field)

    switch (kind)
    {
        case ProtobufValueKind_Int32:
            if (field->cpp_type() == FieldDescriptor::CPPTYPE_ENUM)
                *(int32_t*)out = (repeated ? reflection->GetRepeatedEnum(*msg, field, index) : reflection->GetEnum(*msg, field))->number();
            else
                GET_VALUE(int32_t, Int32);
            break;
        case ProtobufValueKind_Int64: GET_VALUE(int64_t, Int64); break;
        case ProtobufValueKind_UInt32: GET_VALUE(uint32_t, UInt32); break;
        case ProtobufValueKind_UInt64: GET_VALUE(uint64_t, UInt64); break;
        case ProtobufValueKind_Bool: GET_VALUE(bool, Bool); break;
        case ProtobufValueKind_Float: GET_VALUE(float, Float); break;
        case ProtobufValueKind_Double: GET_VALUE(double, Double); break;
        default:
            ReadMessageValue(kind, repeated ? reflection->GetRepeatedMessage(*msg, field, index) : reflection->GetMessage(*msg, field), out);
            break;
    }

#undef GET_VALUE

    return true;
}

bool Bridge_NetMessages_SetValue(void* pmsg, const char* fieldName, int index, int kind, const void* value)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;

    const FieldDescriptor* field = FindField(msg, fieldName, index);
    if (!field || !KindMatchesField(kind, field))
        return false;

    const google::protobuf::Reflection* reflection = msg->GetReflection();
    bool repeated = index >= 0;

#define SET_VALUE(type, Name) \
    if (repeated) reflection->SetRepeated##Name(msg, field, index, *(const type*)value); \
    else reflection->Set##Name(msg, field, *(const type*)value)

    switch (kind)
    {
        case ProtobufValueKind_Int32:
            if (field->cpp_type() == FieldDescriptor::CPPTYPE_ENUM)
            {
                const google::protobuf::EnumValueDescriptor* enumValue = field->enum_type()->FindValueByNumber(*(const int32_t*)value);
                if (!enumValue)
                    return false;

                if (repeated) reflection->SetRepeatedEnum(msg, field, index, enumValue);
                else reflection->SetEnum(msg, field, enumValue);
            }
            else
            {
                SET_VALUE(int32_t, Int32);
            }
            break;
        case ProtobufValueKind_Int64: SET_VALUE(int64_t, Int64); break;
        case ProtobufValueKind_UInt32: SET_VALUE(uint32_t, UInt32); break;
        case ProtobufValueKind_UInt64: SET_VALUE(uint64_t, UInt64); break;
        case ProtobufValueKind_Bool: SET_VALUE(bool, Bool); break;
        case ProtobufValueKind_Float: SET_VALUE(float, Float); break;
        case ProtobufValueKind_Double: SET_VALUE(double, Double); break;
        default:
            WriteMessageValue(kind, repeated ? reflection->MutableRepeatedMessage(msg, field, index) : reflection->MutableMessage(msg, field), value);
            break;
    }

#undef SET_VALUE

    return true;
}

bool Bridge_NetMessages_AddValue(void* pmsg, const char* fieldName, int kind, const void* value)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;

    const FieldDescriptor* field = FindField(msg, fieldName, -1, true);
    if (!field || !KindMatchesField(kind, field))
        return false;

    const google::protobuf::Reflection* reflection = msg->GetReflection();

#define ADD_VALUE(type, Name) reflection->Add##Name(msg, field, *(const type*)value)

    switch (kind)
    {
        case ProtobufValueKind_Int32:
            if (field->cpp_type() == FieldDescriptor::CPPTYPE_ENUM)
            {
                const google::protobuf::EnumValueDescriptor* enumValue = field->enum_type()->FindValueByNumber(*(const int32_t*)value);
                if (!enumValue)
                    return false;

                reflection->AddEnum(msg, field, enumValue);
            }
            else
            {
                ADD_VALUE(int32_t, Int32);
            }
            break;
        case ProtobufValueKind_Int64: ADD_VALUE(int64_t, Int64); break;
        case ProtobufValueKind_UInt32: ADD_VALUE(uint32_t, UInt32); break;
        case ProtobufValueKind_UInt64: ADD_VALUE(uint64_t, UInt64); break;
        case ProtobufValueKind_Bool: ADD_VALUE(bool, Bool); break;
        case ProtobufValueKind_Float: ADD_VALUE(float, Float); break;
        case ProtobufValueKind_Double: ADD_VALUE(double, Double); break;
        default:
            WriteMessageValue(kind, reflection->AddMessage(msg, field), value);
            break;
    }

#undef ADD_VALUE

    return true;
}

char* Bridge_NetMessages_GetString(int* size, void* pmsg, const char* fieldName, int index)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;

    const FieldDescriptor* field = FindField(msg, fieldName, index);
    if (!field || field->cpp_type() != FieldDescriptor::CPPTYPE_STRING)
        return Bridge_NetMessages_CopyString("", size);

    const google::protobuf::Reflection* reflection = msg->GetReflection();
    std::string s = index >= 0 ? reflection->GetRepeatedString(*msg, field, index) : reflection->GetString(*msg, field);
    return Bridge_NetMessages_CopyString(s, size);
}

void Bridge_NetMessages_SetString(void* pmsg, const char* fieldName, int index, const char* value)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;

    const FieldDescriptor* field = FindField(msg, fieldName, index);
    if (!field || field->cpp_type() != FieldDescriptor::CPPTYPE_STRING)
        return;

    if (index >= 0)
        msg->GetReflection()->SetRepeatedString(msg, field, index, value);
    else
        msg->GetReflection()->SetString(msg, field, value);
}

void Bridge_NetMessages_AddString(void* pmsg, const char* fieldName, const char* value)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;

    const FieldDescriptor* field = FindField(msg, fieldName, -1, true);
    if (!field || field->cpp_type() != FieldDescriptor::CPPTYPE_STRING)
        return;

    msg->GetReflection()->AddString(msg, field, value);
}

int Bridge_NetMessages_GetBytes(uint8_t* out, void* pmsg, const char* fieldName, int index)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;

    const FieldDescriptor* field = FindField(msg, fieldName, index);
    if (!field || field->cpp_type() != FieldDescriptor::CPPTYPE_STRING)
        return 0;

    const google::protobuf::Reflection* reflection = msg->GetReflection();
    std::string s = index >= 0 ? reflection->GetRepeatedString(*msg, field, index) : reflection->GetString(*msg, field);
    if (out != nullptr)
    {
        std::memcpy(out, s.data(), s.size());
    }
    return s.size();
}

void Bridge_NetMessages_SetBytes(void* pmsg, const char* fieldName, int index, char* value, int valueLength)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;

    const FieldDescriptor* field = FindField(msg, fieldName, index);
    if (!field || field->cpp_type() != FieldDescriptor::CPPTYPE_STRING)
        return;

    std::string s(value, (size_t)valueLength);
    if (index >= 0)
        msg->GetReflection()->SetRepeatedString(msg, field, index, s);
    else
        msg->GetReflection()->SetString(msg, field, s);
}

void Bridge_NetMessages_AddBytes(void* pmsg, const char* fieldName, char* value, int valueLength)
{
    google::protobuf::Message* msg = (google::protobuf::Message*)pmsg;

    const FieldDescriptor* field = FindField(msg, fieldName, -1, true);
    if (!field || field->cpp_type() != FieldDescriptor::CPPTYPE_STRING)
        return;

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

    auto netmsg = g_pGameNetworkMessages->FindNetworkMessageById(msgid);
    if (!netmsg)
    {
        return;
    }

    bypassPostEventAbstractHook = true;

    CSingleRecipientFilter filter(playerid);
    g_pGameEventSystem->PostEventAbstract(-1, false, &filter, netmsg, msg, 0);

    bypassPostEventAbstractHook = false;
}

void Bridge_NetMessages_SendMessageToPlayers(void* pmsg, int msgid, uint64_t playermask)
{
    CNetMessagePB<google::protobuf::Message>* msg = (CNetMessagePB<google::protobuf::Message>*)pmsg;
    auto netmsg = g_pGameNetworkMessages->FindNetworkMessageById(msgid);
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

    g_pGameEventSystem->PostEventAbstract(-1, false, &filter, netmsg, msg, 0);

    bypassPostEventAbstractHook = false;
}

void Bridge_NetMessages_RegisterMessageHook(int hookType, int messageid)
{
    g_pNetMessages->RegisterMessageHook((NetMessageHookType)hookType, messageid);
}

void Bridge_NetMessages_UnregisterMessageHook(int hookType, int messageid)
{
    g_pNetMessages->UnregisterMessageHook((NetMessageHookType)hookType, messageid);
}

void Bridge_NetMessages_SetNetMessageServerHook(void* callback_ptr)
{
    g_pNetMessages->SetServerMessageSendHandler([callback_ptr](uint64_t* clients, int messageid, void* msg) {
        return ((int (*)(uint64_t*, int, void*))callback_ptr)(clients, messageid, msg);
        });
}

void Bridge_NetMessages_SetNetMessageClientHook(void* callback_ptr)
{
    g_pNetMessages->SetClientMessageSendHandler([callback_ptr](int playerid, int messageid, void* msg) {
        return ((int (*)(int, int, void*))callback_ptr)(playerid, messageid, msg);
        });
}

void Bridge_NetMessages_SetNetMessageServerHookInternal(void* callback_ptr)
{
    g_pNetMessages->SetServerMessageInternalSendHandler([callback_ptr](int playerid, int messageid, void* msg) {
        return ((int (*)(int, int, void*))callback_ptr)(playerid, messageid, msg);
        });
}

DEFINE_NATIVE("NetMessages.AllocateNetMessageByID", Bridge_NetMessages_AllocateNetMessageByID);
DEFINE_NATIVE("NetMessages.AllocateNetMessageByPartialName", Bridge_NetMessages_AllocateNetMessageByPartialName);
DEFINE_NATIVE("NetMessages.DeallocateNetMessage", Bridge_NetMessages_DeallocateNetMessage);
DEFINE_NATIVE("NetMessages.HasField", Bridge_NetMessages_HasField);
DEFINE_NATIVE("NetMessages.GetValue", Bridge_NetMessages_GetValue);
DEFINE_NATIVE("NetMessages.SetValue", Bridge_NetMessages_SetValue);
DEFINE_NATIVE("NetMessages.AddValue", Bridge_NetMessages_AddValue);
DEFINE_NATIVE("NetMessages.GetString", Bridge_NetMessages_GetString);
DEFINE_NATIVE("NetMessages.SetString", Bridge_NetMessages_SetString);
DEFINE_NATIVE("NetMessages.AddString", Bridge_NetMessages_AddString);
DEFINE_NATIVE("NetMessages.GetBytes", Bridge_NetMessages_GetBytes);
DEFINE_NATIVE("NetMessages.SetBytes", Bridge_NetMessages_SetBytes);
DEFINE_NATIVE("NetMessages.AddBytes", Bridge_NetMessages_AddBytes);
DEFINE_NATIVE("NetMessages.GetNestedMessage", Bridge_NetMessages_GetNestedMessage);
DEFINE_NATIVE("NetMessages.GetRepeatedNestedMessage", Bridge_NetMessages_GetRepeatedNestedMessage);
DEFINE_NATIVE("NetMessages.AddNestedMessage", Bridge_NetMessages_AddNestedMessage);
DEFINE_NATIVE("NetMessages.GetRepeatedFieldSize", Bridge_NetMessages_GetRepeatedFieldSize);
DEFINE_NATIVE("NetMessages.ClearRepeatedField", Bridge_NetMessages_ClearRepeatedField);
DEFINE_NATIVE("NetMessages.Clear", Bridge_NetMessages_Clear);
DEFINE_NATIVE("NetMessages.SendMessage", Bridge_NetMessages_SendMessage);
DEFINE_NATIVE("NetMessages.SendMessageToPlayers", Bridge_NetMessages_SendMessageToPlayers);
DEFINE_NATIVE("NetMessages.RegisterMessageHook", Bridge_NetMessages_RegisterMessageHook);
DEFINE_NATIVE("NetMessages.UnregisterMessageHook", Bridge_NetMessages_UnregisterMessageHook);
DEFINE_NATIVE("NetMessages.SetNetMessageServerHook", Bridge_NetMessages_SetNetMessageServerHook);
DEFINE_NATIVE("NetMessages.SetNetMessageClientHook", Bridge_NetMessages_SetNetMessageClientHook);
DEFINE_NATIVE("NetMessages.SetNetMessageServerHookInternal", Bridge_NetMessages_SetNetMessageServerHookInternal);