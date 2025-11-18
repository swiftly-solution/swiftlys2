using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.CommandLine;

namespace SwiftlyS2.Core.CommandLine;

internal class CommandLineService : ICommandLine
{
    public int ParameterCount => NativeCommandLine.GetParameterCount();

    public string CommandLine => NativeCommandLine.GetCommandLine();

    public bool HasParameters => NativeCommandLine.HasParameters();

    public float GetParameterFloat(string paramName, float defaultValue = 0)
    {
        return NativeCommandLine.GetParameterValueFloat(paramName, defaultValue);
    }

    public int GetParameterInt(string paramName, int defaultValue = 0)
    {
        return NativeCommandLine.GetParameterValueInt(paramName, defaultValue);
    }

    public string GetParameterString(string paramName, string defaultValue = "")
    {
        return NativeCommandLine.GetParameterValueString(paramName, defaultValue);
    }

    public bool HasParameter(string paramName)
    {
        return NativeCommandLine.HasParameter(paramName);
    }
}