using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramSetSecondaryAddressResultImpl : TypedProtobuf<CMsgSteamDatagramSetSecondaryAddressResult>, CMsgSteamDatagramSetSecondaryAddressResult
{
    public CMsgSteamDatagramSetSecondaryAddressResultImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public bool Success
    { get => Accessor.GetBool("success"); set => Accessor.SetBool("success", value); }
    public string Message
    { get => Accessor.GetString("message"); set => Accessor.SetString("message", value); }
}