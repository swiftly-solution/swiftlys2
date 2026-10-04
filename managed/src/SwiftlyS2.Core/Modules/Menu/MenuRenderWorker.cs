using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using SwiftlyS2.Shared.Menu;

namespace SwiftlyS2.Core.Menu;

internal sealed class MenuRenderWorker( ILogger<MenuRenderWorker> logger ) : IDisposable
{
    private readonly BlockingCollection<Job> jobs = new();

    private Thread? thread;

    public void Start()
    {
        if (thread is not null)
        {
            return;
        }

        thread = new Thread(Run) { IsBackground = true, Name = "SwiftlyS2 Menu Render" };
        thread.Start();
    }

    public void Enqueue( MenuSession session, IMenuRenderer renderer, MenuRenderContext context )
    {
        jobs.Add(new Job(session, renderer, context));
    }

    private void Run()
    {
        foreach (var job in jobs.GetConsumingEnumerable())
        {
            if (!job.Session.IsOpen)
            {
                continue;
            }

            try
            {
                job.Renderer.Render(job.Context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to render menu '{MenuId}' for player {PlayerId}.", job.Session.Instance.Id, job.Session.Player.PlayerID);
            }
        }
    }

    public void Dispose()
    {
        jobs.CompleteAdding();
        _ = thread?.Join(TimeSpan.FromSeconds(2));
        jobs.Dispose();
    }

    private readonly record struct Job( MenuSession Session, IMenuRenderer Renderer, MenuRenderContext Context );
}
