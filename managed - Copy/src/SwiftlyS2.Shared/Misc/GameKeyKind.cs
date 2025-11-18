namespace SwiftlyS2.Shared.Events;

[Flags]
public enum GameButtonFlags: ulong
{
    None = 0,
    Mouse1 = 1UL << GameButtons.Mouse1,
    Space = 1UL << GameButtons.Space,
    Ctrl = 1UL << GameButtons.Ctrl,
    W = 1UL << GameButtons.W,
    S = 1UL << GameButtons.S,
    E = 1UL << GameButtons.E,
    Esc = 1UL << GameButtons.Esc,
    A = 1UL << GameButtons.A,
    D = 1UL << GameButtons.D,
    A2 = 1UL << GameButtons.A2,
    D2 = 1UL << GameButtons.D2,
    Mouse2 = 1UL << GameButtons.Mouse2,
    UnknownKeyRun = 1UL << GameButtons.UnknownKeyRun,
    R = 1UL << GameButtons.R,
    Alt = 1UL << GameButtons.Alt,
    Alt2 = 1UL << GameButtons.Alt2,
    Shift = 1UL << GameButtons.Shift,
    UnknownKeySpeed = 1UL << GameButtons.UnknownKeySpeed,
    Shift2 = 1UL << GameButtons.Shift2,
    UnknownKeyHudzoom = 1UL << GameButtons.UnknownKeyHudzoom,
    UnknownKeyWeapon1 = 1UL << GameButtons.UnknownKeyWeapon1,
    UnknownKeyWeapon2 = 1UL << GameButtons.UnknownKeyWeapon2,
    UnknownKeyBullrush = 1UL << GameButtons.UnknownKeyBullrush,
    UnknownKeyGrenade1 = 1UL << GameButtons.UnknownKeyGrenade1,
    UnknownKeyGrenade2 = 1UL << GameButtons.UnknownKeyGrenade2,
    UnknownKeyLookspin = 1UL << GameButtons.UnknownKeyLookspin,
    UnknownKey26 = 1UL << GameButtons.UnknownKey26,
    UnknownKey27 = 1UL << GameButtons.UnknownKey27,
    UnknownKey28 = 1UL << GameButtons.UnknownKey28,
    UnknownKey29 = 1UL << GameButtons.UnknownKey29,
    UnknownKey30 = 1UL << GameButtons.UnknownKey30,
    UnknownKey31 = 1UL << GameButtons.UnknownKey31,
    UnknownKey32 = 1UL << GameButtons.UnknownKey32,
    Tab = 1UL << GameButtons.Tab,
    UnknownKey34 = 1UL << GameButtons.UnknownKey34,
    F = 1UL << GameButtons.F,
    UnknownKey36 = 1UL << GameButtons.UnknownKey36,
    UnknownKey37 = 1UL << GameButtons.UnknownKey37,
    UnknownKey38 = 1UL << GameButtons.UnknownKey38,
    UnknownKey39 = 1UL << GameButtons.UnknownKey39,
    UnknownKey40 = 1UL << GameButtons.UnknownKey40,
    UnknownKey41 = 1UL << GameButtons.UnknownKey41,
    UnknownKey42 = 1UL << GameButtons.UnknownKey42,
    UnknownKey43 = 1UL << GameButtons.UnknownKey43,
    UnknownKey44 = 1UL << GameButtons.UnknownKey44,
    UnknownKey45 = 1UL << GameButtons.UnknownKey45,
    UnknownKey46 = 1UL << GameButtons.UnknownKey46,
    UnknownKey47 = 1UL << GameButtons.UnknownKey47,
    UnknownKey48 = 1UL << GameButtons.UnknownKey48,
    UnknownKey49 = 1UL << GameButtons.UnknownKey49,
    UnknownKey50 = 1UL << GameButtons.UnknownKey50,
    UnknownKey51 = 1UL << GameButtons.UnknownKey51,
    UnknownKey52 = 1UL << GameButtons.UnknownKey52,
    UnknownKey53 = 1UL << GameButtons.UnknownKey53,
    UnknownKey54 = 1UL << GameButtons.UnknownKey54,
    UnknownKey55 = 1UL << GameButtons.UnknownKey55,
    UnknownKey56 = 1UL << GameButtons.UnknownKey56,
    UnknownKey57 = 1UL << GameButtons.UnknownKey57,
    UnknownKey58 = 1UL << GameButtons.UnknownKey58,
    UnknownKey59 = 1UL << GameButtons.UnknownKey59,
    UnknownKey60 = 1UL << GameButtons.UnknownKey60,
    UnknownKey61 = 1UL << GameButtons.UnknownKey61,
    UnknownKey62 = 1UL << GameButtons.UnknownKey62,
    UnknownKey63 = 1UL << GameButtons.UnknownKey63,
}

