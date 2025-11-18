// using SwiftlyS2.Shared.Natives;
// using SwiftlyS2.Shared.Players;

// namespace SwiftlyS2.Shared.Menus;

// /// <summary>
// /// Provides a fluent interface for building and configuring menus with various option types and behaviors.
// /// Supports method chaining for easy menu construction and customization.
// /// </summary>
// [Obsolete("IMenuBuilder will be deprecared at the release of SwiftlyS2. Please use IMenuBuilderAPI instead")]
// public interface IMenuBuilder
// {
//     IMenuDesign Design { get; }

//     /// <summary>
//     /// Sets the menu instance that this builder will modify.
//     /// This method is typically called internally to associate the builder with a specific menu.
//     /// </summary>
//     /// <param name="menu">The menu instance to build and configure.</param>
//     /// <returns>The current menu builder instance for method chaining.</returns>
//     IMenuBuilder SetMenu( IMenu menu );

//     /// <summary>
//     /// Sets whether the menu should have associated sounds for interactions.
//     /// </summary>
//     /// <param name="hasSound">Enables/disables sound effects for menu interactions.</param>
//     /// <returns>The current menu builder instance for method chaining.</returns>
//     IMenuBuilder HasSound( bool hasSound );

//     /// <summary>
//     /// Adds a clickable button option to the menu.
//     /// When selected, executes the provided action with the player as parameter.
//     /// </summary>
//     /// <param name="text">The display text for the button.</param>
//     /// <param name="onClick">Optional action to execute when the button is clicked. Receives the player as parameter.</param>
//     /// <param name="size">The text size for the button display. Defaults to Medium.</param>
//     /// <param name="overflowStyle">The overflow style for the text. Defaults to null.</param>
//     /// <returns>The current menu builder instance for method chaining.</returns>
//     IMenuBuilder AddButton( string text, Action<IPlayer>? onClick = null, IMenuTextSize size = IMenuTextSize.Medium, MenuHorizontalStyle? overflowStyle = null );

//     /// <summary>
//     /// Adds a clickable button option to the menu.
//     /// When selected, executes the provided action with the player and option as parameters.
//     /// </summary>
//     /// <param name="text">The display text for the button.</param>
//     /// <param name="onClick">Optional action to execute when the button is clicked. Receives the player and option as parameters.</param>
//     /// <param name="size">The text size for the button display. Defaults to Medium.</param>
//     /// <param name="overflowStyle">The overflow style for the text. Defaults to null.</param>
//     /// <returns>The current menu builder instance for method chaining.</returns>
//     IMenuBuilder AddButton( string text, Action<IPlayer, IOption>? onClick, IMenuTextSize size = IMenuTextSize.Medium, MenuHorizontalStyle? overflowStyle = null );

//     /// <summary>
//     /// Adds a toggle switch option to the menu that can be turned on or off.
//     /// Players can interact with this option to change its boolean state.
//     /// </summary>
//     /// <param name="text">The display text for the toggle option.</param>
//     /// <param name="defaultValue">The initial state of the toggle. Defaults to false.</param>
//     /// <param name="onToggle">Optional action to execute when the toggle state changes. Receives the player and new boolean value.</param>
//     /// <param name="size">The text size for the toggle display. Defaults to Medium.</param>
//     /// <param name="overflowStyle">The overflow style for the text. Defaults to null.</param>
//     /// <returns>The current menu builder instance for method chaining.</returns>
//     IMenuBuilder AddToggle( string text, bool defaultValue = false, Action<IPlayer, bool>? onToggle = null, IMenuTextSize size = IMenuTextSize.Medium, MenuHorizontalStyle? overflowStyle = null );

//     /// <summary>
//     /// Adds a toggle switch option to the menu that can be turned on or off.
//     /// Players can interact with this option to change its boolean state.
//     /// </summary>
//     /// <param name="text">The display text for the toggle option.</param>
//     /// <param name="defaultValue">The initial state of the toggle. Defaults to false.</param>
//     /// <param name="onToggle">Optional action to execute when the toggle state changes. Receives the player, option, and new boolean value.</param>
//     /// <param name="size">The text size for the toggle display. Defaults to Medium.</param>
//     /// <param name="overflowStyle">The overflow style for the text. Defaults to null.</param>
//     /// <returns>The current menu builder instance for method chaining.</returns>
//     IMenuBuilder AddToggle( string text, bool defaultValue, Action<IPlayer, IOption, bool>? onToggle, IMenuTextSize size = IMenuTextSize.Medium, MenuHorizontalStyle? overflowStyle = null );

