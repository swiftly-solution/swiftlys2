using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Scheduler;
using SwiftlyS2.Core.SchemaDefinitions;
using SwiftlyS2.Shared.Events;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.Translation;

namespace SwiftlyS2.Core.Players;

internal class Player : IPlayer
{
    public Player( int pid )
    {
        Slot = pid;
    }

    public int PlayerID => Slot;

    public int Slot { get; }

    public int UserID => NativePlayer.GetUserID(Slot);

    public bool IsFakeClient => NativePlayer.IsFakeClient(Slot);

    public bool IsAuthorized => NativePlayer.IsAuthorized(Slot);

    public uint ConnectedTime => NativePlayer.GetConnectedTime(Slot);

    public Language PlayerLanguage => new(NativePlayer.GetLanguage(Slot));

    public ulong SteamID => NativePlayer.GetSteamID(Slot);

    public ulong UnauthorizedSteamID => NativePlayer.GetUnauthorizedSteamID(Slot);

    public CCSPlayerController Controller => new CCSPlayerControllerImpl(NativePlayer.GetController(Slot));

    public CCSPlayerController RequiredController => Controller is { IsValid: true } controller ? controller : throw new InvalidOperationException("Controller is not valid");

    public CBasePlayerPawn? Pawn => Controller?.Pawn.Value;

    public CBasePlayerPawn RequiredPawn => Pawn is { IsValid: true } pawn ? pawn : throw new InvalidOperationException("Pawn is not valid");

    public CCSPlayerPawn? PlayerPawn => Controller?.PlayerPawn.Value;

    public CCSPlayerPawn RequiredPlayerPawn => PlayerPawn is { IsValid: true } pawn ? pawn : throw new InvalidOperationException("PlayerPawn is not valid");

    public GameButtonFlags PressedButtons => (GameButtonFlags)NativePlayer.GetPressedButtons(Slot);

    public string IPAddress => NativePlayer.GetIPAddress(Slot);

    public VoiceFlagValue VoiceFlags { get => (VoiceFlagValue)NativeVoiceManager.GetClientVoiceFlags(Slot); set => NativeVoiceManager.SetClientVoiceFlags(Slot, (int)value); }

    public bool IsValid =>
        Controller is { IsValid: true, IsHLTV: false, Connected: PlayerConnectedState.PlayerConnected } &&
        Pawn is { IsValid: true };

    public bool IsFirstSpawn => NativePlayer.IsFirstSpawn(Slot);

    Language IPlayer.PlayerLanguage => PlayerLanguage;

    public void ChangeTeam( Team team )
    {
        NativePlayer.ChangeTeam(Slot, (byte)team);
    }

    public Task ChangeTeamAsync( Team team )
    {
        return SchedulerManager.QueueOrNow(() => ChangeTeam(team));
    }

    public void ClearTransmitEntityBlocks()
    {
        NativePlayer.ClearTransmitEntityBlocked(Slot);
    }

    public ListenOverride GetListenOverride( int player )
    {
        return (ListenOverride)NativeVoiceManager.GetClientListenOverride(Slot, player);
    }

    public bool IsTransmitEntityBlocked( int entityid )
    {
        return NativePlayer.IsTransmitEntityBlocked(Slot, entityid);
    }

    public void Kick( string reason, ENetworkDisconnectionReason gameReason )
    {
        NativePlayer.Kick(Slot, reason, (int)gameReason);
    }

    public Task KickAsync( string reason, ENetworkDisconnectionReason gameReason )
    {
        return SchedulerManager.QueueOrNow(() => Kick(reason, gameReason));
    }

    public void SendMessage( MessageType kind, string message )
    {
        NativePlayer.SendMessage(Slot, (int)kind, message, 5000);
    }

    public Task SendMessageAsync( MessageType kind, string message )
    {
        return SchedulerManager.QueueOrNow(() => SendMessage(kind, message));
    }

    public void SetListenOverride( int player, ListenOverride listenOverride )
    {
        NativeVoiceManager.SetClientListenOverride(Slot, player, (int)listenOverride);
    }

    public void ShouldBlockTransmitEntity( int entityid, bool shouldBlockTransmit )
    {
        NativePlayer.ShouldBlockTransmitEntity(Slot, entityid, shouldBlockTransmit);
    }

    public void SwitchTeam( Team team )
    {
        NativePlayer.SwitchTeam(Slot, (byte)team);
    }

    public Task SwitchTeamAsync( Team team )
    {
        return SchedulerManager.QueueOrNow(() => SwitchTeam(team));
    }

    public void TakeDamage( CTakeDamageInfo damageInfo )
    {
        unsafe
        {
            NativePlayer.TakeDamage(Slot, (nint)(&damageInfo));
        }
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
        NativePlayer.ExecuteCommand(Slot, command);
    }

    public Task ExecuteCommandAsync( string command )
    {
        return SchedulerManager.QueueOrNow(() => ExecuteCommand(command));
    }

    public bool Equals( IPlayer? other )
    {
        return other is not null && PlayerID == other.PlayerID;
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

    public static bool operator ==( Player? left, Player? right )
    {
        return left is not null && right is not null && left.Equals(right);
    }

    public static bool operator !=( Player left, Player right ) => !(left == right);
}