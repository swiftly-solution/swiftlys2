using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CClanEventData : ITypedProtobuf<CClanEventData>
{
    static CClanEventData ITypedProtobuf<CClanEventData>.Wrap(nint handle, bool isManuallyAllocated) => new CClanEventDataImpl(handle, isManuallyAllocated);

    public ulong Gid { get; set; }
    public ulong ClanSteamid { get; set; }
    public string EventName { get; set; }
    public EProtoClanEventType EventType { get; set; }
    public uint Appid { get; set; }
    public string ServerAddress { get; set; }
    public string ServerPassword { get; set; }
    public uint Rtime32StartTime { get; set; }
    public uint Rtime32EndTime { get; set; }
    public int CommentCount { get; set; }
    public ulong CreatorSteamid { get; set; }
    public ulong LastUpdateSteamid { get; set; }
    public string EventNotes { get; set; }
    public string Jsondata { get; set; }
    public CCommunity_ClanAnnouncementInfo AnnouncementBody { get; }
    public bool Published { get; set; }
    public bool Hidden { get; set; }
    public uint Rtime32VisibilityStart { get; set; }
    public uint Rtime32VisibilityEnd { get; set; }
    public uint BroadcasterAccountid { get; set; }
    public uint FollowerCount { get; set; }
    public uint IgnoreCount { get; set; }
    public ulong ForumTopicId { get; set; }
    public uint Rtime32LastModified { get; set; }
    public ulong NewsPostGid { get; set; }
    public uint RtimeModReviewed { get; set; }
    public uint FeaturedAppTagid { get; set; }
    public IProtobufRepeatedFieldValueType<uint> ReferencedAppids { get; }
    public uint BuildId { get; set; }
    public string BuildBranch { get; set; }
}