using SwiftlyS2.Core.Services;
using SwiftlyS2.Shared.Profiler;

namespace SwiftlyS2.Core.Profiler;

internal class ContextedProfilerService : IContextedProfilerService {

  private string _Name { get; init; }
  private ProfileService _ProfileService { get; init; }

  public ContextedProfilerService(CoreContext context, ProfileService profileService) {
    _Name = context.Name;
    _ProfileService = profileService;
  }

  public void StartRecording(string name) {
    _ProfileService.StartRecordingWithIdentifier(_Name, name);
  }

  public void StopRecording(string name) {
    _ProfileService.StopRecordingWithIdentifier(_Name, name);
  }

  public void RecordTime(string name, double duration) {
    _ProfileService.RecordTimeWithIdentifier(_Name, name, duration);
  }

  public string GeneratePerformanceTraceJson() {
    return _ProfileService.GenerateJSONPerformance(_Name);
  }
}