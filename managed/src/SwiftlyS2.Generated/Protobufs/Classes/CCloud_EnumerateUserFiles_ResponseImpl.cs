using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCloud_EnumerateUserFiles_ResponseImpl : TypedProtobuf<CCloud_EnumerateUserFiles_Response>, CCloud_EnumerateUserFiles_Response
{
    public CCloud_EnumerateUserFiles_ResponseImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public IProtobufRepeatedFieldSubMessageType<CCloud_UserFile> Files
    { get => new ProtobufRepeatedFieldSubMessageType<CCloud_UserFile>(Accessor, "files"); }
    public uint TotalFiles
    { get => Accessor.GetUInt32("total_files"); set => Accessor.SetUInt32("total_files", value); }
}