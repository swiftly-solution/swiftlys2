using SwiftlyS2.Shared.Schemas;

namespace SwiftlyS2.Shared.EntitySystem;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class EntityInputHandlerAttribute( string designerName, string inputName ) : Attribute
{
    public string DesignerName { get; set; } = designerName;
    public string InputName { get; set; } = inputName;
}

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class EntityInputHandlerAttribute<T>( string inputName ) : EntityInputHandlerAttribute(T.ClassName ?? throw new ArgumentException($"Can't hook entity input with class {typeof(T).Name}, which doesn't have a designer name."), inputName) where T : class, ISchemaClass<T>
{
}