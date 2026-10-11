namespace SwiftlyS2.Shared.SteamAPI;

public static class GameServer
{
    public static bool BSecure()
    {
        return NativeMethods.SteamGameServer_BSecure();
    }

    public static CSteamID GetSteamID()
    {
        return (CSteamID)NativeMethods.SteamGameServer_GetSteamID();
    }

    public static HSteamPipe GetHSteamPipe()
    {
        return (HSteamPipe)NativeMethods.SteamGameServer_GetHSteamPipe();
    }

    public static HSteamUser GetHSteamUser()
    {
        return (HSteamUser)NativeMethods.SteamGameServer_GetHSteamUser();
    }
}

internal static class CSteamGameServerAPIContext
{
    internal static bool Init()
    {
        var hSteamUser = GameServer.GetHSteamUser();
        var hSteamPipe = GameServer.GetHSteamPipe();
        if (hSteamPipe == (HSteamPipe)0) { return false; }

        m_pSteamClient = NativeMethods.SteamInternal_CreateInterface(Constants.STEAMCLIENT_INTERFACE_VERSION);
        if (m_pSteamClient == IntPtr.Zero) { return false; }

        m_pSteamGameServer = SteamGameServerClient.GetISteamGameServer(hSteamUser, hSteamPipe, Constants.STEAMGAMESERVER_INTERFACE_VERSION);
        if (m_pSteamGameServer == IntPtr.Zero) { return false; }

        m_pSteamUtils = SteamGameServerClient.GetISteamUtils(hSteamPipe, Constants.STEAMUTILS_INTERFACE_VERSION);
        if (m_pSteamUtils == IntPtr.Zero) { return false; }

        m_pSteamNetworking = SteamGameServerClient.GetISteamNetworking(hSteamUser, hSteamPipe, Constants.STEAMNETWORKING_INTERFACE_VERSION);
        if (m_pSteamNetworking == IntPtr.Zero) { return false; }

        m_pSteamGameServerStats = SteamGameServerClient.GetISteamGameServerStats(hSteamUser, hSteamPipe, Constants.STEAMGAMESERVERSTATS_INTERFACE_VERSION);
        if (m_pSteamGameServerStats == IntPtr.Zero) { return false; }

        m_pSteamHTTP = SteamGameServerClient.GetISteamHTTP(hSteamUser, hSteamPipe, Constants.STEAMHTTP_INTERFACE_VERSION);
        if (m_pSteamHTTP == IntPtr.Zero) { return false; }

        m_pSteamInventory = SteamGameServerClient.GetISteamInventory(hSteamUser, hSteamPipe, Constants.STEAMINVENTORY_INTERFACE_VERSION);
        if (m_pSteamInventory == IntPtr.Zero) { return false; }

        m_pSteamUGC = SteamGameServerClient.GetISteamUGC(hSteamUser, hSteamPipe, Constants.STEAMUGC_INTERFACE_VERSION);
        if (m_pSteamUGC == IntPtr.Zero) { return false; }

        m_pSteamNetworkingUtils =
            NativeMethods.SteamInternal_FindOrCreateUserInterface(hSteamUser, Constants.STEAMNETWORKINGUTILS_INTERFACE_VERSION) != IntPtr.Zero ?
            NativeMethods.SteamInternal_FindOrCreateUserInterface(hSteamUser, Constants.STEAMNETWORKINGUTILS_INTERFACE_VERSION) :
            NativeMethods.SteamInternal_FindOrCreateGameServerInterface(hSteamUser, Constants.STEAMNETWORKINGUTILS_INTERFACE_VERSION);
        if (m_pSteamNetworkingUtils == IntPtr.Zero) { return false; }

        m_pSteamNetworkingSockets = NativeMethods.SteamInternal_FindOrCreateGameServerInterface(hSteamUser, Constants.STEAMNETWORKINGSOCKETS_INTERFACE_VERSION);
        if (m_pSteamNetworkingSockets == IntPtr.Zero) { return false; }

        return true;
    }

    internal static IntPtr GetSteamClient() { return m_pSteamClient; }
    internal static IntPtr GetSteamGameServer() { return m_pSteamGameServer; }
    internal static IntPtr GetSteamUtils() { return m_pSteamUtils; }
    internal static IntPtr GetSteamNetworking() { return m_pSteamNetworking; }
    internal static IntPtr GetSteamGameServerStats() { return m_pSteamGameServerStats; }
    internal static IntPtr GetSteamHTTP() { return m_pSteamHTTP; }
    internal static IntPtr GetSteamInventory() { return m_pSteamInventory; }
    internal static IntPtr GetSteamUGC() { return m_pSteamUGC; }
    internal static IntPtr GetSteamNetworkingUtils() { return m_pSteamNetworkingUtils; }
    internal static IntPtr GetSteamNetworkingSockets() { return m_pSteamNetworkingSockets; }

    private static IntPtr m_pSteamClient;
    private static IntPtr m_pSteamGameServer;
    private static IntPtr m_pSteamUtils;
    private static IntPtr m_pSteamNetworking;
    private static IntPtr m_pSteamGameServerStats;
    private static IntPtr m_pSteamHTTP;
    private static IntPtr m_pSteamInventory;
    private static IntPtr m_pSteamUGC;
    private static IntPtr m_pSteamNetworkingUtils;
    private static IntPtr m_pSteamNetworkingSockets;
}