//     /// <summary>
//     /// Adds a slider option to the menu for selecting numeric values within a specified range.
//     /// Players can adjust the value using left/right navigation.
//     /// </summary>
//     /// <param name="text">The display text for the slider option.</param>
//     /// <param name="min">The minimum allowed value for the slider.</param>
//     /// <param name="max">The maximum allowed value for the slider.</param>
//     /// <param name="defaultValue">The initial value of the slider.</param>
//     /// <param name="step">The increment/decrement step size. Defaults to 1.</param>
//     /// <param name="onChange">Optional action to execute when the slider value changes. Receives the player and new float value.</param>
//     /// <param name="size">The text size for the slider display. Defaults to Medium.</param>
//     /// <param name="overflowStyle">The overflow style for the text. Defaults to null.</param>
//     /// <returns>The current menu builder instance for method chaining.</returns>
//     IMenuBuilder AddSlider( string text, float min, float max, float defaultValue, float step = 1, Action<IPlayer, float>? onChange = null, IMenuTextSize size = IMenuTextSize.Medium, MenuHorizontalStyle? overflowStyle = null );

//     /// <summary>
//     /// Adds a slider option to the menu for selecting numeric values within a specified range.
//     /// Players can adjust the value using left/right navigation.
//     /// </summary>
//     /// <param name="text">The display text for the slider option.</param>
//     /// <param name="min">The minimum allowed value for the slider.</param>
//     /// <param name="max">The maximum allowed value for the slider.</param>
//     /// <param name="defaultValue">The initial value of the slider.</param>
//     /// <param name="step">The increment/decrement step size.</param>
//     /// <param name="onChange">Optional action to execute when the slider value changes. Receives the player, option, and new float value.</param>
//     /// <param name="size">The text size for the slider display. Defaults to Medium.</param>
//     /// <param name="overflowStyle">The overflow style for the text. Defaults to null.</param>
//     /// <returns>The current menu builder instance for method chaining.</returns>
//     IMenuBuilder AddSlider( string text, float min, float max, float defaultValue, float step, Action<IPlayer, IOption, float>? onChange, IMenuTextSize size = IMenuTextSize.Medium, MenuHorizontalStyle? overflowStyle = null );

//     /// <summary>
//     /// Adds an asynchronous button option to the menu that executes async operations.
//     /// When selected, executes the provided async function with the player as parameter.
//     /// </summary>
//     /// <param name="text">The display text for the async button.</param>
//     /// <param name="onClickAsync">The async function to execute when the button is clicked. Receives the player as parameter.</param>
//     /// <param name="size">The text size for the button display. Defaults to Medium.</param>
//     /// <param name="overflowStyle">The overflow style for the text. Defaults to null.</param>
//     /// <returns>The current menu builder instance for method chaining.</returns>
//     IMenuBuilder AddAsyncButton( string text, Func<IPlayer, Task> onClickAsync, IMenuTextSize size = IMenuTextSize.Medium, MenuHorizontalStyle? overflowStyle = null );

//     /// <summary>
//     /// Adds an asynchronous button option to the menu that executes async operations.
//     /// When selected, executes the provided async function with the player and option as parameters.
//     /// </summary>
//     /// <param name="text">The display text for the async button.</param>
//     /// <param name="onClickAsync">The async function to execute when the button is clicked. Receives the player and option as parameters.</param>
//     /// <param name="size">The text size for the button display. Defaults to Medium.</param>
//     /// <param name="overflowStyle">The overflow style for the text. Defaults to null.</param>
//     /// <returns>The current menu builder instance for method chaining.</returns>
//     IMenuBuilder AddAsyncButton( string text, Func<IPlayer, IOption, Task> onClickAsync, IMenuTextSize size = IMenuTextSize.Medium, MenuHorizontalStyle? overflowStyle = null );

//     /// <summary>
//     /// Adds a non-interactive text display option to the menu.
//     /// Used for showing information, headers, or descriptions within the menu.
//     /// </summary>
//     /// <param name="text">The text content to display.</param>
//     /// <param name="alignment">The text alignment within the menu. Defaults to Left.</param>
//     /// <param name="size">The text size for the display. Defaults to Medium.</param>
//     /// <param name="overflowStyle">The overflow style for the text. Defaults to null.</param>
//     /// <returns>The current menu builder instance for method chaining.</returns>
//     IMenuBuilder AddText( string text, ITextAlign alignment = ITextAlign.Left, IMenuTextSize size = IMenuTextSize.Medium, MenuHorizontalStyle? overflowStyle = null );

