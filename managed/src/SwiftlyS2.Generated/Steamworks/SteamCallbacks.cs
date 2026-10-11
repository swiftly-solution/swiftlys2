using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace SwiftlyS2.Shared.SteamAPI;

/// <summary>
/// <para>callbacks</para>
/// <para>posted after the user gains ownership of DLC &amp; that DLC is installed</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamAppsCallbacks + 5)]
public struct DlcInstalled_t
{
	public const int k_iCallback = Constants.k_iSteamAppsCallbacks + 5;
	/// <summary>
	/// <para>AppID of the DLC</para>
	/// </summary>
	public AppId_t m_nAppID;
}

/// <summary>
/// <para>posted after the user gains executes a Steam URL with command line or query parameters</para>
/// <para>such as steam://run/&lt;appid&gt;//-commandline/?param1=value1&amp;param2=value2&amp;param3=value3 etc</para>
/// <para>while the game is already running.  The new params can be queried</para>
/// <para>with GetLaunchQueryParam and GetLaunchCommandLine</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value, Size = 1)]
[CallbackIdentity(Constants.k_iSteamAppsCallbacks + 14)]
public struct NewUrlLaunchParameters_t
{
	public const int k_iCallback = Constants.k_iSteamAppsCallbacks + 14;
}

/// <summary>
/// <para>response to RequestAppProofOfPurchaseKey/RequestAllProofOfPurchaseKeys</para>
/// <para>for supporting third-party CD keys, or other proof-of-purchase systems.</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamAppsCallbacks + 21)]
public unsafe struct AppProofOfPurchaseKeyResponse_t
{
	public const int k_iCallback = Constants.k_iSteamAppsCallbacks + 21;
	public EResult m_eResult;
	public uint m_nAppID;
	public uint m_cchKeyLength;
	private fixed byte m_rgchKey_[Constants.k_cubAppProofOfPurchaseKeyMax];
	public string m_rgchKey
	{
		get
		{
			fixed (byte* ptr = m_rgchKey_)
			{
				var span = new ReadOnlySpan<byte>(ptr, Constants.k_cubAppProofOfPurchaseKeyMax);
				var length = span.IndexOf((byte)0);
				return Encoding.UTF8.GetString(length < 0 ? span : span[..length]);
			}
		}
		set
		{
			fixed (byte* ptr = m_rgchKey_)
			{
				var span = new Span<byte>(ptr, Constants.k_cubAppProofOfPurchaseKeyMax);
				span.Clear();
				var bytes = Encoding.UTF8.GetBytes(value ?? string.Empty);
				bytes.AsSpan(0, Math.Min(bytes.Length, Constants.k_cubAppProofOfPurchaseKeyMax - 1)).CopyTo(span);
			}
		}
	}
}

/// <summary>
/// <para>response to GetFileDetails</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamAppsCallbacks + 23)]
public unsafe struct FileDetailsResult_t
{
	public const int k_iCallback = Constants.k_iSteamAppsCallbacks + 23;
	public EResult m_eResult;
	/// <summary>
	/// <para>original file size in bytes</para>
	/// </summary>
	public ulong m_ulFileSize;
	/// <summary>
	/// <para>original file SHA1 hash</para>
	/// </summary>
	public fixed byte m_FileSHA[20];
	public uint m_unFlags;
}

/// <summary>
/// <para>called for games in Timed Trial mode</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamAppsCallbacks + 30)]
public struct TimedTrialStatus_t
{
	public const int k_iCallback = Constants.k_iSteamAppsCallbacks + 30;
	/// <summary>
	/// <para>appID</para>
	/// </summary>
	public AppId_t m_unAppID;
	/// <summary>
	/// <para>if true, time allowed / played refers to offline time, not total time</para>
	/// </summary>
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bIsOffline;
	/// <summary>
	/// <para>how many seconds the app can be played in total</para>
	/// </summary>
	public uint m_unSecondsAllowed;
	/// <summary>
	/// <para>how many seconds the app was already played</para>
	/// </summary>
	public uint m_unSecondsPlayed;
}

/// <summary>
/// <para>callbacks</para>
/// <para>called when a friends&apos; status changes</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamFriendsCallbacks + 4)]
public struct PersonaStateChange_t
{
	public const int k_iCallback = Constants.k_iSteamFriendsCallbacks + 4;
	/// <summary>
	/// <para>steamID of the friend who changed</para>
	/// </summary>
	public ulong m_ulSteamID;
	/// <summary>
	/// <para>what&apos;s changed</para>
	/// </summary>
	public EPersonaChange m_nChangeFlags;
}

/// <summary>
/// <para>posted when game overlay activates or deactivates</para>
/// <para>the game can use this to be pause or resume single player games</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamFriendsCallbacks + 31)]
public struct GameOverlayActivated_t
{
	public const int k_iCallback = Constants.k_iSteamFriendsCallbacks + 31;
	/// <summary>
	/// <para>true if it&apos;s just been activated, false otherwise</para>
	/// </summary>
	public byte m_bActive;
	/// <summary>
	/// <para>true if the user asked for the overlay to be activated/deactivated</para>
	/// </summary>
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bUserInitiated;
	/// <summary>
	/// <para>the appID of the game (should always be the current game)</para>
	/// </summary>
	public AppId_t m_nAppID;
	/// <summary>
	/// <para>used internally</para>
	/// </summary>
	public uint m_dwOverlayPID;
}

/// <summary>
/// <para>called when the user tries to join a different game server from their friends list</para>
/// <para>game client should attempt to connect to specified server when this is received</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamFriendsCallbacks + 32)]
public unsafe struct GameServerChangeRequested_t
{
	public const int k_iCallback = Constants.k_iSteamFriendsCallbacks + 32;
	/// <summary>
	/// <para>server address (&quot;127.0.0.1:27015&quot;, &quot;tf2.valvesoftware.com&quot;)</para>
	/// </summary>
	private fixed byte m_rgchServer_[64];
	public string m_rgchServer
	{
		get
		{
			fixed (byte* ptr = m_rgchServer_)
			{
				var span = new ReadOnlySpan<byte>(ptr, 64);
				var length = span.IndexOf((byte)0);
				return Encoding.UTF8.GetString(length < 0 ? span : span[..length]);
			}
		}
		set
		{
			fixed (byte* ptr = m_rgchServer_)
			{
				var span = new Span<byte>(ptr, 64);
				span.Clear();
				var bytes = Encoding.UTF8.GetBytes(value ?? string.Empty);
				bytes.AsSpan(0, Math.Min(bytes.Length, 64 - 1)).CopyTo(span);
			}
		}
	}
	/// <summary>
	/// <para>server password, if any</para>
	/// </summary>
	private fixed byte m_rgchPassword_[64];
	public string m_rgchPassword
	{
		get
		{
			fixed (byte* ptr = m_rgchPassword_)
			{
				var span = new ReadOnlySpan<byte>(ptr, 64);
				var length = span.IndexOf((byte)0);
				return Encoding.UTF8.GetString(length < 0 ? span : span[..length]);
			}
		}
		set
		{
			fixed (byte* ptr = m_rgchPassword_)
			{
				var span = new Span<byte>(ptr, 64);
				span.Clear();
				var bytes = Encoding.UTF8.GetBytes(value ?? string.Empty);
				bytes.AsSpan(0, Math.Min(bytes.Length, 64 - 1)).CopyTo(span);
			}
		}
	}
}

/// <summary>
/// <para>called when the user tries to join a lobby from their friends list</para>
/// <para>game client should attempt to connect to specified lobby when this is received</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamFriendsCallbacks + 33)]
public struct GameLobbyJoinRequested_t
{
	public const int k_iCallback = Constants.k_iSteamFriendsCallbacks + 33;
	public CSteamID m_steamIDLobby;
	/// <summary>
	/// <para>The friend they did the join via (will be invalid if not directly via a friend)</para>
	/// </summary>
	public CSteamID m_steamIDFriend;
}

/// <summary>
/// <para>called when an avatar is loaded in from a previous GetLargeFriendAvatar() call</para>
/// <para>if the image wasn&apos;t already available</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 4)]
[CallbackIdentity(Constants.k_iSteamFriendsCallbacks + 34)]
public struct AvatarImageLoaded_t
{
	public const int k_iCallback = Constants.k_iSteamFriendsCallbacks + 34;
	/// <summary>
	/// <para>steamid the avatar has been loaded for</para>
	/// </summary>
	public CSteamID m_steamID;
	/// <summary>
	/// <para>the image index of the now loaded image</para>
	/// </summary>
	public int m_iImage;
	/// <summary>
	/// <para>width of the loaded image</para>
	/// </summary>
	public int m_iWide;
	/// <summary>
	/// <para>height of the loaded image</para>
	/// </summary>
	public int m_iTall;
}

/// <summary>
/// <para>marks the return of a request officer list call</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamFriendsCallbacks + 35)]
public struct ClanOfficerListResponse_t
{
	public const int k_iCallback = Constants.k_iSteamFriendsCallbacks + 35;
	public CSteamID m_steamIDClan;
	public int m_cOfficers;
	public byte m_bSuccess;
}

/// <summary>
/// <para>callback indicating updated data about friends rich presence information</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 4)]
[CallbackIdentity(Constants.k_iSteamFriendsCallbacks + 36)]
public struct FriendRichPresenceUpdate_t
{
	public const int k_iCallback = Constants.k_iSteamFriendsCallbacks + 36;
	/// <summary>
	/// <para>friend who&apos;s rich presence has changed</para>
	/// </summary>
	public CSteamID m_steamIDFriend;
	/// <summary>
	/// <para>the appID of the game (should always be the current game)</para>
	/// </summary>
	public AppId_t m_nAppID;
}

/// <summary>
/// <para>called when the user tries to join a game from their friends list</para>
/// <para>rich presence will have been set with the &quot;connect&quot; key which is set here</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamFriendsCallbacks + 37)]
public unsafe struct GameRichPresenceJoinRequested_t
{
	public const int k_iCallback = Constants.k_iSteamFriendsCallbacks + 37;
	/// <summary>
	/// <para>the friend they did the join via (will be invalid if not directly via a friend)</para>
	/// </summary>
	public CSteamID m_steamIDFriend;
	private fixed byte m_rgchConnect_[Constants.k_cchMaxRichPresenceValueLength];
	public string m_rgchConnect
	{
		get
		{
			fixed (byte* ptr = m_rgchConnect_)
			{
				var span = new ReadOnlySpan<byte>(ptr, Constants.k_cchMaxRichPresenceValueLength);
				var length = span.IndexOf((byte)0);
				return Encoding.UTF8.GetString(length < 0 ? span : span[..length]);
			}
		}
		set
		{
			fixed (byte* ptr = m_rgchConnect_)
			{
				var span = new Span<byte>(ptr, Constants.k_cchMaxRichPresenceValueLength);
				span.Clear();
				var bytes = Encoding.UTF8.GetBytes(value ?? string.Empty);
				bytes.AsSpan(0, Math.Min(bytes.Length, Constants.k_cchMaxRichPresenceValueLength - 1)).CopyTo(span);
			}
		}
	}
}

/// <summary>
/// <para>a chat message has been received for a clan chat the game has joined</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 4)]
[CallbackIdentity(Constants.k_iSteamFriendsCallbacks + 38)]
public struct GameConnectedClanChatMsg_t
{
	public const int k_iCallback = Constants.k_iSteamFriendsCallbacks + 38;
	public CSteamID m_steamIDClanChat;
	public CSteamID m_steamIDUser;
	public int m_iMessageID;
}

/// <summary>
/// <para>a user has joined a clan chat</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamFriendsCallbacks + 39)]
public struct GameConnectedChatJoin_t
{
	public const int k_iCallback = Constants.k_iSteamFriendsCallbacks + 39;
	public CSteamID m_steamIDClanChat;
	public CSteamID m_steamIDUser;
}

/// <summary>
/// <para>a user has left the chat we&apos;re in</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 1)]
[CallbackIdentity(Constants.k_iSteamFriendsCallbacks + 40)]
public struct GameConnectedChatLeave_t
{
	public const int k_iCallback = Constants.k_iSteamFriendsCallbacks + 40;
	public CSteamID m_steamIDClanChat;
	public CSteamID m_steamIDUser;
	/// <summary>
	/// <para>true if admin kicked</para>
	/// </summary>
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bKicked;
	/// <summary>
	/// <para>true if Steam connection dropped</para>
	/// </summary>
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bDropped;
}

/// <summary>
/// <para>a DownloadClanActivityCounts() call has finished</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamFriendsCallbacks + 41)]
public struct DownloadClanActivityCountsResult_t
{
	public const int k_iCallback = Constants.k_iSteamFriendsCallbacks + 41;
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bSuccess;
}

/// <summary>
/// <para>a JoinClanChatRoom() call has finished</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 4)]
[CallbackIdentity(Constants.k_iSteamFriendsCallbacks + 42)]
public struct JoinClanChatRoomCompletionResult_t
{
	public const int k_iCallback = Constants.k_iSteamFriendsCallbacks + 42;
	public CSteamID m_steamIDClanChat;
	public EChatRoomEnterResponse m_eChatRoomEnterResponse;
}

/// <summary>
/// <para>a chat message has been received from a user</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 4)]
[CallbackIdentity(Constants.k_iSteamFriendsCallbacks + 43)]
public struct GameConnectedFriendChatMsg_t
{
	public const int k_iCallback = Constants.k_iSteamFriendsCallbacks + 43;
	public CSteamID m_steamIDUser;
	public int m_iMessageID;
}

[StructLayout(LayoutKind.Sequential, Pack = 4)]
[CallbackIdentity(Constants.k_iSteamFriendsCallbacks + 44)]
public struct FriendsGetFollowerCount_t
{
	public const int k_iCallback = Constants.k_iSteamFriendsCallbacks + 44;
	public EResult m_eResult;
	public CSteamID m_steamID;
	public int m_nCount;
}

[StructLayout(LayoutKind.Sequential, Pack = 4)]
[CallbackIdentity(Constants.k_iSteamFriendsCallbacks + 45)]
public struct FriendsIsFollowing_t
{
	public const int k_iCallback = Constants.k_iSteamFriendsCallbacks + 45;
	public EResult m_eResult;
	public CSteamID m_steamID;
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bIsFollowing;
}

[StructLayout(LayoutKind.Sequential, Pack = 4)]
[CallbackIdentity(Constants.k_iSteamFriendsCallbacks + 46)]
public struct FriendsEnumerateFollowingList_t
{
	public const int k_iCallback = Constants.k_iSteamFriendsCallbacks + 46;
	public EResult m_eResult;
	[InlineArray(Constants.k_cEnumerateFollowersMax)]
	public struct m_rgSteamIDArray
	{
		private CSteamID _element0;
	}
	public m_rgSteamIDArray m_rgSteamID;
	public int m_nResultsReturned;
	public int m_nTotalResultCount;
}

/// <summary>
/// <para>Invoked when the status of unread messages changes</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value, Size = 1)]
[CallbackIdentity(Constants.k_iSteamFriendsCallbacks + 48)]
public struct UnreadChatMessagesChanged_t
{
	public const int k_iCallback = Constants.k_iSteamFriendsCallbacks + 48;
}

/// <summary>
/// <para>Dispatched when an overlay browser instance is navigated to a protocol/scheme registered by RegisterProtocolInOverlayBrowser()</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamFriendsCallbacks + 49)]
public unsafe struct OverlayBrowserProtocolNavigation_t
{
	public const int k_iCallback = Constants.k_iSteamFriendsCallbacks + 49;
	private fixed byte rgchURI_[1024];
	public string rgchURI
	{
		get
		{
			fixed (byte* ptr = rgchURI_)
			{
				var span = new ReadOnlySpan<byte>(ptr, 1024);
				var length = span.IndexOf((byte)0);
				return Encoding.UTF8.GetString(length < 0 ? span : span[..length]);
			}
		}
		set
		{
			fixed (byte* ptr = rgchURI_)
			{
				var span = new Span<byte>(ptr, 1024);
				span.Clear();
				var bytes = Encoding.UTF8.GetBytes(value ?? string.Empty);
				bytes.AsSpan(0, Math.Min(bytes.Length, 1024 - 1)).CopyTo(span);
			}
		}
	}
}

