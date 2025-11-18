using System.Collections.Concurrent;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace SwiftlyS2.Shared.Schemas;

public static class SchemaSize
{
    private static readonly ConcurrentDictionary<Type, int> _sizeCache = new();

    public static int GetSize<T>() where T : ISchemaClass<T>
    {
        return T.Size;
    }
    public static int Get<T>()
    {
        return _sizeCache.GetOrAdd(typeof(T), static type =>
        {
            var interfaces = type.GetInterfaces();
            foreach (var iface in interfaces)
            {
                if (iface.IsGenericType &&
                    iface.GetGenericTypeDefinition() == typeof(ISchemaClass<>) &&
                    iface.GetGenericArguments()[0] == type)
                {
                    var method = typeof(SchemaSize).GetMethod(nameof(GetSize))?.MakeGenericMethod(type);
                    if (method != null)
                    {
                        return (int)method.Invoke(null, null)!;
                    }
                }
            }

            return Unsafe.SizeOf<T>();
        });
    }}
