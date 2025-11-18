using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "instructor_server_hint_create"
/// create a hint using data supplied entirely by the server/map. Intended for hints to smooth playtests before content is ready to make the hint unneccessary. NOT INTENDED AS A SHIPPABLE CRUTCH
/// </summary>
public interface EventInstructorServerHintCreate : IGameEvent<EventInstructorServerHintCreate> {

  static EventInstructorServerHintCreate IGameEvent<EventInstructorServerHintCreate>.Create(nint address) => new EventInstructorServerHintCreateImpl(address);

  static string IGameEvent<EventInstructorServerHintCreate>.GetName() => "instructor_server_hint_create";

  static uint IGameEvent<EventInstructorServerHintCreate>.GetHash() => 0xB6A33F21u;
  /// <summary>
  /// user ID of the player that triggered the hint
  /// <br/>
  /// type: player_controller
  /// </summary>
  CCSPlayerController UserIdController { get; }

  /// <summary>
  /// user ID of the player that triggered the hint
  /// <br/>
  /// type: player_controller
  /// </summary>
  CCSPlayerPawn UserIdPawn { get; }


  // user ID of the player that triggered the hint
  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }
  /// <summary>
  /// user ID of the player that triggered the hint
  /// <br/>
  /// type: player_controller
  /// </summary>
  int UserId { get; set; }

  /// <summary>
  /// entity id of the env_instructor_hint that fired the event
  /// <br/>
  /// type: long
  /// </summary>
  int HintEntindex { get; set; }

  /// <summary>
  /// what to name the hint. For referencing it again later (e.g. a kill command for the hint instead of a timeout)
  /// <br/>
  /// type: string
  /// </summary>
  string HintName { get; set; }

  /// <summary>
  /// type name so that messages of the same type will replace each other
  /// <br/>
  /// type: string
  /// </summary>
  string HintReplaceKey { get; set; }

  /// <summary>
  /// entity id that the hint should display at
  /// <br/>
  /// type: long
  /// </summary>
  int HintTarget { get; set; }

  /// <summary>
  /// playerslot of the activator
  /// <br/>
  /// type: player_controller
  /// </summary>
  int HintActivatorUserid { get; set; }

  /// <summary>
  /// how long in seconds until the hint automatically times out, 0 = never
  /// <br/>
  /// type: short
  /// </summary>
  short HintTimeout { get; set; }

  /// <summary>
  /// the hint icon to use when the hint is onscreen. e.g. "icon_alert_red"
  /// <br/>
  /// type: string
  /// </summary>
  string HintIconOnscreen { get; set; }

  /// <summary>
  /// the hint icon to use when the hint is offscreen. e.g. "icon_alert"
  /// <br/>
  /// type: string
  /// </summary>
  string HintIconOffscreen { get; set; }

  /// <summary>
  /// the hint caption. e.g. "#ThisIsDangerous"
  /// <br/>
  /// type: string
  /// </summary>
  string HintCaption { get; set; }

  /// <summary>
  /// the hint caption that only the activator sees e.g. "#YouPushedItGood"
  /// <br/>
  /// type: string
  /// </summary>
  string HintActivatorCaption { get; set; }

  /// <summary>
  /// the hint color in "r,g,b" format where each component is 0-255
  /// <br/>
  /// type: string
  /// </summary>
  string HintColor { get; set; }

  /// <summary>
  /// how far on the z axis to offset the hint from entity origin
  /// <br/>
  /// type: float
  /// </summary>
  float HintIconOffset { get; set; }

  /// <summary>
  /// range before the hint is culled
  /// <br/>
  /// type: float
  /// </summary>
  float HintRange { get; set; }

  /// <summary>
  /// hint flags
  /// <br/>
  /// type: long
  /// </summary>
  int HintFlags { get; set; }

  /// <summary>
  /// bindings to use when use_binding is the onscreen icon
  /// <br/>
  /// type: string
  /// </summary>
  string HintBinding { get; set; }

  /// <summary>
  /// if false, the hint will dissappear if the target entity is invisible
  /// <br/>
  /// type: bool
  /// </summary>
  bool HintAllowNodrawTarget { get; set; }

  /// <summary>
  /// if true, the hint will not show when outside the player view
  /// <br/>
  /// type: bool
  /// </summary>
  bool HintNooffscreen { get; set; }

  /// <summary>
  /// if true, the hint caption will show even if the hint is occluded
  /// <br/>
  /// type: bool
  /// </summary>
  bool HintForcecaption { get; set; }

  /// <summary>
  /// if true, only the local player will see the hint
  /// <br/>
  /// type: bool
  /// </summary>
  bool HintLocalPlayerOnly { get; set; }

  /// <summary>
  /// Game sound to play
  /// <br/>
  /// type: string
  /// </summary>
  string HintStartSound { get; set; }

  /// <summary>
  /// Path for Panorama layout file
  /// <br/>
  /// type: string
  /// </summary>
  string HintLayoutfile { get; set; }

  /// <summary>
  /// Attachment type for the Panorama panel
  /// <br/>
  /// type: short
  /// </summary>
  short HintVrPanelType { get; set; }

  /// <summary>
  /// Height offset for attached panels
  /// <br/>
  /// type: float
  /// </summary>
  float HintVrHeightOffset { get; set; }

  /// <summary>
  /// offset for attached panels
  /// <br/>
  /// type: float
  /// </summary>
  float HintVrOffsetX { get; set; }

  /// <summary>
  /// offset for attached panels
  /// <br/>
  /// type: float
  /// </summary>
  float HintVrOffsetY { get; set; }

  /// <summary>
  /// offset for attached panels
  /// <br/>
  /// type: float
  /// </summary>
  float HintVrOffsetZ { get; set; }

  /// <summary>
  /// gamepad bindings to use when use_binding is the onscreen icon
  /// <br/>
  /// type: string
  /// </summary>
  string HintGamepadBinding { get; set; }

}
