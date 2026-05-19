using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface COAuthToken_ImplicitGrantNoPrompt_Response : ITypedProtobuf<COAuthToken_ImplicitGrantNoPrompt_Response>
{
    static COAuthToken_ImplicitGrantNoPrompt_Response ITypedProtobuf<COAuthToken_ImplicitGrantNoPrompt_Response>.Wrap(nint handle, bool isManuallyAllocated) => new COAuthToken_ImplicitGrantNoPrompt_ResponseImpl(handle, isManuallyAllocated);

    public string AccessToken { get; set; }
    public string RedirectUri { get; set; }
}