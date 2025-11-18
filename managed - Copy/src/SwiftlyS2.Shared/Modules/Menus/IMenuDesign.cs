// using SwiftlyS2.Shared.Natives;

// namespace SwiftlyS2.Shared.Menus;

// [Obsolete("IMenuDesign will be deprecared at the release of SwiftlyS2. Please use IMenuDesignAPI instead")]
// public interface IMenuDesign
// {
//     /// <summary>
//     /// Overrides the default button(s) used for selecting menu options.
//     /// Allows customization of the input controls for menu interaction.
//     /// </summary>
//     /// <param name="buttonNames">The names of the buttons to use for selection.</param>
//     /// <returns>The current menu design instance for method chaining.</returns>
//     IMenuDesign OverrideSelectButton( params string[] buttonNames );

//     /// <summary>
//     /// Overrides the default button(s) used for moving through menu options.
//     /// Allows customization of the input controls for menu navigation.
//     /// </summary>
//     /// <param name="buttonNames">The names of the buttons to use for movement.</param>
//     /// <returns>The current menu design instance for method chaining.</returns>
//     IMenuDesign OverrideMoveButton( params string[] buttonNames );

//     /// <summary>
//     /// Overrides the default button(s) used for exiting or closing the menu.
//     /// Allows customization of the input controls for menu exit.
//     /// </summary>
//     /// <param name="buttonNames">The names of the buttons to use for exiting.</param>
//     /// <returns>The current menu design instance for method chaining.</returns>
//     IMenuDesign OverrideExitButton( params string[] buttonNames );

//     /// <summary>
//     /// Sets the maximum number of menu items visible at once.
//     /// When there are more items than this limit, the menu will be paginated.
//     /// </summary>
//     /// <param name="count">The maximum number of visible items.</param>
//     /// <returns>The current menu design instance for method chaining.</returns>
//     /// <remarks>
//     /// If the provided count is less than 1, it will be clamped to 1.
//     /// If the provided count is greater than 5, it will be clamped to 5.
//     /// A warning will be logged when clamping occurs.
//     /// </remarks>
//     IMenuDesign MaxVisibleItems( int count );

//     /// <summary>
//     /// Sets the color used for rendering the menu.
//     /// Affects the visual appearance and styling of the menu display.
//     /// </summary>
//     /// <param name="color">The color to use for menu rendering.</param>
//     /// <returns>The current menu design instance for method chaining.</returns>
//     IMenuDesign SetColor( Color color );

//     /// <summary>
//     /// Sets the vertical scroll style for the menu navigation.
//     /// </summary>
//     /// <param name="style">The vertical scroll style to use.</param>
//     /// <returns>The current menu design instance for method chaining.</returns>
//     IMenuDesign SetVerticalScrollStyle( MenuVerticalScrollStyle style );

//     /// <summary>
//     /// Sets the global horizontal style for menu option text display.
//     /// Controls maximum text width and overflow behavior for all menu options.
//     /// </summary>
//     /// <param name="style">The global horizontal style to apply.</param>
//     /// <returns>The current menu design instance for method chaining.</returns>
//     IMenuDesign SetGlobalHorizontalStyle( MenuHorizontalStyle style );
// }