using SwiftlyS2.Core.Extensions;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Engine;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.SteamAPI;

namespace SwiftlyS2.Core.Engine;

internal static class IServerSideClientBaseFields
{
    public static Lazy<int> UserIDStringLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::m_UserIDString"));
    public static int UserIDStringOffset => UserIDStringLazy.Value;

    public static Lazy<int> NameLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::m_Name"));
    public static int NameOffset => NameLazy.Value;

    public static Lazy<int> ClientSlotLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::m_nClientSlot"));
    public static int ClientSlotOffset => ClientSlotLazy.Value;

    public static Lazy<int> EntityIndexLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::m_nEntityIndex"));
    public static int EntityIndexOffset => EntityIndexLazy.Value;

    public static Lazy<int> NetChannelLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::m_NetChannel"));
    public static int NetChannelOffset => NetChannelLazy.Value;

    public static Lazy<int> ConnectionTypeFlagsLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::m_nConnectionTypeFlags"));
    public static int ConnectionTypeFlagsOffset => ConnectionTypeFlagsLazy.Value;

    public static Lazy<int> SendJobStateLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::m_nSendJobState"));
    public static int SendJobStateOffset => SendJobStateLazy.Value;

    public static Lazy<int> SendJobIdleLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::m_bSendJobIdle"));
    public static int SendJobIdleOffset => SendJobIdleLazy.Value;

    public static Lazy<int> SignonStateLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::m_nSignonState"));
    public static int SignonStateOffset => SignonStateLazy.Value;

    public static Lazy<int> FakePlayerLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::m_bFakePlayer"));
    public static int FakePlayerOffset => FakePlayerLazy.Value;

    public static Lazy<int> LogFullUpdateLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::m_bLogFullUpdate"));
    public static int LogFullUpdateOffset => LogFullUpdateLazy.Value;

    public static Lazy<int> UserIDLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::m_UserID"));
    public static int UserIDOffset => UserIDLazy.Value;

    public static Lazy<int> ReceivedPacketLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::m_bReceivedPacket"));
    public static int ReceivedPacketOffset => ReceivedPacketLazy.Value;

    public static Lazy<int> SteamIDLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::m_SteamID"));
    public static int SteamIDOffset => SteamIDLazy.Value;

    public static Lazy<int> LastSteamIDLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::m_LastSteamID"));
    public static int LastSteamIDOffset => LastSteamIDLazy.Value;

    public static Lazy<int> AuthTicketSteamIDLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::m_AuthTicketSteamID"));
    public static int AuthTicketSteamIDOffset => AuthTicketSteamIDLazy.Value;

    public static Lazy<int> FriendsIDLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::m_nFriendsID"));
    public static int FriendsIDOffset => FriendsIDLazy.Value;

    public static Lazy<int> IPAddressLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::m_nAddr"));
    public static int IPAddressOffset => IPAddressLazy.Value;

    public static Lazy<int> ConVarsLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::m_ConVars"));
    public static int ConVarsOffset => ConVarsLazy.Value;

    public static Lazy<int> ConVarsChangedLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::m_bConVarsChanged"));
    public static int ConVarsChangedOffset => ConVarsChangedLazy.Value;

    public static Lazy<int> IsHLTVLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::m_bIsHLTV"));
    public static int IsHLTVOffset => IsHLTVLazy.Value;

    public static Lazy<int> SendtableCRCLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::m_nSendtableCRC"));
    public static int SendtableCRCOffset => SendtableCRCLazy.Value;

    public static Lazy<int> ChallengeIDLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::m_uChallengeNumber"));
    public static int ChallengeIDOffset => ChallengeIDLazy.Value;

    public static Lazy<int> SignonTickLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::m_nSignonTick"));
    public static int SignonTickOffset => SignonTickLazy.Value;

    public static Lazy<int> DeltaTickLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::m_nDeltaTick"));
    public static int DeltaTickOffset => DeltaTickLazy.Value;

    public static Lazy<int> StringTableAckTickLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::m_nStringTableAckTick"));
    public static int StringTableAckTickOffset => StringTableAckTickLazy.Value;

    public static Lazy<int> LoadedSpawnGroupsLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::m_vecLoadedSpawnGroups"));
    public static int LoadedSpawnGroupsOffset => LoadedSpawnGroupsLazy.Value;

    public static Lazy<int> BaselineLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::m_pBaseline"));
    public static int BaselineOffset => BaselineLazy.Value;

    public static Lazy<int> BaselineUpdateTickLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::m_nBaselineUpdateTick"));
    public static int BaselineUpdateTickOffset => BaselineUpdateTickLazy.Value;

