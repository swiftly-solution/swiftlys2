using SwiftlyS2.Core.Engine;
using SwiftlyS2.Core.EntitySystem;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Scheduler;
using SwiftlyS2.Core.SchemaDefinitions;
using SwiftlyS2.Shared.Engine;
using SwiftlyS2.Shared.Events;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.Translation;

namespace SwiftlyS2.Core.Players;

internal class Player : IPlayer, IDisposable
{
    public Player( int pid )
    {
        Slot = pid;
        SessionId = NativePlayer.GetSessionID(pid);
    }

    ~Player()
    {
        Dispose();
    }

    private bool _disposed = false;
    private ServerSideClient _serverSideClient = new();

    public int PlayerID => Slot;
    public ulong SessionId { get; }

    public int Slot { get; }

    public int UserID { get { ThrowIfDisposed(); return NativePlayer.GetUserID(Slot); } }

    public bool IsFakeClient { get { ThrowIfDisposed(); return NativePlayer.IsFakeClient(Slot); } }

    public bool IsAuthorized { get { ThrowIfDisposed(); return NativePlayer.IsAuthorized(Slot); } }
    public uint ConnectedTime { get { ThrowIfDisposed(); return NativePlayer.GetConnectedTime(Slot); } }

    public Language PlayerLanguage { get { ThrowIfDisposed(); return new(NativePlayer.GetLanguage(Slot)); } }

    public ulong SteamID { get { ThrowIfDisposed(); return NativePlayer.GetSteamID(Slot); } }

    public ulong UnauthorizedSteamID { get { ThrowIfDisposed(); return NativePlayer.GetUnauthorizedSteamID(Slot); } }

    public CCSPlayerController Controller { get { ThrowIfDisposed(); var controllerPtr = NativePlayer.GetController(Slot); return EntityManager.GetEntityByAddress(controllerPtr) as CCSPlayerControllerImpl ?? new CCSPlayerControllerImpl(controllerPtr); } }
    public CCSPlayerController RequiredController => Controller is { IsValid: true } controller ? controller : throw new InvalidOperationException("Controller is not valid");

    public CBasePlayerPawn? Pawn => Controller is { IsValid: true } ? (Controller.Pawn is { IsValid: true } pawn ? pawn.Value : null) : null;

    public CBasePlayerPawn RequiredPawn => Pawn is { IsValid: true } pawn ? pawn : throw new InvalidOperationException("Pawn is not valid");

    public CCSPlayerPawn? PlayerPawn => Controller is { IsValid: true } ? (Controller.PlayerPawn is { IsValid: true } pawn ? pawn.Value : null) : null;

    public CCSPlayerPawn RequiredPlayerPawn => PlayerPawn is { IsValid: true } pawn ? pawn : throw new InvalidOperationException("PlayerPawn is not valid");

    public GameButtonFlags PressedButtons { get { ThrowIfDisposed(); return (GameButtonFlags)NativePlayer.GetPressedButtons(Slot); } }

    public string IPAddress { get { ThrowIfDisposed(); return NativePlayer.GetIPAddress(Slot); } }

    public VoiceFlagValue VoiceFlags { get { ThrowIfDisposed(); return (VoiceFlagValue)NativeVoiceManager.GetClientVoiceFlags(Slot); } set { ThrowIfDisposed(); NativeVoiceManager.SetClientVoiceFlags(Slot, (int)value); } }
    public bool IsValid =>
        !_disposed &&
        Controller is { IsValid: true, IsHLTV: false, Connected: PlayerConnectedState.Connected } &&
        Pawn is { IsValid: true };

    public bool IsAlive => IsValid && Pawn!.LifeState == (byte)LifeState_t.LIFE_ALIVE;

    public bool IsFirstSpawn { get { ThrowIfDisposed(); return NativePlayer.IsFirstSpawn(Slot); } }

    Language IPlayer.PlayerLanguage => PlayerLanguage;

    public string Name { get => ServerSideClient.Name; set => ServerSideClient.Name = value; }

