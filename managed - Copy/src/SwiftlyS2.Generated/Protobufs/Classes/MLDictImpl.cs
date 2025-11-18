
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class MLDictImpl : TypedProtobuf<MLDict>, MLDict
{
  public MLDictImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public string Key
  { get => Accessor.GetString("key"); set => Accessor.SetString("key", value); }


  public string ValString
  { get => Accessor.GetString("val_string"); set => Accessor.SetString("val_string", value); }


  public int ValInt
  { get => Accessor.GetInt32("val_int"); set => Accessor.SetInt32("val_int", value); }


  public float ValFloat
  { get => Accessor.GetFloat("val_float"); set => Accessor.SetFloat("val_float", value); }

}
