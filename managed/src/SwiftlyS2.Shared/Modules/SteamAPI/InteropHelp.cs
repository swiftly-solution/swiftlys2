namespace SwiftlyS2.Shared.SteamAPI;

public class InteropHelp
{
    public static void TestIfAvailableGameServer()
    {
        if (CSteamGameServerAPIContext.GetSteamClient() == IntPtr.Zero)
        {
            if (!CSteamGameServerAPIContext.Init())
            {
                throw new InvalidOperationException("Steamworks GameServer is not initialized.");
            }
        }
    }
}