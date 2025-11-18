
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMessageShowMenuImpl : NetMessage<CUserMessageShowMenu>, CUserMessageShowMenu
{
  public CUserMessageShowMenuImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public uint Validslots
  { get => Accessor.GetUInt32("validslots"); set => Accessor.SetUInt32("validslots", value); }


  public uint Displaytime
  { get => Accessor.GetUInt32("displaytime"); set => Accessor.SetUInt32("displaytime", value); }


  public bool Needmore
  { get => Accessor.GetBool("needmore"); set => Accessor.SetBool("needmore", value); }


  public string Menustring
  { get => Accessor.GetString("menustring"); set => Accessor.SetString("menustring", value); }

}
