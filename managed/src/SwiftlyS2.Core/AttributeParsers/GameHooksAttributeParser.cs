using System.Reflection;
using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;

namespace SwiftlyS2.Core.AttributeParsers;

internal static class GameHooksAttributeParser
{
    private record HookEntry( Func<IGameHooks, object> Accessor, EventInfo Pre, EventInfo Post );

    private static readonly Dictionary<Type, HookEntry> _hookMap = BuildHookMap();

    private static Dictionary<Type, HookEntry> BuildHookMap()
    {
        var map = new Dictionary<Type, HookEntry>();

        foreach (var categoryProp in typeof(IGameHooks).GetProperties())
        {
            foreach (var eventsProp in categoryProp.PropertyType.GetProperties())
            {
                var eventsType = eventsProp.PropertyType;
                var preEvent = eventsType.GetEvent("Pre");
                var postEvent = eventsType.GetEvent("Post");
                if (preEvent == null || postEvent == null) continue;

                var invoke = preEvent.EventHandlerType!.GetMethod("Invoke")!;
                var dataType = invoke.GetParameters()[0].ParameterType.GetElementType()!;

                var catProp = categoryProp;
                var evtProp = eventsProp;
                object accessor( IGameHooks hooks ) =>
                    evtProp.GetValue(catProp.GetValue(hooks)!)!;

                map[dataType] = new HookEntry(accessor, preEvent, postEvent);
            }
        }

        return map;
    }

    public static void ParseFromObject( this IGameHooks self, object instance )
    {
        Console.WriteLine("Parsing the gamehooks");
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
