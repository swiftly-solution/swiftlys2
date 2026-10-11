using System.Globalization;
using SwiftlyS2.Shared;
using Tester.Framework;
using static Tester.Framework.TestContext;

namespace Tester.Sections;

public sealed class CommandLineSection( ISwiftlyCore core ) : Section(core)
{
    private const string Bogus = "-tester_no_such_parameter_zzz";

    public override string Name => "commandline";

    private static List<(string Param, string? Value)> Discover( string line )
    {
        var tokens = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var found = new List<(string, string?)>();
        for (var i = 0; i < tokens.Length; i++)
        {
            if (tokens[i][0] is not ('-' or '+') || tokens[i].Contains('"')) continue;
            var value = i + 1 < tokens.Length && tokens[i + 1][0] is not ('-' or '+') && !tokens[i + 1].Contains('"') ? tokens[i + 1] : null;
            found.Add((tokens[i], value));
        }
        return found;
    }

    public override Task Test( TestContext t )
    {
        var cl = Core.CommandLine;

        t.Test("CommandLine string is readable and stable", () =>
        {
            var line = cl.CommandLine;
            NotNull(line, "CommandLine");
            Equal(line, cl.CommandLine, "second read");
        });

        t.Test("HasParameters / ParameterCount agree with CommandLine", () =>
        {
            var empty = string.IsNullOrWhiteSpace(cl.CommandLine);
            Equal(!empty, cl.HasParameters, "HasParameters");
            Expect(cl.ParameterCount >= 0, $"ParameterCount={cl.ParameterCount}");
            if (cl.HasParameters) Expect(cl.ParameterCount > 0, "HasParameters but ParameterCount == 0");
            else Equal(0, cl.ParameterCount, "ParameterCount");
        });

        t.Test("HasParameter(bogus) is false", () => Expect(!cl.HasParameter(Bogus), "bogus parameter found"));

        t.Test("Missing parameter returns the supplied default", () =>
        {
            Equal("fallback", cl.GetParameterString(Bogus, "fallback"), "string default");
            Equal("", cl.GetParameterString(Bogus), "string implicit default");
            Equal(1234, cl.GetParameterInt(Bogus, 1234), "int default");
            Equal(0, cl.GetParameterInt(Bogus), "int implicit default");
            Equal(2.5f, cl.GetParameterFloat(Bogus, 2.5f), "float default");
            Equal(0f, cl.GetParameterFloat(Bogus), "float implicit default");
        });

        t.Test("Every parameter found in CommandLine is reported by HasParameter", () =>
        {
            var found = Discover(cl.CommandLine);
            if (found.Count == 0) Skip("no -/+ parameters on this server's command line");
            foreach (var (param, _) in found)
                Expect(cl.HasParameter(param), $"HasParameter({param}) false");
        });

        t.Test("GetParameterString returns the value that follows the parameter", () =>
        {
            var withValue = Discover(cl.CommandLine).Where(p => p.Value is not null).ToList();
            if (withValue.Count == 0) Skip("no parameter with a value on this command line");
            foreach (var (param, value) in withValue)
                Equal(value!, cl.GetParameterString(param, "<missing>"), $"GetParameterString({param})");
        });

        t.Test("GetParameterInt parses numeric values", () =>
        {
            var numeric = Discover(cl.CommandLine).Where(p => int.TryParse(p.Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out _)).ToList();
            if (numeric.Count == 0) Skip("no integer parameter on this command line");
            foreach (var (param, value) in numeric)
                Equal(int.Parse(value!, CultureInfo.InvariantCulture), cl.GetParameterInt(param, int.MinValue), $"GetParameterInt({param})");
        });

        t.Test("GetParameterFloat parses numeric values", () =>
        {
            var numeric = Discover(cl.CommandLine).Where(p => float.TryParse(p.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out _)).ToList();
            if (numeric.Count == 0) Skip("no numeric parameter on this command line");
            foreach (var (param, value) in numeric)
                Expect(MathF.Abs(float.Parse(value!, CultureInfo.InvariantCulture) - cl.GetParameterFloat(param, float.NaN)) < 1e-4f, $"GetParameterFloat({param})");
        });

        t.Test("Empty parameter name does not throw", () =>
        {
            _ = cl.HasParameter("");
            _ = cl.GetParameterString("", "d");
            _ = cl.GetParameterInt("", 1);
            _ = cl.GetParameterFloat("", 1f);
        });

        return Task.CompletedTask;
    }

    public override bool ProfileInBackground => true;

    public override Task Profile( ProfileContext p )
    {
        var cl = Core.CommandLine;
        var known = Discover(cl.CommandLine).FirstOrDefault().Param;

        p.Profile("CommandLine.CommandLine", () => _ = cl.CommandLine, 100_000);
        p.Profile("CommandLine.ParameterCount", () => _ = cl.ParameterCount, 200_000);
        p.Profile("CommandLine.HasParameters", () => _ = cl.HasParameters, 200_000);
        p.Profile("CommandLine.HasParameter(bogus)", () => _ = cl.HasParameter(Bogus), 100_000);
        p.Profile("CommandLine.GetParameterString(bogus)", () => _ = cl.GetParameterString(Bogus, "d"), 100_000);
        p.Profile("CommandLine.GetParameterInt(bogus)", () => _ = cl.GetParameterInt(Bogus, 1), 100_000);
        p.Profile("CommandLine.GetParameterFloat(bogus)", () => _ = cl.GetParameterFloat(Bogus, 1f), 100_000);

        if (known is not null)
        {
            p.Profile($"CommandLine.HasParameter({known})", () => _ = cl.HasParameter(known), 100_000);
            p.Profile($"CommandLine.GetParameterString({known})", () => _ = cl.GetParameterString(known, "d"), 100_000);
        }

        return Task.CompletedTask;
    }
}
