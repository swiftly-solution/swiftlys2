namespace SwiftlyS2.Shared.Menu.Components;

internal static class MenuStrings
{
    public static string Repeat( string unit, int count )
    {
        if (count <= 0 || unit.Length == 0)
        {
            return string.Empty;
        }

        if (unit.Length == 1)
        {
            return new string(unit[0], count);
        }

        return string.Create(unit.Length * count, unit, static ( span, source ) => {
            for (var offset = 0; offset < span.Length; offset += source.Length)
            {
                source.CopyTo(span[offset..]);
            }
        });
    }
}