public enum GameButtons : int
{

  Mouse1 = 0,

  Space = 1,

  Ctrl = 2,

  W = 3,

  S = 4,

  E = 5,

  Esc = 6,

  A = 7,

  D = 8,

  A2 = 9,

  D2 = 10,

  Mouse2 = 11,

  UnknownKeyRun = 12,

  R = 13,

  Alt = 14,

  Alt2 = 15,

  Shift = 16,

  UnknownKeySpeed = 17,

  Shift2 = 18,

  UnknownKeyHudzoom = 19,

  UnknownKeyWeapon1 = 20,

  UnknownKeyWeapon2 = 21,

  UnknownKeyBullrush = 22,

  UnknownKeyGrenade1 = 23,

  UnknownKeyGrenade2 = 24,

  UnknownKeyLookspin = 25,

  UnknownKey26 = 26,

  UnknownKey27 = 27,

  UnknownKey28 = 28,

  UnknownKey29 = 29,

  UnknownKey30 = 30,

  UnknownKey31 = 31,

  UnknownKey32 = 32,

  Tab = 33,

  UnknownKey34 = 34,

  F = 35,

  UnknownKey36 = 36,

  UnknownKey37 = 37,

  UnknownKey38 = 38,

  UnknownKey39 = 39,

  UnknownKey40 = 40,

  UnknownKey41 = 41,

  UnknownKey42 = 42,

  UnknownKey43 = 43,

  UnknownKey44 = 44,

  UnknownKey45 = 45,

  UnknownKey46 = 46,

  UnknownKey47 = 47,

  UnknownKey48 = 48,

  UnknownKey49 = 49,

  UnknownKey50 = 50,

  UnknownKey51 = 51,

  UnknownKey52 = 52,

  UnknownKey53 = 53,

  UnknownKey54 = 54,

  UnknownKey55 = 55,

  UnknownKey56 = 56,

  UnknownKey57 = 57,

  UnknownKey58 = 58,

  UnknownKey59 = 59,

  UnknownKey60 = 60,

  UnknownKey61 = 61,

  UnknownKey62 = 62,

  UnknownKey63 = 63,
}

internal static class GameKeyKindExtensions {
  public static KeyKind ToKeyKind(this GameButtons keyKind) {
    return keyKind switch {
      GameButtons.Mouse1 => KeyKind.Mouse1,
      GameButtons.Mouse2 => KeyKind.Mouse2,
      GameButtons.Space => KeyKind.Space,
      GameButtons.Ctrl => KeyKind.Ctrl,
      GameButtons.W => KeyKind.W,
      GameButtons.S => KeyKind.S,
      GameButtons.E => KeyKind.E,
      GameButtons.Esc => KeyKind.Esc,
      GameButtons.A => KeyKind.A,
      GameButtons.A2 => KeyKind.A,
      GameButtons.D => KeyKind.D,
      GameButtons.D2 => KeyKind.D,
      GameButtons.R => KeyKind.R,
      GameButtons.Alt => KeyKind.Alt,
      GameButtons.Shift => KeyKind.Shift,
      GameButtons.UnknownKeyWeapon1 => KeyKind.Weapon1,
      GameButtons.UnknownKeyWeapon2 => KeyKind.Weapon2,
      GameButtons.UnknownKeyGrenade1 => KeyKind.Grenade1,
      GameButtons.UnknownKeyGrenade2 => KeyKind.Grenade2,
      GameButtons.Tab => KeyKind.Tab,
      GameButtons.F => KeyKind.F,
      _ => throw new ArgumentException($"Unknown key kind: {keyKind}. Please report this to the SwiftlyS2 team.")
    };
  }
}