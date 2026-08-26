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

      // Where the sound plays from.
      // -1, the default, plays it at each recipient.
      // 0 plays it at public.position in the world.
      // Any other index attaches it to that entity, from entity.Index.
      SourceEntityIndex = -1,

      // Control the volume.
      Volume = 0.5f,

      // Control the pitch.
      Pitch = 2f
    };

    // Don't forget to add recipients.
    soundEvent.Recipients.AddAllPlayers();

    // Emit the sound event.
    soundEvent.Emit();

    // More params can be set. public.position places the sound somewhere in the
    // world, which needs the source entity to be 0. At -1 the sound already plays
    // at the listener, and on an entity it follows that entity instead, so the
    // position is ignored in both of those.
    using var positioned = new SoundEvent() {
      Name = "Weapon_AK47.Single",
      SourceEntityIndex = 0
    };

    positioned.SetFloat3("public.position", 0.0f, 0.0f, 0.0f);
    positioned.Recipients.AddAllPlayers();
    positioned.Emit();

  }
}