/// <summary>
/// <para>A user&apos;s equipped profile items have changed</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamFriendsCallbacks + 50)]
public struct EquippedProfileItemsChanged_t
{
	public const int k_iCallback = Constants.k_iSteamFriendsCallbacks + 50;
	public CSteamID m_steamID;
}

/// <summary>
/// <para></para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamFriendsCallbacks + 51)]
public struct EquippedProfileItems_t
{
	public const int k_iCallback = Constants.k_iSteamFriendsCallbacks + 51;
	public EResult m_eResult;
	public CSteamID m_steamID;
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bHasAnimatedAvatar;
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bHasAvatarFrame;
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bHasProfileModifier;
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bHasProfileBackground;
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bHasMiniProfileBackground;
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bFromCache;
}

/// <summary>
/// <para>callbacks</para>
/// <para>callback notification - A new message is available for reading from the message queue</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamGameCoordinatorCallbacks + 1)]
public struct GCMessageAvailable_t
{
	public const int k_iCallback = Constants.k_iSteamGameCoordinatorCallbacks + 1;
	public uint m_nMessageSize;
}

/// <summary>
/// <para>callback notification - A message failed to make it to the GC. It may be down temporarily</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value, Size = 1)]
[CallbackIdentity(Constants.k_iSteamGameCoordinatorCallbacks + 2)]
public struct GCMessageFailed_t
{
	public const int k_iCallback = Constants.k_iSteamGameCoordinatorCallbacks + 2;
}

/// <summary>
/// <para>callbacks</para>
/// <para>client has been approved to connect to this game server</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamGameServerCallbacks + 1)]
public struct GSClientApprove_t
{
	public const int k_iCallback = Constants.k_iSteamGameServerCallbacks + 1;
	/// <summary>
	/// <para>SteamID of approved player</para>
	/// </summary>
	public CSteamID m_SteamID;
	/// <summary>
	/// <para>SteamID of original owner for game license</para>
	/// </summary>
	public CSteamID m_OwnerSteamID;
}

/// <summary>
/// <para>client has been denied to connection to this game server</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 4)]
[CallbackIdentity(Constants.k_iSteamGameServerCallbacks + 2)]
public unsafe struct GSClientDeny_t
{
	public const int k_iCallback = Constants.k_iSteamGameServerCallbacks + 2;
	public CSteamID m_SteamID;
	public EDenyReason m_eDenyReason;
	private fixed byte m_rgchOptionalText_[128];
	public string m_rgchOptionalText
	{
		get
		{
			fixed (byte* ptr = m_rgchOptionalText_)
			{
				var span = new ReadOnlySpan<byte>(ptr, 128);
				var length = span.IndexOf((byte)0);
				return Encoding.UTF8.GetString(length < 0 ? span : span[..length]);
			}
		}
		set
		{
			fixed (byte* ptr = m_rgchOptionalText_)
			{
				var span = new Span<byte>(ptr, 128);
				span.Clear();
				var bytes = Encoding.UTF8.GetBytes(value ?? string.Empty);
				bytes.AsSpan(0, Math.Min(bytes.Length, 128 - 1)).CopyTo(span);
			}
		}
	}
}

/// <summary>
/// <para>request the game server should kick the user</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 4)]
[CallbackIdentity(Constants.k_iSteamGameServerCallbacks + 3)]
public struct GSClientKick_t
{
	public const int k_iCallback = Constants.k_iSteamGameServerCallbacks + 3;
	public CSteamID m_SteamID;
	public EDenyReason m_eDenyReason;
}

/// <summary>
/// <para>NOTE: callback values 4 and 5 are skipped because they are used for old deprecated callbacks,</para>
/// <para>do not reuse them here.</para>
/// <para>client achievement info</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamGameServerCallbacks + 6)]
public unsafe struct GSClientAchievementStatus_t
{
	public const int k_iCallback = Constants.k_iSteamGameServerCallbacks + 6;
	public ulong m_SteamID;
	private fixed byte m_pchAchievement_[128];
	public string m_pchAchievement
	{
		get
		{
			fixed (byte* ptr = m_pchAchievement_)
			{
				var span = new ReadOnlySpan<byte>(ptr, 128);
				var length = span.IndexOf((byte)0);
				return Encoding.UTF8.GetString(length < 0 ? span : span[..length]);
			}
		}
		set
		{
			fixed (byte* ptr = m_pchAchievement_)
			{
				var span = new Span<byte>(ptr, 128);
				span.Clear();
				var bytes = Encoding.UTF8.GetBytes(value ?? string.Empty);
				bytes.AsSpan(0, Math.Min(bytes.Length, 128 - 1)).CopyTo(span);
			}
		}
	}
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bUnlocked;
}

/// <summary>
/// <para>received when the game server requests to be displayed as secure (VAC protected)</para>
/// <para>m_bSecure is true if the game server should display itself as secure to users, false otherwise</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamUserCallbacks + 15)]
public struct GSPolicyResponse_t
{
	public const int k_iCallback = Constants.k_iSteamUserCallbacks + 15;
	public byte m_bSecure;
}

/// <summary>
/// <para>GS gameplay stats info</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamGameServerCallbacks + 7)]
public struct GSGameplayStats_t
{
	public const int k_iCallback = Constants.k_iSteamGameServerCallbacks + 7;
	/// <summary>
	/// <para>Result of the call</para>
	/// </summary>
	public EResult m_eResult;
	/// <summary>
	/// <para>Overall rank of the server (0-based)</para>
	/// </summary>
	public int m_nRank;
	/// <summary>
	/// <para>Total number of clients who have ever connected to the server</para>
	/// </summary>
	public uint m_unTotalConnects;
	/// <summary>
	/// <para>Total number of minutes ever played on the server</para>
	/// </summary>
	public uint m_unTotalMinutesPlayed;
}

/// <summary>
/// <para>send as a reply to RequestUserGroupStatus()</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 1)]
[CallbackIdentity(Constants.k_iSteamGameServerCallbacks + 8)]
public struct GSClientGroupStatus_t
{
	public const int k_iCallback = Constants.k_iSteamGameServerCallbacks + 8;
	public CSteamID m_SteamIDUser;
	public CSteamID m_SteamIDGroup;
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bMember;
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bOfficer;
}

/// <summary>
/// <para>Sent as a reply to GetServerReputation()</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamGameServerCallbacks + 9)]
public struct GSReputation_t
{
	public const int k_iCallback = Constants.k_iSteamGameServerCallbacks + 9;
	/// <summary>
	/// <para>Result of the call;</para>
	/// </summary>
	public EResult m_eResult;
	/// <summary>
	/// <para>The reputation score for the game server</para>
	/// </summary>
	public uint m_unReputationScore;
	/// <summary>
	/// <para>True if the server is banned from the Steam</para>
	/// </summary>
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bBanned;
	/// <summary>
	/// <para>master servers</para>
	/// <para>The following members are only filled out if m_bBanned is true. They will all</para>
	/// <para>be set to zero otherwise. Master server bans are by IP so it is possible to be</para>
	/// <para>banned even when the score is good high if there is a bad server on another port.</para>
	/// <para>This information can be used to determine which server is bad.</para>
	/// <para>The IP of the banned server</para>
	/// </summary>
	public uint m_unBannedIP;
	/// <summary>
	/// <para>The port of the banned server</para>
	/// </summary>
	public ushort m_usBannedPort;
	/// <summary>
	/// <para>The game ID the banned server is serving</para>
	/// </summary>
	public ulong m_ulBannedGameID;
	/// <summary>
	/// <para>Time the ban expires, expressed in the Unix epoch (seconds since 1/1/1970)</para>
	/// </summary>
	public uint m_unBanExpires;
}

/// <summary>
/// <para>Sent as a reply to AssociateWithClan()</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamGameServerCallbacks + 10)]
public struct AssociateWithClanResult_t
{
	public const int k_iCallback = Constants.k_iSteamGameServerCallbacks + 10;
	/// <summary>
	/// <para>Result of the call;</para>
	/// </summary>
	public EResult m_eResult;
}

/// <summary>
/// <para>Sent as a reply to ComputeNewPlayerCompatibility()</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamGameServerCallbacks + 11)]
public struct ComputeNewPlayerCompatibilityResult_t
{
	public const int k_iCallback = Constants.k_iSteamGameServerCallbacks + 11;
	/// <summary>
	/// <para>Result of the call;</para>
	/// </summary>
	public EResult m_eResult;
	public int m_cPlayersThatDontLikeCandidate;
	public int m_cPlayersThatCandidateDoesntLike;
	public int m_cClanPlayersThatDontLikeCandidate;
	public CSteamID m_SteamIDCandidate;
}

/// <summary>
/// <para>callbacks</para>
/// <para>called when the latests stats and achievements have been received</para>
/// <para>from the server</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 4)]
[CallbackIdentity(Constants.k_iSteamGameServerStatsCallbacks)]
public struct GSStatsReceived_t
{
	public const int k_iCallback = Constants.k_iSteamGameServerStatsCallbacks;
	/// <summary>
	/// <para>Success / error fetching the stats</para>
	/// </summary>
	public EResult m_eResult;
	/// <summary>
	/// <para>The user for whom the stats are retrieved for</para>
	/// </summary>
	public CSteamID m_steamIDUser;
}

/// <summary>
/// <para>result of a request to store the user stats for a game</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 4)]
[CallbackIdentity(Constants.k_iSteamGameServerStatsCallbacks + 1)]
public struct GSStatsStored_t
{
	public const int k_iCallback = Constants.k_iSteamGameServerStatsCallbacks + 1;
	/// <summary>
	/// <para>success / error</para>
	/// </summary>
	public EResult m_eResult;
	/// <summary>
	/// <para>The user for whom the stats were stored</para>
	/// </summary>
	public CSteamID m_steamIDUser;
}

/// <summary>
/// <para>Callback indicating that a user&apos;s stats have been unloaded.</para>
/// <para>Call RequestUserStats again to access stats for this user</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamUserStatsCallbacks + 8)]
public struct GSStatsUnloaded_t
{
	public const int k_iCallback = Constants.k_iSteamUserStatsCallbacks + 8;
	/// <summary>
	/// <para>User whose stats have been unloaded</para>
	/// </summary>
	public CSteamID m_steamIDUser;
}

/// <summary>
/// <para>callbacks</para>
/// <para>The browser is ready for use</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamHTMLSurfaceCallbacks + 1)]
public struct HTML_BrowserReady_t
{
	public const int k_iCallback = Constants.k_iSteamHTMLSurfaceCallbacks + 1;
	/// <summary>
	/// <para>this browser is now fully created and ready to navigate to pages</para>
	/// </summary>
	public HHTMLBrowser unBrowserHandle;
}

/// <summary>
/// <para>the browser has a pending paint</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamHTMLSurfaceCallbacks + 2)]
public struct HTML_NeedsPaint_t
{
	public const int k_iCallback = Constants.k_iSteamHTMLSurfaceCallbacks + 2;
	/// <summary>
	/// <para>the browser that needs the paint</para>
	/// </summary>
	public HHTMLBrowser unBrowserHandle;
	/// <summary>
	/// <para>a pointer to the B8G8R8A8 data for this surface, valid until SteamAPI_RunCallbacks is next called</para>
	/// </summary>
	public IntPtr pBGRA;
	/// <summary>
	/// <para>the total width of the pBGRA texture</para>
	/// </summary>
	public uint unWide;
	/// <summary>
	/// <para>the total height of the pBGRA texture</para>
	/// </summary>
	public uint unTall;
	/// <summary>
	/// <para>the offset in X for the damage rect for this update</para>
	/// </summary>
	public uint unUpdateX;
	/// <summary>
	/// <para>the offset in Y for the damage rect for this update</para>
	/// </summary>
	public uint unUpdateY;
	/// <summary>
	/// <para>the width of the damage rect for this update</para>
	/// </summary>
	public uint unUpdateWide;
	/// <summary>
	/// <para>the height of the damage rect for this update</para>
	/// </summary>
	public uint unUpdateTall;
	/// <summary>
	/// <para>the page scroll the browser was at when this texture was rendered</para>
	/// </summary>
	public uint unScrollX;
	/// <summary>
	/// <para>the page scroll the browser was at when this texture was rendered</para>
	/// </summary>
	public uint unScrollY;
	/// <summary>
	/// <para>the page scale factor on this page when rendered</para>
	/// </summary>
	public float flPageScale;
	/// <summary>
	/// <para>incremented on each new page load, you can use this to reject draws while navigating to new pages</para>
	/// </summary>
	public uint unPageSerial;
}

/// <summary>
/// <para>The browser wanted to navigate to a new page</para>
/// <para>NOTE - you MUST call AllowStartRequest in response to this callback</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamHTMLSurfaceCallbacks + 3)]
public struct HTML_StartRequest_t
{
	public const int k_iCallback = Constants.k_iSteamHTMLSurfaceCallbacks + 3;
	/// <summary>
	/// <para>the handle of the surface navigating</para>
	/// </summary>
	public HHTMLBrowser unBrowserHandle;
	/// <summary>
	/// <para>the url they wish to navigate to</para>
	/// </summary>
	public string pchURL;
	/// <summary>
	/// <para>the html link target type  (i.e _blank, _self, _parent, _top )</para>
	/// </summary>
	public string pchTarget;
	/// <summary>
	/// <para>any posted data for the request</para>
	/// </summary>
	public string pchPostData;
	/// <summary>
	/// <para>true if this was a http/html redirect from the last load request</para>
	/// </summary>
	[MarshalAs(UnmanagedType.I1)]
	public bool bIsRedirect;
}

/// <summary>
/// <para>The browser has been requested to close due to user interaction (usually from a javascript window.close() call)</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamHTMLSurfaceCallbacks + 4)]
public struct HTML_CloseBrowser_t
{
	public const int k_iCallback = Constants.k_iSteamHTMLSurfaceCallbacks + 4;
	/// <summary>
	/// <para>the handle of the surface</para>
	/// </summary>
	public HHTMLBrowser unBrowserHandle;
}

/// <summary>
/// <para>the browser is navigating to a new url</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamHTMLSurfaceCallbacks + 5)]
public struct HTML_URLChanged_t
{
	public const int k_iCallback = Constants.k_iSteamHTMLSurfaceCallbacks + 5;
	/// <summary>
	/// <para>the handle of the surface navigating</para>
	/// </summary>
	public HHTMLBrowser unBrowserHandle;
	/// <summary>
	/// <para>the url they wish to navigate to</para>
	/// </summary>
	public string pchURL;
	/// <summary>
	/// <para>any posted data for the request</para>
	/// </summary>
	public string pchPostData;
	/// <summary>
	/// <para>true if this was a http/html redirect from the last load request</para>
	/// </summary>
	[MarshalAs(UnmanagedType.I1)]
	public bool bIsRedirect;
	/// <summary>
	/// <para>the title of the page</para>
	/// </summary>
	public string pchPageTitle;
	/// <summary>
	/// <para>true if this was from a fresh tab and not a click on an existing page</para>
	/// </summary>
	[MarshalAs(UnmanagedType.I1)]
	public bool bNewNavigation;
}

/// <summary>
/// <para>A page is finished loading</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamHTMLSurfaceCallbacks + 6)]
public struct HTML_FinishedRequest_t
{
	public const int k_iCallback = Constants.k_iSteamHTMLSurfaceCallbacks + 6;
	/// <summary>
	/// <para>the handle of the surface</para>
	/// </summary>
	public HHTMLBrowser unBrowserHandle;
	public string pchURL;
	public string pchPageTitle;
}

/// <summary>
/// <para>a request to load this url in a new tab</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamHTMLSurfaceCallbacks + 7)]
public struct HTML_OpenLinkInNewTab_t
{
	public const int k_iCallback = Constants.k_iSteamHTMLSurfaceCallbacks + 7;
	/// <summary>
	/// <para>the handle of the surface</para>
	/// </summary>
	public HHTMLBrowser unBrowserHandle;
	public string pchURL;
}

/// <summary>
/// <para>the page has a new title now</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamHTMLSurfaceCallbacks + 8)]
public struct HTML_ChangedTitle_t
{
	public const int k_iCallback = Constants.k_iSteamHTMLSurfaceCallbacks + 8;
	/// <summary>
	/// <para>the handle of the surface</para>
	/// </summary>
	public HHTMLBrowser unBrowserHandle;
	public string pchTitle;
}

