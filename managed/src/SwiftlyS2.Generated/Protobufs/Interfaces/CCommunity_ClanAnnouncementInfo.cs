using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CCommunity_ClanAnnouncementInfo : ITypedProtobuf<CCommunity_ClanAnnouncementInfo>
{
    static CCommunity_ClanAnnouncementInfo ITypedProtobuf<CCommunity_ClanAnnouncementInfo>.Wrap(nint handle, bool isManuallyAllocated) => new CCommunity_ClanAnnouncementInfoImpl(handle, isManuallyAllocated);

    public ulong Gid { get; set; }
    public ulong Clanid { get; set; }
    public ulong Posterid { get; set; }
    public string Headline { get; set; }
    public uint Posttime { get; set; }
    public uint Updatetime { get; set; }
    public string Body { get; set; }
    public int Commentcount { get; set; }
    public IProtobufRepeatedFieldValueType<string> Tags { get; }
    public int Language { get; set; }
    public bool Hidden { get; set; }
    public ulong ForumTopicId { get; set; }
    public ulong EventGid { get; set; }
    public int Voteupcount { get; set; }
    public int Votedowncount { get; set; }
    public EBanContentCheckResult BanCheckResult { get; set; }
}