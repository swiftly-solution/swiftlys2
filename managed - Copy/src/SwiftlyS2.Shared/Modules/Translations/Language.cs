namespace SwiftlyS2.Shared.Translation;


public sealed class Language : IEquatable<Language>
{
  public string Value { get; }

  public Language(string value)
  {
    Value = value;
  }

  internal static readonly List<string> RecognizedLanguages = new() {
    "ar",
    "bg",
    "zh-CN",
    "zh-TW",
    "cs",
    "da",
    "nl",
    "en",
    "fi",
    "fr",
    "de",
    "el",
    "hu",
    "id",
    "it",
    "ja",
    "ko",
    "no",
    "pl",
    "pt",
    "pt-BR",
    "ro",
    "ru",
    "es",
    "es-419",
    "sv",
    "th",
    "tr",
    "uk",
    "vn",
  };

  public static Language Arabic = new("ar");
  public static Language Bulgarian = new("bg");
  public static Language ChineseCN = new("zh-CN");
  public static Language ChineseTW = new("zh-TW");
  public static Language Czech = new("cs");
  public static Language Danish = new("da");
  public static Language Dutch = new("nl");
  public static Language English = new("en");
  public static Language Finnish = new("fi");
  public static Language French = new("fr");
  public static Language German = new("de");
  public static Language Greek = new("el");
  public static Language Hungarian = new("hu");
  public static Language Indonesian = new("id");
  public static Language Italian = new("it");
  public static Language Japanese = new("ja");
  public static Language Korean = new("ko");
  public static Language Norwegian = new("no");
  public static Language Polish = new("pl");
  public static Language Portuguese = new("pt");
  public static Language Brazilian = new("pt-BR");
  public static Language Romanian = new("ro");
  public static Language Russian = new("ru");
  public static Language Spanish = new("es");
  public static Language LatinAmerica = new("es-419");
  public static Language Swedish = new("sv");
  public static Language Thai = new("th");
  public static Language Turkish = new("tr");
  public static Language Ukrainian = new("uk");
  public static Language Vietnamese = new("vn");

  public override string ToString() => Value;

  public static implicit operator string(Language language) => language.Value;

  public bool Equals(Language? other)
  {
    return Value == other?.Value;
  }

  public override bool Equals(object? obj)
  {
    return Equals(obj as Language);
  }

  public override int GetHashCode()
  {
    return Value.GetHashCode();
  }
};