/// <summary>
/// <para>results from a search</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamHTMLSurfaceCallbacks + 9)]
public struct HTML_SearchResults_t
{
	public const int k_iCallback = Constants.k_iSteamHTMLSurfaceCallbacks + 9;
	/// <summary>
	/// <para>the handle of the surface</para>
	/// </summary>
	public HHTMLBrowser unBrowserHandle;
	public uint unResults;
	public uint unCurrentMatch;
}

/// <summary>
/// <para>page history status changed on the ability to go backwards and forward</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamHTMLSurfaceCallbacks + 10)]
public struct HTML_CanGoBackAndForward_t
{
	public const int k_iCallback = Constants.k_iSteamHTMLSurfaceCallbacks + 10;
	/// <summary>
	/// <para>the handle of the surface</para>
	/// </summary>
	public HHTMLBrowser unBrowserHandle;
	[MarshalAs(UnmanagedType.I1)]
	public bool bCanGoBack;
	[MarshalAs(UnmanagedType.I1)]
	public bool bCanGoForward;
}

/// <summary>
/// <para>details on the visibility and size of the horizontal scrollbar</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamHTMLSurfaceCallbacks + 11)]
public struct HTML_HorizontalScroll_t
{
	public const int k_iCallback = Constants.k_iSteamHTMLSurfaceCallbacks + 11;
	/// <summary>
	/// <para>the handle of the surface</para>
	/// </summary>
	public HHTMLBrowser unBrowserHandle;
	public uint unScrollMax;
	public uint unScrollCurrent;
	public float flPageScale;
	[MarshalAs(UnmanagedType.I1)]
	public bool bVisible;
	public uint unPageSize;
}

/// <summary>
/// <para>details on the visibility and size of the vertical scrollbar</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamHTMLSurfaceCallbacks + 12)]
public struct HTML_VerticalScroll_t
{
	public const int k_iCallback = Constants.k_iSteamHTMLSurfaceCallbacks + 12;
	/// <summary>
	/// <para>the handle of the surface</para>
	/// </summary>
	public HHTMLBrowser unBrowserHandle;
	public uint unScrollMax;
	public uint unScrollCurrent;
	public float flPageScale;
	[MarshalAs(UnmanagedType.I1)]
	public bool bVisible;
	public uint unPageSize;
}

/// <summary>
/// <para>response to GetLinkAtPosition call</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamHTMLSurfaceCallbacks + 13)]
public struct HTML_LinkAtPosition_t
{
	public const int k_iCallback = Constants.k_iSteamHTMLSurfaceCallbacks + 13;
	/// <summary>
	/// <para>the handle of the surface</para>
	/// </summary>
	public HHTMLBrowser unBrowserHandle;
	/// <summary>
	/// <para>NOTE - Not currently set</para>
	/// </summary>
	public uint x;
	/// <summary>
	/// <para>NOTE - Not currently set</para>
	/// </summary>
	public uint y;
	public string pchURL;
	[MarshalAs(UnmanagedType.I1)]
	public bool bInput;
	[MarshalAs(UnmanagedType.I1)]
	public bool bLiveLink;
}

/// <summary>
/// <para>show a Javascript alert dialog, call JSDialogResponse</para>
/// <para>when the user dismisses this dialog (or right away to ignore it)</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamHTMLSurfaceCallbacks + 14)]
public struct HTML_JSAlert_t
{
	public const int k_iCallback = Constants.k_iSteamHTMLSurfaceCallbacks + 14;
	/// <summary>
	/// <para>the handle of the surface</para>
	/// </summary>
	public HHTMLBrowser unBrowserHandle;
	public string pchMessage;
}

/// <summary>
/// <para>show a Javascript confirmation dialog, call JSDialogResponse</para>
/// <para>when the user dismisses this dialog (or right away to ignore it)</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamHTMLSurfaceCallbacks + 15)]
public struct HTML_JSConfirm_t
{
	public const int k_iCallback = Constants.k_iSteamHTMLSurfaceCallbacks + 15;
	/// <summary>
	/// <para>the handle of the surface</para>
	/// </summary>
	public HHTMLBrowser unBrowserHandle;
	public string pchMessage;
}

/// <summary>
/// <para>when received show a file open dialog</para>
/// <para>then call FileLoadDialogResponse with the file(s) the user selected.</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamHTMLSurfaceCallbacks + 16)]
public struct HTML_FileOpenDialog_t
{
	public const int k_iCallback = Constants.k_iSteamHTMLSurfaceCallbacks + 16;
	/// <summary>
	/// <para>the handle of the surface</para>
	/// </summary>
	public HHTMLBrowser unBrowserHandle;
	public string pchTitle;
	public string pchInitialFile;
}

/// <summary>
/// <para>a new html window is being created.</para>
/// <para>IMPORTANT NOTE: at this time, the API does not allow you to acknowledge or</para>
/// <para>render the contents of this new window, so the new window is always destroyed</para>
/// <para>immediately. The URL and other parameters of the new window are passed here</para>
/// <para>to give your application the opportunity to call CreateBrowser and set up</para>
/// <para>a new browser in response to the attempted popup, if you wish to do so.</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamHTMLSurfaceCallbacks + 21)]
public struct HTML_NewWindow_t
{
	public const int k_iCallback = Constants.k_iSteamHTMLSurfaceCallbacks + 21;
	/// <summary>
	/// <para>the handle of the current surface</para>
	/// </summary>
	public HHTMLBrowser unBrowserHandle;
	/// <summary>
	/// <para>the page to load</para>
	/// </summary>
	public string pchURL;
	/// <summary>
	/// <para>the x pos into the page to display the popup</para>
	/// </summary>
	public uint unX;
	/// <summary>
	/// <para>the y pos into the page to display the popup</para>
	/// </summary>
	public uint unY;
	/// <summary>
	/// <para>the total width of the pBGRA texture</para>
	/// </summary>
	public uint unWide;
	/// <summary>
	/// <para>the total height of the pBGRA texture</para>
	/// </summary>
	public uint unTall;
	public HHTMLBrowser unNewWindow_BrowserHandle_IGNORE;
}

/// <summary>
/// <para>change the cursor to display</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamHTMLSurfaceCallbacks + 22)]
public struct HTML_SetCursor_t
{
	public const int k_iCallback = Constants.k_iSteamHTMLSurfaceCallbacks + 22;
	/// <summary>
	/// <para>the handle of the surface</para>
	/// </summary>
	public HHTMLBrowser unBrowserHandle;
	/// <summary>
	/// <para>the EHTMLMouseCursor to display</para>
	/// </summary>
	public uint eMouseCursor;
}

/// <summary>
/// <para>informational message from the browser</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamHTMLSurfaceCallbacks + 23)]
public struct HTML_StatusText_t
{
	public const int k_iCallback = Constants.k_iSteamHTMLSurfaceCallbacks + 23;
	/// <summary>
	/// <para>the handle of the surface</para>
	/// </summary>
	public HHTMLBrowser unBrowserHandle;
	/// <summary>
	/// <para>the message text</para>
	/// </summary>
	public string pchMsg;
}

/// <summary>
/// <para>show a tooltip</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamHTMLSurfaceCallbacks + 24)]
public struct HTML_ShowToolTip_t
{
	public const int k_iCallback = Constants.k_iSteamHTMLSurfaceCallbacks + 24;
	/// <summary>
	/// <para>the handle of the surface</para>
	/// </summary>
	public HHTMLBrowser unBrowserHandle;
	/// <summary>
	/// <para>the tooltip text</para>
	/// </summary>
	public string pchMsg;
}

/// <summary>
/// <para>update the text of an existing tooltip</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamHTMLSurfaceCallbacks + 25)]
public struct HTML_UpdateToolTip_t
{
	public const int k_iCallback = Constants.k_iSteamHTMLSurfaceCallbacks + 25;
	/// <summary>
	/// <para>the handle of the surface</para>
	/// </summary>
	public HHTMLBrowser unBrowserHandle;
	/// <summary>
	/// <para>the new tooltip text</para>
	/// </summary>
	public string pchMsg;
}

/// <summary>
/// <para>hide the tooltip you are showing</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamHTMLSurfaceCallbacks + 26)]
public struct HTML_HideToolTip_t
{
	public const int k_iCallback = Constants.k_iSteamHTMLSurfaceCallbacks + 26;
	/// <summary>
	/// <para>the handle of the surface</para>
	/// </summary>
	public HHTMLBrowser unBrowserHandle;
}

/// <summary>
/// <para>The browser has restarted due to an internal failure, use this new handle value</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamHTMLSurfaceCallbacks + 27)]
public struct HTML_BrowserRestarted_t
{
	public const int k_iCallback = Constants.k_iSteamHTMLSurfaceCallbacks + 27;
	/// <summary>
	/// <para>this is the new browser handle after the restart</para>
	/// </summary>
	public HHTMLBrowser unBrowserHandle;
	/// <summary>
	/// <para>the handle for the browser before the restart, if your handle was this then switch to using unBrowserHandle for API calls</para>
	/// </summary>
	public HHTMLBrowser unOldBrowserHandle;
}

/// <summary>
/// <para>callbacks</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamHTTPCallbacks + 1)]
public struct HTTPRequestCompleted_t
{
	public const int k_iCallback = Constants.k_iSteamHTTPCallbacks + 1;
	/// <summary>
	/// <para>Handle value for the request that has completed.</para>
	/// </summary>
	public HTTPRequestHandle m_hRequest;
	/// <summary>
	/// <para>Context value that the user defined on the request that this callback is associated with, 0 if</para>
	/// <para>no context value was set.</para>
	/// </summary>
	public ulong m_ulContextValue;
	/// <summary>
	/// <para>This will be true if we actually got any sort of response from the server (even an error).</para>
	/// <para>It will be false if we failed due to an internal error or client side network failure.</para>
	/// </summary>
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bRequestSuccessful;
	/// <summary>
	/// <para>Will be the HTTP status code value returned by the server, k_EHTTPStatusCode200OK is the normal</para>
	/// <para>OK response, if you get something else you probably need to treat it as a failure.</para>
	/// </summary>
	public EHTTPStatusCode m_eStatusCode;
	/// <summary>
	/// <para>Same as GetHTTPResponseBodySize()</para>
	/// </summary>
	public uint m_unBodySize;
}

[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamHTTPCallbacks + 2)]
public struct HTTPRequestHeadersReceived_t
{
	public const int k_iCallback = Constants.k_iSteamHTTPCallbacks + 2;
	/// <summary>
	/// <para>Handle value for the request that has received headers.</para>
	/// </summary>
	public HTTPRequestHandle m_hRequest;
	/// <summary>
	/// <para>Context value that the user defined on the request that this callback is associated with, 0 if</para>
	/// <para>no context value was set.</para>
	/// </summary>
	public ulong m_ulContextValue;
}

[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamHTTPCallbacks + 3)]
public struct HTTPRequestDataReceived_t
{
	public const int k_iCallback = Constants.k_iSteamHTTPCallbacks + 3;
	/// <summary>
	/// <para>Handle value for the request that has received data.</para>
	/// </summary>
	public HTTPRequestHandle m_hRequest;
	/// <summary>
	/// <para>Context value that the user defined on the request that this callback is associated with, 0 if</para>
	/// <para>no context value was set.</para>
	/// </summary>
	public ulong m_ulContextValue;
	/// <summary>
	/// <para>Offset to provide to GetHTTPStreamingResponseBodyData to get this chunk of data</para>
	/// </summary>
	public uint m_cOffset;
	/// <summary>
	/// <para>Size to provide to GetHTTPStreamingResponseBodyData to get this chunk of data</para>
	/// </summary>
	public uint m_cBytesReceived;
}

/// <summary>
/// <para>called when a new controller has been connected, will fire once</para>
/// <para>per controller if multiple new controllers connect in the same frame</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamControllerCallbacks + 1)]
public struct SteamInputDeviceConnected_t
{
	public const int k_iCallback = Constants.k_iSteamControllerCallbacks + 1;
	/// <summary>
	/// <para>Handle for device</para>
	/// </summary>
	public InputHandle_t m_ulConnectedDeviceHandle;
}

/// <summary>
/// <para>called when a new controller has been connected, will fire once</para>
/// <para>per controller if multiple new controllers connect in the same frame</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamControllerCallbacks + 2)]
public struct SteamInputDeviceDisconnected_t
{
	public const int k_iCallback = Constants.k_iSteamControllerCallbacks + 2;
	/// <summary>
	/// <para>Handle for device</para>
	/// </summary>
	public InputHandle_t m_ulDisconnectedDeviceHandle;
}

/// <summary>
/// <para>called when a controller configuration has been loaded, will fire once</para>
/// <para>per controller per focus change for Steam Input enabled controllers</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamControllerCallbacks + 3)]
public struct SteamInputConfigurationLoaded_t
{
	public const int k_iCallback = Constants.k_iSteamControllerCallbacks + 3;
	public AppId_t m_unAppID;
	/// <summary>
	/// <para>Handle for device</para>
	/// </summary>
	public InputHandle_t m_ulDeviceHandle;
	/// <summary>
	/// <para>May differ from local user when using</para>
	/// </summary>
	public CSteamID m_ulMappingCreator;
	/// <summary>
	/// <para>an unmodified community or official config</para>
	/// <para>Binding revision from In-game Action File.</para>
	/// </summary>
	public uint m_unMajorRevision;
	/// <summary>
	/// <para>Same value as queried by GetDeviceBindingRevision</para>
	/// </summary>
	public uint m_unMinorRevision;
	/// <summary>
	/// <para>Does the configuration contain any Analog/Digital actions?</para>
	/// </summary>
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bUsesSteamInputAPI;
	/// <summary>
	/// <para>Does the configuration contain any Xinput bindings?</para>
	/// </summary>
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bUsesGamepadAPI;
}

/// <summary>
/// <para>called when controller gamepad slots change - on Linux/macOS these</para>
/// <para>slots are shared for all running apps.</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamControllerCallbacks + 4)]
public struct SteamInputGamepadSlotChange_t
{
	public const int k_iCallback = Constants.k_iSteamControllerCallbacks + 4;
	public AppId_t m_unAppID;
	/// <summary>
	/// <para>Handle for device</para>
	/// </summary>
	public InputHandle_t m_ulDeviceHandle;
	/// <summary>
	/// <para>Type of device</para>
	/// </summary>
	public ESteamInputType m_eDeviceType;
	/// <summary>
	/// <para>Previous GamepadSlot - can be -1 controller doesn&apos;t uses gamepad bindings</para>
	/// </summary>
	public int m_nOldGamepadSlot;
	/// <summary>
	/// <para>New Gamepad Slot - can be -1 controller doesn&apos;t uses gamepad bindings</para>
	/// </summary>
	public int m_nNewGamepadSlot;
}

/// <summary>
/// <para>SteamInventoryResultReady_t callbacks are fired whenever asynchronous</para>
/// <para>results transition from &quot;Pending&quot; to &quot;OK&quot; or an error state. There will</para>
/// <para>always be exactly one callback per handle.</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamInventoryCallbacks + 0)]
public struct SteamInventoryResultReady_t
{
	public const int k_iCallback = Constants.k_iSteamInventoryCallbacks + 0;
	public SteamInventoryResult_t m_handle;
	public EResult m_result;
}

/// <summary>
/// <para>SteamInventoryFullUpdate_t callbacks are triggered when GetAllItems</para>
/// <para>successfully returns a result which is newer / fresher than the last</para>
/// <para>known result. (It will not trigger if the inventory hasn&apos;t changed,</para>
/// <para>or if results from two overlapping calls are reversed in flight and</para>
/// <para>the earlier result is already known to be stale/out-of-date.)</para>
/// <para>The normal ResultReady callback will still be triggered immediately</para>
/// <para>afterwards; this is an additional notification for your convenience.</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamInventoryCallbacks + 1)]
public struct SteamInventoryFullUpdate_t
{
	public const int k_iCallback = Constants.k_iSteamInventoryCallbacks + 1;
	public SteamInventoryResult_t m_handle;
}

