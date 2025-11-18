using SwiftlyS2.Shared.Menus;

namespace SwiftlyS2.Shared.Events;

public enum KeyKind : int
{
    Mouse1,
    Mouse2,
    Space,
    Ctrl,
    W,
    A,
    S,
    D,
    E,
    Esc,
    R,
    Alt,
    Shift,
    Weapon1,
    Weapon2,
    Grenade1,
    Grenade2,
    Tab,
    F,
}

/// <summary>
/// Provides extension methods for <see cref="KeyKind"/>.
/// </summary>
internal static class KeyKindExtensions
{
    /// <summary>
    /// Converts a <see cref="KeyKind"/> enum value to its corresponding <see cref="KeyBind"/> enum value.
    /// </summary>
    /// <param name="keyKind">The <see cref="KeyKind"/> value to convert.</param>
    /// <returns>The corresponding <see cref="KeyBind"/> enum value.</returns>
    /// <exception cref="ArgumentException">Thrown when the <paramref name="keyKind"/> is not recognized.</exception>
    public static KeyBind ToKeyBind( this KeyKind keyKind )
    {
        return keyKind switch {
            KeyKind.Mouse1 => KeyBind.Mouse1,
            KeyKind.Mouse2 => KeyBind.Mouse2,
            KeyKind.Space => KeyBind.Space,
            KeyKind.Ctrl => KeyBind.Ctrl,
            KeyKind.W => KeyBind.W,
            KeyKind.A => KeyBind.A,
            KeyKind.S => KeyBind.S,
            KeyKind.D => KeyBind.D,
            KeyKind.E => KeyBind.E,
            KeyKind.Esc => KeyBind.Esc,
            KeyKind.R => KeyBind.R,
            KeyKind.Alt => KeyBind.Alt,
            KeyKind.Shift => KeyBind.Shift,
            KeyKind.Weapon1 => KeyBind.Weapon1,
            KeyKind.Weapon2 => KeyBind.Weapon2,
            KeyKind.Grenade1 => KeyBind.Grenade1,
            KeyKind.Grenade2 => KeyBind.Grenade2,
            KeyKind.Tab => KeyBind.Tab,
            KeyKind.F => KeyBind.F,
            _ => throw new ArgumentException($"Unknown key kind: {keyKind}.")
        };
    }
}