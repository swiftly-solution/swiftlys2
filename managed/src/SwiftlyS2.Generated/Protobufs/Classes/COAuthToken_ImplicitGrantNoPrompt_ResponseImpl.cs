using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class COAuthToken_ImplicitGrantNoPrompt_ResponseImpl : TypedProtobuf<COAuthToken_ImplicitGrantNoPrompt_Response>, COAuthToken_ImplicitGrantNoPrompt_Response
{
    public COAuthToken_ImplicitGrantNoPrompt_ResponseImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public string AccessToken
    { get => Accessor.GetString("access_token"); set => Accessor.SetString("access_token", value); }
    public string RedirectUri
    { get => Accessor.GetString("redirect_uri"); set => Accessor.SetString("redirect_uri", value); }
}