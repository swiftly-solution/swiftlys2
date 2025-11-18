namespace SwiftlyS2.Shared.Commands;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class ClientCommandHookHandler : Attribute {

  public ClientCommandHookHandler() {
  }
}