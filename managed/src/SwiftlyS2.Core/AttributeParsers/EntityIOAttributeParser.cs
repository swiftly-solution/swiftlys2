using System.Reflection;
using SwiftlyS2.Shared.EntitySystem;

namespace SwiftlyS2.Core.AttributeParsers;

internal static class EntityIOAttributeParser
{
    public static void ParseFromObject( this IEntitySystemService self, object instance )
    {
        var methods = instance.GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        foreach (var method in methods)
        {
            var entityInputHandlerAttribute = method.GetCustomAttribute<EntityInputHandlerAttribute>();
            if (entityInputHandlerAttribute != null)
            {
                _ = self.HookEntityInput(entityInputHandlerAttribute.DesignerName, entityInputHandlerAttribute.InputName, method.CreateDelegate<IEntitySystemService.EntityInputEventHandler>(instance));
            }
            else
            {
                var entityOutputHandlerAttribute = method.GetCustomAttribute<EntityOutputHandlerAttribute>();
                if (entityOutputHandlerAttribute != null)
                {
                    _ = self.HookEntityOutput(entityOutputHandlerAttribute.DesignerName, entityOutputHandlerAttribute.OutputName, method.CreateDelegate<IEntitySystemService.EntityOutputEventHandler>(instance));
                }
            }
        }
    }
}