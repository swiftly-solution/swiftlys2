using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.Menu;

/// <summary>
/// Turns a composed menu frame into output and delivers it to a player.
/// </summary>
/// <remarks>
/// A renderer never inspects concrete component types. It walks <see cref="MenuNode"/> trees, which
/// is what lets a renderer added by a plugin draw the built-in components and vice versa.
/// </remarks>
public interface IMenuRenderer
{
    /// <summary>
    /// The unique id menus use to select this renderer.
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// Draws a frame for a player.
    /// </summary>
    /// <param name="context">The frame and its surrounding state.</param>
    /// <remarks>
    /// Called on a single dedicated background thread, not the game thread - renders for one
    /// session are never reordered relative to each other, but never assume this runs where the
    /// rest of the framework does. <paramref name="context"/> already carries everything the frame
    /// needs; a renderer should limit itself to that and to calls its underlying transport
    /// documents as safe off the game thread (such as sending text to the client), never reach back
    /// into live player, controller or pawn state itself.
    /// </remarks>
    public void Render( IMenuRenderContext context );

    /// <summary>
    /// Removes anything this renderer is currently showing to a player.
    /// </summary>
    /// <param name="player">The player to clear.</param>
    /// <remarks>
    /// Called on the game thread, unlike <see cref="Render"/> - it runs on session teardown, not on
    /// the per-tick hot path the background thread exists for.
    /// </remarks>
    public void Clear( IPlayer player );
}

/// <summary>
/// The ids of the renderers shipped with the framework.
/// </summary>
public static class MenuRendererIds
{
    /// <summary>
    /// The default renderer, drawing menus as centre-screen HTML.
    /// </summary>
    public const string CenterHtml = "centerhtml";

    /// <summary>
    /// Draws menus as a block of chat lines.
    /// </summary>
    public const string Chat = "chat";
}
