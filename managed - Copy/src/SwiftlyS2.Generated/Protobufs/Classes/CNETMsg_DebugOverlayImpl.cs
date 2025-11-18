
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CNETMsg_DebugOverlayImpl : NetMessage<CNETMsg_DebugOverlay>, CNETMsg_DebugOverlay
{
  public CNETMsg_DebugOverlayImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int Etype
  { get => Accessor.GetInt32("etype"); set => Accessor.SetInt32("etype", value); }


  public IProtobufRepeatedFieldValueType<Vector> Vectors
  { get => new ProtobufRepeatedFieldValueType<Vector>(Accessor, "vectors"); }


  public IProtobufRepeatedFieldValueType<Color> Colors
  { get => new ProtobufRepeatedFieldValueType<Color>(Accessor, "colors"); }


  public IProtobufRepeatedFieldValueType<float> Dimensions
  { get => new ProtobufRepeatedFieldValueType<float>(Accessor, "dimensions"); }


  public IProtobufRepeatedFieldValueType<float> Times
  { get => new ProtobufRepeatedFieldValueType<float>(Accessor, "times"); }


  public IProtobufRepeatedFieldValueType<bool> Bools
  { get => new ProtobufRepeatedFieldValueType<bool>(Accessor, "bools"); }


  public IProtobufRepeatedFieldValueType<ulong> Uint64s
  { get => new ProtobufRepeatedFieldValueType<ulong>(Accessor, "uint64s"); }


  public IProtobufRepeatedFieldValueType<string> Strings
  { get => new ProtobufRepeatedFieldValueType<string>(Accessor, "strings"); }

}
