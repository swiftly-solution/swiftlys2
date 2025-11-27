using System.Reflection;
using SwiftlyS2.Shared.Commands;

namespace SwiftlyS2.Core.AttributeParsers;

internal static class CommandAttributeParser
{
    public static void ParseFromObject( this ICommandService self, object instance )
    {
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
                var registerRaw = commandAttribute.RegisterRaw;
                var permission = commandAttribute.Permission;
                Console.WriteLine($"[DEBUG-Parser] RegisterCommand: {commandName}, registerRaw={registerRaw}");
                var cmdGuid = self.RegisterCommand(commandName, method.CreateDelegate<ICommandService.CommandListener>(instance), registerRaw, permission);
                Console.WriteLine($"[DEBUG-Parser] RegisterCommand result: guid={cmdGuid}");

                foreach (var aliasAttr in commandAliasAttributes)
                {
                    Console.WriteLine($"[DEBUG-Parser] RegisterCommandAlias: commandName={commandName}, alias={aliasAttr.Alias}, aliasRegisterRaw={aliasAttr.RegisterRaw}");
                    self.RegisterCommandAlias(commandName, aliasAttr.Alias, aliasAttr.RegisterRaw);
                }
            }

            if (clientCommandHookHandlerAttribute != null)
            {
                _ = self.HookClientCommand(method.CreateDelegate<ICommandService.ClientCommandHandler>(instance));
            }

            if (clientChatHookHandlerAttribute != null)
            {
                _ = self.HookClientChat(method.CreateDelegate<ICommandService.ClientChatHandler>(instance));
            }
        }
    }
}