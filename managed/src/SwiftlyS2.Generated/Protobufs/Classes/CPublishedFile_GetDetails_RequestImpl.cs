using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CPublishedFile_GetDetails_RequestImpl : TypedProtobuf<CPublishedFile_GetDetails_Request>, CPublishedFile_GetDetails_Request
{
    public CPublishedFile_GetDetails_RequestImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public IProtobufRepeatedFieldValueType<ulong> Publishedfileids
    { get => new ProtobufRepeatedFieldValueType<ulong>(Accessor, "publishedfileids"); }
    public bool Includetags
    { get => Accessor.GetBool("includetags"); set => Accessor.SetBool("includetags", value); }
    public bool Includeadditionalpreviews
    { get => Accessor.GetBool("includeadditionalpreviews"); set => Accessor.SetBool("includeadditionalpreviews", value); }
    public bool Includechildren
    { get => Accessor.GetBool("includechildren"); set => Accessor.SetBool("includechildren", value); }
    public bool Includekvtags
    { get => Accessor.GetBool("includekvtags"); set => Accessor.SetBool("includekvtags", value); }
    public bool Includevotes
    { get => Accessor.GetBool("includevotes"); set => Accessor.SetBool("includevotes", value); }
    public bool ShortDescription
    { get => Accessor.GetBool("short_description"); set => Accessor.SetBool("short_description", value); }
}