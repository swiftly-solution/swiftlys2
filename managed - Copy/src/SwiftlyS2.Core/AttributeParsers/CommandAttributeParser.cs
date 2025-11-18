using System.Reflection;
using SwiftlyS2.Core.Commands;
using SwiftlyS2.Shared.Commands;

namespace SwiftlyS2.Core.AttributeParsers;

internal static class CommandAttributeParser {
  public static void ParseFromObject(this ICommandService self, object instance) {
    var type = instance.GetType();
    var methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
    foreach (var method in methods)
    {
      var commandAttribute = method.GetCustomAttribute<Command>();
      var clientCommandHookHandlerAttribute = method.GetCustomAttribute<ClientCommandHookHandler>();
      var clientChatHookHandlerAttribute = method.GetCustomAttribute<ClientChatHookHandler>();
      if (commandAttribute != null)
      {
        var commandAliasAttributes = method.GetCustomAttributes<CommandAlias>();
        var commandName = commandAttribute.Name;
        var commandAlias = commandAliasAttributes.Select(a => a.Alias).ToArray();
        var registerRaw = commandAttribute.RegisterRaw;
        var permission = commandAttribute.Permission;
        self.RegisterCommand(commandName, method.CreateDelegate<ICommandService.CommandListener>(instance), registerRaw, permission);
        foreach (var alias in commandAlias)
        {
          self.RegisterCommandAlias(commandName, alias, registerRaw);
        }
      }

      if (clientCommandHookHandlerAttribute != null)
      {
        self.HookClientCommand(method.CreateDelegate<ICommandService.ClientCommandHandler>(instance));
      }

      if (clientChatHookHandlerAttribute != null)
      {
        self.HookClientChat(method.CreateDelegate<ICommandService.ClientChatHandler>(instance));
      }
    }
  }
}