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
    /// Find a existing convar by name with no type.
    /// </summary>
    /// <param name="name">The name of the convar.</param>
    /// <returns>The convar if found, null otherwise.</returns>
    public IConVar? FindAsString( string name );

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
    /// Replicate the value of the convar to a specific client.
    /// You can use this method to replicate those convars that don't exist on server.
    /// 
    /// </summary>
    /// <param name="clientId">The client id to replicate to.</param>
    /// <param name="name">The name of the convar.</param>
    /// <param name="value">The value to replicate.</param>
    public void ReplicateToClient(int clientId, string name, string value);

    /// <summary>
    /// Replicate the value of the convar to all clients.
    /// You can use this method to replicate those convars that don't exist on server.
    /// 
    /// </summary>
    /// <param name="name">The name of the convar.</param>
    /// <param name="value">The value to replicate.</param>
    public void ReplicateToAll(string name, string value);

    /// <summary>
    /// Enumerate all registered convars.
    /// </summary>
    public IReadOnlyList<ConVarInfo> EnumerateAllConVars();

    /// <summary>
    /// Enumerate all registered concommands.
    /// </summary>
    public IReadOnlyList<ConCommandInfo> EnumerateAllCommands();

    /// <summary>
    /// Remove the specified flags from all convars. Returns count of convars unlocked.
    /// Default: HIDDEN | DEVELOPMENT_ONLY | CLIENTDLL | DEFENSIVE.
    /// </summary>
    public int UnlockAllConVars(ConvarFlags flagsToRemove = ConvarFlags.HIDDEN | ConvarFlags.DEVELOPMENT_ONLY | ConvarFlags.CLIENTDLL | ConvarFlags.DEFENSIVE);

    /// <summary>
    /// Remove the specified flags from all concommands. Returns count of commands unlocked.
    /// Default: HIDDEN | DEVELOPMENT_ONLY | CLIENTDLL | DEFENSIVE.
    /// </summary>
    public int UnlockAllCommands(ConvarFlags flagsToRemove = ConvarFlags.HIDDEN | ConvarFlags.DEVELOPMENT_ONLY | ConvarFlags.CLIENTDLL | ConvarFlags.DEFENSIVE);
}

public record ConVarInfo
{
    public required string Name { get; init; }
    public required string Type { get; init; }
    public required string Default { get; init; }
    public required ulong Flags { get; init; }
    public required string Description { get; init; }
}

public record ConCommandInfo
{
    public required string Name { get; init; }
    public required ulong Flags { get; init; }
    public required string Description { get; init; }
}