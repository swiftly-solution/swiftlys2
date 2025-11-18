namespace SwiftlyS2.Shared.ConsoleOutput;

public interface IConsoleOutputService
{
    // /// <summary>
    // /// The handler for console output events.
    // /// </summary>
    // /// <param name="message">The console message.</param>
    // delegate void ConsoleOutputHandler(string message);

    // /// <summary>
    // /// Registers a console output listener.
    // /// </summary>
    // /// <param name="handler">The handler to call when console output occurs.</param>
    // /// <returns>A GUID that can be used to unregister the listener.</returns>
    // Guid RegisterConsoleOutputListener(ConsoleOutputHandler handler);

    // /// <summary>
    // /// Unregisters a console output listener.
    // /// </summary>
    // /// <param name="guid">The GUID returned from RegisterConsoleOutputListener.</param>
    // void UnregisterConsoleOutputListener(Guid guid);

    /// <summary>
    /// Gets whether console filtering is enabled.
    /// </summary>
    /// <returns>True if filtering is enabled, false otherwise.</returns>
    bool IsFilterEnabled();

    /// <summary>
    /// Toggles the console filter on/off.
    /// </summary>
    void ToggleFilter();

    /// <summary>
    /// Reloads the filter configuration from file.
    /// </summary>
    void ReloadFilterConfiguration();

    /// <summary>
    /// Checks if a message needs filtering.
    /// </summary>
    /// <param name="message">The message to check.</param>
    /// <returns>True if the message should be filtered, false otherwise.</returns>
    bool NeedsFiltering(string message);

    /// <summary>
    /// Gets the counter text showing how many messages were filtered.
    /// </summary>
    /// <returns>The counter text.</returns>
    string GetCounterText();

    /// <summary>
    /// Writes a message to the server console using the tier0 logging system.
    /// </summary>
    /// <param name="message">The message</param>
    void WriteToServerConsole(string message);
}