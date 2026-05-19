using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramSessionCryptInfoSignedImpl : TypedProtobuf<CMsgSteamDatagramSessionCryptInfoSigned>, CMsgSteamDatagramSessionCryptInfoSigned
{
    public CMsgSteamDatagramSessionCryptInfoSignedImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public byte[] Info
    { get => Accessor.GetBytes("info"); set => Accessor.SetBytes("info", value); }
    public byte[] Signature
    { get => Accessor.GetBytes("signature"); set => Accessor.SetBytes("signature", value); }
}