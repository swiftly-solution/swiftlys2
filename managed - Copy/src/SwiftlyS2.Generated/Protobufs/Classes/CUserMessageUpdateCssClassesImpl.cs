
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMessageUpdateCssClassesImpl : NetMessage<CUserMessageUpdateCssClasses>, CUserMessageUpdateCssClasses
{
  public CUserMessageUpdateCssClassesImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int TargetWorldPanel
  { get => Accessor.GetInt32("target_world_panel"); set => Accessor.SetInt32("target_world_panel", value); }


  public string CssClasses
  { get => Accessor.GetString("css_classes"); set => Accessor.SetString("css_classes", value); }


  public bool IsAdd
  { get => Accessor.GetBool("is_add"); set => Accessor.SetBool("is_add", value); }

}