    public IServerSideClient ServerSideClient { get { ThrowIfDisposed(); _serverSideClient.SetDangerousHandle(NativePlayer.GetServerSideClient(PlayerID)); return _serverSideClient; } }

    public void ChangeTeam( Team team )
    {
        ThrowIfDisposed();
        if (Controller == null || !Controller.IsValid) return;
        GameFunctions.CCSPlayerControllerChangeTeam(Controller.Address, team);
    }

    public Task ChangeTeamAsync( Team team )
    {
        ThrowIfDisposed();
        return SchedulerManager.QueueOrNow(() => ChangeTeam(team));
    }

    public void ClearTransmitEntityBlocks()
    {
        ThrowIfDisposed();
        NativePlayer.ClearTransmitEntityBlocked(Slot);
    }

    public ListenOverride GetListenOverride( int player )
    {
        ThrowIfDisposed();
        return (ListenOverride)NativeVoiceManager.GetClientListenOverride(Slot, player);
    }

    public bool IsTransmitEntityBlocked( int entityid )
    {
        ThrowIfDisposed();
        return NativePlayer.IsTransmitEntityBlocked(Slot, entityid);
    }

    public string GetClientConvarValue( string convarName )
    {
        ThrowIfDisposed();
        return NativePlayer.GetClientConvarValue(Slot, convarName);
    }

    public void Kick( string reason, ENetworkDisconnectionReason gameReason )
    {
        ThrowIfDisposed();
        CancellationTokenSource cts = new();
        SchedulerManager.NextTick(() =>
        {
            NativePlayer.Kick(Slot, reason, (int)gameReason);
        }, cts.Token);
    }

    public Task KickAsync( string reason, ENetworkDisconnectionReason gameReason )
    {
        return SchedulerManager.NextTickAsync(() =>
        {
            NativePlayer.Kick(Slot, reason, (int)gameReason);
        });
    }

    public void SendMessage( MessageType kind, string message )
    {
        ThrowIfDisposed();
        NativePlayer.SendMessage(Slot, (int)kind, message, 5000);
    }

    public Task SendMessageAsync( MessageType kind, string message )
    {
        return SchedulerManager.QueueOrNow(() => SendMessage(kind, message));
    }

    public void SetListenOverride( int player, ListenOverride listenOverride )
    {
        ThrowIfDisposed();
        NativeVoiceManager.SetClientListenOverride(Slot, player, (int)listenOverride);
    }

    public void ShouldBlockTransmitEntity( int entityid, bool shouldBlockTransmit )
    {
        ThrowIfDisposed();
        NativePlayer.ShouldBlockTransmitEntity(Slot, entityid, shouldBlockTransmit);
    }

    public void SwitchTeam( Team team )
    {
        ThrowIfDisposed();
        if (Controller == null || !Controller.IsValid) return;

        if (team == Team.None || team == Team.Spectator) ChangeTeam(team);
        else GameFunctions.CCSPlayerControllerSwitchTeam(Controller.Address, team);
    }

    public Task SwitchTeamAsync( Team team )
    {
        if (team == Team.None || team == Team.Spectator) return ChangeTeamAsync(team);
        else return SchedulerManager.QueueOrNow(() => SwitchTeam(team));
    }

    public void TakeDamage( CTakeDamageInfo damageInfo )
    {
        ThrowIfDisposed();

        if (Pawn == null || !Pawn.IsValid) return;
        Pawn.TakeDamage(damageInfo);
    }

    public Task TakeDamageAsync( CTakeDamageInfo damageInfo )
    {
        return SchedulerManager.QueueOrNow(() => TakeDamage(damageInfo));
    }

    public void TakeDamage( float damage, DamageTypes_t damageType, CBaseEntity? inflictor = null, CBaseEntity? attacker = null, CBaseEntity? ability = null )
    {
        var info = new CTakeDamageInfo(damage, damageType, inflictor, attacker, ability);
        TakeDamage(info);
    }

