namespace SwiftlyS2.Shared.Profiler;

public interface IContextedProfilerService {

  /// <summary>
  /// Start recording a new profile with the given name.
  /// </summary>
  /// <param name="name">The name of the profile to start.</param>
  void StartRecording(string name);

  /// <summary>
  /// Stop recording the profile with the given name.
  /// </summary>
  /// <param name="name">The name of the profile to stop.</param>
  void StopRecording(string name);

  /// <summary>
  /// Record the time taken for the given profile.
  /// </summary>
  /// <param name="name">The name of the profile to record the time for.</param>
  /// <param name="duration">The duration to record.</param>
  void RecordTime(string name, double duration);

}