using SwiftlyS2.Shared;
using Tester.Framework;
using static Tester.Framework.TestContext;

namespace Tester.Sections;

public sealed class EnvironmentSection( ISwiftlyCore core ) : Section(core)
{
    public override string Name => "environment";

    public override async Task Test( TestContext t )
    {
        t.Test("IsGameThread is true on the game thread", () => Expect(Core.IsGameThread, "false on game thread"));

        t.Test("IsGameThread is false on a pool thread", () =>
        {
            var onPool = Task.Run(() => Core.IsGameThread).GetAwaiter().GetResult();
            Expect(!onPool, "true on a pool thread");
        });

        await t.TestAsync("IsGameThread is true inside a scheduler callback", async () =>
            Expect(await Core.Scheduler.NextTickAsync(() => Core.IsGameThread), "false inside NextTickAsync"));

        t.Test("PluginDataDirectory exists and is absolute", () =>
        {
            var dir = Core.PluginDataDirectory;
            Expect(!string.IsNullOrWhiteSpace(dir), "empty");
            Expect(Path.IsPathRooted(dir), $"not absolute: {dir}");
            Expect(Directory.Exists(dir), $"missing: {dir}");
        });

        t.Test("PluginDataDirectory is writable", () =>
        {
            var file = Path.Combine(Core.PluginDataDirectory, $".tester-{Guid.NewGuid():N}.tmp");
            try
            {
                File.WriteAllText(file, "swiftly");
                Equal("swiftly", File.ReadAllText(file), "round trip");
            }
            finally
            {
                if (File.Exists(file)) File.Delete(file);
            }
            Expect(!File.Exists(file), "temp file not removed");
        });

        t.Test("PluginPath exists and is absolute", () =>
        {
            var dir = Core.PluginPath;
            Expect(!string.IsNullOrWhiteSpace(dir), "empty");
            Expect(Path.IsPathRooted(dir), $"not absolute: {dir}");
            Expect(Directory.Exists(dir), $"missing: {dir}");
        });

        t.Test("PluginPath contains this plugin's assembly", () =>
        {
            var name = typeof(TesterPlugin).Assembly.GetName().Name + ".dll";
            Expect(File.Exists(Path.Combine(Core.PluginPath, name)), $"{name} not found in {Core.PluginPath}");
        });

        t.Test("GameDirectory exists and is absolute", () =>
        {
            var dir = Core.GameDirectory;
            Expect(!string.IsNullOrWhiteSpace(dir), "empty");
            Expect(Path.IsPathRooted(dir), $"not absolute: {dir}");
            Expect(Directory.Exists(dir), $"missing: {dir}");
        });

        t.Test("GameFilesDirectory exists and is inside GameDirectory", () =>
        {
            var dir = Core.GameFilesDirectory;
            Expect(!string.IsNullOrWhiteSpace(dir), "empty");
            Expect(Directory.Exists(dir), $"missing: {dir}");
            var root = Path.TrimEndingDirectorySeparator(Path.GetFullPath(Core.GameDirectory)) + Path.DirectorySeparatorChar;
            Expect(Path.GetFullPath(dir).StartsWith(root, StringComparison.OrdinalIgnoreCase), $"{dir} is not under {Core.GameDirectory}");
        });

        t.Test("GameFilesDirectory contains gameinfo.gi", () =>
        {
            if (!File.Exists(Path.Combine(Core.GameFilesDirectory, "gameinfo.gi")))
                Skip("gameinfo.gi not found (different game layout)");
        });

        t.Test("PluginDataDirectory is neither the plugin path nor a game directory", () =>
        {
            var data = Path.GetFullPath(Core.PluginDataDirectory);
            Expect(!data.Equals(Path.GetFullPath(Core.PluginPath), StringComparison.OrdinalIgnoreCase), "same as PluginPath");
            Expect(!data.Equals(Path.GetFullPath(Core.GameDirectory), StringComparison.OrdinalIgnoreCase), "same as GameDirectory");
            Expect(!data.Equals(Path.GetFullPath(Core.GameFilesDirectory), StringComparison.OrdinalIgnoreCase), "same as GameFilesDirectory");
        });

        t.Test("Directory properties are stable between calls", () =>
        {
            Equal(Core.PluginDataDirectory, Core.PluginDataDirectory, "PluginDataDirectory");
            Equal(Core.PluginPath, Core.PluginPath, "PluginPath");
            Equal(Core.GameDirectory, Core.GameDirectory, "GameDirectory");
            Equal(Core.GameFilesDirectory, Core.GameFilesDirectory, "GameFilesDirectory");
        });
    }

    public override bool ProfileInBackground => true;

    public override Task Profile( ProfileContext p )
    {
        var core = Core;
        p.Profile("Core.IsGameThread", () => _ = core.IsGameThread, 200_000);
        p.Profile("Core.PluginDataDirectory", () => _ = core.PluginDataDirectory, 200_000);
        p.Profile("Core.PluginPath", () => _ = core.PluginPath, 200_000);
        p.Profile("Core.GameDirectory", () => _ = core.GameDirectory, 100_000);
        p.Profile("Core.GameFilesDirectory", () => _ = core.GameFilesDirectory, 100_000);
        p.Profile("Core.IsGameThread (pool thread, via Task.Run)", () => Task.Run(() => core.IsGameThread).GetAwaiter().GetResult(), 2_000);
        return Task.CompletedTask;
    }
}
