using SwiftlyS2.Core.Convars;

namespace SwiftlyS2.Shared.Convars;

public interface IConVarService
{

	/// <summary>
	/// Find a existing convar by name.
	/// </summary>
	/// <typeparam name="T">The type of the convar.</typeparam>
	/// <param name="name">The name of the convar.</param>
	/// <returns>The convar if found, null otherwise.</returns>
	public IConVar<T>? Find<T>( string name );

	/// <summary>
	/// Create a new convar.
	/// </summary>
	/// <typeparam name="T">The type of the convar.</typeparam>
	/// <param name="name">The name of the convar.</param>
	/// <param name="helpMessage">The help message of the convar.</param>
	/// <param name="defaultValue">The default value of the convar.</param>
	/// <param name="flags">The flags of the convar.</param>
	/// <returns>The created convar.</returns>
	/// <returns>Reference to the created convar.</returns>
	public IConVar<T> Create<T>( string name, string helpMessage, T defaultValue, ConvarFlags flags = ConvarFlags.NONE );

	/// <summary>
	/// Create a new convar with min and max values.
	/// </summary>
	/// <typeparam name="T">The type of the convar.</typeparam>
	/// <param name="name">The name of the convar.</param>
	/// <param name="helpMessage">The help message of the convar.</param>
	/// <param name="defaultValue">The default value of the convar.</param>
	/// <param name="flags">The flags of the convar.</param>
	/// <param name="minValue">The min value of the convar.</param>
	/// <param name="maxValue">The max value of the convar.</param>
	/// <returns>The created convar.</returns>
	public IConVar<T> Create<T>( string name, string helpMessage, T defaultValue, T? minValue, T? maxValue, ConvarFlags flags = ConvarFlags.NONE ) where T : unmanaged;

	/// <summary>
	/// Create a new convar or find an existing one by name.
	/// </summary>
	/// <typeparam name="T">The type of the convar.</typeparam>
	/// <param name="name">The name of the convar.</param>
	/// <param name="helpMessage">The help message of the convar.</param>
	/// <param name="defaultValue">The default value of the convar.</param>
	/// <param name="flags">The flags of the convar.</param>
	/// <returns>The created or found convar.</returns>
	public IConVar<T> CreateOrFind<T>( string name, string helpMessage, T defaultValue, ConvarFlags flags = ConvarFlags.NONE );

	/// <summary>
	/// Create a new convar or find an existing one by name with min and max values.
	/// </summary>
	/// <typeparam name="T">The type of the convar.</typeparam>
	/// <param name="name">The name of the convar.</param>
	/// <param name="helpMessage">The help message of the convar.</param>
	/// <param name="defaultValue">The default value of the convar.</param>
	/// <param name="minValue">The min value of the convar.</param>
	/// <param name="maxValue">The max value of the convar.</param>
	/// <param name="flags">The flags of the convar.</param>
	/// <returns>The created or found convar.</returns>
	public IConVar<T> CreateOrFind<T>( string name, string helpMessage, T defaultValue, T? minValue, T? maxValue, ConvarFlags flags = ConvarFlags.NONE ) where T : unmanaged;

	/// <summary>
	/// Reads the value of a convar as a string.
	/// </summary>
	/// <param name="name">The name of the convar.</param>
	/// <returns>The value of the convar as a string.</returns>
	public string? ReadValueAsString( string name );

	/// <summary>
	/// Sets the value of a convar as a string.
	/// </summary>
	/// <param name="name">The name of the convar.</param>
	/// <param name="value">The value to set.</param>
	public void SetValueAsString( string name, string value );

	/// <summary>
	/// Gets the type of a convar by name.
	/// </summary>
	/// <param name="name">The name of the convar.</param>
	/// <returns>The type of the convar.</returns>
	public EConVarType GetConVarType( string name );
}