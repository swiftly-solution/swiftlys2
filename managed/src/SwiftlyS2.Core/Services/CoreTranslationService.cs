using System.Collections.Frozen;
using SwiftlyS2.Core.Players;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Events;

namespace SwiftlyS2.Core.Services;

internal class CoreTranslationService : IDisposable
{
    private static readonly FrozenDictionary<string, string> _languageCodes = new Dictionary<string, string> {
        { "arabic", "ar" },
        { "bulgarian", "bg" },
        { "schinese", "zh-CN" },
        { "tchinese", "zh-TW" },
        { "czech", "cs" },
        { "danish", "da" },
        { "dutch", "nl" },
        { "english", "en" },
        { "finnish", "fi" },
        { "french", "fr" },
        { "german", "de" },
        { "greek", "el" },
        { "hungarian", "hu" },
        { "indonesian", "id" },
        { "italian", "it" },
        { "japanese", "ja" },
        { "koreana", "ko" },
        { "norwegian", "no" },
        { "polish", "pl" },
        { "portuguese", "pt" },
        { "brazilian", "pt-BR" },
        { "romanian", "ro" },
        { "russian", "ru" },
        { "spanish", "es" },
        { "latam", "es-419" },
        { "swedish", "sv" },
        { "thai", "th" },
        { "turkish", "tr" },
        { "ukrainian", "uk" },
        { "vietnamese", "vn" },
    }.ToFrozenDictionary();

    private ISwiftlyCore _core { get; init; }

    public CoreTranslationService( ISwiftlyCore core )
    {
        _core = core;

        _core.Event.OnClientPutInServer += OnClientPutInServer;
    }

    private void OnClientPutInServer( IOnClientPutInServerEvent @event )
    {
        if (@event.Kind == ClientKind.Player)
        {
            var playerid = @event.PlayerId;
            _core.ConVar.QueryClient(playerid, "cl_language", ( value ) =>
            {
                var player = PlayerManagerService.GetPlayerObject(playerid);
                if (player == null) return;

                if (_languageCodes.TryGetValue(value, out var v))
                    player._language = new(v);
            });
        }
    }

    public void Dispose()
    {
        _core.Event.OnClientPutInServer -= OnClientPutInServer;
    }
}