/// <summary>
/// <para>A SteamInventoryDefinitionUpdate_t callback is triggered whenever</para>
/// <para>item definitions have been updated, which could be in response to</para>
/// <para>LoadItemDefinitions() or any other async request which required</para>
/// <para>a definition update in order to process results from the server.</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value, Size = 1)]
[CallbackIdentity(Constants.k_iSteamInventoryCallbacks + 2)]
public struct SteamInventoryDefinitionUpdate_t
{
	public const int k_iCallback = Constants.k_iSteamInventoryCallbacks + 2;
}

/// <summary>
/// <para>Returned</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamInventoryCallbacks + 3)]
public struct SteamInventoryEligiblePromoItemDefIDs_t
{
	public const int k_iCallback = Constants.k_iSteamInventoryCallbacks + 3;
	public EResult m_result;
	public CSteamID m_steamID;
	public int m_numEligiblePromoItemDefs;
	/// <summary>
	/// <para>indicates that the data was retrieved from the cache and not the server</para>
	/// </summary>
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bCachedData;
}

/// <summary>
/// <para>Triggered from StartPurchase call</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamInventoryCallbacks + 4)]
public struct SteamInventoryStartPurchaseResult_t
{
	public const int k_iCallback = Constants.k_iSteamInventoryCallbacks + 4;
	public EResult m_result;
	public ulong m_ulOrderID;
	public ulong m_ulTransID;
}

/// <summary>
/// <para>Triggered from RequestPrices</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamInventoryCallbacks + 5)]
public unsafe struct SteamInventoryRequestPricesResult_t
{
	public const int k_iCallback = Constants.k_iSteamInventoryCallbacks + 5;
	public EResult m_result;
	private fixed byte m_rgchCurrency_[4];
	public string m_rgchCurrency
	{
		get
		{
			fixed (byte* ptr = m_rgchCurrency_)
			{
				var span = new ReadOnlySpan<byte>(ptr, 4);
				var length = span.IndexOf((byte)0);
				return Encoding.UTF8.GetString(length < 0 ? span : span[..length]);
			}
		}
		set
		{
			fixed (byte* ptr = m_rgchCurrency_)
			{
				var span = new Span<byte>(ptr, 4);
				span.Clear();
				var bytes = Encoding.UTF8.GetBytes(value ?? string.Empty);
				bytes.AsSpan(0, Math.Min(bytes.Length, 4 - 1)).CopyTo(span);
			}
		}
	}
}

/// <summary>
/// <para>callbacks</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value, Size = 1)]
[CallbackIdentity(Constants.k_iSteamMusicCallbacks + 1)]
public struct PlaybackStatusHasChanged_t
{
	public const int k_iCallback = Constants.k_iSteamMusicCallbacks + 1;
}

[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamMusicCallbacks + 2)]
public struct VolumeHasChanged_t
{
	public const int k_iCallback = Constants.k_iSteamMusicCallbacks + 2;
	public float m_flNewVolume;
}

/// <summary>
/// <para>callbacks</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value, Size = 1)]
[CallbackIdentity(Constants.k_iSteamMusicRemoteCallbacks + 1)]
public struct MusicPlayerRemoteWillActivate_t
{
	public const int k_iCallback = Constants.k_iSteamMusicRemoteCallbacks + 1;
}

[StructLayout(LayoutKind.Sequential, Pack = Packsize.value, Size = 1)]
[CallbackIdentity(Constants.k_iSteamMusicRemoteCallbacks + 2)]
public struct MusicPlayerRemoteWillDeactivate_t
{
	public const int k_iCallback = Constants.k_iSteamMusicRemoteCallbacks + 2;
}

[StructLayout(LayoutKind.Sequential, Pack = Packsize.value, Size = 1)]
[CallbackIdentity(Constants.k_iSteamMusicRemoteCallbacks + 3)]
public struct MusicPlayerRemoteToFront_t
{
	public const int k_iCallback = Constants.k_iSteamMusicRemoteCallbacks + 3;
}

[StructLayout(LayoutKind.Sequential, Pack = Packsize.value, Size = 1)]
[CallbackIdentity(Constants.k_iSteamMusicRemoteCallbacks + 4)]
public struct MusicPlayerWillQuit_t
{
	public const int k_iCallback = Constants.k_iSteamMusicRemoteCallbacks + 4;
}

[StructLayout(LayoutKind.Sequential, Pack = Packsize.value, Size = 1)]
[CallbackIdentity(Constants.k_iSteamMusicRemoteCallbacks + 5)]
public struct MusicPlayerWantsPlay_t
{
	public const int k_iCallback = Constants.k_iSteamMusicRemoteCallbacks + 5;
}

[StructLayout(LayoutKind.Sequential, Pack = Packsize.value, Size = 1)]
[CallbackIdentity(Constants.k_iSteamMusicRemoteCallbacks + 6)]
public struct MusicPlayerWantsPause_t
{
	public const int k_iCallback = Constants.k_iSteamMusicRemoteCallbacks + 6;
}

[StructLayout(LayoutKind.Sequential, Pack = Packsize.value, Size = 1)]
[CallbackIdentity(Constants.k_iSteamMusicRemoteCallbacks + 7)]
public struct MusicPlayerWantsPlayPrevious_t
{
	public const int k_iCallback = Constants.k_iSteamMusicRemoteCallbacks + 7;
}

[StructLayout(LayoutKind.Sequential, Pack = Packsize.value, Size = 1)]
[CallbackIdentity(Constants.k_iSteamMusicRemoteCallbacks + 8)]
public struct MusicPlayerWantsPlayNext_t
{
	public const int k_iCallback = Constants.k_iSteamMusicRemoteCallbacks + 8;
}

[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamMusicRemoteCallbacks + 9)]
public struct MusicPlayerWantsShuffled_t
{
	public const int k_iCallback = Constants.k_iSteamMusicRemoteCallbacks + 9;
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bShuffled;
}

[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamMusicRemoteCallbacks + 10)]
public struct MusicPlayerWantsLooped_t
{
	public const int k_iCallback = Constants.k_iSteamMusicRemoteCallbacks + 10;
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bLooped;
}

[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamMusicCallbacks + 11)]
public struct MusicPlayerWantsVolume_t
{
	public const int k_iCallback = Constants.k_iSteamMusicCallbacks + 11;
	public float m_flNewVolume;
}

[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamMusicCallbacks + 12)]
public struct MusicPlayerSelectsQueueEntry_t
{
	public const int k_iCallback = Constants.k_iSteamMusicCallbacks + 12;
	public int nID;
}

[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamMusicCallbacks + 13)]
public struct MusicPlayerSelectsPlaylistEntry_t
{
	public const int k_iCallback = Constants.k_iSteamMusicCallbacks + 13;
	public int nID;
}

[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamMusicRemoteCallbacks + 14)]
public struct MusicPlayerWantsPlayingRepeatStatus_t
{
	public const int k_iCallback = Constants.k_iSteamMusicRemoteCallbacks + 14;
	public int m_nPlayingRepeatStatus;
}

/// <summary>
/// <para>callbacks</para>
/// <para>callback notification - a user wants to talk to us over the P2P channel via the SendP2PPacket() API</para>
/// <para>in response, a call to AcceptP2PPacketsFromUser() needs to be made, if you want to talk with them</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamNetworkingCallbacks + 2)]
public struct P2PSessionRequest_t
{
	public const int k_iCallback = Constants.k_iSteamNetworkingCallbacks + 2;
	/// <summary>
	/// <para>user who wants to talk to us</para>
	/// </summary>
	public CSteamID m_steamIDRemote;
}

/// <summary>
/// <para>callback notification - packets can&apos;t get through to the specified user via the SendP2PPacket() API</para>
/// <para>all packets queued packets unsent at this point will be dropped</para>
/// <para>further attempts to send will retry making the connection (but will be dropped if we fail again)</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 1)]
[CallbackIdentity(Constants.k_iSteamNetworkingCallbacks + 3)]
public struct P2PSessionConnectFail_t
{
	public const int k_iCallback = Constants.k_iSteamNetworkingCallbacks + 3;
	/// <summary>
	/// <para>user we were sending packets to</para>
	/// </summary>
	public CSteamID m_steamIDRemote;
	/// <summary>
	/// <para>EP2PSessionError indicating why we&apos;re having trouble</para>
	/// </summary>
	public byte m_eP2PSessionError;
}

/// <summary>
/// <para>callback notification - status of a socket has changed</para>
/// <para>used as part of the CreateListenSocket() / CreateP2PConnectionSocket()</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 4)]
[CallbackIdentity(Constants.k_iSteamNetworkingCallbacks + 1)]
public struct SocketStatusCallback_t
{
	public const int k_iCallback = Constants.k_iSteamNetworkingCallbacks + 1;
	/// <summary>
	/// <para>the socket used to send/receive data to the remote host</para>
	/// </summary>
	public SNetSocket_t m_hSocket;
	/// <summary>
	/// <para>this is the server socket that we were listening on; NULL if this was an outgoing connection</para>
	/// </summary>
	public SNetListenSocket_t m_hListenSocket;
	/// <summary>
	/// <para>remote steamID we have connected to, if it has one</para>
	/// </summary>
	public CSteamID m_steamIDRemote;
	/// <summary>
	/// <para>socket state, ESNetSocketState</para>
	/// </summary>
	public int m_eSNetSocketState;
}

/// <summary>
/// <para>Callbacks</para>
/// <para>Posted when a remote host is sending us a message, and we do not already have a session with them</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamNetworkingMessagesCallbacks + 1)]
public struct SteamNetworkingMessagesSessionRequest_t
{
	public const int k_iCallback = Constants.k_iSteamNetworkingMessagesCallbacks + 1;
	/// <summary>
	/// <para>user who wants to talk to us</para>
	/// </summary>
	public SteamNetworkingIdentity m_identityRemote;
}

/// <summary>
/// <para>Posted when we fail to establish a connection, or we detect that communications</para>
/// <para>have been disrupted it an unusual way.  There is no notification when a peer proactively</para>
/// <para>closes the session.  (&quot;Closed by peer&quot; is not a concept of UDP-style communications, and</para>
/// <para>SteamNetworkingMessages is primarily intended to make porting UDP code easy.)</para>
/// <para>Remember: callbacks are asynchronous.   See notes on SendMessageToUser,</para>
/// <para>and k_nSteamNetworkingSend_AutoRestartBrokenSession in particular.</para>
/// <para>Also, if a session times out due to inactivity, no callbacks will be posted.  The only</para>
/// <para>way to detect that this is happening is that querying the session state may return</para>
/// <para>none, connecting, and findingroute again.</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamNetworkingMessagesCallbacks + 2)]
public struct SteamNetworkingMessagesSessionFailed_t
{
	public const int k_iCallback = Constants.k_iSteamNetworkingMessagesCallbacks + 2;
	/// <summary>
	/// <para>Detailed info about the session that failed.</para>
	/// <para>SteamNetConnectionInfo_t::m_identityRemote indicates who this session</para>
	/// <para>was with.</para>
	/// </summary>
	public SteamNetConnectionInfo_t m_info;
}

// m_eOldState = k_ESteamNetworkingConnectionState_Connecting, and
// m_eOldState = k_ESteamNetworkingConnectionState_Connecting or k_ESteamNetworkingConnectionState_Connected,
/// <summary>
/// <para>Callback struct used to notify when a connection has changed state</para>
/// <para>This callback is posted whenever a connection is created, destroyed, or changes state.</para>
/// <para>The m_info field will contain a complete description of the connection at the time the</para>
/// <para>change occurred and the callback was posted.  In particular, m_eState will have the</para>
/// <para>new connection state.</para>
/// <para>You will usually need to listen for this callback to know when:</para>
/// <para>- A new connection arrives on a listen socket.</para>
/// <para>m_info.m_hListenSocket will be set, m_eOldState = k_ESteamNetworkingConnectionState_None,</para>
/// <para>and m_info.m_eState = k_ESteamNetworkingConnectionState_Connecting.</para>
/// <para>See ISteamNetworkigSockets::AcceptConnection.</para>
/// <para>- A connection you initiated has been accepted by the remote host.</para>
/// <para>m_info.m_eState = k_ESteamNetworkingConnectionState_Connected.</para>
/// <para>Some connections might transition to k_ESteamNetworkingConnectionState_FindingRoute first.</para>
/// <para>- A connection has been actively rejected or closed by the remote host.</para>
/// <para>and m_info.m_eState = k_ESteamNetworkingConnectionState_ClosedByPeer.  m_info.m_eEndReason</para>
/// <para>and m_info.m_szEndDebug will have for more details.</para>
/// <para>NOTE: upon receiving this callback, you must still destroy the connection using</para>
/// <para>ISteamNetworkingSockets::CloseConnection to free up local resources.  (The details</para>
/// <para>passed to the function are not used in this case, since the connection is already closed.)</para>
/// <para>- A problem was detected with the connection, and it has been closed by the local host.</para>
/// <para>The most common failure is timeout, but other configuration or authentication failures</para>
/// <para>can cause this.  m_eOldState = k_ESteamNetworkingConnectionState_Connecting or</para>
/// <para>k_ESteamNetworkingConnectionState_Connected, and m_info.m_eState = k_ESteamNetworkingConnectionState_ProblemDetectedLocally.</para>
/// <para>m_info.m_eEndReason and m_info.m_szEndDebug will have for more details.</para>
/// <para>NOTE: upon receiving this callback, you must still destroy the connection using</para>
/// <para>ISteamNetworkingSockets::CloseConnection to free up local resources.  (The details</para>
/// <para>passed to the function are not used in this case, since the connection is already closed.)</para>
/// <para>Remember that callbacks are posted to a queue, and networking connections can</para>
/// <para>change at any time.  It is possible that the connection has already changed</para>
/// <para>state by the time you process this callback.</para>
/// <para>Also note that callbacks will be posted when connections are created and destroyed by your own API calls.</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamNetworkingSocketsCallbacks + 1)]
public struct SteamNetConnectionStatusChangedCallback_t
{
	public const int k_iCallback = Constants.k_iSteamNetworkingSocketsCallbacks + 1;
	/// <summary>
	/// <para>Connection handle</para>
	/// </summary>
	public HSteamNetConnection m_hConn;
	/// <summary>
	/// <para>Full connection info</para>
	/// </summary>
	public SteamNetConnectionInfo_t m_info;
	/// <summary>
	/// <para>Previous state.  (Current state is in m_info.m_eState)</para>
	/// </summary>
	public ESteamNetworkingConnectionState m_eOldState;
}

/// <summary>
/// <para>A struct used to describe our readiness to participate in authenticated,</para>
/// <para>encrypted communication.  In order to do this we need:</para>
/// <para>- The list of trusted CA certificates that might be relevant for this</para>
/// <para>app.</para>
/// <para>- A valid certificate issued by a CA.</para>
/// <para>This callback is posted whenever the state of our readiness changes.</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamNetworkingSocketsCallbacks + 2)]
public unsafe struct SteamNetAuthenticationStatus_t
{
	public const int k_iCallback = Constants.k_iSteamNetworkingSocketsCallbacks + 2;
	/// <summary>
	/// <para>Status</para>
	/// </summary>
	public ESteamNetworkingAvailability m_eAvail;
	/// <summary>
	/// <para>Non-localized English language status.  For diagnostic/debugging</para>
	/// <para>purposes only.</para>
	/// </summary>
	private fixed byte m_debugMsg_[256];
	public string m_debugMsg
	{
		get
		{
			fixed (byte* ptr = m_debugMsg_)
			{
				var span = new ReadOnlySpan<byte>(ptr, 256);
				var length = span.IndexOf((byte)0);
				return Encoding.UTF8.GetString(length < 0 ? span : span[..length]);
			}
		}
		set
		{
			fixed (byte* ptr = m_debugMsg_)
			{
				var span = new Span<byte>(ptr, 256);
				span.Clear();
				var bytes = Encoding.UTF8.GetBytes(value ?? string.Empty);
				bytes.AsSpan(0, Math.Min(bytes.Length, 256 - 1)).CopyTo(span);
			}
		}
	}
}

