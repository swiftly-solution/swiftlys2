using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class PublishedFileDetails_VoteDataImpl : TypedProtobuf<PublishedFileDetails_VoteData>, PublishedFileDetails_VoteData
{
    public PublishedFileDetails_VoteDataImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public float Score
    { get => Accessor.GetFloat("score"); set => Accessor.SetFloat("score", value); }
    public uint VotesUp
    { get => Accessor.GetUInt32("votes_up"); set => Accessor.SetUInt32("votes_up", value); }
    public uint VotesDown
    { get => Accessor.GetUInt32("votes_down"); set => Accessor.SetUInt32("votes_down", value); }
}