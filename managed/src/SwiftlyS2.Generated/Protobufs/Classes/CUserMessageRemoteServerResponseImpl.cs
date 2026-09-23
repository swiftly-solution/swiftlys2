using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMessageRemoteServerResponseImpl : NetMessage<CUserMessageRemoteServerResponse>, CUserMessageRemoteServerResponse
{
    public CUserMessageRemoteServerResponseImpl(nint handle, bool isManuallyAllocated) : base(handle, isManuallyAllocated)
    {
    }

    public CUserMessageRemoteServerResponse_ECommandResult CommandResult
    { get => (CUserMessageRemoteServerResponse_ECommandResult)Accessor.GetInt32("command_result"); set => Accessor.SetInt32("command_result", (int)value); }
    public string Convar
    { get => Accessor.GetString("convar"); set => Accessor.SetString("convar", value); }
    public string Results
    { get => Accessor.GetString("results"); set => Accessor.SetString("results", value); }
}