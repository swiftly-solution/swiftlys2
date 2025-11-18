using SwiftlyS2.Shared.Sounds;

namespace PluginId;

/// <summary>
/// This is an example that shows how to use sound event.
/// </summary>
public partial class PluginId {

  public void InitializeSoundEvent() {

    // Create a soundevent.
    using var soundEvent = new SoundEvent() {
      // Set the soundevent name.
      Name = "Weapon_AK47.Single",

      // Set the source entity,
      // can be get by entity.Index,
      // or set to -1 for emitting at recipient location (by default)
      SourceEntityIndex = -1,

      // Control the volume.
      Volume = 0.5f,

      // Control the pitch.
      Pitch = 2f


    };

    // More param can be set.
    soundEvent.SetFloat3("public.position", 1.0f, 1.0f, 1.0f);

    // Don't forget to add recipients.
    soundEvent.Recipients.AddAllPlayers();

    // Emit the sound event.
    soundEvent.Emit();
    
  }
}