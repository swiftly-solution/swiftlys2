namespace SwiftlyS2.Shared.Events;

public interface IOnPrecacheResourceEvent
{
  /// <summary>
  /// Add a resource to the precache list.
  /// </summary>
  /// <param name="path">The path of the resource to precache.</param>
  void AddItem(string path);
  
}