using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMessage_RemoteServerCommandImpl : NetMessage<CUserMessage_RemoteServerCommand>, CUserMessage_RemoteServerCommand
{
    public CUserMessage_RemoteServerCommandImpl(nint handle, bool isManuallyAllocated) : base(handle, isManuallyAllocated)
    {
    }

    public CUserMessage_RemoteServerCommand_ECommand Command
    { get => (CUserMessage_RemoteServerCommand_ECommand)Accessor.GetInt32("command"); set => Accessor.SetInt32("command", (int)value); }
    public string Convar
    { get => Accessor.GetString("convar"); set => Accessor.SetString("convar", value); }
    public string Value
    { get => Accessor.GetString("value"); set => Accessor.SetString("value", value); }
}