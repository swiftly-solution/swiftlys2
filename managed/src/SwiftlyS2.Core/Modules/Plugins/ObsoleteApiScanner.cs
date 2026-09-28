using System.Reflection;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;

namespace SwiftlyS2.Core.Plugins;

internal static class ObsoleteApiScanner
{
    private const string CoreAssemblyName = "SwiftlyS2.CS2";
    private const BindingFlags MemberLookupFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;

    internal readonly record struct ObsoleteApiUsage( string Member, string? Reason );

    public static List<ObsoleteApiUsage> Scan( string assemblyPath )
    {
        var results = new List<ObsoleteApiUsage>();
        var seen = new HashSet<string>(StringComparer.Ordinal);

        using var stream = File.Open(assemblyPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using var peReader = new PEReader(stream);
        if (!peReader.HasMetadata)
            return results;

        var reader = peReader.GetMetadataReader();

        foreach (var handle in reader.TypeReferences)
        {
            try
            {
                var (assemblyName, typeName) = ResolveTypeReference(reader, handle);
                if (assemblyName == null || typeName.Length == 0 || !assemblyName.Equals(CoreAssemblyName, StringComparison.Ordinal))
                {
                    continue;
                }

                var type = Assembly.GetExecutingAssembly().GetType(typeName, throwOnError: false);
                var reason = GetObsoleteReason(type);
                if (reason == null)
                {
                    continue;
                }

                if (seen.Add(typeName))
                {
                    results.Add(new ObsoleteApiUsage(typeName, reason));
                }
            }
            catch
            { }
        }

        foreach (var handle in reader.MemberReferences)
        {
            try
            {
                var memberRef = reader.GetMemberReference(handle);
                var (assemblyName, typeName) = ResolveParentType(reader, memberRef.Parent);

                if (assemblyName == null || typeName.Length == 0 || !assemblyName.Equals(CoreAssemblyName, StringComparison.Ordinal))
                    continue;

                var type = Assembly.GetExecutingAssembly().GetType(typeName, throwOnError: false);
                if (type == null)
                    continue;

                var memberName = reader.GetString(memberRef.Name);
                var reason = FindObsoleteReason(type, memberName);
                if (reason == null)
                    continue;

                var displayMember = $"{typeName}.{StripAccessorPrefix(memberName)}";
                if (seen.Add(displayMember))
                    results.Add(new ObsoleteApiUsage(displayMember, reason));
            }
            catch
            { }
        }

        return results;
    }

    private static string? FindObsoleteReason( Type type, string memberName )
    {
        if (memberName.StartsWith("get_", StringComparison.Ordinal) || memberName.StartsWith("set_", StringComparison.Ordinal))
        {
            var reason = GetObsoleteReason(type.GetProperty(memberName[4..], MemberLookupFlags));
            if (reason != null) return reason;
        }
        else if (memberName.StartsWith("add_", StringComparison.Ordinal))
        {
            var reason = GetObsoleteReason(type.GetEvent(memberName[4..], MemberLookupFlags));
            if (reason != null) return reason;
        }
        else if (memberName.StartsWith("remove_", StringComparison.Ordinal))
        {
            var reason = GetObsoleteReason(type.GetEvent(memberName[7..], MemberLookupFlags));
            if (reason != null) return reason;
        }

        var fieldReason = GetObsoleteReason(type.GetField(memberName, MemberLookupFlags));
        if (fieldReason != null)
        {
            return fieldReason;
        }

        foreach (var method in type.GetMethods(MemberLookupFlags))
        {
            if (!method.Name.Equals(memberName, StringComparison.Ordinal))
                continue;

            var reason = GetObsoleteReason(method);
            if (reason != null)
                return reason;
        }

        return null;
    }

    private static string? GetObsoleteReason( MemberInfo? member )
    {
        return member == null || member.IsDefined(typeof(CompilerFeatureRequiredAttribute), inherit: false)
            ? null
            : member.GetCustomAttribute<ObsoleteAttribute>()?.Message;
    }

    private static string StripAccessorPrefix( string memberName )
    {
        return memberName switch {
            _ when memberName.StartsWith("get_", StringComparison.Ordinal) => memberName[4..],
            _ when memberName.StartsWith("set_", StringComparison.Ordinal) => memberName[4..],
            _ when memberName.StartsWith("add_", StringComparison.Ordinal) => memberName[4..],
            _ when memberName.StartsWith("remove_", StringComparison.Ordinal) => memberName[7..],
            _ => memberName
        };
    }

    private static (string? AssemblyName, string TypeName) ResolveParentType( MetadataReader reader, EntityHandle parent )
    {
        return parent.Kind != HandleKind.TypeReference ? (null, string.Empty) : ResolveTypeReference(reader, (TypeReferenceHandle)parent);
    }

    private static (string? AssemblyName, string TypeName) ResolveTypeReference( MetadataReader reader, TypeReferenceHandle handle )
    {
        var typeRef = reader.GetTypeReference(handle);
        var name = reader.GetString(typeRef.Name);
        var scope = typeRef.ResolutionScope;

        if (scope.Kind == HandleKind.TypeReference)
        {
            var (assemblyName, parentName) = ResolveTypeReference(reader, (TypeReferenceHandle)scope);
            return (assemblyName, parentName.Length == 0 ? name : $"{parentName}+{name}");
        }

        var ns = reader.GetString(typeRef.Namespace);
        var fullName = string.IsNullOrEmpty(ns) ? name : $"{ns}.{name}";

        if (scope.Kind == HandleKind.AssemblyReference)
        {
            var asmRef = reader.GetAssemblyReference((AssemblyReferenceHandle)scope);
            return (reader.GetString(asmRef.Name), fullName);
        }

        return (null, fullName);
    }
}
