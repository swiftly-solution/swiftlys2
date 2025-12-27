namespace SwiftlyS2.Shared.Menus;

public interface IMenuDesignAPI
{
    /// <summary>
    /// Sets the title text displayed at the top of the menu.
    /// </summary>
    /// <param name="title">The title text. Pass null to clear the title.</param>
    /// <returns>The menu builder for method chaining.</returns>
    public IMenuBuilderAPI SetMenuTitle( string? title = null );

    /// <summary>
    /// Controls the visibility of the menu title.
    /// </summary>
    /// <param name="visible">True to show the title, false to hide it. Default is true.</param>
    /// <returns>The menu builder for method chaining.</returns>
    public IMenuBuilderAPI SetMenuTitleVisible( bool visible = true );

    /// <summary>
    /// Controls the visibility of the item count in the menu title.
    /// </summary>
    /// <param name="visible">True to show the item count, false to hide it. Default is true.</param>
    /// <returns>The menu builder for method chaining.</returns>
    public IMenuBuilderAPI SetMenuTitleItemCountVisible( bool visible = true );

    /// <summary>
    /// Controls the visibility of the menu footer.
    /// </summary>
    /// <param name="visible">True to show the footer, false to hide it. Default is true.</param>
    /// <returns>The menu builder for method chaining.</returns>
    public IMenuBuilderAPI SetMenuFooterVisible( bool visible = true );

    /// <summary>
    /// Sets the maximum number of menu options visible on screen at once.
    /// </summary>
    /// <param name="count">The maximum visible item count. Valid range is 1-5. Default is 5.</param>
    /// <returns>The menu builder for method chaining.</returns>
    /// <remarks>
    /// Values outside the range of 1-5 will be automatically clamped to the nearest valid value.
    /// Menus with more options than this limit will be paginated.
    /// </remarks>
    public IMenuBuilderAPI SetMaxVisibleItems( int count = 5 );

    /// <summary>
    /// Enables automatic adjustment of visible items when title or footer is hidden.
    /// </summary>
    /// <returns>The menu builder for method chaining.</returns>
    /// <remarks>
    /// When enabled, hiding the title or footer will increase the effective visible item count
    /// during rendering without modifying the configured <see cref="SetMaxVisibleItems"/> value.
    /// </remarks>
    public IMenuBuilderAPI EnableAutoAdjustVisibleItems();

    /// <summary>
    /// Disables automatic adjustment of visible items when title or footer is hidden.
    /// </summary>
    /// <returns>The menu builder for method chaining.</returns>
    public IMenuBuilderAPI DisableAutoAdjustVisibleItems();

    /// <summary>
    /// Sets the default scroll animation style for all menu options.
    /// </summary>
    /// <param name="style">The scroll style to apply globally.</param>
    /// <returns>The menu builder for method chaining.</returns>
    /// <remarks>
    /// Individual options can override this global setting.
    /// </remarks>
    public IMenuBuilderAPI SetGlobalScrollStyle( MenuOptionScrollStyle style );

    /// <summary>
    /// Sets the color of the navigation markers using hex color format.
    /// </summary>
    /// <param name="hexColor">The color in hex format (e.g., "#FFFFFF" for white). Pass null to reset to default.</param>
    /// <returns>The menu builder for method chaining.</returns>
    public IMenuBuilderAPI SetNavigationMarkerColor( string? hexColor = null );

    /// <summary>
    /// Sets the color of the navigation markers using SwiftlyS2 native color.
    /// </summary>
    /// <param name="color">The SwiftlyS2 native color to apply to the navigation markers.</param>
    /// <returns>The menu builder for method chaining.</returns>
    public IMenuBuilderAPI SetNavigationMarkerColor( Natives.Color color );

    /// <summary>
    /// Sets the color of the navigation markers using System.Drawing color.
    /// </summary>
    /// <param name="color">The System.Drawing color to apply to the navigation markers.</param>
    /// <returns>The menu builder for method chaining.</returns>
    public IMenuBuilderAPI SetNavigationMarkerColor( System.Drawing.Color color );

    /// <summary>
    /// Sets the color of the menu footer using hex color format.
    /// </summary>
    /// <param name="hexColor">The color in hex format (e.g., "#FF0000" for red). Pass null to reset to default.</param>
    /// <returns>The menu builder for method chaining.</returns>
    public IMenuBuilderAPI SetMenuFooterColor( string? hexColor = null );

    /// <summary>
    /// Sets the color of the menu footer using SwiftlyS2 native color.
    /// </summary>
    /// <param name="color">The SwiftlyS2 native color to apply to the footer.</param>
    /// <returns>The menu builder for method chaining.</returns>
    public IMenuBuilderAPI SetMenuFooterColor( Natives.Color color );

    /// <summary>
    /// Sets the color of the menu footer using System.Drawing color.
    /// </summary>
    /// <param name="color">The System.Drawing color to apply to the footer.</param>
    /// <returns>The menu builder for method chaining.</returns>
    public IMenuBuilderAPI SetMenuFooterColor( System.Drawing.Color color );

    /// <summary>
    /// Sets the color of the visual guide lines using hex color format.
    /// </summary>
    /// <param name="hexColor">The color in hex format (e.g., "#FFFFFF" for white). Pass null to reset to default.</param>
    /// <returns>The menu builder for method chaining.</returns>
    public IMenuBuilderAPI SetVisualGuideLineColor( string? hexColor = null );

    /// <summary>
    /// Sets the color of the visual guide lines using SwiftlyS2 native color.
    /// </summary>
    /// <param name="color">The SwiftlyS2 native color to apply to the guide lines.</param>
    /// <returns>The menu builder for method chaining.</returns>
    public IMenuBuilderAPI SetVisualGuideLineColor( Natives.Color color );

    /// <summary>
    /// Sets the color of the visual guide lines using System.Drawing color.
    /// </summary>
    /// <param name="color">The System.Drawing color to apply to the guide lines.</param>
    /// <returns>The menu builder for method chaining.</returns>
    public IMenuBuilderAPI SetVisualGuideLineColor( System.Drawing.Color color );

    /// <summary>
    /// Sets the color of disabled menu options using hex color format.
    /// </summary>
    /// <param name="hexColor">The color in hex format (e.g., "#808080" for gray). Pass null to reset to default.</param>
    /// <returns>The menu builder for method chaining.</returns>
    public IMenuBuilderAPI SetDisabledColor( string? hexColor = null );

    /// <summary>
    /// Sets the color of disabled menu options using SwiftlyS2 native color.
    /// </summary>
    /// <param name="color">The SwiftlyS2 native color to apply to disabled options.</param>
    /// <returns>The menu builder for method chaining.</returns>
    public IMenuBuilderAPI SetDisabledColor( Natives.Color color );

    /// <summary>
    /// Sets the color of disabled menu options using System.Drawing color.
    /// </summary>
    /// <param name="color">The System.Drawing color to apply to disabled options.</param>
    /// <returns>The menu builder for method chaining.</returns>
    public IMenuBuilderAPI SetDisabledColor( System.Drawing.Color color );

    // /// <summary>
    // /// Sets the global option text style for the menu.
    // /// </summary>
    // /// <param name="style">The text style to apply to all options in the menu.</param>
    // /// <returns>The menu builder for method chaining.</returns>
    // public IMenuBuilderAPI SetGlobalOptionTextStyle( MenuOptionTextStyle style );
}