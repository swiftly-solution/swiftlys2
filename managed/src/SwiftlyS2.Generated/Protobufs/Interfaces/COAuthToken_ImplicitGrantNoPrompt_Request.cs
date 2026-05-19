using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface COAuthToken_ImplicitGrantNoPrompt_Request : ITypedProtobuf<COAuthToken_ImplicitGrantNoPrompt_Request>
{
    static COAuthToken_ImplicitGrantNoPrompt_Request ITypedProtobuf<COAuthToken_ImplicitGrantNoPrompt_Request>.Wrap(nint handle, bool isManuallyAllocated) => new COAuthToken_ImplicitGrantNoPrompt_RequestImpl(handle, isManuallyAllocated);

    public string Clientid { get; set; }
}