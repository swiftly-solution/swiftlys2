using System.Globalization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SwiftlyS2.Shared.Sounds;

namespace SwiftlyS2.Core.Menu;

internal enum MenuSound
{
    Scroll,
    Select,
    Exit,
    Fail
}

internal sealed class MenuSoundPlayer( IConfiguration configuration, ILogger<MenuSoundPlayer> logger ) : IDisposable
{
    private const string Section = "MenuSounds";

    private readonly SoundEvent effect = new();
    private readonly Lock soundLock = new();

    public void Play( MenuSound sound, int playerId )
    {
        var (key, defaultName) = sound switch {
            MenuSound.Scroll => ("Scroll", "UI.ContractType"),
            MenuSound.Select => ("Select", "Vote.Cast.Yes"),
            MenuSound.Fail => ("Fail", "Vote.Cast.No"),
            _ => ("Exit", "Vote.Failed")
        };

        var section = configuration.GetSection($"{Section}:{key}");
        var name = section["Name"] ?? defaultName;

        if (string.IsNullOrWhiteSpace(name))
        {
            return;
        }

        var volume = float.TryParse(section["Volume"], NumberStyles.Float, CultureInfo.InvariantCulture, out var parsed)
            ? parsed
            : 0.75f;

        try
        {
            lock (soundLock)
            {
                effect.Name = name;
                effect.Volume = volume;
                effect.Recipients.AddRecipient(playerId);
                _ = effect.Emit();
                effect.Recipients.RemoveRecipient(playerId);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to play menu sound '{Sound}' for player {PlayerId}.", sound, playerId);
        }
    }

    public void Dispose() => effect.Dispose();
}
