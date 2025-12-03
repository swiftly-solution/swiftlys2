namespace SwiftlyS2.Core.Menus;

[Flags]
internal enum MenuInputClaim
{
    /// <summary>
    /// No input claimed.
    /// </summary>
    None = 0,

    /// <summary>
    /// Claims the Exit key (A in WASD mode).
    /// </summary>
    Exit = 1 << 0,

    /// <summary>
    /// Claims the Use/Select key (D in WASD mode).
    /// </summary>
    Use = 1 << 1
}

internal readonly record struct MenuInputClaimInfo
{
    /// <summary>
    /// The input types being claimed.
    /// </summary>
    public MenuInputClaim Claims { get; init; }

    /// <summary>
    /// The display label for the Exit key when claimed.
    /// Shown in the menu footer instead of "Exit".
    /// </summary>
    public string? ExitLabel { get; init; }

    /// <summary>
    /// The display label for the Use key when claimed.
    /// Shown in the menu footer instead of "Use".
    /// </summary>
    public string? UseLabel { get; init; }

    /// <summary>
    /// Returns true if any input is claimed.
    /// </summary>
    public bool HasClaims => Claims != MenuInputClaim.None;

    /// <summary>
    /// Returns true if the Exit input is claimed.
    /// </summary>
    public bool ClaimsExit => (Claims & MenuInputClaim.Exit) != 0;

    /// <summary>
    /// Returns true if the Use input is claimed.
    /// </summary>
    public bool ClaimsUse => (Claims & MenuInputClaim.Use) != 0;

    /// <summary>
    /// Creates an empty claim info with no claims.
    /// </summary>
    public static MenuInputClaimInfo Empty => new() { Claims = MenuInputClaim.None };
}