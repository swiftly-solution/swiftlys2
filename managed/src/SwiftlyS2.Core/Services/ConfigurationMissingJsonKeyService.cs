using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace SwiftlyS2.Core.Services;

internal static class ConfigurationMissingJsonKeyService
{
    private static readonly JsonDocumentOptions DocumentOptions = new() {
        CommentHandling = JsonCommentHandling.Skip,
        AllowTrailingCommas = true
    };

    public static string? AddMissingKeys( string text, JsonObject defaults, string sectionName, Type modelType, JsonSerializerOptions options, bool writeComments )
    {
        ObjectSpan? root;
        try
        {
            using (var document = JsonDocument.Parse(text, DocumentOptions))
            {
                if (document.RootElement.ValueKind != JsonValueKind.Object)
                {
                    return null;
                }
            }
            root = new JsoncScanner(text).ReadRoot();
        }
        catch (JsonException)
        {
            return null;
        }
        if (root is null)
        {
            return null;
        }

        var editor = new JsonKeyEditor(text, options, writeComments);
        editor.Collect(root, defaults, key => key == sectionName ? (modelType, null) : (null, null));

        return editor.Apply();
    }

    private sealed record MissingMember( string Key, JsonNode? Value, Type? Type, string? Description );

    private sealed record MemberSpan( string Key, int KeyStart, int ValueEnd, bool HasComma, ObjectSpan? Child );

    private sealed class ObjectSpan
    {
        public int Close { get; set; }
        public List<MemberSpan> Members { get; } = [];
    }

    private sealed class JsonKeyEditor( string text, JsonSerializerOptions options, bool writeComments )
    {
        private readonly string _newLine = text.Contains("\r\n") ? "\r\n" : "\n";
        private readonly string _indent = new(options.IndentCharacter, options.IndentSize);
        private readonly List<(int Position, string Text)> _edits = [];

        public void Collect( ObjectSpan span, JsonObject defaults, Func<string, (Type? Type, string? Description)> resolve )
        {
            var missing = new List<MissingMember>();
            foreach (var (key, value) in defaults)
            {
                var (type, description) = resolve(key);
                var existing = span.Members.FirstOrDefault(member => string.Equals(member.Key, key, StringComparison.OrdinalIgnoreCase));
                if (existing is null)
                {
                    missing.Add(new MissingMember(key, value, type, description));
                }
                else if (existing.Child is not null
                    && value is JsonObject childDefaults
                    && type is not null
                    && ConfigurationCommentService.GetDictionaryValueType(type) is null)
                {
                    Collect(existing.Child, childDefaults, childKey => ConfigurationCommentService.ResolveMember(type, childKey));
                }
            }

            if (missing.Count > 0)
            {
                Insert(span, missing);
            }
        }

        public string? Apply()
        {
            if (_edits.Count == 0)
            {
                return null;
            }

            var builder = new StringBuilder(text);
            foreach (var (position, insert) in _edits.OrderByDescending(edit => edit.Position))
            {
                _ = builder.Insert(position, insert);
            }

            return builder.ToString();
        }

        private void Insert( ObjectSpan span, List<MissingMember> missing )
        {
            var closeLineStart = LineStart(span.Close);
            var closeIndent = LeadingWhitespace(closeLineStart);
            var memberIndent = closeIndent + _indent;
            if (span.Members.Count > 0)
            {
                var keyStart = span.Members[0].KeyStart;
                var keyLineStart = LineStart(keyStart);
                if (IsBlank(keyLineStart, keyStart))
                {
                    memberIndent = text[keyLineStart..keyStart];
                }
            }

            var lines = new List<string>();
            for (var i = 0; i < missing.Count; i++)
            {
                var member = missing[i];
                var entry = ConfigurationCommentService.WriteJsonMember(
                    member.Key,
                    member.Value,
                    writeComments ? member.Type : null,
                    writeComments ? member.Description : null,
                    options);
                if (i < missing.Count - 1)
                {
                    entry += ",";
                }
                lines.AddRange(entry.Replace("\r\n", "\n").Split('\n'));
            }
            var block = string.Join(_newLine, lines.Select(line => memberIndent + line));

            if (span.Members.Count > 0 && !span.Members[^1].HasComma)
            {
                _edits.Add((span.Members[^1].ValueEnd, ","));
            }
            if (IsBlank(closeLineStart, span.Close))
            {
                _edits.Add((closeLineStart, block + _newLine));
            }
            else
            {
                _edits.Add((span.Close, _newLine + block + _newLine + closeIndent));
            }
        }

