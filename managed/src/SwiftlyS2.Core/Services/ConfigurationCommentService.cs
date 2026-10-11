using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Microsoft.Extensions.Configuration;

namespace SwiftlyS2.Core.Services;

internal static class ConfigurationCommentService
{
    public static bool HasDescriptions( Type type )
    {
        return HasDescriptions(type, new HashSet<Type>());
    }

    public static string WriteJson( string json, string sectionName, Type modelType, JsonSerializerOptions options )
    {
        var root = JsonNode.Parse(json)!.AsObject();
        var builder = new StringBuilder();
        var writer = new JsonCommentWriter(builder, options);
        writer.WriteObject(root, 0, key => key == sectionName ? (modelType, null) : (null, null));

        return builder.ToString();
    }

    public static string WriteJsonMember( string key, JsonNode? value, Type? type, string? description, JsonSerializerOptions options )
    {
        var builder = new StringBuilder();
        new JsonCommentWriter(builder, options).WriteMember(key, value, 0, type, description);

        return builder.ToString();
    }

    public static string WriteToml( string toml, string sectionName, Type modelType )
    {
        var newLine = toml.Contains("\r\n") ? "\r\n" : "\n";
        var output = new List<string>();
        var commentedHeaders = new HashSet<string>();
        Type? tableType = null;

        foreach (var line in toml.Replace("\r\n", "\n").Split('\n'))
        {
            var trimmed = line.Trim();
            if (trimmed.StartsWith('['))
            {
                var path = trimmed.Trim('[', ']').Trim();
                var (type, description) = ResolveTomlTable(SplitTomlKey(path), sectionName, modelType);
                tableType = type;
                if (description is not null && commentedHeaders.Add(path))
                {
                    output.AddRange(TomlComment(description, line[..^line.TrimStart().Length]));
                }
            }
            else if (tableType is not null
                && ReadTomlKey(trimmed) is { } key
                && FindMember(tableType, key, useJsonNames: false) is { } member
                && GetDescription(member.Member) is { } description)
            {
                output.AddRange(TomlComment(description, line[..^line.TrimStart().Length]));
            }
            output.Add(line);
        }

        return string.Join(newLine, output);
    }

    private static IEnumerable<string> TomlComment( string description, string indent )
    {
        return SplitLines(description).Select(line => indent + (line.Length == 0 ? "#" : "# " + line));
    }

    private static (Type? Type, string? Description) ResolveTomlTable( List<string> segments, string sectionName, Type modelType )
    {
        if (segments.Count == 0 || segments[0] != sectionName)
        {
            return (null, null);
        }

        Type? type = modelType;
        string? description = null;
        foreach (var segment in segments.Skip(1))
        {
            if (type is null)
            {
                return (null, null);
            }
            if (GetDictionaryValueType(type) is { } valueType)
            {
                type = valueType;
                description = null;
                continue;
            }
            if (FindMember(type, segment, useJsonNames: false) is not { } member)
            {
                return (null, null);
            }

            description = GetDescription(member.Member);
            type = member.Type != typeof(string) && GetDictionaryValueType(member.Type) is null && GetElementType(member.Type) is { } elementType
                ? elementType
                : member.Type;
        }

        return (type, description);
    }

    private static List<string> SplitTomlKey( string path )
    {
        var segments = new List<string>();
        var current = new StringBuilder();
        var quote = '\0';
        for (var i = 0; i < path.Length; i++)
        {
            var c = path[i];
            if (quote != '\0')
            {
                if (c == '\\' && quote == '"' && i + 1 < path.Length)
            {
                    current.Append(path[++i]);
                }
                else if (c == quote)
                {
                    quote = '\0';
                }
                else
                {
                    current.Append(c);
                }
            }
            else if (c is '"' or '\'')
            {
                quote = c;
            }
            else if (c == '.')
            {
                segments.Add(current.ToString().Trim());
                current.Clear();
            }
            else
            {
                current.Append(c);
            }
        }
        segments.Add(current.ToString().Trim());

        return segments;
    }

    private static string? ReadTomlKey( string line )
    {
        if (line.Length == 0 || line[0] == '#')
        {
            return null;
        }

        var equals = -1;
        var quote = '\0';
        for (var i = 0; i < line.Length; i++)
        {
            var c = line[i];
            if (quote != '\0')
            {
                if (c == '\\' && quote == '"')
                {
                    i++;
                }
                else if (c == quote)
                {
                    quote = '\0';
                }
            }
            else if (c is '"' or '\'')
            {
                quote = c;
            }
            else if (c == '=')
            {
                equals = i;
                break;
            }
        }
        if (equals <= 0)
        {
            return null;
        }

        var segments = SplitTomlKey(line[..equals].Trim());

        return segments.Count == 1 ? segments[0] : null;
    }

    private sealed class JsonCommentWriter( StringBuilder builder, JsonSerializerOptions options )
    {
        private readonly string _newLine = options.NewLine;
        private readonly string _indent = new(options.IndentCharacter, options.IndentSize);

        public void WriteObject( JsonObject node, int depth, Func<string, (Type? Type, string? Description)> resolve )
        {
            if (node.Count == 0)
            {
                builder.Append("{}");
                return;
            }

            builder.Append('{').Append(_newLine);
            var index = 0;
            foreach (var (key, value) in node)
            {
                var (type, description) = resolve(key);
                WriteMember(key, value, depth + 1, type, description);
                if (++index < node.Count)
                {
                    builder.Append(',');
                }

                builder.Append(_newLine);
            }

            Indent(depth);
            builder.Append('}');
        }

