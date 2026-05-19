using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CPublishedFile_RefreshVotingQueue_RequestImpl : TypedProtobuf<CPublishedFile_RefreshVotingQueue_Request>, CPublishedFile_RefreshVotingQueue_Request
{
    public CPublishedFile_RefreshVotingQueue_RequestImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint Appid
    { get => Accessor.GetUInt32("appid"); set => Accessor.SetUInt32("appid", value); }
    public uint MatchingFileType
    { get => Accessor.GetUInt32("matching_file_type"); set => Accessor.SetUInt32("matching_file_type", value); }
    public IProtobufRepeatedFieldValueType<string> Tags
    { get => new ProtobufRepeatedFieldValueType<string>(Accessor, "tags"); }
    public bool MatchAllTags
    { get => Accessor.GetBool("match_all_tags"); set => Accessor.SetBool("match_all_tags", value); }
    public IProtobufRepeatedFieldValueType<string> ExcludedTags
    { get => new ProtobufRepeatedFieldValueType<string>(Accessor, "excluded_tags"); }
    public uint DesiredQueueSize
    { get => Accessor.GetUInt32("desired_queue_size"); set => Accessor.SetUInt32("desired_queue_size", value); }
}