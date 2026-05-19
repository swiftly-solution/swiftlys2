using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CPublishedFile_Update_RequestImpl : TypedProtobuf<CPublishedFile_Update_Request>, CPublishedFile_Update_Request
{
    public CPublishedFile_Update_RequestImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint Appid
    { get => Accessor.GetUInt32("appid"); set => Accessor.SetUInt32("appid", value); }
    public ulong Publishedfileid
    { get => Accessor.GetUInt64("publishedfileid"); set => Accessor.SetUInt64("publishedfileid", value); }
    public string Title
    { get => Accessor.GetString("title"); set => Accessor.SetString("title", value); }
    public string FileDescription
    { get => Accessor.GetString("file_description"); set => Accessor.SetString("file_description", value); }
    public uint Visibility
    { get => Accessor.GetUInt32("visibility"); set => Accessor.SetUInt32("visibility", value); }
    public IProtobufRepeatedFieldValueType<string> Tags
    { get => new ProtobufRepeatedFieldValueType<string>(Accessor, "tags"); }
    public string Filename
    { get => Accessor.GetString("filename"); set => Accessor.SetString("filename", value); }
    public string PreviewFilename
    { get => Accessor.GetString("preview_filename"); set => Accessor.SetString("preview_filename", value); }
}