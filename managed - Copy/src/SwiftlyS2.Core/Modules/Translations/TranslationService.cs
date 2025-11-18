using System.Text.Json;
using Microsoft.Extensions.Logging;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Services;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.Translation;

namespace SwiftlyS2.Core.Translations;

internal class TranslationService : ITranslationService
{

  private ILogger<TranslationService> _Logger { get; init; }  
  private CoreContext _Context { get; init; }
  private TranslationResource _TranslationResource { get; set; } = new();

  public TranslationService(ILogger<TranslationService> logger, CoreContext context)
  {
    _Logger = logger;
    _Context = context;

    var translationDir = Path.Combine(_Context.BaseDirectory, "resources", "translations");

    if (!Directory.Exists(translationDir)) {
      return;
    }

    if (!File.Exists(Path.Combine(translationDir, "en.jsonc"))) {
      return;
    }

    _TranslationResource = TranslationFactory.Create(translationDir)!;
  }

  public Language GetServerLanguage() {
    return new Language(NativeServerHelpers.GetServerLanguage());
  }

  public Localizer GetLocalizer() {
    if (_TranslationResource.Resources.Count == 0) {
      return new Localizer(new Dictionary<string, string>(), new Dictionary<string, string>());
    }

    return TranslationFactory.CreateLocalizer(_TranslationResource, GetServerLanguage());
  }

  public ILocalizer GetPlayerLocalizer(IPlayer player)
  {
    if (_TranslationResource.Resources.Count == 0) {
      return new Localizer(new Dictionary<string, string>(), new Dictionary<string, string>());
    }

    var language = NativeServerHelpers.UsePlayerLanguage() ? player.PlayerLanguage : GetServerLanguage();
    return TranslationFactory.CreateLocalizer(_TranslationResource, language);
  }
}