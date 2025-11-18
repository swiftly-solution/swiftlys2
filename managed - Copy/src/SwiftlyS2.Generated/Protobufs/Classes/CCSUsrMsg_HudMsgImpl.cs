
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_HudMsgImpl : NetMessage<CCSUsrMsg_HudMsg>, CCSUsrMsg_HudMsg
{
  public CCSUsrMsg_HudMsgImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int Channel
  { get => Accessor.GetInt32("channel"); set => Accessor.SetInt32("channel", value); }


  public Vector2D Pos
  { get => Accessor.GetVector2D("pos"); set => Accessor.SetVector2D("pos", value); }


  public Color Clr1
  { get => Accessor.GetColor("clr1"); set => Accessor.SetColor("clr1", value); }


  public Color Clr2
  { get => Accessor.GetColor("clr2"); set => Accessor.SetColor("clr2", value); }


  public int Effect
  { get => Accessor.GetInt32("effect"); set => Accessor.SetInt32("effect", value); }


  public float FadeInTime
  { get => Accessor.GetFloat("fade_in_time"); set => Accessor.SetFloat("fade_in_time", value); }


  public float FadeOutTime
  { get => Accessor.GetFloat("fade_out_time"); set => Accessor.SetFloat("fade_out_time", value); }


  public float HoldTime
  { get => Accessor.GetFloat("hold_time"); set => Accessor.SetFloat("hold_time", value); }


  public float FxTime
  { get => Accessor.GetFloat("fx_time"); set => Accessor.SetFloat("fx_time", value); }


  public string Text
  { get => Accessor.GetString("text"); set => Accessor.SetString("text", value); }

}
