namespace SwiftlyS2.Shared.Commands;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class ClientChatHookHandler : Attribute {
  public ClientChatHookHandler() {
  }
}