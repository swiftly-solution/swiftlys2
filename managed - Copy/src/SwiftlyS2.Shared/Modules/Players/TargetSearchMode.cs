namespace SwiftlyS2.Shared.Players;

[Flags]
public enum TargetSearchMode
{
    /// <summary>
    /// No specific search mode; default behavior.
    /// </summary>
    None = 0,
    /// <summary>
    /// Includes alive players in the search results.
    /// </summary>
    Alive = 1 << 0,
    /// <summary>
    /// Includes dead players in the search results.
    /// </summary>
    Dead = 1 << 1,
    /// <summary>
    /// Limits the search to a single target only.
    /// </summary>
    NoMultipleTargets = 1 << 2,
    /// <summary>
    /// Includes the searching player in the search results.
    /// </summary>
    IncludeSelf = 1 << 3,
    /// <summary>
    /// Includes only players from the same team in the search results.
    /// </summary>
    TeamOnly = 1 << 4,
    /// <summary>
    /// Includes only players from the opposite team in the search results.
    /// </summary>
    OppositeTeamOnly = 1 << 5,
    /// <summary>
    /// Excludes bot players from the search results.
    /// </summary>
    NoBots = 1 << 6,

}