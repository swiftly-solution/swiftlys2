using System.Reflection;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Core.AttributeParsers;

internal static class NetMessageAttributeParser {
  public static void ParseFromObject(this INetMessageService self, object instance) {
    var type = instance.GetType();
    var methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

    foreach (var method in methods)
    {
      var serverNetMessageHandlerAttribute = method.GetCustomAttribute<ServerNetMessageHandler>();
      var clientNetMessageHandlerAttribute = method.GetCustomAttribute<ClientNetMessageHandler>();

      if (serverNetMessageHandlerAttribute != null)
      {
        var msgType = method.GetParameters()[0].ParameterType;
        if (!msgType.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(INetMessage<>)))
        {
          throw new InvalidOperationException($"Type {msgType.Name} is not a valid net message type");
        }

        var hookMethod = typeof(INetMessageService).GetMethod("HookServerMessage")!;
        var hookMethodGeneric = hookMethod.MakeGenericMethod(msgType);
        var handlerType = typeof(INetMessageService.ServerNetMessageHandler<>).MakeGenericType(msgType);
        var handler = method.CreateDelegate(handlerType, instance);
        hookMethodGeneric.Invoke(self, new object[] { handler });
      }

      if (clientNetMessageHandlerAttribute != null)
      {
        var msgType = method.GetParameters()[0].ParameterType;
        if (!msgType.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(INetMessage<>)))
        {
          throw new InvalidOperationException($"Type {msgType.Name} is not a valid net message type");
        }

        var hookMethod = typeof(INetMessageService).GetMethod("HookClientMessage")!;
        var hookMethodGeneric = hookMethod.MakeGenericMethod(msgType);
        var handlerType = typeof(INetMessageService.ClientNetMessageHandler<>).MakeGenericType(msgType);
        var handler = method.CreateDelegate(handlerType, instance);
        hookMethodGeneric.Invoke(self, new object[] { handler });
      }
    }
  }
} 