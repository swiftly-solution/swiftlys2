using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CPublishedFile_GetUserFiles_Response_AppImpl : TypedProtobuf<CPublishedFile_GetUserFiles_Response_App>, CPublishedFile_GetUserFiles_Response_App
{
    public CPublishedFile_GetUserFiles_Response_AppImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint Appid
    { get => Accessor.GetUInt32("appid"); set => Accessor.SetUInt32("appid", value); }
    public string Name
    { get => Accessor.GetString("name"); set => Accessor.SetString("name", value); }
    public uint Shortcutid
    { get => Accessor.GetUInt32("shortcutid"); set => Accessor.SetUInt32("shortcutid", value); }
    public bool Private
    { get => Accessor.GetBool("private"); set => Accessor.SetBool("private", value); }
}