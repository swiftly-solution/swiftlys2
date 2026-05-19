using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPublishedFile_RefreshVotingQueue_Request : ITypedProtobuf<CPublishedFile_RefreshVotingQueue_Request>
{
    static CPublishedFile_RefreshVotingQueue_Request ITypedProtobuf<CPublishedFile_RefreshVotingQueue_Request>.Wrap(nint handle, bool isManuallyAllocated) => new CPublishedFile_RefreshVotingQueue_RequestImpl(handle, isManuallyAllocated);

    public uint Appid { get; set; }
    public uint MatchingFileType { get; set; }
    public IProtobufRepeatedFieldValueType<string> Tags { get; }
    public bool MatchAllTags { get; set; }
    public IProtobufRepeatedFieldValueType<string> ExcludedTags { get; }
    public uint DesiredQueueSize { get; set; }
}