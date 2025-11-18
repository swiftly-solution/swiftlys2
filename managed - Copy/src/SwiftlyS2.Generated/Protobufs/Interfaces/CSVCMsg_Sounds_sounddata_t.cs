
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CSVCMsg_Sounds_sounddata_t : ITypedProtobuf<CSVCMsg_Sounds_sounddata_t>
{
  static CSVCMsg_Sounds_sounddata_t ITypedProtobuf<CSVCMsg_Sounds_sounddata_t>.Wrap(nint handle, bool isManuallyAllocated) => new CSVCMsg_Sounds_sounddata_tImpl(handle, isManuallyAllocated);


  public int OriginX { get; set; }


  public int OriginY { get; set; }


  public int OriginZ { get; set; }


  public uint Volume { get; set; }


  public float DelayValue { get; set; }


  public int SequenceNumber { get; set; }


  public int EntityIndex { get; set; }


  public int Channel { get; set; }


  public int Pitch { get; set; }


  public int Flags { get; set; }


  public uint SoundNum { get; set; }


  public uint SoundNumHandle { get; set; }


  public int SpeakerEntity { get; set; }


  public int RandomSeed { get; set; }


  public int SoundLevel { get; set; }


  public bool IsSentence { get; set; }


  public bool IsAmbient { get; set; }


  public uint Guid { get; set; }


  public ulong SoundResourceId { get; set; }

}
