namespace SwiftlyS2.Core.Menu.Config;

internal static class MenuConfigFile
{
  public const string DirectoryName = "menus";

  public const string FileName = "menus.jsonc";

  public static readonly string RelativePath = Path.Combine(DirectoryName, FileName);

  public const string SectionName = "Menus";
}
