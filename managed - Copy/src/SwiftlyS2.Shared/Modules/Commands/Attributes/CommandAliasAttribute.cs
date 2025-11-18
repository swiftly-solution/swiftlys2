namespace SwiftlyS2.Shared.Commands;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class CommandAlias : Attribute {
  public string Alias { get; set; }

  public bool RegisterRaw { get; set; } = false;

  public CommandAlias(string alias, bool registerRaw = false) {
    Alias = alias;
    RegisterRaw = registerRaw;
  }
}
