using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CPlayer_IgnoreFriend_RequestImpl : TypedProtobuf<CPlayer_IgnoreFriend_Request>, CPlayer_IgnoreFriend_Request
{
    public CPlayer_IgnoreFriend_RequestImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public ulong Steamid
    { get => Accessor.GetUInt64("steamid"); set => Accessor.SetUInt64("steamid", value); }
    public bool Unignore
    { get => Accessor.GetBool("unignore"); set => Accessor.SetBool("unignore", value); }
}