    public static Lazy<int> BaselinesSentLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::m_BaselinesSent"));
    public static int BaselinesSentOffset => BaselinesSentLazy.Value;

    public static Lazy<int> BaselineUsedLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::m_nBaselineUsed"));
    public static int BaselineUsedOffset => BaselineUsedLazy.Value;

    public static Lazy<int> LoadingProgressLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::m_nLoadingProgress"));
    public static int LoadingProgressOffset => LoadingProgressLazy.Value;

    public static Lazy<int> ForceWaitForTickLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::m_nForceWaitForTick"));
    public static int ForceWaitForTickOffset => ForceWaitForTickLazy.Value;

    public static Lazy<int> LowViolenceLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::m_bLowViolence"));
    public static int LowViolenceOffset => LowViolenceLazy.Value;

    public static Lazy<int> FullyAuthenticatedLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::m_bFullyAuthenticated"));
    public static int FullyAuthenticatedOffset => FullyAuthenticatedLazy.Value;

    public static Lazy<int> NextMessageTimeLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::m_fNextMessageTime"));
    public static int NextMessageTimeOffset => NextMessageTimeLazy.Value;

    public static Lazy<int> AuthenticatedTimeLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::m_fAuthenticatedTime"));
    public static int AuthenticatedTimeOffset => AuthenticatedTimeLazy.Value;

    public static Lazy<int> SnapshotIntervalLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::m_fSnapshotInterval"));
    public static int SnapshotIntervalOffset => SnapshotIntervalLazy.Value;

    public static Lazy<int> NetworkStatTraceLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::m_Trace"));
    public static int NetworkStatTraceOffset => NetworkStatTraceLazy.Value;

    public static Lazy<int> SpamCommandsCountLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::m_spamCommandsCount"));
    public static int SpamCommandsCountOffset => SpamCommandsCountLazy.Value;

    public static Lazy<int> LastExecutedCommandLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::m_lastExecutedCommand"));
    public static int LastExecutedCommandOffset => LastExecutedCommandLazy.Value;
}

internal static class IServerSideClientBaseVTable
{
    public static Lazy<int> FreeBaselinesLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::FreeBaselines"));
    public static int FreeBaselinesOffset => FreeBaselinesLazy.Value;
    public static unsafe void FreeBaselines( nint basePtr )
    {
        var vFunction = GameFunctions.GetVirtualFunction(basePtr, FreeBaselinesOffset);
        ((delegate* unmanaged< nint, void >)vFunction)(basePtr);
    }

    public static Lazy<int> GetRateLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::GetRate"));
    public static int GetRateOffset => GetRateLazy.Value;
    public static unsafe int GetRate( nint basePtr )
    {
        var vFunction = GameFunctions.GetVirtualFunction(basePtr, GetRateOffset);
        return ((delegate* unmanaged< nint, int >)vFunction)(basePtr);
    }

    public static Lazy<int> OnSendJobFinishedLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::OnSendJobFinished"));
    public static int OnSendJobFinishedOffset => OnSendJobFinishedLazy.Value;
    public static unsafe void OnSendJobFinished( nint basePtr )
    {
        var vFunction = GameFunctions.GetVirtualFunction(basePtr, OnSendJobFinishedOffset);
        ((delegate* unmanaged< nint, void >)vFunction)(basePtr);
    }

    public static Lazy<int> ReconnectLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::Reconnect"));
    public static int ReconnectOffset => ReconnectLazy.Value;
    public static unsafe void Reconnect( nint basePtr )
    {
        var vFunction = GameFunctions.GetVirtualFunction(basePtr, ReconnectOffset);
        ((delegate* unmanaged< nint, void >)vFunction)(basePtr);
    }

    public static Lazy<int> ResetUserSettingsLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::ResetUserSettings"));
    public static int ResetUserSettingsOffset => ResetUserSettingsLazy.Value;
    public static unsafe void ResetUserSettings( nint basePtr )
    {
        var vFunction = GameFunctions.GetVirtualFunction(basePtr, ResetUserSettingsOffset);
        ((delegate* unmanaged< nint, void >)vFunction)(basePtr);
    }

    public static Lazy<int> SetRateLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::SetRate"));
    public static int SetRateOffset => SetRateLazy.Value;
    public static unsafe void SetRate( nint basePtr, int rate )
    {
        var vFunction = GameFunctions.GetVirtualFunction(basePtr, SetRateOffset);
        ((delegate* unmanaged< nint, int, void >)vFunction)(basePtr, rate);
    }

