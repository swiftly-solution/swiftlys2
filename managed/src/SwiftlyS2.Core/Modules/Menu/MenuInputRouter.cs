using Microsoft.Extensions.Logging;
using SwiftlyS2.Shared.Events;
using SwiftlyS2.Shared.Menu;

namespace SwiftlyS2.Core.Menu;

internal sealed class MenuInputRouter( MenuRuntime runtime, MenuSoundPlayer sounds, ILogger<MenuInputRouter> logger )
{
    public void OnClientKeyStateChanged( IOnClientKeyStateChangedEvent @event )
    {
        if (!@event.Pressed)
        {
            return;
        }

        var session = runtime.GetSession(@event.PlayerId);

        if (session is null || !session.IsOpen || !session.Player.IsValid)
        {
            return;
        }

        var key = MenuKeys.FromKeyKind(@event.Key);

        if (key == MenuKey.None)
        {
            return;
        }

        if (!session.Instance.Keymap.TryResolve(key, out var action))
        {
            return;
        }

        var context = new MenuActionContext {
            Action = action,
            Key = key,
            Session = session
        };

        Dispatch(session, context);
    }

    private void PlaySound( MenuSession session, MenuActionContext context )
    {
        var name = context.Action.Name;
        var playerId = session.Player.PlayerID;

        // Moves the selection itself: never blocked by the target's enabled state.
        if (string.Equals(name, MenuActions.NavigateUp.Name, StringComparison.OrdinalIgnoreCase)
            || string.Equals(name, MenuActions.NavigateDown.Name, StringComparison.OrdinalIgnoreCase))
        {
            sounds.Play(MenuSound.Scroll, playerId);
            return;
        }

        if (string.Equals(name, MenuActions.Close.Name, StringComparison.OrdinalIgnoreCase))
        {
            sounds.Play(MenuSound.Exit, playerId);
            return;
        }

        // Everything else - Select, NavigateLeft/Right, and any custom action - is an attempted
        // interaction with the focused component, so a disabled component always fails audibly.
        var component = session.FocusedComponent;

        if (component is null)
        {
            return;
        }

        if (!component.IsEnabled(session))
        {
            sounds.Play(MenuSound.Fail, playerId);
            return;
        }

        if (string.Equals(name, MenuActions.NavigateLeft.Name, StringComparison.OrdinalIgnoreCase)
            || string.Equals(name, MenuActions.NavigateRight.Name, StringComparison.OrdinalIgnoreCase))
        {
            sounds.Play(MenuSound.Scroll, playerId);
            return;
        }

        if (component.PlaySound)
        {
            sounds.Play(MenuSound.Select, playerId);
        }
    }

    private void Dispatch( MenuSession session, MenuActionContext context )
    {
        var component = session.FocusedComponent;

        if (component is null)
        {
            if (ApplyDefault(session, context))
            {
                PlaySound(session, context);
            }

            return;
        }

        try
        {
            var pending = component.HandleActionAsync(context);

            if (pending.IsCompletedSuccessfully)
            {
                var handled = pending.Result || ApplyDefault(session, context);

                if (handled)
                {
                    PlaySound(session, context);
                }

                return;
            }

            _ = AwaitDispatch(pending, session, context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Menu component '{ComponentId}' threw while handling '{Action}'.", component.Id, context.Action);
        }
    }

    private async Task AwaitDispatch( ValueTask<bool> pending, MenuSession session, MenuActionContext context )
    {
        try
        {
            var handled = await pending || ApplyDefault(session, context);

            if (handled)
            {
                PlaySound(session, context);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Menu component threw while handling '{Action}'.", context.Action);
        }
    }

    private bool ApplyDefault( MenuSession session, MenuActionContext context )
    {
        var name = context.Action.Name;

        if (string.Equals(name, MenuActions.NavigateUp.Name, StringComparison.OrdinalIgnoreCase))
        {
            return session.MoveFocus(-1);
        }

        if (string.Equals(name, MenuActions.NavigateDown.Name, StringComparison.OrdinalIgnoreCase))
        {
            return session.MoveFocus(1);
        }

        if (string.Equals(name, MenuActions.Close.Name, StringComparison.OrdinalIgnoreCase))
        {
            runtime.Detach(session.Player.PlayerID);
            return true;
        }

        return false;
    }
}
