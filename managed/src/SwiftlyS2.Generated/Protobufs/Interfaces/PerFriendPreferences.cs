using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface PerFriendPreferences : ITypedProtobuf<PerFriendPreferences>
{
    static PerFriendPreferences ITypedProtobuf<PerFriendPreferences>.Wrap(nint handle, bool isManuallyAllocated) => new PerFriendPreferencesImpl(handle, isManuallyAllocated);

    public uint Accountid { get; set; }
    public string Nickname { get; set; }
    public ENotificationSetting NotificationsShowingame { get; set; }
    public ENotificationSetting NotificationsShowonline { get; set; }
    public ENotificationSetting NotificationsShowmessages { get; set; }
    public ENotificationSetting SoundsShowingame { get; set; }
    public ENotificationSetting SoundsShowonline { get; set; }
    public ENotificationSetting SoundsShowmessages { get; set; }
    public ENotificationSetting NotificationsSendmobile { get; set; }
}