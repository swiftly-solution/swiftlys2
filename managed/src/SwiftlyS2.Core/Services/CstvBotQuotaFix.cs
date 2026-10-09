using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Events;
using SwiftlyS2.Shared.Memory;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.Services;

// The engine's quota removal path treats a pawn-less HLTV client as a bot.
// Filter it out before selection so the engine can still remove an ordinary bot.
// Owned by CoreHookService; uses the existing memory hooks and core gamedata.
internal sealed class CstvBotQuotaFix : IDisposable
{
    private const string SelectionSignature = "CstvBotQuotaFix::SelectAndKickBot";
    private const string CandidateSignature = "CstvBotQuotaFix::TeamCandidate";

    // Native C++ bool is returned in AL on both supported x64 platforms.
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate byte SelectAndKickBot( int team );
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate byte TeamCandidate( nint filter, nint controller, nint pawn );

    [ThreadStatic] private static int selectionDepth;
    private readonly ISwiftlyCore core;
    private readonly ILogger logger;
    private IUnmanagedFunction<SelectAndKickBot>? selection;
    private IUnmanagedFunction<TeamCandidate>? candidate;
    private Guid selectionHook;
    private Guid candidateHook;
    private volatile bool active;
    private bool subscribed;
    private int reportedSkip;
    private long nextErrorLogAt;

    internal CstvBotQuotaFix( ISwiftlyCore core, ILogger logger )
    {
        this.core = core;
        this.logger = logger;
        try
        {
            if (!core.GameData.TryGetSignature(SelectionSignature, out var selectionAddress)
                || !core.GameData.TryGetSignature(CandidateSignature, out var candidateAddress)
                || selectionAddress == 0 || candidateAddress == 0 || selectionAddress == candidateAddress)
                throw new InvalidOperationException("CSTV quota gamedata is missing or invalid.");

            candidate = core.Memory.GetUnmanagedFunctionByAddress<TeamCandidate>(candidateAddress);
            candidateHook = candidate.AddHook(next => ( filter, controller, pawn ) =>
            {
                try
                {
                    // This predicate is shared with unrelated iterators. Only alter
                    // its result inside the native bot selection call on this thread.
                    if (active && selectionDepth > 0 && controller != 0
                        && core.Memory.ToSchemaClass<CCSPlayerController>(controller).IsHLTV)
                    {
                        if (Interlocked.Exchange(ref reportedSkip, 1) == 0)
                            logger.LogInformation("CSTV quota fix skipped HLTV during native bot removal (first occurrence this map).");
                        return 0;
                    }
                    return next()(filter, controller, pawn);
                }
                catch (Exception ex)
                {
                    ReportError(ex);
                    return 0;
                }
            });
            if (candidateHook == Guid.Empty)
                throw new InvalidOperationException("Could not hook the CSTV quota candidate predicate.");

            selection = core.Memory.GetUnmanagedFunctionByAddress<SelectAndKickBot>(selectionAddress);
            selectionHook = selection.AddHook(next => team =>
            {
                ++selectionDepth;
                try { return next()(team); }
                catch (Exception ex)
                {
                    ReportError(ex);
                    return 0; // Do not claim that a bot was removed.
                }
                finally { --selectionDepth; }
            });
            if (selectionHook == Guid.Empty)
                throw new InvalidOperationException("Could not hook native bot selection.");

            core.Event.OnMapLoad += OnMapLoad;
            core.Event.OnMapUnload += OnMapUnload;
            subscribed = true;
            active = true;
            logger.LogInformation("Built-in CSTV quota fix installed: selection={Selection:X}, candidate={Candidate:X}. Bot settings are unchanged.", selectionAddress, candidateAddress);
        }
        catch (Exception ex)
        {
            Dispose();
            logger.LogError(ex, "Built-in CSTV quota fix unavailable. Update the CSTV quota gamedata; automatic bot removal may disconnect SourceTV.");
        }
    }

    private void OnMapLoad( IOnMapLoadEvent @event )
    {
        Interlocked.Exchange(ref reportedSkip, 0);
        active = true;
    }

    private void OnMapUnload( IOnMapUnloadEvent @event ) => active = false;

    private void ReportError( Exception ex )
    {
        // Never propagate a managed exception through a reverse native callback.
        var now = Environment.TickCount64;
        var next = Interlocked.Read(ref nextErrorLogAt);
        if (now >= next && Interlocked.CompareExchange(ref nextErrorLogAt, now + 30_000, next) == next)
            logger.LogError(ex, "CSTV quota fix callback failed; returning false (limited to once per 30 seconds).");
    }

    public void Dispose()
    {
        active = false;
        if (subscribed)
        {
            core.Event.OnMapLoad -= OnMapLoad;
            core.Event.OnMapUnload -= OnMapUnload;
            subscribed = false;
        }
        // Remove the scope first. Also rolls back a partially installed hook pair.
        if (selectionHook != Guid.Empty) selection?.RemoveHook(selectionHook);
        if (candidateHook != Guid.Empty) candidate?.RemoveHook(candidateHook);
        selectionHook = candidateHook = Guid.Empty;
        selection = null;
        candidate = null;
    }
}
