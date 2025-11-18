namespace SwiftlyS2.Shared.NetMessages;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class ServerNetMessageHandler : Attribute {
  public ServerNetMessageHandler() {
  }
}


[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class ClientNetMessageHandler : Attribute {
  public ClientNetMessageHandler() {
  }
}


