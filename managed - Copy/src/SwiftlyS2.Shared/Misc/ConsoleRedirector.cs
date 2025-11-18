using System.Text;
using SwiftlyS2.Core.Natives;

namespace SwiftlyS2.Shared.Misc;

class ConsoleRedirector : TextWriter
{
    private readonly TextWriter originalOut;
    private readonly Lock lockObject = new();
    private bool isRedirecting = false;

    public ConsoleRedirector()
    {
        originalOut = Console.Out;
    }

    public override Encoding Encoding => originalOut.Encoding;

    public override void WriteLine( string? value )
    {
        lock (lockObject)
        {
            if (isRedirecting)
            {
                return;
            }

            try
            {
                isRedirecting = true;
                string v = value ?? "(null)";
                NativeEngineHelpers.SendMessageToConsole(v + (v.EndsWith("\n") ? "" : "\n"));
            }
            finally
            {
                isRedirecting = false;
            }
        }
    }

    public override void Write( string? value )
    {
        lock (lockObject)
        {
            if (isRedirecting)
            {
                return;
            }

            try
            {
                isRedirecting = true;
                NativeEngineHelpers.SendMessageToConsole(value ?? "(null)");
            }
            finally
            {
                isRedirecting = false;
            }
        }
    }
}