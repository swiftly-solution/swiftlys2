namespace SwiftlyS2.Shared.Commands;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class Command : Attribute {
  public string Name { get; set; }

  public bool RegisterRaw { get; set; } = false;

  public string Permission { get; set; } = "";

  public Command(string name, bool registerRaw = false, string permission = "") {
    Name = name;
    RegisterRaw = registerRaw;
    Permission = permission;
  }
}