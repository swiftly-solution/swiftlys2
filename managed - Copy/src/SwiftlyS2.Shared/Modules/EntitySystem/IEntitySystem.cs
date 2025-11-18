using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.Schemas;

namespace SwiftlyS2.Shared.EntitySystem;

public interface IEntitySystemService {


  /// <summary>
  /// Create an entity by class.
  /// </summary>
  /// <typeparam name="T">Entity type.</typeparam>
  /// <returns>Created entity.</returns>
  /// <exception cref="ArgumentException">Thrown when failed to create entity by class or class doesn't have a designer name.</exception>
  public T CreateEntity<T>() where T : class, ISchemaClass<T>;

  /// <summary>
  /// Create an entity by designer name.
  /// </summary>
  /// <typeparam name="T">Entity type.</typeparam>
  /// <param name="designerName">Designer name.</param>
  /// <returns>Created entity.</returns>
  /// <exception cref="ArgumentException">Thrown when failed to create entity by designer name or designer name is invalid.</exception>
  public T CreateEntityByDesignerName<T>(string designerName) where T : ISchemaClass<T>;

  /// <summary>
  /// Get a reference handle to the entity.
  /// </summary>
  /// <typeparam name="T">Entity type.</typeparam>
  /// <param name="entity">Entity instance.</param>
  /// <returns>Reference entity handle to the entity.</returns>
  public CHandle<T> GetRefEHandle<T>(T entity) where T : class, ISchemaClass<T>;

  /// <summary>
  /// Get the game rules entity.
  /// </summary>
  /// <returns>Game rules entity. Nullable.</returns>
  public CCSGameRules? GetGameRules();

  /// <summary>
  /// Get all entities.
  /// </summary>
  /// <returns>All entities.</returns>
  public IEnumerable<CEntityInstance> GetAllEntities();

  /// <summary>
  /// Get all entities by class.
  /// </summary>
  /// <typeparam name="T">Entity type.</typeparam>
  /// <returns>All entities by class.</returns>
  public IEnumerable<T> GetAllEntitiesByClass<T>() where T : class, ISchemaClass<T>;

  /// <summary>
  /// Get all entities by designer name, and cast to type T.
  /// </summary>
  /// <typeparam name="T">Entity type.</typeparam>
  /// <param name="designerName">Designer name.</param>
  /// <returns>All entities by designer name.</returns>
  public IEnumerable<T> GetAllEntitiesByDesignerName<T>(string designerName) where T : class, ISchemaClass<T>;

  /// <summary>
  /// Get an entity by index.
  /// </summary>
  /// <typeparam name="T">Entity type.</typeparam>
  /// <param name="index">Entity index.</param>
  /// <returns>Entity by index. Nullable.</returns>
  public T? GetEntityByIndex<T>(uint index) where T : class, ISchemaClass<T>;
  
  /// <summary>
  /// Represents a method that handles an entity output event, allowing custom logic to be executed when an entity
  /// triggers an output.
  /// </summary>
  /// <param name="entityIO">The entity output object that contains information about the triggered output.</param>
  /// <param name="outputName">The name of the output that was triggered.</param>
  /// <param name="activator">The entity instance that activated the output.</param>
  /// <param name="caller">The entity instance that called the output, if applicable.</param>
  /// <param name="delay">The delay, in seconds, before the output is executed.</param>
  /// <returns>A <see cref="HookResult"/> value indicating the result of the handler's execution,  such as whether the output
  /// should proceed or be blocked.</returns>
  delegate HookResult EntityOutputHandler(CEntityIOOutput entityIO, string outputName, CEntityInstance activator, CEntityInstance caller, float delay);

  /// <summary>
  /// Hooks an output of the specified entity type to a callback function.
  /// </summary>
  /// <remarks>This method allows you to attach a handler to a specific output of an entity. The callback will
  /// be invoked whenever the output is triggered.</remarks>
  /// <typeparam name="T">The type of the entity, which must implement <see cref="ISchemaClass{T}"/>.</typeparam>
  /// <param name="outputName">The name of the output to hook. This value cannot be <see langword="null"/> or empty.</param>
  /// <param name="callback">The callback function to invoke when the output is triggered. This value cannot be <see langword="null"/>.</param>
  /// <returns>A <see cref="Guid"/> that uniquely identifies the hook. This identifier can be used to manage or remove the hook.</returns>
  public Guid HookEntityOutput<T>(string outputName, EntityOutputHandler callback) where T : class, ISchemaClass<T>;

  /// <summary>
  /// Removes the association between the specified entity output and its handler.
  /// </summary>
  /// <param name="guid">The unique identifier of the entity output to unhook.</param>
  public void UnhookEntityOutput(Guid guid);
}