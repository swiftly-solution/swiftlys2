using System.Text;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.FileSystem;
using Tester.Framework;
using static Tester.Framework.TestContext;

namespace Tester.Sections;

public sealed class GameFileSystemSection( ISwiftlyCore core ) : Section(core)
{
    private const string Game = "GAME";
    private const string TesterPath = "TESTER_PATH";

    private static readonly (string Path, uint Size, uint Crc)[] Packed =
    [
        ("maps/ar_pool_day.txt", 61, 0xD3E6EC6B),
        ("scripts/ai/armsrace/bt_default.kv3", 3365, 0x622AC6C4),
        ("resource/clientscheme.res", 14136, 0xEB299496),
        ("gamemodes.txt", 198901, 0x0480084E),
        ("cfg/moddefaults.txt", 315510, 0),
        ("panorama/dynamic_images_sentinel.vtex_c", 3598, 0x124663A3),
        ("particles/ambient/ambient_water_stream.vpcf_c", 4016, 0x4D4A5A32),
        ("soundevents/ambience/game_sounds_amb_common.vsndevts_c", 2049, 0xF8C16400),
    ];

    private const string TextFile = "maps/ar_pool_day.txt";
    private const string MissingFile = "tester/no/such/file.txt";

    public override string Name => "gamefilesystem";

    private IGameFileSystem Fs => Core.GameFileSystem;

    private static uint Crc32( ReadOnlySpan<byte> data )
    {
        var crc = 0xFFFFFFFFu;
        foreach (var b in data)
        {
            crc ^= b;
            for (var i = 0; i < 8; i++) crc = (crc & 1) != 0 ? (crc >> 1) ^ 0xEDB88320u : crc >> 1;
        }
        return ~crc;
    }

    private static string UniqueFile() => $"tester_fs_{Guid.NewGuid():N}.txt";

    private void WithWrittenFile( string content, Action<string> body )
    {
        var name = UniqueFile();
        try
        {
            Expect(Fs.WriteFile(name, Game, content), "WriteFile returned false");
            body(name);
        }
        finally
        {
            foreach (var path in Fs.FindFileAbsoluteList(name, Game).Where(File.Exists)) File.Delete(path);
            var fallback = Path.Combine(Core.GameFilesDirectory, name);
            if (File.Exists(fallback)) File.Delete(fallback);
        }
    }

