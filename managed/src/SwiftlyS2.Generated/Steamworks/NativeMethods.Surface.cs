using System.Runtime.InteropServices;
using SwiftlyS2.Core.Natives;

namespace SwiftlyS2.Shared.SteamAPI;

internal static unsafe partial class NativeMethods
{
	internal static delegate* unmanaged[Cdecl]<byte> _SteamAPI_Init;
	internal static delegate* unmanaged[Cdecl]<void> _SteamAPI_Shutdown;
	internal static delegate* unmanaged[Cdecl]<AppId_t, byte> _SteamAPI_RestartAppIfNecessary;
	internal static delegate* unmanaged[Cdecl]<void> _SteamAPI_ReleaseCurrentThreadMemory;
	internal static delegate* unmanaged[Cdecl]<void> _SteamAPI_RunCallbacks;
	internal static delegate* unmanaged[Cdecl]<nint, int, void> _SteamAPI_RegisterCallback;
	internal static delegate* unmanaged[Cdecl]<nint, void> _SteamAPI_UnregisterCallback;
	internal static delegate* unmanaged[Cdecl]<nint, ulong, void> _SteamAPI_RegisterCallResult;
	internal static delegate* unmanaged[Cdecl]<nint, ulong, void> _SteamAPI_UnregisterCallResult;
	internal static delegate* unmanaged[Cdecl]<byte> _SteamAPI_IsSteamRunning;
	internal static delegate* unmanaged[Cdecl]<int> _SteamAPI_GetHSteamPipe;
	internal static delegate* unmanaged[Cdecl]<int> _SteamAPI_GetHSteamUser;
	internal static delegate* unmanaged[Cdecl]<byte*, nint> _SteamInternal_CreateInterface;
	internal static delegate* unmanaged[Cdecl]<HSteamUser, byte*, nint> _SteamInternal_FindOrCreateUserInterface;
	internal static delegate* unmanaged[Cdecl]<HSteamUser, byte*, nint> _SteamInternal_FindOrCreateGameServerInterface;
	internal static delegate* unmanaged[Cdecl]<void> _SteamGameServer_Shutdown;
	internal static delegate* unmanaged[Cdecl]<void> _SteamGameServer_RunCallbacks;
	internal static delegate* unmanaged[Cdecl]<void> _SteamGameServer_ReleaseCurrentThreadMemory;
	internal static delegate* unmanaged[Cdecl]<byte> _SteamGameServer_BSecure;
	internal static delegate* unmanaged[Cdecl]<ulong> _SteamGameServer_GetSteamID;
	internal static delegate* unmanaged[Cdecl]<int> _SteamGameServer_GetHSteamPipe;
	internal static delegate* unmanaged[Cdecl]<int> _SteamGameServer_GetHSteamUser;
	internal static delegate* unmanaged[Cdecl]<SteamNetworkingIPAddr*, void> _SteamAPI_SteamNetworkingIPAddr_Clear;
	internal static delegate* unmanaged[Cdecl]<SteamNetworkingIPAddr*, byte> _SteamAPI_SteamNetworkingIPAddr_IsIPv6AllZeros;
	internal static delegate* unmanaged[Cdecl]<SteamNetworkingIPAddr*, byte*, ushort, void> _SteamAPI_SteamNetworkingIPAddr_SetIPv6;
	internal static delegate* unmanaged[Cdecl]<SteamNetworkingIPAddr*, uint, ushort, void> _SteamAPI_SteamNetworkingIPAddr_SetIPv4;
	internal static delegate* unmanaged[Cdecl]<SteamNetworkingIPAddr*, byte> _SteamAPI_SteamNetworkingIPAddr_IsIPv4;
	internal static delegate* unmanaged[Cdecl]<SteamNetworkingIPAddr*, uint> _SteamAPI_SteamNetworkingIPAddr_GetIPv4;
	internal static delegate* unmanaged[Cdecl]<SteamNetworkingIPAddr*, ushort, void> _SteamAPI_SteamNetworkingIPAddr_SetIPv6LocalHost;
	internal static delegate* unmanaged[Cdecl]<SteamNetworkingIPAddr*, byte> _SteamAPI_SteamNetworkingIPAddr_IsLocalHost;
	internal static delegate* unmanaged[Cdecl]<SteamNetworkingIPAddr*, nint, uint, byte, void> _SteamAPI_SteamNetworkingIPAddr_ToString;
	internal static delegate* unmanaged[Cdecl]<SteamNetworkingIPAddr*, byte*, byte> _SteamAPI_SteamNetworkingIPAddr_ParseString;
	internal static delegate* unmanaged[Cdecl]<SteamNetworkingIPAddr*, SteamNetworkingIPAddr*, byte> _SteamAPI_SteamNetworkingIPAddr_IsEqualTo;
	internal static delegate* unmanaged[Cdecl]<SteamNetworkingIPAddr*, ESteamNetworkingFakeIPType> _SteamAPI_SteamNetworkingIPAddr_GetFakeIPType;
	internal static delegate* unmanaged[Cdecl]<SteamNetworkingIdentity*, void> _SteamAPI_SteamNetworkingIdentity_Clear;
	internal static delegate* unmanaged[Cdecl]<SteamNetworkingIdentity*, byte> _SteamAPI_SteamNetworkingIdentity_IsInvalid;
	internal static delegate* unmanaged[Cdecl]<SteamNetworkingIdentity*, ulong, void> _SteamAPI_SteamNetworkingIdentity_SetSteamID;
	internal static delegate* unmanaged[Cdecl]<SteamNetworkingIdentity*, ulong> _SteamAPI_SteamNetworkingIdentity_GetSteamID;
	internal static delegate* unmanaged[Cdecl]<SteamNetworkingIdentity*, ulong, void> _SteamAPI_SteamNetworkingIdentity_SetSteamID64;
	internal static delegate* unmanaged[Cdecl]<SteamNetworkingIdentity*, ulong> _SteamAPI_SteamNetworkingIdentity_GetSteamID64;
	internal static delegate* unmanaged[Cdecl]<SteamNetworkingIdentity*, byte*, byte> _SteamAPI_SteamNetworkingIdentity_SetXboxPairwiseID;
	internal static delegate* unmanaged[Cdecl]<SteamNetworkingIdentity*, nint> _SteamAPI_SteamNetworkingIdentity_GetXboxPairwiseID;
	internal static delegate* unmanaged[Cdecl]<SteamNetworkingIdentity*, ulong, void> _SteamAPI_SteamNetworkingIdentity_SetPSNID;
	internal static delegate* unmanaged[Cdecl]<SteamNetworkingIdentity*, ulong> _SteamAPI_SteamNetworkingIdentity_GetPSNID;
	internal static delegate* unmanaged[Cdecl]<SteamNetworkingIdentity*, ulong, void> _SteamAPI_SteamNetworkingIdentity_SetStadiaID;
	internal static delegate* unmanaged[Cdecl]<SteamNetworkingIdentity*, ulong> _SteamAPI_SteamNetworkingIdentity_GetStadiaID;
	internal static delegate* unmanaged[Cdecl]<SteamNetworkingIdentity*, SteamNetworkingIPAddr*, nint> _SteamAPI_SteamNetworkingIdentity_SetIPAddr;
	internal static delegate* unmanaged[Cdecl]<SteamNetworkingIdentity*, nint> _SteamAPI_SteamNetworkingIdentity_GetIPAddr;
	internal static delegate* unmanaged[Cdecl]<SteamNetworkingIdentity*, uint, ushort, void> _SteamAPI_SteamNetworkingIdentity_SetIPv4Addr;
	internal static delegate* unmanaged[Cdecl]<SteamNetworkingIdentity*, uint> _SteamAPI_SteamNetworkingIdentity_GetIPv4;
	internal static delegate* unmanaged[Cdecl]<SteamNetworkingIdentity*, ESteamNetworkingFakeIPType> _SteamAPI_SteamNetworkingIdentity_GetFakeIPType;
	internal static delegate* unmanaged[Cdecl]<SteamNetworkingIdentity*, void> _SteamAPI_SteamNetworkingIdentity_SetLocalHost;
	internal static delegate* unmanaged[Cdecl]<SteamNetworkingIdentity*, byte> _SteamAPI_SteamNetworkingIdentity_IsLocalHost;
	internal static delegate* unmanaged[Cdecl]<SteamNetworkingIdentity*, byte*, byte> _SteamAPI_SteamNetworkingIdentity_SetGenericString;
	internal static delegate* unmanaged[Cdecl]<SteamNetworkingIdentity*, nint> _SteamAPI_SteamNetworkingIdentity_GetGenericString;
	internal static delegate* unmanaged[Cdecl]<SteamNetworkingIdentity*, byte*, uint, byte> _SteamAPI_SteamNetworkingIdentity_SetGenericBytes;
	internal static delegate* unmanaged[Cdecl]<SteamNetworkingIdentity*, int*, nint> _SteamAPI_SteamNetworkingIdentity_GetGenericBytes;
	internal static delegate* unmanaged[Cdecl]<SteamNetworkingIdentity*, SteamNetworkingIdentity*, byte> _SteamAPI_SteamNetworkingIdentity_IsEqualTo;
	internal static delegate* unmanaged[Cdecl]<SteamNetworkingIdentity*, nint, uint, void> _SteamAPI_SteamNetworkingIdentity_ToString;
	internal static delegate* unmanaged[Cdecl]<SteamNetworkingIdentity*, byte*, byte> _SteamAPI_SteamNetworkingIdentity_ParseString;
	internal static delegate* unmanaged[Cdecl]<nint, void> _SteamAPI_SteamNetworkingMessage_t_Release;
	internal static delegate* unmanaged[Cdecl]<ISteamNetworkingConnectionSignaling*, HSteamNetConnection, SteamNetConnectionInfo_t*, nint, int, byte> _SteamAPI_ISteamNetworkingConnectionSignaling_SendSignal;
	internal static delegate* unmanaged[Cdecl]<ISteamNetworkingConnectionSignaling*, void> _SteamAPI_ISteamNetworkingConnectionSignaling_Release;
	internal static delegate* unmanaged[Cdecl]<ISteamNetworkingSignalingRecvContext*, HSteamNetConnection, SteamNetworkingIdentity*, int, nint> _SteamAPI_ISteamNetworkingSignalingRecvContext_OnConnectRequest;
	internal static delegate* unmanaged[Cdecl]<ISteamNetworkingSignalingRecvContext*, SteamNetworkingIdentity*, nint, int, void> _SteamAPI_ISteamNetworkingSignalingRecvContext_SendRejectionSignal;
	internal static delegate* unmanaged[Cdecl]<nint, int> _ISteamClient_CreateSteamPipe;
	internal static delegate* unmanaged[Cdecl]<nint, HSteamPipe, byte> _ISteamClient_BReleaseSteamPipe;
	internal static delegate* unmanaged[Cdecl]<nint, HSteamPipe, int> _ISteamClient_ConnectToGlobalUser;
	internal static delegate* unmanaged[Cdecl]<nint, HSteamPipe*, EAccountType, int> _ISteamClient_CreateLocalUser;
	internal static delegate* unmanaged[Cdecl]<nint, HSteamPipe, HSteamUser, void> _ISteamClient_ReleaseUser;
	internal static delegate* unmanaged[Cdecl]<nint, HSteamUser, HSteamPipe, byte*, nint> _ISteamClient_GetISteamUser;
	internal static delegate* unmanaged[Cdecl]<nint, HSteamUser, HSteamPipe, byte*, nint> _ISteamClient_GetISteamGameServer;
	internal static delegate* unmanaged[Cdecl]<nint, SteamIPAddress_t*, ushort, void> _ISteamClient_SetLocalIPBinding;
	internal static delegate* unmanaged[Cdecl]<nint, HSteamUser, HSteamPipe, byte*, nint> _ISteamClient_GetISteamFriends;
	internal static delegate* unmanaged[Cdecl]<nint, HSteamPipe, byte*, nint> _ISteamClient_GetISteamUtils;
	internal static delegate* unmanaged[Cdecl]<nint, HSteamUser, HSteamPipe, byte*, nint> _ISteamClient_GetISteamGenericInterface;
	internal static delegate* unmanaged[Cdecl]<nint, HSteamUser, HSteamPipe, byte*, nint> _ISteamClient_GetISteamUserStats;
	internal static delegate* unmanaged[Cdecl]<nint, HSteamUser, HSteamPipe, byte*, nint> _ISteamClient_GetISteamGameServerStats;
	internal static delegate* unmanaged[Cdecl]<nint, HSteamUser, HSteamPipe, byte*, nint> _ISteamClient_GetISteamApps;
	internal static delegate* unmanaged[Cdecl]<nint, HSteamUser, HSteamPipe, byte*, nint> _ISteamClient_GetISteamNetworking;
	internal static delegate* unmanaged[Cdecl]<nint, HSteamUser, HSteamPipe, byte*, nint> _ISteamClient_GetISteamRemoteStorage;
	internal static delegate* unmanaged[Cdecl]<nint, HSteamUser, HSteamPipe, byte*, nint> _ISteamClient_GetISteamScreenshots;
	internal static delegate* unmanaged[Cdecl]<nint, HSteamUser, HSteamPipe, byte*, nint> _ISteamClient_GetISteamGameSearch;
	internal static delegate* unmanaged[Cdecl]<nint, uint> _ISteamClient_GetIPCCallCount;
	internal static delegate* unmanaged[Cdecl]<nint, nint, void> _ISteamClient_SetWarningMessageHook;
	internal static delegate* unmanaged[Cdecl]<nint, byte> _ISteamClient_BShutdownIfAllPipesClosed;
	internal static delegate* unmanaged[Cdecl]<nint, HSteamUser, HSteamPipe, byte*, nint> _ISteamClient_GetISteamHTTP;
	internal static delegate* unmanaged[Cdecl]<nint, HSteamUser, HSteamPipe, byte*, nint> _ISteamClient_GetISteamController;
	internal static delegate* unmanaged[Cdecl]<nint, HSteamUser, HSteamPipe, byte*, nint> _ISteamClient_GetISteamUGC;
	internal static delegate* unmanaged[Cdecl]<nint, HSteamUser, HSteamPipe, byte*, nint> _ISteamClient_GetISteamMusic;
	internal static delegate* unmanaged[Cdecl]<nint, HSteamUser, HSteamPipe, byte*, nint> _ISteamClient_GetISteamMusicRemote;
	internal static delegate* unmanaged[Cdecl]<nint, HSteamUser, HSteamPipe, byte*, nint> _ISteamClient_GetISteamHTMLSurface;
	internal static delegate* unmanaged[Cdecl]<nint, HSteamUser, HSteamPipe, byte*, nint> _ISteamClient_GetISteamInventory;
	internal static delegate* unmanaged[Cdecl]<nint, HSteamUser, HSteamPipe, byte*, nint> _ISteamClient_GetISteamVideo;
	internal static delegate* unmanaged[Cdecl]<nint, HSteamUser, HSteamPipe, byte*, nint> _ISteamClient_GetISteamParentalSettings;
	internal static delegate* unmanaged[Cdecl]<nint, HSteamUser, HSteamPipe, byte*, nint> _ISteamClient_GetISteamInput;
	internal static delegate* unmanaged[Cdecl]<nint, HSteamUser, HSteamPipe, byte*, nint> _ISteamClient_GetISteamParties;
	internal static delegate* unmanaged[Cdecl]<nint, HSteamUser, HSteamPipe, byte*, nint> _ISteamClient_GetISteamRemotePlay;
	internal static delegate* unmanaged[Cdecl]<nint, byte*, void> _ISteamGameServer_SetProduct;
	internal static delegate* unmanaged[Cdecl]<nint, byte*, void> _ISteamGameServer_SetGameDescription;
	internal static delegate* unmanaged[Cdecl]<nint, byte*, void> _ISteamGameServer_SetModDir;
	internal static delegate* unmanaged[Cdecl]<nint, byte, void> _ISteamGameServer_SetDedicatedServer;
	internal static delegate* unmanaged[Cdecl]<nint, byte*, void> _ISteamGameServer_LogOn;
	internal static delegate* unmanaged[Cdecl]<nint, void> _ISteamGameServer_LogOnAnonymous;
	internal static delegate* unmanaged[Cdecl]<nint, void> _ISteamGameServer_LogOff;
	internal static delegate* unmanaged[Cdecl]<nint, byte> _ISteamGameServer_BLoggedOn;
	internal static delegate* unmanaged[Cdecl]<nint, byte> _ISteamGameServer_BSecure;
	internal static delegate* unmanaged[Cdecl]<nint, ulong> _ISteamGameServer_GetSteamID;
	internal static delegate* unmanaged[Cdecl]<nint, byte> _ISteamGameServer_WasRestartRequested;
	internal static delegate* unmanaged[Cdecl]<nint, int, void> _ISteamGameServer_SetMaxPlayerCount;
	internal static delegate* unmanaged[Cdecl]<nint, int, void> _ISteamGameServer_SetBotPlayerCount;
	internal static delegate* unmanaged[Cdecl]<nint, byte*, void> _ISteamGameServer_SetServerName;
	internal static delegate* unmanaged[Cdecl]<nint, byte*, void> _ISteamGameServer_SetMapName;
	internal static delegate* unmanaged[Cdecl]<nint, byte, void> _ISteamGameServer_SetPasswordProtected;
	internal static delegate* unmanaged[Cdecl]<nint, ushort, void> _ISteamGameServer_SetSpectatorPort;
	internal static delegate* unmanaged[Cdecl]<nint, byte*, void> _ISteamGameServer_SetSpectatorServerName;
	internal static delegate* unmanaged[Cdecl]<nint, void> _ISteamGameServer_ClearAllKeyValues;
	internal static delegate* unmanaged[Cdecl]<nint, byte*, byte*, void> _ISteamGameServer_SetKeyValue;
	internal static delegate* unmanaged[Cdecl]<nint, byte*, void> _ISteamGameServer_SetGameTags;
	internal static delegate* unmanaged[Cdecl]<nint, byte*, void> _ISteamGameServer_SetGameData;
	internal static delegate* unmanaged[Cdecl]<nint, byte*, void> _ISteamGameServer_SetRegion;
	internal static delegate* unmanaged[Cdecl]<nint, byte, void> _ISteamGameServer_SetAdvertiseServerActive;
	internal static delegate* unmanaged[Cdecl]<nint, byte*, int, uint*, SteamNetworkingIdentity*, uint> _ISteamGameServer_GetAuthSessionTicket;
	internal static delegate* unmanaged[Cdecl]<nint, byte*, int, CSteamID, EBeginAuthSessionResult> _ISteamGameServer_BeginAuthSession;
	internal static delegate* unmanaged[Cdecl]<nint, CSteamID, void> _ISteamGameServer_EndAuthSession;
	internal static delegate* unmanaged[Cdecl]<nint, HAuthTicket, void> _ISteamGameServer_CancelAuthTicket;
	internal static delegate* unmanaged[Cdecl]<nint, CSteamID, AppId_t, EUserHasLicenseForAppResult> _ISteamGameServer_UserHasLicenseForApp;
	internal static delegate* unmanaged[Cdecl]<nint, CSteamID, CSteamID, byte> _ISteamGameServer_RequestUserGroupStatus;
	internal static delegate* unmanaged[Cdecl]<nint, void> _ISteamGameServer_GetGameplayStats;
	internal static delegate* unmanaged[Cdecl]<nint, ulong> _ISteamGameServer_GetServerReputation;
	internal static delegate* unmanaged[Cdecl]<nint, SteamIPAddress_t> _ISteamGameServer_GetPublicIP;
	internal static delegate* unmanaged[Cdecl]<nint, byte*, int, uint, ushort, byte> _ISteamGameServer_HandleIncomingPacket;
	internal static delegate* unmanaged[Cdecl]<nint, byte*, int, uint*, ushort*, int> _ISteamGameServer_GetNextOutgoingPacket;
	internal static delegate* unmanaged[Cdecl]<nint, CSteamID, ulong> _ISteamGameServer_AssociateWithClan;
	internal static delegate* unmanaged[Cdecl]<nint, CSteamID, ulong> _ISteamGameServer_ComputeNewPlayerCompatibility;
	internal static delegate* unmanaged[Cdecl]<nint, uint, byte*, uint, CSteamID*, byte> _ISteamGameServer_SendUserConnectAndAuthenticate_DEPRECATED;
	internal static delegate* unmanaged[Cdecl]<nint, ulong> _ISteamGameServer_CreateUnauthenticatedUserConnection;
	internal static delegate* unmanaged[Cdecl]<nint, CSteamID, void> _ISteamGameServer_SendUserDisconnect_DEPRECATED;
	internal static delegate* unmanaged[Cdecl]<nint, CSteamID, byte*, uint, byte> _ISteamGameServer_BUpdateUserData;
	internal static delegate* unmanaged[Cdecl]<nint, CSteamID, ulong> _ISteamGameServerStats_RequestUserStats;
	internal static delegate* unmanaged[Cdecl]<nint, CSteamID, byte*, int*, byte> _ISteamGameServerStats_GetUserStatInt32;
	internal static delegate* unmanaged[Cdecl]<nint, CSteamID, byte*, float*, byte> _ISteamGameServerStats_GetUserStatFloat;
	internal static delegate* unmanaged[Cdecl]<nint, CSteamID, byte*, byte*, byte> _ISteamGameServerStats_GetUserAchievement;
	internal static delegate* unmanaged[Cdecl]<nint, CSteamID, byte*, int, byte> _ISteamGameServerStats_SetUserStatInt32;
	internal static delegate* unmanaged[Cdecl]<nint, CSteamID, byte*, float, byte> _ISteamGameServerStats_SetUserStatFloat;
	internal static delegate* unmanaged[Cdecl]<nint, CSteamID, byte*, float, double, byte> _ISteamGameServerStats_UpdateUserAvgRateStat;
	internal static delegate* unmanaged[Cdecl]<nint, CSteamID, byte*, byte> _ISteamGameServerStats_SetUserAchievement;
	internal static delegate* unmanaged[Cdecl]<nint, CSteamID, byte*, byte> _ISteamGameServerStats_ClearUserAchievement;
	internal static delegate* unmanaged[Cdecl]<nint, CSteamID, ulong> _ISteamGameServerStats_StoreUserStats;
	internal static delegate* unmanaged[Cdecl]<nint, EHTTPMethod, byte*, uint> _ISteamHTTP_CreateHTTPRequest;
	internal static delegate* unmanaged[Cdecl]<nint, HTTPRequestHandle, ulong, byte> _ISteamHTTP_SetHTTPRequestContextValue;
	internal static delegate* unmanaged[Cdecl]<nint, HTTPRequestHandle, uint, byte> _ISteamHTTP_SetHTTPRequestNetworkActivityTimeout;
	internal static delegate* unmanaged[Cdecl]<nint, HTTPRequestHandle, byte*, byte*, byte> _ISteamHTTP_SetHTTPRequestHeaderValue;
	internal static delegate* unmanaged[Cdecl]<nint, HTTPRequestHandle, byte*, byte*, byte> _ISteamHTTP_SetHTTPRequestGetOrPostParameter;
	internal static delegate* unmanaged[Cdecl]<nint, HTTPRequestHandle, SteamAPICall_t*, byte> _ISteamHTTP_SendHTTPRequest;
	internal static delegate* unmanaged[Cdecl]<nint, HTTPRequestHandle, SteamAPICall_t*, byte> _ISteamHTTP_SendHTTPRequestAndStreamResponse;
	internal static delegate* unmanaged[Cdecl]<nint, HTTPRequestHandle, byte> _ISteamHTTP_DeferHTTPRequest;
	internal static delegate* unmanaged[Cdecl]<nint, HTTPRequestHandle, byte> _ISteamHTTP_PrioritizeHTTPRequest;
	internal static delegate* unmanaged[Cdecl]<nint, HTTPRequestHandle, byte*, uint*, byte> _ISteamHTTP_GetHTTPResponseHeaderSize;
	internal static delegate* unmanaged[Cdecl]<nint, HTTPRequestHandle, byte*, byte*, uint, byte> _ISteamHTTP_GetHTTPResponseHeaderValue;
	internal static delegate* unmanaged[Cdecl]<nint, HTTPRequestHandle, uint*, byte> _ISteamHTTP_GetHTTPResponseBodySize;
	internal static delegate* unmanaged[Cdecl]<nint, HTTPRequestHandle, byte*, uint, byte> _ISteamHTTP_GetHTTPResponseBodyData;
	internal static delegate* unmanaged[Cdecl]<nint, HTTPRequestHandle, uint, byte*, uint, byte> _ISteamHTTP_GetHTTPStreamingResponseBodyData;
	internal static delegate* unmanaged[Cdecl]<nint, HTTPRequestHandle, byte> _ISteamHTTP_ReleaseHTTPRequest;
	internal static delegate* unmanaged[Cdecl]<nint, HTTPRequestHandle, float*, byte> _ISteamHTTP_GetHTTPDownloadProgressPct;
	internal static delegate* unmanaged[Cdecl]<nint, HTTPRequestHandle, byte*, byte*, uint, byte> _ISteamHTTP_SetHTTPRequestRawPostBody;
	internal static delegate* unmanaged[Cdecl]<nint, byte, uint> _ISteamHTTP_CreateCookieContainer;
	internal static delegate* unmanaged[Cdecl]<nint, HTTPCookieContainerHandle, byte> _ISteamHTTP_ReleaseCookieContainer;
	internal static delegate* unmanaged[Cdecl]<nint, HTTPCookieContainerHandle, byte*, byte*, byte*, byte> _ISteamHTTP_SetCookie;
	internal static delegate* unmanaged[Cdecl]<nint, HTTPRequestHandle, HTTPCookieContainerHandle, byte> _ISteamHTTP_SetHTTPRequestCookieContainer;
	internal static delegate* unmanaged[Cdecl]<nint, HTTPRequestHandle, byte*, byte> _ISteamHTTP_SetHTTPRequestUserAgentInfo;
	internal static delegate* unmanaged[Cdecl]<nint, HTTPRequestHandle, byte, byte> _ISteamHTTP_SetHTTPRequestRequiresVerifiedCertificate;
	internal static delegate* unmanaged[Cdecl]<nint, HTTPRequestHandle, uint, byte> _ISteamHTTP_SetHTTPRequestAbsoluteTimeoutMS;
	internal static delegate* unmanaged[Cdecl]<nint, HTTPRequestHandle, byte*, byte> _ISteamHTTP_GetHTTPRequestWasTimedOut;
	internal static delegate* unmanaged[Cdecl]<nint, SteamInventoryResult_t, EResult> _ISteamInventory_GetResultStatus;
	internal static delegate* unmanaged[Cdecl]<nint, SteamInventoryResult_t, SteamItemDetails_t*, uint*, byte> _ISteamInventory_GetResultItems;
	internal static delegate* unmanaged[Cdecl]<nint, SteamInventoryResult_t, uint, byte*, nint, uint*, byte> _ISteamInventory_GetResultItemProperty;
	internal static delegate* unmanaged[Cdecl]<nint, SteamInventoryResult_t, uint> _ISteamInventory_GetResultTimestamp;
	internal static delegate* unmanaged[Cdecl]<nint, SteamInventoryResult_t, CSteamID, byte> _ISteamInventory_CheckResultSteamID;
	internal static delegate* unmanaged[Cdecl]<nint, SteamInventoryResult_t, void> _ISteamInventory_DestroyResult;
	internal static delegate* unmanaged[Cdecl]<nint, SteamInventoryResult_t*, byte> _ISteamInventory_GetAllItems;
	internal static delegate* unmanaged[Cdecl]<nint, SteamInventoryResult_t*, SteamItemInstanceID_t*, uint, byte> _ISteamInventory_GetItemsByID;
	internal static delegate* unmanaged[Cdecl]<nint, SteamInventoryResult_t, byte*, uint*, byte> _ISteamInventory_SerializeResult;
	internal static delegate* unmanaged[Cdecl]<nint, SteamInventoryResult_t*, byte*, uint, byte, byte> _ISteamInventory_DeserializeResult;
	internal static delegate* unmanaged[Cdecl]<nint, SteamInventoryResult_t*, SteamItemDef_t*, uint*, uint, byte> _ISteamInventory_GenerateItems;
	internal static delegate* unmanaged[Cdecl]<nint, SteamInventoryResult_t*, byte> _ISteamInventory_GrantPromoItems;
	internal static delegate* unmanaged[Cdecl]<nint, SteamInventoryResult_t*, SteamItemDef_t, byte> _ISteamInventory_AddPromoItem;
	internal static delegate* unmanaged[Cdecl]<nint, SteamInventoryResult_t*, SteamItemDef_t*, uint, byte> _ISteamInventory_AddPromoItems;
	internal static delegate* unmanaged[Cdecl]<nint, SteamInventoryResult_t*, SteamItemInstanceID_t, uint, byte> _ISteamInventory_ConsumeItem;
	internal static delegate* unmanaged[Cdecl]<nint, SteamInventoryResult_t*, SteamItemDef_t*, uint*, uint, SteamItemInstanceID_t*, uint*, uint, byte> _ISteamInventory_ExchangeItems;
	internal static delegate* unmanaged[Cdecl]<nint, SteamInventoryResult_t*, SteamItemInstanceID_t, uint, SteamItemInstanceID_t, byte> _ISteamInventory_TransferItemQuantity;
	internal static delegate* unmanaged[Cdecl]<nint, void> _ISteamInventory_SendItemDropHeartbeat;
	internal static delegate* unmanaged[Cdecl]<nint, SteamInventoryResult_t*, SteamItemDef_t, byte> _ISteamInventory_TriggerItemDrop;
	internal static delegate* unmanaged[Cdecl]<nint, SteamInventoryResult_t*, CSteamID, SteamItemInstanceID_t*, uint*, uint, SteamItemInstanceID_t*, uint*, uint, byte> _ISteamInventory_TradeItems;
	internal static delegate* unmanaged[Cdecl]<nint, byte> _ISteamInventory_LoadItemDefinitions;
	internal static delegate* unmanaged[Cdecl]<nint, SteamItemDef_t*, uint*, byte> _ISteamInventory_GetItemDefinitionIDs;
	internal static delegate* unmanaged[Cdecl]<nint, SteamItemDef_t, byte*, nint, uint*, byte> _ISteamInventory_GetItemDefinitionProperty;
	internal static delegate* unmanaged[Cdecl]<nint, CSteamID, ulong> _ISteamInventory_RequestEligiblePromoItemDefinitionsIDs;
	internal static delegate* unmanaged[Cdecl]<nint, CSteamID, SteamItemDef_t*, uint*, byte> _ISteamInventory_GetEligiblePromoItemDefinitionIDs;
	internal static delegate* unmanaged[Cdecl]<nint, SteamItemDef_t*, uint*, uint, ulong> _ISteamInventory_StartPurchase;
	internal static delegate* unmanaged[Cdecl]<nint, ulong> _ISteamInventory_RequestPrices;
	internal static delegate* unmanaged[Cdecl]<nint, uint> _ISteamInventory_GetNumItemsWithPrices;
	internal static delegate* unmanaged[Cdecl]<nint, SteamItemDef_t*, ulong*, ulong*, uint, byte> _ISteamInventory_GetItemsWithPrices;
	internal static delegate* unmanaged[Cdecl]<nint, SteamItemDef_t, ulong*, ulong*, byte> _ISteamInventory_GetItemPrice;
	internal static delegate* unmanaged[Cdecl]<nint, ulong> _ISteamInventory_StartUpdateProperties;
	internal static delegate* unmanaged[Cdecl]<nint, SteamInventoryUpdateHandle_t, SteamItemInstanceID_t, byte*, byte> _ISteamInventory_RemoveProperty;
	internal static delegate* unmanaged[Cdecl]<nint, SteamInventoryUpdateHandle_t, SteamItemInstanceID_t, byte*, byte*, byte> _ISteamInventory_SetPropertyString;
	internal static delegate* unmanaged[Cdecl]<nint, SteamInventoryUpdateHandle_t, SteamItemInstanceID_t, byte*, byte, byte> _ISteamInventory_SetPropertyBool;
	internal static delegate* unmanaged[Cdecl]<nint, SteamInventoryUpdateHandle_t, SteamItemInstanceID_t, byte*, long, byte> _ISteamInventory_SetPropertyInt64;
	internal static delegate* unmanaged[Cdecl]<nint, SteamInventoryUpdateHandle_t, SteamItemInstanceID_t, byte*, float, byte> _ISteamInventory_SetPropertyFloat;
	internal static delegate* unmanaged[Cdecl]<nint, SteamInventoryUpdateHandle_t, SteamInventoryResult_t*, byte> _ISteamInventory_SubmitUpdateProperties;
	internal static delegate* unmanaged[Cdecl]<nint, SteamInventoryResult_t*, byte*, byte> _ISteamInventory_InspectItem;
	internal static delegate* unmanaged[Cdecl]<nint, CSteamID, byte*, uint, EP2PSend, int, byte> _ISteamNetworking_SendP2PPacket;
	internal static delegate* unmanaged[Cdecl]<nint, uint*, int, byte> _ISteamNetworking_IsP2PPacketAvailable;
	internal static delegate* unmanaged[Cdecl]<nint, byte*, uint, uint*, CSteamID*, int, byte> _ISteamNetworking_ReadP2PPacket;
	internal static delegate* unmanaged[Cdecl]<nint, CSteamID, byte> _ISteamNetworking_AcceptP2PSessionWithUser;
	internal static delegate* unmanaged[Cdecl]<nint, CSteamID, byte> _ISteamNetworking_CloseP2PSessionWithUser;
	internal static delegate* unmanaged[Cdecl]<nint, CSteamID, int, byte> _ISteamNetworking_CloseP2PChannelWithUser;
	internal static delegate* unmanaged[Cdecl]<nint, CSteamID, P2PSessionState_t*, byte> _ISteamNetworking_GetP2PSessionState;
	internal static delegate* unmanaged[Cdecl]<nint, byte, byte> _ISteamNetworking_AllowP2PPacketRelay;
	internal static delegate* unmanaged[Cdecl]<nint, int, SteamIPAddress_t, ushort, byte, uint> _ISteamNetworking_CreateListenSocket;
	internal static delegate* unmanaged[Cdecl]<nint, CSteamID, int, int, byte, uint> _ISteamNetworking_CreateP2PConnectionSocket;
	internal static delegate* unmanaged[Cdecl]<nint, SteamIPAddress_t, ushort, int, uint> _ISteamNetworking_CreateConnectionSocket;
	internal static delegate* unmanaged[Cdecl]<nint, SNetSocket_t, byte, byte> _ISteamNetworking_DestroySocket;
	internal static delegate* unmanaged[Cdecl]<nint, SNetListenSocket_t, byte, byte> _ISteamNetworking_DestroyListenSocket;
	internal static delegate* unmanaged[Cdecl]<nint, SNetSocket_t, byte*, uint, byte, byte> _ISteamNetworking_SendDataOnSocket;
	internal static delegate* unmanaged[Cdecl]<nint, SNetSocket_t, uint*, byte> _ISteamNetworking_IsDataAvailableOnSocket;
	internal static delegate* unmanaged[Cdecl]<nint, SNetSocket_t, byte*, uint, uint*, byte> _ISteamNetworking_RetrieveDataFromSocket;
	internal static delegate* unmanaged[Cdecl]<nint, SNetListenSocket_t, uint*, SNetSocket_t*, byte> _ISteamNetworking_IsDataAvailable;
	internal static delegate* unmanaged[Cdecl]<nint, SNetListenSocket_t, byte*, uint, uint*, SNetSocket_t*, byte> _ISteamNetworking_RetrieveData;
	internal static delegate* unmanaged[Cdecl]<nint, SNetSocket_t, CSteamID*, int*, SteamIPAddress_t*, ushort*, byte> _ISteamNetworking_GetSocketInfo;
	internal static delegate* unmanaged[Cdecl]<nint, SNetListenSocket_t, SteamIPAddress_t*, ushort*, byte> _ISteamNetworking_GetListenSocketInfo;
	internal static delegate* unmanaged[Cdecl]<nint, SNetSocket_t, ESNetSocketConnectionType> _ISteamNetworking_GetSocketConnectionType;
	internal static delegate* unmanaged[Cdecl]<nint, SNetSocket_t, int> _ISteamNetworking_GetMaxPacketSize;
	internal static delegate* unmanaged[Cdecl]<nint, SteamNetworkingIPAddr*, int, SteamNetworkingConfigValue_t*, uint> _ISteamNetworkingSockets_CreateListenSocketIP;
	internal static delegate* unmanaged[Cdecl]<nint, SteamNetworkingIPAddr*, int, SteamNetworkingConfigValue_t*, uint> _ISteamNetworkingSockets_ConnectByIPAddress;
	internal static delegate* unmanaged[Cdecl]<nint, int, int, SteamNetworkingConfigValue_t*, uint> _ISteamNetworkingSockets_CreateListenSocketP2P;
	internal static delegate* unmanaged[Cdecl]<nint, SteamNetworkingIdentity*, int, int, SteamNetworkingConfigValue_t*, uint> _ISteamNetworkingSockets_ConnectP2P;
	internal static delegate* unmanaged[Cdecl]<nint, HSteamNetConnection, EResult> _ISteamNetworkingSockets_AcceptConnection;
	internal static delegate* unmanaged[Cdecl]<nint, HSteamNetConnection, int, byte*, byte, byte> _ISteamNetworkingSockets_CloseConnection;
	internal static delegate* unmanaged[Cdecl]<nint, HSteamListenSocket, byte> _ISteamNetworkingSockets_CloseListenSocket;
	internal static delegate* unmanaged[Cdecl]<nint, HSteamNetConnection, long, byte> _ISteamNetworkingSockets_SetConnectionUserData;
	internal static delegate* unmanaged[Cdecl]<nint, HSteamNetConnection, long> _ISteamNetworkingSockets_GetConnectionUserData;
	internal static delegate* unmanaged[Cdecl]<nint, HSteamNetConnection, byte*, void> _ISteamNetworkingSockets_SetConnectionName;
	internal static delegate* unmanaged[Cdecl]<nint, HSteamNetConnection, nint, int, byte> _ISteamNetworkingSockets_GetConnectionName;
	internal static delegate* unmanaged[Cdecl]<nint, HSteamNetConnection, nint, uint, int, long*, EResult> _ISteamNetworkingSockets_SendMessageToConnection;
	internal static delegate* unmanaged[Cdecl]<nint, int, SteamNetworkingMessage_t*, long*, void> _ISteamNetworkingSockets_SendMessages;
	internal static delegate* unmanaged[Cdecl]<nint, HSteamNetConnection, EResult> _ISteamNetworkingSockets_FlushMessagesOnConnection;
	internal static delegate* unmanaged[Cdecl]<nint, HSteamNetConnection, nint*, int, int> _ISteamNetworkingSockets_ReceiveMessagesOnConnection;
	internal static delegate* unmanaged[Cdecl]<nint, HSteamNetConnection, SteamNetConnectionInfo_t*, byte> _ISteamNetworkingSockets_GetConnectionInfo;
	internal static delegate* unmanaged[Cdecl]<nint, HSteamNetConnection, SteamNetConnectionRealTimeStatus_t*, int, SteamNetConnectionRealTimeLaneStatus_t*, EResult> _ISteamNetworkingSockets_GetConnectionRealTimeStatus;
	internal static delegate* unmanaged[Cdecl]<nint, HSteamNetConnection, nint, int, int> _ISteamNetworkingSockets_GetDetailedConnectionStatus;
	internal static delegate* unmanaged[Cdecl]<nint, HSteamListenSocket, SteamNetworkingIPAddr*, byte> _ISteamNetworkingSockets_GetListenSocketAddress;
	internal static delegate* unmanaged[Cdecl]<nint, HSteamNetConnection*, HSteamNetConnection*, byte, SteamNetworkingIdentity*, SteamNetworkingIdentity*, byte> _ISteamNetworkingSockets_CreateSocketPair;
	internal static delegate* unmanaged[Cdecl]<nint, HSteamNetConnection, int, int*, ushort*, EResult> _ISteamNetworkingSockets_ConfigureConnectionLanes;
	internal static delegate* unmanaged[Cdecl]<nint, SteamNetworkingIdentity*, byte> _ISteamNetworkingSockets_GetIdentity;
	internal static delegate* unmanaged[Cdecl]<nint, ESteamNetworkingAvailability> _ISteamNetworkingSockets_InitAuthentication;
	internal static delegate* unmanaged[Cdecl]<nint, SteamNetAuthenticationStatus_t*, ESteamNetworkingAvailability> _ISteamNetworkingSockets_GetAuthenticationStatus;
	internal static delegate* unmanaged[Cdecl]<nint, uint> _ISteamNetworkingSockets_CreatePollGroup;
	internal static delegate* unmanaged[Cdecl]<nint, HSteamNetPollGroup, byte> _ISteamNetworkingSockets_DestroyPollGroup;
	internal static delegate* unmanaged[Cdecl]<nint, HSteamNetConnection, HSteamNetPollGroup, byte> _ISteamNetworkingSockets_SetConnectionPollGroup;
	internal static delegate* unmanaged[Cdecl]<nint, HSteamNetPollGroup, nint*, int, int> _ISteamNetworkingSockets_ReceiveMessagesOnPollGroup;
	internal static delegate* unmanaged[Cdecl]<nint, nint, int, SteamDatagramRelayAuthTicket*, byte> _ISteamNetworkingSockets_ReceivedRelayAuthTicket;
	internal static delegate* unmanaged[Cdecl]<nint, SteamNetworkingIdentity*, int, SteamDatagramRelayAuthTicket*, int> _ISteamNetworkingSockets_FindRelayAuthTicketForServer;
	internal static delegate* unmanaged[Cdecl]<nint, SteamNetworkingIdentity*, int, int, SteamNetworkingConfigValue_t*, uint> _ISteamNetworkingSockets_ConnectToHostedDedicatedServer;
	internal static delegate* unmanaged[Cdecl]<nint, ushort> _ISteamNetworkingSockets_GetHostedDedicatedServerPort;
	internal static delegate* unmanaged[Cdecl]<nint, uint> _ISteamNetworkingSockets_GetHostedDedicatedServerPOPID;
	internal static delegate* unmanaged[Cdecl]<nint, SteamDatagramHostedAddress*, EResult> _ISteamNetworkingSockets_GetHostedDedicatedServerAddress;
	internal static delegate* unmanaged[Cdecl]<nint, int, int, SteamNetworkingConfigValue_t*, uint> _ISteamNetworkingSockets_CreateHostedDedicatedServerListenSocket;
	internal static delegate* unmanaged[Cdecl]<nint, nint, int*, nint, EResult> _ISteamNetworkingSockets_GetGameCoordinatorServerLogin;
	internal static delegate* unmanaged[Cdecl]<nint, ISteamNetworkingConnectionSignaling*, SteamNetworkingIdentity*, int, int, SteamNetworkingConfigValue_t*, uint> _ISteamNetworkingSockets_ConnectP2PCustomSignaling;
	internal static delegate* unmanaged[Cdecl]<nint, nint, int, ISteamNetworkingSignalingRecvContext*, byte> _ISteamNetworkingSockets_ReceivedP2PCustomSignal;
	internal static delegate* unmanaged[Cdecl]<nint, int*, nint, SteamNetworkingErrMsg*, byte> _ISteamNetworkingSockets_GetCertificateRequest;
	internal static delegate* unmanaged[Cdecl]<nint, nint, int, SteamNetworkingErrMsg*, byte> _ISteamNetworkingSockets_SetCertificate;
	internal static delegate* unmanaged[Cdecl]<nint, SteamNetworkingIdentity*, void> _ISteamNetworkingSockets_ResetIdentity;
	internal static delegate* unmanaged[Cdecl]<nint, void> _ISteamNetworkingSockets_RunCallbacks;
	internal static delegate* unmanaged[Cdecl]<nint, int, byte> _ISteamNetworkingSockets_BeginAsyncRequestFakeIP;
	internal static delegate* unmanaged[Cdecl]<nint, int, SteamNetworkingFakeIPResult_t*, void> _ISteamNetworkingSockets_GetFakeIP;
	internal static delegate* unmanaged[Cdecl]<nint, int, int, SteamNetworkingConfigValue_t*, uint> _ISteamNetworkingSockets_CreateListenSocketP2PFakeIP;
	internal static delegate* unmanaged[Cdecl]<nint, HSteamNetConnection, SteamNetworkingIPAddr*, EResult> _ISteamNetworkingSockets_GetRemoteFakeIPForConnection;
	internal static delegate* unmanaged[Cdecl]<nint, int, nint> _ISteamNetworkingSockets_CreateFakeUDPPort;
	internal static delegate* unmanaged[Cdecl]<nint, int, nint> _ISteamNetworkingUtils_AllocateMessage;
	internal static delegate* unmanaged[Cdecl]<nint, void> _ISteamNetworkingUtils_InitRelayNetworkAccess;
	internal static delegate* unmanaged[Cdecl]<nint, SteamRelayNetworkStatus_t*, ESteamNetworkingAvailability> _ISteamNetworkingUtils_GetRelayNetworkStatus;
	internal static delegate* unmanaged[Cdecl]<nint, SteamNetworkPingLocation_t*, float> _ISteamNetworkingUtils_GetLocalPingLocation;
	internal static delegate* unmanaged[Cdecl]<nint, SteamNetworkPingLocation_t*, SteamNetworkPingLocation_t*, int> _ISteamNetworkingUtils_EstimatePingTimeBetweenTwoLocations;
	internal static delegate* unmanaged[Cdecl]<nint, SteamNetworkPingLocation_t*, int> _ISteamNetworkingUtils_EstimatePingTimeFromLocalHost;
	internal static delegate* unmanaged[Cdecl]<nint, SteamNetworkPingLocation_t*, nint, int, void> _ISteamNetworkingUtils_ConvertPingLocationToString;
	internal static delegate* unmanaged[Cdecl]<nint, byte*, SteamNetworkPingLocation_t*, byte> _ISteamNetworkingUtils_ParsePingLocationString;
	internal static delegate* unmanaged[Cdecl]<nint, float, byte> _ISteamNetworkingUtils_CheckPingDataUpToDate;
	internal static delegate* unmanaged[Cdecl]<nint, SteamNetworkingPOPID, SteamNetworkingPOPID*, int> _ISteamNetworkingUtils_GetPingToDataCenter;
	internal static delegate* unmanaged[Cdecl]<nint, SteamNetworkingPOPID, int> _ISteamNetworkingUtils_GetDirectPingToPOP;
	internal static delegate* unmanaged[Cdecl]<nint, int> _ISteamNetworkingUtils_GetPOPCount;
	internal static delegate* unmanaged[Cdecl]<nint, SteamNetworkingPOPID*, int, int> _ISteamNetworkingUtils_GetPOPList;
	internal static delegate* unmanaged[Cdecl]<nint, long> _ISteamNetworkingUtils_GetLocalTimestamp;
	internal static delegate* unmanaged[Cdecl]<nint, ESteamNetworkingSocketsDebugOutputType, nint, void> _ISteamNetworkingUtils_SetDebugOutputFunction;
	internal static delegate* unmanaged[Cdecl]<nint, uint, byte> _ISteamNetworkingUtils_IsFakeIPv4;
	internal static delegate* unmanaged[Cdecl]<nint, uint, ESteamNetworkingFakeIPType> _ISteamNetworkingUtils_GetIPv4FakeIPType;
	internal static delegate* unmanaged[Cdecl]<nint, SteamNetworkingIPAddr*, SteamNetworkingIdentity*, EResult> _ISteamNetworkingUtils_GetRealIdentityForFakeIP;
	internal static delegate* unmanaged[Cdecl]<nint, ESteamNetworkingConfigValue, ESteamNetworkingConfigScope, nint, ESteamNetworkingConfigDataType, nint, byte> _ISteamNetworkingUtils_SetConfigValue;
	internal static delegate* unmanaged[Cdecl]<nint, ESteamNetworkingConfigValue, ESteamNetworkingConfigScope, nint, ESteamNetworkingConfigDataType*, nint, ulong*, ESteamNetworkingGetConfigValueResult> _ISteamNetworkingUtils_GetConfigValue;
	internal static delegate* unmanaged[Cdecl]<nint, ESteamNetworkingConfigValue, ESteamNetworkingConfigDataType*, ESteamNetworkingConfigScope*, nint> _ISteamNetworkingUtils_GetConfigValueInfo;
	internal static delegate* unmanaged[Cdecl]<nint, ESteamNetworkingConfigValue, byte, ESteamNetworkingConfigValue> _ISteamNetworkingUtils_IterateGenericEditableConfigValues;
	internal static delegate* unmanaged[Cdecl]<nint, SteamNetworkingIPAddr*, nint, uint, byte, void> _ISteamNetworkingUtils_SteamNetworkingIPAddr_ToString;
	internal static delegate* unmanaged[Cdecl]<nint, SteamNetworkingIPAddr*, byte*, byte> _ISteamNetworkingUtils_SteamNetworkingIPAddr_ParseString;
	internal static delegate* unmanaged[Cdecl]<nint, SteamNetworkingIPAddr*, ESteamNetworkingFakeIPType> _ISteamNetworkingUtils_SteamNetworkingIPAddr_GetFakeIPType;
	internal static delegate* unmanaged[Cdecl]<nint, SteamNetworkingIdentity*, nint, uint, void> _ISteamNetworkingUtils_SteamNetworkingIdentity_ToString;
	internal static delegate* unmanaged[Cdecl]<nint, SteamNetworkingIdentity*, byte*, byte> _ISteamNetworkingUtils_SteamNetworkingIdentity_ParseString;
	internal static delegate* unmanaged[Cdecl]<nint, AccountID_t, EUserUGCList, EUGCMatchingUGCType, EUserUGCListSortOrder, AppId_t, AppId_t, uint, ulong> _ISteamUGC_CreateQueryUserUGCRequest;
	internal static delegate* unmanaged[Cdecl]<nint, EUGCQuery, EUGCMatchingUGCType, AppId_t, AppId_t, uint, ulong> _ISteamUGC_CreateQueryAllUGCRequestPage;
	internal static delegate* unmanaged[Cdecl]<nint, EUGCQuery, EUGCMatchingUGCType, AppId_t, AppId_t, byte*, ulong> _ISteamUGC_CreateQueryAllUGCRequestCursor;
	internal static delegate* unmanaged[Cdecl]<nint, PublishedFileId_t*, uint, ulong> _ISteamUGC_CreateQueryUGCDetailsRequest;
	internal static delegate* unmanaged[Cdecl]<nint, UGCQueryHandle_t, ulong> _ISteamUGC_SendQueryUGCRequest;
	internal static delegate* unmanaged[Cdecl]<nint, UGCQueryHandle_t, uint, SteamUGCDetails_t*, byte> _ISteamUGC_GetQueryUGCResult;
	internal static delegate* unmanaged[Cdecl]<nint, UGCQueryHandle_t, uint, uint> _ISteamUGC_GetQueryUGCNumTags;
	internal static delegate* unmanaged[Cdecl]<nint, UGCQueryHandle_t, uint, uint, nint, uint, byte> _ISteamUGC_GetQueryUGCTag;
	internal static delegate* unmanaged[Cdecl]<nint, UGCQueryHandle_t, uint, uint, nint, uint, byte> _ISteamUGC_GetQueryUGCTagDisplayName;
	internal static delegate* unmanaged[Cdecl]<nint, UGCQueryHandle_t, uint, nint, uint, byte> _ISteamUGC_GetQueryUGCPreviewURL;
	internal static delegate* unmanaged[Cdecl]<nint, UGCQueryHandle_t, uint, nint, uint, byte> _ISteamUGC_GetQueryUGCMetadata;
	internal static delegate* unmanaged[Cdecl]<nint, UGCQueryHandle_t, uint, PublishedFileId_t*, uint, byte> _ISteamUGC_GetQueryUGCChildren;
	internal static delegate* unmanaged[Cdecl]<nint, UGCQueryHandle_t, uint, EItemStatistic, ulong*, byte> _ISteamUGC_GetQueryUGCStatistic;
	internal static delegate* unmanaged[Cdecl]<nint, UGCQueryHandle_t, uint, uint> _ISteamUGC_GetQueryUGCNumAdditionalPreviews;
	internal static delegate* unmanaged[Cdecl]<nint, UGCQueryHandle_t, uint, uint, nint, uint, nint, uint, EItemPreviewType*, byte> _ISteamUGC_GetQueryUGCAdditionalPreview;
	internal static delegate* unmanaged[Cdecl]<nint, UGCQueryHandle_t, uint, uint> _ISteamUGC_GetQueryUGCNumKeyValueTags;
	internal static delegate* unmanaged[Cdecl]<nint, UGCQueryHandle_t, uint, uint, nint, uint, nint, uint, byte> _ISteamUGC_GetQueryUGCKeyValueTag;
	internal static delegate* unmanaged[Cdecl]<nint, UGCQueryHandle_t, uint, byte*, nint, uint, byte> _ISteamUGC_GetQueryFirstUGCKeyValueTag;
	internal static delegate* unmanaged[Cdecl]<nint, UGCQueryHandle_t, uint, uint> _ISteamUGC_GetNumSupportedGameVersions;
	internal static delegate* unmanaged[Cdecl]<nint, UGCQueryHandle_t, uint, uint, nint, nint, uint, byte> _ISteamUGC_GetSupportedGameVersionData;
	internal static delegate* unmanaged[Cdecl]<nint, UGCQueryHandle_t, uint, EUGCContentDescriptorID*, uint, uint> _ISteamUGC_GetQueryUGCContentDescriptors;
	internal static delegate* unmanaged[Cdecl]<nint, UGCQueryHandle_t, byte> _ISteamUGC_ReleaseQueryUGCRequest;
	internal static delegate* unmanaged[Cdecl]<nint, UGCQueryHandle_t, byte*, byte> _ISteamUGC_AddRequiredTag;
	internal static delegate* unmanaged[Cdecl]<nint, UGCQueryHandle_t, nint, byte> _ISteamUGC_AddRequiredTagGroup;
	internal static delegate* unmanaged[Cdecl]<nint, UGCQueryHandle_t, byte*, byte> _ISteamUGC_AddExcludedTag;
	internal static delegate* unmanaged[Cdecl]<nint, UGCQueryHandle_t, byte, byte> _ISteamUGC_SetReturnOnlyIDs;
	internal static delegate* unmanaged[Cdecl]<nint, UGCQueryHandle_t, byte, byte> _ISteamUGC_SetReturnKeyValueTags;
	internal static delegate* unmanaged[Cdecl]<nint, UGCQueryHandle_t, byte, byte> _ISteamUGC_SetReturnLongDescription;
	internal static delegate* unmanaged[Cdecl]<nint, UGCQueryHandle_t, byte, byte> _ISteamUGC_SetReturnMetadata;
	internal static delegate* unmanaged[Cdecl]<nint, UGCQueryHandle_t, byte, byte> _ISteamUGC_SetReturnChildren;
	internal static delegate* unmanaged[Cdecl]<nint, UGCQueryHandle_t, byte, byte> _ISteamUGC_SetReturnAdditionalPreviews;
	internal static delegate* unmanaged[Cdecl]<nint, UGCQueryHandle_t, byte, byte> _ISteamUGC_SetReturnTotalOnly;
	internal static delegate* unmanaged[Cdecl]<nint, UGCQueryHandle_t, uint, byte> _ISteamUGC_SetReturnPlaytimeStats;
	internal static delegate* unmanaged[Cdecl]<nint, UGCQueryHandle_t, byte*, byte> _ISteamUGC_SetLanguage;
	internal static delegate* unmanaged[Cdecl]<nint, UGCQueryHandle_t, uint, byte> _ISteamUGC_SetAllowCachedResponse;
	internal static delegate* unmanaged[Cdecl]<nint, UGCUpdateHandle_t, byte, byte> _ISteamUGC_SetAdminQuery;
	internal static delegate* unmanaged[Cdecl]<nint, UGCQueryHandle_t, byte*, byte> _ISteamUGC_SetCloudFileNameFilter;
	internal static delegate* unmanaged[Cdecl]<nint, UGCQueryHandle_t, byte, byte> _ISteamUGC_SetMatchAnyTag;
	internal static delegate* unmanaged[Cdecl]<nint, UGCQueryHandle_t, byte*, byte> _ISteamUGC_SetSearchText;
	internal static delegate* unmanaged[Cdecl]<nint, UGCQueryHandle_t, uint, byte> _ISteamUGC_SetRankedByTrendDays;
	internal static delegate* unmanaged[Cdecl]<nint, UGCQueryHandle_t, uint, uint, byte> _ISteamUGC_SetTimeCreatedDateRange;
	internal static delegate* unmanaged[Cdecl]<nint, UGCQueryHandle_t, uint, uint, byte> _ISteamUGC_SetTimeUpdatedDateRange;
	internal static delegate* unmanaged[Cdecl]<nint, UGCQueryHandle_t, byte*, byte*, byte> _ISteamUGC_AddRequiredKeyValueTag;
	internal static delegate* unmanaged[Cdecl]<nint, PublishedFileId_t, uint, ulong> _ISteamUGC_RequestUGCDetails;
	internal static delegate* unmanaged[Cdecl]<nint, AppId_t, EWorkshopFileType, ulong> _ISteamUGC_CreateItem;
	internal static delegate* unmanaged[Cdecl]<nint, AppId_t, PublishedFileId_t, ulong> _ISteamUGC_StartItemUpdate;
	internal static delegate* unmanaged[Cdecl]<nint, UGCUpdateHandle_t, byte*, byte> _ISteamUGC_SetItemTitle;
	internal static delegate* unmanaged[Cdecl]<nint, UGCUpdateHandle_t, byte*, byte> _ISteamUGC_SetItemDescription;
	internal static delegate* unmanaged[Cdecl]<nint, UGCUpdateHandle_t, byte*, byte> _ISteamUGC_SetItemUpdateLanguage;
	internal static delegate* unmanaged[Cdecl]<nint, UGCUpdateHandle_t, byte*, byte> _ISteamUGC_SetItemMetadata;
	internal static delegate* unmanaged[Cdecl]<nint, UGCUpdateHandle_t, ERemoteStoragePublishedFileVisibility, byte> _ISteamUGC_SetItemVisibility;
	internal static delegate* unmanaged[Cdecl]<nint, UGCUpdateHandle_t, nint, byte, byte> _ISteamUGC_SetItemTags;
	internal static delegate* unmanaged[Cdecl]<nint, UGCUpdateHandle_t, byte*, byte> _ISteamUGC_SetItemContent;
	internal static delegate* unmanaged[Cdecl]<nint, UGCUpdateHandle_t, byte*, byte> _ISteamUGC_SetItemPreview;
	internal static delegate* unmanaged[Cdecl]<nint, UGCUpdateHandle_t, byte, byte> _ISteamUGC_SetAllowLegacyUpload;
	internal static delegate* unmanaged[Cdecl]<nint, UGCUpdateHandle_t, byte> _ISteamUGC_RemoveAllItemKeyValueTags;
	internal static delegate* unmanaged[Cdecl]<nint, UGCUpdateHandle_t, byte*, byte> _ISteamUGC_RemoveItemKeyValueTags;
	internal static delegate* unmanaged[Cdecl]<nint, UGCUpdateHandle_t, byte*, byte*, byte> _ISteamUGC_AddItemKeyValueTag;
	internal static delegate* unmanaged[Cdecl]<nint, UGCUpdateHandle_t, byte*, EItemPreviewType, byte> _ISteamUGC_AddItemPreviewFile;
	internal static delegate* unmanaged[Cdecl]<nint, UGCUpdateHandle_t, byte*, byte> _ISteamUGC_AddItemPreviewVideo;
	internal static delegate* unmanaged[Cdecl]<nint, UGCUpdateHandle_t, uint, byte*, byte> _ISteamUGC_UpdateItemPreviewFile;
	internal static delegate* unmanaged[Cdecl]<nint, UGCUpdateHandle_t, uint, byte*, byte> _ISteamUGC_UpdateItemPreviewVideo;
	internal static delegate* unmanaged[Cdecl]<nint, UGCUpdateHandle_t, uint, byte> _ISteamUGC_RemoveItemPreview;
	internal static delegate* unmanaged[Cdecl]<nint, UGCUpdateHandle_t, EUGCContentDescriptorID, byte> _ISteamUGC_AddContentDescriptor;
	internal static delegate* unmanaged[Cdecl]<nint, UGCUpdateHandle_t, EUGCContentDescriptorID, byte> _ISteamUGC_RemoveContentDescriptor;
	internal static delegate* unmanaged[Cdecl]<nint, UGCUpdateHandle_t, byte*, byte*, byte> _ISteamUGC_SetRequiredGameVersions;
	internal static delegate* unmanaged[Cdecl]<nint, UGCUpdateHandle_t, byte*, ulong> _ISteamUGC_SubmitItemUpdate;
	internal static delegate* unmanaged[Cdecl]<nint, UGCUpdateHandle_t, ulong*, ulong*, EItemUpdateStatus> _ISteamUGC_GetItemUpdateProgress;
	internal static delegate* unmanaged[Cdecl]<nint, PublishedFileId_t, byte, ulong> _ISteamUGC_SetUserItemVote;
	internal static delegate* unmanaged[Cdecl]<nint, PublishedFileId_t, ulong> _ISteamUGC_GetUserItemVote;
	internal static delegate* unmanaged[Cdecl]<nint, AppId_t, PublishedFileId_t, ulong> _ISteamUGC_AddItemToFavorites;
	internal static delegate* unmanaged[Cdecl]<nint, AppId_t, PublishedFileId_t, ulong> _ISteamUGC_RemoveItemFromFavorites;
	internal static delegate* unmanaged[Cdecl]<nint, PublishedFileId_t, ulong> _ISteamUGC_SubscribeItem;
	internal static delegate* unmanaged[Cdecl]<nint, PublishedFileId_t, ulong> _ISteamUGC_UnsubscribeItem;
	internal static delegate* unmanaged[Cdecl]<nint, byte, uint> _ISteamUGC_GetNumSubscribedItems;
	internal static delegate* unmanaged[Cdecl]<nint, PublishedFileId_t*, uint, byte, uint> _ISteamUGC_GetSubscribedItems;
	internal static delegate* unmanaged[Cdecl]<nint, PublishedFileId_t, uint> _ISteamUGC_GetItemState;
	internal static delegate* unmanaged[Cdecl]<nint, PublishedFileId_t, ulong*, nint, uint, uint*, byte> _ISteamUGC_GetItemInstallInfo;
	internal static delegate* unmanaged[Cdecl]<nint, PublishedFileId_t, ulong*, ulong*, byte> _ISteamUGC_GetItemDownloadInfo;
	internal static delegate* unmanaged[Cdecl]<nint, PublishedFileId_t, byte, byte> _ISteamUGC_DownloadItem;
	internal static delegate* unmanaged[Cdecl]<nint, DepotId_t, byte*, byte> _ISteamUGC_BInitWorkshopForGameServer;
	internal static delegate* unmanaged[Cdecl]<nint, byte, void> _ISteamUGC_SuspendDownloads;
	internal static delegate* unmanaged[Cdecl]<nint, PublishedFileId_t*, uint, ulong> _ISteamUGC_StartPlaytimeTracking;
	internal static delegate* unmanaged[Cdecl]<nint, PublishedFileId_t*, uint, ulong> _ISteamUGC_StopPlaytimeTracking;
	internal static delegate* unmanaged[Cdecl]<nint, ulong> _ISteamUGC_StopPlaytimeTrackingForAllItems;
	internal static delegate* unmanaged[Cdecl]<nint, PublishedFileId_t, PublishedFileId_t, ulong> _ISteamUGC_AddDependency;
	internal static delegate* unmanaged[Cdecl]<nint, PublishedFileId_t, PublishedFileId_t, ulong> _ISteamUGC_RemoveDependency;
	internal static delegate* unmanaged[Cdecl]<nint, PublishedFileId_t, AppId_t, ulong> _ISteamUGC_AddAppDependency;
	internal static delegate* unmanaged[Cdecl]<nint, PublishedFileId_t, AppId_t, ulong> _ISteamUGC_RemoveAppDependency;
	internal static delegate* unmanaged[Cdecl]<nint, PublishedFileId_t, ulong> _ISteamUGC_GetAppDependencies;
	internal static delegate* unmanaged[Cdecl]<nint, PublishedFileId_t, ulong> _ISteamUGC_DeleteItem;
	internal static delegate* unmanaged[Cdecl]<nint, byte> _ISteamUGC_ShowWorkshopEULA;
	internal static delegate* unmanaged[Cdecl]<nint, ulong> _ISteamUGC_GetWorkshopEULAStatus;
	internal static delegate* unmanaged[Cdecl]<nint, EUGCContentDescriptorID*, uint, uint> _ISteamUGC_GetUserContentDescriptorPreferences;
	internal static delegate* unmanaged[Cdecl]<nint, PublishedFileId_t*, uint, byte, byte> _ISteamUGC_SetItemsDisabledLocally;
	internal static delegate* unmanaged[Cdecl]<nint, PublishedFileId_t*, uint, byte> _ISteamUGC_SetSubscriptionsLoadOrder;
	internal static delegate* unmanaged[Cdecl]<nint, uint> _ISteamUtils_GetSecondsSinceAppActive;
	internal static delegate* unmanaged[Cdecl]<nint, uint> _ISteamUtils_GetSecondsSinceComputerActive;
	internal static delegate* unmanaged[Cdecl]<nint, EUniverse> _ISteamUtils_GetConnectedUniverse;
	internal static delegate* unmanaged[Cdecl]<nint, uint> _ISteamUtils_GetServerRealTime;
	internal static delegate* unmanaged[Cdecl]<nint, nint> _ISteamUtils_GetIPCountry;
	internal static delegate* unmanaged[Cdecl]<nint, int, uint*, uint*, byte> _ISteamUtils_GetImageSize;
	internal static delegate* unmanaged[Cdecl]<nint, int, byte*, int, byte> _ISteamUtils_GetImageRGBA;
	internal static delegate* unmanaged[Cdecl]<nint, byte> _ISteamUtils_GetCurrentBatteryPower;
	internal static delegate* unmanaged[Cdecl]<nint, uint> _ISteamUtils_GetAppID;
	internal static delegate* unmanaged[Cdecl]<nint, ENotificationPosition, void> _ISteamUtils_SetOverlayNotificationPosition;
	internal static delegate* unmanaged[Cdecl]<nint, SteamAPICall_t, byte*, byte> _ISteamUtils_IsAPICallCompleted;
	internal static delegate* unmanaged[Cdecl]<nint, SteamAPICall_t, ESteamAPICallFailure> _ISteamUtils_GetAPICallFailureReason;
	internal static delegate* unmanaged[Cdecl]<nint, SteamAPICall_t, nint, int, int, byte*, byte> _ISteamUtils_GetAPICallResult;
	internal static delegate* unmanaged[Cdecl]<nint, uint> _ISteamUtils_GetIPCCallCount;
	internal static delegate* unmanaged[Cdecl]<nint, nint, void> _ISteamUtils_SetWarningMessageHook;
	internal static delegate* unmanaged[Cdecl]<nint, byte> _ISteamUtils_IsOverlayEnabled;
	internal static delegate* unmanaged[Cdecl]<nint, byte> _ISteamUtils_BOverlayNeedsPresent;
	internal static delegate* unmanaged[Cdecl]<nint, byte*, ulong> _ISteamUtils_CheckFileSignature;
	internal static delegate* unmanaged[Cdecl]<nint, EGamepadTextInputMode, EGamepadTextInputLineMode, byte*, uint, byte*, byte> _ISteamUtils_ShowGamepadTextInput;
	internal static delegate* unmanaged[Cdecl]<nint, uint> _ISteamUtils_GetEnteredGamepadTextLength;
	internal static delegate* unmanaged[Cdecl]<nint, nint, uint, byte> _ISteamUtils_GetEnteredGamepadTextInput;
	internal static delegate* unmanaged[Cdecl]<nint, nint> _ISteamUtils_GetSteamUILanguage;
	internal static delegate* unmanaged[Cdecl]<nint, byte> _ISteamUtils_IsSteamRunningInVR;
	internal static delegate* unmanaged[Cdecl]<nint, int, int, void> _ISteamUtils_SetOverlayNotificationInset;
	internal static delegate* unmanaged[Cdecl]<nint, byte> _ISteamUtils_IsSteamInBigPictureMode;
	internal static delegate* unmanaged[Cdecl]<nint, void> _ISteamUtils_StartVRDashboard;
	internal static delegate* unmanaged[Cdecl]<nint, byte> _ISteamUtils_IsVRHeadsetStreamingEnabled;
	internal static delegate* unmanaged[Cdecl]<nint, byte, void> _ISteamUtils_SetVRHeadsetStreamingEnabled;
	internal static delegate* unmanaged[Cdecl]<nint, byte> _ISteamUtils_IsSteamChinaLauncher;
	internal static delegate* unmanaged[Cdecl]<nint, uint, byte> _ISteamUtils_InitFilterText;
	internal static delegate* unmanaged[Cdecl]<nint, ETextFilteringContext, CSteamID, byte*, nint, uint, int> _ISteamUtils_FilterText;
	internal static delegate* unmanaged[Cdecl]<nint, ESteamIPv6ConnectivityProtocol, ESteamIPv6ConnectivityState> _ISteamUtils_GetIPv6ConnectivityState;
	internal static delegate* unmanaged[Cdecl]<nint, byte> _ISteamUtils_IsSteamRunningOnSteamDeck;
	internal static delegate* unmanaged[Cdecl]<nint, EFloatingGamepadTextInputMode, int, int, int, int, byte> _ISteamUtils_ShowFloatingGamepadTextInput;
	internal static delegate* unmanaged[Cdecl]<nint, byte, void> _ISteamUtils_SetGameLauncherMode;
	internal static delegate* unmanaged[Cdecl]<nint, byte> _ISteamUtils_DismissFloatingGamepadTextInput;
	internal static delegate* unmanaged[Cdecl]<nint, byte> _ISteamUtils_DismissGamepadTextInput;

