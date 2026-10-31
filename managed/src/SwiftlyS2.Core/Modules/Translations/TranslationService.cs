using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Services;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.Translation;

namespace SwiftlyS2.Core.Translations;

internal class TranslationService : ITranslationService
{
    private CoreContext _Context { get; init; }
    private TranslationResource _TranslationResource { get; set; } = new();
    internal static Language ServerLanguage = new(NativeServerHelpers.GetServerLanguage());
    private static Localizer _EmptyLocalizer = new(ServerLanguage, [], []);

    public TranslationService( CoreContext context )
    {
        _Context = context;
        CreateTranslationResource();
    }

    public void CreateTranslationResource()
    {
        var translationDir = ResolveTranslationDirectory();

        if (translationDir == null)
        {
            return;
        }

        _TranslationResource = TranslationFactory.Create(translationDir);
    }

    public Language GetServerLanguage()
    {
        return ServerLanguage;
    }

    public Localizer GetLocalizer()
    {
        return _TranslationResource.Resources.Count == 0
        ? _EmptyLocalizer
        : TranslationFactory.CreateLocalizer(_TranslationResource, GetServerLanguage());
    }

    public ILocalizer GetPlayerLocalizer( IPlayer player )
    {
        if (_TranslationResource.Resources.Count == 0)
        {
            return _EmptyLocalizer;
        }

        var language = NativeServerHelpers.UsePlayerLanguage() ? player.PlayerLanguage : GetServerLanguage();
        return TranslationFactory.CreateLocalizer(_TranslationResource, language);
    }

    private string? ResolveTranslationDirectory()
    {
        var pluginTranslationDir = Path.Combine(_Context.BaseDirectory, "resources", "translations");
        return Directory.Exists(pluginTranslationDir) ? pluginTranslationDir : null;
    }

}