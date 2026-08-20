using System.Reflection;
using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;

namespace SwiftlyS2.Core.AttributeParsers;

internal static class GameHooksAttributeParser
{
    private record HookEntry(Func<IGameHooks, object> Accessor, EventInfo Pre, EventInfo Post);

    private const int MaxDepth = 6;

    private static readonly Dictionary<Type, HookEntry> _hookMap = BuildHookMap();

    private static Dictionary<Type, HookEntry> BuildHookMap()
    {
        var map = new Dictionary<Type, HookEntry>();
        Visit(typeof(IGameHooks), hooks => hooks, 0, []);
        return map;

        void Visit( Type nodeType, Func<IGameHooks, object> accessorSoFar, int depth, HashSet<Type> visited )
        {
            if (depth >= MaxDepth || !visited.Add(nodeType)) return;

            foreach (var prop in nodeType.GetProperties())
            {
                var propType = prop.PropertyType;
                var preEvent = propType.GetEvent("Pre");
                var postEvent = propType.GetEvent("Post");

                var p = prop;
                object nextAccessor( IGameHooks hooks ) => p.GetValue(accessorSoFar(hooks))!;

                if (preEvent != null && postEvent != null)
                {
                    var invoke = preEvent.EventHandlerType!.GetMethod("Invoke")!;
                    var dataType = invoke.GetParameters()[0].ParameterType.GetElementType()!;
                    map[dataType] = new HookEntry(nextAccessor, preEvent, postEvent);
                }
                else
                {
                    Visit(propType, nextAccessor, depth + 1, visited);
                }
            }
        }
    }

    public static void ParseFromObject(this IGameHooks self, object instance)
    {
        var methods = instance.GetType()
            .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        foreach (var method in methods)
        {
            var attr = method.GetCustomAttribute<GameHookHandler>();
            if (attr == null) continue;

            var parameters = method.GetParameters();
            if (parameters.Length == 0) continue;

            var paramType = parameters[0].ParameterType;
            if (!paramType.IsByRef) continue;

            var eventType = paramType.GetElementType()!;
            if (!_hookMap.TryGetValue(eventType, out var entry)) continue;

            var eventsObj = entry.Accessor(self);
            var ev = attr.HookMode == HookMode.Pre ? entry.Pre : entry.Post;
            var handler = method.CreateDelegate(ev.EventHandlerType!, instance);
            ev.AddEventHandler(eventsObj, handler);
        }
    }
}
