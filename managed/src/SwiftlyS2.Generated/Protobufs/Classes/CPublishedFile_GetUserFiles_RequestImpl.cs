using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CPublishedFile_GetUserFiles_RequestImpl : TypedProtobuf<CPublishedFile_GetUserFiles_Request>, CPublishedFile_GetUserFiles_Request
{
    public CPublishedFile_GetUserFiles_RequestImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint Appid
    { get => Accessor.GetUInt32("appid"); set => Accessor.SetUInt32("appid", value); }
    public uint Page
    { get => Accessor.GetUInt32("page"); set => Accessor.SetUInt32("page", value); }
    public uint Numperpage
    { get => Accessor.GetUInt32("numperpage"); set => Accessor.SetUInt32("numperpage", value); }
    public string Sortmethod
    { get => Accessor.GetString("sortmethod"); set => Accessor.SetString("sortmethod", value); }
    public bool Totalonly
    { get => Accessor.GetBool("totalonly"); set => Accessor.SetBool("totalonly", value); }
    public uint Privacy
    { get => Accessor.GetUInt32("privacy"); set => Accessor.SetUInt32("privacy", value); }
    public bool IdsOnly
    { get => Accessor.GetBool("ids_only"); set => Accessor.SetBool("ids_only", value); }
    public IProtobufRepeatedFieldValueType<string> Requiredtags
    { get => new ProtobufRepeatedFieldValueType<string>(Accessor, "requiredtags"); }
    public IProtobufRepeatedFieldValueType<string> Excludedtags
    { get => new ProtobufRepeatedFieldValueType<string>(Accessor, "excludedtags"); }
}