// using SwiftlyS2.Shared.Events;
// using SwiftlyS2.Shared.Menus;

// namespace SwiftlyS2.Core.Menus;

// [Obsolete("MenuButtonOverrides will be deprecared at the release of SwiftlyS2.")]
// internal class MenuButtonOverrides : IMenuButtonOverrides
// {
//     public KeyKind? Select { get; set; }
//     public KeyKind? Move { get; set; }
//     public KeyKind? MoveBack { get; set; }
//     public KeyKind? Exit { get; set; }

//     internal static KeyKind ParseButton( string buttonName )
//     {
//         if (Enum.TryParse<KeyKind>(buttonName, true, out var button))
//         {
//             return button;
//         }
//         return 0;
//     }

//     internal static KeyKind ParseButtons( params string[] buttonNames )
//     {
//         KeyKind result = 0;
//         foreach (var buttonName in buttonNames)
//         {
//             if (IsWASDButton(buttonName)) continue;
//             result |= ParseButton(buttonName);
//         }
//         return result;
//     }

//     private static bool IsWASDButton( string buttonName )
//     {
//         var lower = buttonName.ToLowerInvariant();
//         return lower == "forward" || lower == "back" || lower == "moveleft" ||
//                lower == "moveright" || lower == "w" || lower == "a" || lower == "s" || lower == "d";
//     }
// }