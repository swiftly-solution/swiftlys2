using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCommunity_ClanAnnouncementInfoImpl : TypedProtobuf<CCommunity_ClanAnnouncementInfo>, CCommunity_ClanAnnouncementInfo
{
    public CCommunity_ClanAnnouncementInfoImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public ulong Gid
    { get => Accessor.GetUInt64("gid"); set => Accessor.SetUInt64("gid", value); }
    public ulong Clanid
    { get => Accessor.GetUInt64("clanid"); set => Accessor.SetUInt64("clanid", value); }
    public ulong Posterid
    { get => Accessor.GetUInt64("posterid"); set => Accessor.SetUInt64("posterid", value); }
    public string Headline
    { get => Accessor.GetString("headline"); set => Accessor.SetString("headline", value); }
    public uint Posttime
    { get => Accessor.GetUInt32("posttime"); set => Accessor.SetUInt32("posttime", value); }
    public uint Updatetime
    { get => Accessor.GetUInt32("updatetime"); set => Accessor.SetUInt32("updatetime", value); }
    public string Body
    { get => Accessor.GetString("body"); set => Accessor.SetString("body", value); }
    public int Commentcount
    { get => Accessor.GetInt32("commentcount"); set => Accessor.SetInt32("commentcount", value); }
    public IProtobufRepeatedFieldValueType<string> Tags
    { get => new ProtobufRepeatedFieldValueType<string>(Accessor, "tags"); }
    public int Language
    { get => Accessor.GetInt32("language"); set => Accessor.SetInt32("language", value); }
    public bool Hidden
    { get => Accessor.GetBool("hidden"); set => Accessor.SetBool("hidden", value); }
    public ulong ForumTopicId
    { get => Accessor.GetUInt64("forum_topic_id"); set => Accessor.SetUInt64("forum_topic_id", value); }
    public ulong EventGid
    { get => Accessor.GetUInt64("event_gid"); set => Accessor.SetUInt64("event_gid", value); }
    public int Voteupcount
    { get => Accessor.GetInt32("voteupcount"); set => Accessor.SetInt32("voteupcount", value); }
    public int Votedowncount
    { get => Accessor.GetInt32("votedowncount"); set => Accessor.SetInt32("votedowncount", value); }
    public EBanContentCheckResult BanCheckResult
    { get => (EBanContentCheckResult)Accessor.GetInt32("ban_check_result"); set => Accessor.SetInt32("ban_check_result", (int)value); }
}