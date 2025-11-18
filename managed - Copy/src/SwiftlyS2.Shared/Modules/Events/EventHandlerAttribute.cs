namespace SwiftlyS2.Shared.Events;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class EventListener<T> : Attribute where T : Delegate {


  public EventListener() {
  }

}
