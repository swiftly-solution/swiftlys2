using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEventDefinitions;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "teamplay_broadcast_audio"
/// emits a sound to everyone on a team
/// </summary>
internal class EventTeamplayBroadcastAudioImpl : GameEvent<EventTeamplayBroadcastAudio>, EventTeamplayBroadcastAudio
{

    public EventTeamplayBroadcastAudioImpl( nint address ) : base(address)
    {
    }

    // unique team id
    public byte Team { get => (byte)Accessor.GetInt32("team"); set => Accessor.SetInt32("team", value); }

    // name of the sound to emit
    public string Sound { get => Accessor.GetString("sound"); set => Accessor.SetString("sound", value); }
}
