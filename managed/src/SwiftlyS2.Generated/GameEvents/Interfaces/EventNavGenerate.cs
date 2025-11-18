using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "nav_generate"
/// </summary>
public interface EventNavGenerate : IGameEvent<EventNavGenerate>
{

    static EventNavGenerate IGameEvent<EventNavGenerate>.Create( nint address ) => new EventNavGenerateImpl(address);

    static string IGameEvent<EventNavGenerate>.GetName() => "nav_generate";

    static uint IGameEvent<EventNavGenerate>.GetHash() => 0x06197C30u;
}
