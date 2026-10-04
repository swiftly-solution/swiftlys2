using System.Text.Json;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared;

namespace SwiftlyS2.Core.Translations;

internal static class GlobalLocalization
{
    private static IReadOnlyDictionary<string, string> _values = new Dictionary<string, string>();
    private static readonly Lock _sync = new();

    public static void InitializeFromCore( string basePath )
    {
        lock (_sync)
        {
            var language = NativeServerHelpers.GetServerLanguage();
            var translationsDir = Path.Combine(basePath, "translations");
            _values = ParseTranslations(translationsDir, language);
        }
    }

    public static string MenuDefaultTitle() => Get("menu.default_title");
    public static string MenuDefaultOption() => Get("menu.default_option");
    public static string MenuMoveLabel() => Get("menu.footer.move");
    public static string MenuUseLabel() => Get("menu.footer.use");
    public static string MenuExitLabel() => Get("menu.footer.exit");
    public static string MenuChatNumberLabel() => Get("menu.footer.chat_number");
    public static string MenuInputHintTemplate() => Get("menu.input.hint_template");
    public static string MenuInputEmptyValue() => Get("menu.input.empty_value");
    public static string MenuInputWaiting() => Get("menu.input.waiting");
    public static string MenuInputCancelHint() => Get("menu.input.cancel_hint");
    public static string MenuInputInvalid() => Get("menu.input.invalid");
    public static string MenuInputAccepted() => Get("menu.input.accepted");
    public static string MenuChoiceDefaultValue() => Get("menu.choice.default_value");
    public static string MenuSelectorEmptyValue() => Get("menu.selector.empty_value");
    public static string MenuSelectorClaimLeft() => Get("menu.selector.claim_left");
    public static string MenuSelectorClaimRight() => Get("menu.selector.claim_right");
    public static string PermissionCommandDenied() => Get("permission.command.denied");

    public static string MenuInputHint( int maxLength ) => string.Format(MenuInputHintTemplate(), maxLength);

    private static Dictionary<string, string> ParseTranslations( string translationsDir, string language )
    {
        if (!Directory.Exists(translationsDir))
        {
            return [];
        }

        var languageFile = Path.Combine(translationsDir, $"{language}.jsonc");

        if (!File.Exists(languageFile))
        {
            throw new Exception($"No translation file found for language: {language}");
        }

        var options = new JsonSerializerOptions {
            AllowTrailingCommas = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
        };

        var parsed = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(languageFile), options) ?? [];
        foreach (var entry in parsed)
        {
            parsed[entry.Key] = entry.Value.Colored();
        }

        return parsed;
    }

    private static string Get( string key )
    {
        return _values.TryGetValue(key, out var value) && !string.IsNullOrWhiteSpace(value) ? value : key;
    }
}
