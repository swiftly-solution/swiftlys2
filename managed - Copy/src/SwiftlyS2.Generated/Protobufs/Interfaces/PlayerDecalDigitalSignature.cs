
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface PlayerDecalDigitalSignature : ITypedProtobuf<PlayerDecalDigitalSignature>
{
  static PlayerDecalDigitalSignature ITypedProtobuf<PlayerDecalDigitalSignature>.Wrap(nint handle, bool isManuallyAllocated) => new PlayerDecalDigitalSignatureImpl(handle, isManuallyAllocated);


  public byte[] Signature { get; set; }


  public uint Accountid { get; set; }


  public uint Rtime { get; set; }


  public IProtobufRepeatedFieldValueType<float> Endpos { get; }


  public IProtobufRepeatedFieldValueType<float> Startpos { get; }


  public IProtobufRepeatedFieldValueType<float> Left { get; }


  public uint TxDefidx { get; set; }


  public int Entindex { get; set; }


  public uint Hitbox { get; set; }


  public float Creationtime { get; set; }


  public uint Equipslot { get; set; }


  public uint TraceId { get; set; }


  public IProtobufRepeatedFieldValueType<float> Normal { get; }


  public uint TintId { get; set; }

}
