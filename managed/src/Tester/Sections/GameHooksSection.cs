using System.Runtime.Loader;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.GameHooks;
using Tester.Framework;
using Tester.Sections.Support;

namespace Tester.Sections;

public sealed class GameHooksSection( ISwiftlyCore core ) : Section(core)
{
    public override string Name => "gamehooks";

    public override Task Test( TestContext t )
    {
        var isolated = new AssemblyLoadContext("Tester.GameHooks", isCollectible: true);
        try
        {
            // Synthetic dispatch and seeded native counts must never reach the live server.
            var framework = typeof(IGameHooks).Assembly;
            var owner = AssemblyLoadContext.GetLoadContext(framework)!;
            isolated.Resolving += (_, name) => owner.LoadFromAssemblyName(name);
            isolated.LoadFromAssemblyPath(framework.Location);

            // Plugins load from memory, so their Assembly.Location is empty.
            var tests = isolated.LoadFromAssemblyPath(Path.Combine(Core.PluginPath, "Tester.dll"));
            var run = tests.GetType(typeof(GameHooksTests).FullName!)!
                .GetMethod(nameof(GameHooksTests.Run))!
                .CreateDelegate<Action<Action<string, Action>>>();
            run(t.Test);
        }
        finally
        {
            isolated.Unload();
        }
        return Task.CompletedTask;
    }
}