    public Task TakeDamageAsync( float damage, DamageTypes_t damageType, CBaseEntity? inflictor = null, CBaseEntity? attacker = null, CBaseEntity? ability = null )
    {
        return SchedulerManager.QueueOrNow(() => TakeDamage(damage, damageType, inflictor, attacker, ability));
    }

    public void Teleport( Vector pos, QAngle angle, Vector velocity )
    {
        Pawn!.Teleport(pos, angle, velocity);
    }

    public void Teleport( Vector? pos = null, QAngle? angle = null, Vector? velocity = null )
    {
        Pawn!.Teleport(pos, angle, velocity);
    }

    public Task TeleportAsync( Vector pos, QAngle angle, Vector velocity )
    {
        return SchedulerManager.QueueOrNow(() => Teleport(pos, angle, velocity));
    }

    public Task TeleportAsync( Vector? pos = null, QAngle? angle = null, Vector? velocity = null )
    {
        return SchedulerManager.QueueOrNow(() => Teleport(pos, angle, velocity));
    }

    public void Respawn()
    {
        Controller?.Respawn();
    }

    public void ExecuteCommand( string command )
    {
        ThrowIfDisposed();
        NativePlayer.ExecuteCommand(Slot, command);
    }

    public Task ExecuteCommandAsync( string command )
    {
        return SchedulerManager.QueueOrNow(() => ExecuteCommand(command));
    }

    public bool Equals( IPlayer? other )
    {
        return other is not null && SessionId == other.SessionId;
    }

    public override bool Equals( object? obj )
    {
        return obj is IPlayer player && Equals(player);
    }

    public override int GetHashCode()
    {
        return PlayerID.GetHashCode();
    }

    public void SendMessage( MessageType kind, string message, int htmlDuration = 5000 )
    {
        ThrowIfDisposed();
        NativePlayer.SendMessage(Slot, (int)kind, message, htmlDuration);
    }

    public Task SendMessageAsync( MessageType kind, string message, int htmlDuration = 5000 )
    {
        return SchedulerManager.QueueOrNow(() => SendMessage(kind, message, htmlDuration));
    }

    public void SendNotify( string message )
    {
        SendMessage(MessageType.Notify, message);
    }

    public Task SendNotifyAsync( string message )
    {
        return SendMessageAsync(MessageType.Notify, message);
    }

    public void SendConsole( string message )
    {
        SendMessage(MessageType.Console, message);
    }

    public Task SendConsoleAsync( string message )
    {
        return SendMessageAsync(MessageType.Console, message);
    }

    public void SendChat( string message )
    {
        SendMessage(MessageType.Chat, message);
    }

    public Task SendChatAsync( string message )
    {
        return SendMessageAsync(MessageType.Chat, message);
    }

    public void SendCenter( string message )
    {
        SendMessage(MessageType.Center, message);
    }

    public Task SendCenterAsync( string message )
    {
        return SendMessageAsync(MessageType.Center, message);
    }

    public void SendAlert( string message )
    {
        SendMessage(MessageType.Alert, message);
    }

    public Task SendAlertAsync( string message )
    {
        return SendMessageAsync(MessageType.Alert, message);
    }

    public void SendCenterHTML( string message, int duration = 5000 )
    {
        SendMessage(MessageType.CenterHTML, message, duration);
    }

    public Task SendCenterHTMLAsync( string message, int duration = 5000 )
    {
        return SendMessageAsync(MessageType.CenterHTML, message, duration);
    }

    public void SendChatEOT( string message )
    {
        SendMessage(MessageType.ChatEOT, message);
    }

    public Task SendChatEOTAsync( string message )
    {
        return SendMessageAsync(MessageType.ChatEOT, message);
    }

    public void Dispose()
    {
        _disposed = true;
    }

    public void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException($"Player object (slot={Slot},sessionId={SessionId}) has been disposed.");
        }
    }

    public static bool operator ==( Player? left, Player? right )
    {
        return left is not null && right is not null && left.Equals(right);
    }

    public static bool operator !=( Player left, Player right ) => !(left == right);
}