/// <summary>
/// <para>A struct used to describe our readiness to use the relay network.</para>
/// <para>To do this we first need to fetch the network configuration,</para>
/// <para>which describes what POPs are available.</para>
/// </summary>
[CallbackIdentity(Constants.k_iSteamNetworkingUtilsCallbacks + 1)]
public unsafe struct SteamRelayNetworkStatus_t
{
	public const int k_iCallback = Constants.k_iSteamNetworkingUtilsCallbacks + 1;
	/// <summary>
	/// <para>Summary status.  When this is &quot;current&quot;, initialization has</para>
	/// <para>completed.  Anything else means you are not ready yet, or</para>
	/// <para>there is a significant problem.</para>
	/// </summary>
	public ESteamNetworkingAvailability m_eAvail;
	/// <summary>
	/// <para>Nonzero if latency measurement is in progress (or pending,</para>
	/// <para>awaiting a prerequisite).</para>
	/// </summary>
	public int m_bPingMeasurementInProgress;
	/// <summary>
	/// <para>Status obtaining the network config.  This is a prerequisite</para>
	/// <para>for relay network access.</para>
	/// <para>Failure to obtain the network config almost always indicates</para>
	/// <para>a problem with the local internet connection.</para>
	/// </summary>
	public ESteamNetworkingAvailability m_eAvailNetworkConfig;
	/// <summary>
	/// <para>Current ability to communicate with ANY relay.  Note that</para>
	/// <para>the complete failure to communicate with any relays almost</para>
	/// <para>always indicates a problem with the local Internet connection.</para>
	/// <para>(However, just because you can reach a single relay doesn&apos;t</para>
	/// <para>mean that the local connection is in perfect health.)</para>
	/// </summary>
	public ESteamNetworkingAvailability m_eAvailAnyRelay;
	/// <summary>
	/// <para>Non-localized English language status.  For diagnostic/debugging</para>
	/// <para>purposes only.</para>
	/// </summary>
	private fixed byte m_debugMsg_[256];
	public string m_debugMsg
	{
		get
		{
			fixed (byte* ptr = m_debugMsg_)
			{
				var span = new ReadOnlySpan<byte>(ptr, 256);
				var length = span.IndexOf((byte)0);
				return Encoding.UTF8.GetString(length < 0 ? span : span[..length]);
			}
		}
		set
		{
			fixed (byte* ptr = m_debugMsg_)
			{
				var span = new Span<byte>(ptr, 256);
				span.Clear();
				var bytes = Encoding.UTF8.GetBytes(value ?? string.Empty);
				bytes.AsSpan(0, Math.Min(bytes.Length, 256 - 1)).CopyTo(span);
			}
		}
	}
}

/// <summary>
/// <para>Callback for querying UGC</para>
/// </summary>
[CallbackIdentity(Constants.k_ISteamParentalSettingsCallbacks + 1)]
public struct SteamParentalSettingsChanged_t
{
	public const int k_iCallback = Constants.k_ISteamParentalSettingsCallbacks + 1;
}

/// <summary>
/// <para>callbacks</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamRemotePlayCallbacks + 1)]
public struct SteamRemotePlaySessionConnected_t
{
	public const int k_iCallback = Constants.k_iSteamRemotePlayCallbacks + 1;
	public RemotePlaySessionID_t m_unSessionID;
}

[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamRemotePlayCallbacks + 2)]
public struct SteamRemotePlaySessionDisconnected_t
{
	public const int k_iCallback = Constants.k_iSteamRemotePlayCallbacks + 2;
	public RemotePlaySessionID_t m_unSessionID;
}

[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamRemotePlayCallbacks + 3)]
public unsafe struct SteamRemotePlayTogetherGuestInvite_t
{
	public const int k_iCallback = Constants.k_iSteamRemotePlayCallbacks + 3;
	private fixed byte m_szConnectURL_[1024];
	public string m_szConnectURL
	{
		get
		{
			fixed (byte* ptr = m_szConnectURL_)
			{
				var span = new ReadOnlySpan<byte>(ptr, 1024);
				var length = span.IndexOf((byte)0);
				return Encoding.UTF8.GetString(length < 0 ? span : span[..length]);
			}
		}
		set
		{
			fixed (byte* ptr = m_szConnectURL_)
			{
				var span = new Span<byte>(ptr, 1024);
				span.Clear();
				var bytes = Encoding.UTF8.GetBytes(value ?? string.Empty);
				bytes.AsSpan(0, Math.Min(bytes.Length, 1024 - 1)).CopyTo(span);
			}
		}
	}
}

/// <summary>
/// <para>callbacks</para>
/// <para>The result of a call to FileShare()</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamRemoteStorageCallbacks + 7)]
public unsafe struct RemoteStorageFileShareResult_t
{
	public const int k_iCallback = Constants.k_iSteamRemoteStorageCallbacks + 7;
	/// <summary>
	/// <para>The result of the operation</para>
	/// </summary>
	public EResult m_eResult;
	/// <summary>
	/// <para>The handle that can be shared with users and features</para>
	/// </summary>
	public UGCHandle_t m_hFile;
	/// <summary>
	/// <para>The name of the file that was shared</para>
	/// </summary>
	private fixed byte m_rgchFilename_[Constants.k_cchFilenameMax];
	public string m_rgchFilename
	{
		get
		{
			fixed (byte* ptr = m_rgchFilename_)
			{
				var span = new ReadOnlySpan<byte>(ptr, Constants.k_cchFilenameMax);
				var length = span.IndexOf((byte)0);
				return Encoding.UTF8.GetString(length < 0 ? span : span[..length]);
			}
		}
		set
		{
			fixed (byte* ptr = m_rgchFilename_)
			{
				var span = new Span<byte>(ptr, Constants.k_cchFilenameMax);
				span.Clear();
				var bytes = Encoding.UTF8.GetBytes(value ?? string.Empty);
				bytes.AsSpan(0, Math.Min(bytes.Length, Constants.k_cchFilenameMax - 1)).CopyTo(span);
			}
		}
	}
}

/// <summary>
/// <para>k_iSteamRemoteStorageCallbacks + 8 is deprecated! Do not reuse</para>
/// <para>The result of a call to PublishFile()</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamRemoteStorageCallbacks + 9)]
public struct RemoteStoragePublishFileResult_t
{
	public const int k_iCallback = Constants.k_iSteamRemoteStorageCallbacks + 9;
	/// <summary>
	/// <para>The result of the operation.</para>
	/// </summary>
	public EResult m_eResult;
	public PublishedFileId_t m_nPublishedFileId;
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bUserNeedsToAcceptWorkshopLegalAgreement;
}

/// <summary>
/// <para>k_iSteamRemoteStorageCallbacks + 10 is deprecated! Do not reuse</para>
/// <para>The result of a call to DeletePublishedFile()</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamRemoteStorageCallbacks + 11)]
public struct RemoteStorageDeletePublishedFileResult_t
{
	public const int k_iCallback = Constants.k_iSteamRemoteStorageCallbacks + 11;
	/// <summary>
	/// <para>The result of the operation.</para>
	/// </summary>
	public EResult m_eResult;
	public PublishedFileId_t m_nPublishedFileId;
}

/// <summary>
/// <para>The result of a call to EnumerateUserPublishedFiles()</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamRemoteStorageCallbacks + 12)]
public struct RemoteStorageEnumerateUserPublishedFilesResult_t
{
	public const int k_iCallback = Constants.k_iSteamRemoteStorageCallbacks + 12;
	/// <summary>
	/// <para>The result of the operation.</para>
	/// </summary>
	public EResult m_eResult;
	public int m_nResultsReturned;
	public int m_nTotalResultCount;
	[InlineArray(Constants.k_unEnumeratePublishedFilesMaxResults)]
	public struct m_rgPublishedFileIdArray
	{
		private PublishedFileId_t _element0;
	}
	public m_rgPublishedFileIdArray m_rgPublishedFileId;
}

/// <summary>
/// <para>The result of a call to SubscribePublishedFile()</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamRemoteStorageCallbacks + 13)]
public struct RemoteStorageSubscribePublishedFileResult_t
{
	public const int k_iCallback = Constants.k_iSteamRemoteStorageCallbacks + 13;
	/// <summary>
	/// <para>The result of the operation.</para>
	/// </summary>
	public EResult m_eResult;
	public PublishedFileId_t m_nPublishedFileId;
}

/// <summary>
/// <para>The result of a call to EnumerateSubscribePublishedFiles()</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamRemoteStorageCallbacks + 14)]
public unsafe struct RemoteStorageEnumerateUserSubscribedFilesResult_t
{
	public const int k_iCallback = Constants.k_iSteamRemoteStorageCallbacks + 14;
	/// <summary>
	/// <para>The result of the operation.</para>
	/// </summary>
	public EResult m_eResult;
	public int m_nResultsReturned;
	public int m_nTotalResultCount;
	[InlineArray(Constants.k_unEnumeratePublishedFilesMaxResults)]
	public struct m_rgPublishedFileIdArray
	{
		private PublishedFileId_t _element0;
	}
	public m_rgPublishedFileIdArray m_rgPublishedFileId;
	public fixed uint m_rgRTimeSubscribed[Constants.k_unEnumeratePublishedFilesMaxResults];
}

/// <summary>
/// <para>The result of a call to UnsubscribePublishedFile()</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamRemoteStorageCallbacks + 15)]
public struct RemoteStorageUnsubscribePublishedFileResult_t
{
	public const int k_iCallback = Constants.k_iSteamRemoteStorageCallbacks + 15;
	/// <summary>
	/// <para>The result of the operation.</para>
	/// </summary>
	public EResult m_eResult;
	public PublishedFileId_t m_nPublishedFileId;
}

/// <summary>
/// <para>The result of a call to CommitPublishedFileUpdate()</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamRemoteStorageCallbacks + 16)]
public struct RemoteStorageUpdatePublishedFileResult_t
{
	public const int k_iCallback = Constants.k_iSteamRemoteStorageCallbacks + 16;
	/// <summary>
	/// <para>The result of the operation.</para>
	/// </summary>
	public EResult m_eResult;
	public PublishedFileId_t m_nPublishedFileId;
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bUserNeedsToAcceptWorkshopLegalAgreement;
}

/// <summary>
/// <para>The result of a call to UGCDownload()</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamRemoteStorageCallbacks + 17)]
public unsafe struct RemoteStorageDownloadUGCResult_t
{
	public const int k_iCallback = Constants.k_iSteamRemoteStorageCallbacks + 17;
	/// <summary>
	/// <para>The result of the operation.</para>
	/// </summary>
	public EResult m_eResult;
	/// <summary>
	/// <para>The handle to the file that was attempted to be downloaded.</para>
	/// </summary>
	public UGCHandle_t m_hFile;
	/// <summary>
	/// <para>ID of the app that created this file.</para>
	/// </summary>
	public AppId_t m_nAppID;
	/// <summary>
	/// <para>The size of the file that was downloaded, in bytes.</para>
	/// </summary>
	public int m_nSizeInBytes;
	/// <summary>
	/// <para>The name of the file that was downloaded.</para>
	/// </summary>
	private fixed byte m_pchFileName_[Constants.k_cchFilenameMax];
	public string m_pchFileName
	{
		get
		{
			fixed (byte* ptr = m_pchFileName_)
			{
				var span = new ReadOnlySpan<byte>(ptr, Constants.k_cchFilenameMax);
				var length = span.IndexOf((byte)0);
				return Encoding.UTF8.GetString(length < 0 ? span : span[..length]);
			}
		}
		set
		{
			fixed (byte* ptr = m_pchFileName_)
			{
				var span = new Span<byte>(ptr, Constants.k_cchFilenameMax);
				span.Clear();
				var bytes = Encoding.UTF8.GetBytes(value ?? string.Empty);
				bytes.AsSpan(0, Math.Min(bytes.Length, Constants.k_cchFilenameMax - 1)).CopyTo(span);
			}
		}
	}
	/// <summary>
	/// <para>Steam ID of the user who created this content.</para>
	/// </summary>
	public ulong m_ulSteamIDOwner;
}

/// <summary>
/// <para>The result of a call to GetPublishedFileDetails()</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamRemoteStorageCallbacks + 18)]
public unsafe struct RemoteStorageGetPublishedFileDetailsResult_t
{
	public const int k_iCallback = Constants.k_iSteamRemoteStorageCallbacks + 18;
	/// <summary>
	/// <para>The result of the operation.</para>
	/// </summary>
	public EResult m_eResult;
	public PublishedFileId_t m_nPublishedFileId;
	/// <summary>
	/// <para>ID of the app that created this file.</para>
	/// </summary>
	public AppId_t m_nCreatorAppID;
	/// <summary>
	/// <para>ID of the app that will consume this file.</para>
	/// </summary>
	public AppId_t m_nConsumerAppID;
	/// <summary>
	/// <para>title of document</para>
	/// </summary>
	private fixed byte m_rgchTitle_[Constants.k_cchPublishedDocumentTitleMax];
	public string m_rgchTitle
	{
		get
		{
			fixed (byte* ptr = m_rgchTitle_)
			{
				var span = new ReadOnlySpan<byte>(ptr, Constants.k_cchPublishedDocumentTitleMax);
				var length = span.IndexOf((byte)0);
				return Encoding.UTF8.GetString(length < 0 ? span : span[..length]);
			}
		}
		set
		{
			fixed (byte* ptr = m_rgchTitle_)
			{
				var span = new Span<byte>(ptr, Constants.k_cchPublishedDocumentTitleMax);
				span.Clear();
				var bytes = Encoding.UTF8.GetBytes(value ?? string.Empty);
				bytes.AsSpan(0, Math.Min(bytes.Length, Constants.k_cchPublishedDocumentTitleMax - 1)).CopyTo(span);
			}
		}
	}
	/// <summary>
	/// <para>description of document</para>
	/// </summary>
	private fixed byte m_rgchDescription_[Constants.k_cchPublishedDocumentDescriptionMax];
	public string m_rgchDescription
	{
		get
		{
			fixed (byte* ptr = m_rgchDescription_)
			{
				var span = new ReadOnlySpan<byte>(ptr, Constants.k_cchPublishedDocumentDescriptionMax);
				var length = span.IndexOf((byte)0);
				return Encoding.UTF8.GetString(length < 0 ? span : span[..length]);
			}
		}
		set
		{
			fixed (byte* ptr = m_rgchDescription_)
			{
				var span = new Span<byte>(ptr, Constants.k_cchPublishedDocumentDescriptionMax);
				span.Clear();
				var bytes = Encoding.UTF8.GetBytes(value ?? string.Empty);
				bytes.AsSpan(0, Math.Min(bytes.Length, Constants.k_cchPublishedDocumentDescriptionMax - 1)).CopyTo(span);
			}
		}
	}
	/// <summary>
	/// <para>The handle of the primary file</para>
	/// </summary>
	public UGCHandle_t m_hFile;
	/// <summary>
	/// <para>The handle of the preview file</para>
	/// </summary>
	public UGCHandle_t m_hPreviewFile;
	/// <summary>
	/// <para>Steam ID of the user who created this content.</para>
	/// </summary>
	public ulong m_ulSteamIDOwner;
	/// <summary>
	/// <para>time when the published file was created</para>
	/// </summary>
	public uint m_rtimeCreated;
	/// <summary>
	/// <para>time when the published file was last updated</para>
	/// </summary>
	public uint m_rtimeUpdated;
	public ERemoteStoragePublishedFileVisibility m_eVisibility;
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bBanned;
	/// <summary>
	/// <para>comma separated list of all tags associated with this file</para>
	/// </summary>
	private fixed byte m_rgchTags_[Constants.k_cchTagListMax];
	public string m_rgchTags
	{
		get
		{
			fixed (byte* ptr = m_rgchTags_)
			{
				var span = new ReadOnlySpan<byte>(ptr, Constants.k_cchTagListMax);
				var length = span.IndexOf((byte)0);
				return Encoding.UTF8.GetString(length < 0 ? span : span[..length]);
			}
		}
		set
		{
			fixed (byte* ptr = m_rgchTags_)
			{
				var span = new Span<byte>(ptr, Constants.k_cchTagListMax);
				span.Clear();
				var bytes = Encoding.UTF8.GetBytes(value ?? string.Empty);
				bytes.AsSpan(0, Math.Min(bytes.Length, Constants.k_cchTagListMax - 1)).CopyTo(span);
			}
		}
	}
	/// <summary>
	/// <para>whether the list of tags was too long to be returned in the provided buffer</para>
	/// </summary>
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bTagsTruncated;
	/// <summary>
	/// <para>The name of the primary file</para>
	/// </summary>
	private fixed byte m_pchFileName_[Constants.k_cchFilenameMax];
	public string m_pchFileName
	{
		get
		{
			fixed (byte* ptr = m_pchFileName_)
			{
				var span = new ReadOnlySpan<byte>(ptr, Constants.k_cchFilenameMax);
				var length = span.IndexOf((byte)0);
				return Encoding.UTF8.GetString(length < 0 ? span : span[..length]);
			}
		}
		set
		{
			fixed (byte* ptr = m_pchFileName_)
			{
				var span = new Span<byte>(ptr, Constants.k_cchFilenameMax);
				span.Clear();
				var bytes = Encoding.UTF8.GetBytes(value ?? string.Empty);
				bytes.AsSpan(0, Math.Min(bytes.Length, Constants.k_cchFilenameMax - 1)).CopyTo(span);
			}
		}
	}
	/// <summary>
	/// <para>Size of the primary file</para>
	/// </summary>
	public int m_nFileSize;
	/// <summary>
	/// <para>Size of the preview file</para>
	/// </summary>
	public int m_nPreviewFileSize;
	/// <summary>
	/// <para>URL (for a video or a website)</para>
	/// </summary>
	private fixed byte m_rgchURL_[Constants.k_cchPublishedFileURLMax];
	public string m_rgchURL
	{
		get
		{
			fixed (byte* ptr = m_rgchURL_)
			{
				var span = new ReadOnlySpan<byte>(ptr, Constants.k_cchPublishedFileURLMax);
				var length = span.IndexOf((byte)0);
				return Encoding.UTF8.GetString(length < 0 ? span : span[..length]);
			}
		}
		set
		{
			fixed (byte* ptr = m_rgchURL_)
			{
				var span = new Span<byte>(ptr, Constants.k_cchPublishedFileURLMax);
				span.Clear();
				var bytes = Encoding.UTF8.GetBytes(value ?? string.Empty);
				bytes.AsSpan(0, Math.Min(bytes.Length, Constants.k_cchPublishedFileURLMax - 1)).CopyTo(span);
			}
		}
	}
	/// <summary>
	/// <para>Type of the file</para>
	/// </summary>
	public EWorkshopFileType m_eFileType;
	/// <summary>
	/// <para>developer has specifically flagged this item as accepted in the Workshop</para>
	/// </summary>
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bAcceptedForUse;
}

