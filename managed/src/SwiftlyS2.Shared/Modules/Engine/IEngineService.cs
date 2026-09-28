using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.Services;

public interface IEngineService
{
    /// <summary>
    /// The IP address of the server.
    /// </summary>
    public string? ServerIP { get; }

    /// <summary>
    /// Gets the Workshop ID of the current map.
    /// </summary>
    public string WorkshopId { get; }

    /// <summary>
    /// Gets a reference to the global variables structure.
    /// </summary>
    public ref CGlobalVars GlobalVars { get; }

    /// <summary>
    /// Determines whether the specified map string represents a valid map in server files.
    /// </summary>
    /// <param name="map">The map string to validate. It also supports Workshop ID.</param>
    /// <returns>true if the map is valid; otherwise, false.</returns>
    public bool IsMapValid( string map );

    /// <summary>
    /// Executes the specified command string in the current context.
    /// </summary>
    /// <param name="command">The command to execute. Cannot be null or empty.</param>
    public void ExecuteCommand( string command );

    /// <summary>
    /// Executes the specified command string in the current context asynchronously.
    /// </summary>
    /// <param name="command">The command to execute. Cannot be null or empty.</param>
    public Task ExecuteCommandAsync( string command );

    /// <summary>
    /// Executes the specified command string in the current context.
    /// </summary>
    /// <param name="command">The command to execute. Cannot be null or empty.</param>
    /// <param name="bufferCallback">The callback to receive the output of the command.</param>
    [Obsolete("This method is planned to be removed.")]
    public void ExecuteCommandWithBuffer( string command, Action<string> bufferCallback );

    /// <summary>
    /// Executes the specified command string in the current context with a buffer callback asynchronously.
    /// </summary>
    /// <param name="command">The command to execute. Cannot be null or empty.</param>
    /// <param name="bufferCallback">The callback to receive the output of the command.</param>
    [Obsolete("This method is planned to be removed.")]
    public Task ExecuteCommandWithBufferAsync( string command, Action<string> bufferCallback );

    /// <summary>
    /// Find a game system by name.
    /// </summary>
    /// <param name="name">The name of the game system.</param>
    /// <returns>The game system handle. Null if not found.</returns>
    public nint? FindGameSystemByName( string name );

    /// <summary>
    /// Dispatches a particle effect to the specified recipients.
    ///
    /// Thread unsafe, use async variant instead for non-main thread context.
    /// </summary>
    /// <param name="particleName">The name of the particle effect.</param>
    /// <param name="attachmentType">The type of attachment for the particle effect.</param>
    /// <param name="attachmentPoint">The attachment point for the particle effect.</param>
    /// <param name="attachmentName">The name of the attachment for the particle effect.</param>
    /// <param name="filter">The recipient filter for the particle effect.</param>
    /// <param name="resetAllParticlesOnEntity">Whether to reset all particles on the entity.</param>
    /// <param name="splitScreenSlot">The split screen slot for the particle effect.</param>
    /// <param name="entity">The entity to attach the particle effect to.</param>
    public void DispatchParticleEffect( string particleName, ParticleAttachment_t attachmentType, byte attachmentPoint,
        CUtlSymbolLarge attachmentName, CRecipientFilter filter, bool resetAllParticlesOnEntity = false,
        int splitScreenSlot = 0, CBaseEntity? entity = null );

    /// <summary>
    /// Dispatches a particle effect to the specified recipients asynchronously.
    /// </summary>
    /// <param name="particleName">The name of the particle effect.</param>
    /// <param name="attachmentType">The type of attachment for the particle effect.</param>
    /// <param name="attachmentPoint">The attachment point for the particle effect.</param>
    /// <param name="attachmentName">The name of the attachment for the particle effect.</param>
    /// <param name="filter">The recipient filter for the particle effect.</param>
    /// <param name="resetAllParticlesOnEntity">Whether to reset all particles on the entity.</param>
    /// <param name="splitScreenSlot">The split screen slot for the particle effect.</param>
    /// <param name="entity">The entity to attach the particle effect to.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task DispatchParticleEffectAsync( string particleName, ParticleAttachment_t attachmentType,
        byte attachmentPoint, CUtlSymbolLarge attachmentName, CRecipientFilter filter,
        bool resetAllParticlesOnEntity = false, int splitScreenSlot = 0, CBaseEntity? entity = null );
}