//     /// <summary>
//     /// Adds a submenu option that navigates to another menu when selected.
//     /// Creates a hierarchical menu structure with parent-child relationships.
//     /// </summary>
//     /// <param name="text">The display text for the submenu option.</param>
//     /// <param name="submenu">The submenu instance to navigate to.</param>
//     /// <param name="size">The text size for the submenu display. Defaults to Medium.</param>
//     /// <returns>The current menu builder instance for method chaining.</returns>
//     IMenuBuilder AddSubmenu( string text, IMenu submenu, IMenuTextSize size = IMenuTextSize.Medium );

//     /// <summary>
//     /// Adds a submenu option that navigates to another menu when selected.
//     /// Creates a hierarchical menu structure with parent-child relationships.
//     /// </summary>
//     /// <param name="text">The display text for the submenu option.</param>
//     /// <param name="submenu">The submenu instance to navigate to.</param>
//     /// <returns>The current menu builder instance for method chaining.</returns>
//     IMenuBuilder AddSubmenu( string text, IMenu submenu );

//     /// <summary>
//     /// Adds a submenu option that creates and navigates to a dynamically built menu when selected.
//     /// The submenu is constructed on-demand using the provided builder function.
//     /// </summary>
//     /// <param name="text">The display text for the submenu option.</param>
//     /// <param name="submenuBuilder">A function that returns the submenu instance when called.</param>
//     /// <param name="size">The text size for the submenu display. Defaults to Medium.</param>
//     /// <returns>The current menu builder instance for method chaining.</returns>
//     IMenuBuilder AddSubmenu( string text, Func<IMenu> submenuBuilder, IMenuTextSize size = IMenuTextSize.Medium );

//     /// <summary>
//     /// Adds a submenu option that creates and navigates to a dynamically built menu when selected.
//     /// The submenu is constructed on-demand using the provided builder function.
//     /// </summary>
//     /// <param name="text">The display text for the submenu option.</param>
//     /// <param name="submenuBuilder">A function that returns the submenu instance when called.</param>
//     /// <returns>The current menu builder instance for method chaining.</returns>
//     IMenuBuilder AddSubmenu( string text, Func<IMenu> submenuBuilder );

//     /// <summary>
//     /// Adds a submenu option that creates and navigates to a dynamically built menu when selected.
//     /// The submenu is constructed on-demand using the provided asynchronous builder function.
//     /// </summary>
//     /// <param name="text">The display text for the submenu option.</param>
//     /// <param name="submenuBuilder">An asynchronous function that returns the submenu instance when called.</param>
//     /// <param name="size">The text size for the submenu display. Defaults to Medium.</param>
//     /// <returns>The current menu builder instance for method chaining.</returns>
//     IMenuBuilder AddSubmenu( string text, Func<Task<IMenu>> submenuBuilder, IMenuTextSize size = IMenuTextSize.Medium );

//     /// <summary>
//     /// Adds a submenu option that creates and navigates to a dynamically built menu when selected.
//     /// The submenu is constructed on-demand using the provided asynchronous builder function.
//     /// </summary>
//     /// <param name="text">The display text for the submenu option.</param>
//     /// <param name="submenuBuilder">An asynchronous function that returns the submenu instance when called.</param>
//     /// <returns>The current menu builder instance for method chaining.</returns>
//     IMenuBuilder AddSubmenu( string text, Func<Task<IMenu>> submenuBuilder );

//     /// <summary>
//     /// Adds a choice selection option that allows players to select from multiple predefined options.
//     /// Players can cycle through the available choices using left/right navigation.
//     /// </summary>
//     /// <param name="text">The display text for the choice option.</param>
//     /// <param name="choices">An array of available choice strings.</param>
//     /// <param name="defaultChoice">The initially selected choice. Defaults to null (first choice).</param>
//     /// <param name="onChange">Optional action to execute when the choice changes. Receives the player and selected choice string.</param>
//     /// <param name="size">The text size for the choice display. Defaults to Medium.</param>
//     /// <param name="overflowStyle">The overflow style for the text. Defaults to null.</param>
//     /// <returns>The current menu builder instance for method chaining.</returns>
//     IMenuBuilder AddChoice( string text, string[] choices, string? defaultChoice = null, Action<IPlayer, string>? onChange = null, IMenuTextSize size = IMenuTextSize.Medium, MenuHorizontalStyle? overflowStyle = null );

