using SwiftlyS2.Shared.Schemas;

namespace SwiftlyS2.Shared.EntitySystem;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class EntityOutputHandlerAttribute( string designerName, string outputName ) : Attribute
{
    public string DesignerName { get; set; } = designerName;
    public string OutputName { get; set; } = outputName;
}

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class EntityOutputHandlerAttribute<T>( string outputName ) : EntityOutputHandlerAttribute(T.ClassName ?? throw new ArgumentException($"Can't hook entity output with class {typeof(T).Name}, which doesn't have a designer name."), outputName) where T : class, ISchemaClass<T>
{
}