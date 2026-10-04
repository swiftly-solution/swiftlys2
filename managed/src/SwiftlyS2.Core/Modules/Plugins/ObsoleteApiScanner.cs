using System.Collections.Immutable;
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
                var reason = FindObsoleteReason(type, memberName, memberRef);
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

    private static string? FindObsoleteReason( Type type, string memberName, MemberReference memberRef )
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

        var candidates = type.GetMethods(MemberLookupFlags)
            .Where(m => m.Name.Equals(memberName, StringComparison.Ordinal))
            .ToArray();

        if (candidates.Length == 0)
            return null;

        if (candidates.Length == 1)
            return GetObsoleteReason(candidates[0]);

        var reasons = candidates.Select(GetObsoleteReason).ToArray();
        if (reasons.All(r => r == null))
            return null;
        if (reasons.All(r => r != null))
            return reasons[0];

        var matched = ResolveOverloadBySignature(memberRef, candidates);
        return matched != null ? GetObsoleteReason(matched) : null;
    }

    private static MethodInfo? ResolveOverloadBySignature( MemberReference memberRef, MethodInfo[] candidates )
    {
        try
        {
            var provider = new SimpleTypeNameProvider();
            var signature = memberRef.DecodeMethodSignature(provider, null);

            foreach (var candidate in candidates)
            {
                var genericArity = candidate.IsGenericMethodDefinition ? candidate.GetGenericArguments().Length : 0;
                if (genericArity != signature.GenericParameterCount)
                    continue;

                var parameters = candidate.GetParameters();
                if (parameters.Length != signature.ParameterTypes.Length)
                    continue;

                var isMatch = true;
                for (var i = 0; i < parameters.Length; i++)
                {
                    if (!parameters[i].ParameterType.Name.Equals(signature.ParameterTypes[i], StringComparison.Ordinal))
                    {
                        isMatch = false;
                        break;
                    }
                }

                if (isMatch)
                    return candidate;
            }
        }
        catch
        {
        }

        return null;
    }

    private sealed class SimpleTypeNameProvider : ISignatureTypeProvider<string, object?>
    {
        public string GetPrimitiveType( PrimitiveTypeCode typeCode ) => typeCode.ToString();

        public string GetTypeFromDefinition( MetadataReader reader, TypeDefinitionHandle handle, byte rawTypeKind )
            => reader.GetString(reader.GetTypeDefinition(handle).Name);

        public string GetTypeFromReference( MetadataReader reader, TypeReferenceHandle handle, byte rawTypeKind )
            => reader.GetString(reader.GetTypeReference(handle).Name);

        public string GetTypeFromSpecification( MetadataReader reader, object? genericContext, TypeSpecificationHandle handle, byte rawTypeKind )
            => "?";

        public string GetSZArrayType( string elementType ) => elementType + "[]";
        public string GetArrayType( string elementType, ArrayShape shape ) => elementType + "[]";
        public string GetByReferenceType( string elementType ) => elementType;
        public string GetPointerType( string elementType ) => elementType + "*";
        public string GetPinnedType( string elementType ) => elementType;
        public string GetFunctionPointerType( MethodSignature<string> signature ) => "*()";
        public string GetGenericMethodParameter( object? genericContext, int index ) => "!!" + index;
        public string GetGenericTypeParameter( object? genericContext, int index ) => "!" + index;
        public string GetModifiedType( string modifier, string unmodifiedType, bool isRequired ) => unmodifiedType;
        public string GetGenericInstantiation( string genericType, ImmutableArray<string> typeArguments ) => genericType;
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