    public static Lazy<int> SetUpdateRateLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::SetUpdateRate"));
    public static int SetUpdateRateOffset => SetUpdateRateLazy.Value;
    public static unsafe void SetUpdateRate( nint basePtr, float updateRate )
    {
        var vFunction = GameFunctions.GetVirtualFunction(basePtr, SetUpdateRateOffset);
        ((delegate* unmanaged< nint, float, void >)vFunction)(basePtr, updateRate);
    }

    public static Lazy<int> OnSendJobStartedLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::OnSendJobStarted"));
    public static int OnSendJobStartedOffset => OnSendJobStartedLazy.Value;
    public static unsafe void OnSendJobStarted( nint basePtr )
    {
        var vFunction = GameFunctions.GetVirtualFunction(basePtr, OnSendJobStartedOffset);
        ((delegate* unmanaged< nint, void >)vFunction)(basePtr);
    }

    public static Lazy<int> UpdateSendStateLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::UpdateSendState"));
    public static int UpdateSendStateOffset => UpdateSendStateLazy.Value;
    public static unsafe void UpdateSendState( nint basePtr )
    {
        var vFunction = GameFunctions.GetVirtualFunction(basePtr, UpdateSendStateOffset);
        ((delegate* unmanaged< nint, void >)vFunction)(basePtr);
    }

    public static Lazy<int> UpdateUserSettingsLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::UpdateUserSettings"));
    public static int UpdateUserSettingsOffset => UpdateUserSettingsLazy.Value;
    public static unsafe void UpdateUserSettings( nint basePtr )
    {
        var vFunction = GameFunctions.GetVirtualFunction(basePtr, UpdateUserSettingsOffset);
        ((delegate* unmanaged< nint, void >)vFunction)(basePtr);
    }

    public static Lazy<int> ShouldSendMessagesLazy = new(() => NativeOffsets.Fetch("CServerSideClientBase::ShouldSendMessages"));
    public static int ShouldSendMessagesOffset => ShouldSendMessagesLazy.Value;
    public static unsafe bool ShouldSendMessages( nint basePtr )
    {
        var vFunction = GameFunctions.GetVirtualFunction(basePtr, ShouldSendMessagesOffset);
        return ((delegate* unmanaged< nint, byte >)vFunction)(basePtr) == 1;
    }
}

internal class ServerSideClientBase : IServerSideClientBase
{
    private NetChannel _netChannel = new();

    public string UserIDString => Address.AsRef<CUtlString>(IServerSideClientBaseFields.UserIDStringOffset).Value;

    public string Name { get => Address.AsRef<CUtlString>(IServerSideClientBaseFields.NameOffset).Value; set => Address.AsRef<CUtlString>(IServerSideClientBaseFields.NameOffset).Value = value; }

    public int ClientSlot => Address.AsRef<int>(IServerSideClientBaseFields.ClientSlotOffset);

    public uint EntityIndex => Address.AsRef<uint>(IServerSideClientBaseFields.EntityIndexOffset);

    public INetChannel NetChannel {
        get {
            _netChannel.DangerouslySetPtr(Address.AsRef<nint>(IServerSideClientBaseFields.NetChannelOffset));
            return _netChannel;
        }
    }

    public byte ConnectionTypeFlags => Address.AsRef<byte>(IServerSideClientBaseFields.ConnectionTypeFlagsOffset);
    public byte SendJobState => Address.AsRef<byte>(IServerSideClientBaseFields.SendJobStateOffset);

    public bool SendJobIdle { get => Address.AsRef<byte>(IServerSideClientBaseFields.SendJobIdleOffset) == 1; set => Address.AsRef<byte>(IServerSideClientBaseFields.SendJobIdleOffset) = (byte)(value ? 1 : 0); }

    public SignonState_t SignonState => Address.AsRef<SignonState_t>(IServerSideClientBaseFields.SignonStateOffset);

    public bool FakePlayer => Address.AsRef<byte>(IServerSideClientBaseFields.FakePlayerOffset) == 1;

    public bool LogFullUpdate => Address.AsRef<byte>(IServerSideClientBaseFields.LogFullUpdateOffset) == 1;

    public int UserID => Address.AsRef<int>(IServerSideClientBaseFields.UserIDOffset);

    public bool ReceivedPacket => Address.AsRef<byte>(IServerSideClientBaseFields.ReceivedPacketOffset) == 1;

    public ref CSteamID SteamID => ref Address.AsRef<CSteamID>(IServerSideClientBaseFields.SteamIDOffset);

    public ref CSteamID LastSteamID => ref Address.AsRef<CSteamID>(IServerSideClientBaseFields.LastSteamIDOffset);

    public ref CSteamID AuthTicketSteamID => ref Address.AsRef<CSteamID>(IServerSideClientBaseFields.AuthTicketSteamIDOffset);

