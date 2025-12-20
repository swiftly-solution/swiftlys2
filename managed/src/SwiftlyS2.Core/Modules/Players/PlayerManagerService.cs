using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Scheduler;
using SwiftlyS2.Core.SchemaDefinitions;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.SteamAPI;
using SwiftlyS2.Shared.Translation;

namespace SwiftlyS2.Core.Players;

internal class PlayerManagerService : IPlayerManagerService
{
    private ITranslationService _translationService;

    public PlayerManagerService( ITranslationService translationService )
    {
        _translationService = translationService;
    }

    public int PlayerCount => NativePlayerManager.GetPlayerCount();

    public int PlayerCap => NativePlayerManager.GetPlayerCap();

    public void ClearAllBlockedTransmitEntities()
    {
        NativePlayerManager.ClearAllBlockedTransmitEntity();
    }

    public IPlayer? GetPlayer( int playerid )
    {
        if (!IsPlayerOnline(playerid)) return null;
        return new Player(playerid);
    }

    public IPlayer? GetPlayerFromController( CBasePlayerController controller )
    {
        return GetPlayer((int)(controller.Index - 1));
    }

    public IPlayer? GetPlayerFromPawn( CBasePlayerPawn pawn )
    {
        return pawn.Controller.Value is not { IsValid: true } controller ? null : GetPlayerFromController(controller);
    }

    public bool IsPlayerOnline( int playerid )
    {
        return NativePlayerManager.IsPlayerOnline(playerid);
    }

    public void SendMessage( MessageType kind, string message )
    {
        NativePlayerManager.SendMessage((int)kind, message, 5000);
    }

    public Task SendMessageAsync( MessageType kind, string message )
    {
        return SchedulerManager.QueueOrNow(() => SendMessage(kind, message));
    }

    public void ShouldBlockTransmitEntity( int entityid, bool shouldBlockTransmit )
    {
        NativePlayerManager.ShouldBlockTransmitEntity(entityid, shouldBlockTransmit);
    }

    public IEnumerable<IPlayer> GetAllPlayers()
    {
        return Enumerable.Range(0, PlayerCap)
            .Where(IsPlayerOnline)
            .Select(( pid ) => GetPlayer(pid)!);
    }

    public IEnumerable<IPlayer> FindTargettedPlayers( IPlayer player, string target, TargetSearchMode searchMode,
        StringComparison nameComparison = StringComparison.OrdinalIgnoreCase )
    {
        IEnumerable<IPlayer> allPlayers = [];

        var players = GetAllPlayers();
        foreach (var targetPlayer in players)
        {
            if (searchMode.HasFlag(TargetSearchMode.NoBots) && targetPlayer.IsFakeClient)
                continue;

            if (searchMode.HasFlag(TargetSearchMode.IncludeSelf) == false && targetPlayer.PlayerID == player.PlayerID)
                continue;

            if (searchMode.HasFlag(TargetSearchMode.Alive) &&
                targetPlayer.Pawn?.LifeState != (byte)LifeState_t.LIFE_ALIVE)
                continue;

            if (searchMode.HasFlag(TargetSearchMode.Dead) &&
                targetPlayer.Pawn?.LifeState != (byte)LifeState_t.LIFE_DEAD)
                continue;

            if (searchMode.HasFlag(TargetSearchMode.TeamOnly) && targetPlayer.Pawn?.TeamNum != player.Pawn?.TeamNum)
                continue;

            if (searchMode.HasFlag(TargetSearchMode.OppositeTeamOnly) &&
                targetPlayer.Pawn?.TeamNum == player.Pawn?.TeamNum)
                continue;

            if (target == "@all")
            {
                allPlayers = allPlayers.Append(targetPlayer);
            }
            else if (target == "@me" && targetPlayer.PlayerID == player.PlayerID)
            {
                allPlayers = allPlayers.Append(targetPlayer);
            }
            else if (target == "@!me" && targetPlayer.PlayerID != player.PlayerID)
            {
                allPlayers = allPlayers.Append(targetPlayer);
            }
            else if ((target == "@bots" || target == "@!human") && targetPlayer.IsFakeClient)
            {
                allPlayers = allPlayers.Append(targetPlayer);
            }
            else if ((target == "@!bots" || target == "@human") && !targetPlayer.IsFakeClient)
            {
                allPlayers = allPlayers.Append(targetPlayer);
            }
            else if (target == "@alive" && targetPlayer.Pawn?.LifeState == (byte)LifeState_t.LIFE_ALIVE)
            {
                allPlayers = allPlayers.Append(targetPlayer);
            }
            else if (target == "@dead" && targetPlayer.Pawn?.LifeState == (byte)LifeState_t.LIFE_DEAD)
            {
                allPlayers = allPlayers.Append(targetPlayer);
            }
            else if (target == "@aim")
            {
                var ccsgamerules = new CCSGameRulesImpl(NativeEntitySystem.GetGameRules());
                var pickerEntity = ccsgamerules.FindPickerEntity<CCSPlayerPawn>(player.Controller);

                if (pickerEntity != null && pickerEntity.IsValid && pickerEntity.DesignerName == "player")
                {
                    var entIndex = pickerEntity.OriginalController.Value?.Entity?.EntityHandle.EntityIndex;
                    if (entIndex.HasValue)
                    {
                        allPlayers = allPlayers.Append(GetPlayer((int)entIndex.Value - 1)!);
                    }
                }
            }
            else if (target == "@ct" && targetPlayer.Pawn?.TeamNum == (int)Team.CT)
            {
                allPlayers = allPlayers.Append(targetPlayer);
            }
            else if (target == "@t" && targetPlayer.Pawn?.TeamNum == (int)Team.T)
            {
                allPlayers = allPlayers.Append(targetPlayer);
            }
            else if (target == "@spec" && targetPlayer.Pawn?.TeamNum == (int)Team.Spectator)
            {
                allPlayers = allPlayers.Append(targetPlayer);
            }
            else if (target.StartsWith('#'))
            {
                if (int.TryParse(target[1..], out int id) && targetPlayer.PlayerID == id)
                {
                    allPlayers = allPlayers.Append(targetPlayer);
                }
            }
            else if (targetPlayer.Controller.PlayerName.Contains(target, nameComparison))
            {
                allPlayers = allPlayers.Append(targetPlayer);
            }
            else if (new CSteamID(target) is var steamId && steamId.IsValid() &&
                     steamId.GetSteamID64() == targetPlayer.SteamID)
            {
                allPlayers = allPlayers.Append(targetPlayer);
            }
        }

        return allPlayers;
    }

