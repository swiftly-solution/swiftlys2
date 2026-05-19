using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCloud_Delete_RequestImpl : TypedProtobuf<CCloud_Delete_Request>, CCloud_Delete_Request
{
    public CCloud_Delete_RequestImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public string Filename
    { get => Accessor.GetString("filename"); set => Accessor.SetString("filename", value); }
    public uint Appid
    { get => Accessor.GetUInt32("appid"); set => Accessor.SetUInt32("appid", value); }
}