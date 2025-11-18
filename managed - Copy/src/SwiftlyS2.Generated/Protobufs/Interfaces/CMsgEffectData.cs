
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgEffectData : ITypedProtobuf<CMsgEffectData>
{
  static CMsgEffectData ITypedProtobuf<CMsgEffectData>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgEffectDataImpl(handle, isManuallyAllocated);


  public Vector Origin { get; set; }


  public Vector Start { get; set; }


  public Vector Normal { get; set; }


  public QAngle Angles { get; set; }


  public uint Entity { get; set; }


  public uint Otherentity { get; set; }


  public float Scale { get; set; }


  public float Magnitude { get; set; }


  public float Radius { get; set; }


  public uint Surfaceprop { get; set; }


  public ulong Effectindex { get; set; }


  public uint Damagetype { get; set; }


  public uint Material { get; set; }


  public uint Hitbox { get; set; }


  public uint Color { get; set; }


  public uint Flags { get; set; }


  public int Attachmentindex { get; set; }


  public uint Effectname { get; set; }


  public uint Attachmentname { get; set; }

}