	internal static bool SteamAPI_Init()
	{
		if ((nint)_SteamAPI_Init == 0) throw new EntryPointNotFoundException("'SteamAPI_Init' is not available.");
		return _SteamAPI_Init() != 0;
	}

	internal static void SteamAPI_Shutdown()
	{
		if ((nint)_SteamAPI_Shutdown == 0) throw new EntryPointNotFoundException("'SteamAPI_Shutdown' is not available.");
		_SteamAPI_Shutdown();
	}

	internal static bool SteamAPI_RestartAppIfNecessary(AppId_t unOwnAppID)
	{
		if ((nint)_SteamAPI_RestartAppIfNecessary == 0) throw new EntryPointNotFoundException("'SteamAPI_RestartAppIfNecessary' is not available.");
		return _SteamAPI_RestartAppIfNecessary(unOwnAppID) != 0;
	}

	internal static void SteamAPI_ReleaseCurrentThreadMemory()
	{
		if ((nint)_SteamAPI_ReleaseCurrentThreadMemory == 0) throw new EntryPointNotFoundException("'SteamAPI_ReleaseCurrentThreadMemory' is not available.");
		_SteamAPI_ReleaseCurrentThreadMemory();
	}

	internal static void SteamAPI_RunCallbacks()
	{
		if ((nint)_SteamAPI_RunCallbacks == 0) throw new EntryPointNotFoundException("'SteamAPI_RunCallbacks' is not available.");
		_SteamAPI_RunCallbacks();
	}

	internal static void SteamAPI_RegisterCallback(IntPtr pCallback, int iCallback)
	{
		if ((nint)_SteamAPI_RegisterCallback == 0) throw new EntryPointNotFoundException("'SteamAPI_RegisterCallback' is not available.");
		_SteamAPI_RegisterCallback(pCallback, iCallback);
	}

	internal static void SteamAPI_UnregisterCallback(IntPtr pCallback)
	{
		if ((nint)_SteamAPI_UnregisterCallback == 0) throw new EntryPointNotFoundException("'SteamAPI_UnregisterCallback' is not available.");
		_SteamAPI_UnregisterCallback(pCallback);
	}

	internal static void SteamAPI_RegisterCallResult(IntPtr pCallback, ulong hAPICall)
	{
		if ((nint)_SteamAPI_RegisterCallResult == 0) throw new EntryPointNotFoundException("'SteamAPI_RegisterCallResult' is not available.");
		_SteamAPI_RegisterCallResult(pCallback, hAPICall);
	}

	internal static void SteamAPI_UnregisterCallResult(IntPtr pCallback, ulong hAPICall)
	{
		if ((nint)_SteamAPI_UnregisterCallResult == 0) throw new EntryPointNotFoundException("'SteamAPI_UnregisterCallResult' is not available.");
		_SteamAPI_UnregisterCallResult(pCallback, hAPICall);
	}

	internal static bool SteamAPI_IsSteamRunning()
	{
		if ((nint)_SteamAPI_IsSteamRunning == 0) throw new EntryPointNotFoundException("'SteamAPI_IsSteamRunning' is not available.");
		return _SteamAPI_IsSteamRunning() != 0;
	}

	internal static int SteamAPI_GetHSteamPipe()
	{
		if ((nint)_SteamAPI_GetHSteamPipe == 0) throw new EntryPointNotFoundException("'SteamAPI_GetHSteamPipe' is not available.");
		return _SteamAPI_GetHSteamPipe();
	}

	internal static int SteamAPI_GetHSteamUser()
	{
		if ((nint)_SteamAPI_GetHSteamUser == 0) throw new EntryPointNotFoundException("'SteamAPI_GetHSteamUser' is not available.");
		return _SteamAPI_GetHSteamUser();
	}

	internal static IntPtr SteamInternal_CreateInterface(string ver)
	{
		if ((nint)_SteamInternal_CreateInterface == 0) throw new EntryPointNotFoundException("'SteamInternal_CreateInterface' is not available.");
		using var verStr = new ScopedCString(ver ?? string.Empty);
		nint ret;
		fixed (byte* verBufferPtr = verStr)
		{
			ret = _SteamInternal_CreateInterface(ver is null ? null : verBufferPtr);
		}
		return ret;
	}

	internal static IntPtr SteamInternal_FindOrCreateUserInterface(HSteamUser hSteamUser, string pszVersion)
	{
		if ((nint)_SteamInternal_FindOrCreateUserInterface == 0) throw new EntryPointNotFoundException("'SteamInternal_FindOrCreateUserInterface' is not available.");
		using var pszVersionStr = new ScopedCString(pszVersion ?? string.Empty);
		nint ret;
		fixed (byte* pszVersionBufferPtr = pszVersionStr)
		{
			ret = _SteamInternal_FindOrCreateUserInterface(hSteamUser, pszVersion is null ? null : pszVersionBufferPtr);
		}
		return ret;
	}

	internal static IntPtr SteamInternal_FindOrCreateGameServerInterface(HSteamUser hSteamUser, string pszVersion)
	{
		if ((nint)_SteamInternal_FindOrCreateGameServerInterface == 0) throw new EntryPointNotFoundException("'SteamInternal_FindOrCreateGameServerInterface' is not available.");
		using var pszVersionStr = new ScopedCString(pszVersion ?? string.Empty);
		nint ret;
		fixed (byte* pszVersionBufferPtr = pszVersionStr)
		{
			ret = _SteamInternal_FindOrCreateGameServerInterface(hSteamUser, pszVersion is null ? null : pszVersionBufferPtr);
		}
		return ret;
	}

	internal static void SteamGameServer_Shutdown()
	{
		if ((nint)_SteamGameServer_Shutdown == 0) throw new EntryPointNotFoundException("'SteamGameServer_Shutdown' is not available.");
		_SteamGameServer_Shutdown();
	}

	internal static void SteamGameServer_RunCallbacks()
	{
		if ((nint)_SteamGameServer_RunCallbacks == 0) throw new EntryPointNotFoundException("'SteamGameServer_RunCallbacks' is not available.");
		_SteamGameServer_RunCallbacks();
	}

	internal static void SteamGameServer_ReleaseCurrentThreadMemory()
	{
		if ((nint)_SteamGameServer_ReleaseCurrentThreadMemory == 0) throw new EntryPointNotFoundException("'SteamGameServer_ReleaseCurrentThreadMemory' is not available.");
		_SteamGameServer_ReleaseCurrentThreadMemory();
	}

	internal static bool SteamGameServer_BSecure()
	{
		if ((nint)_SteamGameServer_BSecure == 0) throw new EntryPointNotFoundException("'SteamGameServer_BSecure' is not available.");
		return _SteamGameServer_BSecure() != 0;
	}

	internal static ulong SteamGameServer_GetSteamID()
	{
		if ((nint)_SteamGameServer_GetSteamID == 0) throw new EntryPointNotFoundException("'SteamGameServer_GetSteamID' is not available.");
		return _SteamGameServer_GetSteamID();
	}

	internal static int SteamGameServer_GetHSteamPipe()
	{
		if ((nint)_SteamGameServer_GetHSteamPipe == 0) throw new EntryPointNotFoundException("'SteamGameServer_GetHSteamPipe' is not available.");
		return _SteamGameServer_GetHSteamPipe();
	}

	internal static int SteamGameServer_GetHSteamUser()
	{
		if ((nint)_SteamGameServer_GetHSteamUser == 0) throw new EntryPointNotFoundException("'SteamGameServer_GetHSteamUser' is not available.");
		return _SteamGameServer_GetHSteamUser();
	}

	internal static void SteamAPI_SteamNetworkingIPAddr_Clear(ref SteamNetworkingIPAddr self)
	{
		if ((nint)_SteamAPI_SteamNetworkingIPAddr_Clear == 0) throw new EntryPointNotFoundException("'SteamAPI_SteamNetworkingIPAddr_Clear' is not available.");
		fixed (SteamNetworkingIPAddr* selfPtr = &self)
		{
			_SteamAPI_SteamNetworkingIPAddr_Clear(selfPtr);
		}
	}

