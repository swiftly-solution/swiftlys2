using System.Reflection;
using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.Events;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Misc;

namespace SwiftlyS2.Core.AttributeParsers;

internal static class EventAttributeParser
{
  public static void ParseFromObject(this IEventSubscriber self, object instance)
  {
    var type = instance.GetType();
    var methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
    foreach (var method in methods)
    {
      var eventHandlerAttributes = method.GetCustomAttributes();
      foreach (var eventHandlerAttribute in eventHandlerAttributes) {
        var attrType = eventHandlerAttribute.GetType();
        if (!attrType.IsGenericType || attrType.GetGenericTypeDefinition() != typeof(EventListener<>)) continue;

        var delegateType = attrType.GetGenericArguments()[0];
        if (delegateType.DeclaringType != typeof(EventDelegates)) {
          throw new InvalidOperationException($"Event type {delegateType.Name} is not a valid event type");
        }
        self
          .GetType()
          .GetEvents()
          .First(f => f.EventHandlerType == delegateType)
          .AddEventHandler(self, method.CreateDelegate(delegateType, instance));
      }
    }
  }
}