[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamRemoteStorageCallbacks + 19)]
public unsafe struct RemoteStorageEnumerateWorkshopFilesResult_t
{
	public const int k_iCallback = Constants.k_iSteamRemoteStorageCallbacks + 19;
	public EResult m_eResult;
	public int m_nResultsReturned;
	public int m_nTotalResultCount;
	[InlineArray(Constants.k_unEnumeratePublishedFilesMaxResults)]
	public struct m_rgPublishedFileIdArray
	{
		private PublishedFileId_t _element0;
	}
	public m_rgPublishedFileIdArray m_rgPublishedFileId;
	public fixed float m_rgScore[Constants.k_unEnumeratePublishedFilesMaxResults];
	public AppId_t m_nAppId;
	public uint m_unStartIndex;
}

/// <summary>
/// <para>The result of GetPublishedItemVoteDetails</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamRemoteStorageCallbacks + 20)]
public struct RemoteStorageGetPublishedItemVoteDetailsResult_t
{
	public const int k_iCallback = Constants.k_iSteamRemoteStorageCallbacks + 20;
	public EResult m_eResult;
	public PublishedFileId_t m_unPublishedFileId;
	public int m_nVotesFor;
	public int m_nVotesAgainst;
	public int m_nReports;
	public float m_fScore;
}

/// <summary>
/// <para>User subscribed to a file for the app (from within the app or on the web)</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamRemoteStorageCallbacks + 21)]
public struct RemoteStoragePublishedFileSubscribed_t
{
	public const int k_iCallback = Constants.k_iSteamRemoteStorageCallbacks + 21;
	/// <summary>
	/// <para>The published file id</para>
	/// </summary>
	public PublishedFileId_t m_nPublishedFileId;
	/// <summary>
	/// <para>ID of the app that will consume this file.</para>
	/// </summary>
	public AppId_t m_nAppID;
}

/// <summary>
/// <para>User unsubscribed from a file for the app (from within the app or on the web)</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamRemoteStorageCallbacks + 22)]
public struct RemoteStoragePublishedFileUnsubscribed_t
{
	public const int k_iCallback = Constants.k_iSteamRemoteStorageCallbacks + 22;
	/// <summary>
	/// <para>The published file id</para>
	/// </summary>
	public PublishedFileId_t m_nPublishedFileId;
	/// <summary>
	/// <para>ID of the app that will consume this file.</para>
	/// </summary>
	public AppId_t m_nAppID;
}

/// <summary>
/// <para>Published file that a user owns was deleted (from within the app or the web)</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamRemoteStorageCallbacks + 23)]
public struct RemoteStoragePublishedFileDeleted_t
{
	public const int k_iCallback = Constants.k_iSteamRemoteStorageCallbacks + 23;
	/// <summary>
	/// <para>The published file id</para>
	/// </summary>
	public PublishedFileId_t m_nPublishedFileId;
	/// <summary>
	/// <para>ID of the app that will consume this file.</para>
	/// </summary>
	public AppId_t m_nAppID;
}

/// <summary>
/// <para>The result of a call to UpdateUserPublishedItemVote()</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamRemoteStorageCallbacks + 24)]
public struct RemoteStorageUpdateUserPublishedItemVoteResult_t
{
	public const int k_iCallback = Constants.k_iSteamRemoteStorageCallbacks + 24;
	/// <summary>
	/// <para>The result of the operation.</para>
	/// </summary>
	public EResult m_eResult;
	/// <summary>
	/// <para>The published file id</para>
	/// </summary>
	public PublishedFileId_t m_nPublishedFileId;
}

/// <summary>
/// <para>The result of a call to GetUserPublishedItemVoteDetails()</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamRemoteStorageCallbacks + 25)]
public struct RemoteStorageUserVoteDetails_t
{
	public const int k_iCallback = Constants.k_iSteamRemoteStorageCallbacks + 25;
	/// <summary>
	/// <para>The result of the operation.</para>
	/// </summary>
	public EResult m_eResult;
	/// <summary>
	/// <para>The published file id</para>
	/// </summary>
	public PublishedFileId_t m_nPublishedFileId;
	/// <summary>
	/// <para>what the user voted</para>
	/// </summary>
	public EWorkshopVote m_eVote;
}

[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamRemoteStorageCallbacks + 26)]
public struct RemoteStorageEnumerateUserSharedWorkshopFilesResult_t
{
	public const int k_iCallback = Constants.k_iSteamRemoteStorageCallbacks + 26;
	/// <summary>
	/// <para>The result of the operation.</para>
	/// </summary>
	public EResult m_eResult;
	public int m_nResultsReturned;
	public int m_nTotalResultCount;
	[InlineArray(Constants.k_unEnumeratePublishedFilesMaxResults)]
	public struct m_rgPublishedFileIdArray
	{
		private PublishedFileId_t _element0;
	}
	public m_rgPublishedFileIdArray m_rgPublishedFileId;
}

[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamRemoteStorageCallbacks + 27)]
public struct RemoteStorageSetUserPublishedFileActionResult_t
{
	public const int k_iCallback = Constants.k_iSteamRemoteStorageCallbacks + 27;
	/// <summary>
	/// <para>The result of the operation.</para>
	/// </summary>
	public EResult m_eResult;
	/// <summary>
	/// <para>The published file id</para>
	/// </summary>
	public PublishedFileId_t m_nPublishedFileId;
	/// <summary>
	/// <para>the action that was attempted</para>
	/// </summary>
	public EWorkshopFileAction m_eAction;
}

[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamRemoteStorageCallbacks + 28)]
public unsafe struct RemoteStorageEnumeratePublishedFilesByUserActionResult_t
{
	public const int k_iCallback = Constants.k_iSteamRemoteStorageCallbacks + 28;
	/// <summary>
	/// <para>The result of the operation.</para>
	/// </summary>
	public EResult m_eResult;
	/// <summary>
	/// <para>the action that was filtered on</para>
	/// </summary>
	public EWorkshopFileAction m_eAction;
	public int m_nResultsReturned;
	public int m_nTotalResultCount;
	[InlineArray(Constants.k_unEnumeratePublishedFilesMaxResults)]
	public struct m_rgPublishedFileIdArray
	{
		private PublishedFileId_t _element0;
	}
	public m_rgPublishedFileIdArray m_rgPublishedFileId;
	public fixed uint m_rgRTimeUpdated[Constants.k_unEnumeratePublishedFilesMaxResults];
}

/// <summary>
/// <para>Called periodically while a PublishWorkshopFile is in progress</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamRemoteStorageCallbacks + 29)]
public struct RemoteStoragePublishFileProgress_t
{
	public const int k_iCallback = Constants.k_iSteamRemoteStorageCallbacks + 29;
	public double m_dPercentFile;
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bPreview;
}

/// <summary>
/// <para>Called when the content for a published file is updated</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamRemoteStorageCallbacks + 30)]
public struct RemoteStoragePublishedFileUpdated_t
{
	public const int k_iCallback = Constants.k_iSteamRemoteStorageCallbacks + 30;
	/// <summary>
	/// <para>The published file id</para>
	/// </summary>
	public PublishedFileId_t m_nPublishedFileId;
	/// <summary>
	/// <para>ID of the app that will consume this file.</para>
	/// </summary>
	public AppId_t m_nAppID;
	/// <summary>
	/// <para>not used anymore</para>
	/// </summary>
	public ulong m_ulUnused;
}

/// <summary>
/// <para>Called when a FileWriteAsync completes</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamRemoteStorageCallbacks + 31)]
public struct RemoteStorageFileWriteAsyncComplete_t
{
	public const int k_iCallback = Constants.k_iSteamRemoteStorageCallbacks + 31;
	/// <summary>
	/// <para>result</para>
	/// </summary>
	public EResult m_eResult;
}

/// <summary>
/// <para>Called when a FileReadAsync completes</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamRemoteStorageCallbacks + 32)]
public struct RemoteStorageFileReadAsyncComplete_t
{
	public const int k_iCallback = Constants.k_iSteamRemoteStorageCallbacks + 32;
	/// <summary>
	/// <para>call handle of the async read which was made</para>
	/// </summary>
	public SteamAPICall_t m_hFileReadAsync;
	/// <summary>
	/// <para>result</para>
	/// </summary>
	public EResult m_eResult;
	/// <summary>
	/// <para>offset in the file this read was at</para>
	/// </summary>
	public uint m_nOffset;
	/// <summary>
	/// <para>amount read - will the &lt;= the amount requested</para>
	/// </summary>
	public uint m_cubRead;
}

/// <summary>
/// <para>one or more files for this app have changed locally after syncing</para>
/// <para>to remote session changes</para>
/// <para>Note: only posted if this happens DURING the local app session</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value, Size = 1)]
[CallbackIdentity(Constants.k_iSteamRemoteStorageCallbacks + 33)]
public struct RemoteStorageLocalFileChange_t
{
	public const int k_iCallback = Constants.k_iSteamRemoteStorageCallbacks + 33;
}

/// <summary>
/// <para>callbacks</para>
/// <para>Screenshot successfully written or otherwise added to the library</para>
/// <para>and can now be tagged</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamScreenshotsCallbacks + 1)]
public struct ScreenshotReady_t
{
	public const int k_iCallback = Constants.k_iSteamScreenshotsCallbacks + 1;
	public ScreenshotHandle m_hLocal;
	public EResult m_eResult;
}

/// <summary>
/// <para>Screenshot has been requested by the user.  Only sent if</para>
/// <para>HookScreenshots() has been called, in which case Steam will not take</para>
/// <para>the screenshot itself.</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value, Size = 1)]
[CallbackIdentity(Constants.k_iSteamScreenshotsCallbacks + 2)]
public struct ScreenshotRequested_t
{
	public const int k_iCallback = Constants.k_iSteamScreenshotsCallbacks + 2;
}

/// <summary>
/// <para>Callback for querying UGC</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamTimelineCallbacks + 1)]
public unsafe struct SteamTimelineGamePhaseRecordingExists_t
{
	public const int k_iCallback = Constants.k_iSteamTimelineCallbacks + 1;
	private fixed byte m_rgchPhaseID_[Constants.k_cchMaxPhaseIDLength];
	public string m_rgchPhaseID
	{
		get
		{
			fixed (byte* ptr = m_rgchPhaseID_)
			{
				var span = new ReadOnlySpan<byte>(ptr, Constants.k_cchMaxPhaseIDLength);
				var length = span.IndexOf((byte)0);
				return Encoding.UTF8.GetString(length < 0 ? span : span[..length]);
			}
		}
		set
		{
			fixed (byte* ptr = m_rgchPhaseID_)
			{
				var span = new Span<byte>(ptr, Constants.k_cchMaxPhaseIDLength);
				span.Clear();
				var bytes = Encoding.UTF8.GetBytes(value ?? string.Empty);
				bytes.AsSpan(0, Math.Min(bytes.Length, Constants.k_cchMaxPhaseIDLength - 1)).CopyTo(span);
			}
		}
	}
	public ulong m_ulRecordingMS;
	public ulong m_ulLongestClipMS;
	public uint m_unClipCount;
	public uint m_unScreenshotCount;
}

/// <summary>
/// <para>Callback for querying UGC</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamTimelineCallbacks + 2)]
public struct SteamTimelineEventRecordingExists_t
{
	public const int k_iCallback = Constants.k_iSteamTimelineCallbacks + 2;
	public ulong m_ulEventID;
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bRecordingExists;
}

/// <summary>
/// <para>Callback for querying UGC</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamUGCCallbacks + 1)]
public unsafe struct SteamUGCQueryCompleted_t
{
	public const int k_iCallback = Constants.k_iSteamUGCCallbacks + 1;
	public UGCQueryHandle_t m_handle;
	public EResult m_eResult;
	public uint m_unNumResultsReturned;
	public uint m_unTotalMatchingResults;
	/// <summary>
	/// <para>indicates whether this data was retrieved from the local on-disk cache</para>
	/// </summary>
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bCachedData;
	/// <summary>
	/// <para>If a paging cursor was used, then this will be the next cursor to get the next result set.</para>
	/// </summary>
	private fixed byte m_rgchNextCursor_[Constants.k_cchPublishedFileURLMax];
	public string m_rgchNextCursor
	{
		get
		{
			fixed (byte* ptr = m_rgchNextCursor_)
			{
				var span = new ReadOnlySpan<byte>(ptr, Constants.k_cchPublishedFileURLMax);
				var length = span.IndexOf((byte)0);
				return Encoding.UTF8.GetString(length < 0 ? span : span[..length]);
			}
		}
		set
		{
			fixed (byte* ptr = m_rgchNextCursor_)
			{
				var span = new Span<byte>(ptr, Constants.k_cchPublishedFileURLMax);
				span.Clear();
				var bytes = Encoding.UTF8.GetBytes(value ?? string.Empty);
				bytes.AsSpan(0, Math.Min(bytes.Length, Constants.k_cchPublishedFileURLMax - 1)).CopyTo(span);
			}
		}
	}
}

/// <summary>
/// <para>Callback for requesting details on one piece of UGC</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamUGCCallbacks + 2)]
public struct SteamUGCRequestUGCDetailsResult_t
{
	public const int k_iCallback = Constants.k_iSteamUGCCallbacks + 2;
	public SteamUGCDetails_t m_details;
	/// <summary>
	/// <para>indicates whether this data was retrieved from the local on-disk cache</para>
	/// </summary>
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bCachedData;
}

/// <summary>
/// <para>result for ISteamUGC::CreateItem()</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamUGCCallbacks + 3)]
public struct CreateItemResult_t
{
	public const int k_iCallback = Constants.k_iSteamUGCCallbacks + 3;
	public EResult m_eResult;
	/// <summary>
	/// <para>new item got this UGC PublishFileID</para>
	/// </summary>
	public PublishedFileId_t m_nPublishedFileId;
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bUserNeedsToAcceptWorkshopLegalAgreement;
}

/// <summary>
/// <para>result for ISteamUGC::SubmitItemUpdate()</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamUGCCallbacks + 4)]
public struct SubmitItemUpdateResult_t
{
	public const int k_iCallback = Constants.k_iSteamUGCCallbacks + 4;
	public EResult m_eResult;
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bUserNeedsToAcceptWorkshopLegalAgreement;
	public PublishedFileId_t m_nPublishedFileId;
}

