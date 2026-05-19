using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCloud_GetFileDetails_RequestImpl : TypedProtobuf<CCloud_GetFileDetails_Request>, CCloud_GetFileDetails_Request
{
    public CCloud_GetFileDetails_RequestImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public ulong Ugcid
    { get => Accessor.GetUInt64("ugcid"); set => Accessor.SetUInt64("ugcid", value); }
    public uint Appid
    { get => Accessor.GetUInt32("appid"); set => Accessor.SetUInt32("appid", value); }
}