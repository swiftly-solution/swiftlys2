using System.Text.Json;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Translation;

namespace SwiftlyS2.Core.Translations;

internal class TranslationResource
{
    public Dictionary<Language, Dictionary<string, string>> Resources { get; set; } = [];
}

internal class TranslationFactory
{

    /// <summary>
    /// Creates a new <see cref="TranslationResource"/> from the specified resource directory.
    /// </summary>
    /// <param name="resourceDir">The directory containing the translation files.</param>
    /// <returns>A <see cref="TranslationResource"/> containing the translation files.</returns>
    /// <exception cref="Exception">Thrown when the language is not recognized.</exception>
    public static TranslationResource Create( string resourceDir )
    {

        var resource = new TranslationResource();

        var translationFiles = Directory.GetFiles(resourceDir, "*.jsonc");

        foreach (var translationFile in translationFiles)
        {
            var language = Path.GetFileNameWithoutExtension(translationFile);
            if (!Language.RecognizedLanguages.Contains(language))
            {
                throw new Exception($"Invalid language: {language}");
            }

            var options = new JsonSerializerOptions() {
                AllowTrailingCommas = true,
                ReadCommentHandling = JsonCommentHandling.Skip,
            };

            var translation = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(translationFile), options) ?? [];
            foreach (var translationEntry in translation)
            {
                translation[translationEntry.Key] = translationEntry.Value.Colored();
            }
            resource.Resources[new Language(language)] = translation;
        }

        if (resource.Resources.Count == 0)
        {
            throw new Exception("No translation files found.");
        }

        return !resource.Resources.ContainsKey(Language.English)
            ? throw new Exception("English primary translation file not found.")
            : resource;
    }


    public static Localizer CreateLocalizer( TranslationResource resource, Language language )
    {

        var defaultResource = resource.Resources[Language.English];

        return !resource.Resources.ContainsKey(language)
            ? new Localizer(defaultResource, defaultResource)
            : new Localizer(resource.Resources[language], defaultResource);
    }
}