using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.SteamAPI;

namespace SwiftlyS2.Shared.Engine;

public interface IServerSideClientBase
{
    /// <summary>Gets the user ID string of the client.</summary>
    public string UserIDString { get; }

    /// <summary>Gets the name of the client.</summary>
    public string Name { get; set; }

    /// <summary>Gets the client slot index.</summary>
    public int ClientSlot { get; }

    /// <summary>Gets the entity index associated with the client.</summary>
    public uint EntityIndex { get; }

    /// <summary>Gets the network channel associated with the client.</summary>
    public INetChannel NetChannel { get; }

    /// <summary>Gets the connection type flags for the client.</summary>
    public byte ConnectionTypeFlags { get; }

    /// <summary>Gets the send job state (ClientSendJobState_t: 0 idle, 1 running, 2 finished) of the client.</summary>
    public byte SendJobState { get; }

    /// <summary>Gets or sets a value indicating whether the client's send job is idle.</summary>
    public bool SendJobIdle { get; set; }

    /// <summary>Gets the signon state of the client.</summary>
    public SignonState_t SignonState { get; }

    /// <summary>Gets a value indicating whether the client is a fake player (bot).</summary>
    public bool FakePlayer { get; }

    /// <summary>Gets a value indicating whether the client's full update is logged.</summary>
    public bool LogFullUpdate { get; }

    /// <summary>Gets the user ID of the client.</summary>
    public int UserID { get; }

    /// <summary>Gets a value indicating whether the client has received a packet from the server.</summary>
    public bool ReceivedPacket { get; }

    /// <summary>Gets the Steam ID of the client.</summary>
    public ref CSteamID SteamID { get; }

    /// <summary>Gets the last Steam ID of the client.</summary>
    public ref CSteamID LastSteamID { get; }

    /// <summary>Gets the authentication ticket Steam ID of the client.</summary>
    public ref CSteamID AuthTicketSteamID { get; }

    /// <summary>Gets the friends Steam ID of the client.</summary>
    public ref CSteamID FriendsID { get; }

    /// <summary>Gets the IP address of the client.</summary>
    public ref NSAddress IPAddress { get; }

    /// <summary>Gets the console variables associated with the client.</summary>
    public unsafe KeyValues* Convars { get; }

    /// <summary>Gets a value indicating whether the client's console variables have changed.</summary>
    public bool ConvarsChanged { get; }

    /// <summary>Gets a value indicating whether the client is connected through HLTV.</summary>
    public bool IsHLTV { get; }

    /// <summary>Gets the sendtable CRC value for the client.</summary>
    public uint SendtableCRC { get; }

    /// <summary>Gets the challenge ID for the client.</summary>
    public uint ChallengeID { get; }

    /// <summary>Gets the sign-on tick for the client.</summary>
    public int SignonTick { get; }

    /// <summary>Gets the delta tick for the client.</summary>
    public int DeltaTick { get; set; }

    /// <summary>Gets the string table acknowledgment tick for the client.</summary>
    public int StringTableAckTick { get; }

    /// <summary>The raw address of the ServerSideClientBase instance in memory.</summary>
    public nint Address { get; }

    /// <summary>Gets the list of loaded spawn groups for the client.</summary>
    public ref CUtlVector<nint> LoadedSpawnGroups { get; }

    /// <summary>Gets the bit vector of baselines sent to the client.</summary>
    public ref CBitVec16384 BaselinesSent { get; }

    /// <summary>Gets the baseline used for the client.</summary>
    public int BaselineUsed { get; }

    /// <summary>Gets the loading progress for the client.</summary>
    public int LoadingProgress { get; }

    /// <summary>Gets the force wait for tick value for the client.</summary>
    public int ForceWaitForTick { get; }

    /// <summary>Gets a value indicating whether the client is in low violence mode.</summary>
    public bool LowViolence { get; }

    /// <summary>Gets a value indicating whether the client is fully authenticated.</summary>
    public bool FullyAuthenticated { get; }

    /// <summary>Gets the next message time for the client.</summary>
    public float NextMessageTime { get; }

    /// <summary>Gets the authenticated time for the client.</summary>
    public float AuthenticatedTime { get; }

    /// <summary>Gets the snapshot interval for the client.</summary>
    public float SnapshotInterval { get; }

    /// <summary>Gets the count of spam commands for the client.</summary>
    public int SpamCommandsCount { get; }

    /// <summary>Gets the network stat trace for the client.</summary>
    public ref CNetworkStatTrace NetworkStatTrace { get; }

    /// <summary>Gets the time at which the last command was executed for the client.</summary>
    public double LastExecutedCommand { get; }

    /// <summary>Reconnects the client to the server.</summary>
    public void Reconnect();

    /// <summary>Sets the rate for the client.</summary>
    public void SetRate( int rate );

    /// <summary>Sets the update rate for the client.</summary>
    public void SetUpdateRate( float updateRate );

    /// <summary>Gets the current rate for the client.</summary>
    public int GetRate();

    public bool IsConnected => SignonState >= SignonState_t.SIGNONSTATE_CONNECTED;
    public bool IsInGame => SignonState == SignonState_t.SIGNONSTATE_FULL;
    public bool IsSpawned => SignonState >= SignonState_t.SIGNONSTATE_NEW;
    public bool IsActive => IsInGame;

    /// <summary>Forces a full update for the client.</summary>
    public void ForceFullUpdate();

    /// <summary>Gets a value indicating whether the client should send messages to the server.</summary>
    public bool ShouldSendMessages { get; }

    /// <summary>Updates the send state for the client.</summary>
    public void UpdateSendState();
    /// <summary>Updates the user settings for the client.</summary>
    public void UpdateUserSettings();
    /// <summary>Resets the user settings for the client.</summary>
    public void ResetUserSettings();

    /// <summary>Frees the baselines for the client.</summary>
    public void FreeBaselines();

    /// <summary>Called when the client's send job finishes.</summary>
    public void OnSendJobFinished();

    /// <summary>Called when the client's send job starts.</summary>
    public void OnSendJobStarted();
}