	internal static bool SteamAPI_SteamNetworkingIPAddr_IsIPv6AllZeros(ref SteamNetworkingIPAddr self)
	{
		if ((nint)_SteamAPI_SteamNetworkingIPAddr_IsIPv6AllZeros == 0) throw new EntryPointNotFoundException("'SteamAPI_SteamNetworkingIPAddr_IsIPv6AllZeros' is not available.");
		byte ret;
		fixed (SteamNetworkingIPAddr* selfPtr = &self)
		{
			ret = _SteamAPI_SteamNetworkingIPAddr_IsIPv6AllZeros(selfPtr);
		}
		return ret != 0;
	}

	internal static void SteamAPI_SteamNetworkingIPAddr_SetIPv6(ref SteamNetworkingIPAddr self, byte[] ipv6, ushort nPort)
	{
		if ((nint)_SteamAPI_SteamNetworkingIPAddr_SetIPv6 == 0) throw new EntryPointNotFoundException("'SteamAPI_SteamNetworkingIPAddr_SetIPv6' is not available.");
		fixed (SteamNetworkingIPAddr* selfPtr = &self)
		{
			fixed (byte* ipv6Ptr = ipv6)
			{
				_SteamAPI_SteamNetworkingIPAddr_SetIPv6(selfPtr, ipv6Ptr, nPort);
			}
		}
	}

	internal static void SteamAPI_SteamNetworkingIPAddr_SetIPv4(ref SteamNetworkingIPAddr self, uint nIP, ushort nPort)
	{
		if ((nint)_SteamAPI_SteamNetworkingIPAddr_SetIPv4 == 0) throw new EntryPointNotFoundException("'SteamAPI_SteamNetworkingIPAddr_SetIPv4' is not available.");
		fixed (SteamNetworkingIPAddr* selfPtr = &self)
		{
			_SteamAPI_SteamNetworkingIPAddr_SetIPv4(selfPtr, nIP, nPort);
		}
	}

	internal static bool SteamAPI_SteamNetworkingIPAddr_IsIPv4(ref SteamNetworkingIPAddr self)
	{
		if ((nint)_SteamAPI_SteamNetworkingIPAddr_IsIPv4 == 0) throw new EntryPointNotFoundException("'SteamAPI_SteamNetworkingIPAddr_IsIPv4' is not available.");
		byte ret;
		fixed (SteamNetworkingIPAddr* selfPtr = &self)
		{
			ret = _SteamAPI_SteamNetworkingIPAddr_IsIPv4(selfPtr);
		}
		return ret != 0;
	}

	internal static uint SteamAPI_SteamNetworkingIPAddr_GetIPv4(ref SteamNetworkingIPAddr self)
	{
		if ((nint)_SteamAPI_SteamNetworkingIPAddr_GetIPv4 == 0) throw new EntryPointNotFoundException("'SteamAPI_SteamNetworkingIPAddr_GetIPv4' is not available.");
		uint ret;
		fixed (SteamNetworkingIPAddr* selfPtr = &self)
		{
			ret = _SteamAPI_SteamNetworkingIPAddr_GetIPv4(selfPtr);
		}
		return ret;
	}

	internal static void SteamAPI_SteamNetworkingIPAddr_SetIPv6LocalHost(ref SteamNetworkingIPAddr self, ushort nPort)
	{
		if ((nint)_SteamAPI_SteamNetworkingIPAddr_SetIPv6LocalHost == 0) throw new EntryPointNotFoundException("'SteamAPI_SteamNetworkingIPAddr_SetIPv6LocalHost' is not available.");
		fixed (SteamNetworkingIPAddr* selfPtr = &self)
		{
			_SteamAPI_SteamNetworkingIPAddr_SetIPv6LocalHost(selfPtr, nPort);
		}
	}

	internal static bool SteamAPI_SteamNetworkingIPAddr_IsLocalHost(ref SteamNetworkingIPAddr self)
	{
		if ((nint)_SteamAPI_SteamNetworkingIPAddr_IsLocalHost == 0) throw new EntryPointNotFoundException("'SteamAPI_SteamNetworkingIPAddr_IsLocalHost' is not available.");
		byte ret;
		fixed (SteamNetworkingIPAddr* selfPtr = &self)
		{
			ret = _SteamAPI_SteamNetworkingIPAddr_IsLocalHost(selfPtr);
		}
		return ret != 0;
	}

	internal static void SteamAPI_SteamNetworkingIPAddr_ToString(ref SteamNetworkingIPAddr self, IntPtr buf, uint cbBuf, bool bWithPort)
	{
		if ((nint)_SteamAPI_SteamNetworkingIPAddr_ToString == 0) throw new EntryPointNotFoundException("'SteamAPI_SteamNetworkingIPAddr_ToString' is not available.");
		fixed (SteamNetworkingIPAddr* selfPtr = &self)
		{
			_SteamAPI_SteamNetworkingIPAddr_ToString(selfPtr, buf, cbBuf, (byte)(bWithPort ? 1 : 0));
		}
	}

	internal static bool SteamAPI_SteamNetworkingIPAddr_ParseString(ref SteamNetworkingIPAddr self, string pszStr)
	{
		if ((nint)_SteamAPI_SteamNetworkingIPAddr_ParseString == 0) throw new EntryPointNotFoundException("'SteamAPI_SteamNetworkingIPAddr_ParseString' is not available.");
		using var pszStrStr = new ScopedCString(pszStr ?? string.Empty);
		byte ret;
		fixed (SteamNetworkingIPAddr* selfPtr = &self)
		{
			fixed (byte* pszStrBufferPtr = pszStrStr)
			{
				ret = _SteamAPI_SteamNetworkingIPAddr_ParseString(selfPtr, pszStr is null ? null : pszStrBufferPtr);
			}
		}
		return ret != 0;
	}

	internal static bool SteamAPI_SteamNetworkingIPAddr_IsEqualTo(ref SteamNetworkingIPAddr self, ref SteamNetworkingIPAddr x)
	{
		if ((nint)_SteamAPI_SteamNetworkingIPAddr_IsEqualTo == 0) throw new EntryPointNotFoundException("'SteamAPI_SteamNetworkingIPAddr_IsEqualTo' is not available.");
		byte ret;
		fixed (SteamNetworkingIPAddr* selfPtr = &self)
		{
			fixed (SteamNetworkingIPAddr* xPtr = &x)
			{
				ret = _SteamAPI_SteamNetworkingIPAddr_IsEqualTo(selfPtr, xPtr);
			}
		}
		return ret != 0;
	}

	internal static ESteamNetworkingFakeIPType SteamAPI_SteamNetworkingIPAddr_GetFakeIPType(ref SteamNetworkingIPAddr self)
	{
		if ((nint)_SteamAPI_SteamNetworkingIPAddr_GetFakeIPType == 0) throw new EntryPointNotFoundException("'SteamAPI_SteamNetworkingIPAddr_GetFakeIPType' is not available.");
		ESteamNetworkingFakeIPType ret;
		fixed (SteamNetworkingIPAddr* selfPtr = &self)
		{
			ret = _SteamAPI_SteamNetworkingIPAddr_GetFakeIPType(selfPtr);
		}
		return ret;
	}

	internal static void SteamAPI_SteamNetworkingIdentity_Clear(ref SteamNetworkingIdentity self)
	{
		if ((nint)_SteamAPI_SteamNetworkingIdentity_Clear == 0) throw new EntryPointNotFoundException("'SteamAPI_SteamNetworkingIdentity_Clear' is not available.");
		fixed (SteamNetworkingIdentity* selfPtr = &self)
		{
			_SteamAPI_SteamNetworkingIdentity_Clear(selfPtr);
		}
	}

	internal static bool SteamAPI_SteamNetworkingIdentity_IsInvalid(ref SteamNetworkingIdentity self)
	{
		if ((nint)_SteamAPI_SteamNetworkingIdentity_IsInvalid == 0) throw new EntryPointNotFoundException("'SteamAPI_SteamNetworkingIdentity_IsInvalid' is not available.");
		byte ret;
		fixed (SteamNetworkingIdentity* selfPtr = &self)
		{
			ret = _SteamAPI_SteamNetworkingIdentity_IsInvalid(selfPtr);
		}
		return ret != 0;
	}

	internal static void SteamAPI_SteamNetworkingIdentity_SetSteamID(ref SteamNetworkingIdentity self, ulong steamID)
	{
		if ((nint)_SteamAPI_SteamNetworkingIdentity_SetSteamID == 0) throw new EntryPointNotFoundException("'SteamAPI_SteamNetworkingIdentity_SetSteamID' is not available.");
		fixed (SteamNetworkingIdentity* selfPtr = &self)
		{
			_SteamAPI_SteamNetworkingIdentity_SetSteamID(selfPtr, steamID);
		}
	}

	internal static ulong SteamAPI_SteamNetworkingIdentity_GetSteamID(ref SteamNetworkingIdentity self)
	{
		if ((nint)_SteamAPI_SteamNetworkingIdentity_GetSteamID == 0) throw new EntryPointNotFoundException("'SteamAPI_SteamNetworkingIdentity_GetSteamID' is not available.");
		ulong ret;
		fixed (SteamNetworkingIdentity* selfPtr = &self)
		{
			ret = _SteamAPI_SteamNetworkingIdentity_GetSteamID(selfPtr);
		}
		return ret;
	}

	internal static void SteamAPI_SteamNetworkingIdentity_SetSteamID64(ref SteamNetworkingIdentity self, ulong steamID)
	{
		if ((nint)_SteamAPI_SteamNetworkingIdentity_SetSteamID64 == 0) throw new EntryPointNotFoundException("'SteamAPI_SteamNetworkingIdentity_SetSteamID64' is not available.");
		fixed (SteamNetworkingIdentity* selfPtr = &self)
		{
			_SteamAPI_SteamNetworkingIdentity_SetSteamID64(selfPtr, steamID);
		}
	}

	internal static ulong SteamAPI_SteamNetworkingIdentity_GetSteamID64(ref SteamNetworkingIdentity self)
	{
		if ((nint)_SteamAPI_SteamNetworkingIdentity_GetSteamID64 == 0) throw new EntryPointNotFoundException("'SteamAPI_SteamNetworkingIdentity_GetSteamID64' is not available.");
		ulong ret;
		fixed (SteamNetworkingIdentity* selfPtr = &self)
		{
			ret = _SteamAPI_SteamNetworkingIdentity_GetSteamID64(selfPtr);
		}
		return ret;
	}

	internal static bool SteamAPI_SteamNetworkingIdentity_SetXboxPairwiseID(ref SteamNetworkingIdentity self, string pszString)
	{
		if ((nint)_SteamAPI_SteamNetworkingIdentity_SetXboxPairwiseID == 0) throw new EntryPointNotFoundException("'SteamAPI_SteamNetworkingIdentity_SetXboxPairwiseID' is not available.");
		using var pszStringStr = new ScopedCString(pszString ?? string.Empty);
		byte ret;
		fixed (SteamNetworkingIdentity* selfPtr = &self)
		{
			fixed (byte* pszStringBufferPtr = pszStringStr)
			{
				ret = _SteamAPI_SteamNetworkingIdentity_SetXboxPairwiseID(selfPtr, pszString is null ? null : pszStringBufferPtr);
			}
		}
		return ret != 0;
	}

	internal static IntPtr SteamAPI_SteamNetworkingIdentity_GetXboxPairwiseID(ref SteamNetworkingIdentity self)
	{
		if ((nint)_SteamAPI_SteamNetworkingIdentity_GetXboxPairwiseID == 0) throw new EntryPointNotFoundException("'SteamAPI_SteamNetworkingIdentity_GetXboxPairwiseID' is not available.");
		nint ret;
		fixed (SteamNetworkingIdentity* selfPtr = &self)
		{
			ret = _SteamAPI_SteamNetworkingIdentity_GetXboxPairwiseID(selfPtr);
		}
		return ret;
	}

	internal static void SteamAPI_SteamNetworkingIdentity_SetPSNID(ref SteamNetworkingIdentity self, ulong id)
	{
		if ((nint)_SteamAPI_SteamNetworkingIdentity_SetPSNID == 0) throw new EntryPointNotFoundException("'SteamAPI_SteamNetworkingIdentity_SetPSNID' is not available.");
		fixed (SteamNetworkingIdentity* selfPtr = &self)
		{
			_SteamAPI_SteamNetworkingIdentity_SetPSNID(selfPtr, id);
		}
	}

	internal static ulong SteamAPI_SteamNetworkingIdentity_GetPSNID(ref SteamNetworkingIdentity self)
	{
		if ((nint)_SteamAPI_SteamNetworkingIdentity_GetPSNID == 0) throw new EntryPointNotFoundException("'SteamAPI_SteamNetworkingIdentity_GetPSNID' is not available.");
		ulong ret;
		fixed (SteamNetworkingIdentity* selfPtr = &self)
		{
			ret = _SteamAPI_SteamNetworkingIdentity_GetPSNID(selfPtr);
		}
		return ret;
	}

	internal static void SteamAPI_SteamNetworkingIdentity_SetStadiaID(ref SteamNetworkingIdentity self, ulong id)
	{
		if ((nint)_SteamAPI_SteamNetworkingIdentity_SetStadiaID == 0) throw new EntryPointNotFoundException("'SteamAPI_SteamNetworkingIdentity_SetStadiaID' is not available.");
		fixed (SteamNetworkingIdentity* selfPtr = &self)
		{
			_SteamAPI_SteamNetworkingIdentity_SetStadiaID(selfPtr, id);
		}
	}

	internal static ulong SteamAPI_SteamNetworkingIdentity_GetStadiaID(ref SteamNetworkingIdentity self)
	{
		if ((nint)_SteamAPI_SteamNetworkingIdentity_GetStadiaID == 0) throw new EntryPointNotFoundException("'SteamAPI_SteamNetworkingIdentity_GetStadiaID' is not available.");
		ulong ret;
		fixed (SteamNetworkingIdentity* selfPtr = &self)
		{
			ret = _SteamAPI_SteamNetworkingIdentity_GetStadiaID(selfPtr);
		}
		return ret;
	}

	internal static IntPtr SteamAPI_SteamNetworkingIdentity_SetIPAddr(ref SteamNetworkingIdentity self, ref SteamNetworkingIPAddr addr)
	{
		if ((nint)_SteamAPI_SteamNetworkingIdentity_SetIPAddr == 0) throw new EntryPointNotFoundException("'SteamAPI_SteamNetworkingIdentity_SetIPAddr' is not available.");
		nint ret;
		fixed (SteamNetworkingIdentity* selfPtr = &self)
		{
			fixed (SteamNetworkingIPAddr* addrPtr = &addr)
			{
				ret = _SteamAPI_SteamNetworkingIdentity_SetIPAddr(selfPtr, addrPtr);
			}
		}
		return ret;
	}

	internal static IntPtr SteamAPI_SteamNetworkingIdentity_GetIPAddr(ref SteamNetworkingIdentity self)
	{
		if ((nint)_SteamAPI_SteamNetworkingIdentity_GetIPAddr == 0) throw new EntryPointNotFoundException("'SteamAPI_SteamNetworkingIdentity_GetIPAddr' is not available.");
		nint ret;
		fixed (SteamNetworkingIdentity* selfPtr = &self)
		{
			ret = _SteamAPI_SteamNetworkingIdentity_GetIPAddr(selfPtr);
		}
		return ret;
	}

	internal static void SteamAPI_SteamNetworkingIdentity_SetIPv4Addr(ref SteamNetworkingIdentity self, uint nIPv4, ushort nPort)
	{
		if ((nint)_SteamAPI_SteamNetworkingIdentity_SetIPv4Addr == 0) throw new EntryPointNotFoundException("'SteamAPI_SteamNetworkingIdentity_SetIPv4Addr' is not available.");
		fixed (SteamNetworkingIdentity* selfPtr = &self)
		{
			_SteamAPI_SteamNetworkingIdentity_SetIPv4Addr(selfPtr, nIPv4, nPort);
		}
	}

	internal static uint SteamAPI_SteamNetworkingIdentity_GetIPv4(ref SteamNetworkingIdentity self)
	{
		if ((nint)_SteamAPI_SteamNetworkingIdentity_GetIPv4 == 0) throw new EntryPointNotFoundException("'SteamAPI_SteamNetworkingIdentity_GetIPv4' is not available.");
		uint ret;
		fixed (SteamNetworkingIdentity* selfPtr = &self)
		{
			ret = _SteamAPI_SteamNetworkingIdentity_GetIPv4(selfPtr);
		}
		return ret;
	}

	internal static ESteamNetworkingFakeIPType SteamAPI_SteamNetworkingIdentity_GetFakeIPType(ref SteamNetworkingIdentity self)
	{
		if ((nint)_SteamAPI_SteamNetworkingIdentity_GetFakeIPType == 0) throw new EntryPointNotFoundException("'SteamAPI_SteamNetworkingIdentity_GetFakeIPType' is not available.");
		ESteamNetworkingFakeIPType ret;
		fixed (SteamNetworkingIdentity* selfPtr = &self)
		{
			ret = _SteamAPI_SteamNetworkingIdentity_GetFakeIPType(selfPtr);
		}
		return ret;
	}

	internal static void SteamAPI_SteamNetworkingIdentity_SetLocalHost(ref SteamNetworkingIdentity self)
	{
		if ((nint)_SteamAPI_SteamNetworkingIdentity_SetLocalHost == 0) throw new EntryPointNotFoundException("'SteamAPI_SteamNetworkingIdentity_SetLocalHost' is not available.");
		fixed (SteamNetworkingIdentity* selfPtr = &self)
		{
			_SteamAPI_SteamNetworkingIdentity_SetLocalHost(selfPtr);
		}
	}

	internal static bool SteamAPI_SteamNetworkingIdentity_IsLocalHost(ref SteamNetworkingIdentity self)
	{
		if ((nint)_SteamAPI_SteamNetworkingIdentity_IsLocalHost == 0) throw new EntryPointNotFoundException("'SteamAPI_SteamNetworkingIdentity_IsLocalHost' is not available.");
		byte ret;
		fixed (SteamNetworkingIdentity* selfPtr = &self)
		{
			ret = _SteamAPI_SteamNetworkingIdentity_IsLocalHost(selfPtr);
		}
		return ret != 0;
	}

	internal static bool SteamAPI_SteamNetworkingIdentity_SetGenericString(ref SteamNetworkingIdentity self, string pszString)
	{
		if ((nint)_SteamAPI_SteamNetworkingIdentity_SetGenericString == 0) throw new EntryPointNotFoundException("'SteamAPI_SteamNetworkingIdentity_SetGenericString' is not available.");
		using var pszStringStr = new ScopedCString(pszString ?? string.Empty);
		byte ret;
		fixed (SteamNetworkingIdentity* selfPtr = &self)
		{
			fixed (byte* pszStringBufferPtr = pszStringStr)
			{
				ret = _SteamAPI_SteamNetworkingIdentity_SetGenericString(selfPtr, pszString is null ? null : pszStringBufferPtr);
			}
		}
		return ret != 0;
	}

	internal static IntPtr SteamAPI_SteamNetworkingIdentity_GetGenericString(ref SteamNetworkingIdentity self)
	{
		if ((nint)_SteamAPI_SteamNetworkingIdentity_GetGenericString == 0) throw new EntryPointNotFoundException("'SteamAPI_SteamNetworkingIdentity_GetGenericString' is not available.");
		nint ret;
		fixed (SteamNetworkingIdentity* selfPtr = &self)
		{
			ret = _SteamAPI_SteamNetworkingIdentity_GetGenericString(selfPtr);
		}
		return ret;
	}

	internal static bool SteamAPI_SteamNetworkingIdentity_SetGenericBytes(ref SteamNetworkingIdentity self, byte[] data, uint cbLen)
	{
		if ((nint)_SteamAPI_SteamNetworkingIdentity_SetGenericBytes == 0) throw new EntryPointNotFoundException("'SteamAPI_SteamNetworkingIdentity_SetGenericBytes' is not available.");
		byte ret;
		fixed (SteamNetworkingIdentity* selfPtr = &self)
		{
			fixed (byte* dataPtr = data)
			{
				ret = _SteamAPI_SteamNetworkingIdentity_SetGenericBytes(selfPtr, dataPtr, cbLen);
			}
		}
		return ret != 0;
	}

	internal static IntPtr SteamAPI_SteamNetworkingIdentity_GetGenericBytes(ref SteamNetworkingIdentity self, out int cbLen)
	{
		if ((nint)_SteamAPI_SteamNetworkingIdentity_GetGenericBytes == 0) throw new EntryPointNotFoundException("'SteamAPI_SteamNetworkingIdentity_GetGenericBytes' is not available.");
		cbLen = default;
		nint ret;
		fixed (SteamNetworkingIdentity* selfPtr = &self)
		{
			fixed (int* cbLenPtr = &cbLen)
			{
				ret = _SteamAPI_SteamNetworkingIdentity_GetGenericBytes(selfPtr, cbLenPtr);
			}
		}
		return ret;
	}

	internal static bool SteamAPI_SteamNetworkingIdentity_IsEqualTo(ref SteamNetworkingIdentity self, ref SteamNetworkingIdentity x)
	{
		if ((nint)_SteamAPI_SteamNetworkingIdentity_IsEqualTo == 0) throw new EntryPointNotFoundException("'SteamAPI_SteamNetworkingIdentity_IsEqualTo' is not available.");
		byte ret;
		fixed (SteamNetworkingIdentity* selfPtr = &self)
		{
			fixed (SteamNetworkingIdentity* xPtr = &x)
			{
				ret = _SteamAPI_SteamNetworkingIdentity_IsEqualTo(selfPtr, xPtr);
			}
		}
		return ret != 0;
	}

	internal static void SteamAPI_SteamNetworkingIdentity_ToString(ref SteamNetworkingIdentity self, IntPtr buf, uint cbBuf)
	{
		if ((nint)_SteamAPI_SteamNetworkingIdentity_ToString == 0) throw new EntryPointNotFoundException("'SteamAPI_SteamNetworkingIdentity_ToString' is not available.");
		fixed (SteamNetworkingIdentity* selfPtr = &self)
		{
			_SteamAPI_SteamNetworkingIdentity_ToString(selfPtr, buf, cbBuf);
		}
	}

	internal static bool SteamAPI_SteamNetworkingIdentity_ParseString(ref SteamNetworkingIdentity self, string pszStr)
	{
		if ((nint)_SteamAPI_SteamNetworkingIdentity_ParseString == 0) throw new EntryPointNotFoundException("'SteamAPI_SteamNetworkingIdentity_ParseString' is not available.");
		using var pszStrStr = new ScopedCString(pszStr ?? string.Empty);
		byte ret;
		fixed (SteamNetworkingIdentity* selfPtr = &self)
		{
			fixed (byte* pszStrBufferPtr = pszStrStr)
			{
				ret = _SteamAPI_SteamNetworkingIdentity_ParseString(selfPtr, pszStr is null ? null : pszStrBufferPtr);
			}
		}
		return ret != 0;
	}

	internal static void SteamAPI_SteamNetworkingMessage_t_Release(IntPtr self)
	{
		if ((nint)_SteamAPI_SteamNetworkingMessage_t_Release == 0) throw new EntryPointNotFoundException("'SteamAPI_SteamNetworkingMessage_t_Release' is not available.");
		_SteamAPI_SteamNetworkingMessage_t_Release(self);
	}