//     // /// <summary>
//     // /// Adds a choice selection option that allows players to select from multiple predefined options.
//     // /// Players can cycle through the available choices using left/right navigation.
//     // /// </summary>
//     // /// <param name="text">The display text for the choice option.</param>
//     // /// <param name="choices">An array of available choice strings.</param>
//     // /// <param name="defaultChoice">The initially selected choice.</param>
//     // /// <param name="onChange">Optional action to execute when the choice changes. Receives the player and selected choice string.</param>
//     // /// <param name="overflowStyle">The overflow style for the text. Defaults to null.</param>
//     // /// <returns>The current menu builder instance for method chaining.</returns>
//     // [Obsolete("This overload causes ambiguity. Use AddChoice(text, choices, defaultChoice, onChange, IMenuTextSize.Medium, overflowStyle) instead.")]
//     // IMenuBuilder AddChoice(string text, string[] choices, string? defaultChoice, Action<IPlayer, string>? onChange, MenuHorizontalStyle? overflowStyle = null);

//     /// <summary>
//     /// Adds a choice selection option that allows players to select from multiple predefined options.
//     /// Players can cycle through the available choices using left/right navigation.
//     /// </summary>
//     /// <param name="text">The display text for the choice option.</param>
//     /// <param name="choices">An array of available choice strings.</param>
//     /// <param name="defaultChoice">The initially selected choice.</param>
//     /// <param name="onChange">Optional action to execute when the choice changes. Receives the player, option, and selected choice string.</param>
//     /// <param name="size">The text size for the choice display. Defaults to Medium.</param>
//     /// <param name="overflowStyle">The overflow style for the text. Defaults to null.</param>
//     /// <returns>The current menu builder instance for method chaining.</returns>
//     IMenuBuilder AddChoice( string text, string[] choices, string? defaultChoice, Action<IPlayer, IOption, string>? onChange, IMenuTextSize size = IMenuTextSize.Medium, MenuHorizontalStyle? overflowStyle = null );

//     /// <summary>
//     /// Adds a visual separator line to the menu for organizing content.
//     /// Creates a non-interactive divider between menu sections.
//     /// </summary>
//     /// <returns>The current menu builder instance for method chaining.</returns>
//     IMenuBuilder AddSeparator();

//     /// <summary>
//     /// Adds a progress bar display option that shows dynamic progress information.
//     /// The progress value is retrieved from the provided function and updated automatically.
//     /// </summary>
//     /// <param name="text">The display text for the progress bar.</param>
//     /// <param name="progressProvider">A function that returns the current progress value (0.0 to 1.0).</param>
//     /// <param name="barWidth">The character width of the progress bar. Defaults to 20.</param>
//     /// <param name="size">The text size for the progress bar display. Defaults to Medium.</param>
//     /// <param name="overflowStyle">The overflow style for the text. Defaults to null.</param>
//     /// <returns>The current menu builder instance for method chaining.</returns>
//     IMenuBuilder AddProgressBar( string text, Func<float> progressProvider, int barWidth = 20, IMenuTextSize size = IMenuTextSize.Medium, MenuHorizontalStyle? overflowStyle = null );

//     /// <summary>
//     /// Adds a progress bar display option that shows dynamic progress information.
//     /// The progress value is retrieved from the provided function and updated automatically.
//     /// </summary>
//     /// <param name="text">The display text for the progress bar.</param>
//     /// <param name="progressProvider">A function that returns the current progress value (0.0 to 1.0).</param>
//     /// <param name="barWidth">The character width of the progress bar.</param>
//     /// <param name="overflowStyle">The overflow style for the text. Defaults to null.</param>
//     /// <returns>The current menu builder instance for method chaining.</returns>
//     IMenuBuilder AddProgressBar( string text, Func<float> progressProvider, int barWidth, MenuHorizontalStyle? overflowStyle = null );

//     /// <summary>
//     /// Sets the parent menu for the menu being built, creating a hierarchical menu structure.
//     /// Allows players to navigate back to the parent menu from the current menu.
//     /// </summary>
//     /// <param name="parent">The parent menu instance.</param>
//     /// <returns>The current menu builder instance for method chaining.</returns>
//     IMenuBuilder WithParent( IMenu parent );

//     /// <summary>
//     /// Sets a condition that determines when the menu should be visible to players.
//     /// The menu will only be shown to players for whom the condition returns true.
//     /// </summary>
//     /// <param name="condition">A function that takes a player and returns true if the menu should be visible.</param>
//     /// <returns>The current menu builder instance for method chaining.</returns>
//     IMenuBuilder VisibleWhen( Func<IPlayer, bool> condition );

