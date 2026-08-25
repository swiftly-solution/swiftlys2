using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using SwiftlyS2.Shared.Menu;

namespace SwiftlyS2.Core.Menu;

internal sealed class MenuRenderDiagnostics( ILogger<MenuRenderDiagnostics> logger )
{
    private readonly ConcurrentDictionary<string, byte> reported = new();

    public void ReportUnsupported( string rendererId, MenuNode node )
    {
        var key = $"{rendererId}:{node.GetType().FullName}";

        if (!reported.TryAdd(key, 0))
        {
            return;
        }

        logger.LogWarning(
            "Renderer '{RendererId}' cannot draw node type '{NodeType}'. It will be skipped for the lifetime of this process.",
            rendererId,
            node.GetType().Name);
    }
}
