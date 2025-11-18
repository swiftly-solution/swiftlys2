using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.Translation;

public interface ITranslationService
{

  /// <summary>
  /// Gets the localizer for the specified player.
  /// </summary>
  /// <param name="player">The player to get the localizer for.</param>
  /// <returns>The localizer for the specified player.</returns>
  ILocalizer GetPlayerLocalizer(IPlayer player);
}