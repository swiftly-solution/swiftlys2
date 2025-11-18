using System.Reflection;
using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Misc;

namespace SwiftlyS2.Core.AttributeParsers;

internal static class GameEventAttributeParser {
  public static void ParseFromObject(this IGameEventService self, object instance) {
    var type = instance.GetType();
    var methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
    foreach (var method in methods)
    {
      var gameEventHandlerAttribute = method.GetCustomAttribute<GameEventHandler>();
      if (gameEventHandlerAttribute != null)
      {
        var eventType = method.GetParameters()[0].ParameterType;
        var handlerType = typeof(IGameEventService.GameEventHandler<>).MakeGenericType(eventType);
        var eventHandler = method.CreateDelegate(handlerType, instance);
        MethodInfo hookMethod;
        if (gameEventHandlerAttribute.HookMode == HookMode.Pre)
        {
          hookMethod = typeof(IGameEventService).GetMethod("HookPre")!;
        }
        else if (gameEventHandlerAttribute.HookMode == HookMode.Post)
        {
          hookMethod = typeof(IGameEventService).GetMethod("HookPost")!;
        }
        else
        {
          throw new InvalidOperationException($"Invalid hook mode: {gameEventHandlerAttribute.HookMode}");
        }
        var hookMethodGeneric = hookMethod.MakeGenericMethod(eventType);
        hookMethodGeneric.Invoke(self, new object[] { eventHandler });
      }
    }
  }
}