/// <summary>
/// <para>a Workshop item has been installed or updated</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamUGCCallbacks + 5)]
public struct ItemInstalled_t
{
	public const int k_iCallback = Constants.k_iSteamUGCCallbacks + 5;
	public AppId_t m_unAppID;
	public PublishedFileId_t m_nPublishedFileId;
	public UGCHandle_t m_hLegacyContent;
	public ulong m_unManifestID;
}

/// <summary>
/// <para>result of DownloadItem(), existing item files can be accessed again</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamUGCCallbacks + 6)]
public struct DownloadItemResult_t
{
	public const int k_iCallback = Constants.k_iSteamUGCCallbacks + 6;
	public AppId_t m_unAppID;
	public PublishedFileId_t m_nPublishedFileId;
	public EResult m_eResult;
}

/// <summary>
/// <para>result of AddItemToFavorites() or RemoveItemFromFavorites()</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamUGCCallbacks + 7)]
public struct UserFavoriteItemsListChanged_t
{
	public const int k_iCallback = Constants.k_iSteamUGCCallbacks + 7;
	public PublishedFileId_t m_nPublishedFileId;
	public EResult m_eResult;
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bWasAddRequest;
}

/// <summary>
/// <para>The result of a call to SetUserItemVote()</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamUGCCallbacks + 8)]
public struct SetUserItemVoteResult_t
{
	public const int k_iCallback = Constants.k_iSteamUGCCallbacks + 8;
	public PublishedFileId_t m_nPublishedFileId;
	public EResult m_eResult;
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bVoteUp;
}

/// <summary>
/// <para>The result of a call to GetUserItemVote()</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamUGCCallbacks + 9)]
public struct GetUserItemVoteResult_t
{
	public const int k_iCallback = Constants.k_iSteamUGCCallbacks + 9;
	public PublishedFileId_t m_nPublishedFileId;
	public EResult m_eResult;
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bVotedUp;
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bVotedDown;
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bVoteSkipped;
}

/// <summary>
/// <para>The result of a call to StartPlaytimeTracking()</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamUGCCallbacks + 10)]
public struct StartPlaytimeTrackingResult_t
{
	public const int k_iCallback = Constants.k_iSteamUGCCallbacks + 10;
	public EResult m_eResult;
}

/// <summary>
/// <para>The result of a call to StopPlaytimeTracking()</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamUGCCallbacks + 11)]
public struct StopPlaytimeTrackingResult_t
{
	public const int k_iCallback = Constants.k_iSteamUGCCallbacks + 11;
	public EResult m_eResult;
}

/// <summary>
/// <para>The result of a call to AddDependency</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamUGCCallbacks + 12)]
public struct AddUGCDependencyResult_t
{
	public const int k_iCallback = Constants.k_iSteamUGCCallbacks + 12;
	public EResult m_eResult;
	public PublishedFileId_t m_nPublishedFileId;
	public PublishedFileId_t m_nChildPublishedFileId;
}

/// <summary>
/// <para>The result of a call to RemoveDependency</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamUGCCallbacks + 13)]
public struct RemoveUGCDependencyResult_t
{
	public const int k_iCallback = Constants.k_iSteamUGCCallbacks + 13;
	public EResult m_eResult;
	public PublishedFileId_t m_nPublishedFileId;
	public PublishedFileId_t m_nChildPublishedFileId;
}

/// <summary>
/// <para>The result of a call to AddAppDependency</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamUGCCallbacks + 14)]
public struct AddAppDependencyResult_t
{
	public const int k_iCallback = Constants.k_iSteamUGCCallbacks + 14;
	public EResult m_eResult;
	public PublishedFileId_t m_nPublishedFileId;
	public AppId_t m_nAppID;
}

/// <summary>
/// <para>The result of a call to RemoveAppDependency</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamUGCCallbacks + 15)]
public struct RemoveAppDependencyResult_t
{
	public const int k_iCallback = Constants.k_iSteamUGCCallbacks + 15;
	public EResult m_eResult;
	public PublishedFileId_t m_nPublishedFileId;
	public AppId_t m_nAppID;
}

/// <summary>
/// <para>The result of a call to GetAppDependencies.  Callback may be called</para>
/// <para>multiple times until all app dependencies have been returned.</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamUGCCallbacks + 16)]
public struct GetAppDependenciesResult_t
{
	public const int k_iCallback = Constants.k_iSteamUGCCallbacks + 16;
	public EResult m_eResult;
	public PublishedFileId_t m_nPublishedFileId;
	[InlineArray(32)]
	public struct m_rgAppIDsArray
	{
		private AppId_t _element0;
	}
	public m_rgAppIDsArray m_rgAppIDs;
	/// <summary>
	/// <para>number returned in this struct</para>
	/// </summary>
	public uint m_nNumAppDependencies;
	/// <summary>
	/// <para>total found</para>
	/// </summary>
	public uint m_nTotalNumAppDependencies;
}

/// <summary>
/// <para>The result of a call to DeleteItem</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamUGCCallbacks + 17)]
public struct DeleteItemResult_t
{
	public const int k_iCallback = Constants.k_iSteamUGCCallbacks + 17;
	public EResult m_eResult;
	public PublishedFileId_t m_nPublishedFileId;
}

/// <summary>
/// <para>signal that the list of subscribed items changed</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamUGCCallbacks + 18)]
public struct UserSubscribedItemsListChanged_t
{
	public const int k_iCallback = Constants.k_iSteamUGCCallbacks + 18;
	public AppId_t m_nAppID;
}

/// <summary>
/// <para>Status of the user&apos;s acceptable/rejection of the app&apos;s specific Workshop EULA</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamUGCCallbacks + 20)]
public struct WorkshopEULAStatus_t
{
	public const int k_iCallback = Constants.k_iSteamUGCCallbacks + 20;
	public EResult m_eResult;
	public AppId_t m_nAppID;
	public uint m_unVersion;
	public RTime32 m_rtAction;
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bAccepted;
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bNeedsAction;
}

/// <summary>
/// <para>callbacks</para>
/// <para>Called when an authenticated connection to the Steam back-end has been established.</para>
/// <para>This means the Steam client now has a working connection to the Steam servers.</para>
/// <para>Usually this will have occurred before the game has launched, and should</para>
/// <para>only be seen if the user has dropped connection due to a networking issue</para>
/// <para>or a Steam server update.</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value, Size = 1)]
[CallbackIdentity(Constants.k_iSteamUserCallbacks + 1)]
public struct SteamServersConnected_t
{
	public const int k_iCallback = Constants.k_iSteamUserCallbacks + 1;
}

/// <summary>
/// <para>called when a connection attempt has failed</para>
/// <para>this will occur periodically if the Steam client is not connected,</para>
/// <para>and has failed in it&apos;s retry to establish a connection</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamUserCallbacks + 2)]
public struct SteamServerConnectFailure_t
{
	public const int k_iCallback = Constants.k_iSteamUserCallbacks + 2;
	public EResult m_eResult;
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bStillRetrying;
}

/// <summary>
/// <para>called if the client has lost connection to the Steam servers</para>
/// <para>real-time services will be disabled until a matching SteamServersConnected_t has been posted</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamUserCallbacks + 3)]
public struct SteamServersDisconnected_t
{
	public const int k_iCallback = Constants.k_iSteamUserCallbacks + 3;
	public EResult m_eResult;
}

/// <summary>
/// <para>Sent by the Steam server to the client telling it to disconnect from the specified game server,</para>
/// <para>which it may be in the process of or already connected to.</para>
/// <para>The game client should immediately disconnect upon receiving this message.</para>
/// <para>This can usually occur if the user doesn&apos;t have rights to play on the game server.</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamUserCallbacks + 13)]
public struct ClientGameServerDeny_t
{
	public const int k_iCallback = Constants.k_iSteamUserCallbacks + 13;
	public uint m_uAppID;
	public uint m_unGameServerIP;
	public ushort m_usGameServerPort;
	public ushort m_bSecure;
	public uint m_uReason;
}

/// <summary>
/// <para>called when the callback system for this client is in an error state (and has flushed pending callbacks)</para>
/// <para>When getting this message the client should disconnect from Steam, reset any stored Steam state and reconnect.</para>
/// <para>This usually occurs in the rare event the Steam client has some kind of fatal error.</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamUserCallbacks + 17)]
public struct IPCFailure_t
{
	public const int k_iCallback = Constants.k_iSteamUserCallbacks + 17;
	public byte m_eFailureType;
}

/// <summary>
/// <para>Signaled whenever licenses change</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value, Size = 1)]
[CallbackIdentity(Constants.k_iSteamUserCallbacks + 25)]
public struct LicensesUpdated_t
{
	public const int k_iCallback = Constants.k_iSteamUserCallbacks + 25;
}

/// <summary>
/// <para>callback for BeginAuthSession</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 4)]
[CallbackIdentity(Constants.k_iSteamUserCallbacks + 43)]
public struct ValidateAuthTicketResponse_t
{
	public const int k_iCallback = Constants.k_iSteamUserCallbacks + 43;
	public CSteamID m_SteamID;
	public EAuthSessionResponse m_eAuthSessionResponse;
	/// <summary>
	/// <para>different from m_SteamID if borrowed</para>
	/// </summary>
	public CSteamID m_OwnerSteamID;
}

/// <summary>
/// <para>called when a user has responded to a microtransaction authorization request</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamUserCallbacks + 52)]
public struct MicroTxnAuthorizationResponse_t
{
	public const int k_iCallback = Constants.k_iSteamUserCallbacks + 52;
	/// <summary>
	/// <para>AppID for this microtransaction</para>
	/// </summary>
	public uint m_unAppID;
	/// <summary>
	/// <para>OrderID provided for the microtransaction</para>
	/// </summary>
	public ulong m_ulOrderID;
	/// <summary>
	/// <para>if user authorized transaction</para>
	/// </summary>
	public byte m_bAuthorized;
}

/// <summary>
/// <para>callback for GetAuthSessionTicket</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamUserCallbacks + 63)]
public struct GetAuthSessionTicketResponse_t
{
	public const int k_iCallback = Constants.k_iSteamUserCallbacks + 63;
	public HAuthTicket m_hAuthTicket;
	public EResult m_eResult;
}

/// <summary>
/// <para>sent to your game in response to a steam://gamewebcallback/ command</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamUserCallbacks + 64)]
public unsafe struct GameWebCallback_t
{
	public const int k_iCallback = Constants.k_iSteamUserCallbacks + 64;
	private fixed byte m_szURL_[256];
	public string m_szURL
	{
		get
		{
			fixed (byte* ptr = m_szURL_)
			{
				var span = new ReadOnlySpan<byte>(ptr, 256);
				var length = span.IndexOf((byte)0);
				return Encoding.UTF8.GetString(length < 0 ? span : span[..length]);
			}
		}
		set
		{
			fixed (byte* ptr = m_szURL_)
			{
				var span = new Span<byte>(ptr, 256);
				span.Clear();
				var bytes = Encoding.UTF8.GetBytes(value ?? string.Empty);
				bytes.AsSpan(0, Math.Min(bytes.Length, 256 - 1)).CopyTo(span);
			}
		}
	}
}

/// <summary>
/// <para>sent to your game in response to ISteamUser::RequestStoreAuthURL</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamUserCallbacks + 65)]
public unsafe struct StoreAuthURLResponse_t
{
	public const int k_iCallback = Constants.k_iSteamUserCallbacks + 65;
	private fixed byte m_szURL_[512];
	public string m_szURL
	{
		get
		{
			fixed (byte* ptr = m_szURL_)
			{
				var span = new ReadOnlySpan<byte>(ptr, 512);
				var length = span.IndexOf((byte)0);
				return Encoding.UTF8.GetString(length < 0 ? span : span[..length]);
			}
		}
		set
		{
			fixed (byte* ptr = m_szURL_)
			{
				var span = new Span<byte>(ptr, 512);
				span.Clear();
				var bytes = Encoding.UTF8.GetBytes(value ?? string.Empty);
				bytes.AsSpan(0, Math.Min(bytes.Length, 512 - 1)).CopyTo(span);
			}
		}
	}
}

/// <summary>
/// <para>sent in response to ISteamUser::GetMarketEligibility</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamUserCallbacks + 66)]
public struct MarketEligibilityResponse_t
{
	public const int k_iCallback = Constants.k_iSteamUserCallbacks + 66;
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bAllowed;
	public EMarketNotAllowedReasonFlags m_eNotAllowedReason;
	public RTime32 m_rtAllowedAtTime;
	/// <summary>
	/// <para>The number of days any user is required to have had Steam Guard before they can use the market</para>
	/// </summary>
	public int m_cdaySteamGuardRequiredDays;
	/// <summary>
	/// <para>The number of days after initial device authorization a user must wait before using the market on that device</para>
	/// </summary>
	public int m_cdayNewDeviceCooldown;
}

/// <summary>
/// <para>sent for games with enabled anti indulgence / duration control, for</para>
/// <para>enabled users. Lets the game know whether the user can keep playing or</para>
/// <para>whether the game should exit, and returns info about remaining gameplay time.</para>
/// <para>This callback is fired asynchronously in response to timers triggering.</para>
/// <para>It is also fired in response to calls to GetDurationControl().</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamUserCallbacks + 67)]
public struct DurationControl_t
{
	public const int k_iCallback = Constants.k_iSteamUserCallbacks + 67;
	/// <summary>
	/// <para>result of call (always k_EResultOK for asynchronous timer-based notifications)</para>
	/// </summary>
	public EResult m_eResult;
	/// <summary>
	/// <para>appid generating playtime</para>
	/// </summary>
	public AppId_t m_appid;
	/// <summary>
	/// <para>is duration control applicable to user + game combination</para>
	/// </summary>
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bApplicable;
	/// <summary>
	/// <para>playtime since most recent 5 hour gap in playtime, only counting up to regulatory limit of playtime, in seconds</para>
	/// </summary>
	public int m_csecsLast5h;
	/// <summary>
	/// <para>recommended progress (either everything is fine, or please exit game)</para>
	/// </summary>
	public EDurationControlProgress m_progress;
	/// <summary>
	/// <para>notification to show, if any (always k_EDurationControlNotification_None for API calls)</para>
	/// </summary>
	public EDurationControlNotification m_notification;
	/// <summary>
	/// <para>playtime on current calendar day</para>
	/// </summary>
	public int m_csecsToday;
	/// <summary>
	/// <para>playtime remaining until the user hits a regulatory limit</para>
	/// </summary>
	public int m_csecsRemaining;
}

/// <summary>
/// <para>callback for GetTicketForWebApi</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamUserCallbacks + 68)]
public unsafe struct GetTicketForWebApiResponse_t
{
	public const int k_iCallback = Constants.k_iSteamUserCallbacks + 68;
	public HAuthTicket m_hAuthTicket;
	public EResult m_eResult;
	public int m_cubTicket;
	public fixed byte m_rgubTicket[Constants.k_nCubTicketMaxLength];
}

/// <summary>
/// <para>callbacks</para>
/// <para>called when the latests stats and achievements have been received</para>
/// <para>from the server</para>
/// </summary>
[StructLayout(LayoutKind.Explicit, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamUserStatsCallbacks + 1)]
public struct UserStatsReceived_t
{
	public const int k_iCallback = Constants.k_iSteamUserStatsCallbacks + 1;
	/// <summary>
	/// <para>Game these stats are for</para>
	/// </summary>
	[FieldOffset(0)]
	public ulong m_nGameID;
	/// <summary>
	/// <para>Success / error fetching the stats</para>
	/// </summary>
	[FieldOffset(8)]
	public EResult m_eResult;
	/// <summary>
	/// <para>The user for whom the stats are retrieved for</para>
	/// </summary>
	[FieldOffset(12)]
	public CSteamID m_steamIDUser;
}

/// <summary>
/// <para>result of a request to store the user stats for a game</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamUserStatsCallbacks + 2)]
public struct UserStatsStored_t
{
	public const int k_iCallback = Constants.k_iSteamUserStatsCallbacks + 2;
	/// <summary>
	/// <para>Game these stats are for</para>
	/// </summary>
	public ulong m_nGameID;
	/// <summary>
	/// <para>success / error</para>
	/// </summary>
	public EResult m_eResult;
}