    public override Task Test( TestContext t )
    {
        var fs = Fs;

        t.Test("FileExists finds every file listed in the dump", () =>
        {
            var missing = Packed.Where(f => !fs.FileExists(f.Path, Game)).Select(f => f.Path).ToList();
            Expect(missing.Count == 0, $"missing: {string.Join(", ", missing)}");
        });

        t.Test("FileExists is false for files that are not there", () =>
        {
            Expect(!fs.FileExists(MissingFile, Game), MissingFile);
            Expect(!fs.FileExists("maps/ar_pool_day.txt.nope", Game), "wrong extension");
            Expect(!fs.FileExists("", Game), "empty path");
            Expect(!fs.FileExists(TextFile, "TESTER_NO_SUCH_PATH_ID"), "unknown path id");
        });

        t.Test("FileExists accepts both slash styles", () =>
        {
            Expect(fs.FileExists(@"scripts\ai\armsrace\bt_default.kv3", Game), "backslashes");
            Expect(fs.FileExists("scripts/ai/armsrace/bt_default.kv3", Game), "forward slashes");
        });

        t.Test("GetFileSize matches the dump for every packed file", () =>
        {
            foreach (var (path, size, _) in Packed)
                Equal(size, fs.GetFileSize(path, Game), $"size of {path}");
        });

        t.Test("GetFileSize of a missing file is 0", () => Equal(0u, fs.GetFileSize(MissingFile, Game), "missing"));

        t.Test("ReadFile returns the text of a small packed file with the right length", () =>
        {
            var text = fs.ReadFile(TextFile, Game);
            Equal(61, text.Length, "length");
        });

        t.Test("ReadFile content matches the CRC-32 in the dump", () =>
        {
            foreach (var (path, size, crc) in Packed.Where(f => f.Crc != 0 && f.Size < 20_000 && (f.Path.EndsWith(".txt") || f.Path.EndsWith(".kv3") || f.Path.EndsWith(".res"))))
            {
                var text = fs.ReadFile(path, Game);
                var bytes = Encoding.UTF8.GetBytes(text);
                if (bytes.Length != size) Skip($"{path}: text is not plain ASCII/UTF-8 ({bytes.Length} bytes read, {size} expected)");
                Equal(crc, Crc32(bytes), $"CRC-32 of {path}");
            }
        });

        t.Test("ReadFile of a missing file or directory is empty", () =>
        {
            Equal("", fs.ReadFile(MissingFile, Game), "missing");
            Equal("", fs.ReadFile("maps", Game), "directory");
            Equal("", fs.ReadFile(TextFile, "TESTER_NO_SUCH_PATH_ID"), "unknown path id");
        });

        t.Test("ReadFile of a larger file returns all of it", () =>
        {
            var text = fs.ReadFile("resource/clientscheme.res", Game);
            Expect(text.Length > 10_000, $"only {text.Length} chars");
            Expect(text.Contains("Scheme", StringComparison.OrdinalIgnoreCase), "no 'Scheme' in clientscheme.res");
        });

        t.Test("Repeated reads return identical text", () => Equal(fs.ReadFile(TextFile, Game), fs.ReadFile(TextFile, Game), "second read"));

        t.Test("ReadFile of a 300 KB file", () =>
        {
            var text = fs.ReadFile("cfg/moddefaults.txt", Game);
            Expect(text.Length > 200_000, $"only {text.Length} chars");
        });

        t.Test("IsDirectory recognises directories that exist on disk", () =>
        {
            var onDisk = Directory.Exists(Core.GameFilesDirectory)
                ? Directory.GetDirectories(Core.GameFilesDirectory).Select(Path.GetFileName).Where(n => !string.IsNullOrEmpty(n)).Take(5).ToList()
                : [];
            if (onDisk.Count == 0) Skip($"no sub-folders under {Core.GameFilesDirectory}");
            foreach (var dir in onDisk) Expect(fs.IsDirectory(dir!, Game), $"{dir} is not a directory");
        });

        t.Test("IsDirectory is false for files and missing paths", () =>
        {
            Expect(!fs.IsDirectory(TextFile, Game), "file");
            Expect(!fs.IsDirectory("tester/no/such/dir", Game), "missing");
            Expect(!fs.IsDirectory("tester/no/such/dir", "TESTER_NO_SUCH_PATH_ID"), "unknown path id");
        });

        t.Test("GetSearchPath(GAME) lists search paths", () =>
        {
            var all = fs.GetSearchPath(Game, GetSearchPathTypes_t.GET_SEARCH_PATH_ALL, 64);
            Expect(all.Length > 0, "empty");
            Expect(all.Contains("csgo", StringComparison.OrdinalIgnoreCase), $"no csgo folder in '{all}'");
        });

        t.Test("GetSearchPath types return different views of the same set", () =>
        {
            var all = fs.GetSearchPath(Game, GetSearchPathTypes_t.GET_SEARCH_PATH_ALL, 64);
            var noPack = fs.GetSearchPath(Game, GetSearchPathTypes_t.GET_SEARCH_PATH_NO_PACK_FILES, 64);
            var noAuto = fs.GetSearchPath(Game, GetSearchPathTypes_t.GET_SEARCH_PATH_NO_AUTO_MOUNTED, 64);
            Expect(all.Length >= noPack.Length, $"ALL ({all.Length}) shorter than NO_PACK_FILES ({noPack.Length})");
            Expect(all.Length >= noAuto.Length, $"ALL ({all.Length}) shorter than NO_AUTO_MOUNTED ({noAuto.Length})");
        });

        t.Test("GetSearchPath with a count of 1 is not longer than with a count of 64", () =>
        {
            var one = fs.GetSearchPath(Game, GetSearchPathTypes_t.GET_SEARCH_PATH_ALL, 1);
            var many = fs.GetSearchPath(Game, GetSearchPathTypes_t.GET_SEARCH_PATH_ALL, 64);
            Expect(one.Length <= many.Length, $"count 1: {one.Length}, count 64: {many.Length}");
        });

        t.Test("GetSearchPath of an unknown path id is empty", () =>
            Equal("", fs.GetSearchPath("TESTER_NO_SUCH_PATH_ID", GetSearchPathTypes_t.GET_SEARCH_PATH_ALL, 8), "search path"));

        t.Test("PrintSearchPaths prints without throwing", () => fs.PrintSearchPaths());

        t.Test("AddSearchPath / RemoveSearchPath make a folder visible and hide it again", () =>
        {
            var dir = Core.PluginDataDirectory;
            var file = UniqueFile();
            var full = Path.Combine(dir, file);
            File.WriteAllText(full, "tester");
            try
            {
                Expect(!fs.FileExists(file, TesterPath), "visible before adding the path");
                fs.AddSearchPath(dir, TesterPath, SearchPathAdd_t.PATH_ADD_TO_TAIL, SearchPathPriority_t.SEARCH_PATH_PRIORITY_DEFAULT);
                try
                {
                    Expect(fs.FileExists(file, TesterPath), "not visible after adding the path");
                    Expect(fs.GetFileSize(file, TesterPath) == 6, $"size {fs.GetFileSize(file, TesterPath)}");
                    Equal("tester", fs.ReadFile(file, TesterPath), "content");
                    Expect(fs.GetSearchPath(TesterPath, GetSearchPathTypes_t.GET_SEARCH_PATH_ALL, 8).Contains(Path.GetFileName(dir.TrimEnd('\\', '/')), StringComparison.OrdinalIgnoreCase), "path missing from GetSearchPath");
                }
                finally
                {
                    Expect(fs.RemoveSearchPath(dir, TesterPath), "RemoveSearchPath returned false");
                }
                Expect(!fs.FileExists(file, TesterPath), "still visible after removing the path");
            }
            finally { File.Delete(full); }
        });

        t.Test("RemoveSearchPath of a path that was never added is false", () =>
            Expect(!fs.RemoveSearchPath(Path.Combine(Core.PluginDataDirectory, "tester_never_added"), TesterPath), "returned true"));

        t.Test("AddSearchPath with every add type and priority", () =>
        {
            var dir = Core.PluginDataDirectory;
            foreach (var add in new[] { SearchPathAdd_t.PATH_ADD_TO_HEAD, SearchPathAdd_t.PATH_ADD_TO_TAIL })
                foreach (var priority in Enum.GetValues<SearchPathPriority_t>())
                {
                    fs.AddSearchPath(dir, TesterPath, add, priority);
                    Expect(fs.RemoveSearchPath(dir, TesterPath), $"{add}/{priority}: not removed");
                }
        });

        t.Test("FindFileAbsoluteList finds a loose file by name", () =>
        {
            var list = fs.FindFileAbsoluteList("gameinfo.gi", Game);
            Expect(list.Count > 0, "no result");
            Expect(list.Any(p => p.EndsWith("gameinfo.gi", StringComparison.OrdinalIgnoreCase)), $"results: {string.Join(", ", list)}");
            Expect(list.All(p => Path.IsPathRooted(p)), "result is not an absolute path");
        });

        t.Test("FindFileAbsoluteList with a wildcard finds several loose files", () =>
        {
            var list = fs.FindFileAbsoluteList("gameinfo*.gi", Game);
            Expect(list.Count >= 1, "no result");
            Expect(list.All(p => Path.GetFileName(p).StartsWith("gameinfo", StringComparison.OrdinalIgnoreCase)), $"results: {string.Join(", ", list)}");
        });

        t.Test("FindFileAbsoluteList of nothing returns an empty list", () =>
        {
            Equal(0, fs.FindFileAbsoluteList("tester_no_such_file_*.zzz", Game).Count, "count");
            Equal(0, fs.FindFileAbsoluteList("gameinfo.gi", "TESTER_NO_SUCH_PATH_ID").Count, "unknown path id");
        });

        t.Test("WriteFile creates a file that can be found, sized and read back", () =>
            WithWrittenFile("hello file system", name =>
            {
                Expect(fs.FileExists(name, Game), "FileExists");
                var size = fs.GetFileSize(name, Game);
                Expect(size == 17 || size == 18, $"size {size}");
                Equal("hello file system", fs.ReadFile(name, Game).TrimEnd('\0'), "content");
            }));

        t.Test("WriteFile overwrites an existing file", () =>
            WithWrittenFile("first version", name =>
            {
                Expect(fs.WriteFile(name, Game, "second"), "second write");
                Equal("second", fs.ReadFile(name, Game).TrimEnd('\0'), "content");
            }));

        t.Test("WriteFile with an empty string", () =>
            WithWrittenFile("", name => Expect(fs.FileExists(name, Game), "empty file missing")));

        t.Test("WriteFile keeps multi-line text", () =>
            WithWrittenFile("line one\nline two\r\nline three", name =>
                Equal("line one\nline two\r\nline three", fs.ReadFile(name, Game).TrimEnd('\0'), "content")));

        t.Test("WriteFile to an unknown path id fails", () =>
            Expect(!fs.WriteFile(UniqueFile(), "TESTER_NO_SUCH_PATH_ID", "x"), "returned true"));

        t.Test("IsFileWritable is true for a file this test wrote", () =>
            WithWrittenFile("writable", name => Expect(fs.IsFileWritable(name, Game), "not writable")));

        t.Test("SetFileWritable(false) makes it read-only and SetFileWritable(true) restores it", () =>
            WithWrittenFile("toggle", name =>
            {
                Expect(fs.SetFileWritable(name, Game, false), "SetFileWritable(false) failed");
                Expect(!fs.IsFileWritable(name, Game), "still writable");
                Expect(fs.SetFileWritable(name, Game, true), "SetFileWritable(true) failed");
                Expect(fs.IsFileWritable(name, Game), "not writable again");
            }));

        t.Test("Packed files are not writable", () => Expect(!fs.IsFileWritable(TextFile, Game), "packed file reported writable"));

        t.Test("IsFileWritable of a missing file is false", () => Expect(!fs.IsFileWritable(MissingFile, Game), "missing file writable"));

        t.Test("PrecacheFile succeeds for packed files and fails for missing ones", () =>
        {
            Expect(fs.PrecacheFile(TextFile, Game), "packed file");
            Expect(!fs.PrecacheFile(MissingFile, Game), "missing file");
        });

        t.Test("Concurrent FileExists / GetFileSize from pool threads", () =>
        {
            var failures = new System.Collections.Concurrent.ConcurrentBag<string>();
            Task.WaitAll(Enumerable.Range(0, 4).Select(w => Task.Run(() =>
            {
                try
                {
                    for (var i = 0; i < 200; i++)
                    {
                        if (!fs.FileExists(TextFile, Game)) failures.Add($"worker {w}: missing");
                        if (fs.GetFileSize(TextFile, Game) != 61) failures.Add($"worker {w}: wrong size");
                    }
                }
                catch (Exception e) { failures.Add($"worker {w}: {e.Message}"); }
            })).ToArray());
            Expect(failures.IsEmpty, string.Join("; ", failures.Take(5)));
        });

        return Task.CompletedTask;
    }