	internal static bool SteamAPI_ISteamNetworkingConnectionSignaling_SendSignal(ref ISteamNetworkingConnectionSignaling self, HSteamNetConnection hConn, ref SteamNetConnectionInfo_t info, IntPtr pMsg, int cbMsg)
	{
		if ((nint)_SteamAPI_ISteamNetworkingConnectionSignaling_SendSignal == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingConnectionSignaling_SendSignal' is not available.");
		byte ret;
		fixed (ISteamNetworkingConnectionSignaling* selfPtr = &self)
		{
			fixed (SteamNetConnectionInfo_t* infoPtr = &info)
			{
				ret = _SteamAPI_ISteamNetworkingConnectionSignaling_SendSignal(selfPtr, hConn, infoPtr, pMsg, cbMsg);
			}
		}
		return ret != 0;
	}

	internal static void SteamAPI_ISteamNetworkingConnectionSignaling_Release(ref ISteamNetworkingConnectionSignaling self)
	{
		if ((nint)_SteamAPI_ISteamNetworkingConnectionSignaling_Release == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingConnectionSignaling_Release' is not available.");
		fixed (ISteamNetworkingConnectionSignaling* selfPtr = &self)
		{
			_SteamAPI_ISteamNetworkingConnectionSignaling_Release(selfPtr);
		}
	}

	internal static IntPtr SteamAPI_ISteamNetworkingSignalingRecvContext_OnConnectRequest(ref ISteamNetworkingSignalingRecvContext self, HSteamNetConnection hConn, ref SteamNetworkingIdentity identityPeer, int nLocalVirtualPort)
	{
		if ((nint)_SteamAPI_ISteamNetworkingSignalingRecvContext_OnConnectRequest == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingSignalingRecvContext_OnConnectRequest' is not available.");
		nint ret;
		fixed (ISteamNetworkingSignalingRecvContext* selfPtr = &self)
		{
			fixed (SteamNetworkingIdentity* identityPeerPtr = &identityPeer)
			{
				ret = _SteamAPI_ISteamNetworkingSignalingRecvContext_OnConnectRequest(selfPtr, hConn, identityPeerPtr, nLocalVirtualPort);
			}
		}
		return ret;
	}

	internal static void SteamAPI_ISteamNetworkingSignalingRecvContext_SendRejectionSignal(ref ISteamNetworkingSignalingRecvContext self, ref SteamNetworkingIdentity identityPeer, IntPtr pMsg, int cbMsg)
	{
		if ((nint)_SteamAPI_ISteamNetworkingSignalingRecvContext_SendRejectionSignal == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingSignalingRecvContext_SendRejectionSignal' is not available.");
		fixed (ISteamNetworkingSignalingRecvContext* selfPtr = &self)
		{
			fixed (SteamNetworkingIdentity* identityPeerPtr = &identityPeer)
			{
				_SteamAPI_ISteamNetworkingSignalingRecvContext_SendRejectionSignal(selfPtr, identityPeerPtr, pMsg, cbMsg);
			}
		}
	}

	internal static int ISteamClient_CreateSteamPipe(IntPtr instancePtr)
	{
		if ((nint)_ISteamClient_CreateSteamPipe == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamClient_CreateSteamPipe' is not available.");
		return _ISteamClient_CreateSteamPipe(instancePtr);
	}

	internal static bool ISteamClient_BReleaseSteamPipe(IntPtr instancePtr, HSteamPipe hSteamPipe)
	{
		if ((nint)_ISteamClient_BReleaseSteamPipe == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamClient_BReleaseSteamPipe' is not available.");
		return _ISteamClient_BReleaseSteamPipe(instancePtr, hSteamPipe) != 0;
	}

	internal static int ISteamClient_ConnectToGlobalUser(IntPtr instancePtr, HSteamPipe hSteamPipe)
	{
		if ((nint)_ISteamClient_ConnectToGlobalUser == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamClient_ConnectToGlobalUser' is not available.");
		return _ISteamClient_ConnectToGlobalUser(instancePtr, hSteamPipe);
	}

	internal static int ISteamClient_CreateLocalUser(IntPtr instancePtr, out HSteamPipe phSteamPipe, EAccountType eAccountType)
	{
		if ((nint)_ISteamClient_CreateLocalUser == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamClient_CreateLocalUser' is not available.");
		phSteamPipe = default;
		int ret;
		fixed (HSteamPipe* phSteamPipePtr = &phSteamPipe)
		{
			ret = _ISteamClient_CreateLocalUser(instancePtr, phSteamPipePtr, eAccountType);
		}
		return ret;
	}

	internal static void ISteamClient_ReleaseUser(IntPtr instancePtr, HSteamPipe hSteamPipe, HSteamUser hUser)
	{
		if ((nint)_ISteamClient_ReleaseUser == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamClient_ReleaseUser' is not available.");
		_ISteamClient_ReleaseUser(instancePtr, hSteamPipe, hUser);
	}

	internal static IntPtr ISteamClient_GetISteamUser(IntPtr instancePtr, HSteamUser hSteamUser, HSteamPipe hSteamPipe, string? pchVersion)
	{
		if ((nint)_ISteamClient_GetISteamUser == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamClient_GetISteamUser' is not available.");
		using var pchVersionStr = new ScopedCString(pchVersion ?? string.Empty);
		nint ret;
		fixed (byte* pchVersionBufferPtr = pchVersionStr)
		{
			ret = _ISteamClient_GetISteamUser(instancePtr, hSteamUser, hSteamPipe, pchVersion is null ? null : pchVersionBufferPtr);
		}
		return ret;
	}

	internal static IntPtr ISteamClient_GetISteamGameServer(IntPtr instancePtr, HSteamUser hSteamUser, HSteamPipe hSteamPipe, string? pchVersion)
	{
		if ((nint)_ISteamClient_GetISteamGameServer == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamClient_GetISteamGameServer' is not available.");
		using var pchVersionStr = new ScopedCString(pchVersion ?? string.Empty);
		nint ret;
		fixed (byte* pchVersionBufferPtr = pchVersionStr)
		{
			ret = _ISteamClient_GetISteamGameServer(instancePtr, hSteamUser, hSteamPipe, pchVersion is null ? null : pchVersionBufferPtr);
		}
		return ret;
	}

	internal static void ISteamClient_SetLocalIPBinding(IntPtr instancePtr, ref SteamIPAddress_t unIP, ushort usPort)
	{
		if ((nint)_ISteamClient_SetLocalIPBinding == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamClient_SetLocalIPBinding' is not available.");
		fixed (SteamIPAddress_t* unIPPtr = &unIP)
		{
			_ISteamClient_SetLocalIPBinding(instancePtr, unIPPtr, usPort);
		}
	}

	internal static IntPtr ISteamClient_GetISteamFriends(IntPtr instancePtr, HSteamUser hSteamUser, HSteamPipe hSteamPipe, string? pchVersion)
	{
		if ((nint)_ISteamClient_GetISteamFriends == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamClient_GetISteamFriends' is not available.");
		using var pchVersionStr = new ScopedCString(pchVersion ?? string.Empty);
		nint ret;
		fixed (byte* pchVersionBufferPtr = pchVersionStr)
		{
			ret = _ISteamClient_GetISteamFriends(instancePtr, hSteamUser, hSteamPipe, pchVersion is null ? null : pchVersionBufferPtr);
		}
		return ret;
	}

	internal static IntPtr ISteamClient_GetISteamUtils(IntPtr instancePtr, HSteamPipe hSteamPipe, string? pchVersion)
	{
		if ((nint)_ISteamClient_GetISteamUtils == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamClient_GetISteamUtils' is not available.");
		using var pchVersionStr = new ScopedCString(pchVersion ?? string.Empty);
		nint ret;
		fixed (byte* pchVersionBufferPtr = pchVersionStr)
		{
			ret = _ISteamClient_GetISteamUtils(instancePtr, hSteamPipe, pchVersion is null ? null : pchVersionBufferPtr);
		}
		return ret;
	}

	internal static IntPtr ISteamClient_GetISteamGenericInterface(IntPtr instancePtr, HSteamUser hSteamUser, HSteamPipe hSteamPipe, string? pchVersion)
	{
		if ((nint)_ISteamClient_GetISteamGenericInterface == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamClient_GetISteamGenericInterface' is not available.");
		using var pchVersionStr = new ScopedCString(pchVersion ?? string.Empty);
		nint ret;
		fixed (byte* pchVersionBufferPtr = pchVersionStr)
		{
			ret = _ISteamClient_GetISteamGenericInterface(instancePtr, hSteamUser, hSteamPipe, pchVersion is null ? null : pchVersionBufferPtr);
		}
		return ret;
	}

	internal static IntPtr ISteamClient_GetISteamUserStats(IntPtr instancePtr, HSteamUser hSteamUser, HSteamPipe hSteamPipe, string? pchVersion)
	{
		if ((nint)_ISteamClient_GetISteamUserStats == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamClient_GetISteamUserStats' is not available.");
		using var pchVersionStr = new ScopedCString(pchVersion ?? string.Empty);
		nint ret;
		fixed (byte* pchVersionBufferPtr = pchVersionStr)
		{
			ret = _ISteamClient_GetISteamUserStats(instancePtr, hSteamUser, hSteamPipe, pchVersion is null ? null : pchVersionBufferPtr);
		}
		return ret;
	}

	internal static IntPtr ISteamClient_GetISteamGameServerStats(IntPtr instancePtr, HSteamUser hSteamuser, HSteamPipe hSteamPipe, string? pchVersion)
	{
		if ((nint)_ISteamClient_GetISteamGameServerStats == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamClient_GetISteamGameServerStats' is not available.");
		using var pchVersionStr = new ScopedCString(pchVersion ?? string.Empty);
		nint ret;
		fixed (byte* pchVersionBufferPtr = pchVersionStr)
		{
			ret = _ISteamClient_GetISteamGameServerStats(instancePtr, hSteamuser, hSteamPipe, pchVersion is null ? null : pchVersionBufferPtr);
		}
		return ret;
	}

	internal static IntPtr ISteamClient_GetISteamApps(IntPtr instancePtr, HSteamUser hSteamUser, HSteamPipe hSteamPipe, string? pchVersion)
	{
		if ((nint)_ISteamClient_GetISteamApps == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamClient_GetISteamApps' is not available.");
		using var pchVersionStr = new ScopedCString(pchVersion ?? string.Empty);
		nint ret;
		fixed (byte* pchVersionBufferPtr = pchVersionStr)
		{
			ret = _ISteamClient_GetISteamApps(instancePtr, hSteamUser, hSteamPipe, pchVersion is null ? null : pchVersionBufferPtr);
		}
		return ret;
	}

	internal static IntPtr ISteamClient_GetISteamNetworking(IntPtr instancePtr, HSteamUser hSteamUser, HSteamPipe hSteamPipe, string? pchVersion)
	{
		if ((nint)_ISteamClient_GetISteamNetworking == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamClient_GetISteamNetworking' is not available.");
		using var pchVersionStr = new ScopedCString(pchVersion ?? string.Empty);
		nint ret;
		fixed (byte* pchVersionBufferPtr = pchVersionStr)
		{
			ret = _ISteamClient_GetISteamNetworking(instancePtr, hSteamUser, hSteamPipe, pchVersion is null ? null : pchVersionBufferPtr);
		}
		return ret;
	}

	internal static IntPtr ISteamClient_GetISteamRemoteStorage(IntPtr instancePtr, HSteamUser hSteamuser, HSteamPipe hSteamPipe, string? pchVersion)
	{
		if ((nint)_ISteamClient_GetISteamRemoteStorage == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamClient_GetISteamRemoteStorage' is not available.");
		using var pchVersionStr = new ScopedCString(pchVersion ?? string.Empty);
		nint ret;
		fixed (byte* pchVersionBufferPtr = pchVersionStr)
		{
			ret = _ISteamClient_GetISteamRemoteStorage(instancePtr, hSteamuser, hSteamPipe, pchVersion is null ? null : pchVersionBufferPtr);
		}
		return ret;
	}

	internal static IntPtr ISteamClient_GetISteamScreenshots(IntPtr instancePtr, HSteamUser hSteamuser, HSteamPipe hSteamPipe, string? pchVersion)
	{
		if ((nint)_ISteamClient_GetISteamScreenshots == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamClient_GetISteamScreenshots' is not available.");
		using var pchVersionStr = new ScopedCString(pchVersion ?? string.Empty);
		nint ret;
		fixed (byte* pchVersionBufferPtr = pchVersionStr)
		{
			ret = _ISteamClient_GetISteamScreenshots(instancePtr, hSteamuser, hSteamPipe, pchVersion is null ? null : pchVersionBufferPtr);
		}
		return ret;
	}

	internal static IntPtr ISteamClient_GetISteamGameSearch(IntPtr instancePtr, HSteamUser hSteamuser, HSteamPipe hSteamPipe, string? pchVersion)
	{
		if ((nint)_ISteamClient_GetISteamGameSearch == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamClient_GetISteamGameSearch' is not available.");
		using var pchVersionStr = new ScopedCString(pchVersion ?? string.Empty);
		nint ret;
		fixed (byte* pchVersionBufferPtr = pchVersionStr)
		{
			ret = _ISteamClient_GetISteamGameSearch(instancePtr, hSteamuser, hSteamPipe, pchVersion is null ? null : pchVersionBufferPtr);
		}
		return ret;
	}

	internal static uint ISteamClient_GetIPCCallCount(IntPtr instancePtr)
	{
		if ((nint)_ISteamClient_GetIPCCallCount == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamClient_GetIPCCallCount' is not available.");
		return _ISteamClient_GetIPCCallCount(instancePtr);
	}

	internal static void ISteamClient_SetWarningMessageHook(IntPtr instancePtr, SteamAPIWarningMessageHook_t pFunction)
	{
		if ((nint)_ISteamClient_SetWarningMessageHook == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamClient_SetWarningMessageHook' is not available.");
		_ISteamClient_SetWarningMessageHook(instancePtr, pFunction is null ? 0 : Marshal.GetFunctionPointerForDelegate(pFunction));
	}

	internal static bool ISteamClient_BShutdownIfAllPipesClosed(IntPtr instancePtr)
	{
		if ((nint)_ISteamClient_BShutdownIfAllPipesClosed == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamClient_BShutdownIfAllPipesClosed' is not available.");
		return _ISteamClient_BShutdownIfAllPipesClosed(instancePtr) != 0;
	}

	internal static IntPtr ISteamClient_GetISteamHTTP(IntPtr instancePtr, HSteamUser hSteamuser, HSteamPipe hSteamPipe, string? pchVersion)
	{
		if ((nint)_ISteamClient_GetISteamHTTP == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamClient_GetISteamHTTP' is not available.");
		using var pchVersionStr = new ScopedCString(pchVersion ?? string.Empty);
		nint ret;
		fixed (byte* pchVersionBufferPtr = pchVersionStr)
		{
			ret = _ISteamClient_GetISteamHTTP(instancePtr, hSteamuser, hSteamPipe, pchVersion is null ? null : pchVersionBufferPtr);
		}
		return ret;
	}

	internal static IntPtr ISteamClient_GetISteamController(IntPtr instancePtr, HSteamUser hSteamUser, HSteamPipe hSteamPipe, string? pchVersion)
	{
		if ((nint)_ISteamClient_GetISteamController == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamClient_GetISteamController' is not available.");
		using var pchVersionStr = new ScopedCString(pchVersion ?? string.Empty);
		nint ret;
		fixed (byte* pchVersionBufferPtr = pchVersionStr)
		{
			ret = _ISteamClient_GetISteamController(instancePtr, hSteamUser, hSteamPipe, pchVersion is null ? null : pchVersionBufferPtr);
		}
		return ret;
	}

	internal static IntPtr ISteamClient_GetISteamUGC(IntPtr instancePtr, HSteamUser hSteamUser, HSteamPipe hSteamPipe, string? pchVersion)
	{
		if ((nint)_ISteamClient_GetISteamUGC == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamClient_GetISteamUGC' is not available.");
		using var pchVersionStr = new ScopedCString(pchVersion ?? string.Empty);
		nint ret;
		fixed (byte* pchVersionBufferPtr = pchVersionStr)
		{
			ret = _ISteamClient_GetISteamUGC(instancePtr, hSteamUser, hSteamPipe, pchVersion is null ? null : pchVersionBufferPtr);
		}
		return ret;
	}

	internal static IntPtr ISteamClient_GetISteamMusic(IntPtr instancePtr, HSteamUser hSteamuser, HSteamPipe hSteamPipe, string? pchVersion)
	{
		if ((nint)_ISteamClient_GetISteamMusic == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamClient_GetISteamMusic' is not available.");
		using var pchVersionStr = new ScopedCString(pchVersion ?? string.Empty);
		nint ret;
		fixed (byte* pchVersionBufferPtr = pchVersionStr)
		{
			ret = _ISteamClient_GetISteamMusic(instancePtr, hSteamuser, hSteamPipe, pchVersion is null ? null : pchVersionBufferPtr);
		}
		return ret;
	}

	internal static IntPtr ISteamClient_GetISteamMusicRemote(IntPtr instancePtr, HSteamUser hSteamuser, HSteamPipe hSteamPipe, string? pchVersion)
	{
		if ((nint)_ISteamClient_GetISteamMusicRemote == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamClient_GetISteamMusicRemote' is not available.");
		using var pchVersionStr = new ScopedCString(pchVersion ?? string.Empty);
		nint ret;
		fixed (byte* pchVersionBufferPtr = pchVersionStr)
		{
			ret = _ISteamClient_GetISteamMusicRemote(instancePtr, hSteamuser, hSteamPipe, pchVersion is null ? null : pchVersionBufferPtr);
		}
		return ret;
	}

	internal static IntPtr ISteamClient_GetISteamHTMLSurface(IntPtr instancePtr, HSteamUser hSteamuser, HSteamPipe hSteamPipe, string? pchVersion)
	{
		if ((nint)_ISteamClient_GetISteamHTMLSurface == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamClient_GetISteamHTMLSurface' is not available.");
		using var pchVersionStr = new ScopedCString(pchVersion ?? string.Empty);
		nint ret;
		fixed (byte* pchVersionBufferPtr = pchVersionStr)
		{
			ret = _ISteamClient_GetISteamHTMLSurface(instancePtr, hSteamuser, hSteamPipe, pchVersion is null ? null : pchVersionBufferPtr);
		}
		return ret;
	}

	internal static IntPtr ISteamClient_GetISteamInventory(IntPtr instancePtr, HSteamUser hSteamuser, HSteamPipe hSteamPipe, string? pchVersion)
	{
		if ((nint)_ISteamClient_GetISteamInventory == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamClient_GetISteamInventory' is not available.");
		using var pchVersionStr = new ScopedCString(pchVersion ?? string.Empty);
		nint ret;
		fixed (byte* pchVersionBufferPtr = pchVersionStr)
		{
			ret = _ISteamClient_GetISteamInventory(instancePtr, hSteamuser, hSteamPipe, pchVersion is null ? null : pchVersionBufferPtr);
		}
		return ret;
	}

	internal static IntPtr ISteamClient_GetISteamVideo(IntPtr instancePtr, HSteamUser hSteamuser, HSteamPipe hSteamPipe, string? pchVersion)
	{
		if ((nint)_ISteamClient_GetISteamVideo == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamClient_GetISteamVideo' is not available.");
		using var pchVersionStr = new ScopedCString(pchVersion ?? string.Empty);
		nint ret;
		fixed (byte* pchVersionBufferPtr = pchVersionStr)
		{
			ret = _ISteamClient_GetISteamVideo(instancePtr, hSteamuser, hSteamPipe, pchVersion is null ? null : pchVersionBufferPtr);
		}
		return ret;
	}

	internal static IntPtr ISteamClient_GetISteamParentalSettings(IntPtr instancePtr, HSteamUser hSteamuser, HSteamPipe hSteamPipe, string? pchVersion)
	{
		if ((nint)_ISteamClient_GetISteamParentalSettings == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamClient_GetISteamParentalSettings' is not available.");
		using var pchVersionStr = new ScopedCString(pchVersion ?? string.Empty);
		nint ret;
		fixed (byte* pchVersionBufferPtr = pchVersionStr)
		{
			ret = _ISteamClient_GetISteamParentalSettings(instancePtr, hSteamuser, hSteamPipe, pchVersion is null ? null : pchVersionBufferPtr);
		}
		return ret;
	}

	internal static IntPtr ISteamClient_GetISteamInput(IntPtr instancePtr, HSteamUser hSteamUser, HSteamPipe hSteamPipe, string? pchVersion)
	{
		if ((nint)_ISteamClient_GetISteamInput == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamClient_GetISteamInput' is not available.");
		using var pchVersionStr = new ScopedCString(pchVersion ?? string.Empty);
		nint ret;
		fixed (byte* pchVersionBufferPtr = pchVersionStr)
		{
			ret = _ISteamClient_GetISteamInput(instancePtr, hSteamUser, hSteamPipe, pchVersion is null ? null : pchVersionBufferPtr);
		}
		return ret;
	}

	internal static IntPtr ISteamClient_GetISteamParties(IntPtr instancePtr, HSteamUser hSteamUser, HSteamPipe hSteamPipe, string? pchVersion)
	{
		if ((nint)_ISteamClient_GetISteamParties == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamClient_GetISteamParties' is not available.");
		using var pchVersionStr = new ScopedCString(pchVersion ?? string.Empty);
		nint ret;
		fixed (byte* pchVersionBufferPtr = pchVersionStr)
		{
			ret = _ISteamClient_GetISteamParties(instancePtr, hSteamUser, hSteamPipe, pchVersion is null ? null : pchVersionBufferPtr);
		}
		return ret;
	}

	internal static IntPtr ISteamClient_GetISteamRemotePlay(IntPtr instancePtr, HSteamUser hSteamUser, HSteamPipe hSteamPipe, string? pchVersion)
	{
		if ((nint)_ISteamClient_GetISteamRemotePlay == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamClient_GetISteamRemotePlay' is not available.");
		using var pchVersionStr = new ScopedCString(pchVersion ?? string.Empty);
		nint ret;
		fixed (byte* pchVersionBufferPtr = pchVersionStr)
		{
			ret = _ISteamClient_GetISteamRemotePlay(instancePtr, hSteamUser, hSteamPipe, pchVersion is null ? null : pchVersionBufferPtr);
		}
		return ret;
	}

	internal static void ISteamGameServer_SetProduct(IntPtr instancePtr, string? pszProduct)
	{
		if ((nint)_ISteamGameServer_SetProduct == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServer_SetProduct' is not available.");
		using var pszProductStr = new ScopedCString(pszProduct ?? string.Empty);
		fixed (byte* pszProductBufferPtr = pszProductStr)
		{
			_ISteamGameServer_SetProduct(instancePtr, pszProduct is null ? null : pszProductBufferPtr);
		}
	}

	internal static void ISteamGameServer_SetGameDescription(IntPtr instancePtr, string? pszGameDescription)
	{
		if ((nint)_ISteamGameServer_SetGameDescription == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServer_SetGameDescription' is not available.");
		using var pszGameDescriptionStr = new ScopedCString(pszGameDescription ?? string.Empty);
		fixed (byte* pszGameDescriptionBufferPtr = pszGameDescriptionStr)
		{
			_ISteamGameServer_SetGameDescription(instancePtr, pszGameDescription is null ? null : pszGameDescriptionBufferPtr);
		}
	}

	internal static void ISteamGameServer_SetModDir(IntPtr instancePtr, string? pszModDir)
	{
		if ((nint)_ISteamGameServer_SetModDir == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServer_SetModDir' is not available.");
		using var pszModDirStr = new ScopedCString(pszModDir ?? string.Empty);
		fixed (byte* pszModDirBufferPtr = pszModDirStr)
		{
			_ISteamGameServer_SetModDir(instancePtr, pszModDir is null ? null : pszModDirBufferPtr);
		}
	}

	internal static void ISteamGameServer_SetDedicatedServer(IntPtr instancePtr, bool bDedicated)
	{
		if ((nint)_ISteamGameServer_SetDedicatedServer == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServer_SetDedicatedServer' is not available.");
		_ISteamGameServer_SetDedicatedServer(instancePtr, (byte)(bDedicated ? 1 : 0));
	}

	internal static void ISteamGameServer_LogOn(IntPtr instancePtr, string? pszToken)
	{
		if ((nint)_ISteamGameServer_LogOn == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServer_LogOn' is not available.");
		using var pszTokenStr = new ScopedCString(pszToken ?? string.Empty);
		fixed (byte* pszTokenBufferPtr = pszTokenStr)
		{
			_ISteamGameServer_LogOn(instancePtr, pszToken is null ? null : pszTokenBufferPtr);
		}
	}

	internal static void ISteamGameServer_LogOnAnonymous(IntPtr instancePtr)
	{
		if ((nint)_ISteamGameServer_LogOnAnonymous == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServer_LogOnAnonymous' is not available.");
		_ISteamGameServer_LogOnAnonymous(instancePtr);
	}

	internal static void ISteamGameServer_LogOff(IntPtr instancePtr)
	{
		if ((nint)_ISteamGameServer_LogOff == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServer_LogOff' is not available.");
		_ISteamGameServer_LogOff(instancePtr);
	}

	internal static bool ISteamGameServer_BLoggedOn(IntPtr instancePtr)
	{
		if ((nint)_ISteamGameServer_BLoggedOn == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServer_BLoggedOn' is not available.");
		return _ISteamGameServer_BLoggedOn(instancePtr) != 0;
	}

	internal static bool ISteamGameServer_BSecure(IntPtr instancePtr)
	{
		if ((nint)_ISteamGameServer_BSecure == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServer_BSecure' is not available.");
		return _ISteamGameServer_BSecure(instancePtr) != 0;
	}

	internal static ulong ISteamGameServer_GetSteamID(IntPtr instancePtr)
	{
		if ((nint)_ISteamGameServer_GetSteamID == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServer_GetSteamID' is not available.");
		return _ISteamGameServer_GetSteamID(instancePtr);
	}

	internal static bool ISteamGameServer_WasRestartRequested(IntPtr instancePtr)
	{
		if ((nint)_ISteamGameServer_WasRestartRequested == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServer_WasRestartRequested' is not available.");
		return _ISteamGameServer_WasRestartRequested(instancePtr) != 0;
	}

	internal static void ISteamGameServer_SetMaxPlayerCount(IntPtr instancePtr, int cPlayersMax)
	{
		if ((nint)_ISteamGameServer_SetMaxPlayerCount == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServer_SetMaxPlayerCount' is not available.");
		_ISteamGameServer_SetMaxPlayerCount(instancePtr, cPlayersMax);
	}

	internal static void ISteamGameServer_SetBotPlayerCount(IntPtr instancePtr, int cBotplayers)
	{
		if ((nint)_ISteamGameServer_SetBotPlayerCount == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServer_SetBotPlayerCount' is not available.");
		_ISteamGameServer_SetBotPlayerCount(instancePtr, cBotplayers);
	}

	internal static void ISteamGameServer_SetServerName(IntPtr instancePtr, string? pszServerName)
	{
		if ((nint)_ISteamGameServer_SetServerName == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServer_SetServerName' is not available.");
		using var pszServerNameStr = new ScopedCString(pszServerName ?? string.Empty);
		fixed (byte* pszServerNameBufferPtr = pszServerNameStr)
		{
			_ISteamGameServer_SetServerName(instancePtr, pszServerName is null ? null : pszServerNameBufferPtr);
		}
	}

	internal static void ISteamGameServer_SetMapName(IntPtr instancePtr, string? pszMapName)
	{
		if ((nint)_ISteamGameServer_SetMapName == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServer_SetMapName' is not available.");
		using var pszMapNameStr = new ScopedCString(pszMapName ?? string.Empty);
		fixed (byte* pszMapNameBufferPtr = pszMapNameStr)
		{
			_ISteamGameServer_SetMapName(instancePtr, pszMapName is null ? null : pszMapNameBufferPtr);
		}
	}

	internal static void ISteamGameServer_SetPasswordProtected(IntPtr instancePtr, bool bPasswordProtected)
	{
		if ((nint)_ISteamGameServer_SetPasswordProtected == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServer_SetPasswordProtected' is not available.");
		_ISteamGameServer_SetPasswordProtected(instancePtr, (byte)(bPasswordProtected ? 1 : 0));
	}

	internal static void ISteamGameServer_SetSpectatorPort(IntPtr instancePtr, ushort unSpectatorPort)
	{
		if ((nint)_ISteamGameServer_SetSpectatorPort == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServer_SetSpectatorPort' is not available.");
		_ISteamGameServer_SetSpectatorPort(instancePtr, unSpectatorPort);
	}

	internal static void ISteamGameServer_SetSpectatorServerName(IntPtr instancePtr, string? pszSpectatorServerName)
	{
		if ((nint)_ISteamGameServer_SetSpectatorServerName == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServer_SetSpectatorServerName' is not available.");
		using var pszSpectatorServerNameStr = new ScopedCString(pszSpectatorServerName ?? string.Empty);
		fixed (byte* pszSpectatorServerNameBufferPtr = pszSpectatorServerNameStr)
		{
			_ISteamGameServer_SetSpectatorServerName(instancePtr, pszSpectatorServerName is null ? null : pszSpectatorServerNameBufferPtr);
		}
	}

	internal static void ISteamGameServer_ClearAllKeyValues(IntPtr instancePtr)
	{
		if ((nint)_ISteamGameServer_ClearAllKeyValues == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServer_ClearAllKeyValues' is not available.");
		_ISteamGameServer_ClearAllKeyValues(instancePtr);
	}

	internal static void ISteamGameServer_SetKeyValue(IntPtr instancePtr, string? pKey, string? pValue)
	{
		if ((nint)_ISteamGameServer_SetKeyValue == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServer_SetKeyValue' is not available.");
		using var pKeyStr = new ScopedCString(pKey ?? string.Empty);
		using var pValueStr = new ScopedCString(pValue ?? string.Empty);
		fixed (byte* pKeyBufferPtr = pKeyStr)
		{
			fixed (byte* pValueBufferPtr = pValueStr)
			{
				_ISteamGameServer_SetKeyValue(instancePtr, pKey is null ? null : pKeyBufferPtr, pValue is null ? null : pValueBufferPtr);
			}
		}
	}

	internal static void ISteamGameServer_SetGameTags(IntPtr instancePtr, string? pchGameTags)
	{
		if ((nint)_ISteamGameServer_SetGameTags == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServer_SetGameTags' is not available.");
		using var pchGameTagsStr = new ScopedCString(pchGameTags ?? string.Empty);
		fixed (byte* pchGameTagsBufferPtr = pchGameTagsStr)
		{
			_ISteamGameServer_SetGameTags(instancePtr, pchGameTags is null ? null : pchGameTagsBufferPtr);
		}
	}

	internal static void ISteamGameServer_SetGameData(IntPtr instancePtr, string? pchGameData)
	{
		if ((nint)_ISteamGameServer_SetGameData == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServer_SetGameData' is not available.");
		using var pchGameDataStr = new ScopedCString(pchGameData ?? string.Empty);
		fixed (byte* pchGameDataBufferPtr = pchGameDataStr)
		{
			_ISteamGameServer_SetGameData(instancePtr, pchGameData is null ? null : pchGameDataBufferPtr);
		}
	}

	internal static void ISteamGameServer_SetRegion(IntPtr instancePtr, string? pszRegion)
	{
		if ((nint)_ISteamGameServer_SetRegion == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServer_SetRegion' is not available.");
		using var pszRegionStr = new ScopedCString(pszRegion ?? string.Empty);
		fixed (byte* pszRegionBufferPtr = pszRegionStr)
		{
			_ISteamGameServer_SetRegion(instancePtr, pszRegion is null ? null : pszRegionBufferPtr);
		}
	}

	internal static void ISteamGameServer_SetAdvertiseServerActive(IntPtr instancePtr, bool bActive)
	{
		if ((nint)_ISteamGameServer_SetAdvertiseServerActive == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServer_SetAdvertiseServerActive' is not available.");
		_ISteamGameServer_SetAdvertiseServerActive(instancePtr, (byte)(bActive ? 1 : 0));
	}

	internal static uint ISteamGameServer_GetAuthSessionTicket(IntPtr instancePtr, byte[]? pTicket, int cbMaxTicket, out uint pcbTicket, ref SteamNetworkingIdentity pSnid)
	{
		if ((nint)_ISteamGameServer_GetAuthSessionTicket == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServer_GetAuthSessionTicket' is not available.");
		pcbTicket = default;
		uint ret;
		fixed (byte* pTicketPtr = pTicket)
		{
			fixed (uint* pcbTicketPtr = &pcbTicket)
			{
				fixed (SteamNetworkingIdentity* pSnidPtr = &pSnid)
				{
					ret = _ISteamGameServer_GetAuthSessionTicket(instancePtr, pTicketPtr, cbMaxTicket, pcbTicketPtr, pSnidPtr);
				}
			}
		}
		return ret;
	}

	internal static EBeginAuthSessionResult ISteamGameServer_BeginAuthSession(IntPtr instancePtr, byte[]? pAuthTicket, int cbAuthTicket, CSteamID steamID)
	{
		if ((nint)_ISteamGameServer_BeginAuthSession == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServer_BeginAuthSession' is not available.");
		EBeginAuthSessionResult ret;
		fixed (byte* pAuthTicketPtr = pAuthTicket)
		{
			ret = _ISteamGameServer_BeginAuthSession(instancePtr, pAuthTicketPtr, cbAuthTicket, steamID);
		}
		return ret;
	}

	internal static void ISteamGameServer_EndAuthSession(IntPtr instancePtr, CSteamID steamID)
	{
		if ((nint)_ISteamGameServer_EndAuthSession == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServer_EndAuthSession' is not available.");
		_ISteamGameServer_EndAuthSession(instancePtr, steamID);
	}

	internal static void ISteamGameServer_CancelAuthTicket(IntPtr instancePtr, HAuthTicket hAuthTicket)
	{
		if ((nint)_ISteamGameServer_CancelAuthTicket == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServer_CancelAuthTicket' is not available.");
		_ISteamGameServer_CancelAuthTicket(instancePtr, hAuthTicket);
	}

	internal static EUserHasLicenseForAppResult ISteamGameServer_UserHasLicenseForApp(IntPtr instancePtr, CSteamID steamID, AppId_t appID)
	{
		if ((nint)_ISteamGameServer_UserHasLicenseForApp == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServer_UserHasLicenseForApp' is not available.");
		return _ISteamGameServer_UserHasLicenseForApp(instancePtr, steamID, appID);
	}

	internal static bool ISteamGameServer_RequestUserGroupStatus(IntPtr instancePtr, CSteamID steamIDUser, CSteamID steamIDGroup)
	{
		if ((nint)_ISteamGameServer_RequestUserGroupStatus == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServer_RequestUserGroupStatus' is not available.");
		return _ISteamGameServer_RequestUserGroupStatus(instancePtr, steamIDUser, steamIDGroup) != 0;
	}

	internal static void ISteamGameServer_GetGameplayStats(IntPtr instancePtr)
	{
		if ((nint)_ISteamGameServer_GetGameplayStats == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServer_GetGameplayStats' is not available.");
		_ISteamGameServer_GetGameplayStats(instancePtr);
	}

	internal static ulong ISteamGameServer_GetServerReputation(IntPtr instancePtr)
	{
		if ((nint)_ISteamGameServer_GetServerReputation == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServer_GetServerReputation' is not available.");
		return _ISteamGameServer_GetServerReputation(instancePtr);
	}

	internal static SteamIPAddress_t ISteamGameServer_GetPublicIP(IntPtr instancePtr)
	{
		if ((nint)_ISteamGameServer_GetPublicIP == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServer_GetPublicIP' is not available.");
		return _ISteamGameServer_GetPublicIP(instancePtr);
	}

	internal static bool ISteamGameServer_HandleIncomingPacket(IntPtr instancePtr, byte[]? pData, int cbData, uint srcIP, ushort srcPort)
	{
		if ((nint)_ISteamGameServer_HandleIncomingPacket == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServer_HandleIncomingPacket' is not available.");
		byte ret;
		fixed (byte* pDataPtr = pData)
		{
			ret = _ISteamGameServer_HandleIncomingPacket(instancePtr, pDataPtr, cbData, srcIP, srcPort);
		}
		return ret != 0;
	}

	internal static int ISteamGameServer_GetNextOutgoingPacket(IntPtr instancePtr, byte[]? pOut, int cbMaxOut, out uint pNetAdr, out ushort pPort)
	{
		if ((nint)_ISteamGameServer_GetNextOutgoingPacket == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServer_GetNextOutgoingPacket' is not available.");
		pNetAdr = default;
		pPort = default;
		int ret;
		fixed (byte* pOutPtr = pOut)
		{
			fixed (uint* pNetAdrPtr = &pNetAdr)
			{
				fixed (ushort* pPortPtr = &pPort)
				{
					ret = _ISteamGameServer_GetNextOutgoingPacket(instancePtr, pOutPtr, cbMaxOut, pNetAdrPtr, pPortPtr);
				}
			}
		}
		return ret;
	}

	internal static ulong ISteamGameServer_AssociateWithClan(IntPtr instancePtr, CSteamID steamIDClan)
	{
		if ((nint)_ISteamGameServer_AssociateWithClan == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServer_AssociateWithClan' is not available.");
		return _ISteamGameServer_AssociateWithClan(instancePtr, steamIDClan);
	}

	internal static ulong ISteamGameServer_ComputeNewPlayerCompatibility(IntPtr instancePtr, CSteamID steamIDNewPlayer)
	{
		if ((nint)_ISteamGameServer_ComputeNewPlayerCompatibility == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServer_ComputeNewPlayerCompatibility' is not available.");
		return _ISteamGameServer_ComputeNewPlayerCompatibility(instancePtr, steamIDNewPlayer);
	}

	internal static bool ISteamGameServer_SendUserConnectAndAuthenticate_DEPRECATED(IntPtr instancePtr, uint unIPClient, byte[]? pvAuthBlob, uint cubAuthBlobSize, out CSteamID pSteamIDUser)
	{
		if ((nint)_ISteamGameServer_SendUserConnectAndAuthenticate_DEPRECATED == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServer_SendUserConnectAndAuthenticate_DEPRECATED' is not available.");
		pSteamIDUser = default;
		byte ret;
		fixed (byte* pvAuthBlobPtr = pvAuthBlob)
		{
			fixed (CSteamID* pSteamIDUserPtr = &pSteamIDUser)
			{
				ret = _ISteamGameServer_SendUserConnectAndAuthenticate_DEPRECATED(instancePtr, unIPClient, pvAuthBlobPtr, cubAuthBlobSize, pSteamIDUserPtr);
			}
		}
		return ret != 0;
	}

	internal static ulong ISteamGameServer_CreateUnauthenticatedUserConnection(IntPtr instancePtr)
	{
		if ((nint)_ISteamGameServer_CreateUnauthenticatedUserConnection == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServer_CreateUnauthenticatedUserConnection' is not available.");
		return _ISteamGameServer_CreateUnauthenticatedUserConnection(instancePtr);
	}

	internal static void ISteamGameServer_SendUserDisconnect_DEPRECATED(IntPtr instancePtr, CSteamID steamIDUser)
	{
		if ((nint)_ISteamGameServer_SendUserDisconnect_DEPRECATED == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServer_SendUserDisconnect_DEPRECATED' is not available.");
		_ISteamGameServer_SendUserDisconnect_DEPRECATED(instancePtr, steamIDUser);
	}

	internal static bool ISteamGameServer_BUpdateUserData(IntPtr instancePtr, CSteamID steamIDUser, string? pchPlayerName, uint uScore)
	{
		if ((nint)_ISteamGameServer_BUpdateUserData == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServer_BUpdateUserData' is not available.");
		using var pchPlayerNameStr = new ScopedCString(pchPlayerName ?? string.Empty);
		byte ret;
		fixed (byte* pchPlayerNameBufferPtr = pchPlayerNameStr)
		{
			ret = _ISteamGameServer_BUpdateUserData(instancePtr, steamIDUser, pchPlayerName is null ? null : pchPlayerNameBufferPtr, uScore);
		}
		return ret != 0;
	}

	internal static ulong ISteamGameServerStats_RequestUserStats(IntPtr instancePtr, CSteamID steamIDUser)
	{
		if ((nint)_ISteamGameServerStats_RequestUserStats == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServerStats_RequestUserStats' is not available.");
		return _ISteamGameServerStats_RequestUserStats(instancePtr, steamIDUser);
	}

	internal static bool ISteamGameServerStats_GetUserStatInt32(IntPtr instancePtr, CSteamID steamIDUser, string? pchName, out int pData)
	{
		if ((nint)_ISteamGameServerStats_GetUserStatInt32 == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServerStats_GetUserStatInt32' is not available.");
		using var pchNameStr = new ScopedCString(pchName ?? string.Empty);
		pData = default;
		byte ret;
		fixed (byte* pchNameBufferPtr = pchNameStr)
		{
			fixed (int* pDataPtr = &pData)
			{
				ret = _ISteamGameServerStats_GetUserStatInt32(instancePtr, steamIDUser, pchName is null ? null : pchNameBufferPtr, pDataPtr);
			}
		}
		return ret != 0;
	}

	internal static bool ISteamGameServerStats_GetUserStatFloat(IntPtr instancePtr, CSteamID steamIDUser, string? pchName, out float pData)
	{
		if ((nint)_ISteamGameServerStats_GetUserStatFloat == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServerStats_GetUserStatFloat' is not available.");
		using var pchNameStr = new ScopedCString(pchName ?? string.Empty);
		pData = default;
		byte ret;
		fixed (byte* pchNameBufferPtr = pchNameStr)
		{
			fixed (float* pDataPtr = &pData)
			{
				ret = _ISteamGameServerStats_GetUserStatFloat(instancePtr, steamIDUser, pchName is null ? null : pchNameBufferPtr, pDataPtr);
			}
		}
		return ret != 0;
	}

	internal static bool ISteamGameServerStats_GetUserAchievement(IntPtr instancePtr, CSteamID steamIDUser, string? pchName, out bool pbAchieved)
	{
		if ((nint)_ISteamGameServerStats_GetUserAchievement == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServerStats_GetUserAchievement' is not available.");
		using var pchNameStr = new ScopedCString(pchName ?? string.Empty);
		byte pbAchievedVal = 0;
		byte ret;
		fixed (byte* pchNameBufferPtr = pchNameStr)
		{
			ret = _ISteamGameServerStats_GetUserAchievement(instancePtr, steamIDUser, pchName is null ? null : pchNameBufferPtr, &pbAchievedVal);
			pbAchieved = pbAchievedVal != 0;
		}
		return ret != 0;
	}

	internal static bool ISteamGameServerStats_SetUserStatInt32(IntPtr instancePtr, CSteamID steamIDUser, string? pchName, int nData)
	{
		if ((nint)_ISteamGameServerStats_SetUserStatInt32 == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServerStats_SetUserStatInt32' is not available.");
		using var pchNameStr = new ScopedCString(pchName ?? string.Empty);
		byte ret;
		fixed (byte* pchNameBufferPtr = pchNameStr)
		{
			ret = _ISteamGameServerStats_SetUserStatInt32(instancePtr, steamIDUser, pchName is null ? null : pchNameBufferPtr, nData);
		}
		return ret != 0;
	}

	internal static bool ISteamGameServerStats_SetUserStatFloat(IntPtr instancePtr, CSteamID steamIDUser, string? pchName, float fData)
	{
		if ((nint)_ISteamGameServerStats_SetUserStatFloat == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServerStats_SetUserStatFloat' is not available.");
		using var pchNameStr = new ScopedCString(pchName ?? string.Empty);
		byte ret;
		fixed (byte* pchNameBufferPtr = pchNameStr)
		{
			ret = _ISteamGameServerStats_SetUserStatFloat(instancePtr, steamIDUser, pchName is null ? null : pchNameBufferPtr, fData);
		}
		return ret != 0;
	}

	internal static bool ISteamGameServerStats_UpdateUserAvgRateStat(IntPtr instancePtr, CSteamID steamIDUser, string? pchName, float flCountThisSession, double dSessionLength)
	{
		if ((nint)_ISteamGameServerStats_UpdateUserAvgRateStat == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServerStats_UpdateUserAvgRateStat' is not available.");
		using var pchNameStr = new ScopedCString(pchName ?? string.Empty);
		byte ret;
		fixed (byte* pchNameBufferPtr = pchNameStr)
		{
			ret = _ISteamGameServerStats_UpdateUserAvgRateStat(instancePtr, steamIDUser, pchName is null ? null : pchNameBufferPtr, flCountThisSession, dSessionLength);
		}
		return ret != 0;
	}

	internal static bool ISteamGameServerStats_SetUserAchievement(IntPtr instancePtr, CSteamID steamIDUser, string? pchName)
	{
		if ((nint)_ISteamGameServerStats_SetUserAchievement == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServerStats_SetUserAchievement' is not available.");
		using var pchNameStr = new ScopedCString(pchName ?? string.Empty);
		byte ret;
		fixed (byte* pchNameBufferPtr = pchNameStr)
		{
			ret = _ISteamGameServerStats_SetUserAchievement(instancePtr, steamIDUser, pchName is null ? null : pchNameBufferPtr);
		}
		return ret != 0;
	}

	internal static bool ISteamGameServerStats_ClearUserAchievement(IntPtr instancePtr, CSteamID steamIDUser, string? pchName)
	{
		if ((nint)_ISteamGameServerStats_ClearUserAchievement == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServerStats_ClearUserAchievement' is not available.");
		using var pchNameStr = new ScopedCString(pchName ?? string.Empty);
		byte ret;
		fixed (byte* pchNameBufferPtr = pchNameStr)
		{
			ret = _ISteamGameServerStats_ClearUserAchievement(instancePtr, steamIDUser, pchName is null ? null : pchNameBufferPtr);
		}
		return ret != 0;
	}

	internal static ulong ISteamGameServerStats_StoreUserStats(IntPtr instancePtr, CSteamID steamIDUser)
	{
		if ((nint)_ISteamGameServerStats_StoreUserStats == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamGameServerStats_StoreUserStats' is not available.");
		return _ISteamGameServerStats_StoreUserStats(instancePtr, steamIDUser);
	}

	internal static uint ISteamHTTP_CreateHTTPRequest(IntPtr instancePtr, EHTTPMethod eHTTPRequestMethod, string? pchAbsoluteURL)
	{
		if ((nint)_ISteamHTTP_CreateHTTPRequest == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamHTTP_CreateHTTPRequest' is not available.");
		using var pchAbsoluteURLStr = new ScopedCString(pchAbsoluteURL ?? string.Empty);
		uint ret;
		fixed (byte* pchAbsoluteURLBufferPtr = pchAbsoluteURLStr)
		{
			ret = _ISteamHTTP_CreateHTTPRequest(instancePtr, eHTTPRequestMethod, pchAbsoluteURL is null ? null : pchAbsoluteURLBufferPtr);
		}
		return ret;
	}

	internal static bool ISteamHTTP_SetHTTPRequestContextValue(IntPtr instancePtr, HTTPRequestHandle hRequest, ulong ulContextValue)
	{
		if ((nint)_ISteamHTTP_SetHTTPRequestContextValue == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamHTTP_SetHTTPRequestContextValue' is not available.");
		return _ISteamHTTP_SetHTTPRequestContextValue(instancePtr, hRequest, ulContextValue) != 0;
	}

	internal static bool ISteamHTTP_SetHTTPRequestNetworkActivityTimeout(IntPtr instancePtr, HTTPRequestHandle hRequest, uint unTimeoutSeconds)
	{
		if ((nint)_ISteamHTTP_SetHTTPRequestNetworkActivityTimeout == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamHTTP_SetHTTPRequestNetworkActivityTimeout' is not available.");
		return _ISteamHTTP_SetHTTPRequestNetworkActivityTimeout(instancePtr, hRequest, unTimeoutSeconds) != 0;
	}

	internal static bool ISteamHTTP_SetHTTPRequestHeaderValue(IntPtr instancePtr, HTTPRequestHandle hRequest, string? pchHeaderName, string? pchHeaderValue)
	{
		if ((nint)_ISteamHTTP_SetHTTPRequestHeaderValue == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamHTTP_SetHTTPRequestHeaderValue' is not available.");
		using var pchHeaderNameStr = new ScopedCString(pchHeaderName ?? string.Empty);
		using var pchHeaderValueStr = new ScopedCString(pchHeaderValue ?? string.Empty);
		byte ret;
		fixed (byte* pchHeaderNameBufferPtr = pchHeaderNameStr)
		{
			fixed (byte* pchHeaderValueBufferPtr = pchHeaderValueStr)
			{
				ret = _ISteamHTTP_SetHTTPRequestHeaderValue(instancePtr, hRequest, pchHeaderName is null ? null : pchHeaderNameBufferPtr, pchHeaderValue is null ? null : pchHeaderValueBufferPtr);
			}
		}
		return ret != 0;
	}

	internal static bool ISteamHTTP_SetHTTPRequestGetOrPostParameter(IntPtr instancePtr, HTTPRequestHandle hRequest, string? pchParamName, string? pchParamValue)
	{
		if ((nint)_ISteamHTTP_SetHTTPRequestGetOrPostParameter == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamHTTP_SetHTTPRequestGetOrPostParameter' is not available.");
		using var pchParamNameStr = new ScopedCString(pchParamName ?? string.Empty);
		using var pchParamValueStr = new ScopedCString(pchParamValue ?? string.Empty);
		byte ret;
		fixed (byte* pchParamNameBufferPtr = pchParamNameStr)
		{
			fixed (byte* pchParamValueBufferPtr = pchParamValueStr)
			{
				ret = _ISteamHTTP_SetHTTPRequestGetOrPostParameter(instancePtr, hRequest, pchParamName is null ? null : pchParamNameBufferPtr, pchParamValue is null ? null : pchParamValueBufferPtr);
			}
		}
		return ret != 0;
	}

	internal static bool ISteamHTTP_SendHTTPRequest(IntPtr instancePtr, HTTPRequestHandle hRequest, out SteamAPICall_t pCallHandle)
	{
		if ((nint)_ISteamHTTP_SendHTTPRequest == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamHTTP_SendHTTPRequest' is not available.");
		pCallHandle = default;
		byte ret;
		fixed (SteamAPICall_t* pCallHandlePtr = &pCallHandle)
		{
			ret = _ISteamHTTP_SendHTTPRequest(instancePtr, hRequest, pCallHandlePtr);
		}
		return ret != 0;
	}

	internal static bool ISteamHTTP_SendHTTPRequestAndStreamResponse(IntPtr instancePtr, HTTPRequestHandle hRequest, out SteamAPICall_t pCallHandle)
	{
		if ((nint)_ISteamHTTP_SendHTTPRequestAndStreamResponse == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamHTTP_SendHTTPRequestAndStreamResponse' is not available.");
		pCallHandle = default;
		byte ret;
		fixed (SteamAPICall_t* pCallHandlePtr = &pCallHandle)
		{
			ret = _ISteamHTTP_SendHTTPRequestAndStreamResponse(instancePtr, hRequest, pCallHandlePtr);
		}
		return ret != 0;
	}

	internal static bool ISteamHTTP_DeferHTTPRequest(IntPtr instancePtr, HTTPRequestHandle hRequest)
	{
		if ((nint)_ISteamHTTP_DeferHTTPRequest == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamHTTP_DeferHTTPRequest' is not available.");
		return _ISteamHTTP_DeferHTTPRequest(instancePtr, hRequest) != 0;
	}

	internal static bool ISteamHTTP_PrioritizeHTTPRequest(IntPtr instancePtr, HTTPRequestHandle hRequest)
	{
		if ((nint)_ISteamHTTP_PrioritizeHTTPRequest == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamHTTP_PrioritizeHTTPRequest' is not available.");
		return _ISteamHTTP_PrioritizeHTTPRequest(instancePtr, hRequest) != 0;
	}

	internal static bool ISteamHTTP_GetHTTPResponseHeaderSize(IntPtr instancePtr, HTTPRequestHandle hRequest, string? pchHeaderName, out uint unResponseHeaderSize)
	{
		if ((nint)_ISteamHTTP_GetHTTPResponseHeaderSize == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamHTTP_GetHTTPResponseHeaderSize' is not available.");
		using var pchHeaderNameStr = new ScopedCString(pchHeaderName ?? string.Empty);
		unResponseHeaderSize = default;
		byte ret;
		fixed (byte* pchHeaderNameBufferPtr = pchHeaderNameStr)
		{
			fixed (uint* unResponseHeaderSizePtr = &unResponseHeaderSize)
			{
				ret = _ISteamHTTP_GetHTTPResponseHeaderSize(instancePtr, hRequest, pchHeaderName is null ? null : pchHeaderNameBufferPtr, unResponseHeaderSizePtr);
			}
		}
		return ret != 0;
	}

	internal static bool ISteamHTTP_GetHTTPResponseHeaderValue(IntPtr instancePtr, HTTPRequestHandle hRequest, string? pchHeaderName, byte[]? pHeaderValueBuffer, uint unBufferSize)
	{
		if ((nint)_ISteamHTTP_GetHTTPResponseHeaderValue == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamHTTP_GetHTTPResponseHeaderValue' is not available.");
		using var pchHeaderNameStr = new ScopedCString(pchHeaderName ?? string.Empty);
		byte ret;
		fixed (byte* pchHeaderNameBufferPtr = pchHeaderNameStr)
		{
			fixed (byte* pHeaderValueBufferPtr = pHeaderValueBuffer)
			{
				ret = _ISteamHTTP_GetHTTPResponseHeaderValue(instancePtr, hRequest, pchHeaderName is null ? null : pchHeaderNameBufferPtr, pHeaderValueBufferPtr, unBufferSize);
			}
		}
		return ret != 0;
	}

	internal static bool ISteamHTTP_GetHTTPResponseBodySize(IntPtr instancePtr, HTTPRequestHandle hRequest, out uint unBodySize)
	{
		if ((nint)_ISteamHTTP_GetHTTPResponseBodySize == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamHTTP_GetHTTPResponseBodySize' is not available.");
		unBodySize = default;
		byte ret;
		fixed (uint* unBodySizePtr = &unBodySize)
		{
			ret = _ISteamHTTP_GetHTTPResponseBodySize(instancePtr, hRequest, unBodySizePtr);
		}
		return ret != 0;
	}

	internal static bool ISteamHTTP_GetHTTPResponseBodyData(IntPtr instancePtr, HTTPRequestHandle hRequest, byte[]? pBodyDataBuffer, uint unBufferSize)
	{
		if ((nint)_ISteamHTTP_GetHTTPResponseBodyData == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamHTTP_GetHTTPResponseBodyData' is not available.");
		byte ret;
		fixed (byte* pBodyDataBufferPtr = pBodyDataBuffer)
		{
			ret = _ISteamHTTP_GetHTTPResponseBodyData(instancePtr, hRequest, pBodyDataBufferPtr, unBufferSize);
		}
		return ret != 0;
	}

	internal static bool ISteamHTTP_GetHTTPStreamingResponseBodyData(IntPtr instancePtr, HTTPRequestHandle hRequest, uint cOffset, byte[]? pBodyDataBuffer, uint unBufferSize)
	{
		if ((nint)_ISteamHTTP_GetHTTPStreamingResponseBodyData == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamHTTP_GetHTTPStreamingResponseBodyData' is not available.");
		byte ret;
		fixed (byte* pBodyDataBufferPtr = pBodyDataBuffer)
		{
			ret = _ISteamHTTP_GetHTTPStreamingResponseBodyData(instancePtr, hRequest, cOffset, pBodyDataBufferPtr, unBufferSize);
		}
		return ret != 0;
	}

	internal static bool ISteamHTTP_ReleaseHTTPRequest(IntPtr instancePtr, HTTPRequestHandle hRequest)
	{
		if ((nint)_ISteamHTTP_ReleaseHTTPRequest == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamHTTP_ReleaseHTTPRequest' is not available.");
		return _ISteamHTTP_ReleaseHTTPRequest(instancePtr, hRequest) != 0;
	}

	internal static bool ISteamHTTP_GetHTTPDownloadProgressPct(IntPtr instancePtr, HTTPRequestHandle hRequest, out float pflPercentOut)
	{
		if ((nint)_ISteamHTTP_GetHTTPDownloadProgressPct == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamHTTP_GetHTTPDownloadProgressPct' is not available.");
		pflPercentOut = default;
		byte ret;
		fixed (float* pflPercentOutPtr = &pflPercentOut)
		{
			ret = _ISteamHTTP_GetHTTPDownloadProgressPct(instancePtr, hRequest, pflPercentOutPtr);
		}
		return ret != 0;
	}

	internal static bool ISteamHTTP_SetHTTPRequestRawPostBody(IntPtr instancePtr, HTTPRequestHandle hRequest, string? pchContentType, byte[]? pubBody, uint unBodyLen)
	{
		if ((nint)_ISteamHTTP_SetHTTPRequestRawPostBody == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamHTTP_SetHTTPRequestRawPostBody' is not available.");
		using var pchContentTypeStr = new ScopedCString(pchContentType ?? string.Empty);
		byte ret;
		fixed (byte* pchContentTypeBufferPtr = pchContentTypeStr)
		{
			fixed (byte* pubBodyPtr = pubBody)
			{
				ret = _ISteamHTTP_SetHTTPRequestRawPostBody(instancePtr, hRequest, pchContentType is null ? null : pchContentTypeBufferPtr, pubBodyPtr, unBodyLen);
			}
		}
		return ret != 0;
	}

	internal static uint ISteamHTTP_CreateCookieContainer(IntPtr instancePtr, bool bAllowResponsesToModify)
	{
		if ((nint)_ISteamHTTP_CreateCookieContainer == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamHTTP_CreateCookieContainer' is not available.");
		return _ISteamHTTP_CreateCookieContainer(instancePtr, (byte)(bAllowResponsesToModify ? 1 : 0));
	}

	internal static bool ISteamHTTP_ReleaseCookieContainer(IntPtr instancePtr, HTTPCookieContainerHandle hCookieContainer)
	{
		if ((nint)_ISteamHTTP_ReleaseCookieContainer == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamHTTP_ReleaseCookieContainer' is not available.");
		return _ISteamHTTP_ReleaseCookieContainer(instancePtr, hCookieContainer) != 0;
	}

	internal static bool ISteamHTTP_SetCookie(IntPtr instancePtr, HTTPCookieContainerHandle hCookieContainer, string? pchHost, string? pchUrl, string? pchCookie)
	{
		if ((nint)_ISteamHTTP_SetCookie == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamHTTP_SetCookie' is not available.");
		using var pchHostStr = new ScopedCString(pchHost ?? string.Empty);
		using var pchUrlStr = new ScopedCString(pchUrl ?? string.Empty);
		using var pchCookieStr = new ScopedCString(pchCookie ?? string.Empty);
		byte ret;
		fixed (byte* pchHostBufferPtr = pchHostStr)
		{
			fixed (byte* pchUrlBufferPtr = pchUrlStr)
			{
				fixed (byte* pchCookieBufferPtr = pchCookieStr)
				{
					ret = _ISteamHTTP_SetCookie(instancePtr, hCookieContainer, pchHost is null ? null : pchHostBufferPtr, pchUrl is null ? null : pchUrlBufferPtr, pchCookie is null ? null : pchCookieBufferPtr);
				}
			}
		}
		return ret != 0;
	}

	internal static bool ISteamHTTP_SetHTTPRequestCookieContainer(IntPtr instancePtr, HTTPRequestHandle hRequest, HTTPCookieContainerHandle hCookieContainer)
	{
		if ((nint)_ISteamHTTP_SetHTTPRequestCookieContainer == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamHTTP_SetHTTPRequestCookieContainer' is not available.");
		return _ISteamHTTP_SetHTTPRequestCookieContainer(instancePtr, hRequest, hCookieContainer) != 0;
	}

	internal static bool ISteamHTTP_SetHTTPRequestUserAgentInfo(IntPtr instancePtr, HTTPRequestHandle hRequest, string? pchUserAgentInfo)
	{
		if ((nint)_ISteamHTTP_SetHTTPRequestUserAgentInfo == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamHTTP_SetHTTPRequestUserAgentInfo' is not available.");
		using var pchUserAgentInfoStr = new ScopedCString(pchUserAgentInfo ?? string.Empty);
		byte ret;
		fixed (byte* pchUserAgentInfoBufferPtr = pchUserAgentInfoStr)
		{
			ret = _ISteamHTTP_SetHTTPRequestUserAgentInfo(instancePtr, hRequest, pchUserAgentInfo is null ? null : pchUserAgentInfoBufferPtr);
		}
		return ret != 0;
	}

	internal static bool ISteamHTTP_SetHTTPRequestRequiresVerifiedCertificate(IntPtr instancePtr, HTTPRequestHandle hRequest, bool bRequireVerifiedCertificate)
	{
		if ((nint)_ISteamHTTP_SetHTTPRequestRequiresVerifiedCertificate == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamHTTP_SetHTTPRequestRequiresVerifiedCertificate' is not available.");
		return _ISteamHTTP_SetHTTPRequestRequiresVerifiedCertificate(instancePtr, hRequest, (byte)(bRequireVerifiedCertificate ? 1 : 0)) != 0;
	}

	internal static bool ISteamHTTP_SetHTTPRequestAbsoluteTimeoutMS(IntPtr instancePtr, HTTPRequestHandle hRequest, uint unMilliseconds)
	{
		if ((nint)_ISteamHTTP_SetHTTPRequestAbsoluteTimeoutMS == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamHTTP_SetHTTPRequestAbsoluteTimeoutMS' is not available.");
		return _ISteamHTTP_SetHTTPRequestAbsoluteTimeoutMS(instancePtr, hRequest, unMilliseconds) != 0;
	}

	internal static bool ISteamHTTP_GetHTTPRequestWasTimedOut(IntPtr instancePtr, HTTPRequestHandle hRequest, out bool pbWasTimedOut)
	{
		if ((nint)_ISteamHTTP_GetHTTPRequestWasTimedOut == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamHTTP_GetHTTPRequestWasTimedOut' is not available.");
		byte pbWasTimedOutVal = 0;
		byte ret;
		ret = _ISteamHTTP_GetHTTPRequestWasTimedOut(instancePtr, hRequest, &pbWasTimedOutVal);
		pbWasTimedOut = pbWasTimedOutVal != 0;
		return ret != 0;
	}

	internal static EResult ISteamInventory_GetResultStatus(IntPtr instancePtr, SteamInventoryResult_t resultHandle)
	{
		if ((nint)_ISteamInventory_GetResultStatus == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamInventory_GetResultStatus' is not available.");
		return _ISteamInventory_GetResultStatus(instancePtr, resultHandle);
	}

	internal static bool ISteamInventory_GetResultItems(IntPtr instancePtr, SteamInventoryResult_t resultHandle, SteamItemDetails_t[]? pOutItemsArray, ref uint punOutItemsArraySize)
	{
		if ((nint)_ISteamInventory_GetResultItems == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamInventory_GetResultItems' is not available.");
		byte ret;
		fixed (SteamItemDetails_t* pOutItemsArrayPtr = pOutItemsArray)
		{
			fixed (uint* punOutItemsArraySizePtr = &punOutItemsArraySize)
			{
				ret = _ISteamInventory_GetResultItems(instancePtr, resultHandle, pOutItemsArrayPtr, punOutItemsArraySizePtr);
			}
		}
		return ret != 0;
	}

	internal static bool ISteamInventory_GetResultItemProperty(IntPtr instancePtr, SteamInventoryResult_t resultHandle, uint unItemIndex, string? pchPropertyName, IntPtr pchValueBuffer, ref uint punValueBufferSizeOut)
	{
		if ((nint)_ISteamInventory_GetResultItemProperty == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamInventory_GetResultItemProperty' is not available.");
		using var pchPropertyNameStr = new ScopedCString(pchPropertyName ?? string.Empty);
		byte ret;
		fixed (byte* pchPropertyNameBufferPtr = pchPropertyNameStr)
		{
			fixed (uint* punValueBufferSizeOutPtr = &punValueBufferSizeOut)
			{
				ret = _ISteamInventory_GetResultItemProperty(instancePtr, resultHandle, unItemIndex, pchPropertyName is null ? null : pchPropertyNameBufferPtr, pchValueBuffer, punValueBufferSizeOutPtr);
			}
		}
		return ret != 0;
	}

	internal static uint ISteamInventory_GetResultTimestamp(IntPtr instancePtr, SteamInventoryResult_t resultHandle)
	{
		if ((nint)_ISteamInventory_GetResultTimestamp == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamInventory_GetResultTimestamp' is not available.");
		return _ISteamInventory_GetResultTimestamp(instancePtr, resultHandle);
	}

	internal static bool ISteamInventory_CheckResultSteamID(IntPtr instancePtr, SteamInventoryResult_t resultHandle, CSteamID steamIDExpected)
	{
		if ((nint)_ISteamInventory_CheckResultSteamID == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamInventory_CheckResultSteamID' is not available.");
		return _ISteamInventory_CheckResultSteamID(instancePtr, resultHandle, steamIDExpected) != 0;
	}

	internal static void ISteamInventory_DestroyResult(IntPtr instancePtr, SteamInventoryResult_t resultHandle)
	{
		if ((nint)_ISteamInventory_DestroyResult == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamInventory_DestroyResult' is not available.");
		_ISteamInventory_DestroyResult(instancePtr, resultHandle);
	}

	internal static bool ISteamInventory_GetAllItems(IntPtr instancePtr, out SteamInventoryResult_t pResultHandle)
	{
		if ((nint)_ISteamInventory_GetAllItems == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamInventory_GetAllItems' is not available.");
		pResultHandle = default;
		byte ret;
		fixed (SteamInventoryResult_t* pResultHandlePtr = &pResultHandle)
		{
			ret = _ISteamInventory_GetAllItems(instancePtr, pResultHandlePtr);
		}
		return ret != 0;
	}

	internal static bool ISteamInventory_GetItemsByID(IntPtr instancePtr, out SteamInventoryResult_t pResultHandle, SteamItemInstanceID_t[]? pInstanceIDs, uint unCountInstanceIDs)
	{
		if ((nint)_ISteamInventory_GetItemsByID == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamInventory_GetItemsByID' is not available.");
		pResultHandle = default;
		byte ret;
		fixed (SteamInventoryResult_t* pResultHandlePtr = &pResultHandle)
		{
			fixed (SteamItemInstanceID_t* pInstanceIDsPtr = pInstanceIDs)
			{
				ret = _ISteamInventory_GetItemsByID(instancePtr, pResultHandlePtr, pInstanceIDsPtr, unCountInstanceIDs);
			}
		}
		return ret != 0;
	}

	internal static bool ISteamInventory_SerializeResult(IntPtr instancePtr, SteamInventoryResult_t resultHandle, byte[]? pOutBuffer, out uint punOutBufferSize)
	{
		if ((nint)_ISteamInventory_SerializeResult == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamInventory_SerializeResult' is not available.");
		punOutBufferSize = default;
		byte ret;
		fixed (byte* pOutBufferPtr = pOutBuffer)
		{
			fixed (uint* punOutBufferSizePtr = &punOutBufferSize)
			{
				ret = _ISteamInventory_SerializeResult(instancePtr, resultHandle, pOutBufferPtr, punOutBufferSizePtr);
			}
		}
		return ret != 0;
	}

	internal static bool ISteamInventory_DeserializeResult(IntPtr instancePtr, out SteamInventoryResult_t pOutResultHandle, byte[]? pBuffer, uint unBufferSize, bool bRESERVED_MUST_BE_FALSE)
	{
		if ((nint)_ISteamInventory_DeserializeResult == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamInventory_DeserializeResult' is not available.");
		pOutResultHandle = default;
		byte ret;
		fixed (SteamInventoryResult_t* pOutResultHandlePtr = &pOutResultHandle)
		{
			fixed (byte* pBufferPtr = pBuffer)
			{
				ret = _ISteamInventory_DeserializeResult(instancePtr, pOutResultHandlePtr, pBufferPtr, unBufferSize, (byte)(bRESERVED_MUST_BE_FALSE ? 1 : 0));
			}
		}
		return ret != 0;
	}

	internal static bool ISteamInventory_GenerateItems(IntPtr instancePtr, out SteamInventoryResult_t pResultHandle, SteamItemDef_t[]? pArrayItemDefs, uint[]? punArrayQuantity, uint unArrayLength)
	{
		if ((nint)_ISteamInventory_GenerateItems == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamInventory_GenerateItems' is not available.");
		pResultHandle = default;
		byte ret;
		fixed (SteamInventoryResult_t* pResultHandlePtr = &pResultHandle)
		{
			fixed (SteamItemDef_t* pArrayItemDefsPtr = pArrayItemDefs)
			{
				fixed (uint* punArrayQuantityPtr = punArrayQuantity)
				{
					ret = _ISteamInventory_GenerateItems(instancePtr, pResultHandlePtr, pArrayItemDefsPtr, punArrayQuantityPtr, unArrayLength);
				}
			}
		}
		return ret != 0;
	}

	internal static bool ISteamInventory_GrantPromoItems(IntPtr instancePtr, out SteamInventoryResult_t pResultHandle)
	{
		if ((nint)_ISteamInventory_GrantPromoItems == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamInventory_GrantPromoItems' is not available.");
		pResultHandle = default;
		byte ret;
		fixed (SteamInventoryResult_t* pResultHandlePtr = &pResultHandle)
		{
			ret = _ISteamInventory_GrantPromoItems(instancePtr, pResultHandlePtr);
		}
		return ret != 0;
	}

	internal static bool ISteamInventory_AddPromoItem(IntPtr instancePtr, out SteamInventoryResult_t pResultHandle, SteamItemDef_t itemDef)
	{
		if ((nint)_ISteamInventory_AddPromoItem == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamInventory_AddPromoItem' is not available.");
		pResultHandle = default;
		byte ret;
		fixed (SteamInventoryResult_t* pResultHandlePtr = &pResultHandle)
		{
			ret = _ISteamInventory_AddPromoItem(instancePtr, pResultHandlePtr, itemDef);
		}
		return ret != 0;
	}

	internal static bool ISteamInventory_AddPromoItems(IntPtr instancePtr, out SteamInventoryResult_t pResultHandle, SteamItemDef_t[]? pArrayItemDefs, uint unArrayLength)
	{
		if ((nint)_ISteamInventory_AddPromoItems == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamInventory_AddPromoItems' is not available.");
		pResultHandle = default;
		byte ret;
		fixed (SteamInventoryResult_t* pResultHandlePtr = &pResultHandle)
		{
			fixed (SteamItemDef_t* pArrayItemDefsPtr = pArrayItemDefs)
			{
				ret = _ISteamInventory_AddPromoItems(instancePtr, pResultHandlePtr, pArrayItemDefsPtr, unArrayLength);
			}
		}
		return ret != 0;
	}

	internal static bool ISteamInventory_ConsumeItem(IntPtr instancePtr, out SteamInventoryResult_t pResultHandle, SteamItemInstanceID_t itemConsume, uint unQuantity)
	{
		if ((nint)_ISteamInventory_ConsumeItem == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamInventory_ConsumeItem' is not available.");
		pResultHandle = default;
		byte ret;
		fixed (SteamInventoryResult_t* pResultHandlePtr = &pResultHandle)
		{
			ret = _ISteamInventory_ConsumeItem(instancePtr, pResultHandlePtr, itemConsume, unQuantity);
		}
		return ret != 0;
	}

	internal static bool ISteamInventory_ExchangeItems(IntPtr instancePtr, out SteamInventoryResult_t pResultHandle, SteamItemDef_t[]? pArrayGenerate, uint[]? punArrayGenerateQuantity, uint unArrayGenerateLength, SteamItemInstanceID_t[]? pArrayDestroy, uint[]? punArrayDestroyQuantity, uint unArrayDestroyLength)
	{
		if ((nint)_ISteamInventory_ExchangeItems == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamInventory_ExchangeItems' is not available.");
		pResultHandle = default;
		byte ret;
		fixed (SteamInventoryResult_t* pResultHandlePtr = &pResultHandle)
		{
			fixed (SteamItemDef_t* pArrayGeneratePtr = pArrayGenerate)
			{
				fixed (uint* punArrayGenerateQuantityPtr = punArrayGenerateQuantity)
				{
					fixed (SteamItemInstanceID_t* pArrayDestroyPtr = pArrayDestroy)
					{
						fixed (uint* punArrayDestroyQuantityPtr = punArrayDestroyQuantity)
						{
							ret = _ISteamInventory_ExchangeItems(instancePtr, pResultHandlePtr, pArrayGeneratePtr, punArrayGenerateQuantityPtr, unArrayGenerateLength, pArrayDestroyPtr, punArrayDestroyQuantityPtr, unArrayDestroyLength);
						}
					}
				}
			}
		}
		return ret != 0;
	}

	internal static bool ISteamInventory_TransferItemQuantity(IntPtr instancePtr, out SteamInventoryResult_t pResultHandle, SteamItemInstanceID_t itemIdSource, uint unQuantity, SteamItemInstanceID_t itemIdDest)
	{
		if ((nint)_ISteamInventory_TransferItemQuantity == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamInventory_TransferItemQuantity' is not available.");
		pResultHandle = default;
		byte ret;
		fixed (SteamInventoryResult_t* pResultHandlePtr = &pResultHandle)
		{
			ret = _ISteamInventory_TransferItemQuantity(instancePtr, pResultHandlePtr, itemIdSource, unQuantity, itemIdDest);
		}
		return ret != 0;
	}

	internal static void ISteamInventory_SendItemDropHeartbeat(IntPtr instancePtr)
	{
		if ((nint)_ISteamInventory_SendItemDropHeartbeat == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamInventory_SendItemDropHeartbeat' is not available.");
		_ISteamInventory_SendItemDropHeartbeat(instancePtr);
	}

	internal static bool ISteamInventory_TriggerItemDrop(IntPtr instancePtr, out SteamInventoryResult_t pResultHandle, SteamItemDef_t dropListDefinition)
	{
		if ((nint)_ISteamInventory_TriggerItemDrop == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamInventory_TriggerItemDrop' is not available.");
		pResultHandle = default;
		byte ret;
		fixed (SteamInventoryResult_t* pResultHandlePtr = &pResultHandle)
		{
			ret = _ISteamInventory_TriggerItemDrop(instancePtr, pResultHandlePtr, dropListDefinition);
		}
		return ret != 0;
	}

	internal static bool ISteamInventory_TradeItems(IntPtr instancePtr, out SteamInventoryResult_t pResultHandle, CSteamID steamIDTradePartner, SteamItemInstanceID_t[]? pArrayGive, uint[]? pArrayGiveQuantity, uint nArrayGiveLength, SteamItemInstanceID_t[]? pArrayGet, uint[]? pArrayGetQuantity, uint nArrayGetLength)
	{
		if ((nint)_ISteamInventory_TradeItems == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamInventory_TradeItems' is not available.");
		pResultHandle = default;
		byte ret;
		fixed (SteamInventoryResult_t* pResultHandlePtr = &pResultHandle)
		{
			fixed (SteamItemInstanceID_t* pArrayGivePtr = pArrayGive)
			{
				fixed (uint* pArrayGiveQuantityPtr = pArrayGiveQuantity)
				{
					fixed (SteamItemInstanceID_t* pArrayGetPtr = pArrayGet)
					{
						fixed (uint* pArrayGetQuantityPtr = pArrayGetQuantity)
						{
							ret = _ISteamInventory_TradeItems(instancePtr, pResultHandlePtr, steamIDTradePartner, pArrayGivePtr, pArrayGiveQuantityPtr, nArrayGiveLength, pArrayGetPtr, pArrayGetQuantityPtr, nArrayGetLength);
						}
					}
				}
			}
		}
		return ret != 0;
	}

	internal static bool ISteamInventory_LoadItemDefinitions(IntPtr instancePtr)
	{
		if ((nint)_ISteamInventory_LoadItemDefinitions == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamInventory_LoadItemDefinitions' is not available.");
		return _ISteamInventory_LoadItemDefinitions(instancePtr) != 0;
	}

	internal static bool ISteamInventory_GetItemDefinitionIDs(IntPtr instancePtr, SteamItemDef_t[]? pItemDefIDs, ref uint punItemDefIDsArraySize)
	{
		if ((nint)_ISteamInventory_GetItemDefinitionIDs == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamInventory_GetItemDefinitionIDs' is not available.");
		byte ret;
		fixed (SteamItemDef_t* pItemDefIDsPtr = pItemDefIDs)
		{
			fixed (uint* punItemDefIDsArraySizePtr = &punItemDefIDsArraySize)
			{
				ret = _ISteamInventory_GetItemDefinitionIDs(instancePtr, pItemDefIDsPtr, punItemDefIDsArraySizePtr);
			}
		}
		return ret != 0;
	}

	internal static bool ISteamInventory_GetItemDefinitionProperty(IntPtr instancePtr, SteamItemDef_t iDefinition, string? pchPropertyName, IntPtr pchValueBuffer, ref uint punValueBufferSizeOut)
	{
		if ((nint)_ISteamInventory_GetItemDefinitionProperty == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamInventory_GetItemDefinitionProperty' is not available.");
		using var pchPropertyNameStr = new ScopedCString(pchPropertyName ?? string.Empty);
		byte ret;
		fixed (byte* pchPropertyNameBufferPtr = pchPropertyNameStr)
		{
			fixed (uint* punValueBufferSizeOutPtr = &punValueBufferSizeOut)
			{
				ret = _ISteamInventory_GetItemDefinitionProperty(instancePtr, iDefinition, pchPropertyName is null ? null : pchPropertyNameBufferPtr, pchValueBuffer, punValueBufferSizeOutPtr);
			}
		}
		return ret != 0;
	}

	internal static ulong ISteamInventory_RequestEligiblePromoItemDefinitionsIDs(IntPtr instancePtr, CSteamID steamID)
	{
		if ((nint)_ISteamInventory_RequestEligiblePromoItemDefinitionsIDs == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamInventory_RequestEligiblePromoItemDefinitionsIDs' is not available.");
		return _ISteamInventory_RequestEligiblePromoItemDefinitionsIDs(instancePtr, steamID);
	}

	internal static bool ISteamInventory_GetEligiblePromoItemDefinitionIDs(IntPtr instancePtr, CSteamID steamID, SteamItemDef_t[]? pItemDefIDs, ref uint punItemDefIDsArraySize)
	{
		if ((nint)_ISteamInventory_GetEligiblePromoItemDefinitionIDs == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamInventory_GetEligiblePromoItemDefinitionIDs' is not available.");
		byte ret;
		fixed (SteamItemDef_t* pItemDefIDsPtr = pItemDefIDs)
		{
			fixed (uint* punItemDefIDsArraySizePtr = &punItemDefIDsArraySize)
			{
				ret = _ISteamInventory_GetEligiblePromoItemDefinitionIDs(instancePtr, steamID, pItemDefIDsPtr, punItemDefIDsArraySizePtr);
			}
		}
		return ret != 0;
	}

	internal static ulong ISteamInventory_StartPurchase(IntPtr instancePtr, SteamItemDef_t[]? pArrayItemDefs, uint[]? punArrayQuantity, uint unArrayLength)
	{
		if ((nint)_ISteamInventory_StartPurchase == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamInventory_StartPurchase' is not available.");
		ulong ret;
		fixed (SteamItemDef_t* pArrayItemDefsPtr = pArrayItemDefs)
		{
			fixed (uint* punArrayQuantityPtr = punArrayQuantity)
			{
				ret = _ISteamInventory_StartPurchase(instancePtr, pArrayItemDefsPtr, punArrayQuantityPtr, unArrayLength);
			}
		}
		return ret;
	}

	internal static ulong ISteamInventory_RequestPrices(IntPtr instancePtr)
	{
		if ((nint)_ISteamInventory_RequestPrices == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamInventory_RequestPrices' is not available.");
		return _ISteamInventory_RequestPrices(instancePtr);
	}

	internal static uint ISteamInventory_GetNumItemsWithPrices(IntPtr instancePtr)
	{
		if ((nint)_ISteamInventory_GetNumItemsWithPrices == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamInventory_GetNumItemsWithPrices' is not available.");
		return _ISteamInventory_GetNumItemsWithPrices(instancePtr);
	}

	internal static bool ISteamInventory_GetItemsWithPrices(IntPtr instancePtr, SteamItemDef_t[]? pArrayItemDefs, ulong[]? pCurrentPrices, ulong[]? pBasePrices, uint unArrayLength)
	{
		if ((nint)_ISteamInventory_GetItemsWithPrices == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamInventory_GetItemsWithPrices' is not available.");
		byte ret;
		fixed (SteamItemDef_t* pArrayItemDefsPtr = pArrayItemDefs)
		{
			fixed (ulong* pCurrentPricesPtr = pCurrentPrices)
			{
				fixed (ulong* pBasePricesPtr = pBasePrices)
				{
					ret = _ISteamInventory_GetItemsWithPrices(instancePtr, pArrayItemDefsPtr, pCurrentPricesPtr, pBasePricesPtr, unArrayLength);
				}
			}
		}
		return ret != 0;
	}

	internal static bool ISteamInventory_GetItemPrice(IntPtr instancePtr, SteamItemDef_t iDefinition, out ulong pCurrentPrice, out ulong pBasePrice)
	{
		if ((nint)_ISteamInventory_GetItemPrice == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamInventory_GetItemPrice' is not available.");
		pCurrentPrice = default;
		pBasePrice = default;
		byte ret;
		fixed (ulong* pCurrentPricePtr = &pCurrentPrice)
		{
			fixed (ulong* pBasePricePtr = &pBasePrice)
			{
				ret = _ISteamInventory_GetItemPrice(instancePtr, iDefinition, pCurrentPricePtr, pBasePricePtr);
			}
		}
		return ret != 0;
	}

	internal static ulong ISteamInventory_StartUpdateProperties(IntPtr instancePtr)
	{
		if ((nint)_ISteamInventory_StartUpdateProperties == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamInventory_StartUpdateProperties' is not available.");
		return _ISteamInventory_StartUpdateProperties(instancePtr);
	}

	internal static bool ISteamInventory_RemoveProperty(IntPtr instancePtr, SteamInventoryUpdateHandle_t handle, SteamItemInstanceID_t nItemID, string? pchPropertyName)
	{
		if ((nint)_ISteamInventory_RemoveProperty == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamInventory_RemoveProperty' is not available.");
		using var pchPropertyNameStr = new ScopedCString(pchPropertyName ?? string.Empty);
		byte ret;
		fixed (byte* pchPropertyNameBufferPtr = pchPropertyNameStr)
		{
			ret = _ISteamInventory_RemoveProperty(instancePtr, handle, nItemID, pchPropertyName is null ? null : pchPropertyNameBufferPtr);
		}
		return ret != 0;
	}

	internal static bool ISteamInventory_SetPropertyString(IntPtr instancePtr, SteamInventoryUpdateHandle_t handle, SteamItemInstanceID_t nItemID, string? pchPropertyName, string? pchPropertyValue)
	{
		if ((nint)_ISteamInventory_SetPropertyString == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamInventory_SetPropertyString' is not available.");
		using var pchPropertyNameStr = new ScopedCString(pchPropertyName ?? string.Empty);
		using var pchPropertyValueStr = new ScopedCString(pchPropertyValue ?? string.Empty);
		byte ret;
		fixed (byte* pchPropertyNameBufferPtr = pchPropertyNameStr)
		{
			fixed (byte* pchPropertyValueBufferPtr = pchPropertyValueStr)
			{
				ret = _ISteamInventory_SetPropertyString(instancePtr, handle, nItemID, pchPropertyName is null ? null : pchPropertyNameBufferPtr, pchPropertyValue is null ? null : pchPropertyValueBufferPtr);
			}
		}
		return ret != 0;
	}

	internal static bool ISteamInventory_SetPropertyBool(IntPtr instancePtr, SteamInventoryUpdateHandle_t handle, SteamItemInstanceID_t nItemID, string? pchPropertyName, bool bValue)
	{
		if ((nint)_ISteamInventory_SetPropertyBool == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamInventory_SetPropertyBool' is not available.");
		using var pchPropertyNameStr = new ScopedCString(pchPropertyName ?? string.Empty);
		byte ret;
		fixed (byte* pchPropertyNameBufferPtr = pchPropertyNameStr)
		{
			ret = _ISteamInventory_SetPropertyBool(instancePtr, handle, nItemID, pchPropertyName is null ? null : pchPropertyNameBufferPtr, (byte)(bValue ? 1 : 0));
		}
		return ret != 0;
	}

	internal static bool ISteamInventory_SetPropertyInt64(IntPtr instancePtr, SteamInventoryUpdateHandle_t handle, SteamItemInstanceID_t nItemID, string? pchPropertyName, long nValue)
	{
		if ((nint)_ISteamInventory_SetPropertyInt64 == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamInventory_SetPropertyInt64' is not available.");
		using var pchPropertyNameStr = new ScopedCString(pchPropertyName ?? string.Empty);
		byte ret;
		fixed (byte* pchPropertyNameBufferPtr = pchPropertyNameStr)
		{
			ret = _ISteamInventory_SetPropertyInt64(instancePtr, handle, nItemID, pchPropertyName is null ? null : pchPropertyNameBufferPtr, nValue);
		}
		return ret != 0;
	}

	internal static bool ISteamInventory_SetPropertyFloat(IntPtr instancePtr, SteamInventoryUpdateHandle_t handle, SteamItemInstanceID_t nItemID, string? pchPropertyName, float flValue)
	{
		if ((nint)_ISteamInventory_SetPropertyFloat == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamInventory_SetPropertyFloat' is not available.");
		using var pchPropertyNameStr = new ScopedCString(pchPropertyName ?? string.Empty);
		byte ret;
		fixed (byte* pchPropertyNameBufferPtr = pchPropertyNameStr)
		{
			ret = _ISteamInventory_SetPropertyFloat(instancePtr, handle, nItemID, pchPropertyName is null ? null : pchPropertyNameBufferPtr, flValue);
		}
		return ret != 0;
	}

	internal static bool ISteamInventory_SubmitUpdateProperties(IntPtr instancePtr, SteamInventoryUpdateHandle_t handle, out SteamInventoryResult_t pResultHandle)
	{
		if ((nint)_ISteamInventory_SubmitUpdateProperties == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamInventory_SubmitUpdateProperties' is not available.");
		pResultHandle = default;
		byte ret;
		fixed (SteamInventoryResult_t* pResultHandlePtr = &pResultHandle)
		{
			ret = _ISteamInventory_SubmitUpdateProperties(instancePtr, handle, pResultHandlePtr);
		}
		return ret != 0;
	}

	internal static bool ISteamInventory_InspectItem(IntPtr instancePtr, out SteamInventoryResult_t pResultHandle, string? pchItemToken)
	{
		if ((nint)_ISteamInventory_InspectItem == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamInventory_InspectItem' is not available.");
		pResultHandle = default;
		using var pchItemTokenStr = new ScopedCString(pchItemToken ?? string.Empty);
		byte ret;
		fixed (SteamInventoryResult_t* pResultHandlePtr = &pResultHandle)
		{
			fixed (byte* pchItemTokenBufferPtr = pchItemTokenStr)
			{
				ret = _ISteamInventory_InspectItem(instancePtr, pResultHandlePtr, pchItemToken is null ? null : pchItemTokenBufferPtr);
			}
		}
		return ret != 0;
	}

	internal static bool ISteamNetworking_SendP2PPacket(IntPtr instancePtr, CSteamID steamIDRemote, byte[]? pubData, uint cubData, EP2PSend eP2PSendType, int nChannel)
	{
		if ((nint)_ISteamNetworking_SendP2PPacket == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworking_SendP2PPacket' is not available.");
		byte ret;
		fixed (byte* pubDataPtr = pubData)
		{
			ret = _ISteamNetworking_SendP2PPacket(instancePtr, steamIDRemote, pubDataPtr, cubData, eP2PSendType, nChannel);
		}
		return ret != 0;
	}

	internal static bool ISteamNetworking_IsP2PPacketAvailable(IntPtr instancePtr, out uint pcubMsgSize, int nChannel)
	{
		if ((nint)_ISteamNetworking_IsP2PPacketAvailable == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworking_IsP2PPacketAvailable' is not available.");
		pcubMsgSize = default;
		byte ret;
		fixed (uint* pcubMsgSizePtr = &pcubMsgSize)
		{
			ret = _ISteamNetworking_IsP2PPacketAvailable(instancePtr, pcubMsgSizePtr, nChannel);
		}
		return ret != 0;
	}

	internal static bool ISteamNetworking_ReadP2PPacket(IntPtr instancePtr, byte[]? pubDest, uint cubDest, out uint pcubMsgSize, out CSteamID psteamIDRemote, int nChannel)
	{
		if ((nint)_ISteamNetworking_ReadP2PPacket == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworking_ReadP2PPacket' is not available.");
		pcubMsgSize = default;
		psteamIDRemote = default;
		byte ret;
		fixed (byte* pubDestPtr = pubDest)
		{
			fixed (uint* pcubMsgSizePtr = &pcubMsgSize)
			{
				fixed (CSteamID* psteamIDRemotePtr = &psteamIDRemote)
				{
					ret = _ISteamNetworking_ReadP2PPacket(instancePtr, pubDestPtr, cubDest, pcubMsgSizePtr, psteamIDRemotePtr, nChannel);
				}
			}
		}
		return ret != 0;
	}

	internal static bool ISteamNetworking_AcceptP2PSessionWithUser(IntPtr instancePtr, CSteamID steamIDRemote)
	{
		if ((nint)_ISteamNetworking_AcceptP2PSessionWithUser == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworking_AcceptP2PSessionWithUser' is not available.");
		return _ISteamNetworking_AcceptP2PSessionWithUser(instancePtr, steamIDRemote) != 0;
	}

	internal static bool ISteamNetworking_CloseP2PSessionWithUser(IntPtr instancePtr, CSteamID steamIDRemote)
	{
		if ((nint)_ISteamNetworking_CloseP2PSessionWithUser == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworking_CloseP2PSessionWithUser' is not available.");
		return _ISteamNetworking_CloseP2PSessionWithUser(instancePtr, steamIDRemote) != 0;
	}

	internal static bool ISteamNetworking_CloseP2PChannelWithUser(IntPtr instancePtr, CSteamID steamIDRemote, int nChannel)
	{
		if ((nint)_ISteamNetworking_CloseP2PChannelWithUser == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworking_CloseP2PChannelWithUser' is not available.");
		return _ISteamNetworking_CloseP2PChannelWithUser(instancePtr, steamIDRemote, nChannel) != 0;
	}

	internal static bool ISteamNetworking_GetP2PSessionState(IntPtr instancePtr, CSteamID steamIDRemote, out P2PSessionState_t pConnectionState)
	{
		if ((nint)_ISteamNetworking_GetP2PSessionState == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworking_GetP2PSessionState' is not available.");
		pConnectionState = default;
		byte ret;
		fixed (P2PSessionState_t* pConnectionStatePtr = &pConnectionState)
		{
			ret = _ISteamNetworking_GetP2PSessionState(instancePtr, steamIDRemote, pConnectionStatePtr);
		}
		return ret != 0;
	}

	internal static bool ISteamNetworking_AllowP2PPacketRelay(IntPtr instancePtr, bool bAllow)
	{
		if ((nint)_ISteamNetworking_AllowP2PPacketRelay == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworking_AllowP2PPacketRelay' is not available.");
		return _ISteamNetworking_AllowP2PPacketRelay(instancePtr, (byte)(bAllow ? 1 : 0)) != 0;
	}

	internal static uint ISteamNetworking_CreateListenSocket(IntPtr instancePtr, int nVirtualP2PPort, SteamIPAddress_t nIP, ushort nPort, bool bAllowUseOfPacketRelay)
	{
		if ((nint)_ISteamNetworking_CreateListenSocket == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworking_CreateListenSocket' is not available.");
		return _ISteamNetworking_CreateListenSocket(instancePtr, nVirtualP2PPort, nIP, nPort, (byte)(bAllowUseOfPacketRelay ? 1 : 0));
	}

	internal static uint ISteamNetworking_CreateP2PConnectionSocket(IntPtr instancePtr, CSteamID steamIDTarget, int nVirtualPort, int nTimeoutSec, bool bAllowUseOfPacketRelay)
	{
		if ((nint)_ISteamNetworking_CreateP2PConnectionSocket == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworking_CreateP2PConnectionSocket' is not available.");
		return _ISteamNetworking_CreateP2PConnectionSocket(instancePtr, steamIDTarget, nVirtualPort, nTimeoutSec, (byte)(bAllowUseOfPacketRelay ? 1 : 0));
	}

	internal static uint ISteamNetworking_CreateConnectionSocket(IntPtr instancePtr, SteamIPAddress_t nIP, ushort nPort, int nTimeoutSec)
	{
		if ((nint)_ISteamNetworking_CreateConnectionSocket == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworking_CreateConnectionSocket' is not available.");
		return _ISteamNetworking_CreateConnectionSocket(instancePtr, nIP, nPort, nTimeoutSec);
	}

	internal static bool ISteamNetworking_DestroySocket(IntPtr instancePtr, SNetSocket_t hSocket, bool bNotifyRemoteEnd)
	{
		if ((nint)_ISteamNetworking_DestroySocket == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworking_DestroySocket' is not available.");
		return _ISteamNetworking_DestroySocket(instancePtr, hSocket, (byte)(bNotifyRemoteEnd ? 1 : 0)) != 0;
	}

	internal static bool ISteamNetworking_DestroyListenSocket(IntPtr instancePtr, SNetListenSocket_t hSocket, bool bNotifyRemoteEnd)
	{
		if ((nint)_ISteamNetworking_DestroyListenSocket == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworking_DestroyListenSocket' is not available.");
		return _ISteamNetworking_DestroyListenSocket(instancePtr, hSocket, (byte)(bNotifyRemoteEnd ? 1 : 0)) != 0;
	}

	internal static bool ISteamNetworking_SendDataOnSocket(IntPtr instancePtr, SNetSocket_t hSocket, byte[]? pubData, uint cubData, bool bReliable)
	{
		if ((nint)_ISteamNetworking_SendDataOnSocket == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworking_SendDataOnSocket' is not available.");
		byte ret;
		fixed (byte* pubDataPtr = pubData)
		{
			ret = _ISteamNetworking_SendDataOnSocket(instancePtr, hSocket, pubDataPtr, cubData, (byte)(bReliable ? 1 : 0));
		}
		return ret != 0;
	}

	internal static bool ISteamNetworking_IsDataAvailableOnSocket(IntPtr instancePtr, SNetSocket_t hSocket, out uint pcubMsgSize)
	{
		if ((nint)_ISteamNetworking_IsDataAvailableOnSocket == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworking_IsDataAvailableOnSocket' is not available.");
		pcubMsgSize = default;
		byte ret;
		fixed (uint* pcubMsgSizePtr = &pcubMsgSize)
		{
			ret = _ISteamNetworking_IsDataAvailableOnSocket(instancePtr, hSocket, pcubMsgSizePtr);
		}
		return ret != 0;
	}

	internal static bool ISteamNetworking_RetrieveDataFromSocket(IntPtr instancePtr, SNetSocket_t hSocket, byte[]? pubDest, uint cubDest, out uint pcubMsgSize)
	{
		if ((nint)_ISteamNetworking_RetrieveDataFromSocket == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworking_RetrieveDataFromSocket' is not available.");
		pcubMsgSize = default;
		byte ret;
		fixed (byte* pubDestPtr = pubDest)
		{
			fixed (uint* pcubMsgSizePtr = &pcubMsgSize)
			{
				ret = _ISteamNetworking_RetrieveDataFromSocket(instancePtr, hSocket, pubDestPtr, cubDest, pcubMsgSizePtr);
			}
		}
		return ret != 0;
	}

	internal static bool ISteamNetworking_IsDataAvailable(IntPtr instancePtr, SNetListenSocket_t hListenSocket, out uint pcubMsgSize, out SNetSocket_t phSocket)
	{
		if ((nint)_ISteamNetworking_IsDataAvailable == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworking_IsDataAvailable' is not available.");
		pcubMsgSize = default;
		phSocket = default;
		byte ret;
		fixed (uint* pcubMsgSizePtr = &pcubMsgSize)
		{
			fixed (SNetSocket_t* phSocketPtr = &phSocket)
			{
				ret = _ISteamNetworking_IsDataAvailable(instancePtr, hListenSocket, pcubMsgSizePtr, phSocketPtr);
			}
		}
		return ret != 0;
	}

	internal static bool ISteamNetworking_RetrieveData(IntPtr instancePtr, SNetListenSocket_t hListenSocket, byte[]? pubDest, uint cubDest, out uint pcubMsgSize, out SNetSocket_t phSocket)
	{
		if ((nint)_ISteamNetworking_RetrieveData == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworking_RetrieveData' is not available.");
		pcubMsgSize = default;
		phSocket = default;
		byte ret;
		fixed (byte* pubDestPtr = pubDest)
		{
			fixed (uint* pcubMsgSizePtr = &pcubMsgSize)
			{
				fixed (SNetSocket_t* phSocketPtr = &phSocket)
				{
					ret = _ISteamNetworking_RetrieveData(instancePtr, hListenSocket, pubDestPtr, cubDest, pcubMsgSizePtr, phSocketPtr);
				}
			}
		}
		return ret != 0;
	}

	internal static bool ISteamNetworking_GetSocketInfo(IntPtr instancePtr, SNetSocket_t hSocket, out CSteamID pSteamIDRemote, out int peSocketStatus, out SteamIPAddress_t punIPRemote, out ushort punPortRemote)
	{
		if ((nint)_ISteamNetworking_GetSocketInfo == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworking_GetSocketInfo' is not available.");
		pSteamIDRemote = default;
		peSocketStatus = default;
		punIPRemote = default;
		punPortRemote = default;
		byte ret;
		fixed (CSteamID* pSteamIDRemotePtr = &pSteamIDRemote)
		{
			fixed (int* peSocketStatusPtr = &peSocketStatus)
			{
				fixed (SteamIPAddress_t* punIPRemotePtr = &punIPRemote)
				{
					fixed (ushort* punPortRemotePtr = &punPortRemote)
					{
						ret = _ISteamNetworking_GetSocketInfo(instancePtr, hSocket, pSteamIDRemotePtr, peSocketStatusPtr, punIPRemotePtr, punPortRemotePtr);
					}
				}
			}
		}
		return ret != 0;
	}

	internal static bool ISteamNetworking_GetListenSocketInfo(IntPtr instancePtr, SNetListenSocket_t hListenSocket, out SteamIPAddress_t pnIP, out ushort pnPort)
	{
		if ((nint)_ISteamNetworking_GetListenSocketInfo == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworking_GetListenSocketInfo' is not available.");
		pnIP = default;
		pnPort = default;
		byte ret;
		fixed (SteamIPAddress_t* pnIPPtr = &pnIP)
		{
			fixed (ushort* pnPortPtr = &pnPort)
			{
				ret = _ISteamNetworking_GetListenSocketInfo(instancePtr, hListenSocket, pnIPPtr, pnPortPtr);
			}
		}
		return ret != 0;
	}

	internal static ESNetSocketConnectionType ISteamNetworking_GetSocketConnectionType(IntPtr instancePtr, SNetSocket_t hSocket)
	{
		if ((nint)_ISteamNetworking_GetSocketConnectionType == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworking_GetSocketConnectionType' is not available.");
		return _ISteamNetworking_GetSocketConnectionType(instancePtr, hSocket);
	}

	internal static int ISteamNetworking_GetMaxPacketSize(IntPtr instancePtr, SNetSocket_t hSocket)
	{
		if ((nint)_ISteamNetworking_GetMaxPacketSize == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworking_GetMaxPacketSize' is not available.");
		return _ISteamNetworking_GetMaxPacketSize(instancePtr, hSocket);
	}

	internal static uint ISteamNetworkingSockets_CreateListenSocketIP(IntPtr instancePtr, ref SteamNetworkingIPAddr localAddress, int nOptions, SteamNetworkingConfigValue_t[]? pOptions)
	{
		if ((nint)_ISteamNetworkingSockets_CreateListenSocketIP == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingSockets_CreateListenSocketIP' is not available.");
		uint ret;
		fixed (SteamNetworkingIPAddr* localAddressPtr = &localAddress)
		{
			fixed (SteamNetworkingConfigValue_t* pOptionsPtr = pOptions)
			{
				ret = _ISteamNetworkingSockets_CreateListenSocketIP(instancePtr, localAddressPtr, nOptions, pOptionsPtr);
			}
		}
		return ret;
	}

	internal static uint ISteamNetworkingSockets_ConnectByIPAddress(IntPtr instancePtr, ref SteamNetworkingIPAddr address, int nOptions, SteamNetworkingConfigValue_t[]? pOptions)
	{
		if ((nint)_ISteamNetworkingSockets_ConnectByIPAddress == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingSockets_ConnectByIPAddress' is not available.");
		uint ret;
		fixed (SteamNetworkingIPAddr* addressPtr = &address)
		{
			fixed (SteamNetworkingConfigValue_t* pOptionsPtr = pOptions)
			{
				ret = _ISteamNetworkingSockets_ConnectByIPAddress(instancePtr, addressPtr, nOptions, pOptionsPtr);
			}
		}
		return ret;
	}

	internal static uint ISteamNetworkingSockets_CreateListenSocketP2P(IntPtr instancePtr, int nLocalVirtualPort, int nOptions, SteamNetworkingConfigValue_t[]? pOptions)
	{
		if ((nint)_ISteamNetworkingSockets_CreateListenSocketP2P == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingSockets_CreateListenSocketP2P' is not available.");
		uint ret;
		fixed (SteamNetworkingConfigValue_t* pOptionsPtr = pOptions)
		{
			ret = _ISteamNetworkingSockets_CreateListenSocketP2P(instancePtr, nLocalVirtualPort, nOptions, pOptionsPtr);
		}
		return ret;
	}

	internal static uint ISteamNetworkingSockets_ConnectP2P(IntPtr instancePtr, ref SteamNetworkingIdentity identityRemote, int nRemoteVirtualPort, int nOptions, SteamNetworkingConfigValue_t[]? pOptions)
	{
		if ((nint)_ISteamNetworkingSockets_ConnectP2P == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingSockets_ConnectP2P' is not available.");
		uint ret;
		fixed (SteamNetworkingIdentity* identityRemotePtr = &identityRemote)
		{
			fixed (SteamNetworkingConfigValue_t* pOptionsPtr = pOptions)
			{
				ret = _ISteamNetworkingSockets_ConnectP2P(instancePtr, identityRemotePtr, nRemoteVirtualPort, nOptions, pOptionsPtr);
			}
		}
		return ret;
	}

	internal static EResult ISteamNetworkingSockets_AcceptConnection(IntPtr instancePtr, HSteamNetConnection hConn)
	{
		if ((nint)_ISteamNetworkingSockets_AcceptConnection == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingSockets_AcceptConnection' is not available.");
		return _ISteamNetworkingSockets_AcceptConnection(instancePtr, hConn);
	}

	internal static bool ISteamNetworkingSockets_CloseConnection(IntPtr instancePtr, HSteamNetConnection hPeer, int nReason, string? pszDebug, bool bEnableLinger)
	{
		if ((nint)_ISteamNetworkingSockets_CloseConnection == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingSockets_CloseConnection' is not available.");
		using var pszDebugStr = new ScopedCString(pszDebug ?? string.Empty);
		byte ret;
		fixed (byte* pszDebugBufferPtr = pszDebugStr)
		{
			ret = _ISteamNetworkingSockets_CloseConnection(instancePtr, hPeer, nReason, pszDebug is null ? null : pszDebugBufferPtr, (byte)(bEnableLinger ? 1 : 0));
		}
		return ret != 0;
	}

	internal static bool ISteamNetworkingSockets_CloseListenSocket(IntPtr instancePtr, HSteamListenSocket hSocket)
	{
		if ((nint)_ISteamNetworkingSockets_CloseListenSocket == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingSockets_CloseListenSocket' is not available.");
		return _ISteamNetworkingSockets_CloseListenSocket(instancePtr, hSocket) != 0;
	}

	internal static bool ISteamNetworkingSockets_SetConnectionUserData(IntPtr instancePtr, HSteamNetConnection hPeer, long nUserData)
	{
		if ((nint)_ISteamNetworkingSockets_SetConnectionUserData == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingSockets_SetConnectionUserData' is not available.");
		return _ISteamNetworkingSockets_SetConnectionUserData(instancePtr, hPeer, nUserData) != 0;
	}

	internal static long ISteamNetworkingSockets_GetConnectionUserData(IntPtr instancePtr, HSteamNetConnection hPeer)
	{
		if ((nint)_ISteamNetworkingSockets_GetConnectionUserData == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingSockets_GetConnectionUserData' is not available.");
		return _ISteamNetworkingSockets_GetConnectionUserData(instancePtr, hPeer);
	}

	internal static void ISteamNetworkingSockets_SetConnectionName(IntPtr instancePtr, HSteamNetConnection hPeer, string? pszName)
	{
		if ((nint)_ISteamNetworkingSockets_SetConnectionName == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingSockets_SetConnectionName' is not available.");
		using var pszNameStr = new ScopedCString(pszName ?? string.Empty);
		fixed (byte* pszNameBufferPtr = pszNameStr)
		{
			_ISteamNetworkingSockets_SetConnectionName(instancePtr, hPeer, pszName is null ? null : pszNameBufferPtr);
		}
	}

	internal static bool ISteamNetworkingSockets_GetConnectionName(IntPtr instancePtr, HSteamNetConnection hPeer, IntPtr pszName, int nMaxLen)
	{
		if ((nint)_ISteamNetworkingSockets_GetConnectionName == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingSockets_GetConnectionName' is not available.");
		return _ISteamNetworkingSockets_GetConnectionName(instancePtr, hPeer, pszName, nMaxLen) != 0;
	}

	internal static EResult ISteamNetworkingSockets_SendMessageToConnection(IntPtr instancePtr, HSteamNetConnection hConn, IntPtr pData, uint cbData, int nSendFlags, out long pOutMessageNumber)
	{
		if ((nint)_ISteamNetworkingSockets_SendMessageToConnection == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingSockets_SendMessageToConnection' is not available.");
		pOutMessageNumber = default;
		EResult ret;
		fixed (long* pOutMessageNumberPtr = &pOutMessageNumber)
		{
			ret = _ISteamNetworkingSockets_SendMessageToConnection(instancePtr, hConn, pData, cbData, nSendFlags, pOutMessageNumberPtr);
		}
		return ret;
	}

	internal static void ISteamNetworkingSockets_SendMessages(IntPtr instancePtr, int nMessages, SteamNetworkingMessage_t[]? pMessages, long[]? pOutMessageNumberOrResult)
	{
		if ((nint)_ISteamNetworkingSockets_SendMessages == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingSockets_SendMessages' is not available.");
		fixed (SteamNetworkingMessage_t* pMessagesPtr = pMessages)
		{
			fixed (long* pOutMessageNumberOrResultPtr = pOutMessageNumberOrResult)
			{
				_ISteamNetworkingSockets_SendMessages(instancePtr, nMessages, pMessagesPtr, pOutMessageNumberOrResultPtr);
			}
		}
	}

	internal static EResult ISteamNetworkingSockets_FlushMessagesOnConnection(IntPtr instancePtr, HSteamNetConnection hConn)
	{
		if ((nint)_ISteamNetworkingSockets_FlushMessagesOnConnection == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingSockets_FlushMessagesOnConnection' is not available.");
		return _ISteamNetworkingSockets_FlushMessagesOnConnection(instancePtr, hConn);
	}

	internal static int ISteamNetworkingSockets_ReceiveMessagesOnConnection(IntPtr instancePtr, HSteamNetConnection hConn, IntPtr[]? ppOutMessages, int nMaxMessages)
	{
		if ((nint)_ISteamNetworkingSockets_ReceiveMessagesOnConnection == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingSockets_ReceiveMessagesOnConnection' is not available.");
		int ret;
		fixed (nint* ppOutMessagesPtr = ppOutMessages)
		{
			ret = _ISteamNetworkingSockets_ReceiveMessagesOnConnection(instancePtr, hConn, ppOutMessagesPtr, nMaxMessages);
		}
		return ret;
	}

	internal static bool ISteamNetworkingSockets_GetConnectionInfo(IntPtr instancePtr, HSteamNetConnection hConn, out SteamNetConnectionInfo_t pInfo)
	{
		if ((nint)_ISteamNetworkingSockets_GetConnectionInfo == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingSockets_GetConnectionInfo' is not available.");
		pInfo = default;
		byte ret;
		fixed (SteamNetConnectionInfo_t* pInfoPtr = &pInfo)
		{
			ret = _ISteamNetworkingSockets_GetConnectionInfo(instancePtr, hConn, pInfoPtr);
		}
		return ret != 0;
	}

	internal static EResult ISteamNetworkingSockets_GetConnectionRealTimeStatus(IntPtr instancePtr, HSteamNetConnection hConn, ref SteamNetConnectionRealTimeStatus_t pStatus, int nLanes, ref SteamNetConnectionRealTimeLaneStatus_t pLanes)
	{
		if ((nint)_ISteamNetworkingSockets_GetConnectionRealTimeStatus == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingSockets_GetConnectionRealTimeStatus' is not available.");
		EResult ret;
		fixed (SteamNetConnectionRealTimeStatus_t* pStatusPtr = &pStatus)
		{
			fixed (SteamNetConnectionRealTimeLaneStatus_t* pLanesPtr = &pLanes)
			{
				ret = _ISteamNetworkingSockets_GetConnectionRealTimeStatus(instancePtr, hConn, pStatusPtr, nLanes, pLanesPtr);
			}
		}
		return ret;
	}

	internal static int ISteamNetworkingSockets_GetDetailedConnectionStatus(IntPtr instancePtr, HSteamNetConnection hConn, IntPtr pszBuf, int cbBuf)
	{
		if ((nint)_ISteamNetworkingSockets_GetDetailedConnectionStatus == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingSockets_GetDetailedConnectionStatus' is not available.");
		return _ISteamNetworkingSockets_GetDetailedConnectionStatus(instancePtr, hConn, pszBuf, cbBuf);
	}

	internal static bool ISteamNetworkingSockets_GetListenSocketAddress(IntPtr instancePtr, HSteamListenSocket hSocket, out SteamNetworkingIPAddr address)
	{
		if ((nint)_ISteamNetworkingSockets_GetListenSocketAddress == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingSockets_GetListenSocketAddress' is not available.");
		address = default;
		byte ret;
		fixed (SteamNetworkingIPAddr* addressPtr = &address)
		{
			ret = _ISteamNetworkingSockets_GetListenSocketAddress(instancePtr, hSocket, addressPtr);
		}
		return ret != 0;
	}

	internal static bool ISteamNetworkingSockets_CreateSocketPair(IntPtr instancePtr, out HSteamNetConnection pOutConnection1, out HSteamNetConnection pOutConnection2, bool bUseNetworkLoopback, ref SteamNetworkingIdentity pIdentity1, ref SteamNetworkingIdentity pIdentity2)
	{
		if ((nint)_ISteamNetworkingSockets_CreateSocketPair == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingSockets_CreateSocketPair' is not available.");
		pOutConnection1 = default;
		pOutConnection2 = default;
		byte ret;
		fixed (HSteamNetConnection* pOutConnection1Ptr = &pOutConnection1)
		{
			fixed (HSteamNetConnection* pOutConnection2Ptr = &pOutConnection2)
			{
				fixed (SteamNetworkingIdentity* pIdentity1Ptr = &pIdentity1)
				{
					fixed (SteamNetworkingIdentity* pIdentity2Ptr = &pIdentity2)
					{
						ret = _ISteamNetworkingSockets_CreateSocketPair(instancePtr, pOutConnection1Ptr, pOutConnection2Ptr, (byte)(bUseNetworkLoopback ? 1 : 0), pIdentity1Ptr, pIdentity2Ptr);
					}
				}
			}
		}
		return ret != 0;
	}

	internal static EResult ISteamNetworkingSockets_ConfigureConnectionLanes(IntPtr instancePtr, HSteamNetConnection hConn, int nNumLanes, out int pLanePriorities, out ushort pLaneWeights)
	{
		if ((nint)_ISteamNetworkingSockets_ConfigureConnectionLanes == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingSockets_ConfigureConnectionLanes' is not available.");
		pLanePriorities = default;
		pLaneWeights = default;
		EResult ret;
		fixed (int* pLanePrioritiesPtr = &pLanePriorities)
		{
			fixed (ushort* pLaneWeightsPtr = &pLaneWeights)
			{
				ret = _ISteamNetworkingSockets_ConfigureConnectionLanes(instancePtr, hConn, nNumLanes, pLanePrioritiesPtr, pLaneWeightsPtr);
			}
		}
		return ret;
	}

	internal static bool ISteamNetworkingSockets_GetIdentity(IntPtr instancePtr, out SteamNetworkingIdentity pIdentity)
	{
		if ((nint)_ISteamNetworkingSockets_GetIdentity == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingSockets_GetIdentity' is not available.");
		pIdentity = default;
		byte ret;
		fixed (SteamNetworkingIdentity* pIdentityPtr = &pIdentity)
		{
			ret = _ISteamNetworkingSockets_GetIdentity(instancePtr, pIdentityPtr);
		}
		return ret != 0;
	}

	internal static ESteamNetworkingAvailability ISteamNetworkingSockets_InitAuthentication(IntPtr instancePtr)
	{
		if ((nint)_ISteamNetworkingSockets_InitAuthentication == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingSockets_InitAuthentication' is not available.");
		return _ISteamNetworkingSockets_InitAuthentication(instancePtr);
	}

	internal static ESteamNetworkingAvailability ISteamNetworkingSockets_GetAuthenticationStatus(IntPtr instancePtr, out SteamNetAuthenticationStatus_t pDetails)
	{
		if ((nint)_ISteamNetworkingSockets_GetAuthenticationStatus == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingSockets_GetAuthenticationStatus' is not available.");
		pDetails = default;
		ESteamNetworkingAvailability ret;
		fixed (SteamNetAuthenticationStatus_t* pDetailsPtr = &pDetails)
		{
			ret = _ISteamNetworkingSockets_GetAuthenticationStatus(instancePtr, pDetailsPtr);
		}
		return ret;
	}

	internal static uint ISteamNetworkingSockets_CreatePollGroup(IntPtr instancePtr)
	{
		if ((nint)_ISteamNetworkingSockets_CreatePollGroup == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingSockets_CreatePollGroup' is not available.");
		return _ISteamNetworkingSockets_CreatePollGroup(instancePtr);
	}

	internal static bool ISteamNetworkingSockets_DestroyPollGroup(IntPtr instancePtr, HSteamNetPollGroup hPollGroup)
	{
		if ((nint)_ISteamNetworkingSockets_DestroyPollGroup == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingSockets_DestroyPollGroup' is not available.");
		return _ISteamNetworkingSockets_DestroyPollGroup(instancePtr, hPollGroup) != 0;
	}

	internal static bool ISteamNetworkingSockets_SetConnectionPollGroup(IntPtr instancePtr, HSteamNetConnection hConn, HSteamNetPollGroup hPollGroup)
	{
		if ((nint)_ISteamNetworkingSockets_SetConnectionPollGroup == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingSockets_SetConnectionPollGroup' is not available.");
		return _ISteamNetworkingSockets_SetConnectionPollGroup(instancePtr, hConn, hPollGroup) != 0;
	}

	internal static int ISteamNetworkingSockets_ReceiveMessagesOnPollGroup(IntPtr instancePtr, HSteamNetPollGroup hPollGroup, IntPtr[]? ppOutMessages, int nMaxMessages)
	{
		if ((nint)_ISteamNetworkingSockets_ReceiveMessagesOnPollGroup == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingSockets_ReceiveMessagesOnPollGroup' is not available.");
		int ret;
		fixed (nint* ppOutMessagesPtr = ppOutMessages)
		{
			ret = _ISteamNetworkingSockets_ReceiveMessagesOnPollGroup(instancePtr, hPollGroup, ppOutMessagesPtr, nMaxMessages);
		}
		return ret;
	}

	internal static bool ISteamNetworkingSockets_ReceivedRelayAuthTicket(IntPtr instancePtr, IntPtr pvTicket, int cbTicket, out SteamDatagramRelayAuthTicket pOutParsedTicket)
	{
		if ((nint)_ISteamNetworkingSockets_ReceivedRelayAuthTicket == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingSockets_ReceivedRelayAuthTicket' is not available.");
		pOutParsedTicket = default;
		byte ret;
		fixed (SteamDatagramRelayAuthTicket* pOutParsedTicketPtr = &pOutParsedTicket)
		{
			ret = _ISteamNetworkingSockets_ReceivedRelayAuthTicket(instancePtr, pvTicket, cbTicket, pOutParsedTicketPtr);
		}
		return ret != 0;
	}

	internal static int ISteamNetworkingSockets_FindRelayAuthTicketForServer(IntPtr instancePtr, ref SteamNetworkingIdentity identityGameServer, int nRemoteVirtualPort, out SteamDatagramRelayAuthTicket pOutParsedTicket)
	{
		if ((nint)_ISteamNetworkingSockets_FindRelayAuthTicketForServer == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingSockets_FindRelayAuthTicketForServer' is not available.");
		pOutParsedTicket = default;
		int ret;
		fixed (SteamNetworkingIdentity* identityGameServerPtr = &identityGameServer)
		{
			fixed (SteamDatagramRelayAuthTicket* pOutParsedTicketPtr = &pOutParsedTicket)
			{
				ret = _ISteamNetworkingSockets_FindRelayAuthTicketForServer(instancePtr, identityGameServerPtr, nRemoteVirtualPort, pOutParsedTicketPtr);
			}
		}
		return ret;
	}

	internal static uint ISteamNetworkingSockets_ConnectToHostedDedicatedServer(IntPtr instancePtr, ref SteamNetworkingIdentity identityTarget, int nRemoteVirtualPort, int nOptions, SteamNetworkingConfigValue_t[]? pOptions)
	{
		if ((nint)_ISteamNetworkingSockets_ConnectToHostedDedicatedServer == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingSockets_ConnectToHostedDedicatedServer' is not available.");
		uint ret;
		fixed (SteamNetworkingIdentity* identityTargetPtr = &identityTarget)
		{
			fixed (SteamNetworkingConfigValue_t* pOptionsPtr = pOptions)
			{
				ret = _ISteamNetworkingSockets_ConnectToHostedDedicatedServer(instancePtr, identityTargetPtr, nRemoteVirtualPort, nOptions, pOptionsPtr);
			}
		}
		return ret;
	}

	internal static ushort ISteamNetworkingSockets_GetHostedDedicatedServerPort(IntPtr instancePtr)
	{
		if ((nint)_ISteamNetworkingSockets_GetHostedDedicatedServerPort == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingSockets_GetHostedDedicatedServerPort' is not available.");
		return _ISteamNetworkingSockets_GetHostedDedicatedServerPort(instancePtr);
	}

	internal static uint ISteamNetworkingSockets_GetHostedDedicatedServerPOPID(IntPtr instancePtr)
	{
		if ((nint)_ISteamNetworkingSockets_GetHostedDedicatedServerPOPID == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingSockets_GetHostedDedicatedServerPOPID' is not available.");
		return _ISteamNetworkingSockets_GetHostedDedicatedServerPOPID(instancePtr);
	}

	internal static EResult ISteamNetworkingSockets_GetHostedDedicatedServerAddress(IntPtr instancePtr, out SteamDatagramHostedAddress pRouting)
	{
		if ((nint)_ISteamNetworkingSockets_GetHostedDedicatedServerAddress == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingSockets_GetHostedDedicatedServerAddress' is not available.");
		pRouting = default;
		EResult ret;
		fixed (SteamDatagramHostedAddress* pRoutingPtr = &pRouting)
		{
			ret = _ISteamNetworkingSockets_GetHostedDedicatedServerAddress(instancePtr, pRoutingPtr);
		}
		return ret;
	}

	internal static uint ISteamNetworkingSockets_CreateHostedDedicatedServerListenSocket(IntPtr instancePtr, int nLocalVirtualPort, int nOptions, SteamNetworkingConfigValue_t[]? pOptions)
	{
		if ((nint)_ISteamNetworkingSockets_CreateHostedDedicatedServerListenSocket == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingSockets_CreateHostedDedicatedServerListenSocket' is not available.");
		uint ret;
		fixed (SteamNetworkingConfigValue_t* pOptionsPtr = pOptions)
		{
			ret = _ISteamNetworkingSockets_CreateHostedDedicatedServerListenSocket(instancePtr, nLocalVirtualPort, nOptions, pOptionsPtr);
		}
		return ret;
	}

	internal static EResult ISteamNetworkingSockets_GetGameCoordinatorServerLogin(IntPtr instancePtr, IntPtr pLoginInfo, out int pcbSignedBlob, IntPtr pBlob)
	{
		if ((nint)_ISteamNetworkingSockets_GetGameCoordinatorServerLogin == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingSockets_GetGameCoordinatorServerLogin' is not available.");
		pcbSignedBlob = default;
		EResult ret;
		fixed (int* pcbSignedBlobPtr = &pcbSignedBlob)
		{
			ret = _ISteamNetworkingSockets_GetGameCoordinatorServerLogin(instancePtr, pLoginInfo, pcbSignedBlobPtr, pBlob);
		}
		return ret;
	}

	internal static uint ISteamNetworkingSockets_ConnectP2PCustomSignaling(IntPtr instancePtr, out ISteamNetworkingConnectionSignaling pSignaling, ref SteamNetworkingIdentity pPeerIdentity, int nRemoteVirtualPort, int nOptions, SteamNetworkingConfigValue_t[]? pOptions)
	{
		if ((nint)_ISteamNetworkingSockets_ConnectP2PCustomSignaling == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingSockets_ConnectP2PCustomSignaling' is not available.");
		pSignaling = default;
		uint ret;
		fixed (ISteamNetworkingConnectionSignaling* pSignalingPtr = &pSignaling)
		{
			fixed (SteamNetworkingIdentity* pPeerIdentityPtr = &pPeerIdentity)
			{
				fixed (SteamNetworkingConfigValue_t* pOptionsPtr = pOptions)
				{
					ret = _ISteamNetworkingSockets_ConnectP2PCustomSignaling(instancePtr, pSignalingPtr, pPeerIdentityPtr, nRemoteVirtualPort, nOptions, pOptionsPtr);
				}
			}
		}
		return ret;
	}

	internal static bool ISteamNetworkingSockets_ReceivedP2PCustomSignal(IntPtr instancePtr, IntPtr pMsg, int cbMsg, out ISteamNetworkingSignalingRecvContext pContext)
	{
		if ((nint)_ISteamNetworkingSockets_ReceivedP2PCustomSignal == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingSockets_ReceivedP2PCustomSignal' is not available.");
		pContext = default;
		byte ret;
		fixed (ISteamNetworkingSignalingRecvContext* pContextPtr = &pContext)
		{
			ret = _ISteamNetworkingSockets_ReceivedP2PCustomSignal(instancePtr, pMsg, cbMsg, pContextPtr);
		}
		return ret != 0;
	}

	internal static bool ISteamNetworkingSockets_GetCertificateRequest(IntPtr instancePtr, out int pcbBlob, IntPtr pBlob, out SteamNetworkingErrMsg errMsg)
	{
		if ((nint)_ISteamNetworkingSockets_GetCertificateRequest == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingSockets_GetCertificateRequest' is not available.");
		pcbBlob = default;
		errMsg = default;
		byte ret;
		fixed (int* pcbBlobPtr = &pcbBlob)
		{
			fixed (SteamNetworkingErrMsg* errMsgPtr = &errMsg)
			{
				ret = _ISteamNetworkingSockets_GetCertificateRequest(instancePtr, pcbBlobPtr, pBlob, errMsgPtr);
			}
		}
		return ret != 0;
	}

	internal static bool ISteamNetworkingSockets_SetCertificate(IntPtr instancePtr, IntPtr pCertificate, int cbCertificate, out SteamNetworkingErrMsg errMsg)
	{
		if ((nint)_ISteamNetworkingSockets_SetCertificate == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingSockets_SetCertificate' is not available.");
		errMsg = default;
		byte ret;
		fixed (SteamNetworkingErrMsg* errMsgPtr = &errMsg)
		{
			ret = _ISteamNetworkingSockets_SetCertificate(instancePtr, pCertificate, cbCertificate, errMsgPtr);
		}
		return ret != 0;
	}

	internal static void ISteamNetworkingSockets_ResetIdentity(IntPtr instancePtr, ref SteamNetworkingIdentity pIdentity)
	{
		if ((nint)_ISteamNetworkingSockets_ResetIdentity == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingSockets_ResetIdentity' is not available.");
		fixed (SteamNetworkingIdentity* pIdentityPtr = &pIdentity)
		{
			_ISteamNetworkingSockets_ResetIdentity(instancePtr, pIdentityPtr);
		}
	}

	internal static void ISteamNetworkingSockets_RunCallbacks(IntPtr instancePtr)
	{
		if ((nint)_ISteamNetworkingSockets_RunCallbacks == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingSockets_RunCallbacks' is not available.");
		_ISteamNetworkingSockets_RunCallbacks(instancePtr);
	}

	internal static bool ISteamNetworkingSockets_BeginAsyncRequestFakeIP(IntPtr instancePtr, int nNumPorts)
	{
		if ((nint)_ISteamNetworkingSockets_BeginAsyncRequestFakeIP == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingSockets_BeginAsyncRequestFakeIP' is not available.");
		return _ISteamNetworkingSockets_BeginAsyncRequestFakeIP(instancePtr, nNumPorts) != 0;
	}

	internal static void ISteamNetworkingSockets_GetFakeIP(IntPtr instancePtr, int idxFirstPort, out SteamNetworkingFakeIPResult_t pInfo)
	{
		if ((nint)_ISteamNetworkingSockets_GetFakeIP == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingSockets_GetFakeIP' is not available.");
		pInfo = default;
		fixed (SteamNetworkingFakeIPResult_t* pInfoPtr = &pInfo)
		{
			_ISteamNetworkingSockets_GetFakeIP(instancePtr, idxFirstPort, pInfoPtr);
		}
	}

	internal static uint ISteamNetworkingSockets_CreateListenSocketP2PFakeIP(IntPtr instancePtr, int idxFakePort, int nOptions, SteamNetworkingConfigValue_t[]? pOptions)
	{
		if ((nint)_ISteamNetworkingSockets_CreateListenSocketP2PFakeIP == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingSockets_CreateListenSocketP2PFakeIP' is not available.");
		uint ret;
		fixed (SteamNetworkingConfigValue_t* pOptionsPtr = pOptions)
		{
			ret = _ISteamNetworkingSockets_CreateListenSocketP2PFakeIP(instancePtr, idxFakePort, nOptions, pOptionsPtr);
		}
		return ret;
	}

	internal static EResult ISteamNetworkingSockets_GetRemoteFakeIPForConnection(IntPtr instancePtr, HSteamNetConnection hConn, out SteamNetworkingIPAddr pOutAddr)
	{
		if ((nint)_ISteamNetworkingSockets_GetRemoteFakeIPForConnection == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingSockets_GetRemoteFakeIPForConnection' is not available.");
		pOutAddr = default;
		EResult ret;
		fixed (SteamNetworkingIPAddr* pOutAddrPtr = &pOutAddr)
		{
			ret = _ISteamNetworkingSockets_GetRemoteFakeIPForConnection(instancePtr, hConn, pOutAddrPtr);
		}
		return ret;
	}

	internal static IntPtr ISteamNetworkingSockets_CreateFakeUDPPort(IntPtr instancePtr, int idxFakeServerPort)
	{
		if ((nint)_ISteamNetworkingSockets_CreateFakeUDPPort == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingSockets_CreateFakeUDPPort' is not available.");
		return _ISteamNetworkingSockets_CreateFakeUDPPort(instancePtr, idxFakeServerPort);
	}

	internal static IntPtr ISteamNetworkingUtils_AllocateMessage(IntPtr instancePtr, int cbAllocateBuffer)
	{
		if ((nint)_ISteamNetworkingUtils_AllocateMessage == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingUtils_AllocateMessage' is not available.");
		return _ISteamNetworkingUtils_AllocateMessage(instancePtr, cbAllocateBuffer);
	}

	internal static void ISteamNetworkingUtils_InitRelayNetworkAccess(IntPtr instancePtr)
	{
		if ((nint)_ISteamNetworkingUtils_InitRelayNetworkAccess == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingUtils_InitRelayNetworkAccess' is not available.");
		_ISteamNetworkingUtils_InitRelayNetworkAccess(instancePtr);
	}

	internal static ESteamNetworkingAvailability ISteamNetworkingUtils_GetRelayNetworkStatus(IntPtr instancePtr, out SteamRelayNetworkStatus_t pDetails)
	{
		if ((nint)_ISteamNetworkingUtils_GetRelayNetworkStatus == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingUtils_GetRelayNetworkStatus' is not available.");
		pDetails = default;
		ESteamNetworkingAvailability ret;
		fixed (SteamRelayNetworkStatus_t* pDetailsPtr = &pDetails)
		{
			ret = _ISteamNetworkingUtils_GetRelayNetworkStatus(instancePtr, pDetailsPtr);
		}
		return ret;
	}

	internal static float ISteamNetworkingUtils_GetLocalPingLocation(IntPtr instancePtr, out SteamNetworkPingLocation_t result)
	{
		if ((nint)_ISteamNetworkingUtils_GetLocalPingLocation == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingUtils_GetLocalPingLocation' is not available.");
		result = default;
		float ret;
		fixed (SteamNetworkPingLocation_t* resultPtr = &result)
		{
			ret = _ISteamNetworkingUtils_GetLocalPingLocation(instancePtr, resultPtr);
		}
		return ret;
	}

	internal static int ISteamNetworkingUtils_EstimatePingTimeBetweenTwoLocations(IntPtr instancePtr, ref SteamNetworkPingLocation_t location1, ref SteamNetworkPingLocation_t location2)
	{
		if ((nint)_ISteamNetworkingUtils_EstimatePingTimeBetweenTwoLocations == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingUtils_EstimatePingTimeBetweenTwoLocations' is not available.");
		int ret;
		fixed (SteamNetworkPingLocation_t* location1Ptr = &location1)
		{
			fixed (SteamNetworkPingLocation_t* location2Ptr = &location2)
			{
				ret = _ISteamNetworkingUtils_EstimatePingTimeBetweenTwoLocations(instancePtr, location1Ptr, location2Ptr);
			}
		}
		return ret;
	}

	internal static int ISteamNetworkingUtils_EstimatePingTimeFromLocalHost(IntPtr instancePtr, ref SteamNetworkPingLocation_t remoteLocation)
	{
		if ((nint)_ISteamNetworkingUtils_EstimatePingTimeFromLocalHost == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingUtils_EstimatePingTimeFromLocalHost' is not available.");
		int ret;
		fixed (SteamNetworkPingLocation_t* remoteLocationPtr = &remoteLocation)
		{
			ret = _ISteamNetworkingUtils_EstimatePingTimeFromLocalHost(instancePtr, remoteLocationPtr);
		}
		return ret;
	}

	internal static void ISteamNetworkingUtils_ConvertPingLocationToString(IntPtr instancePtr, ref SteamNetworkPingLocation_t location, IntPtr pszBuf, int cchBufSize)
	{
		if ((nint)_ISteamNetworkingUtils_ConvertPingLocationToString == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingUtils_ConvertPingLocationToString' is not available.");
		fixed (SteamNetworkPingLocation_t* locationPtr = &location)
		{
			_ISteamNetworkingUtils_ConvertPingLocationToString(instancePtr, locationPtr, pszBuf, cchBufSize);
		}
	}

	internal static bool ISteamNetworkingUtils_ParsePingLocationString(IntPtr instancePtr, string? pszString, out SteamNetworkPingLocation_t result)
	{
		if ((nint)_ISteamNetworkingUtils_ParsePingLocationString == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingUtils_ParsePingLocationString' is not available.");
		using var pszStringStr = new ScopedCString(pszString ?? string.Empty);
		result = default;
		byte ret;
		fixed (byte* pszStringBufferPtr = pszStringStr)
		{
			fixed (SteamNetworkPingLocation_t* resultPtr = &result)
			{
				ret = _ISteamNetworkingUtils_ParsePingLocationString(instancePtr, pszString is null ? null : pszStringBufferPtr, resultPtr);
			}
		}
		return ret != 0;
	}

	internal static bool ISteamNetworkingUtils_CheckPingDataUpToDate(IntPtr instancePtr, float flMaxAgeSeconds)
	{
		if ((nint)_ISteamNetworkingUtils_CheckPingDataUpToDate == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingUtils_CheckPingDataUpToDate' is not available.");
		return _ISteamNetworkingUtils_CheckPingDataUpToDate(instancePtr, flMaxAgeSeconds) != 0;
	}

	internal static int ISteamNetworkingUtils_GetPingToDataCenter(IntPtr instancePtr, SteamNetworkingPOPID popID, out SteamNetworkingPOPID pViaRelayPoP)
	{
		if ((nint)_ISteamNetworkingUtils_GetPingToDataCenter == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingUtils_GetPingToDataCenter' is not available.");
		pViaRelayPoP = default;
		int ret;
		fixed (SteamNetworkingPOPID* pViaRelayPoPPtr = &pViaRelayPoP)
		{
			ret = _ISteamNetworkingUtils_GetPingToDataCenter(instancePtr, popID, pViaRelayPoPPtr);
		}
		return ret;
	}

	internal static int ISteamNetworkingUtils_GetDirectPingToPOP(IntPtr instancePtr, SteamNetworkingPOPID popID)
	{
		if ((nint)_ISteamNetworkingUtils_GetDirectPingToPOP == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingUtils_GetDirectPingToPOP' is not available.");
		return _ISteamNetworkingUtils_GetDirectPingToPOP(instancePtr, popID);
	}

	internal static int ISteamNetworkingUtils_GetPOPCount(IntPtr instancePtr)
	{
		if ((nint)_ISteamNetworkingUtils_GetPOPCount == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingUtils_GetPOPCount' is not available.");
		return _ISteamNetworkingUtils_GetPOPCount(instancePtr);
	}

	internal static int ISteamNetworkingUtils_GetPOPList(IntPtr instancePtr, out SteamNetworkingPOPID list, int nListSz)
	{
		if ((nint)_ISteamNetworkingUtils_GetPOPList == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingUtils_GetPOPList' is not available.");
		list = default;
		int ret;
		fixed (SteamNetworkingPOPID* listPtr = &list)
		{
			ret = _ISteamNetworkingUtils_GetPOPList(instancePtr, listPtr, nListSz);
		}
		return ret;
	}

	internal static long ISteamNetworkingUtils_GetLocalTimestamp(IntPtr instancePtr)
	{
		if ((nint)_ISteamNetworkingUtils_GetLocalTimestamp == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingUtils_GetLocalTimestamp' is not available.");
		return _ISteamNetworkingUtils_GetLocalTimestamp(instancePtr);
	}

	internal static void ISteamNetworkingUtils_SetDebugOutputFunction(IntPtr instancePtr, ESteamNetworkingSocketsDebugOutputType eDetailLevel, FSteamNetworkingSocketsDebugOutput pfnFunc)
	{
		if ((nint)_ISteamNetworkingUtils_SetDebugOutputFunction == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingUtils_SetDebugOutputFunction' is not available.");
		_ISteamNetworkingUtils_SetDebugOutputFunction(instancePtr, eDetailLevel, pfnFunc is null ? 0 : Marshal.GetFunctionPointerForDelegate(pfnFunc));
	}

	internal static bool ISteamNetworkingUtils_IsFakeIPv4(IntPtr instancePtr, uint nIPv4)
	{
		if ((nint)_ISteamNetworkingUtils_IsFakeIPv4 == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingUtils_IsFakeIPv4' is not available.");
		return _ISteamNetworkingUtils_IsFakeIPv4(instancePtr, nIPv4) != 0;
	}

	internal static ESteamNetworkingFakeIPType ISteamNetworkingUtils_GetIPv4FakeIPType(IntPtr instancePtr, uint nIPv4)
	{
		if ((nint)_ISteamNetworkingUtils_GetIPv4FakeIPType == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingUtils_GetIPv4FakeIPType' is not available.");
		return _ISteamNetworkingUtils_GetIPv4FakeIPType(instancePtr, nIPv4);
	}

	internal static EResult ISteamNetworkingUtils_GetRealIdentityForFakeIP(IntPtr instancePtr, ref SteamNetworkingIPAddr fakeIP, out SteamNetworkingIdentity pOutRealIdentity)
	{
		if ((nint)_ISteamNetworkingUtils_GetRealIdentityForFakeIP == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingUtils_GetRealIdentityForFakeIP' is not available.");
		pOutRealIdentity = default;
		EResult ret;
		fixed (SteamNetworkingIPAddr* fakeIPPtr = &fakeIP)
		{
			fixed (SteamNetworkingIdentity* pOutRealIdentityPtr = &pOutRealIdentity)
			{
				ret = _ISteamNetworkingUtils_GetRealIdentityForFakeIP(instancePtr, fakeIPPtr, pOutRealIdentityPtr);
			}
		}
		return ret;
	}

	internal static bool ISteamNetworkingUtils_SetConfigValue(IntPtr instancePtr, ESteamNetworkingConfigValue eValue, ESteamNetworkingConfigScope eScopeType, IntPtr scopeObj, ESteamNetworkingConfigDataType eDataType, IntPtr pArg)
	{
		if ((nint)_ISteamNetworkingUtils_SetConfigValue == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingUtils_SetConfigValue' is not available.");
		return _ISteamNetworkingUtils_SetConfigValue(instancePtr, eValue, eScopeType, scopeObj, eDataType, pArg) != 0;
	}

	internal static ESteamNetworkingGetConfigValueResult ISteamNetworkingUtils_GetConfigValue(IntPtr instancePtr, ESteamNetworkingConfigValue eValue, ESteamNetworkingConfigScope eScopeType, IntPtr scopeObj, out ESteamNetworkingConfigDataType pOutDataType, IntPtr pResult, ref ulong cbResult)
	{
		if ((nint)_ISteamNetworkingUtils_GetConfigValue == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingUtils_GetConfigValue' is not available.");
		pOutDataType = default;
		ESteamNetworkingGetConfigValueResult ret;
		fixed (ESteamNetworkingConfigDataType* pOutDataTypePtr = &pOutDataType)
		{
			fixed (ulong* cbResultPtr = &cbResult)
			{
				ret = _ISteamNetworkingUtils_GetConfigValue(instancePtr, eValue, eScopeType, scopeObj, pOutDataTypePtr, pResult, cbResultPtr);
			}
		}
		return ret;
	}

	internal static IntPtr ISteamNetworkingUtils_GetConfigValueInfo(IntPtr instancePtr, ESteamNetworkingConfigValue eValue, out ESteamNetworkingConfigDataType pOutDataType, out ESteamNetworkingConfigScope pOutScope)
	{
		if ((nint)_ISteamNetworkingUtils_GetConfigValueInfo == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingUtils_GetConfigValueInfo' is not available.");
		pOutDataType = default;
		pOutScope = default;
		nint ret;
		fixed (ESteamNetworkingConfigDataType* pOutDataTypePtr = &pOutDataType)
		{
			fixed (ESteamNetworkingConfigScope* pOutScopePtr = &pOutScope)
			{
				ret = _ISteamNetworkingUtils_GetConfigValueInfo(instancePtr, eValue, pOutDataTypePtr, pOutScopePtr);
			}
		}
		return ret;
	}

	internal static ESteamNetworkingConfigValue ISteamNetworkingUtils_IterateGenericEditableConfigValues(IntPtr instancePtr, ESteamNetworkingConfigValue eCurrent, bool bEnumerateDevVars)
	{
		if ((nint)_ISteamNetworkingUtils_IterateGenericEditableConfigValues == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingUtils_IterateGenericEditableConfigValues' is not available.");
		return _ISteamNetworkingUtils_IterateGenericEditableConfigValues(instancePtr, eCurrent, (byte)(bEnumerateDevVars ? 1 : 0));
	}

	internal static void ISteamNetworkingUtils_SteamNetworkingIPAddr_ToString(IntPtr instancePtr, ref SteamNetworkingIPAddr addr, IntPtr buf, uint cbBuf, bool bWithPort)
	{
		if ((nint)_ISteamNetworkingUtils_SteamNetworkingIPAddr_ToString == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingUtils_SteamNetworkingIPAddr_ToString' is not available.");
		fixed (SteamNetworkingIPAddr* addrPtr = &addr)
		{
			_ISteamNetworkingUtils_SteamNetworkingIPAddr_ToString(instancePtr, addrPtr, buf, cbBuf, (byte)(bWithPort ? 1 : 0));
		}
	}

	internal static bool ISteamNetworkingUtils_SteamNetworkingIPAddr_ParseString(IntPtr instancePtr, out SteamNetworkingIPAddr pAddr, string? pszStr)
	{
		if ((nint)_ISteamNetworkingUtils_SteamNetworkingIPAddr_ParseString == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingUtils_SteamNetworkingIPAddr_ParseString' is not available.");
		pAddr = default;
		using var pszStrStr = new ScopedCString(pszStr ?? string.Empty);
		byte ret;
		fixed (SteamNetworkingIPAddr* pAddrPtr = &pAddr)
		{
			fixed (byte* pszStrBufferPtr = pszStrStr)
			{
				ret = _ISteamNetworkingUtils_SteamNetworkingIPAddr_ParseString(instancePtr, pAddrPtr, pszStr is null ? null : pszStrBufferPtr);
			}
		}
		return ret != 0;
	}

	internal static ESteamNetworkingFakeIPType ISteamNetworkingUtils_SteamNetworkingIPAddr_GetFakeIPType(IntPtr instancePtr, ref SteamNetworkingIPAddr addr)
	{
		if ((nint)_ISteamNetworkingUtils_SteamNetworkingIPAddr_GetFakeIPType == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingUtils_SteamNetworkingIPAddr_GetFakeIPType' is not available.");
		ESteamNetworkingFakeIPType ret;
		fixed (SteamNetworkingIPAddr* addrPtr = &addr)
		{
			ret = _ISteamNetworkingUtils_SteamNetworkingIPAddr_GetFakeIPType(instancePtr, addrPtr);
		}
		return ret;
	}

	internal static void ISteamNetworkingUtils_SteamNetworkingIdentity_ToString(IntPtr instancePtr, ref SteamNetworkingIdentity identity, IntPtr buf, uint cbBuf)
	{
		if ((nint)_ISteamNetworkingUtils_SteamNetworkingIdentity_ToString == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingUtils_SteamNetworkingIdentity_ToString' is not available.");
		fixed (SteamNetworkingIdentity* identityPtr = &identity)
		{
			_ISteamNetworkingUtils_SteamNetworkingIdentity_ToString(instancePtr, identityPtr, buf, cbBuf);
		}
	}

	internal static bool ISteamNetworkingUtils_SteamNetworkingIdentity_ParseString(IntPtr instancePtr, out SteamNetworkingIdentity pIdentity, string? pszStr)
	{
		if ((nint)_ISteamNetworkingUtils_SteamNetworkingIdentity_ParseString == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamNetworkingUtils_SteamNetworkingIdentity_ParseString' is not available.");
		pIdentity = default;
		using var pszStrStr = new ScopedCString(pszStr ?? string.Empty);
		byte ret;
		fixed (SteamNetworkingIdentity* pIdentityPtr = &pIdentity)
		{
			fixed (byte* pszStrBufferPtr = pszStrStr)
			{
				ret = _ISteamNetworkingUtils_SteamNetworkingIdentity_ParseString(instancePtr, pIdentityPtr, pszStr is null ? null : pszStrBufferPtr);
			}
		}
		return ret != 0;
	}

	internal static ulong ISteamUGC_CreateQueryUserUGCRequest(IntPtr instancePtr, AccountID_t unAccountID, EUserUGCList eListType, EUGCMatchingUGCType eMatchingUGCType, EUserUGCListSortOrder eSortOrder, AppId_t nCreatorAppID, AppId_t nConsumerAppID, uint unPage)
	{
		if ((nint)_ISteamUGC_CreateQueryUserUGCRequest == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_CreateQueryUserUGCRequest' is not available.");
		return _ISteamUGC_CreateQueryUserUGCRequest(instancePtr, unAccountID, eListType, eMatchingUGCType, eSortOrder, nCreatorAppID, nConsumerAppID, unPage);
	}

	internal static ulong ISteamUGC_CreateQueryAllUGCRequestPage(IntPtr instancePtr, EUGCQuery eQueryType, EUGCMatchingUGCType eMatchingeMatchingUGCTypeFileType, AppId_t nCreatorAppID, AppId_t nConsumerAppID, uint unPage)
	{
		if ((nint)_ISteamUGC_CreateQueryAllUGCRequestPage == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_CreateQueryAllUGCRequestPage' is not available.");
		return _ISteamUGC_CreateQueryAllUGCRequestPage(instancePtr, eQueryType, eMatchingeMatchingUGCTypeFileType, nCreatorAppID, nConsumerAppID, unPage);
	}

	internal static ulong ISteamUGC_CreateQueryAllUGCRequestCursor(IntPtr instancePtr, EUGCQuery eQueryType, EUGCMatchingUGCType eMatchingeMatchingUGCTypeFileType, AppId_t nCreatorAppID, AppId_t nConsumerAppID, string? pchCursor)
	{
		if ((nint)_ISteamUGC_CreateQueryAllUGCRequestCursor == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_CreateQueryAllUGCRequestCursor' is not available.");
		using var pchCursorStr = new ScopedCString(pchCursor ?? string.Empty);
		ulong ret;
		fixed (byte* pchCursorBufferPtr = pchCursorStr)
		{
			ret = _ISteamUGC_CreateQueryAllUGCRequestCursor(instancePtr, eQueryType, eMatchingeMatchingUGCTypeFileType, nCreatorAppID, nConsumerAppID, pchCursor is null ? null : pchCursorBufferPtr);
		}
		return ret;
	}

	internal static ulong ISteamUGC_CreateQueryUGCDetailsRequest(IntPtr instancePtr, PublishedFileId_t[]? pvecPublishedFileID, uint unNumPublishedFileIDs)
	{
		if ((nint)_ISteamUGC_CreateQueryUGCDetailsRequest == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_CreateQueryUGCDetailsRequest' is not available.");
		ulong ret;
		fixed (PublishedFileId_t* pvecPublishedFileIDPtr = pvecPublishedFileID)
		{
			ret = _ISteamUGC_CreateQueryUGCDetailsRequest(instancePtr, pvecPublishedFileIDPtr, unNumPublishedFileIDs);
		}
		return ret;
	}

	internal static ulong ISteamUGC_SendQueryUGCRequest(IntPtr instancePtr, UGCQueryHandle_t handle)
	{
		if ((nint)_ISteamUGC_SendQueryUGCRequest == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_SendQueryUGCRequest' is not available.");
		return _ISteamUGC_SendQueryUGCRequest(instancePtr, handle);
	}

	internal static bool ISteamUGC_GetQueryUGCResult(IntPtr instancePtr, UGCQueryHandle_t handle, uint index, out SteamUGCDetails_t pDetails)
	{
		if ((nint)_ISteamUGC_GetQueryUGCResult == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_GetQueryUGCResult' is not available.");
		pDetails = default;
		byte ret;
		fixed (SteamUGCDetails_t* pDetailsPtr = &pDetails)
		{
			ret = _ISteamUGC_GetQueryUGCResult(instancePtr, handle, index, pDetailsPtr);
		}
		return ret != 0;
	}

	internal static uint ISteamUGC_GetQueryUGCNumTags(IntPtr instancePtr, UGCQueryHandle_t handle, uint index)
	{
		if ((nint)_ISteamUGC_GetQueryUGCNumTags == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_GetQueryUGCNumTags' is not available.");
		return _ISteamUGC_GetQueryUGCNumTags(instancePtr, handle, index);
	}

	internal static bool ISteamUGC_GetQueryUGCTag(IntPtr instancePtr, UGCQueryHandle_t handle, uint index, uint indexTag, IntPtr pchValue, uint cchValueSize)
	{
		if ((nint)_ISteamUGC_GetQueryUGCTag == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_GetQueryUGCTag' is not available.");
		return _ISteamUGC_GetQueryUGCTag(instancePtr, handle, index, indexTag, pchValue, cchValueSize) != 0;
	}

	internal static bool ISteamUGC_GetQueryUGCTagDisplayName(IntPtr instancePtr, UGCQueryHandle_t handle, uint index, uint indexTag, IntPtr pchValue, uint cchValueSize)
	{
		if ((nint)_ISteamUGC_GetQueryUGCTagDisplayName == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_GetQueryUGCTagDisplayName' is not available.");
		return _ISteamUGC_GetQueryUGCTagDisplayName(instancePtr, handle, index, indexTag, pchValue, cchValueSize) != 0;
	}

	internal static bool ISteamUGC_GetQueryUGCPreviewURL(IntPtr instancePtr, UGCQueryHandle_t handle, uint index, IntPtr pchURL, uint cchURLSize)
	{
		if ((nint)_ISteamUGC_GetQueryUGCPreviewURL == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_GetQueryUGCPreviewURL' is not available.");
		return _ISteamUGC_GetQueryUGCPreviewURL(instancePtr, handle, index, pchURL, cchURLSize) != 0;
	}

	internal static bool ISteamUGC_GetQueryUGCMetadata(IntPtr instancePtr, UGCQueryHandle_t handle, uint index, IntPtr pchMetadata, uint cchMetadatasize)
	{
		if ((nint)_ISteamUGC_GetQueryUGCMetadata == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_GetQueryUGCMetadata' is not available.");
		return _ISteamUGC_GetQueryUGCMetadata(instancePtr, handle, index, pchMetadata, cchMetadatasize) != 0;
	}

	internal static bool ISteamUGC_GetQueryUGCChildren(IntPtr instancePtr, UGCQueryHandle_t handle, uint index, PublishedFileId_t[]? pvecPublishedFileID, uint cMaxEntries)
	{
		if ((nint)_ISteamUGC_GetQueryUGCChildren == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_GetQueryUGCChildren' is not available.");
		byte ret;
		fixed (PublishedFileId_t* pvecPublishedFileIDPtr = pvecPublishedFileID)
		{
			ret = _ISteamUGC_GetQueryUGCChildren(instancePtr, handle, index, pvecPublishedFileIDPtr, cMaxEntries);
		}
		return ret != 0;
	}

	internal static bool ISteamUGC_GetQueryUGCStatistic(IntPtr instancePtr, UGCQueryHandle_t handle, uint index, EItemStatistic eStatType, out ulong pStatValue)
	{
		if ((nint)_ISteamUGC_GetQueryUGCStatistic == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_GetQueryUGCStatistic' is not available.");
		pStatValue = default;
		byte ret;
		fixed (ulong* pStatValuePtr = &pStatValue)
		{
			ret = _ISteamUGC_GetQueryUGCStatistic(instancePtr, handle, index, eStatType, pStatValuePtr);
		}
		return ret != 0;
	}

	internal static uint ISteamUGC_GetQueryUGCNumAdditionalPreviews(IntPtr instancePtr, UGCQueryHandle_t handle, uint index)
	{
		if ((nint)_ISteamUGC_GetQueryUGCNumAdditionalPreviews == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_GetQueryUGCNumAdditionalPreviews' is not available.");
		return _ISteamUGC_GetQueryUGCNumAdditionalPreviews(instancePtr, handle, index);
	}

	internal static bool ISteamUGC_GetQueryUGCAdditionalPreview(IntPtr instancePtr, UGCQueryHandle_t handle, uint index, uint previewIndex, IntPtr pchURLOrVideoID, uint cchURLSize, IntPtr pchOriginalFileName, uint cchOriginalFileNameSize, out EItemPreviewType pPreviewType)
	{
		if ((nint)_ISteamUGC_GetQueryUGCAdditionalPreview == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_GetQueryUGCAdditionalPreview' is not available.");
		pPreviewType = default;
		byte ret;
		fixed (EItemPreviewType* pPreviewTypePtr = &pPreviewType)
		{
			ret = _ISteamUGC_GetQueryUGCAdditionalPreview(instancePtr, handle, index, previewIndex, pchURLOrVideoID, cchURLSize, pchOriginalFileName, cchOriginalFileNameSize, pPreviewTypePtr);
		}
		return ret != 0;
	}

	internal static uint ISteamUGC_GetQueryUGCNumKeyValueTags(IntPtr instancePtr, UGCQueryHandle_t handle, uint index)
	{
		if ((nint)_ISteamUGC_GetQueryUGCNumKeyValueTags == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_GetQueryUGCNumKeyValueTags' is not available.");
		return _ISteamUGC_GetQueryUGCNumKeyValueTags(instancePtr, handle, index);
	}

	internal static bool ISteamUGC_GetQueryUGCKeyValueTag(IntPtr instancePtr, UGCQueryHandle_t handle, uint index, uint keyValueTagIndex, IntPtr pchKey, uint cchKeySize, IntPtr pchValue, uint cchValueSize)
	{
		if ((nint)_ISteamUGC_GetQueryUGCKeyValueTag == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_GetQueryUGCKeyValueTag' is not available.");
		return _ISteamUGC_GetQueryUGCKeyValueTag(instancePtr, handle, index, keyValueTagIndex, pchKey, cchKeySize, pchValue, cchValueSize) != 0;
	}

	internal static bool ISteamUGC_GetQueryFirstUGCKeyValueTag(IntPtr instancePtr, UGCQueryHandle_t handle, uint index, string? pchKey, IntPtr pchValue, uint cchValueSize)
	{
		if ((nint)_ISteamUGC_GetQueryFirstUGCKeyValueTag == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_GetQueryFirstUGCKeyValueTag' is not available.");
		using var pchKeyStr = new ScopedCString(pchKey ?? string.Empty);
		byte ret;
		fixed (byte* pchKeyBufferPtr = pchKeyStr)
		{
			ret = _ISteamUGC_GetQueryFirstUGCKeyValueTag(instancePtr, handle, index, pchKey is null ? null : pchKeyBufferPtr, pchValue, cchValueSize);
		}
		return ret != 0;
	}

	internal static uint ISteamUGC_GetNumSupportedGameVersions(IntPtr instancePtr, UGCQueryHandle_t handle, uint index)
	{
		if ((nint)_ISteamUGC_GetNumSupportedGameVersions == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_GetNumSupportedGameVersions' is not available.");
		return _ISteamUGC_GetNumSupportedGameVersions(instancePtr, handle, index);
	}

	internal static bool ISteamUGC_GetSupportedGameVersionData(IntPtr instancePtr, UGCQueryHandle_t handle, uint index, uint versionIndex, IntPtr pchGameBranchMin, IntPtr pchGameBranchMax, uint cchGameBranchSize)
	{
		if ((nint)_ISteamUGC_GetSupportedGameVersionData == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_GetSupportedGameVersionData' is not available.");
		return _ISteamUGC_GetSupportedGameVersionData(instancePtr, handle, index, versionIndex, pchGameBranchMin, pchGameBranchMax, cchGameBranchSize) != 0;
	}

	internal static uint ISteamUGC_GetQueryUGCContentDescriptors(IntPtr instancePtr, UGCQueryHandle_t handle, uint index, out EUGCContentDescriptorID pvecDescriptors, uint cMaxEntries)
	{
		if ((nint)_ISteamUGC_GetQueryUGCContentDescriptors == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_GetQueryUGCContentDescriptors' is not available.");
		pvecDescriptors = default;
		uint ret;
		fixed (EUGCContentDescriptorID* pvecDescriptorsPtr = &pvecDescriptors)
		{
			ret = _ISteamUGC_GetQueryUGCContentDescriptors(instancePtr, handle, index, pvecDescriptorsPtr, cMaxEntries);
		}
		return ret;
	}

	internal static bool ISteamUGC_ReleaseQueryUGCRequest(IntPtr instancePtr, UGCQueryHandle_t handle)
	{
		if ((nint)_ISteamUGC_ReleaseQueryUGCRequest == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_ReleaseQueryUGCRequest' is not available.");
		return _ISteamUGC_ReleaseQueryUGCRequest(instancePtr, handle) != 0;
	}

	internal static bool ISteamUGC_AddRequiredTag(IntPtr instancePtr, UGCQueryHandle_t handle, string? pTagName)
	{
		if ((nint)_ISteamUGC_AddRequiredTag == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_AddRequiredTag' is not available.");
		using var pTagNameStr = new ScopedCString(pTagName ?? string.Empty);
		byte ret;
		fixed (byte* pTagNameBufferPtr = pTagNameStr)
		{
			ret = _ISteamUGC_AddRequiredTag(instancePtr, handle, pTagName is null ? null : pTagNameBufferPtr);
		}
		return ret != 0;
	}

	internal static bool ISteamUGC_AddRequiredTagGroup(IntPtr instancePtr, UGCQueryHandle_t handle, IntPtr pTagGroups)
	{
		if ((nint)_ISteamUGC_AddRequiredTagGroup == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_AddRequiredTagGroup' is not available.");
		return _ISteamUGC_AddRequiredTagGroup(instancePtr, handle, pTagGroups) != 0;
	}

	internal static bool ISteamUGC_AddExcludedTag(IntPtr instancePtr, UGCQueryHandle_t handle, string? pTagName)
	{
		if ((nint)_ISteamUGC_AddExcludedTag == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_AddExcludedTag' is not available.");
		using var pTagNameStr = new ScopedCString(pTagName ?? string.Empty);
		byte ret;
		fixed (byte* pTagNameBufferPtr = pTagNameStr)
		{
			ret = _ISteamUGC_AddExcludedTag(instancePtr, handle, pTagName is null ? null : pTagNameBufferPtr);
		}
		return ret != 0;
	}

	internal static bool ISteamUGC_SetReturnOnlyIDs(IntPtr instancePtr, UGCQueryHandle_t handle, bool bReturnOnlyIDs)
	{
		if ((nint)_ISteamUGC_SetReturnOnlyIDs == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_SetReturnOnlyIDs' is not available.");
		return _ISteamUGC_SetReturnOnlyIDs(instancePtr, handle, (byte)(bReturnOnlyIDs ? 1 : 0)) != 0;
	}

	internal static bool ISteamUGC_SetReturnKeyValueTags(IntPtr instancePtr, UGCQueryHandle_t handle, bool bReturnKeyValueTags)
	{
		if ((nint)_ISteamUGC_SetReturnKeyValueTags == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_SetReturnKeyValueTags' is not available.");
		return _ISteamUGC_SetReturnKeyValueTags(instancePtr, handle, (byte)(bReturnKeyValueTags ? 1 : 0)) != 0;
	}

	internal static bool ISteamUGC_SetReturnLongDescription(IntPtr instancePtr, UGCQueryHandle_t handle, bool bReturnLongDescription)
	{
		if ((nint)_ISteamUGC_SetReturnLongDescription == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_SetReturnLongDescription' is not available.");
		return _ISteamUGC_SetReturnLongDescription(instancePtr, handle, (byte)(bReturnLongDescription ? 1 : 0)) != 0;
	}

	internal static bool ISteamUGC_SetReturnMetadata(IntPtr instancePtr, UGCQueryHandle_t handle, bool bReturnMetadata)
	{
		if ((nint)_ISteamUGC_SetReturnMetadata == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_SetReturnMetadata' is not available.");
		return _ISteamUGC_SetReturnMetadata(instancePtr, handle, (byte)(bReturnMetadata ? 1 : 0)) != 0;
	}

	internal static bool ISteamUGC_SetReturnChildren(IntPtr instancePtr, UGCQueryHandle_t handle, bool bReturnChildren)
	{
		if ((nint)_ISteamUGC_SetReturnChildren == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_SetReturnChildren' is not available.");
		return _ISteamUGC_SetReturnChildren(instancePtr, handle, (byte)(bReturnChildren ? 1 : 0)) != 0;
	}

	internal static bool ISteamUGC_SetReturnAdditionalPreviews(IntPtr instancePtr, UGCQueryHandle_t handle, bool bReturnAdditionalPreviews)
	{
		if ((nint)_ISteamUGC_SetReturnAdditionalPreviews == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_SetReturnAdditionalPreviews' is not available.");
		return _ISteamUGC_SetReturnAdditionalPreviews(instancePtr, handle, (byte)(bReturnAdditionalPreviews ? 1 : 0)) != 0;
	}

	internal static bool ISteamUGC_SetReturnTotalOnly(IntPtr instancePtr, UGCQueryHandle_t handle, bool bReturnTotalOnly)
	{
		if ((nint)_ISteamUGC_SetReturnTotalOnly == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_SetReturnTotalOnly' is not available.");
		return _ISteamUGC_SetReturnTotalOnly(instancePtr, handle, (byte)(bReturnTotalOnly ? 1 : 0)) != 0;
	}

	internal static bool ISteamUGC_SetReturnPlaytimeStats(IntPtr instancePtr, UGCQueryHandle_t handle, uint unDays)
	{
		if ((nint)_ISteamUGC_SetReturnPlaytimeStats == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_SetReturnPlaytimeStats' is not available.");
		return _ISteamUGC_SetReturnPlaytimeStats(instancePtr, handle, unDays) != 0;
	}

	internal static bool ISteamUGC_SetLanguage(IntPtr instancePtr, UGCQueryHandle_t handle, string? pchLanguage)
	{
		if ((nint)_ISteamUGC_SetLanguage == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_SetLanguage' is not available.");
		using var pchLanguageStr = new ScopedCString(pchLanguage ?? string.Empty);
		byte ret;
		fixed (byte* pchLanguageBufferPtr = pchLanguageStr)
		{
			ret = _ISteamUGC_SetLanguage(instancePtr, handle, pchLanguage is null ? null : pchLanguageBufferPtr);
		}
		return ret != 0;
	}

	internal static bool ISteamUGC_SetAllowCachedResponse(IntPtr instancePtr, UGCQueryHandle_t handle, uint unMaxAgeSeconds)
	{
		if ((nint)_ISteamUGC_SetAllowCachedResponse == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_SetAllowCachedResponse' is not available.");
		return _ISteamUGC_SetAllowCachedResponse(instancePtr, handle, unMaxAgeSeconds) != 0;
	}

	internal static bool ISteamUGC_SetAdminQuery(IntPtr instancePtr, UGCUpdateHandle_t handle, bool bAdminQuery)
	{
		if ((nint)_ISteamUGC_SetAdminQuery == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_SetAdminQuery' is not available.");
		return _ISteamUGC_SetAdminQuery(instancePtr, handle, (byte)(bAdminQuery ? 1 : 0)) != 0;
	}

	internal static bool ISteamUGC_SetCloudFileNameFilter(IntPtr instancePtr, UGCQueryHandle_t handle, string? pMatchCloudFileName)
	{
		if ((nint)_ISteamUGC_SetCloudFileNameFilter == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_SetCloudFileNameFilter' is not available.");
		using var pMatchCloudFileNameStr = new ScopedCString(pMatchCloudFileName ?? string.Empty);
		byte ret;
		fixed (byte* pMatchCloudFileNameBufferPtr = pMatchCloudFileNameStr)
		{
			ret = _ISteamUGC_SetCloudFileNameFilter(instancePtr, handle, pMatchCloudFileName is null ? null : pMatchCloudFileNameBufferPtr);
		}
		return ret != 0;
	}

	internal static bool ISteamUGC_SetMatchAnyTag(IntPtr instancePtr, UGCQueryHandle_t handle, bool bMatchAnyTag)
	{
		if ((nint)_ISteamUGC_SetMatchAnyTag == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_SetMatchAnyTag' is not available.");
		return _ISteamUGC_SetMatchAnyTag(instancePtr, handle, (byte)(bMatchAnyTag ? 1 : 0)) != 0;
	}

	internal static bool ISteamUGC_SetSearchText(IntPtr instancePtr, UGCQueryHandle_t handle, string? pSearchText)
	{
		if ((nint)_ISteamUGC_SetSearchText == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_SetSearchText' is not available.");
		using var pSearchTextStr = new ScopedCString(pSearchText ?? string.Empty);
		byte ret;
		fixed (byte* pSearchTextBufferPtr = pSearchTextStr)
		{
			ret = _ISteamUGC_SetSearchText(instancePtr, handle, pSearchText is null ? null : pSearchTextBufferPtr);
		}
		return ret != 0;
	}

	internal static bool ISteamUGC_SetRankedByTrendDays(IntPtr instancePtr, UGCQueryHandle_t handle, uint unDays)
	{
		if ((nint)_ISteamUGC_SetRankedByTrendDays == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_SetRankedByTrendDays' is not available.");
		return _ISteamUGC_SetRankedByTrendDays(instancePtr, handle, unDays) != 0;
	}

	internal static bool ISteamUGC_SetTimeCreatedDateRange(IntPtr instancePtr, UGCQueryHandle_t handle, uint rtStart, uint rtEnd)
	{
		if ((nint)_ISteamUGC_SetTimeCreatedDateRange == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_SetTimeCreatedDateRange' is not available.");
		return _ISteamUGC_SetTimeCreatedDateRange(instancePtr, handle, rtStart, rtEnd) != 0;
	}

	internal static bool ISteamUGC_SetTimeUpdatedDateRange(IntPtr instancePtr, UGCQueryHandle_t handle, uint rtStart, uint rtEnd)
	{
		if ((nint)_ISteamUGC_SetTimeUpdatedDateRange == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_SetTimeUpdatedDateRange' is not available.");
		return _ISteamUGC_SetTimeUpdatedDateRange(instancePtr, handle, rtStart, rtEnd) != 0;
	}

	internal static bool ISteamUGC_AddRequiredKeyValueTag(IntPtr instancePtr, UGCQueryHandle_t handle, string? pKey, string? pValue)
	{
		if ((nint)_ISteamUGC_AddRequiredKeyValueTag == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_AddRequiredKeyValueTag' is not available.");
		using var pKeyStr = new ScopedCString(pKey ?? string.Empty);
		using var pValueStr = new ScopedCString(pValue ?? string.Empty);
		byte ret;
		fixed (byte* pKeyBufferPtr = pKeyStr)
		{
			fixed (byte* pValueBufferPtr = pValueStr)
			{
				ret = _ISteamUGC_AddRequiredKeyValueTag(instancePtr, handle, pKey is null ? null : pKeyBufferPtr, pValue is null ? null : pValueBufferPtr);
			}
		}
		return ret != 0;
	}

	internal static ulong ISteamUGC_RequestUGCDetails(IntPtr instancePtr, PublishedFileId_t nPublishedFileID, uint unMaxAgeSeconds)
	{
		if ((nint)_ISteamUGC_RequestUGCDetails == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_RequestUGCDetails' is not available.");
		return _ISteamUGC_RequestUGCDetails(instancePtr, nPublishedFileID, unMaxAgeSeconds);
	}

	internal static ulong ISteamUGC_CreateItem(IntPtr instancePtr, AppId_t nConsumerAppId, EWorkshopFileType eFileType)
	{
		if ((nint)_ISteamUGC_CreateItem == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_CreateItem' is not available.");
		return _ISteamUGC_CreateItem(instancePtr, nConsumerAppId, eFileType);
	}

	internal static ulong ISteamUGC_StartItemUpdate(IntPtr instancePtr, AppId_t nConsumerAppId, PublishedFileId_t nPublishedFileID)
	{
		if ((nint)_ISteamUGC_StartItemUpdate == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_StartItemUpdate' is not available.");
		return _ISteamUGC_StartItemUpdate(instancePtr, nConsumerAppId, nPublishedFileID);
	}

	internal static bool ISteamUGC_SetItemTitle(IntPtr instancePtr, UGCUpdateHandle_t handle, string? pchTitle)
	{
		if ((nint)_ISteamUGC_SetItemTitle == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_SetItemTitle' is not available.");
		using var pchTitleStr = new ScopedCString(pchTitle ?? string.Empty);
		byte ret;
		fixed (byte* pchTitleBufferPtr = pchTitleStr)
		{
			ret = _ISteamUGC_SetItemTitle(instancePtr, handle, pchTitle is null ? null : pchTitleBufferPtr);
		}
		return ret != 0;
	}

	internal static bool ISteamUGC_SetItemDescription(IntPtr instancePtr, UGCUpdateHandle_t handle, string? pchDescription)
	{
		if ((nint)_ISteamUGC_SetItemDescription == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_SetItemDescription' is not available.");
		using var pchDescriptionStr = new ScopedCString(pchDescription ?? string.Empty);
		byte ret;
		fixed (byte* pchDescriptionBufferPtr = pchDescriptionStr)
		{
			ret = _ISteamUGC_SetItemDescription(instancePtr, handle, pchDescription is null ? null : pchDescriptionBufferPtr);
		}
		return ret != 0;
	}

	internal static bool ISteamUGC_SetItemUpdateLanguage(IntPtr instancePtr, UGCUpdateHandle_t handle, string? pchLanguage)
	{
		if ((nint)_ISteamUGC_SetItemUpdateLanguage == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_SetItemUpdateLanguage' is not available.");
		using var pchLanguageStr = new ScopedCString(pchLanguage ?? string.Empty);
		byte ret;
		fixed (byte* pchLanguageBufferPtr = pchLanguageStr)
		{
			ret = _ISteamUGC_SetItemUpdateLanguage(instancePtr, handle, pchLanguage is null ? null : pchLanguageBufferPtr);
		}
		return ret != 0;
	}

	internal static bool ISteamUGC_SetItemMetadata(IntPtr instancePtr, UGCUpdateHandle_t handle, string? pchMetaData)
	{
		if ((nint)_ISteamUGC_SetItemMetadata == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_SetItemMetadata' is not available.");
		using var pchMetaDataStr = new ScopedCString(pchMetaData ?? string.Empty);
		byte ret;
		fixed (byte* pchMetaDataBufferPtr = pchMetaDataStr)
		{
			ret = _ISteamUGC_SetItemMetadata(instancePtr, handle, pchMetaData is null ? null : pchMetaDataBufferPtr);
		}
		return ret != 0;
	}

	internal static bool ISteamUGC_SetItemVisibility(IntPtr instancePtr, UGCUpdateHandle_t handle, ERemoteStoragePublishedFileVisibility eVisibility)
	{
		if ((nint)_ISteamUGC_SetItemVisibility == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_SetItemVisibility' is not available.");
		return _ISteamUGC_SetItemVisibility(instancePtr, handle, eVisibility) != 0;
	}

	internal static bool ISteamUGC_SetItemTags(IntPtr instancePtr, UGCUpdateHandle_t updateHandle, IntPtr pTags, bool bAllowAdminTags)
	{
		if ((nint)_ISteamUGC_SetItemTags == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_SetItemTags' is not available.");
		return _ISteamUGC_SetItemTags(instancePtr, updateHandle, pTags, (byte)(bAllowAdminTags ? 1 : 0)) != 0;
	}

	internal static bool ISteamUGC_SetItemContent(IntPtr instancePtr, UGCUpdateHandle_t handle, string? pszContentFolder)
	{
		if ((nint)_ISteamUGC_SetItemContent == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_SetItemContent' is not available.");
		using var pszContentFolderStr = new ScopedCString(pszContentFolder ?? string.Empty);
		byte ret;
		fixed (byte* pszContentFolderBufferPtr = pszContentFolderStr)
		{
			ret = _ISteamUGC_SetItemContent(instancePtr, handle, pszContentFolder is null ? null : pszContentFolderBufferPtr);
		}
		return ret != 0;
	}

	internal static bool ISteamUGC_SetItemPreview(IntPtr instancePtr, UGCUpdateHandle_t handle, string? pszPreviewFile)
	{
		if ((nint)_ISteamUGC_SetItemPreview == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_SetItemPreview' is not available.");
		using var pszPreviewFileStr = new ScopedCString(pszPreviewFile ?? string.Empty);
		byte ret;
		fixed (byte* pszPreviewFileBufferPtr = pszPreviewFileStr)
		{
			ret = _ISteamUGC_SetItemPreview(instancePtr, handle, pszPreviewFile is null ? null : pszPreviewFileBufferPtr);
		}
		return ret != 0;
	}

	internal static bool ISteamUGC_SetAllowLegacyUpload(IntPtr instancePtr, UGCUpdateHandle_t handle, bool bAllowLegacyUpload)
	{
		if ((nint)_ISteamUGC_SetAllowLegacyUpload == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_SetAllowLegacyUpload' is not available.");
		return _ISteamUGC_SetAllowLegacyUpload(instancePtr, handle, (byte)(bAllowLegacyUpload ? 1 : 0)) != 0;
	}

	internal static bool ISteamUGC_RemoveAllItemKeyValueTags(IntPtr instancePtr, UGCUpdateHandle_t handle)
	{
		if ((nint)_ISteamUGC_RemoveAllItemKeyValueTags == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_RemoveAllItemKeyValueTags' is not available.");
		return _ISteamUGC_RemoveAllItemKeyValueTags(instancePtr, handle) != 0;
	}

	internal static bool ISteamUGC_RemoveItemKeyValueTags(IntPtr instancePtr, UGCUpdateHandle_t handle, string? pchKey)
	{
		if ((nint)_ISteamUGC_RemoveItemKeyValueTags == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_RemoveItemKeyValueTags' is not available.");
		using var pchKeyStr = new ScopedCString(pchKey ?? string.Empty);
		byte ret;
		fixed (byte* pchKeyBufferPtr = pchKeyStr)
		{
			ret = _ISteamUGC_RemoveItemKeyValueTags(instancePtr, handle, pchKey is null ? null : pchKeyBufferPtr);
		}
		return ret != 0;
	}

	internal static bool ISteamUGC_AddItemKeyValueTag(IntPtr instancePtr, UGCUpdateHandle_t handle, string? pchKey, string? pchValue)
	{
		if ((nint)_ISteamUGC_AddItemKeyValueTag == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_AddItemKeyValueTag' is not available.");
		using var pchKeyStr = new ScopedCString(pchKey ?? string.Empty);
		using var pchValueStr = new ScopedCString(pchValue ?? string.Empty);
		byte ret;
		fixed (byte* pchKeyBufferPtr = pchKeyStr)
		{
			fixed (byte* pchValueBufferPtr = pchValueStr)
			{
				ret = _ISteamUGC_AddItemKeyValueTag(instancePtr, handle, pchKey is null ? null : pchKeyBufferPtr, pchValue is null ? null : pchValueBufferPtr);
			}
		}
		return ret != 0;
	}

	internal static bool ISteamUGC_AddItemPreviewFile(IntPtr instancePtr, UGCUpdateHandle_t handle, string? pszPreviewFile, EItemPreviewType type)
	{
		if ((nint)_ISteamUGC_AddItemPreviewFile == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_AddItemPreviewFile' is not available.");
		using var pszPreviewFileStr = new ScopedCString(pszPreviewFile ?? string.Empty);
		byte ret;
		fixed (byte* pszPreviewFileBufferPtr = pszPreviewFileStr)
		{
			ret = _ISteamUGC_AddItemPreviewFile(instancePtr, handle, pszPreviewFile is null ? null : pszPreviewFileBufferPtr, type);
		}
		return ret != 0;
	}

	internal static bool ISteamUGC_AddItemPreviewVideo(IntPtr instancePtr, UGCUpdateHandle_t handle, string? pszVideoID)
	{
		if ((nint)_ISteamUGC_AddItemPreviewVideo == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_AddItemPreviewVideo' is not available.");
		using var pszVideoIDStr = new ScopedCString(pszVideoID ?? string.Empty);
		byte ret;
		fixed (byte* pszVideoIDBufferPtr = pszVideoIDStr)
		{
			ret = _ISteamUGC_AddItemPreviewVideo(instancePtr, handle, pszVideoID is null ? null : pszVideoIDBufferPtr);
		}
		return ret != 0;
	}

	internal static bool ISteamUGC_UpdateItemPreviewFile(IntPtr instancePtr, UGCUpdateHandle_t handle, uint index, string? pszPreviewFile)
	{
		if ((nint)_ISteamUGC_UpdateItemPreviewFile == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_UpdateItemPreviewFile' is not available.");
		using var pszPreviewFileStr = new ScopedCString(pszPreviewFile ?? string.Empty);
		byte ret;
		fixed (byte* pszPreviewFileBufferPtr = pszPreviewFileStr)
		{
			ret = _ISteamUGC_UpdateItemPreviewFile(instancePtr, handle, index, pszPreviewFile is null ? null : pszPreviewFileBufferPtr);
		}
		return ret != 0;
	}

	internal static bool ISteamUGC_UpdateItemPreviewVideo(IntPtr instancePtr, UGCUpdateHandle_t handle, uint index, string? pszVideoID)
	{
		if ((nint)_ISteamUGC_UpdateItemPreviewVideo == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_UpdateItemPreviewVideo' is not available.");
		using var pszVideoIDStr = new ScopedCString(pszVideoID ?? string.Empty);
		byte ret;
		fixed (byte* pszVideoIDBufferPtr = pszVideoIDStr)
		{
			ret = _ISteamUGC_UpdateItemPreviewVideo(instancePtr, handle, index, pszVideoID is null ? null : pszVideoIDBufferPtr);
		}
		return ret != 0;
	}

	internal static bool ISteamUGC_RemoveItemPreview(IntPtr instancePtr, UGCUpdateHandle_t handle, uint index)
	{
		if ((nint)_ISteamUGC_RemoveItemPreview == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_RemoveItemPreview' is not available.");
		return _ISteamUGC_RemoveItemPreview(instancePtr, handle, index) != 0;
	}

	internal static bool ISteamUGC_AddContentDescriptor(IntPtr instancePtr, UGCUpdateHandle_t handle, EUGCContentDescriptorID descid)
	{
		if ((nint)_ISteamUGC_AddContentDescriptor == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_AddContentDescriptor' is not available.");
		return _ISteamUGC_AddContentDescriptor(instancePtr, handle, descid) != 0;
	}

	internal static bool ISteamUGC_RemoveContentDescriptor(IntPtr instancePtr, UGCUpdateHandle_t handle, EUGCContentDescriptorID descid)
	{
		if ((nint)_ISteamUGC_RemoveContentDescriptor == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_RemoveContentDescriptor' is not available.");
		return _ISteamUGC_RemoveContentDescriptor(instancePtr, handle, descid) != 0;
	}

	internal static bool ISteamUGC_SetRequiredGameVersions(IntPtr instancePtr, UGCUpdateHandle_t handle, string? pszGameBranchMin, string? pszGameBranchMax)
	{
		if ((nint)_ISteamUGC_SetRequiredGameVersions == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_SetRequiredGameVersions' is not available.");
		using var pszGameBranchMinStr = new ScopedCString(pszGameBranchMin ?? string.Empty);
		using var pszGameBranchMaxStr = new ScopedCString(pszGameBranchMax ?? string.Empty);
		byte ret;
		fixed (byte* pszGameBranchMinBufferPtr = pszGameBranchMinStr)
		{
			fixed (byte* pszGameBranchMaxBufferPtr = pszGameBranchMaxStr)
			{
				ret = _ISteamUGC_SetRequiredGameVersions(instancePtr, handle, pszGameBranchMin is null ? null : pszGameBranchMinBufferPtr, pszGameBranchMax is null ? null : pszGameBranchMaxBufferPtr);
			}
		}
		return ret != 0;
	}

	internal static ulong ISteamUGC_SubmitItemUpdate(IntPtr instancePtr, UGCUpdateHandle_t handle, string? pchChangeNote)
	{
		if ((nint)_ISteamUGC_SubmitItemUpdate == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_SubmitItemUpdate' is not available.");
		using var pchChangeNoteStr = new ScopedCString(pchChangeNote ?? string.Empty);
		ulong ret;
		fixed (byte* pchChangeNoteBufferPtr = pchChangeNoteStr)
		{
			ret = _ISteamUGC_SubmitItemUpdate(instancePtr, handle, pchChangeNote is null ? null : pchChangeNoteBufferPtr);
		}
		return ret;
	}

	internal static EItemUpdateStatus ISteamUGC_GetItemUpdateProgress(IntPtr instancePtr, UGCUpdateHandle_t handle, out ulong punBytesProcessed, out ulong punBytesTotal)
	{
		if ((nint)_ISteamUGC_GetItemUpdateProgress == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_GetItemUpdateProgress' is not available.");
		punBytesProcessed = default;
		punBytesTotal = default;
		EItemUpdateStatus ret;
		fixed (ulong* punBytesProcessedPtr = &punBytesProcessed)
		{
			fixed (ulong* punBytesTotalPtr = &punBytesTotal)
			{
				ret = _ISteamUGC_GetItemUpdateProgress(instancePtr, handle, punBytesProcessedPtr, punBytesTotalPtr);
			}
		}
		return ret;
	}

	internal static ulong ISteamUGC_SetUserItemVote(IntPtr instancePtr, PublishedFileId_t nPublishedFileID, bool bVoteUp)
	{
		if ((nint)_ISteamUGC_SetUserItemVote == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_SetUserItemVote' is not available.");
		return _ISteamUGC_SetUserItemVote(instancePtr, nPublishedFileID, (byte)(bVoteUp ? 1 : 0));
	}

	internal static ulong ISteamUGC_GetUserItemVote(IntPtr instancePtr, PublishedFileId_t nPublishedFileID)
	{
		if ((nint)_ISteamUGC_GetUserItemVote == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_GetUserItemVote' is not available.");
		return _ISteamUGC_GetUserItemVote(instancePtr, nPublishedFileID);
	}

	internal static ulong ISteamUGC_AddItemToFavorites(IntPtr instancePtr, AppId_t nAppId, PublishedFileId_t nPublishedFileID)
	{
		if ((nint)_ISteamUGC_AddItemToFavorites == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_AddItemToFavorites' is not available.");
		return _ISteamUGC_AddItemToFavorites(instancePtr, nAppId, nPublishedFileID);
	}

	internal static ulong ISteamUGC_RemoveItemFromFavorites(IntPtr instancePtr, AppId_t nAppId, PublishedFileId_t nPublishedFileID)
	{
		if ((nint)_ISteamUGC_RemoveItemFromFavorites == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_RemoveItemFromFavorites' is not available.");
		return _ISteamUGC_RemoveItemFromFavorites(instancePtr, nAppId, nPublishedFileID);
	}

	internal static ulong ISteamUGC_SubscribeItem(IntPtr instancePtr, PublishedFileId_t nPublishedFileID)
	{
		if ((nint)_ISteamUGC_SubscribeItem == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_SubscribeItem' is not available.");
		return _ISteamUGC_SubscribeItem(instancePtr, nPublishedFileID);
	}

	internal static ulong ISteamUGC_UnsubscribeItem(IntPtr instancePtr, PublishedFileId_t nPublishedFileID)
	{
		if ((nint)_ISteamUGC_UnsubscribeItem == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_UnsubscribeItem' is not available.");
		return _ISteamUGC_UnsubscribeItem(instancePtr, nPublishedFileID);
	}

	internal static uint ISteamUGC_GetNumSubscribedItems(IntPtr instancePtr, bool bIncludeLocallyDisabled)
	{
		if ((nint)_ISteamUGC_GetNumSubscribedItems == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_GetNumSubscribedItems' is not available.");
		return _ISteamUGC_GetNumSubscribedItems(instancePtr, (byte)(bIncludeLocallyDisabled ? 1 : 0));
	}

	internal static uint ISteamUGC_GetSubscribedItems(IntPtr instancePtr, PublishedFileId_t[]? pvecPublishedFileID, uint cMaxEntries, bool bIncludeLocallyDisabled)
	{
		if ((nint)_ISteamUGC_GetSubscribedItems == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_GetSubscribedItems' is not available.");
		uint ret;
		fixed (PublishedFileId_t* pvecPublishedFileIDPtr = pvecPublishedFileID)
		{
			ret = _ISteamUGC_GetSubscribedItems(instancePtr, pvecPublishedFileIDPtr, cMaxEntries, (byte)(bIncludeLocallyDisabled ? 1 : 0));
		}
		return ret;
	}

	internal static uint ISteamUGC_GetItemState(IntPtr instancePtr, PublishedFileId_t nPublishedFileID)
	{
		if ((nint)_ISteamUGC_GetItemState == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_GetItemState' is not available.");
		return _ISteamUGC_GetItemState(instancePtr, nPublishedFileID);
	}

	internal static bool ISteamUGC_GetItemInstallInfo(IntPtr instancePtr, PublishedFileId_t nPublishedFileID, out ulong punSizeOnDisk, IntPtr pchFolder, uint cchFolderSize, out uint punTimeStamp)
	{
		if ((nint)_ISteamUGC_GetItemInstallInfo == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_GetItemInstallInfo' is not available.");
		punSizeOnDisk = default;
		punTimeStamp = default;
		byte ret;
		fixed (ulong* punSizeOnDiskPtr = &punSizeOnDisk)
		{
			fixed (uint* punTimeStampPtr = &punTimeStamp)
			{
				ret = _ISteamUGC_GetItemInstallInfo(instancePtr, nPublishedFileID, punSizeOnDiskPtr, pchFolder, cchFolderSize, punTimeStampPtr);
			}
		}
		return ret != 0;
	}

	internal static bool ISteamUGC_GetItemDownloadInfo(IntPtr instancePtr, PublishedFileId_t nPublishedFileID, out ulong punBytesDownloaded, out ulong punBytesTotal)
	{
		if ((nint)_ISteamUGC_GetItemDownloadInfo == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_GetItemDownloadInfo' is not available.");
		punBytesDownloaded = default;
		punBytesTotal = default;
		byte ret;
		fixed (ulong* punBytesDownloadedPtr = &punBytesDownloaded)
		{
			fixed (ulong* punBytesTotalPtr = &punBytesTotal)
			{
				ret = _ISteamUGC_GetItemDownloadInfo(instancePtr, nPublishedFileID, punBytesDownloadedPtr, punBytesTotalPtr);
			}
		}
		return ret != 0;
	}

	internal static bool ISteamUGC_DownloadItem(IntPtr instancePtr, PublishedFileId_t nPublishedFileID, bool bHighPriority)
	{
		if ((nint)_ISteamUGC_DownloadItem == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_DownloadItem' is not available.");
		return _ISteamUGC_DownloadItem(instancePtr, nPublishedFileID, (byte)(bHighPriority ? 1 : 0)) != 0;
	}

	internal static bool ISteamUGC_BInitWorkshopForGameServer(IntPtr instancePtr, DepotId_t unWorkshopDepotID, string? pszFolder)
	{
		if ((nint)_ISteamUGC_BInitWorkshopForGameServer == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_BInitWorkshopForGameServer' is not available.");
		using var pszFolderStr = new ScopedCString(pszFolder ?? string.Empty);
		byte ret;
		fixed (byte* pszFolderBufferPtr = pszFolderStr)
		{
			ret = _ISteamUGC_BInitWorkshopForGameServer(instancePtr, unWorkshopDepotID, pszFolder is null ? null : pszFolderBufferPtr);
		}
		return ret != 0;
	}

	internal static void ISteamUGC_SuspendDownloads(IntPtr instancePtr, bool bSuspend)
	{
		if ((nint)_ISteamUGC_SuspendDownloads == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_SuspendDownloads' is not available.");
		_ISteamUGC_SuspendDownloads(instancePtr, (byte)(bSuspend ? 1 : 0));
	}

	internal static ulong ISteamUGC_StartPlaytimeTracking(IntPtr instancePtr, PublishedFileId_t[]? pvecPublishedFileID, uint unNumPublishedFileIDs)
	{
		if ((nint)_ISteamUGC_StartPlaytimeTracking == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_StartPlaytimeTracking' is not available.");
		ulong ret;
		fixed (PublishedFileId_t* pvecPublishedFileIDPtr = pvecPublishedFileID)
		{
			ret = _ISteamUGC_StartPlaytimeTracking(instancePtr, pvecPublishedFileIDPtr, unNumPublishedFileIDs);
		}
		return ret;
	}

	internal static ulong ISteamUGC_StopPlaytimeTracking(IntPtr instancePtr, PublishedFileId_t[]? pvecPublishedFileID, uint unNumPublishedFileIDs)
	{
		if ((nint)_ISteamUGC_StopPlaytimeTracking == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_StopPlaytimeTracking' is not available.");
		ulong ret;
		fixed (PublishedFileId_t* pvecPublishedFileIDPtr = pvecPublishedFileID)
		{
			ret = _ISteamUGC_StopPlaytimeTracking(instancePtr, pvecPublishedFileIDPtr, unNumPublishedFileIDs);
		}
		return ret;
	}

	internal static ulong ISteamUGC_StopPlaytimeTrackingForAllItems(IntPtr instancePtr)
	{
		if ((nint)_ISteamUGC_StopPlaytimeTrackingForAllItems == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_StopPlaytimeTrackingForAllItems' is not available.");
		return _ISteamUGC_StopPlaytimeTrackingForAllItems(instancePtr);
	}

	internal static ulong ISteamUGC_AddDependency(IntPtr instancePtr, PublishedFileId_t nParentPublishedFileID, PublishedFileId_t nChildPublishedFileID)
	{
		if ((nint)_ISteamUGC_AddDependency == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_AddDependency' is not available.");
		return _ISteamUGC_AddDependency(instancePtr, nParentPublishedFileID, nChildPublishedFileID);
	}

	internal static ulong ISteamUGC_RemoveDependency(IntPtr instancePtr, PublishedFileId_t nParentPublishedFileID, PublishedFileId_t nChildPublishedFileID)
	{
		if ((nint)_ISteamUGC_RemoveDependency == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_RemoveDependency' is not available.");
		return _ISteamUGC_RemoveDependency(instancePtr, nParentPublishedFileID, nChildPublishedFileID);
	}

	internal static ulong ISteamUGC_AddAppDependency(IntPtr instancePtr, PublishedFileId_t nPublishedFileID, AppId_t nAppID)
	{
		if ((nint)_ISteamUGC_AddAppDependency == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_AddAppDependency' is not available.");
		return _ISteamUGC_AddAppDependency(instancePtr, nPublishedFileID, nAppID);
	}

	internal static ulong ISteamUGC_RemoveAppDependency(IntPtr instancePtr, PublishedFileId_t nPublishedFileID, AppId_t nAppID)
	{
		if ((nint)_ISteamUGC_RemoveAppDependency == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_RemoveAppDependency' is not available.");
		return _ISteamUGC_RemoveAppDependency(instancePtr, nPublishedFileID, nAppID);
	}

	internal static ulong ISteamUGC_GetAppDependencies(IntPtr instancePtr, PublishedFileId_t nPublishedFileID)
	{
		if ((nint)_ISteamUGC_GetAppDependencies == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_GetAppDependencies' is not available.");
		return _ISteamUGC_GetAppDependencies(instancePtr, nPublishedFileID);
	}

	internal static ulong ISteamUGC_DeleteItem(IntPtr instancePtr, PublishedFileId_t nPublishedFileID)
	{
		if ((nint)_ISteamUGC_DeleteItem == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_DeleteItem' is not available.");
		return _ISteamUGC_DeleteItem(instancePtr, nPublishedFileID);
	}

	internal static bool ISteamUGC_ShowWorkshopEULA(IntPtr instancePtr)
	{
		if ((nint)_ISteamUGC_ShowWorkshopEULA == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_ShowWorkshopEULA' is not available.");
		return _ISteamUGC_ShowWorkshopEULA(instancePtr) != 0;
	}

	internal static ulong ISteamUGC_GetWorkshopEULAStatus(IntPtr instancePtr)
	{
		if ((nint)_ISteamUGC_GetWorkshopEULAStatus == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_GetWorkshopEULAStatus' is not available.");
		return _ISteamUGC_GetWorkshopEULAStatus(instancePtr);
	}

	internal static uint ISteamUGC_GetUserContentDescriptorPreferences(IntPtr instancePtr, out EUGCContentDescriptorID pvecDescriptors, uint cMaxEntries)
	{
		if ((nint)_ISteamUGC_GetUserContentDescriptorPreferences == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_GetUserContentDescriptorPreferences' is not available.");
		pvecDescriptors = default;
		uint ret;
		fixed (EUGCContentDescriptorID* pvecDescriptorsPtr = &pvecDescriptors)
		{
			ret = _ISteamUGC_GetUserContentDescriptorPreferences(instancePtr, pvecDescriptorsPtr, cMaxEntries);
		}
		return ret;
	}

	internal static bool ISteamUGC_SetItemsDisabledLocally(IntPtr instancePtr, out PublishedFileId_t pvecPublishedFileIDs, uint unNumPublishedFileIDs, bool bDisabledLocally)
	{
		if ((nint)_ISteamUGC_SetItemsDisabledLocally == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_SetItemsDisabledLocally' is not available.");
		pvecPublishedFileIDs = default;
		byte ret;
		fixed (PublishedFileId_t* pvecPublishedFileIDsPtr = &pvecPublishedFileIDs)
		{
			ret = _ISteamUGC_SetItemsDisabledLocally(instancePtr, pvecPublishedFileIDsPtr, unNumPublishedFileIDs, (byte)(bDisabledLocally ? 1 : 0));
		}
		return ret != 0;
	}

	internal static bool ISteamUGC_SetSubscriptionsLoadOrder(IntPtr instancePtr, out PublishedFileId_t pvecPublishedFileIDs, uint unNumPublishedFileIDs)
	{
		if ((nint)_ISteamUGC_SetSubscriptionsLoadOrder == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUGC_SetSubscriptionsLoadOrder' is not available.");
		pvecPublishedFileIDs = default;
		byte ret;
		fixed (PublishedFileId_t* pvecPublishedFileIDsPtr = &pvecPublishedFileIDs)
		{
			ret = _ISteamUGC_SetSubscriptionsLoadOrder(instancePtr, pvecPublishedFileIDsPtr, unNumPublishedFileIDs);
		}
		return ret != 0;
	}

	internal static uint ISteamUtils_GetSecondsSinceAppActive(IntPtr instancePtr)
	{
		if ((nint)_ISteamUtils_GetSecondsSinceAppActive == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUtils_GetSecondsSinceAppActive' is not available.");
		return _ISteamUtils_GetSecondsSinceAppActive(instancePtr);
	}

	internal static uint ISteamUtils_GetSecondsSinceComputerActive(IntPtr instancePtr)
	{
		if ((nint)_ISteamUtils_GetSecondsSinceComputerActive == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUtils_GetSecondsSinceComputerActive' is not available.");
		return _ISteamUtils_GetSecondsSinceComputerActive(instancePtr);
	}

	internal static EUniverse ISteamUtils_GetConnectedUniverse(IntPtr instancePtr)
	{
		if ((nint)_ISteamUtils_GetConnectedUniverse == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUtils_GetConnectedUniverse' is not available.");
		return _ISteamUtils_GetConnectedUniverse(instancePtr);
	}

	internal static uint ISteamUtils_GetServerRealTime(IntPtr instancePtr)
	{
		if ((nint)_ISteamUtils_GetServerRealTime == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUtils_GetServerRealTime' is not available.");
		return _ISteamUtils_GetServerRealTime(instancePtr);
	}

	internal static IntPtr ISteamUtils_GetIPCountry(IntPtr instancePtr)
	{
		if ((nint)_ISteamUtils_GetIPCountry == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUtils_GetIPCountry' is not available.");
		return _ISteamUtils_GetIPCountry(instancePtr);
	}

	internal static bool ISteamUtils_GetImageSize(IntPtr instancePtr, int iImage, out uint pnWidth, out uint pnHeight)
	{
		if ((nint)_ISteamUtils_GetImageSize == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUtils_GetImageSize' is not available.");
		pnWidth = default;
		pnHeight = default;
		byte ret;
		fixed (uint* pnWidthPtr = &pnWidth)
		{
			fixed (uint* pnHeightPtr = &pnHeight)
			{
				ret = _ISteamUtils_GetImageSize(instancePtr, iImage, pnWidthPtr, pnHeightPtr);
			}
		}
		return ret != 0;
	}

	internal static bool ISteamUtils_GetImageRGBA(IntPtr instancePtr, int iImage, byte[]? pubDest, int nDestBufferSize)
	{
		if ((nint)_ISteamUtils_GetImageRGBA == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUtils_GetImageRGBA' is not available.");
		byte ret;
		fixed (byte* pubDestPtr = pubDest)
		{
			ret = _ISteamUtils_GetImageRGBA(instancePtr, iImage, pubDestPtr, nDestBufferSize);
		}
		return ret != 0;
	}

	internal static byte ISteamUtils_GetCurrentBatteryPower(IntPtr instancePtr)
	{
		if ((nint)_ISteamUtils_GetCurrentBatteryPower == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUtils_GetCurrentBatteryPower' is not available.");
		return _ISteamUtils_GetCurrentBatteryPower(instancePtr);
	}

	internal static uint ISteamUtils_GetAppID(IntPtr instancePtr)
	{
		if ((nint)_ISteamUtils_GetAppID == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUtils_GetAppID' is not available.");
		return _ISteamUtils_GetAppID(instancePtr);
	}

	internal static void ISteamUtils_SetOverlayNotificationPosition(IntPtr instancePtr, ENotificationPosition eNotificationPosition)
	{
		if ((nint)_ISteamUtils_SetOverlayNotificationPosition == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUtils_SetOverlayNotificationPosition' is not available.");
		_ISteamUtils_SetOverlayNotificationPosition(instancePtr, eNotificationPosition);
	}

	internal static bool ISteamUtils_IsAPICallCompleted(IntPtr instancePtr, SteamAPICall_t hSteamAPICall, out bool pbFailed)
	{
		if ((nint)_ISteamUtils_IsAPICallCompleted == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUtils_IsAPICallCompleted' is not available.");
		byte pbFailedVal = 0;
		byte ret;
		ret = _ISteamUtils_IsAPICallCompleted(instancePtr, hSteamAPICall, &pbFailedVal);
		pbFailed = pbFailedVal != 0;
		return ret != 0;
	}

	internal static ESteamAPICallFailure ISteamUtils_GetAPICallFailureReason(IntPtr instancePtr, SteamAPICall_t hSteamAPICall)
	{
		if ((nint)_ISteamUtils_GetAPICallFailureReason == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUtils_GetAPICallFailureReason' is not available.");
		return _ISteamUtils_GetAPICallFailureReason(instancePtr, hSteamAPICall);
	}

	internal static bool ISteamUtils_GetAPICallResult(IntPtr instancePtr, SteamAPICall_t hSteamAPICall, IntPtr pCallback, int cubCallback, int iCallbackExpected, out bool pbFailed)
	{
		if ((nint)_ISteamUtils_GetAPICallResult == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUtils_GetAPICallResult' is not available.");
		byte pbFailedVal = 0;
		byte ret;
		ret = _ISteamUtils_GetAPICallResult(instancePtr, hSteamAPICall, pCallback, cubCallback, iCallbackExpected, &pbFailedVal);
		pbFailed = pbFailedVal != 0;
		return ret != 0;
	}

	internal static uint ISteamUtils_GetIPCCallCount(IntPtr instancePtr)
	{
		if ((nint)_ISteamUtils_GetIPCCallCount == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUtils_GetIPCCallCount' is not available.");
		return _ISteamUtils_GetIPCCallCount(instancePtr);
	}

	internal static void ISteamUtils_SetWarningMessageHook(IntPtr instancePtr, SteamAPIWarningMessageHook_t pFunction)
	{
		if ((nint)_ISteamUtils_SetWarningMessageHook == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUtils_SetWarningMessageHook' is not available.");
		_ISteamUtils_SetWarningMessageHook(instancePtr, pFunction is null ? 0 : Marshal.GetFunctionPointerForDelegate(pFunction));
	}

	internal static bool ISteamUtils_IsOverlayEnabled(IntPtr instancePtr)
	{
		if ((nint)_ISteamUtils_IsOverlayEnabled == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUtils_IsOverlayEnabled' is not available.");
		return _ISteamUtils_IsOverlayEnabled(instancePtr) != 0;
	}

	internal static bool ISteamUtils_BOverlayNeedsPresent(IntPtr instancePtr)
	{
		if ((nint)_ISteamUtils_BOverlayNeedsPresent == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUtils_BOverlayNeedsPresent' is not available.");
		return _ISteamUtils_BOverlayNeedsPresent(instancePtr) != 0;
	}

	internal static ulong ISteamUtils_CheckFileSignature(IntPtr instancePtr, string? szFileName)
	{
		if ((nint)_ISteamUtils_CheckFileSignature == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUtils_CheckFileSignature' is not available.");
		using var szFileNameStr = new ScopedCString(szFileName ?? string.Empty);
		ulong ret;
		fixed (byte* szFileNameBufferPtr = szFileNameStr)
		{
			ret = _ISteamUtils_CheckFileSignature(instancePtr, szFileName is null ? null : szFileNameBufferPtr);
		}
		return ret;
	}

	internal static bool ISteamUtils_ShowGamepadTextInput(IntPtr instancePtr, EGamepadTextInputMode eInputMode, EGamepadTextInputLineMode eLineInputMode, string? pchDescription, uint unCharMax, string? pchExistingText)
	{
		if ((nint)_ISteamUtils_ShowGamepadTextInput == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUtils_ShowGamepadTextInput' is not available.");
		using var pchDescriptionStr = new ScopedCString(pchDescription ?? string.Empty);
		using var pchExistingTextStr = new ScopedCString(pchExistingText ?? string.Empty);
		byte ret;
		fixed (byte* pchDescriptionBufferPtr = pchDescriptionStr)
		{
			fixed (byte* pchExistingTextBufferPtr = pchExistingTextStr)
			{
				ret = _ISteamUtils_ShowGamepadTextInput(instancePtr, eInputMode, eLineInputMode, pchDescription is null ? null : pchDescriptionBufferPtr, unCharMax, pchExistingText is null ? null : pchExistingTextBufferPtr);
			}
		}
		return ret != 0;
	}

	internal static uint ISteamUtils_GetEnteredGamepadTextLength(IntPtr instancePtr)
	{
		if ((nint)_ISteamUtils_GetEnteredGamepadTextLength == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUtils_GetEnteredGamepadTextLength' is not available.");
		return _ISteamUtils_GetEnteredGamepadTextLength(instancePtr);
	}

	internal static bool ISteamUtils_GetEnteredGamepadTextInput(IntPtr instancePtr, IntPtr pchText, uint cchText)
	{
		if ((nint)_ISteamUtils_GetEnteredGamepadTextInput == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUtils_GetEnteredGamepadTextInput' is not available.");
		return _ISteamUtils_GetEnteredGamepadTextInput(instancePtr, pchText, cchText) != 0;
	}

	internal static IntPtr ISteamUtils_GetSteamUILanguage(IntPtr instancePtr)
	{
		if ((nint)_ISteamUtils_GetSteamUILanguage == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUtils_GetSteamUILanguage' is not available.");
		return _ISteamUtils_GetSteamUILanguage(instancePtr);
	}

	internal static bool ISteamUtils_IsSteamRunningInVR(IntPtr instancePtr)
	{
		if ((nint)_ISteamUtils_IsSteamRunningInVR == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUtils_IsSteamRunningInVR' is not available.");
		return _ISteamUtils_IsSteamRunningInVR(instancePtr) != 0;
	}

	internal static void ISteamUtils_SetOverlayNotificationInset(IntPtr instancePtr, int nHorizontalInset, int nVerticalInset)
	{
		if ((nint)_ISteamUtils_SetOverlayNotificationInset == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUtils_SetOverlayNotificationInset' is not available.");
		_ISteamUtils_SetOverlayNotificationInset(instancePtr, nHorizontalInset, nVerticalInset);
	}

	internal static bool ISteamUtils_IsSteamInBigPictureMode(IntPtr instancePtr)
	{
		if ((nint)_ISteamUtils_IsSteamInBigPictureMode == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUtils_IsSteamInBigPictureMode' is not available.");
		return _ISteamUtils_IsSteamInBigPictureMode(instancePtr) != 0;
	}

	internal static void ISteamUtils_StartVRDashboard(IntPtr instancePtr)
	{
		if ((nint)_ISteamUtils_StartVRDashboard == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUtils_StartVRDashboard' is not available.");
		_ISteamUtils_StartVRDashboard(instancePtr);
	}

	internal static bool ISteamUtils_IsVRHeadsetStreamingEnabled(IntPtr instancePtr)
	{
		if ((nint)_ISteamUtils_IsVRHeadsetStreamingEnabled == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUtils_IsVRHeadsetStreamingEnabled' is not available.");
		return _ISteamUtils_IsVRHeadsetStreamingEnabled(instancePtr) != 0;
	}

	internal static void ISteamUtils_SetVRHeadsetStreamingEnabled(IntPtr instancePtr, bool bEnabled)
	{
		if ((nint)_ISteamUtils_SetVRHeadsetStreamingEnabled == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUtils_SetVRHeadsetStreamingEnabled' is not available.");
		_ISteamUtils_SetVRHeadsetStreamingEnabled(instancePtr, (byte)(bEnabled ? 1 : 0));
	}

	internal static bool ISteamUtils_IsSteamChinaLauncher(IntPtr instancePtr)
	{
		if ((nint)_ISteamUtils_IsSteamChinaLauncher == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUtils_IsSteamChinaLauncher' is not available.");
		return _ISteamUtils_IsSteamChinaLauncher(instancePtr) != 0;
	}

	internal static bool ISteamUtils_InitFilterText(IntPtr instancePtr, uint unFilterOptions)
	{
		if ((nint)_ISteamUtils_InitFilterText == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUtils_InitFilterText' is not available.");
		return _ISteamUtils_InitFilterText(instancePtr, unFilterOptions) != 0;
	}

	internal static int ISteamUtils_FilterText(IntPtr instancePtr, ETextFilteringContext eContext, CSteamID sourceSteamID, string? pchInputMessage, IntPtr pchOutFilteredText, uint nByteSizeOutFilteredText)
	{
		if ((nint)_ISteamUtils_FilterText == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUtils_FilterText' is not available.");
		using var pchInputMessageStr = new ScopedCString(pchInputMessage ?? string.Empty);
		int ret;
		fixed (byte* pchInputMessageBufferPtr = pchInputMessageStr)
		{
			ret = _ISteamUtils_FilterText(instancePtr, eContext, sourceSteamID, pchInputMessage is null ? null : pchInputMessageBufferPtr, pchOutFilteredText, nByteSizeOutFilteredText);
		}
		return ret;
	}

	internal static ESteamIPv6ConnectivityState ISteamUtils_GetIPv6ConnectivityState(IntPtr instancePtr, ESteamIPv6ConnectivityProtocol eProtocol)
	{
		if ((nint)_ISteamUtils_GetIPv6ConnectivityState == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUtils_GetIPv6ConnectivityState' is not available.");
		return _ISteamUtils_GetIPv6ConnectivityState(instancePtr, eProtocol);
	}

	internal static bool ISteamUtils_IsSteamRunningOnSteamDeck(IntPtr instancePtr)
	{
		if ((nint)_ISteamUtils_IsSteamRunningOnSteamDeck == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUtils_IsSteamRunningOnSteamDeck' is not available.");
		return _ISteamUtils_IsSteamRunningOnSteamDeck(instancePtr) != 0;
	}

	internal static bool ISteamUtils_ShowFloatingGamepadTextInput(IntPtr instancePtr, EFloatingGamepadTextInputMode eKeyboardMode, int nTextFieldXPosition, int nTextFieldYPosition, int nTextFieldWidth, int nTextFieldHeight)
	{
		if ((nint)_ISteamUtils_ShowFloatingGamepadTextInput == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUtils_ShowFloatingGamepadTextInput' is not available.");
		return _ISteamUtils_ShowFloatingGamepadTextInput(instancePtr, eKeyboardMode, nTextFieldXPosition, nTextFieldYPosition, nTextFieldWidth, nTextFieldHeight) != 0;
	}

	internal static void ISteamUtils_SetGameLauncherMode(IntPtr instancePtr, bool bLauncherMode)
	{
		if ((nint)_ISteamUtils_SetGameLauncherMode == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUtils_SetGameLauncherMode' is not available.");
		_ISteamUtils_SetGameLauncherMode(instancePtr, (byte)(bLauncherMode ? 1 : 0));
	}

	internal static bool ISteamUtils_DismissFloatingGamepadTextInput(IntPtr instancePtr)
	{
		if ((nint)_ISteamUtils_DismissFloatingGamepadTextInput == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUtils_DismissFloatingGamepadTextInput' is not available.");
		return _ISteamUtils_DismissFloatingGamepadTextInput(instancePtr) != 0;
	}

	internal static bool ISteamUtils_DismissGamepadTextInput(IntPtr instancePtr)
	{
		if ((nint)_ISteamUtils_DismissGamepadTextInput == 0) throw new EntryPointNotFoundException("'SteamAPI_ISteamUtils_DismissGamepadTextInput' is not available.");
		return _ISteamUtils_DismissGamepadTextInput(instancePtr) != 0;
	}

}
