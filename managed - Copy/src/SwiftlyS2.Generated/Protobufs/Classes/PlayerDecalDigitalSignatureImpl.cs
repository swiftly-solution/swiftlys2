
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class PlayerDecalDigitalSignatureImpl : TypedProtobuf<PlayerDecalDigitalSignature>, PlayerDecalDigitalSignature
{
  public PlayerDecalDigitalSignatureImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public byte[] Signature
  { get => Accessor.GetBytes("signature"); set => Accessor.SetBytes("signature", value); }


  public uint Accountid
  { get => Accessor.GetUInt32("accountid"); set => Accessor.SetUInt32("accountid", value); }


  public uint Rtime
  { get => Accessor.GetUInt32("rtime"); set => Accessor.SetUInt32("rtime", value); }


  public IProtobufRepeatedFieldValueType<float> Endpos
  { get => new ProtobufRepeatedFieldValueType<float>(Accessor, "endpos"); }


  public IProtobufRepeatedFieldValueType<float> Startpos
  { get => new ProtobufRepeatedFieldValueType<float>(Accessor, "startpos"); }


  public IProtobufRepeatedFieldValueType<float> Left
  { get => new ProtobufRepeatedFieldValueType<float>(Accessor, "left"); }


  public uint TxDefidx
  { get => Accessor.GetUInt32("tx_defidx"); set => Accessor.SetUInt32("tx_defidx", value); }


  public int Entindex
  { get => Accessor.GetInt32("entindex"); set => Accessor.SetInt32("entindex", value); }


  public uint Hitbox
  { get => Accessor.GetUInt32("hitbox"); set => Accessor.SetUInt32("hitbox", value); }


  public float Creationtime
  { get => Accessor.GetFloat("creationtime"); set => Accessor.SetFloat("creationtime", value); }


  public uint Equipslot
  { get => Accessor.GetUInt32("equipslot"); set => Accessor.SetUInt32("equipslot", value); }


  public uint TraceId
  { get => Accessor.GetUInt32("trace_id"); set => Accessor.SetUInt32("trace_id", value); }


  public IProtobufRepeatedFieldValueType<float> Normal
  { get => new ProtobufRepeatedFieldValueType<float>(Accessor, "normal"); }


  public uint TintId
  { get => Accessor.GetUInt32("tint_id"); set => Accessor.SetUInt32("tint_id", value); }

}
