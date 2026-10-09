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

    [ThreadStatic]
    private static int selectionDepth;
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
            if (!this.core.GameData.TryGetSignature(SelectionSignature, out var selectionAddress)
                || !this.core.GameData.TryGetSignature(CandidateSignature, out var candidateAddress)
                || selectionAddress == 0 || candidateAddress == 0 || selectionAddress == candidateAddress)
                throw new InvalidOperationException("CSTV quota gamedata is missing or invalid.");

            this.candidate = this.core.Memory.GetUnmanagedFunctionByAddress<TeamCandidate>(candidateAddress);
            this.candidateHook = this.candidate.AddHook(next => ( filter, controller, pawn ) =>
            {
                try
                {
                    // This predicate is shared with unrelated iterators. Only alter
                    // its result inside the native bot selection call on this thread.
                    if (this.active && selectionDepth > 0 && controller != 0
                        && this.core.Memory.ToSchemaClass<CCSPlayerController>(controller).IsHLTV)
                    {
                        if (Interlocked.Exchange(ref this.reportedSkip, 1) == 0)
                            this.logger.LogInformation("CSTV quota fix skipped HLTV during native bot removal (first occurrence this map).");
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
            if (this.candidateHook == Guid.Empty)
                throw new InvalidOperationException("Could not hook the CSTV quota candidate predicate.");

            this.selection = this.core.Memory.GetUnmanagedFunctionByAddress<SelectAndKickBot>(selectionAddress);
            this.selectionHook = this.selection.AddHook(next => team =>
            {
                ++selectionDepth;
                try
                {
                    return next()(team);
                }
                catch (Exception ex)
                {
                    ReportError(ex);
                    return 0; // Do not claim that a bot was removed.
                }
                finally
                {
                    --selectionDepth;
                }
            });
            if (this.selectionHook == Guid.Empty)
                throw new InvalidOperationException("Could not hook native bot selection.");

            this.core.Event.OnMapLoad += OnMapLoad;
            this.core.Event.OnMapUnload += OnMapUnload;
            this.subscribed = true;
            this.active = true;
            this.logger.LogInformation("Built-in CSTV quota fix installed: selection={Selection:X}, candidate={Candidate:X}. Bot settings are unchanged.", selectionAddress, candidateAddress);
        }
        catch (Exception ex)
        {
            Dispose();
            this.logger.LogError(ex, "Built-in CSTV quota fix unavailable. Update the CSTV quota gamedata; automatic bot removal may disconnect SourceTV.");
        }
    }

    private void OnMapLoad( IOnMapLoadEvent @event )
    {
        _ = Interlocked.Exchange(ref this.reportedSkip, 0);
        this.active = true;
    }

    private void OnMapUnload( IOnMapUnloadEvent @event ) => this.active = false;

    private void ReportError( Exception ex )
    {
        // Never propagate a managed exception through a reverse native callback.
        var now = Environment.TickCount64;
        var next = Interlocked.Read(ref this.nextErrorLogAt);
        if (now >= next && Interlocked.CompareExchange(ref this.nextErrorLogAt, now + 30_000, next) == next)
            this.logger.LogError(ex, "CSTV quota fix callback failed; returning false (limited to once per 30 seconds).");
    }

    public void Dispose()
    {
        this.active = false;
        if (this.subscribed)
        {
            this.core.Event.OnMapLoad -= OnMapLoad;
            this.core.Event.OnMapUnload -= OnMapUnload;
            this.subscribed = false;
        }
        // Remove the scope first. Also rolls back a partially installed hook pair.
        if (this.selectionHook != Guid.Empty) this.selection?.RemoveHook(this.selectionHook);
        if (this.candidateHook != Guid.Empty) this.candidate?.RemoveHook(this.candidateHook);
        this.selectionHook = this.candidateHook = Guid.Empty;
        this.selection = null;
        this.candidate = null;
    }
}
