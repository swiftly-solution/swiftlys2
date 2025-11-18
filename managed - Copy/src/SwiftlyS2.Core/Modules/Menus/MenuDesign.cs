// using SwiftlyS2.Shared.Menus;
// using SwiftlyS2.Shared.Natives;

// namespace SwiftlyS2.Core.Menus;

// [Obsolete("MenuDesign will be deprecared at the release of SwiftlyS2. Please use MenuDesignAPI instead")]
// internal sealed class MenuDesign : IMenuDesign
// {
//     private readonly IMenu _menu;

//     public MenuDesign( IMenu menu )
//     {
//         _menu = menu;
//     }

//     public IMenuDesign OverrideSelectButton( params string[] buttonNames )
//     {
//         _menu.ButtonOverrides!.Select = MenuButtonOverrides.ParseButtons(buttonNames);
//         return this;
//     }

//     public IMenuDesign OverrideMoveButton( params string[] buttonNames )
//     {
//         _menu.ButtonOverrides!.Move = MenuButtonOverrides.ParseButtons(buttonNames);
//         return this;
//     }

//     public IMenuDesign OverrideExitButton( params string[] buttonNames )
//     {
//         _menu.ButtonOverrides!.Exit = MenuButtonOverrides.ParseButtons(buttonNames);
//         return this;
//     }

//     public IMenuDesign MaxVisibleItems( int count )
//     {
//         if (count < 1 || count > 5)
//         {
//             Spectre.Console.AnsiConsole.WriteException(new ArgumentOutOfRangeException(nameof(count), $"MaxVisibleItems: value {count} is out of range [1, 5]."));
//         }
//         _menu.MaxVisibleOptions = Math.Clamp(count, 1, 5);
//         return this;
//     }

//     public IMenuDesign SetColor( Color color )
//     {
//         _menu.RenderColor = color;
//         return this;
//     }

//     public IMenuDesign SetVerticalScrollStyle( MenuVerticalScrollStyle style )
//     {
//         _menu.VerticalScrollStyle = style;
//         return this;
//     }

//     public IMenuDesign SetGlobalHorizontalStyle( MenuHorizontalStyle style )
//     {
//         _menu.HorizontalStyle = style;
//         return this;
//     }
// }