
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CEntityMessageDoSparkImpl : TypedProtobuf<CEntityMessageDoSpark>, CEntityMessageDoSpark
{
  public CEntityMessageDoSparkImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public Vector Origin
  { get => Accessor.GetVector("origin"); set => Accessor.SetVector("origin", value); }


  public int Entityindex
  { get => Accessor.GetInt32("entityindex"); set => Accessor.SetInt32("entityindex", value); }


  public float Radius
  { get => Accessor.GetFloat("radius"); set => Accessor.SetFloat("radius", value); }


  public uint Color
  { get => Accessor.GetUInt32("color"); set => Accessor.SetUInt32("color", value); }


  public uint Beams
  { get => Accessor.GetUInt32("beams"); set => Accessor.SetUInt32("beams", value); }


  public float Thick
  { get => Accessor.GetFloat("thick"); set => Accessor.SetFloat("thick", value); }


  public float Duration
  { get => Accessor.GetFloat("duration"); set => Accessor.SetFloat("duration", value); }


  public CEntityMsg EntityMsg
  { get => new CEntityMsgImpl(NativeNetMessages.GetNestedMessage(Address, "entity_msg"), false); }

}