    public IEnumerable<IPlayer> GetBots()
    {
        return GetAllPlayers().Where(p => p.IsFakeClient);
    }

    public IEnumerable<IPlayer> GetAlive()
    {
        return GetAllPlayers().Where(p => p.Pawn?.LifeState == (byte)LifeState_t.LIFE_ALIVE);
    }

    public IEnumerable<IPlayer> GetCT()
    {
        return GetAllPlayers().Where(p => p.Pawn?.TeamNum == (int)Team.CT);
    }

    public IEnumerable<IPlayer> GetT()
    {
        return GetAllPlayers().Where(p => p.Pawn?.TeamNum == (int)Team.T);
    }

    public IEnumerable<IPlayer> GetSpectators()
    {
        return GetAllPlayers().Where(p => p.Pawn?.TeamNum == (int)Team.Spectator);
    }

    public IEnumerable<IPlayer> GetInTeam( Team team )
    {
        return GetAllPlayers().Where(p => p.Pawn?.TeamNum == (int)team);
    }

    public IEnumerable<IPlayer> GetTAlive()
    {
        return GetAllPlayers().Where(p =>
            p.Pawn?.TeamNum == (int)Team.T && p.Pawn?.LifeState == (byte)LifeState_t.LIFE_ALIVE);
    }

    public IEnumerable<IPlayer> GetCTAlive()
    {
        return GetAllPlayers().Where(p =>
            p.Pawn?.TeamNum == (int)Team.CT && p.Pawn?.LifeState == (byte)LifeState_t.LIFE_ALIVE);
    }

    public void SendMessage( MessageType kind, string message, int htmlDuration = 5000 )
    {
        NativePlayerManager.SendMessage((int)kind, message, htmlDuration);
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

    public void SendMessage( MessageType kind, Func<IPlayer, ILocalizer, string> messageCallback )
    {
        var players = GetAllPlayers();
        foreach (var player in players)
        {
            var localizer = _translationService.GetPlayerLocalizer(player);
            player.SendMessage(kind, messageCallback(player, localizer));
        }
    }

    public void SendMessage( MessageType kind, Func<IPlayer, ILocalizer, string> messageCallback, int htmlDuration = 5000 )
    {
        var players = GetAllPlayers();
        foreach (var player in players)
        {
            var localizer = _translationService.GetPlayerLocalizer(player);
            player.SendMessage(kind, messageCallback(player, localizer), htmlDuration);
        }
    }

    public Task SendMessageAsync( MessageType kind, Func<IPlayer, ILocalizer, string> messageCallback )
    {
        return SchedulerManager.QueueOrNow(() => SendMessage(kind, messageCallback));
    }

    public Task SendMessageAsync( MessageType kind, Func<IPlayer, ILocalizer, string> messageCallback, int htmlDuration = 5000 )
    {
        return SchedulerManager.QueueOrNow(() => SendMessage(kind, messageCallback, htmlDuration));
    }
}