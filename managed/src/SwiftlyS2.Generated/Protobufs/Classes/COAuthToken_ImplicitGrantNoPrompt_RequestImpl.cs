using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class COAuthToken_ImplicitGrantNoPrompt_RequestImpl : TypedProtobuf<COAuthToken_ImplicitGrantNoPrompt_Request>, COAuthToken_ImplicitGrantNoPrompt_Request
{
    public COAuthToken_ImplicitGrantNoPrompt_RequestImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public string Clientid
    { get => Accessor.GetString("clientid"); set => Accessor.SetString("clientid", value); }
}