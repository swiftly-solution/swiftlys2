namespace SwiftlyS2.Shared.SteamAPI;

public static class Constants
{
	public const string STEAMAPPS_INTERFACE_VERSION = "STEAMAPPS_INTERFACE_VERSION008";
	public const string STEAMAPPTICKET_INTERFACE_VERSION = "STEAMAPPTICKET_INTERFACE_VERSION001";
	public const string STEAMCLIENT_INTERFACE_VERSION = "SteamClient023";
	public const string STEAMFRIENDS_INTERFACE_VERSION = "SteamFriends018";
	public const string STEAMGAMECOORDINATOR_INTERFACE_VERSION = "SteamGameCoordinator001";
	public const string STEAMGAMESERVER_INTERFACE_VERSION = "SteamGameServer015";
	public const string STEAMGAMESERVERSTATS_INTERFACE_VERSION = "SteamGameServerStats001";
	public const string STEAMHTMLSURFACE_INTERFACE_VERSION = "STEAMHTMLSURFACE_INTERFACE_VERSION_005";
	public const string STEAMHTTP_INTERFACE_VERSION = "STEAMHTTP_INTERFACE_VERSION003";
	public const string STEAMINPUT_INTERFACE_VERSION = "SteamInput006";
	public const string STEAMINVENTORY_INTERFACE_VERSION = "STEAMINVENTORY_INTERFACE_V003";
	public const string STEAMMUSIC_INTERFACE_VERSION = "STEAMMUSIC_INTERFACE_VERSION001";
	public const string STEAMMUSICREMOTE_INTERFACE_VERSION = "STEAMMUSICREMOTE_INTERFACE_VERSION001";
	public const string STEAMNETWORKING_INTERFACE_VERSION = "SteamNetworking006";
	public const string STEAMNETWORKINGMESSAGES_INTERFACE_VERSION = "SteamNetworkingMessages002";
	/// <summary>
	/// <para>Silence some warnings</para>
	/// </summary>
	public const string STEAMNETWORKINGSOCKETS_INTERFACE_VERSION = "SteamNetworkingSockets012";
	/// <summary>
	/// <para>Silence some warnings</para>
	/// </summary>
	public const string STEAMNETWORKINGUTILS_INTERFACE_VERSION = "SteamNetworkingUtils004";
	public const string STEAMPARENTALSETTINGS_INTERFACE_VERSION = "STEAMPARENTALSETTINGS_INTERFACE_VERSION001";
	public const string STEAMREMOTEPLAY_INTERFACE_VERSION = "STEAMREMOTEPLAY_INTERFACE_VERSION003";
	public const string STEAMREMOTESTORAGE_INTERFACE_VERSION = "STEAMREMOTESTORAGE_INTERFACE_VERSION016";
	public const string STEAMSCREENSHOTS_INTERFACE_VERSION = "STEAMSCREENSHOTS_INTERFACE_VERSION003";
	public const string STEAMTIMELINE_INTERFACE_VERSION = "STEAMTIMELINE_INTERFACE_V004";
	public const string STEAMUGC_INTERFACE_VERSION = "STEAMUGC_INTERFACE_VERSION021";
	public const string STEAMUSER_INTERFACE_VERSION = "SteamUser023";
	public const string STEAMUSERSTATS_INTERFACE_VERSION = "STEAMUSERSTATS_INTERFACE_VERSION013";
	public const string STEAMUTILS_INTERFACE_VERSION = "SteamUtils010";
	public const string STEAMVIDEO_INTERFACE_VERSION = "STEAMVIDEO_INTERFACE_V007";
	/// <summary>
	/// <para>max supported length of a legacy cd key</para>
	/// </summary>
	public const int k_cubAppProofOfPurchaseKeyMax = 240;
	/// <summary>
	/// <para>maximum length of friend group name (not including terminating nul!)</para>
	/// </summary>
	public const int k_cchMaxFriendsGroupName = 64;
	/// <summary>
	/// <para>maximum number of groups a single user is allowed</para>
	/// </summary>
	public const int k_cFriendsGroupLimit = 100;
	public const int k_cEnumerateFollowersMax = 50;
	/// <summary>
	/// <para>special values for FriendGameInfo_t::m_usQueryPort</para>
	/// <para>We haven&apos;t asked the GS for this query port&apos;s actual value yet.  Was #define QUERY_PORT_NOT_INITIALIZED in older versions of Steamworks SDK.</para>
	/// </summary>
	public const ushort k_usFriendGameInfoQueryPort_NotInitialized = 0xFFFF;
	/// <summary>
	/// <para>We were unable to get the query port for this server.  Was #define QUERY_PORT_ERROR in older versions of Steamworks SDK.</para>
	/// </summary>
	public const ushort k_usFriendGameInfoQueryPort_Error = 0xFFFE;
	/// <summary>
	/// <para>maximum number of characters in a user&apos;s name. Two flavors; one for UTF-8 and one for UTF-16.</para>
	/// <para>The UTF-8 version has to be very generous to accomodate characters that get large when encoded</para>
	/// <para>in UTF-8.</para>
	/// </summary>
	public const int k_cchPersonaNameMax = 128;
	public const int k_cwchPersonaNameMax = 32;
	/// <summary>
	/// <para>size limit on chat room or member metadata</para>
	/// </summary>
	public const int k_cubChatMetadataMax = 8192;
	/// <summary>
	/// <para>size limits on Rich Presence data</para>
	/// </summary>
	public const int k_cchMaxRichPresenceKeys = 30;
	public const int k_cchMaxRichPresenceKeyLength = 64;
	public const int k_cchMaxRichPresenceValueLength = 256;
	/// <summary>
	/// <para>Defines the largest allowed file size. Cloud files cannot be written</para>
	/// <para>in a single chunk over 100MB (and cannot be over 200MB total.)</para>
	/// </summary>
	public const int k_unMaxCloudFileChunkSize = 100 * 1024 * 1024;
	public const int k_cchPublishedDocumentTitleMax = 128 + 1;
	public const int k_cchPublishedDocumentDescriptionMax = 8000;
	public const int k_cchPublishedDocumentChangeDescriptionMax = 8000;
	public const int k_unEnumeratePublishedFilesMaxResults = 50;
	public const int k_cchTagListMax = 1024 + 1;
	public const int k_cchFilenameMax = 260;
	public const int k_cchPublishedFileURLMax = 256;
	public const int k_nScreenshotMaxTaggedUsers = 32;
	public const int k_nScreenshotMaxTaggedPublishedFiles = 32;
	public const int k_cubUFSTagTypeMax = 255;
	public const int k_cubUFSTagValueMax = 255;
	/// <summary>
	/// <para>Required with of a thumbnail provided to AddScreenshotToLibrary.  If you do not provide a thumbnail</para>
	/// <para>one will be generated.</para>
	/// </summary>
	public const int k_ScreenshotThumbWidth = 200;
	public const int k_unMaxTimelinePriority = 1000;
	/// <summary>
	/// <para>Use with UpdateRangeTimelineEvent to not change the priority</para>
	/// </summary>
	public const int k_unTimelinePriority_KeepCurrentValue = 1000000;
	public const float k_flMaxTimelineEventDuration = 600;
	public const int k_cchMaxPhaseIDLength = 64;
	public const int kNumUGCResultsPerPage = 50;
	public const int k_cchDeveloperMetadataMax = 5000;
	public const int k_nCubTicketMaxLength = 2560;
	/// <summary>
	/// <para>size limit on stat or achievement name (UTF-8 encoded)</para>
	/// </summary>
	public const int k_cchStatNameMax = 128;
	/// <summary>
	/// <para>maximum number of bytes for a leaderboard name (UTF-8 encoded)</para>
	/// </summary>
	public const int k_cchLeaderboardNameMax = 128;
	/// <summary>
	/// <para>maximum number of details int32&apos;s storable for a single leaderboard entry</para>
	/// </summary>
	public const int k_cLeaderboardDetailsMax = 64;
	/// <summary>
	/// <para>A fixed size buffer to receive an error message that is returned by some API</para>
	/// <para>calls.</para>
	/// </summary>
	public const int k_cchMaxSteamErrMsg = 1024;
	/// <summary>
	/// <para>Forward declare types</para>
	/// <para>Base values for callback identifiers, each callback must</para>
	/// <para>have a unique ID.</para>
	/// </summary>
	public const int k_iSteamUserCallbacks = 100;
	public const int k_iSteamGameServerCallbacks = 200;
	public const int k_iSteamFriendsCallbacks = 300;
	public const int k_iSteamBillingCallbacks = 400;
	public const int k_iSteamContentServerCallbacks = 600;
	public const int k_iSteamUtilsCallbacks = 700;
	public const int k_iSteamAppsCallbacks = 1000;
	public const int k_iSteamUserStatsCallbacks = 1100;
	public const int k_iSteamNetworkingCallbacks = 1200;
	public const int k_iSteamNetworkingSocketsCallbacks = 1220;
	public const int k_iSteamNetworkingMessagesCallbacks = 1250;
	public const int k_iSteamNetworkingUtilsCallbacks = 1280;
	public const int k_iSteamRemoteStorageCallbacks = 1300;
	public const int k_iSteamGameServerItemsCallbacks = 1500;
	public const int k_iSteamGameCoordinatorCallbacks = 1700;
	public const int k_iSteamGameServerStatsCallbacks = 1800;
	public const int k_iSteam2AsyncCallbacks = 1900;
	public const int k_iSteamGameStatsCallbacks = 2000;
	public const int k_iSteamHTTPCallbacks = 2100;
	public const int k_iSteamScreenshotsCallbacks = 2300;
	/// <summary>
	/// <para>NOTE: 2500-2599 are reserved</para>
	/// </summary>
	public const int k_iSteamStreamLauncherCallbacks = 2600;
	public const int k_iSteamControllerCallbacks = 2800;
	public const int k_iSteamUGCCallbacks = 3400;
	public const int k_iSteamStreamClientCallbacks = 3500;
	public const int k_iSteamMusicCallbacks = 4000;
	public const int k_iSteamMusicRemoteCallbacks = 4100;
	public const int k_iSteamGameNotificationCallbacks = 4400;
	public const int k_iSteamHTMLSurfaceCallbacks = 4500;
	public const int k_iSteamVideoCallbacks = 4600;
	public const int k_iSteamInventoryCallbacks = 4700;
	public const int k_ISteamParentalSettingsCallbacks = 5000;
	public const int k_iSteamGameSearchCallbacks = 5200;
	public const int k_iSteamPartiesCallbacks = 5300;
	public const int k_iSteamSTARCallbacks = 5500;
	public const int k_iSteamRemotePlayCallbacks = 5700;
	public const int k_iSteamChatCallbacks = 5900;
	public const int k_iSteamTimelineCallbacks = 6000;
	/// <summary>
	/// <para>Pass to SteamGameServer_Init to indicate that the same UDP port will be used for game traffic</para>
	/// <para>UDP queries for server browser pings and LAN discovery.  In this case, Steam will not open up a</para>
	/// <para>socket to handle server browser queries, and you must use ISteamGameServer::HandleIncomingPacket</para>
	/// <para>and ISteamGameServer::GetNextOutgoingPacket to handle packets related to server discovery on your socket.</para>
	/// </summary>
	public const ushort STEAMGAMESERVER_QUERY_PORT_SHARED = 0xffff;
	public const int k_unSteamAccountIDMask = -1;
	public const int k_unSteamAccountInstanceMask = 0x000FFFFF;
	/// <summary>
	/// <para>fixed instance for all individual users</para>
	/// </summary>
	public const int k_unSteamUserDefaultInstance = 1;
	public const int k_cchGameExtraInfoMax = 64;
	/// <summary>
	/// <para>Port number(s) assigned to us.  Only the first entries will contain</para>
	/// <para>nonzero values.  Entries corresponding to ports beyond what was</para>
	/// <para>allocated for you will be zero.</para>
	/// <para>(NOTE: At the time of this writing, the maximum number of ports you may</para>
	/// <para>request is 4.)</para>
	/// </summary>
	public const int k_nMaxReturnPorts = 8;
	/// <summary>
	/// <para>Max length of diagnostic error message</para>
	/// </summary>
	public const int k_cchMaxSteamNetworkingErrMsg = 1024;
	/// <summary>
	/// <para>Max length, in bytes (including null terminator) of the reason string</para>
	/// <para>when a connection is closed.</para>
	/// </summary>
	public const int k_cchSteamNetworkingMaxConnectionCloseReason = 128;
	/// <summary>
	/// <para>Max length, in bytes (include null terminator) of debug description</para>
	/// <para>of a connection.</para>
	/// </summary>
	public const int k_cchSteamNetworkingMaxConnectionDescription = 128;
	/// <summary>
	/// <para>Max length of the app&apos;s part of the description</para>
	/// </summary>
	public const int k_cchSteamNetworkingMaxConnectionAppName = 32;
	/// <summary>
	/// <para>We don&apos;t have a certificate for the remote host.</para>
	/// </summary>
	public const int k_nSteamNetworkConnectionInfoFlags_Unauthenticated = 1;
	/// <summary>
	/// <para>Information is being sent out over a wire unencrypted (by this library)</para>
	/// </summary>
	public const int k_nSteamNetworkConnectionInfoFlags_Unencrypted = 2;
	/// <summary>
	/// <para>Internal loopback buffers.  Won&apos;t be true for localhost.  (You can check the address to determine that.)  This implies k_nSteamNetworkConnectionInfoFlags_FastLAN</para>
	/// </summary>
	public const int k_nSteamNetworkConnectionInfoFlags_LoopbackBuffers = 4;
	/// <summary>
	/// <para>The connection is &quot;fast&quot; and &quot;reliable&quot;.  Either internal/localhost (check the address to find out), or the peer is on the same LAN.  (Probably.  It&apos;s based on the address and the ping time, this is actually hard to determine unambiguously).</para>
	/// </summary>
	public const int k_nSteamNetworkConnectionInfoFlags_Fast = 8;
	/// <summary>
	/// <para>The connection is relayed somehow (SDR or TURN).</para>
	/// </summary>
	public const int k_nSteamNetworkConnectionInfoFlags_Relayed = 16;
	/// <summary>
	/// <para>We&apos;re taking advantage of dual-wifi multi-path</para>
	/// </summary>
	public const int k_nSteamNetworkConnectionInfoFlags_DualWifi = 32;
	/// <summary>
	/// <para>Network messages</para>
	/// <para>Max size of a single message that we can SEND.</para>
	/// <para>Note: We might be wiling to receive larger messages,</para>
	/// <para>and our peer might, too.</para>
	/// </summary>
	public const int k_cbMaxSteamNetworkingSocketsMessageSizeSend = 512 * 1024;
	/// <summary>
	/// <para>Flags used to set options for message sending</para>
	/// <para>Send the message unreliably. Can be lost.  Messages *can* be larger than a</para>
	/// <para>single MTU (UDP packet), but there is no retransmission, so if any piece</para>
	/// <para>of the message is lost, the entire message will be dropped.</para>
	/// <para>The sending API does have some knowledge of the underlying connection, so</para>
	/// <para>if there is no NAT-traversal accomplished or there is a recognized adjustment</para>
	/// <para>happening on the connection, the packet will be batched until the connection</para>
	/// <para>is open again.</para>
	/// <para>Migration note: This is not exactly the same as k_EP2PSendUnreliable!  You</para>
	/// <para>probably want k_ESteamNetworkingSendType_UnreliableNoNagle</para>
	/// </summary>
	public const int k_nSteamNetworkingSend_Unreliable = 0;
	/// <summary>
	/// <para>Disable Nagle&apos;s algorithm.</para>
	/// <para>By default, Nagle&apos;s algorithm is applied to all outbound messages.  This means</para>
	/// <para>that the message will NOT be sent immediately, in case further messages are</para>
	/// <para>sent soon after you send this, which can be grouped together.  Any time there</para>
	/// <para>is enough buffered data to fill a packet, the packets will be pushed out immediately,</para>
	/// <para>but partially-full packets not be sent until the Nagle timer expires.  See</para>
	/// <para>ISteamNetworkingSockets::FlushMessagesOnConnection, ISteamNetworkingMessages::FlushMessagesToUser</para>
	/// <para>NOTE: Don&apos;t just send every message without Nagle because you want packets to get there</para>
	/// <para>quicker.  Make sure you understand the problem that Nagle is solving before disabling it.</para>
	/// <para>If you are sending small messages, often many at the same time, then it is very likely that</para>
	/// <para>it will be more efficient to leave Nagle enabled.  A typical proper use of this flag is</para>
	/// <para>when you are sending what you know will be the last message sent for a while (e.g. the last</para>
	/// <para>in the server simulation tick to a particular client), and you use this flag to flush all</para>
	/// <para>messages.</para>
	/// </summary>
	public const int k_nSteamNetworkingSend_NoNagle = 1;
	/// <summary>
	/// <para>Send a message unreliably, bypassing Nagle&apos;s algorithm for this message and any messages</para>
	/// <para>currently pending on the Nagle timer.  This is equivalent to using k_ESteamNetworkingSend_Unreliable</para>
	/// <para>and then immediately flushing the messages using ISteamNetworkingSockets::FlushMessagesOnConnection</para>
	/// <para>or ISteamNetworkingMessages::FlushMessagesToUser.  (But using this flag is more efficient since you</para>
	/// <para>only make one API call.)</para>
	/// </summary>
	public const int k_nSteamNetworkingSend_UnreliableNoNagle = k_nSteamNetworkingSend_Unreliable | k_nSteamNetworkingSend_NoNagle;
	/// <summary>
	/// <para>If the message cannot be sent very soon (because the connection is still doing some initial</para>
	/// <para>handshaking, route negotiations, etc), then just drop it.  This is only applicable for unreliable</para>
	/// <para>messages.  Using this flag on reliable messages is invalid.</para>
	/// </summary>
	public const int k_nSteamNetworkingSend_NoDelay = 4;
	/// <summary>
	/// <para>Send an unreliable message, but if it cannot be sent relatively quickly, just drop it instead of queuing it.</para>
	/// <para>This is useful for messages that are not useful if they are excessively delayed, such as voice data.</para>
	/// <para>NOTE: The Nagle algorithm is not used, and if the message is not dropped, any messages waiting on the</para>
	/// <para>Nagle timer are immediately flushed.</para>
	/// <para>A message will be dropped under the following circumstances:</para>
	/// <para>- the connection is not fully connected.  (E.g. the &quot;Connecting&quot; or &quot;FindingRoute&quot; states)</para>
	/// <para>- there is a sufficiently large number of messages queued up already such that the current message</para>
	/// <para>will not be placed on the wire in the next ~200ms or so.</para>
	/// <para>If a message is dropped for these reasons, k_EResultIgnored will be returned.</para>
	/// </summary>
	public const int k_nSteamNetworkingSend_UnreliableNoDelay = k_nSteamNetworkingSend_Unreliable | k_nSteamNetworkingSend_NoDelay | k_nSteamNetworkingSend_NoNagle;
	/// <summary>
	/// <para>Reliable message send. Can send up to k_cbMaxSteamNetworkingSocketsMessageSizeSend bytes in a single message.</para>
	/// <para>Does fragmentation/re-assembly of messages under the hood, as well as a sliding window for</para>
	/// <para>efficient sends of large chunks of data.</para>
	/// <para>The Nagle algorithm is used.  See notes on k_ESteamNetworkingSendType_Unreliable for more details.</para>
	/// <para>See k_ESteamNetworkingSendType_ReliableNoNagle, ISteamNetworkingSockets::FlushMessagesOnConnection,</para>
	/// <para>ISteamNetworkingMessages::FlushMessagesToUser</para>
	/// <para>Migration note: This is NOT the same as k_EP2PSendReliable, it&apos;s more like k_EP2PSendReliableWithBuffering</para>
	/// </summary>
	public const int k_nSteamNetworkingSend_Reliable = 8;
	/// <summary>
	/// <para>Send a message reliably, but bypass Nagle&apos;s algorithm.</para>
	/// <para>Migration note: This is equivalent to k_EP2PSendReliable</para>
	/// </summary>
	public const int k_nSteamNetworkingSend_ReliableNoNagle = k_nSteamNetworkingSend_Reliable | k_nSteamNetworkingSend_NoNagle;
	/// <summary>
	/// <para>By default, message sending is queued, and the work of encryption and talking to</para>
	/// <para>the operating system sockets, etc is done on a service thread.  This is usually a</para>
	/// <para>a performance win when messages are sent from the &quot;main thread&quot;.  However, if this</para>
	/// <para>flag is set, and data is ready to be sent immediately (either from this message</para>
	/// <para>or earlier queued data), then that work will be done in the current thread, before</para>
	/// <para>the current call returns.  If data is not ready to be sent (due to rate limiting</para>
	/// <para>or Nagle), then this flag has no effect.</para>
	/// <para>This is an advanced flag used to control performance at a very low level.  For</para>
	/// <para>most applications running on modern hardware with more than one CPU core, doing</para>
	/// <para>the work of sending on a service thread will yield the best performance.  Only</para>
	/// <para>use this flag if you have a really good reason and understand what you are doing.</para>
	/// <para>Otherwise you will probably just make performance worse.</para>
	/// </summary>
	public const int k_nSteamNetworkingSend_UseCurrentThread = 16;
	/// <summary>
	/// <para>When sending a message using ISteamNetworkingMessages, automatically re-establish</para>
	/// <para>a broken session, without returning k_EResultNoConnection.  Without this flag,</para>
	/// <para>if you attempt to send a message, and the session was proactively closed by the</para>
	/// <para>peer, or an error occurred that disrupted communications, then you must close the</para>
	/// <para>session using ISteamNetworkingMessages::CloseSessionWithUser before attempting to</para>
	/// <para>send another message.  (Or you can simply add this flag and retry.)  In this way,</para>
	/// <para>the disruption cannot go unnoticed, and a more clear order of events can be</para>
	/// <para>ascertained. This is especially important when reliable messages are used, since</para>
	/// <para>if the connection is disrupted, some of those messages will not have been delivered,</para>
	/// <para>and it is in general not possible to know which.  Although a</para>
	/// <para>SteamNetworkingMessagesSessionFailed_t callback will be posted when an error occurs</para>
	/// <para>to notify you that a failure has happened, callbacks are asynchronous, so it is not</para>
	/// <para>possible to tell exactly when it happened.  And because the primary purpose of</para>
	/// <para>ISteamNetworkingMessages is to be like UDP, there is no notification when a peer closes</para>
	/// <para>the session.</para>
	/// <para>If you are not using any reliable messages (e.g. you are using ISteamNetworkingMessages</para>
	/// <para>exactly as a transport replacement for UDP-style datagrams only), you may not need to</para>
	/// <para>know when an underlying connection fails, and so you may not need this notification.</para>
	/// </summary>
	public const int k_nSteamNetworkingSend_AutoRestartBrokenSession = 32;
	/// <summary>
	/// <para>Max possible length of a ping location, in string format.  This is</para>
	/// <para>an extremely conservative worst case value which leaves room for future</para>
	/// <para>syntax enhancements.  Most strings in practice are a lot shorter.</para>
	/// <para>If you are storing many of these, you will very likely benefit from</para>
	/// <para>using dynamic memory.</para>
	/// </summary>
	public const int k_cchMaxSteamNetworkingPingLocationString = 1024;
	/// <summary>
	/// <para>Special values that are returned by some functions that return a ping.</para>
	/// </summary>
	public const int k_nSteamNetworkingPing_Failed = -1;
	public const int k_nSteamNetworkingPing_Unknown = -2;
	/// <summary>
	/// <para>Bitmask of types to share</para>
	/// <para>Special value - use user defaults</para>
	/// </summary>
	public const int k_nSteamNetworkingConfig_P2P_Transport_ICE_Enable_Default = -1;
	/// <summary>
	/// <para>Do not do any ICE work at all or share any IP addresses with peer</para>
	/// </summary>
	public const int k_nSteamNetworkingConfig_P2P_Transport_ICE_Enable_Disable = 0;
	/// <summary>
	/// <para>Relayed connection via TURN server.</para>
	/// </summary>
	public const int k_nSteamNetworkingConfig_P2P_Transport_ICE_Enable_Relay = 1;
	/// <summary>
	/// <para>host addresses that appear to be link-local or RFC1918 addresses</para>
	/// </summary>
	public const int k_nSteamNetworkingConfig_P2P_Transport_ICE_Enable_Private = 2;
	/// <summary>
	/// <para>STUN reflexive addresses, or host address that isn&apos;t a &quot;private&quot; address</para>
	/// </summary>
	public const int k_nSteamNetworkingConfig_P2P_Transport_ICE_Enable_Public = 4;
	public const int k_nSteamNetworkingConfig_P2P_Transport_ICE_Enable_All = 0x7fffffff;
	public const int k_uAccountIdInvalid = 0;
	public const ulong k_ulPartyBeaconIdInvalid = 0;
	public const int INVALID_HTTPREQUEST_HANDLE		= 0;
	public const int STEAM_INPUT_MAX_COUNT = 16;
	public const int STEAM_INPUT_MAX_ANALOG_ACTIONS = 24;
	public const int STEAM_INPUT_MAX_DIGITAL_ACTIONS = 256;
	public const int STEAM_INPUT_MAX_ORIGINS = 8;
	public const int STEAM_INPUT_MAX_ACTIVE_LAYERS = 16;
	/// <summary>
	/// <para>When sending an option to a specific controller handle, you can send to all devices via this command</para>
	/// </summary>
	public const ulong STEAM_INPUT_HANDLE_ALL_CONTROLLERS = 0xFFFFFFFFFFFFFFFF;
	public const float STEAM_INPUT_MIN_ANALOG_ACTION_DATA = -1.0f;
	public const float STEAM_INPUT_MAX_ANALOG_ACTION_DATA = 1.0f;
	public const int k_SteamMusicNameMaxLength = 255;
	public const int k_SteamMusicPNGMaxLength = 65535;
}
