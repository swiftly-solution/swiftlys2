using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.Menu;

/// <summary>
/// Turns a composed menu frame into output and delivers it to a player.
/// </summary>
/// <remarks>
/// A renderer never inspects concrete component types. It walks <see cref="MenuNode"/> trees, which
/// is what lets a renderer added by a plugin draw the built-in components and vice versa.
/// <para>
/// Implementations are called on the game thread once per changed frame.
/// </para>
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
    public void Render( IMenuRenderContext context );

    /// <summary>
    /// Removes anything this renderer is currently showing to a player.
    /// </summary>
    /// <param name="player">The player to clear.</param>
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
