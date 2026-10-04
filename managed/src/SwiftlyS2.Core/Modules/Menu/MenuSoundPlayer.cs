using System.Globalization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using SwiftlyS2.Shared.Sounds;

namespace SwiftlyS2.Core.Menu;

internal enum MenuSound
{
    Scroll,
    Select,
    Exit,
    Fail
}

internal sealed class MenuSoundPlayer : IDisposable
{
    private const string Section = "MenuSounds";

    private static readonly (string Key, string DefaultName)[] Defaults = [
        ("Scroll", "UI.ContractType"),
        ("Select", "Vote.Cast.Yes"),
        ("Exit", "Vote.Failed"),
        ("Fail", "Vote.Cast.No")
    ];

    private readonly IConfiguration configuration;
    private readonly ILogger<MenuSoundPlayer> logger;
    private readonly SoundEvent effect = new();
    private readonly Lock soundLock = new();

    private volatile (string Name, float Volume)?[] settings = new (string, float)?[Defaults.Length];

    public MenuSoundPlayer( IConfiguration configuration, ILogger<MenuSoundPlayer> logger )
    {
        this.configuration = configuration;
        this.logger = logger;

        _ = ChangeToken.OnChange(configuration.GetReloadToken, () => settings = new (string, float)?[Defaults.Length]);
    }

    public void Play( MenuSound sound, int playerId )
    {
        var (name, volume) = Resolve(sound);

        if (string.IsNullOrWhiteSpace(name))
        {
            return;
        }

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

    private (string Name, float Volume) Resolve( MenuSound sound )
    {
        var index = sound switch {
            MenuSound.Scroll => 0,
            MenuSound.Select => 1,
            MenuSound.Exit => 2,
            MenuSound.Fail => 3,
            _ => 2
        };

        var current = settings;

        if (current[index] is { } cached)
        {
            return cached;
        }

        var (key, defaultName) = Defaults[index];
        var section = configuration.GetSection($"{Section}:{key}");

        var resolved = (
            section["Name"] ?? defaultName,
            float.TryParse(section["Volume"], NumberStyles.Float, CultureInfo.InvariantCulture, out var parsed) ? parsed : 0.75f);

        current[index] = resolved;
        return resolved;
    }
}
