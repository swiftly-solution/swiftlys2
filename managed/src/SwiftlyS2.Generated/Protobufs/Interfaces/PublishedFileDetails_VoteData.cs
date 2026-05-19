using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface PublishedFileDetails_VoteData : ITypedProtobuf<PublishedFileDetails_VoteData>
{
    static PublishedFileDetails_VoteData ITypedProtobuf<PublishedFileDetails_VoteData>.Wrap(nint handle, bool isManuallyAllocated) => new PublishedFileDetails_VoteDataImpl(handle, isManuallyAllocated);

    public float Score { get; set; }
    public uint VotesUp { get; set; }
    public uint VotesDown { get; set; }
}