//     /// <summary>
//     /// Sets a condition that determines when the menu should be enabled for interaction.
//     /// When disabled, the menu may be visible but not interactive.
//     /// </summary>
//     /// <param name="condition">A function that takes a player and returns true if the menu should be enabled.</param>
//     /// <returns>The current menu builder instance for method chaining.</returns>
//     IMenuBuilder EnabledWhen( Func<IPlayer, bool> condition );

//     /// <summary>
//     /// Configures the menu to automatically close when any option is selected.
//     /// Useful for action menus where selection should immediately close the menu.
//     /// </summary>
//     /// <returns>The current menu builder instance for method chaining.</returns>
//     IMenuBuilder CloseOnSelect();

//     /// <summary>
//     /// Sets an automatic close timer for the menu.
//     /// The menu will automatically close after the specified number of seconds.
//     /// </summary>
//     /// <param name="seconds">The number of seconds after which to automatically close the menu.</param>
//     /// <returns>The current menu builder instance for method chaining.</returns>
//     IMenuBuilder AutoClose( float seconds );

//     /// <summary>
//     /// Overrides the default button(s) used for selecting menu options.
//     /// Allows customization of the input controls for menu interaction.
//     /// </summary>
//     /// <param name="buttonNames">The names of the buttons to use for selection.</param>
//     /// <returns>The current menu builder instance for method chaining.</returns>
//     [Obsolete("Use Design.OverrideSelectButton instead")]
//     IMenuBuilder OverrideSelectButton( params string[] buttonNames );

//     /// <summary>
//     /// Overrides the default button(s) used for moving through menu options.
//     /// Allows customization of the input controls for menu navigation.
//     /// </summary>
//     /// <param name="buttonNames">The names of the buttons to use for movement.</param>
//     /// <returns>The current menu builder instance for method chaining.</returns>
//     [Obsolete("Use Design.OverrideMoveButton instead")]
//     IMenuBuilder OverrideMoveButton( params string[] buttonNames );

//     /// <summary>
//     /// Overrides the default button(s) used for exiting or closing the menu.
//     /// Allows customization of the input controls for menu exit.
//     /// </summary>
//     /// <param name="buttonNames">The names of the buttons to use for exiting.</param>
//     /// <returns>The current menu builder instance for method chaining.</returns>
//     [Obsolete("Use Design.OverrideExitButton instead")]
//     IMenuBuilder OverrideExitButton( params string[] buttonNames );

//     /// <summary>
//     /// Sets the maximum number of menu items visible at once.
//     /// When there are more items than this limit, the menu will be paginated.
//     /// </summary>
//     /// <param name="count">The maximum number of visible items.</param>
//     /// <returns>The current menu builder instance for method chaining.</returns>
//     /// <remarks>
//     /// If the provided count is less than 1, it will be clamped to 1.
//     /// If the provided count is greater than 5, it will be clamped to 5.
//     /// A warning will be logged when clamping occurs.
//     /// </remarks>
//     [Obsolete("Use Design.MaxVisibleItems instead")]
//     IMenuBuilder MaxVisibleItems( int count );

//     /// <summary>
//     /// Configures the menu to not freeze player movement when displayed.
//     /// Players will be able to move around while the menu is open.
//     /// </summary>
//     /// <returns>The current menu builder instance for method chaining.</returns>
//     IMenuBuilder NoFreeze();

//     /// <summary>
//     /// Configures the menu to freeze player movement when displayed.
//     /// Players will be unable to move while the menu is open.
//     /// </summary>
//     /// <returns>The current menu builder instance for method chaining.</returns>
//     IMenuBuilder ForceFreeze();

//     /// <summary>
//     /// Sets the color used for rendering the menu.
//     /// Affects the visual appearance and styling of the menu display.
//     /// </summary>
//     /// <param name="color">The color to use for menu rendering.</param>
//     /// <returns>The current menu builder instance for method chaining.</returns>
//     [Obsolete("Use Design.SetColor instead")]
//     IMenuBuilder SetColor( Color color );
// }

// /// <summary>
// /// Defines text alignment options for menu text elements.
// /// Used to control how text is positioned within menu displays.
// /// </summary>
// public enum ITextAlign
// {
//     /// <summary>
//     /// Aligns text to the left side of the display area.
//     /// </summary>
//     Left,

//     /// <summary>
//     /// Centers text horizontally within the display area.
//     /// </summary>
//     Center,

//     /// <summary>
//     /// Aligns text to the right side of the display area.
//     /// </summary>
//     Right
// }