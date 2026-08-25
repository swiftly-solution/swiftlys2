namespace SwiftlyS2.Shared.Menu;

/// <summary>
/// The catalogue of available renderers and their component specialisations.
/// </summary>
/// <remarks>
/// Registrations made through a plugin's own menu service are released when that plugin unloads.
/// </remarks>
public interface IMenuRendererRegistry
{
    /// <summary>
    /// The ids of every currently registered renderer.
    /// </summary>
    public IReadOnlyList<string> RendererIds { get; }

    /// <summary>
    /// Adds a renderer, replacing any renderer already registered under the same id.
    /// </summary>
    /// <param name="renderer">The renderer to add.</param>
    /// <returns>A handle that removes the renderer when disposed.</returns>
    public IDisposable Register( IMenuRenderer renderer );

    /// <summary>
    /// Adds a renderer-specific way of drawing one component type.
    /// </summary>
    /// <param name="componentRenderer">The specialisation to add.</param>
    /// <returns>A handle that removes the specialisation when disposed.</returns>
    public IDisposable RegisterComponentRenderer( IComponentRenderer componentRenderer );

    /// <summary>
    /// Looks up a renderer by id.
    /// </summary>
    /// <param name="rendererId">The renderer id.</param>
    /// <param name="renderer">The renderer, when registered.</param>
    /// <returns><see langword="true"/> when the renderer exists.</returns>
    public bool TryGet( string rendererId, out IMenuRenderer renderer );

    /// <summary>
    /// Resolves the specialisation to use for a component on a renderer.
    /// </summary>
    /// <param name="rendererId">The renderer drawing the component.</param>
    /// <param name="componentType">The concrete component type.</param>
    /// <returns>The specialisation, or null when the component should draw itself.</returns>
    public IComponentRenderer? ResolveComponentRenderer( string rendererId, Type componentType );
}
