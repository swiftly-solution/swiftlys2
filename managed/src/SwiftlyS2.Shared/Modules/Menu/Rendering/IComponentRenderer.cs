namespace SwiftlyS2.Shared.Menu;

/// <summary>
/// An optional, renderer-specific way of drawing one component type.
/// </summary>
/// <remarks>
/// Every component can already draw itself through <see cref="IMenuComponent.Render"/>, so this
/// exists purely to let a renderer produce richer output for a component it knows well. When no
/// component renderer is registered for a pair, the component's own output is used, which is why an
/// unrecognised component is never an error.
/// <para>
/// Registering one of these is also how a plugin teaches a renderer it does not own about a
/// component that renderer has never seen.
/// </para>
/// </remarks>
public interface IComponentRenderer
{
    /// <summary>
    /// The renderer this specialisation applies to.
    /// </summary>
    public string RendererId { get; }

    /// <summary>
    /// The component type this specialisation applies to. Derived types match as well.
    /// </summary>
    public Type ComponentType { get; }

    /// <summary>
    /// Attempts to draw a component.
    /// </summary>
    /// <param name="component">The component to draw.</param>
    /// <param name="context">The component's render context.</param>
    /// <param name="node">The produced node, when handled.</param>
    /// <returns>
    /// <see langword="false"/> to decline, which falls back to the component's own rendering.
    /// </returns>
    public bool TryRender( IMenuComponent component, IMenuComponentRenderContext context, out MenuNode? node );
}