        private int LineStart( int position )
        {
            return position == 0 ? 0 : text.LastIndexOf('\n', position - 1) + 1;
        }

        private bool IsBlank( int start, int end )
        {
            for (var i = start; i < end; i++)
            {
                if (!char.IsWhiteSpace(text[i]))
                {
                    return false;
                }
            }

            return true;
        }

        private string LeadingWhitespace( int lineStart )
        {
            var end = lineStart;
            while (end < text.Length && text[end] is ' ' or '\t')
            {
                end++;
            }

            return text[lineStart..end];
        }
    }

    private sealed class JsoncScanner( string text )
    {
        private int _position;

        private char Current => _position < text.Length ? text[_position] : throw new JsonException("Unexpected end of JSON.");

        public ObjectSpan? ReadRoot()
        {
            SkipTrivia();

            return Current == '{' ? ReadObject() : null;
        }

        private ObjectSpan ReadObject()
        {
            var span = new ObjectSpan();
            _position++;
            while (true)
            {
                SkipTrivia();
                if (Current == '}')
                {
                    span.Close = _position++;
                    return span;
                }

                var keyStart = _position;
                var key = ReadString();
                SkipTrivia();
                _position++;
                SkipTrivia();
                var child = Current == '{' ? ReadObject() : null;
                if (child is null)
                {
                    SkipValue();
                }
                var valueEnd = _position;
                SkipTrivia();
                var hasComma = Current == ',';
                if (hasComma)
                {
                    _position++;
                }
                span.Members.Add(new MemberSpan(key, keyStart, valueEnd, hasComma, child));
            }
        }

        private void SkipValue()
        {
            switch (Current)
            {
                case '{':
                    _ = ReadObject();
                    break;
                case '[':
                    SkipArray();
                    break;
                case '"':
                    _ = ReadString();
                    break;
                default:
                    while (_position < text.Length && text[_position] is not (',' or '}' or ']' or '/') && !char.IsWhiteSpace(text[_position]))
                    {
                        _position++;
                    }
                    break;
            }
        }

        private void SkipArray()
        {
            _position++;
            while (true)
            {
                SkipTrivia();
                if (Current == ']')
                {
                    _position++;
                    return;
                }
                SkipValue();
                SkipTrivia();
                if (Current == ',')
                {
                    _position++;
                }
            }
        }

        private string ReadString()
        {
            var start = _position;
            _position++;
            while (Current != '"')
            {
                _position += Current == '\\' ? 2 : 1;
            }
            _position++;

            return JsonSerializer.Deserialize<string>(text.AsSpan(start, _position - start))!;
        }

        private void SkipTrivia()
        {
            while (_position < text.Length)
            {
                if (char.IsWhiteSpace(text[_position]))
                {
                    _position++;
                }
                else if (text[_position] == '/' && _position + 1 < text.Length && text[_position + 1] == '/')
                {
                    var end = text.IndexOf('\n', _position);
                    _position = end < 0 ? text.Length : end + 1;
                }
                else if (text[_position] == '/' && _position + 1 < text.Length && text[_position + 1] == '*')
                {
                    var end = text.IndexOf("*/", _position + 2, StringComparison.Ordinal);
                    _position = end < 0 ? text.Length : end + 2;
                }
                else
                {
                    return;
                }
            }
        }
    }
}
