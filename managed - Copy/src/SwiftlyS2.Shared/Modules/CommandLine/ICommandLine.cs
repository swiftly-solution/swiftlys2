namespace SwiftlyS2.Shared.CommandLine;

public interface ICommandLine
{
    /// <summary>
    /// Checks if a parameter exists in the command line.
    /// </summary>
    public bool HasParameter(string paramName);

    /// <summary>
    /// Gets the total number of parameters in the command line.
    /// </summary>
    public int ParameterCount { get; }

    /// <summary>
    /// Gets a string parameter from the command line.
    /// </summary>
    public string GetParameterString(string paramName, string defaultValue = "");

    /// <summary>
    /// Gets an integer parameter from the command line.
    /// </summary>
    public int GetParameterInt(string paramName, int defaultValue = 0);

    /// <summary>
    /// Gets a float parameter from the command line.
    /// </summary>
    public float GetParameterFloat(string paramName, float defaultValue = 0f);

    public string CommandLine { get; }

    public bool HasParameters { get; }
}