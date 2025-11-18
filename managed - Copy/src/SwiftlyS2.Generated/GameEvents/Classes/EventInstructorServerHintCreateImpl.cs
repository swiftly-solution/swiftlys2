using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "instructor_server_hint_create"
/// create a hint using data supplied entirely by the server/map. Intended for hints to smooth playtests before content is ready to make the hint unneccessary. NOT INTENDED AS A SHIPPABLE CRUTCH
/// </summary>
internal class EventInstructorServerHintCreateImpl : GameEvent<EventInstructorServerHintCreate>, EventInstructorServerHintCreate
{

  public EventInstructorServerHintCreateImpl(nint address) : base(address)
  {
  }

  // user ID of the player that triggered the hint
  public CCSPlayerController UserIdController
  { get => Accessor.GetPlayerController("userid"); }

  // user ID of the player that triggered the hint
  public CCSPlayerPawn UserIdPawn
  { get => Accessor.GetPlayerPawn("userid"); }

  // user ID of the player that triggered the hint
  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }

  // user ID of the player that triggered the hint
  public int UserId
  { get => Accessor.GetInt32("userid"); set => Accessor.SetInt32("userid", value); }

  // entity id of the env_instructor_hint that fired the event
  public int HintEntindex
  { get => Accessor.GetInt32("hint_entindex"); set => Accessor.SetInt32("hint_entindex", value); }

  // what to name the hint. For referencing it again later (e.g. a kill command for the hint instead of a timeout)
  public string HintName
  { get => Accessor.GetString("hint_name"); set => Accessor.SetString("hint_name", value); }

  // type name so that messages of the same type will replace each other
  public string HintReplaceKey
  { get => Accessor.GetString("hint_replace_key"); set => Accessor.SetString("hint_replace_key", value); }

  // entity id that the hint should display at
  public int HintTarget
  { get => Accessor.GetInt32("hint_target"); set => Accessor.SetInt32("hint_target", value); }

  // playerslot of the activator
  public int HintActivatorUserid
  { get => Accessor.GetPlayerSlot("hint_activator_userid"); set => Accessor.SetPlayerSlot("hint_activator_userid", value); }

  // how long in seconds until the hint automatically times out, 0 = never
  public short HintTimeout
  { get => (short)Accessor.GetInt32("hint_timeout"); set => Accessor.SetInt32("hint_timeout", value); }

  // the hint icon to use when the hint is onscreen. e.g. "icon_alert_red"
  public string HintIconOnscreen
  { get => Accessor.GetString("hint_icon_onscreen"); set => Accessor.SetString("hint_icon_onscreen", value); }

  // the hint icon to use when the hint is offscreen. e.g. "icon_alert"
  public string HintIconOffscreen
  { get => Accessor.GetString("hint_icon_offscreen"); set => Accessor.SetString("hint_icon_offscreen", value); }

  // the hint caption. e.g. "#ThisIsDangerous"
  public string HintCaption
  { get => Accessor.GetString("hint_caption"); set => Accessor.SetString("hint_caption", value); }

  // the hint caption that only the activator sees e.g. "#YouPushedItGood"
  public string HintActivatorCaption
  { get => Accessor.GetString("hint_activator_caption"); set => Accessor.SetString("hint_activator_caption", value); }

  // the hint color in "r,g,b" format where each component is 0-255
  public string HintColor
  { get => Accessor.GetString("hint_color"); set => Accessor.SetString("hint_color", value); }

  // how far on the z axis to offset the hint from entity origin
  public float HintIconOffset
  { get => Accessor.GetFloat("hint_icon_offset"); set => Accessor.SetFloat("hint_icon_offset", value); }

  // range before the hint is culled
  public float HintRange
  { get => Accessor.GetFloat("hint_range"); set => Accessor.SetFloat("hint_range", value); }

  // hint flags
  public int HintFlags
  { get => Accessor.GetInt32("hint_flags"); set => Accessor.SetInt32("hint_flags", value); }

  // bindings to use when use_binding is the onscreen icon
  public string HintBinding
  { get => Accessor.GetString("hint_binding"); set => Accessor.SetString("hint_binding", value); }

  // if false, the hint will dissappear if the target entity is invisible
  public bool HintAllowNodrawTarget
  { get => Accessor.GetBool("hint_allow_nodraw_target"); set => Accessor.SetBool("hint_allow_nodraw_target", value); }

  // if true, the hint will not show when outside the player view
  public bool HintNooffscreen
  { get => Accessor.GetBool("hint_nooffscreen"); set => Accessor.SetBool("hint_nooffscreen", value); }

  // if true, the hint caption will show even if the hint is occluded
  public bool HintForcecaption
  { get => Accessor.GetBool("hint_forcecaption"); set => Accessor.SetBool("hint_forcecaption", value); }

  // if true, only the local player will see the hint
  public bool HintLocalPlayerOnly
  { get => Accessor.GetBool("hint_local_player_only"); set => Accessor.SetBool("hint_local_player_only", value); }

  // Game sound to play
  public string HintStartSound
  { get => Accessor.GetString("hint_start_sound"); set => Accessor.SetString("hint_start_sound", value); }

  // Path for Panorama layout file
  public string HintLayoutfile
  { get => Accessor.GetString("hint_layoutfile"); set => Accessor.SetString("hint_layoutfile", value); }

  // Attachment type for the Panorama panel
  public short HintVrPanelType
  { get => (short)Accessor.GetInt32("hint_vr_panel_type"); set => Accessor.SetInt32("hint_vr_panel_type", value); }

  // Height offset for attached panels
  public float HintVrHeightOffset
  { get => Accessor.GetFloat("hint_vr_height_offset"); set => Accessor.SetFloat("hint_vr_height_offset", value); }

  // offset for attached panels
  public float HintVrOffsetX
  { get => Accessor.GetFloat("hint_vr_offset_x"); set => Accessor.SetFloat("hint_vr_offset_x", value); }

  // offset for attached panels
  public float HintVrOffsetY
  { get => Accessor.GetFloat("hint_vr_offset_y"); set => Accessor.SetFloat("hint_vr_offset_y", value); }

  // offset for attached panels
  public float HintVrOffsetZ
  { get => Accessor.GetFloat("hint_vr_offset_z"); set => Accessor.SetFloat("hint_vr_offset_z", value); }

  // gamepad bindings to use when use_binding is the onscreen icon
  public string HintGamepadBinding
  { get => Accessor.GetString("hint_gamepad_binding"); set => Accessor.SetString("hint_gamepad_binding", value); }
}
