using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace SwiftlyS2.Shared.SteamAPI;

/// <summary>
/// <para>friend game played information</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
public struct FriendGameInfo_t
{
	public CGameID m_gameID;
	public uint m_unGameIP;
	public ushort m_usGamePort;
	public ushort m_usQueryPort;
	public CSteamID m_steamIDLobby;
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct InputAnalogActionData_t
{
	/// <summary>
	/// <para>Type of data coming from this action, this will match what got specified in the action set</para>
	/// </summary>
	public EInputSourceMode eMode;
	/// <summary>
	/// <para>The current state of this action; will be delta updates for mouse actions</para>
	/// </summary>
	public float x, y;
	/// <summary>
	/// <para>Whether or not this action is currently available to be bound in the active action set</para>
	/// </summary>
	public byte bActive;
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct InputDigitalActionData_t
{
	/// <summary>
	/// <para>The current state of this action; will be true if currently pressed</para>
	/// </summary>
	public byte bState;
	/// <summary>
	/// <para>Whether or not this action is currently available to be bound in the active action set</para>
	/// </summary>
	public byte bActive;
}

[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
public struct InputMotionData_t
{
	/// <summary>
	/// <para>Gyro Quaternion:</para>
	/// <para>Absolute rotation of the controller since wakeup, using the Accelerometer reading at startup to determine the first value.</para>
	/// <para>This means real world &quot;up&quot; is know, but heading is not known.</para>
	/// <para>Every rotation packet is integrated using sensor time delta, and that change is used to update this quaternion.</para>
	/// <para>A Quaternion Identity ( x:0, y:0, z:0, w:1 ) will be sent in the first few packets while the controller&apos;s IMU is still waking up;</para>
	/// <para>some controllers have a short &quot;warmup&quot; period before these values should be used.</para>
	/// <para>After the first time GetMotionData is called per controller handle, the IMU will be active until your app is closed.</para>
	/// <para>The exception is the Sony Dualshock, which will stay on until the controller has been turned off.</para>
	/// <para>Filtering: When rotating the controller at low speeds, low level noise is filtered out without noticeable latency. High speed movement is always unfiltered.</para>
	/// <para>Drift: Gyroscopic &quot;Drift&quot; can be fixed using the Steam Input &quot;Gyro Calibration&quot; button. Users will have to be informed of this feature.</para>
	/// </summary>
	public float rotQuatX;
	public float rotQuatY;
	public float rotQuatZ;
	public float rotQuatW;
	/// <summary>
	/// <para>Positional acceleration</para>
	/// <para>This represents only the latest hardware packet&apos;s state.</para>
	/// <para>Values range from -SHRT_MAX..SHRT_MAX</para>
	/// <para>This represents -2G..+2G along each axis</para>
	/// <para>+tive when controller&apos;s Right hand side is pointed toward the sky.</para>
	/// </summary>
	public float posAccelX;
	/// <summary>
	/// <para>+tive when controller&apos;s charging port (forward side of controller) is pointed toward the sky.</para>
	/// </summary>
	public float posAccelY;
	/// <summary>
	/// <para>+tive when controller&apos;s sticks point toward the sky.</para>
	/// </summary>
	public float posAccelZ;
	/// <summary>
	/// <para>Angular velocity</para>
	/// <para>Values range from -SHRT_MAX..SHRT_MAX</para>
	/// <para>These values map to a real world range of -2000..+2000 degrees per second on each axis (SDL standard)</para>
	/// <para>This represents only the latest hardware packet&apos;s state.</para>
	/// <para>Local Pitch</para>
	/// </summary>
	public float rotVelX;
	/// <summary>
	/// <para>Local Roll</para>
	/// </summary>
	public float rotVelY;
	/// <summary>
	/// <para>Local Yaw</para>
	/// </summary>
	public float rotVelZ;
}

[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
public struct SteamItemDetails_t
{
	public SteamItemInstanceID_t m_itemId;
	public SteamItemDef_t m_iDefinition;
	public ushort m_unQuantity;
	/// <summary>
	/// <para>see ESteamItemFlags</para>
	/// </summary>
	public ushort m_unFlags;
}

/// <summary>
/// <para>connection state to a specified user, returned by GetP2PSessionState()</para>
/// <para>this is under-the-hood info about what&apos;s going on with a SendP2PPacket(), shouldn&apos;t be needed except for debuggin</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
public struct P2PSessionState_t
{
	/// <summary>
	/// <para>true if we&apos;ve got an active open connection</para>
	/// </summary>
	public byte m_bConnectionActive;
	/// <summary>
	/// <para>true if we&apos;re currently trying to establish a connection</para>
	/// </summary>
	public byte m_bConnecting;
	/// <summary>
	/// <para>last error recorded (see enum above)</para>
	/// </summary>
	public byte m_eP2PSessionError;
	/// <summary>
	/// <para>true if it&apos;s going through a relay server (TURN)</para>
	/// </summary>
	public byte m_bUsingRelay;
	public int m_nBytesQueuedForSend;
	public int m_nPacketsQueuedForSend;
	/// <summary>
	/// <para>potential IP:Port of remote host. Could be TURN server.</para>
	/// </summary>
	public uint m_nRemoteIP;
	/// <summary>
	/// <para>Only exists for compatibility with older authentication api&apos;s</para>
	/// </summary>
	public ushort m_nRemotePort;
}

/// <summary>
/// <para>Mouse motion event data, valid when m_eType is k_ERemotePlayInputMouseMotion</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
public struct RemotePlayInputMouseMotion_t
{
	/// <summary>
	/// <para>True if this is absolute mouse motion and m_flNormalizedX and m_flNormalizedY are valid</para>
	/// </summary>
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bAbsolute;
	/// <summary>
	/// <para>The absolute X position of the mouse, normalized to the display, if m_bAbsolute is true</para>
	/// </summary>
	public float m_flNormalizedX;
	/// <summary>
	/// <para>The absolute Y position of the mouse, normalized to the display, if m_bAbsolute is true</para>
	/// </summary>
	public float m_flNormalizedY;
	/// <summary>
	/// <para>Relative mouse motion in the X direction</para>
	/// </summary>
	public int m_nDeltaX;
	/// <summary>
	/// <para>Relative mouse motion in the Y direction</para>
	/// </summary>
	public int m_nDeltaY;
}

/// <summary>
/// <para>Mouse wheel event data, valid when m_eType is k_ERemotePlayInputMouseWheel</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
public struct RemotePlayInputMouseWheel_t
{
	public ERemotePlayMouseWheelDirection m_eDirection;
	/// <summary>
	/// <para>1.0f is a single click of the wheel, 120 units on Windows</para>
	/// </summary>
	public float m_flAmount;
}

/// <summary>
/// <para>Key event data, valid when m_eType is k_ERemotePlayInputKeyDown or k_ERemotePlayInputKeyUp</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
public struct RemotePlayInputKey_t
{
	/// <summary>
	/// <para>Keyboard scancode, common values are defined in ERemotePlayScancode</para>
	/// </summary>
	public int m_eScancode;
	/// <summary>
	/// <para>Mask of ERemotePlayKeyModifier active for this key event</para>
	/// </summary>
	public uint m_unModifiers;
	/// <summary>
	/// <para>UCS-4 character generated by the keypress, or 0 if it wasn&apos;t a character key, e.g. Delete or Left Arrow</para>
	/// </summary>
	public uint m_unKeycode;
}

[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
public struct RemotePlayInput_t
{
	public RemotePlaySessionID_t m_unSessionID;
	public ERemotePlayInputType m_eType;
	/// <summary>
	/// <para>Mouse motion event data, valid when m_eType is k_ERemotePlayInputMouseMotion</para>
	/// </summary>
	public RemotePlayInputMouseMotion_t m_MouseMotion;
	/// <summary>
	/// <para>Mouse button event data, valid when m_eType is k_ERemotePlayInputMouseButtonDown or k_ERemotePlayInputMouseButtonUp</para>
	/// </summary>
	public ERemotePlayMouseButton m_eMouseButton;
	/// <summary>
	/// <para>Mouse wheel event data, valid when m_eType is k_ERemotePlayInputMouseWheel</para>
	/// </summary>
	public RemotePlayInputMouseWheel_t m_MouseWheel;
	/// <summary>
	/// <para>Key event data, valid when m_eType is k_ERemotePlayInputKeyDown or k_ERemotePlayInputKeyUp</para>
	/// </summary>
	public RemotePlayInputKey_t m_Key;
}

/// <summary>
/// <para>Structure that contains an array of const char * strings and the number of those strings</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
public struct SteamParamStringArray_t
{
	public IntPtr m_ppStrings;
	public int m_nNumStrings;
}

/// <summary>
/// <para>Details for a single published file/UGC</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
public unsafe struct SteamUGCDetails_t
{
	public PublishedFileId_t m_nPublishedFileId;
	/// <summary>
	/// <para>The result of the operation.</para>
	/// </summary>
	public EResult m_eResult;
	/// <summary>
	/// <para>Type of the file</para>
	/// </summary>
	public EWorkshopFileType m_eFileType;
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
	/// <summary>
	/// <para>time when the user added the published file to their list (not always applicable)</para>
	/// </summary>
	public uint m_rtimeAddedToUserList;
	/// <summary>
	/// <para>visibility</para>
	/// </summary>
	public ERemoteStoragePublishedFileVisibility m_eVisibility;
	/// <summary>
	/// <para>whether the file was banned</para>
	/// </summary>
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bBanned;
	/// <summary>
	/// <para>developer has specifically flagged this item as accepted in the Workshop</para>
	/// </summary>
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bAcceptedForUse;
	/// <summary>
	/// <para>whether the list of tags was too long to be returned in the provided buffer</para>
	/// </summary>
	[MarshalAs(UnmanagedType.I1)]
	public bool m_bTagsTruncated;
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
	/// <para>file/url information</para>
	/// <para>The handle of the primary file</para>
	/// </summary>
	public UGCHandle_t m_hFile;
	/// <summary>
	/// <para>The handle of the preview file</para>
	/// </summary>
	public UGCHandle_t m_hPreviewFile;
	/// <summary>
	/// <para>The cloud filename of the primary file</para>
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
	/// <para>Size of the primary file (for legacy items which only support one file). This may not be accurate for non-legacy items which can be greater than 4gb in size.</para>
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
	/// <para>voting information</para>
	/// <para>number of votes up</para>
	/// </summary>
	public uint m_unVotesUp;
	/// <summary>
	/// <para>number of votes down</para>
	/// </summary>
	public uint m_unVotesDown;
	/// <summary>
	/// <para>calculated score</para>
	/// </summary>
	public float m_flScore;
	/// <summary>
	/// <para>collection details</para>
	/// </summary>
	public uint m_unNumChildren;
	/// <summary>
	/// <para>Total size of all files (non-legacy), excluding the preview file</para>
	/// </summary>
	public ulong m_ulTotalFilesSize;
}

/// <summary>
/// <para>a single entry in a leaderboard, as returned by GetDownloadedLeaderboardEntry()</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
public struct LeaderboardEntry_t
{
	/// <summary>
	/// <para>user with the entry - use SteamFriends()-&gt;GetFriendPersonaName() &amp; SteamFriends()-&gt;GetFriendAvatar() to get more info</para>
	/// </summary>
	public CSteamID m_steamIDUser;
	/// <summary>
	/// <para>[1..N], where N is the number of users with an entry in the leaderboard</para>
	/// </summary>
	public int m_nGlobalRank;
	/// <summary>
	/// <para>score as set in the leaderboard</para>
	/// </summary>
	public int m_nScore;
	/// <summary>
	/// <para>number of int32 details available for this entry</para>
	/// </summary>
	public int m_cDetails;
	/// <summary>
	/// <para>handle for UGC attached to the entry</para>
	/// </summary>
	public UGCHandle_t m_hUGC;
}

/// <summary>
/// <para>structure that contains client callback data</para>
/// <para>see callbacks documentation for more details</para>
/// <para>Internal structure used in manual callback dispatch</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
public struct CallbackMsg_t
{
	/// <summary>
	/// <para>Specific user to whom this callback applies.</para>
	/// </summary>
	public int m_hSteamUser;
	/// <summary>
	/// <para>Callback identifier.  (Corresponds to the k_iCallback enum in the callback structure.)</para>
	/// </summary>
	public int m_iCallback;
	/// <summary>
	/// <para>Points to the callback structure</para>
	/// </summary>
	public IntPtr m_pubParam;
	/// <summary>
	/// <para>Size of the data pointed to by m_pubParam</para>
	/// </summary>
	public int m_cubParam;
}

/// <summary>
/// <para>Describe the state of a connection.</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
public unsafe struct SteamNetConnectionInfo_t
{
	/// <summary>
	/// <para>Who is on the other end?  Depending on the connection type and phase of the connection, we might not know</para>
	/// </summary>
	public SteamNetworkingIdentity m_identityRemote;
	/// <summary>
	/// <para>Arbitrary user data set by the local application code</para>
	/// </summary>
	public long m_nUserData;
	/// <summary>
	/// <para>Handle to listen socket this was connected on, or k_HSteamListenSocket_Invalid if we initiated the connection</para>
	/// </summary>
	public HSteamListenSocket m_hListenSocket;
	/// <summary>
	/// <para>Remote address.  Might be all 0&apos;s if we don&apos;t know it, or if this is N/A.</para>
	/// <para>(E.g. Basically everything except direct UDP connection.)</para>
	/// </summary>
	public SteamNetworkingIPAddr m_addrRemote;
	public ushort m__pad1;
	/// <summary>
	/// <para>What data center is the remote host in?  (0 if we don&apos;t know.)</para>
	/// </summary>
	public SteamNetworkingPOPID m_idPOPRemote;
	/// <summary>
	/// <para>What relay are we using to communicate with the remote host?</para>
	/// <para>(0 if not applicable.)</para>
	/// </summary>
	public SteamNetworkingPOPID m_idPOPRelay;
	/// <summary>
	/// <para>High level state of the connection</para>
	/// </summary>
	public ESteamNetworkingConnectionState m_eState;
	/// <summary>
	/// <para>Basic cause of the connection termination or problem.</para>
	/// <para>See ESteamNetConnectionEnd for the values used</para>
	/// </summary>
	public int m_eEndReason;
	/// <summary>
	/// <para>Human-readable, but non-localized explanation for connection</para>
	/// <para>termination or problem.  This is intended for debugging /</para>
	/// <para>diagnostic purposes only, not to display to users.  It might</para>
	/// <para>have some details specific to the issue.</para>
	/// </summary>
	private fixed byte m_szEndDebug_[Constants.k_cchSteamNetworkingMaxConnectionCloseReason];
	public string m_szEndDebug
	{
		get
		{
			fixed (byte* ptr = m_szEndDebug_)
			{
				var span = new ReadOnlySpan<byte>(ptr, Constants.k_cchSteamNetworkingMaxConnectionCloseReason);
				var length = span.IndexOf((byte)0);
				return Encoding.UTF8.GetString(length < 0 ? span : span[..length]);
			}
		}
		set
		{
			fixed (byte* ptr = m_szEndDebug_)
			{
				var span = new Span<byte>(ptr, Constants.k_cchSteamNetworkingMaxConnectionCloseReason);
				span.Clear();
				var bytes = Encoding.UTF8.GetBytes(value ?? string.Empty);
				bytes.AsSpan(0, Math.Min(bytes.Length, Constants.k_cchSteamNetworkingMaxConnectionCloseReason - 1)).CopyTo(span);
			}
		}
	}
	/// <summary>
	/// <para>Debug description.  This includes the internal connection ID,</para>
	/// <para>connection type (and peer information), and any name</para>
	/// <para>given to the connection by the app.  This string is used in various</para>
	/// <para>internal logging messages.</para>
	/// <para>Note that the connection ID *usually* matches the HSteamNetConnection</para>
	/// <para>handle, but in certain cases with symmetric connections it might not.</para>
	/// </summary>
	private fixed byte m_szConnectionDescription_[Constants.k_cchSteamNetworkingMaxConnectionDescription];
	public string m_szConnectionDescription
	{
		get
		{
			fixed (byte* ptr = m_szConnectionDescription_)
			{
				var span = new ReadOnlySpan<byte>(ptr, Constants.k_cchSteamNetworkingMaxConnectionDescription);
				var length = span.IndexOf((byte)0);
				return Encoding.UTF8.GetString(length < 0 ? span : span[..length]);
			}
		}
		set
		{
			fixed (byte* ptr = m_szConnectionDescription_)
			{
				var span = new Span<byte>(ptr, Constants.k_cchSteamNetworkingMaxConnectionDescription);
				span.Clear();
				var bytes = Encoding.UTF8.GetBytes(value ?? string.Empty);
				bytes.AsSpan(0, Math.Min(bytes.Length, Constants.k_cchSteamNetworkingMaxConnectionDescription - 1)).CopyTo(span);
			}
		}
	}
	/// <summary>
	/// <para>Misc flags.  Bitmask of k_nSteamNetworkConnectionInfoFlags_Xxxx</para>
	/// </summary>
	public int m_nFlags;
	/// <summary>
	/// <para>Internal stuff, room to change API easily</para>
	/// </summary>
	public fixed uint reserved[63];
}

/// <summary>
/// <para>Quick connection state, pared down to something you could call</para>
/// <para>more frequently without it being too big of a perf hit.</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
public unsafe struct SteamNetConnectionRealTimeStatus_t
{
	/// <summary>
	/// <para>High level state of the connection</para>
	/// </summary>
	public ESteamNetworkingConnectionState m_eState;
	/// <summary>
	/// <para>Current ping (ms)</para>
	/// </summary>
	public int m_nPing;
	/// <summary>
	/// <para>Connection quality measured locally, 0...1.  (Percentage of packets delivered</para>
	/// <para>end-to-end in order).</para>
	/// </summary>
	public float m_flConnectionQualityLocal;
	/// <summary>
	/// <para>Packet delivery success rate as observed from remote host</para>
	/// </summary>
	public float m_flConnectionQualityRemote;
	/// <summary>
	/// <para>Current data rates from recent history.</para>
	/// </summary>
	public float m_flOutPacketsPerSec;
	public float m_flOutBytesPerSec;
	public float m_flInPacketsPerSec;
	public float m_flInBytesPerSec;
	/// <summary>
	/// <para>Estimate rate that we believe that we can send data to our peer.</para>
	/// <para>Note that this could be significantly higher than m_flOutBytesPerSec,</para>
	/// <para>meaning the capacity of the channel is higher than you are sending data.</para>
	/// <para>(That&apos;s OK!)</para>
	/// </summary>
	public int m_nSendRateBytesPerSecond;
	/// <summary>
	/// <para>Number of bytes pending to be sent.  This is data that you have recently</para>
	/// <para>requested to be sent but has not yet actually been put on the wire.  The</para>
	/// <para>reliable number ALSO includes data that was previously placed on the wire,</para>
	/// <para>but has now been scheduled for re-transmission.  Thus, it&apos;s possible to</para>
	/// <para>observe m_cbPendingReliable increasing between two checks, even if no</para>
	/// <para>calls were made to send reliable data between the checks.  Data that is</para>
	/// <para>awaiting the Nagle delay will appear in these numbers.</para>
	/// </summary>
	public int m_cbPendingUnreliable;
	public int m_cbPendingReliable;
	/// <summary>
	/// <para>Number of bytes of reliable data that has been placed the wire, but</para>
	/// <para>for which we have not yet received an acknowledgment, and thus we may</para>
	/// <para>have to re-transmit.</para>
	/// </summary>
	public int m_cbSentUnackedReliable;
	/// <summary>
	/// <para>If you queued a message right now, approximately how long would that message</para>
	/// <para>wait in the queue before we actually started putting its data on the wire in</para>
	/// <para>a packet?</para>
	/// <para>In general, data that is sent by the application is limited by the bandwidth</para>
	/// <para>of the channel.  If you send data faster than this, it must be queued and</para>
	/// <para>put on the wire at a metered rate.  Even sending a small amount of data (e.g.</para>
	/// <para>a few MTU, say ~3k) will require some of the data to be delayed a bit.</para>
	/// <para>Ignoring multiple lanes, the estimated delay will be approximately equal to</para>
	/// <para>( m_cbPendingUnreliable+m_cbPendingReliable ) / m_nSendRateBytesPerSecond</para>
	/// <para>plus or minus one MTU.  It depends on how much time has elapsed since the last</para>
	/// <para>packet was put on the wire.  For example, the queue might have *just* been emptied,</para>
	/// <para>and the last packet placed on the wire, and we are exactly up against the send</para>
	/// <para>rate limit.  In that case we might need to wait for one packet&apos;s worth of time to</para>
	/// <para>elapse before we can send again.  On the other extreme, the queue might have data</para>
	/// <para>in it waiting for Nagle.  (This will always be less than one packet, because as</para>
	/// <para>soon as we have a complete packet we would send it.)  In that case, we might be</para>
	/// <para>ready to send data now, and this value will be 0.</para>
	/// <para>This value is only valid if multiple lanes are not used.  If multiple lanes are</para>
	/// <para>in use, then the queue time will be different for each lane, and you must use</para>
	/// <para>the value in SteamNetConnectionRealTimeLaneStatus_t.</para>
	/// <para>Nagle delay is ignored for the purposes of this calculation.</para>
	/// </summary>
	public SteamNetworkingMicroseconds m_usecQueueTime;
	/// <summary>
	/// <para>Internal stuff, room to change API easily</para>
	/// </summary>
	public fixed uint reserved[16];
}

/// <summary>
/// <para>Quick status of a particular lane</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
public unsafe struct SteamNetConnectionRealTimeLaneStatus_t
{
	/// <summary>
	/// <para>Counters for this particular lane.  See the corresponding variables</para>
	/// <para>in SteamNetConnectionRealTimeStatus_t</para>
	/// </summary>
	public int m_cbPendingUnreliable;
	public int m_cbPendingReliable;
	public int m_cbSentUnackedReliable;
	/// <summary>
	/// <para>Reserved for future use</para>
	/// </summary>
	public int _reservePad1;
	/// <summary>
	/// <para>Lane-specific queue time.  This value takes into consideration lane priorities</para>
	/// <para>and weights, and how much data is queued in each lane, and attempts to predict</para>
	/// <para>how any data currently queued will be sent out.</para>
	/// </summary>
	public SteamNetworkingMicroseconds m_usecQueueTime;
	/// <summary>
	/// <para>Internal stuff, room to change API easily</para>
	/// </summary>
	public fixed uint reserved[10];
}

/// <summary>
/// <para>Ping location / measurement</para>
/// <para>Object that describes a &quot;location&quot; on the Internet with sufficient</para>
/// <para>detail that we can reasonably estimate an upper bound on the ping between</para>
/// <para>the two hosts, even if a direct route between the hosts is not possible,</para>
/// <para>and the connection must be routed through the Steam Datagram Relay network.</para>
/// <para>This does not contain any information that identifies the host.  Indeed,</para>
/// <para>if two hosts are in the same building or otherwise have nearly identical</para>
/// <para>networking characteristics, then it&apos;s valid to use the same location</para>
/// <para>object for both of them.</para>
/// <para>NOTE: This object should only be used in the same process!  Do not serialize it,</para>
/// <para>send it over the wire, or persist it in a file or database!  If you need</para>
/// <para>to do that, convert it to a string representation using the methods in</para>
/// <para>ISteamNetworkingUtils().</para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
public unsafe struct SteamNetworkPingLocation_t
{
	public fixed byte m_data[512];
}

