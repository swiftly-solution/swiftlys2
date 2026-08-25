namespace SwiftlyS2.Shared.Menu;

/// <summary>
/// The renderer-agnostic output of a component.
/// </summary>
/// <remarks>
/// Components describe what they want shown as a small tree of these primitives rather than
/// producing markup directly. Because every renderer walks the same tree, a renderer can draw a
/// component it has never heard of, and a component works on a renderer that did not exist when it
/// was written.
/// <para>
/// A renderer that meets a node type it does not understand is expected to skip it and report it
/// once, never to fail.
/// </para>
/// </remarks>
public abstract record MenuNode;

/// <summary>
/// A run of styled text.
/// </summary>
/// <param name="Text">The text to display.</param>
/// <param name="Style">Presentation hints for the text.</param>
public sealed record MenuTextNode( string Text, MenuTextStyle Style ) : MenuNode
{
    /// <summary>
    /// Creates a text node using <see cref="MenuTextStyle.Default"/>.
    /// </summary>
    /// <param name="text">The text to display.</param>
    public MenuTextNode( string text ) : this(text, MenuTextStyle.Default) { }
}

/// <summary>
/// A single visual line composed of several nodes laid out horizontally.
/// </summary>
/// <param name="Children">The nodes making up the line.</param>
public sealed record MenuLineNode( IReadOnlyList<MenuNode> Children ) : MenuNode
{
    /// <summary>
    /// Creates a line from a sequence of nodes.
    /// </summary>
    /// <param name="children">The nodes making up the line.</param>
    /// <returns>The line node.</returns>
    public static MenuLineNode Of( params MenuNode[] children ) => new(children);
}

/// <summary>
/// Several nodes stacked vertically, each on its own line.
/// </summary>
/// <param name="Children">The stacked nodes.</param>
/// <remarks>
/// Used by components that occupy more than one line.
/// </remarks>
public sealed record MenuStackNode( IReadOnlyList<MenuNode> Children ) : MenuNode
{
    /// <summary>
    /// Creates a stack from a sequence of nodes.
    /// </summary>
    /// <param name="children">The stacked nodes.</param>
    /// <returns>The stack node.</returns>
    public static MenuStackNode Of( params MenuNode[] children ) => new(children);
}

/// <summary>
/// Vertical empty space.
/// </summary>
/// <param name="Lines">How many blank lines to reserve.</param>
public sealed record MenuBlankNode( int Lines = 1 ) : MenuNode;

/// <summary>
/// The selection marker slot for a body entry.
/// </summary>
/// <param name="Focused">Whether the entry this marker belongs to currently has focus.</param>
/// <remarks>
/// Emitting a marker slot rather than a literal arrow keeps the choice of indicator with the
/// renderer, so a text renderer and a graphical renderer can indicate selection differently
/// without any component changing.
/// </remarks>
public sealed record MenuSelectionNode( bool Focused ) : MenuNode;

/// <summary>
/// Output aimed at one specific renderer.
/// </summary>
/// <param name="RendererId">The id of the renderer this payload is meant for.</param>
/// <param name="Payload">The renderer-specific payload.</param>
/// <remarks>
/// An escape hatch for components that want to use a capability the shared node set cannot
/// express. Renderers ignore raw nodes addressed to a different id, so using one never breaks
/// rendering elsewhere.
/// </remarks>
public sealed record MenuRawNode( string RendererId, string Payload ) : MenuNode;