    public ref CSteamID FriendsID => ref Address.AsRef<CSteamID>(IServerSideClientBaseFields.FriendsIDOffset);

    public ref NSAddress IPAddress => ref Address.AsRef<NSAddress>(IServerSideClientBaseFields.IPAddressOffset);

    public unsafe KeyValues* Convars => (KeyValues*)Address.AsRef<nint>(IServerSideClientBaseFields.ConVarsOffset);

    public bool ConvarsChanged => Address.AsRef<byte>(IServerSideClientBaseFields.ConVarsChangedOffset) == 1;

    public bool IsHLTV => Address.AsRef<byte>(IServerSideClientBaseFields.IsHLTVOffset) == 1;

    public uint SendtableCRC => Address.AsRef<uint>(IServerSideClientBaseFields.SendtableCRCOffset);

    public uint ChallengeID => Address.AsRef<uint>(IServerSideClientBaseFields.ChallengeIDOffset);

    public int SignonTick => Address.AsRef<int>(IServerSideClientBaseFields.SignonTickOffset);

    public int DeltaTick { get => Address.AsRef<int>(IServerSideClientBaseFields.DeltaTickOffset); set => Address.AsRef<int>(IServerSideClientBaseFields.DeltaTickOffset) = value; }

    public int StringTableAckTick => Address.AsRef<int>(IServerSideClientBaseFields.StringTableAckTickOffset);

    public nint Address { get; internal set; } = 0;

    public ref CUtlVector<nint> LoadedSpawnGroups => ref Address.AsRef<CUtlVector<nint>>(IServerSideClientBaseFields.LoadedSpawnGroupsOffset);

    public ref CBitVec16384 BaselinesSent => ref Address.AsRef<CBitVec16384>(IServerSideClientBaseFields.BaselinesSentOffset);

    public int BaselineUsed => Address.AsRef<int>(IServerSideClientBaseFields.BaselineUsedOffset);

    public int LoadingProgress => Address.AsRef<int>(IServerSideClientBaseFields.LoadingProgressOffset);

    public int ForceWaitForTick => Address.AsRef<int>(IServerSideClientBaseFields.ForceWaitForTickOffset);

    public bool LowViolence => Address.AsRef<byte>(IServerSideClientBaseFields.LowViolenceOffset) == 1;

    public bool FullyAuthenticated => Address.AsRef<byte>(IServerSideClientBaseFields.FullyAuthenticatedOffset) == 1;

    public float NextMessageTime => Address.AsRef<float>(IServerSideClientBaseFields.NextMessageTimeOffset);

    public float AuthenticatedTime => Address.AsRef<float>(IServerSideClientBaseFields.AuthenticatedTimeOffset);

    public float SnapshotInterval => Address.AsRef<float>(IServerSideClientBaseFields.SnapshotIntervalOffset);

    public int SpamCommandsCount => Address.AsRef<int>(IServerSideClientBaseFields.SpamCommandsCountOffset);

    public ref CNetworkStatTrace NetworkStatTrace => ref Address.AsRef<CNetworkStatTrace>(IServerSideClientBaseFields.NetworkStatTraceOffset);

    public double LastExecutedCommand => Address.AsRef<double>(IServerSideClientBaseFields.LastExecutedCommandOffset);

    public bool ShouldSendMessages => IServerSideClientBaseVTable.ShouldSendMessages(Address);

    public void ForceFullUpdate()
    {
        DeltaTick = -1;
    }

    public void FreeBaselines()
    {
        IServerSideClientBaseVTable.FreeBaselines(Address);
    }

    public int GetRate()
    {
        return IServerSideClientBaseVTable.GetRate(Address);
    }

    public void OnSendJobFinished()
    {
        IServerSideClientBaseVTable.OnSendJobFinished(Address);
    }

    public void Reconnect()
    {
        IServerSideClientBaseVTable.Reconnect(Address);
    }

    public void ResetUserSettings()
    {
        IServerSideClientBaseVTable.ResetUserSettings(Address);
    }

    public void SetRate( int rate )
    {
        IServerSideClientBaseVTable.SetRate(Address, rate);
    }

    public void SetUpdateRate( float updateRate )
    {
        IServerSideClientBaseVTable.SetUpdateRate(Address, updateRate);
    }

    public void OnSendJobStarted()
    {
        IServerSideClientBaseVTable.OnSendJobStarted(Address);
    }

    public void UpdateSendState()
    {
        IServerSideClientBaseVTable.UpdateSendState(Address);
    }

    public void UpdateUserSettings()
    {
        IServerSideClientBaseVTable.UpdateUserSettings(Address);
    }

    internal void SetDangerousHandle( nint ptr )
    {
        Address = ptr;
    }
}