
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_EntityOutlineHighlightImpl : NetMessage<CCSUsrMsg_EntityOutlineHighlight>, CCSUsrMsg_EntityOutlineHighlight
{
  public CCSUsrMsg_EntityOutlineHighlightImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int Entidx
  { get => Accessor.GetInt32("entidx"); set => Accessor.SetInt32("entidx", value); }


  public bool Removehighlight
  { get => Accessor.GetBool("removehighlight"); set => Accessor.SetBool("removehighlight", value); }

}
