using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using SwiftlyS2.Shared.Menu;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.Menu;

internal sealed class MenuRuntime(
    MenuFrameComposer composer,
    MenuRenderDiagnostics diagnostics,
    MenuChatCapture chat,
    ILogger<MenuRuntime> logger )
{
    private static readonly MenuRegion[] Regions = Enum.GetValues<MenuRegion>();

    private readonly ConcurrentDictionary<int, MenuSession> sessions = new();

    public MenuChatCapture Chat => chat;

    public MenuSession? GetSession( int playerId ) => sessions.GetValueOrDefault(playerId);

    public void Attach( MenuSession session )
    {
        var playerId = session.Player.PlayerID;

        if (sessions.TryGetValue(playerId, out var existing) && !ReferenceEquals(existing, session))
        {
            var sameRenderer = string.Equals(
                existing.Instance.Renderer.Id,
                session.Instance.Renderer.Id,
                StringComparison.OrdinalIgnoreCase);

            Release(existing, returnToParent: false, clearRenderer: !sameRenderer);
        }

        sessions[playerId] = session;
        session.Invalidate();
    }

    public void Detach( int playerId ) => Detach(playerId, returnToParent: true);

    public void Detach( int playerId, bool returnToParent )
    {
        if (sessions.TryRemove(playerId, out var session))
        {
            Release(session, returnToParent);
        }
    }

    public void DetachSession( MenuSession session )
    {
        var playerId = session.Player.PlayerID;

        if (sessions.TryGetValue(playerId, out var current) && ReferenceEquals(current, session))
        {
            Detach(playerId, returnToParent: true);
        }
    }

    public void CloseByOwner( string owner )
    {
        foreach (var pair in sessions)
        {
            if (string.Equals(pair.Value.Instance.Owner, owner, StringComparison.Ordinal))
            {
                Detach(pair.Key, returnToParent: false);
            }
        }
    }

    public void CloseAll()
    {
        foreach (var playerId in sessions.Keys.ToList())
        {
            Detach(playerId, returnToParent: false);
        }
    }

    public void OnTick()
    {
        if (sessions.IsEmpty)
        {
            return;
        }

        var now = DateTime.UtcNow;

        foreach (var pair in sessions)
        {
            var session = pair.Value;

            if (!session.Player.IsValid)
            {
                Detach(pair.Key);
                continue;
            }

            if (!session.IsDirty && !WantsRedraw(session, now))
            {
                continue;
            }

            session.ClearDirty();
            Draw(session);
        }
    }

    private bool WantsRedraw( MenuSession session, DateTime now )
    {
        foreach (var region in Regions)
        {
            foreach (var component in session.Instance.GetVisible(region, session))
            {
                try
                {
                    if (component.NeedsRedraw(session, now))
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Menu component '{ComponentId}' threw while reporting redraw.", component.Id);
                }
            }
        }

        return false;
    }

    public void OnClientDisconnected( int playerId )
    {
        if (sessions.TryRemove(playerId, out var session))
        {
            session.IsOpen = false;
            chat.Release(playerId);
            session.Instance.NotifyClosed(session);
        }
    }

    private void Draw( MenuSession session )
    {
        try
        {
            var frame = composer.Compose(session);
            var renderer = session.Instance.Renderer;
            var context = new MenuRenderContext(session.Instance, session, frame, renderer.Id, diagnostics);
            renderer.Render(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to render menu '{MenuId}' for player {PlayerId}.", session.Instance.Id, session.Player.PlayerID);
        }
    }

    private void Release( MenuSession session, bool returnToParent, bool clearRenderer = true )
    {
        session.IsOpen = false;
        chat.Release(session.Player.PlayerID);

        if (clearRenderer)
        {
            try
            {
                session.Instance.Renderer.Clear(session.Player);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to clear menu '{MenuId}' for player {PlayerId}.", session.Instance.Id, session.Player.PlayerID);
            }
        }

        session.Instance.NotifyClosed(session);

        if (returnToParent && session.Instance.Parent is { } parent && session.Player.IsValid)
        {
            _ = parent.Open(session.Player);
        }
    }
}
