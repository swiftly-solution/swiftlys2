using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Services;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.Translation;

namespace SwiftlyS2.Core.Translations;

internal class TranslationService : ITranslationService
{
    private CoreContext _Context { get; init; }
    private TranslationResource _TranslationResource { get; set; } = new();
    private static Language ServerLanguage = new(NativeServerHelpers.GetServerLanguage());

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
        ? new Localizer([], [])
        : TranslationFactory.CreateLocalizer(_TranslationResource, GetServerLanguage());
    }

    public ILocalizer GetPlayerLocalizer( IPlayer player )
    {
        if (_TranslationResource.Resources.Count == 0)
        {
            return new Localizer([], []);
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