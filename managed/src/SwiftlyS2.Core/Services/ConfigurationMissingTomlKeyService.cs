using Tomlyn;
using Tomlyn.Syntax;

namespace SwiftlyS2.Core.Services;

internal static class ConfigurationMissingTomlKeyService
{
    public static string? AddMissingKeys( string text, string defaults )
    {
        var newLine = text.Contains("\r\n") ? "\r\n" : "\n";
        var normalized = text.Replace("\r\n", "\n");
        var document = Toml.Parse(normalized);
        if (document.HasErrors)
        {
            return null;
        }

        var normalizedDefaults = defaults.Replace("\r\n", "\n");
        var lines = normalized.Split('\n');
        var defaultLines = normalizedDefaults.Split('\n');
        var file = TableSpan.Read(document);
        var inserts = new Dictionary<int, List<string>>();
        var appended = new List<string>();

        foreach (var table in TableSpan.Read(Toml.Parse(normalizedDefaults)).Where(table => table.Path.Count > 0))
        {
            var existing = table.IsArray ? null : file.FirstOrDefault(other => !other.IsArray && SamePath(other.Path, table.Path));
            if (existing is null)
            {
                if (!IsMentioned(file, table.Path))
                {
                    appended.AddRange(TrimBlankLines(defaultLines[table.Start..(table.LastLine + 1)]));
                }
                continue;
            }

            foreach (var key in table.Keys)
            {
                if (existing.Keys.Any(other => SameSegment(other.Name, key.Name))
                    || file.Any(other => StartsWith(other.Path, [.. table.Path, key.Name])))
                {
                    continue;
                }
                if (!inserts.TryGetValue(existing.LastLine, out var block))
                {
                    inserts[existing.LastLine] = block = [];
                }
                block.AddRange(TrimBlankLines(defaultLines[key.Start..(key.End + 1)]));
            }
        }

        if (inserts.Count == 0 && appended.Count == 0)
        {
            return null;
        }

        var output = new List<string>();
        for (var i = 0; i < lines.Length; i++)
        {
            output.Add(lines[i]);
            if (inserts.TryGetValue(i, out var block))
            {
                output.AddRange(block);
            }
        }
        if (appended.Count > 0)
        {
            output = TrimBlankLines(output);
            if (output.Count > 0)
            {
                output.Add("");
            }
            output.AddRange(appended);
            output.Add("");
        }

        return string.Join(newLine, output);
    }

    private static bool SameSegment( string left, string right )
    {
        return string.Equals(left, right, StringComparison.OrdinalIgnoreCase);
    }

    private static bool SamePath( List<string> left, List<string> right )
    {
        return left.Count == right.Count && StartsWith(left, right);
    }

    private static bool StartsWith( List<string> path, List<string> prefix )
    {
        if (path.Count < prefix.Count)
        {
            return false;
        }
        for (var i = 0; i < prefix.Count; i++)
        {
            if (!SameSegment(path[i], prefix[i]))
            {
                return false;
            }
        }

        return true;
    }

    private static bool IsMentioned( List<TableSpan> file, List<string> path )
    {
        return file.Any(table => StartsWith(table.Path, path)
            || (table.Path.Count < path.Count
                && StartsWith(path, table.Path)
                && (table.IsArray || table.Keys.Any(key => SameSegment(key.Name, path[table.Path.Count])))));
    }

    private static List<string> TrimBlankLines( IEnumerable<string> lines )
    {
        var list = lines.SkipWhile(string.IsNullOrWhiteSpace).ToList();
        while (list.Count > 0 && string.IsNullOrWhiteSpace(list[^1]))
        {
            list.RemoveAt(list.Count - 1);
        }

        return list;
    }

    private sealed record KeySpan( string Name, int Start, int End );

    private sealed class TableSpan( List<string> path, bool isArray, int start, int lastLine )
    {
        public List<string> Path { get; } = path;
        public bool IsArray { get; } = isArray;
        public int Start { get; } = start;
        public int LastLine { get; private set; } = lastLine;
        public List<KeySpan> Keys { get; } = [];

        public static List<TableSpan> Read( DocumentSyntax document )
        {
            var root = new TableSpan([], false, 0, -1);
            var tables = new List<TableSpan> { root };
            var previous = -1;
            foreach (var keyValue in document.KeyValues)
            {
                previous = root.Add(keyValue, previous);
            }
            foreach (var syntax in document.Tables)
            {
                var header = (syntax.OpenBracket?.Span ?? syntax.Span).Start.Line;
                var table = new TableSpan(ReadPath(syntax.Name), syntax is TableArraySyntax, previous + 1, header);
                previous = header;
                foreach (var keyValue in syntax.Items)
                {
                    previous = table.Add(keyValue, previous);
                }
                tables.Add(table);
            }

            return tables;
        }

        private int Add( KeyValueSyntax keyValue, int previous )
        {
            LastLine = keyValue.Span.End.Line;
            Keys.Add(new KeySpan(ReadName(keyValue.Key!.Key), previous + 1, LastLine));

            return LastLine;
        }

        private static List<string> ReadPath( KeySyntax? key )
        {
            return key is null ? [] : [ReadName(key.Key), .. key.DotKeys.Select(dotKey => ReadName(dotKey.Key))];
        }

        private static string ReadName( BareKeyOrStringValueSyntax? key )
        {
            return key switch {
                BareKeySyntax bare => bare.Key?.Text ?? "",
                StringValueSyntax quoted => quoted.Value ?? "",
                _ => ""
            };
        }
    }
}
