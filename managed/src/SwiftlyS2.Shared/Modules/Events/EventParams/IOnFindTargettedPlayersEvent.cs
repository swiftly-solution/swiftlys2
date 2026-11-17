using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.Events;

/// <summary>
/// Called for every candidate while <see cref="IPlayerManagerService.FindTargettedPlayers"/> resolves a target.
/// Use it to add your own target selectors (for example "@vip") or to veto candidates.
/// Candidates that fail the <see cref="TargetSearchMode"/> filters are not passed in.
/// This callback runs once per candidate, so don't do anything expensive.
/// </summary>
public interface IOnFindTargettedPlayersEvent
{
    /// <summary>
    /// The player initiating the search.
    /// </summary>
    public IPlayer Player { get; }

    /// <summary>
    /// The raw target string, for example "@vip", "#3" or part of a name.
    /// </summary>
    public string Target { get; }

    /// <summary>
    /// The search mode requested by the caller.
    /// </summary>
    public TargetSearchMode SearchMode { get; }

    /// <summary>
    /// The player being tested against <see cref="Target"/>.
    /// </summary>
    public IPlayer Candidate { get; }

    /// <summary>
    /// Whether <see cref="Candidate"/> is included in the result.
    /// Starts as the built-in verdict (or the previous plugin's). Set it to true to include the candidate, false to exclude it.
    /// </summary>
    public bool IsMatch { get; set; }

    /// <summary>
    /// Set to <see cref="HookResult.Handled"/> or <see cref="HookResult.Stop"/> to skip the remaining handlers for this candidate.
    /// The current <see cref="IsMatch"/> value is kept.
    /// </summary>
    public HookResult Result { get; set; }
}