/// <summary>
/// <para>result of a request to store the achievements for a game, or an</para>
/// <para>&quot;indicate progress&quot; call. If both m_nCurProgress and m_nMaxProgress</para>
/// <para>are zero, that means the achievement has been fully unlocked.</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamUserStatsCallbacks + 3)]
public unsafe struct UserAchievementStored_t
{
	public const int k_iCallback = Constants.k_iSteamUserStatsCallbacks + 3;
	/// <summary>
	/// <para>Game this is for</para>
	/// </summary>
	public ulong m_nGameID;
	/// <summary>
	/// <para>if this is a &quot;group&quot; achievement</para>
	/// </summary>
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bGroupAchievement;
	/// <summary>
	/// <para>name of the achievement</para>
	/// </summary>
	private fixed byte m_rgchAchievementName_[Constants.k_cchStatNameMax];
	public string m_rgchAchievementName
	{
		get
		{
			fixed (byte* ptr = m_rgchAchievementName_)
			{
				var span = new ReadOnlySpan<byte>(ptr, Constants.k_cchStatNameMax);
				var length = span.IndexOf((byte)0);
				return Encoding.UTF8.GetString(length < 0 ? span : span[..length]);
			}
		}
		set
		{
			fixed (byte* ptr = m_rgchAchievementName_)
			{
				var span = new Span<byte>(ptr, Constants.k_cchStatNameMax);
				span.Clear();
				var bytes = Encoding.UTF8.GetBytes(value ?? string.Empty);
				bytes.AsSpan(0, Math.Min(bytes.Length, Constants.k_cchStatNameMax - 1)).CopyTo(span);
			}
		}
	}
	/// <summary>
	/// <para>current progress towards the achievement</para>
	/// </summary>
	public uint m_nCurProgress;
	/// <summary>
	/// <para>&quot;out of&quot; this many</para>
	/// </summary>
	public uint m_nMaxProgress;
}

/// <summary>
/// <para>call result for finding a leaderboard, returned as a result of FindOrCreateLeaderboard() or FindLeaderboard()</para>
/// <para>use CCallResult&lt;&gt; to map this async result to a member function</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamUserStatsCallbacks + 4)]
public struct LeaderboardFindResult_t
{
	public const int k_iCallback = Constants.k_iSteamUserStatsCallbacks + 4;
	/// <summary>
	/// <para>handle to the leaderboard serarched for, 0 if no leaderboard found</para>
	/// </summary>
	public SteamLeaderboard_t m_hSteamLeaderboard;
	/// <summary>
	/// <para>0 if no leaderboard found</para>
	/// </summary>
	public byte m_bLeaderboardFound;
}

/// <summary>
/// <para>call result indicating scores for a leaderboard have been downloaded and are ready to be retrieved, returned as a result of DownloadLeaderboardEntries()</para>
/// <para>use CCallResult&lt;&gt; to map this async result to a member function</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamUserStatsCallbacks + 5)]
public struct LeaderboardScoresDownloaded_t
{
	public const int k_iCallback = Constants.k_iSteamUserStatsCallbacks + 5;
	public SteamLeaderboard_t m_hSteamLeaderboard;
	/// <summary>
	/// <para>the handle to pass into GetDownloadedLeaderboardEntries()</para>
	/// </summary>
	public SteamLeaderboardEntries_t m_hSteamLeaderboardEntries;
	/// <summary>
	/// <para>the number of entries downloaded</para>
	/// </summary>
	public int m_cEntryCount;
}

/// <summary>
/// <para>call result indicating scores has been uploaded, returned as a result of UploadLeaderboardScore()</para>
/// <para>use CCallResult&lt;&gt; to map this async result to a member function</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamUserStatsCallbacks + 6)]
public struct LeaderboardScoreUploaded_t
{
	public const int k_iCallback = Constants.k_iSteamUserStatsCallbacks + 6;
	/// <summary>
	/// <para>1 if the call was successful</para>
	/// </summary>
	public byte m_bSuccess;
	/// <summary>
	/// <para>the leaderboard handle that was</para>
	/// </summary>
	public SteamLeaderboard_t m_hSteamLeaderboard;
	/// <summary>
	/// <para>the score that was attempted to set</para>
	/// </summary>
	public int m_nScore;
	/// <summary>
	/// <para>true if the score in the leaderboard change, false if the existing score was better</para>
	/// </summary>
	public byte m_bScoreChanged;
	/// <summary>
	/// <para>the new global rank of the user in this leaderboard</para>
	/// </summary>
	public int m_nGlobalRankNew;
	/// <summary>
	/// <para>the previous global rank of the user in this leaderboard; 0 if the user had no existing entry in the leaderboard</para>
	/// </summary>
	public int m_nGlobalRankPrevious;
}

[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamUserStatsCallbacks + 7)]
public struct NumberOfCurrentPlayers_t
{
	public const int k_iCallback = Constants.k_iSteamUserStatsCallbacks + 7;
	/// <summary>
	/// <para>1 if the call was successful</para>
	/// </summary>
	public byte m_bSuccess;
	/// <summary>
	/// <para>Number of players currently playing</para>
	/// </summary>
	public int m_cPlayers;
}

/// <summary>
/// <para>Callback indicating that a user&apos;s stats have been unloaded.</para>
/// <para>Call RequestUserStats again to access stats for this user</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamUserStatsCallbacks + 8)]
public struct UserStatsUnloaded_t
{
	public const int k_iCallback = Constants.k_iSteamUserStatsCallbacks + 8;
	/// <summary>
	/// <para>User whose stats have been unloaded</para>
	/// </summary>
	public CSteamID m_steamIDUser;
}

/// <summary>
/// <para>Callback indicating that an achievement icon has been fetched</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamUserStatsCallbacks + 9)]
public unsafe struct UserAchievementIconFetched_t
{
	public const int k_iCallback = Constants.k_iSteamUserStatsCallbacks + 9;
	/// <summary>
	/// <para>Game this is for</para>
	/// </summary>
	public CGameID m_nGameID;
	/// <summary>
	/// <para>name of the achievement</para>
	/// </summary>
	private fixed byte m_rgchAchievementName_[Constants.k_cchStatNameMax];
	public string m_rgchAchievementName
	{
		get
		{
			fixed (byte* ptr = m_rgchAchievementName_)
			{
				var span = new ReadOnlySpan<byte>(ptr, Constants.k_cchStatNameMax);
				var length = span.IndexOf((byte)0);
				return Encoding.UTF8.GetString(length < 0 ? span : span[..length]);
			}
		}
		set
		{
			fixed (byte* ptr = m_rgchAchievementName_)
			{
				var span = new Span<byte>(ptr, Constants.k_cchStatNameMax);
				span.Clear();
				var bytes = Encoding.UTF8.GetBytes(value ?? string.Empty);
				bytes.AsSpan(0, Math.Min(bytes.Length, Constants.k_cchStatNameMax - 1)).CopyTo(span);
			}
		}
	}
	/// <summary>
	/// <para>Is the icon for the achieved or not achieved version?</para>
	/// </summary>
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bAchieved;
	/// <summary>
	/// <para>Handle to the image, which can be used in SteamUtils()-&gt;GetImageRGBA(), 0 means no image is set for the achievement</para>
	/// </summary>
	public int m_nIconHandle;
}

/// <summary>
/// <para>Callback indicating that global achievement percentages are fetched</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamUserStatsCallbacks + 10)]
public struct GlobalAchievementPercentagesReady_t
{
	public const int k_iCallback = Constants.k_iSteamUserStatsCallbacks + 10;
	/// <summary>
	/// <para>Game this is for</para>
	/// </summary>
	public ulong m_nGameID;
	/// <summary>
	/// <para>Result of the operation</para>
	/// </summary>
	public EResult m_eResult;
}

/// <summary>
/// <para>call result indicating UGC has been uploaded, returned as a result of SetLeaderboardUGC()</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamUserStatsCallbacks + 11)]
public struct LeaderboardUGCSet_t
{
	public const int k_iCallback = Constants.k_iSteamUserStatsCallbacks + 11;
	/// <summary>
	/// <para>The result of the operation</para>
	/// </summary>
	public EResult m_eResult;
	/// <summary>
	/// <para>the leaderboard handle that was</para>
	/// </summary>
	public SteamLeaderboard_t m_hSteamLeaderboard;
}

/// <summary>
/// <para>callback indicating global stats have been received.</para>
/// <para>Returned as a result of RequestGlobalStats()</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamUserStatsCallbacks + 12)]
public struct GlobalStatsReceived_t
{
	public const int k_iCallback = Constants.k_iSteamUserStatsCallbacks + 12;
	/// <summary>
	/// <para>Game global stats were requested for</para>
	/// </summary>
	public ulong m_nGameID;
	/// <summary>
	/// <para>The result of the request</para>
	/// </summary>
	public EResult m_eResult;
}

/// <summary>
/// <para>callbacks</para>
/// <para>The country of the user changed</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value, Size = 1)]
[CallbackIdentity(Constants.k_iSteamUtilsCallbacks + 1)]
public struct IPCountry_t
{
	public const int k_iCallback = Constants.k_iSteamUtilsCallbacks + 1;
}

/// <summary>
/// <para>Fired when running on a handheld PC or laptop with less than 10 minutes of battery is left, fires then every minute</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamUtilsCallbacks + 2)]
public struct LowBatteryPower_t
{
	public const int k_iCallback = Constants.k_iSteamUtilsCallbacks + 2;
	public byte m_nMinutesBatteryLeft;
}

/// <summary>
/// <para>called when a SteamAsyncCall_t has completed (or failed)</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamUtilsCallbacks + 3)]
public struct SteamAPICallCompleted_t
{
	public const int k_iCallback = Constants.k_iSteamUtilsCallbacks + 3;
	public SteamAPICall_t m_hAsyncCall;
	public int m_iCallback;
	public uint m_cubParam;
}

/// <summary>
/// <para>called when Steam wants to shutdown</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value, Size = 1)]
[CallbackIdentity(Constants.k_iSteamUtilsCallbacks + 4)]
public struct SteamShutdown_t
{
	public const int k_iCallback = Constants.k_iSteamUtilsCallbacks + 4;
}

/// <summary>
/// <para>callback for CheckFileSignature</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamUtilsCallbacks + 5)]
public struct CheckFileSignature_t
{
	public const int k_iCallback = Constants.k_iSteamUtilsCallbacks + 5;
	public ECheckFileSignature m_eCheckFileSignature;
}

/// <summary>
/// <para>k_iSteamUtilsCallbacks + 13 is taken</para>
/// <para>Full Screen gamepad text input has been closed</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamUtilsCallbacks + 14)]
public struct GamepadTextInputDismissed_t
{
	public const int k_iCallback = Constants.k_iSteamUtilsCallbacks + 14;
	/// <summary>
	/// <para>true if user entered &amp; accepted text (Call ISteamUtils::GetEnteredGamepadTextInput() for text), false if canceled input</para>
	/// </summary>
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bSubmitted;
	public uint m_unSubmittedText;
	public AppId_t m_unAppID;
}

/// <summary>
/// <para>k_iSteamUtilsCallbacks + 15 through 35 are taken</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value, Size = 1)]
[CallbackIdentity(Constants.k_iSteamUtilsCallbacks + 36)]
public struct AppResumingFromSuspend_t
{
	public const int k_iCallback = Constants.k_iSteamUtilsCallbacks + 36;
}

/// <summary>
/// <para>k_iSteamUtilsCallbacks + 37 is taken</para>
/// <para>The floating on-screen keyboard has been closed</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value, Size = 1)]
[CallbackIdentity(Constants.k_iSteamUtilsCallbacks + 38)]
public struct FloatingGamepadTextInputDismissed_t
{
	public const int k_iCallback = Constants.k_iSteamUtilsCallbacks + 38;
}

/// <summary>
/// <para>The text filtering dictionary has changed</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamUtilsCallbacks + 39)]
public struct FilterTextDictionaryChanged_t
{
	public const int k_iCallback = Constants.k_iSteamUtilsCallbacks + 39;
	/// <summary>
	/// <para>One of ELanguage, or k_LegallyRequiredFiltering</para>
	/// </summary>
	public int m_eLanguage;
}

[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamVideoCallbacks + 11)]
public unsafe struct GetVideoURLResult_t
{
	public const int k_iCallback = Constants.k_iSteamVideoCallbacks + 11;
	public EResult m_eResult;
	public AppId_t m_unVideoAppID;
	private fixed byte m_rgchURL_[256];
	public string m_rgchURL
	{
		get
		{
			fixed (byte* ptr = m_rgchURL_)
			{
				var span = new ReadOnlySpan<byte>(ptr, 256);
				var length = span.IndexOf((byte)0);
				return Encoding.UTF8.GetString(length < 0 ? span : span[..length]);
			}
		}
		set
		{
			fixed (byte* ptr = m_rgchURL_)
			{
				var span = new Span<byte>(ptr, 256);
				span.Clear();
				var bytes = Encoding.UTF8.GetBytes(value ?? string.Empty);
				bytes.AsSpan(0, Math.Min(bytes.Length, 256 - 1)).CopyTo(span);
			}
		}
	}
}

[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamVideoCallbacks + 24)]
public struct GetOPFSettingsResult_t
{
	public const int k_iCallback = Constants.k_iSteamVideoCallbacks + 24;
	public EResult m_eResult;
	public AppId_t m_unVideoAppID;
}

[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamVideoCallbacks + 4)]
public struct BroadcastUploadStart_t
{
	public const int k_iCallback = Constants.k_iSteamVideoCallbacks + 4;
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bIsRTMP;
}

[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamVideoCallbacks + 5)]
public struct BroadcastUploadStop_t
{
	public const int k_iCallback = Constants.k_iSteamVideoCallbacks + 5;
	public EBroadcastUploadResult m_eResult;
}

/// <summary>
/// <para>Callback struct used to notify when a connection has changed state</para>
/// <para>A struct used to describe a &quot;fake IP&quot; we have been assigned to</para>
/// <para>use as an identifier.  This callback is posted when</para>
/// <para>ISteamNetworkingSoockets::BeginAsyncRequestFakeIP completes.</para>
/// <para>See also ISteamNetworkingSockets::GetFakeIP</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
[CallbackIdentity(Constants.k_iSteamNetworkingSocketsCallbacks + 3)]
public unsafe struct SteamNetworkingFakeIPResult_t
{
	public const int k_iCallback = Constants.k_iSteamNetworkingSocketsCallbacks + 3;
	/// <summary>
	/// <para>Status/result of the allocation request.  Possible failure values are:</para>
	/// <para>- k_EResultBusy - you called GetFakeIP but the request has not completed.</para>
	/// <para>- k_EResultInvalidParam - you called GetFakeIP with an invalid port index</para>
	/// <para>- k_EResultLimitExceeded - You asked for too many ports, or made an</para>
	/// <para>additional request after one had already succeeded</para>
	/// <para>- k_EResultNoMatch - GetFakeIP was called, but no request has been made</para>
	/// <para>Note that, with the exception of k_EResultBusy (if you are polling),</para>
	/// <para>it is highly recommended to treat all failures as fatal.</para>
	/// </summary>
	public EResult m_eResult;
	/// <summary>
	/// <para>Local identity of the ISteamNetworkingSockets object that made</para>
	/// <para>this request and is assigned the IP.  This is needed in the callback</para>
	/// <para>in the case where there are multiple ISteamNetworkingSockets objects.</para>
	/// <para>(E.g. one for the user, and another for the local gameserver).</para>
	/// </summary>
	public SteamNetworkingIdentity m_identity;
	/// <summary>
	/// <para>Fake IPv4 IP address that we have been assigned.  NOTE: this</para>
	/// <para>IP address is not exclusively ours!  Steam tries to avoid sharing</para>
	/// <para>IP addresses, but this may not always be possible.  The IP address</para>
	/// <para>may be currently in use by another host, but with different port(s).</para>
	/// <para>The exact same IP:port address may have been used previously.</para>
	/// <para>Steam tries to avoid reusing ports until they have not been in use for</para>
	/// <para>some time, but this may not always be possible.</para>
	/// </summary>
	public uint m_unIP;
	public fixed ushort m_unPorts[Constants.k_nMaxReturnPorts];
}

