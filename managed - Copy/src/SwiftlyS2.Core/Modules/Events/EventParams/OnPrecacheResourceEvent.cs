using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Events;

namespace SwiftlyS2.Core.Events;


internal class OnPrecacheResourceEvent : IOnPrecacheResourceEvent
{

  internal required nint pResourceManifest;

  public void AddItem(string path) {
    GameFunctions.CEntityResourceManifest_AddResource(pResourceManifest, path);
  }
  
}