using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class PerFriendPreferencesImpl : TypedProtobuf<PerFriendPreferences>, PerFriendPreferences
{
    public PerFriendPreferencesImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint Accountid
    { get => Accessor.GetUInt32("accountid"); set => Accessor.SetUInt32("accountid", value); }
    public string Nickname
    { get => Accessor.GetString("nickname"); set => Accessor.SetString("nickname", value); }
    public ENotificationSetting NotificationsShowingame
    { get => (ENotificationSetting)Accessor.GetInt32("notifications_showingame"); set => Accessor.SetInt32("notifications_showingame", (int)value); }
    public ENotificationSetting NotificationsShowonline
    { get => (ENotificationSetting)Accessor.GetInt32("notifications_showonline"); set => Accessor.SetInt32("notifications_showonline", (int)value); }
    public ENotificationSetting NotificationsShowmessages
    { get => (ENotificationSetting)Accessor.GetInt32("notifications_showmessages"); set => Accessor.SetInt32("notifications_showmessages", (int)value); }
    public ENotificationSetting SoundsShowingame
    { get => (ENotificationSetting)Accessor.GetInt32("sounds_showingame"); set => Accessor.SetInt32("sounds_showingame", (int)value); }
    public ENotificationSetting SoundsShowonline
    { get => (ENotificationSetting)Accessor.GetInt32("sounds_showonline"); set => Accessor.SetInt32("sounds_showonline", (int)value); }
    public ENotificationSetting SoundsShowmessages
    { get => (ENotificationSetting)Accessor.GetInt32("sounds_showmessages"); set => Accessor.SetInt32("sounds_showmessages", (int)value); }
    public ENotificationSetting NotificationsSendmobile
    { get => (ENotificationSetting)Accessor.GetInt32("notifications_sendmobile"); set => Accessor.SetInt32("notifications_sendmobile", (int)value); }
}