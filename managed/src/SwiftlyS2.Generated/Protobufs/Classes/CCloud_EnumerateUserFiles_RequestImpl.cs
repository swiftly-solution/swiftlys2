using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCloud_EnumerateUserFiles_RequestImpl : TypedProtobuf<CCloud_EnumerateUserFiles_Request>, CCloud_EnumerateUserFiles_Request
{
    public CCloud_EnumerateUserFiles_RequestImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint Appid
    { get => Accessor.GetUInt32("appid"); set => Accessor.SetUInt32("appid", value); }
    public bool ExtendedDetails
    { get => Accessor.GetBool("extended_details"); set => Accessor.SetBool("extended_details", value); }
    public uint Count
    { get => Accessor.GetUInt32("count"); set => Accessor.SetUInt32("count", value); }
    public uint StartIndex
    { get => Accessor.GetUInt32("start_index"); set => Accessor.SetUInt32("start_index", value); }
}