        public void WriteMember( string key, JsonNode? value, int depth, Type? type, string? description )
        {
            if (description is not null)
            {
                foreach (var line in SplitLines(description))
                {
                    Indent(depth);
                    builder.Append(line.Length == 0 ? "//" : "// " + line).Append(_newLine);
                }
            }

            Indent(depth);
            builder.Append(JsonSerializer.Serialize(key, options)).Append(": ");
            WriteValue(value, depth, type);
        }

        private void WriteArray( JsonArray node, int depth, Type? elementType )
        {
            if (node.Count == 0)
            {
                builder.Append("[]");
                return;
            }

            builder.Append('[').Append(_newLine);
            for (var i = 0; i < node.Count; i++)
            {
                Indent(depth + 1);
                WriteValue(node[i], depth + 1, elementType);
                if (i < node.Count - 1)
                {
                    builder.Append(',');
                }
                builder.Append(_newLine);
            }
            Indent(depth);
            builder.Append(']');
        }

        private void WriteValue( JsonNode? value, int depth, Type? type )
        {
            switch (value)
            {
                case JsonObject obj when type is not null && GetDictionaryValueType(type) is { } valueType:
                    WriteObject(obj, depth, _ => (valueType, null));
                    break;
                case JsonObject obj:
                    WriteObject(obj, depth, key => ResolveMember(type, key));
                    break;
                case JsonArray array:
                    WriteArray(array, depth, type is null ? null : GetElementType(type));
                    break;
                case null:
                    builder.Append("null");
                    break;
                default:
                    builder.Append(value.ToJsonString(options));
                    break;
            }
        }

        private void Indent( int depth )
        {
            for (var i = 0; i < depth; i++)
            {
                builder.Append(_indent);
            }
        }
    }

    public static (Type? Type, string? Description) ResolveMember( Type? type, string key )
    {
        if (type is null || FindMember(type, key, useJsonNames: true) is not { } member)
        {
            return (null, null);
        }

        return (member.Type, GetDescription(member.Member));
    }

    private static (MemberInfo Member, Type Type)? FindMember( Type type, string key, bool useJsonNames )
    {
        foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            if (property.GetIndexParameters().Length == 0 && Matches(property, key, useJsonNames))
            {
                return (property, property.PropertyType);
            }
        }
        foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.Instance))
        {
            if (Matches(field, key, useJsonNames))
            {
                return (field, field.FieldType);
            }
        }

        return null;
    }

    public static string? GetConfigurationKeyName( MemberInfo member )
    {
        return member.GetCustomAttribute<ConfigurationKeyNameAttribute>()?.Name;
    }

    public static void ApplyConfigurationKeyNames( JsonTypeInfo typeInfo )
    {
        if (typeInfo.Kind != JsonTypeInfoKind.Object)
        {
            return;
        }

        foreach (var property in typeInfo.Properties)
        {
            if (property.AttributeProvider is MemberInfo member && GetConfigurationKeyName(member) is { } name)
            {
                property.Name = name;
            }
        }
    }

    private static bool Matches( MemberInfo member, string key, bool useJsonNames )
    {
        var jsonName = GetConfigurationKeyName(member) ?? member.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name;
        if (useJsonNames)
        {
            return (jsonName ?? member.Name) == key;
        }

        return member.Name == key || jsonName == key;
    }

    private static string? GetDescription( MemberInfo member )
    {
        var description = member.GetCustomAttribute<DescriptionAttribute>()?.Description;

        return string.IsNullOrWhiteSpace(description) ? null : description;
    }

    private static IEnumerable<string> SplitLines( string text )
    {
        return text.Replace("\r\n", "\n").Split('\n').Select(line => line.TrimEnd());
    }

    public static Type? GetDictionaryValueType( Type type )
    {
        var dictionary = type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IDictionary<,>)
            ? type
            : type.GetInterfaces().FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDictionary<,>));

        return dictionary?.GetGenericArguments()[1];
    }

    private static Type? GetElementType( Type type )
    {
        if (type.IsArray)
        {
            return type.GetElementType();
        }
        var enumerable = type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>)
            ? type
            : type.GetInterfaces().FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>));

        return enumerable?.GetGenericArguments()[0];
    }

    private static bool HasDescriptions( Type type, HashSet<Type> visited )
    {
        if (type == typeof(string) || type.IsPrimitive || type.IsEnum || !visited.Add(type))
        {
            return false;
        }
        if (GetDictionaryValueType(type) is { } valueType)
        {
            return HasDescriptions(valueType, visited);
        }
        if (typeof(IEnumerable).IsAssignableFrom(type))
        {
            return GetElementType(type) is { } elementType && HasDescriptions(elementType, visited);
        }

        foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            if (property.GetIndexParameters().Length == 0 && (GetDescription(property) is not null || HasDescriptions(property.PropertyType, visited)))
            {
                return true;
            }
        }
        foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.Instance))
        {
            if (GetDescription(field) is not null || HasDescriptions(field.FieldType, visited))
            {
                return true;
            }
        }
        
        return false;
    }
}
