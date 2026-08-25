using SwiftlyS2.Shared.Events;

namespace SwiftlyS2.Shared.Menu;

/// <summary>
/// A set of physical keys a menu action can be bound to.
/// </summary>
/// <remarks>
/// This is a flags enum, so a single action may be bound to several keys at once.
/// </remarks>
[Flags]
public enum MenuKey : uint
{
    /// <summary>
    /// No key. An action bound to <see cref="None"/> can never be triggered.
    /// </summary>
    None = 0,

    /// <summary>Primary mouse button.</summary>
    Mouse1 = 1u << 0,

    /// <summary>Secondary mouse button.</summary>
    Mouse2 = 1u << 1,

    /// <summary>Jump key.</summary>
    Space = 1u << 2,

    /// <summary>Duck key.</summary>
    Ctrl = 1u << 3,

    /// <summary>Forward movement key.</summary>
    W = 1u << 4,

    /// <summary>Left movement key.</summary>
    A = 1u << 5,

    /// <summary>Backward movement key.</summary>
    S = 1u << 6,

    /// <summary>Right movement key.</summary>
    D = 1u << 7,

    /// <summary>Use key.</summary>
    E = 1u << 8,

    /// <summary>Escape key.</summary>
    Esc = 1u << 9,

    /// <summary>Reload key.</summary>
    R = 1u << 10,

    /// <summary>Walk key.</summary>
    Alt = 1u << 11,

    /// <summary>Speed key.</summary>
    Shift = 1u << 12,

    /// <summary>Primary weapon slot key.</summary>
    Weapon1 = 1u << 13,

    /// <summary>Secondary weapon slot key.</summary>
    Weapon2 = 1u << 14,

    /// <summary>First grenade slot key.</summary>
    Grenade1 = 1u << 15,

    /// <summary>Second grenade slot key.</summary>
    Grenade2 = 1u << 16,

    /// <summary>Scoreboard key.</summary>
    Tab = 1u << 17,

    /// <summary>Inspect key.</summary>
    F = 1u << 18,
}

/// <summary>
/// Parsing and formatting helpers for <see cref="MenuKey"/>.
/// </summary>
public static class MenuKeys
{
    private static readonly Dictionary<string, MenuKey> byName = new(StringComparer.OrdinalIgnoreCase) {
        ["mouse1"] = MenuKey.Mouse1,
        ["mouse2"] = MenuKey.Mouse2,
        ["space"] = MenuKey.Space,
        ["ctrl"] = MenuKey.Ctrl,
        ["w"] = MenuKey.W,
        ["a"] = MenuKey.A,
        ["s"] = MenuKey.S,
        ["d"] = MenuKey.D,
        ["e"] = MenuKey.E,
        ["esc"] = MenuKey.Esc,
        ["r"] = MenuKey.R,
        ["alt"] = MenuKey.Alt,
        ["shift"] = MenuKey.Shift,
        ["weapon1"] = MenuKey.Weapon1,
        ["weapon2"] = MenuKey.Weapon2,
        ["grenade1"] = MenuKey.Grenade1,
        ["grenade2"] = MenuKey.Grenade2,
        ["tab"] = MenuKey.Tab,
        ["f"] = MenuKey.F,
    };

    /// <summary>
    /// All key names accepted by <see cref="TryParse(string, out MenuKey)"/>.
    /// </summary>
    public static IReadOnlyCollection<string> Names => byName.Keys;

    /// <summary>
    /// Attempts to parse a single key name, such as <c>"e"</c> or <c>"mouse1"</c>.
    /// </summary>
    /// <param name="name">The key name. Case insensitive.</param>
    /// <param name="key">The parsed key, or <see cref="MenuKey.None"/> when parsing fails.</param>
    /// <returns><see langword="true"/> when the name was recognised.</returns>
    public static bool TryParse( string? name, out MenuKey key )
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            key = MenuKey.None;
            return false;
        }

        return byName.TryGetValue(name.Trim(), out key);
    }

    /// <summary>
    /// Parses a sequence of key names into a single combined <see cref="MenuKey"/>.
    /// </summary>
    /// <param name="names">The key names to combine. Unrecognised names are ignored.</param>
    /// <returns>The combined key set, or <see cref="MenuKey.None"/> when nothing was recognised.</returns>
    public static MenuKey ParseAll( IEnumerable<string?> names )
    {
        var result = MenuKey.None;

        foreach (var name in names)
        {
            if (TryParse(name, out var key))
            {
                result |= key;
            }
        }

        return result;
    }

    /// <summary>
    /// Converts an engine key event value into its <see cref="MenuKey"/> equivalent.
    /// </summary>
    /// <param name="keyKind">The engine key value.</param>
    /// <returns>The matching key, or <see cref="MenuKey.None"/> when the value has no menu equivalent.</returns>
    public static MenuKey FromKeyKind( KeyKind keyKind )
    {
        return keyKind switch {
            KeyKind.Mouse1 => MenuKey.Mouse1,
            KeyKind.Mouse2 => MenuKey.Mouse2,
            KeyKind.Space => MenuKey.Space,
            KeyKind.Ctrl => MenuKey.Ctrl,
            KeyKind.W => MenuKey.W,
            KeyKind.A => MenuKey.A,
            KeyKind.S => MenuKey.S,
            KeyKind.D => MenuKey.D,
            KeyKind.E => MenuKey.E,
            KeyKind.Esc => MenuKey.Esc,
            KeyKind.R => MenuKey.R,
            KeyKind.Alt => MenuKey.Alt,
            KeyKind.Shift => MenuKey.Shift,
            KeyKind.Weapon1 => MenuKey.Weapon1,
            KeyKind.Weapon2 => MenuKey.Weapon2,
            KeyKind.Grenade1 => MenuKey.Grenade1,
            KeyKind.Grenade2 => MenuKey.Grenade2,
            KeyKind.Tab => MenuKey.Tab,
            KeyKind.F => MenuKey.F,
            _ => MenuKey.None
        };
    }

    /// <summary>
    /// Produces a human readable label for a key set, suitable for a menu footer.
    /// </summary>
    /// <param name="key">The key set to describe.</param>
    /// <returns>An upper case label such as <c>"E"</c> or <c>"SHIFT/F"</c>.</returns>
    public static string Describe( MenuKey key )
    {
        if (key == MenuKey.None)
        {
            return string.Empty;
        }

        var parts = new List<string>();

        foreach (var pair in byName)
        {
            if (key.HasFlag(pair.Value) && !parts.Contains(pair.Key.ToUpperInvariant()))
            {
                parts.Add(pair.Key.ToUpperInvariant());
            }
        }

        return string.Join('/', parts);
    }
}
