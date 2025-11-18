
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class OperationalVarValueImpl : TypedProtobuf<OperationalVarValue>, OperationalVarValue
{
  public OperationalVarValueImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public string Name
  { get => Accessor.GetString("name"); set => Accessor.SetString("name", value); }


  public int Ivalue
  { get => Accessor.GetInt32("ivalue"); set => Accessor.SetInt32("ivalue", value); }


  public float Fvalue
  { get => Accessor.GetFloat("fvalue"); set => Accessor.SetFloat("fvalue", value); }


  public byte[] Svalue
  { get => Accessor.GetBytes("svalue"); set => Accessor.SetBytes("svalue", value); }

}
