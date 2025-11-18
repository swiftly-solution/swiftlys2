using System.Reflection;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Misc;

namespace SwiftlyS2.Core.AttributeParsers;

internal static class GameEventAttributeParser
{
    public static void ParseFromObject( this IGameEventService self, object instance )
    {
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
                var hookMethod = gameEventHandlerAttribute.HookMode == HookMode.Pre
                    ? typeof(IGameEventService).GetMethod("HookPre")!
                    : gameEventHandlerAttribute.HookMode == HookMode.Post
                        ? typeof(IGameEventService).GetMethod("HookPost")!
                        : throw new InvalidOperationException($"Invalid hook mode: {gameEventHandlerAttribute.HookMode}");
                var hookMethodGeneric = hookMethod.MakeGenericMethod(eventType);
                _ = hookMethodGeneric.Invoke(self, new object[] { eventHandler });
            }
        }
    }
}