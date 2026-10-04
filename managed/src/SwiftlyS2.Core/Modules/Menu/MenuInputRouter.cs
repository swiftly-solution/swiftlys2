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

        if (session is null || !session.IsOpen || !session.Player.IsValid || session.Instance.InputMethod != MenuInputMethod.Buttons)
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

    public bool OnClientChat( int playerId, string text )
    {
        var session = runtime.GetSession(playerId);

        if (session is null || !session.IsOpen || !session.Player.IsValid || session.Instance.InputMethod != MenuInputMethod.Chat)
        {
            return false;
        }

        var command = text.Trim().Trim('"');

        if (command.Length < 2 || command[0] != '!' || !int.TryParse(command.AsSpan(1), out var number) || number < 0)
        {
            return false;
        }

        if (number == 0)
        {
            Dispatch(session, new MenuActionContext {
                Action = MenuActions.Close,
                Key = MenuKey.None,
                Session = session
            });

            return true;
        }

        var selectables = session.PageSelectables;

        if (number > selectables.Count)
        {
            sounds.Play(MenuSound.Fail, playerId);
            return true;
        }

        var target = selectables[number - 1];
        var index = session.Instance.IndexOfFocusable(session, target);

        if (index < 0)
        {
            sounds.Play(MenuSound.Fail, playerId);
            return true;
        }

        _ = session.SetFocus(index);

        Dispatch(session, new MenuActionContext {
            Action = MenuActions.Select,
            Key = MenuKey.None,
            Session = session
        }, target);

        return true;
    }

    private void PlaySound( MenuSession session, MenuActionContext context, IMenuComponent? component )
    {
        var name = context.Action.Name;
        var playerId = session.Player.PlayerID;

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

    private void Dispatch( MenuSession session, MenuActionContext context, IMenuComponent? target = null )
    {
        var component = target ?? session.FocusedComponent;

        if (component is null)
        {
            _ = DispatchUnhandled(session, context, null);
            return;
        }

        try
        {
            var pending = component.HandleActionAsync(context);

            if (pending.IsCompletedSuccessfully)
            {
                if (pending.Result)
                {
                    PlaySound(session, context, component);
                    return;
                }

                _ = DispatchUnhandled(session, context, component);
                return;
            }

            _ = AwaitDispatch(pending, session, context, component);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Menu component '{ComponentId}' threw while handling '{Action}'.", component.Id, context.Action);
        }
    }

    private async Task AwaitDispatch( ValueTask<bool> pending, MenuSession session, MenuActionContext context, IMenuComponent component )
    {
        try
        {
            if (await pending)
            {
                PlaySound(session, context, component);
                return;
            }

            await DispatchUnhandled(session, context, component);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Menu component threw while handling '{Action}'.", context.Action);
        }
    }

    private async Task DispatchUnhandled( MenuSession session, MenuActionContext context, IMenuComponent? component )
    {
        if (ApplyDefault(session, context))
        {
            PlaySound(session, context, component);
            return;
        }

        if (!session.Instance.TryGetActionHandler(context.Action.Name, out var handler))
        {
            return;
        }

        try
        {
            await handler(context);
            sounds.Play(MenuSound.Select, session.Player.PlayerID);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Menu '{MenuId}' threw while handling custom action '{Action}'.", session.Instance.Id, context.Action);
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
