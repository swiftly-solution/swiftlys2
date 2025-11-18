namespace SwiftlyS2.Shared.Menus;

[Flags]
public enum KeyBind : uint
{
    Mouse1 = 1 << 0,
    Mouse2 = 1 << 1,
    Space = 1 << 2,
    Ctrl = 1 << 3,
    W = 1 << 4,
    A = 1 << 5,
    S = 1 << 6,
    D = 1 << 7,
    E = 1 << 8,
    Esc = 1 << 9,
    R = 1 << 10,
    Alt = 1 << 11,
    Shift = 1 << 12,
    Weapon1 = 1 << 13,
    Weapon2 = 1 << 14,
    Grenade1 = 1 << 15,
    Grenade2 = 1 << 16,
    Tab = 1 << 17,
    F = 1 << 18,
}