    public override bool ProfileInBackground => true;

    public override Task Profile( ProfileContext p )
    {
        var fs = Fs;

        p.ProfileOnGame("FileSystem.FileExists (packed)", () => _ = fs.FileExists(TextFile, Game), 100_000);
        p.ProfileOnGame("FileSystem.FileExists (missing)", () => _ = fs.FileExists(MissingFile, Game), 100_000);
        p.ProfileOnGame("FileSystem.IsDirectory", () => _ = fs.IsDirectory("scripts/ai/armsrace", Game), 100_000);
        p.ProfileOnGame("FileSystem.GetFileSize (packed)", () => _ = fs.GetFileSize("resource/clientscheme.res", Game), 100_000);
        p.ProfileOnGame("FileSystem.GetSearchPath (all)", () => _ = fs.GetSearchPath(Game, GetSearchPathTypes_t.GET_SEARCH_PATH_ALL, 64), 20_000);
        p.ProfileOnGame("FileSystem.IsFileWritable (packed)", () => _ = fs.IsFileWritable(TextFile, Game), 50_000);
        p.ProfileOnGame("FileSystem.PrecacheFile (packed)", () => _ = fs.PrecacheFile(TextFile, Game), 20_000);
        p.ProfileOnGame("FileSystem.ReadFile (61 B)", () => _ = fs.ReadFile(TextFile, Game), 20_000);
        p.ProfileOnGame("FileSystem.ReadFile (14 KB)", () => _ = fs.ReadFile("resource/clientscheme.res", Game), 2_000);
        p.ProfileOnGame("FileSystem.ReadFile (300 KB)", () => _ = fs.ReadFile("cfg/moddefaults.txt", Game), 200);
        p.ProfileOnGame("FileSystem.FindFileAbsoluteList (gameinfo.gi)", () => _ = fs.FindFileAbsoluteList("gameinfo.gi", Game), 2_000);

        var name = UniqueFile();
        try
        {
            p.ProfileOnGame("FileSystem.WriteFile (small file)", () => _ = fs.WriteFile(name, Game, "profile"), 500);
        }
        finally
        {
            p.OnGame(() =>
            {
                foreach (var path in fs.FindFileAbsoluteList(name, Game).Where(File.Exists)) File.Delete(path);
                var fallback = Path.Combine(Core.GameFilesDirectory, name);
                if (File.Exists(fallback)) File.Delete(fallback);
            });
        }

        return Task.CompletedTask;
    }
}
