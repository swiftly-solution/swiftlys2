using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.SchemaDefinitions;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.Services;

namespace SwiftlyS2.Core.Players;

internal class PlayerManagerService : IPlayerManagerService
{
    public int PlayerCount => NativePlayerManager.GetPlayerCount();

    public int PlayerCap => NativePlayerManager.GetPlayerCap();

    public void ClearAllBlockedTransmitEntities()
    {
        NativePlayerManager.ClearAllBlockedTransmitEntity();
    }

    public IPlayer GetPlayer( int playerid )
    {
        return new Player(playerid);
    }

    public bool IsPlayerOnline( int playerid )
    {
        return NativePlayerManager.IsPlayerOnline(playerid);
    }

    public void SendMessage( MessageType kind, string message )
    {
        NativePlayerManager.SendMessage((int)kind, message, 5000);
    }

    public void ShouldBlockTransmitEntity( int entityid, bool shouldBlockTransmit )
    {
        NativePlayerManager.ShouldBlockTransmitEntity(entityid, shouldBlockTransmit);
    }

    public IEnumerable<IPlayer> GetAllPlayers()
    {
        return Enumerable.Range(0, PlayerCap)
            .Where(IsPlayerOnline)
            .Select(GetPlayer);
    }

    private static ulong SteamIDToSteamID64( string steamID )
    {
        string[] parts = steamID.Split(':');
        if (parts.Length != 3) return 0;

        int X = int.Parse(parts[1]);
        int Y = int.Parse(parts[2]);

        ulong steamID64 = (ulong)Y * 2 + (ulong)X + 76561197960265728UL;
        return steamID64;
    }


    public IEnumerable<IPlayer> FindTargettedPlayers( IPlayer player, string target, TargetSearchMode searchMode )
    {
        IEnumerable<IPlayer> allPlayers = [];

        for (int i = 0; i < PlayerCap; i++)
        {
            if (!IsPlayerOnline(i))
                continue;

            IPlayer targetPlayer = GetPlayer(i);

            if (searchMode.HasFlag(TargetSearchMode.NoBots) && targetPlayer.IsFakeClient)
                continue;

            if (searchMode.HasFlag(TargetSearchMode.IncludeSelf) == false && targetPlayer.PlayerID == player.PlayerID)
                continue;

            if (searchMode.HasFlag(TargetSearchMode.Alive) && targetPlayer.Pawn?.LifeState != (byte)LifeState_t.LIFE_ALIVE)
                continue;

            if (searchMode.HasFlag(TargetSearchMode.Dead) && targetPlayer.Pawn?.LifeState != (byte)LifeState_t.LIFE_DEAD)
                continue;

            if (searchMode.HasFlag(TargetSearchMode.TeamOnly) && targetPlayer.Pawn?.TeamNum != player.Pawn?.TeamNum)
                continue;

            if (searchMode.HasFlag(TargetSearchMode.OppositeTeamOnly) && targetPlayer.Pawn?.TeamNum == player.Pawn?.TeamNum)
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

                if (pickerEntity != null && pickerEntity.DesignerName == "player")
                {
                    var entIndex = pickerEntity.OriginalController.Value?.Entity?.EntityHandle.EntityIndex;
                    if (entIndex.HasValue)
                    {
                        allPlayers = allPlayers.Append(GetPlayer((int)entIndex.Value - 1));
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
            else if (targetPlayer.Controller.PlayerName.Contains(target))
            {
                allPlayers = allPlayers.Append(targetPlayer);
            }
            else if (ulong.TryParse(target, out ulong steamId) && targetPlayer.SteamID == steamId)
            {
                allPlayers = allPlayers.Append(targetPlayer);
            }
            else if (SteamIDToSteamID64(target) == targetPlayer.SteamID)
            {
                allPlayers = allPlayers.Append(targetPlayer);
            }
        }

        return allPlayers;
    }

    public void SendMessage( MessageType kind, string message, int htmlDuration = 5000 )
    {
        NativePlayerManager.SendMessage((int)kind, message, htmlDuration);
    }

    public void SendNotify( string message )
    {
        SendMessage(MessageType.Notify, message);
    }

    public void SendConsole( string message )
    {
        SendMessage(MessageType.Console, message);
    }

    public void SendChat( string message )
    {
        SendMessage(MessageType.Chat, message);
    }

    public void SendCenter( string message )
    {
        SendMessage(MessageType.Center, message);
    }

    public void SendAlert( string message )
    {
        SendMessage(MessageType.Alert, message);
    }

    public void SendCenterHTML( string message, int duration = 5000 )
    {
        SendMessage(MessageType.CenterHTML, message, duration);
    }

    public void SendChatEOT( string message )
    {
        SendMessage(MessageType.ChatEOT, message);
    }
}