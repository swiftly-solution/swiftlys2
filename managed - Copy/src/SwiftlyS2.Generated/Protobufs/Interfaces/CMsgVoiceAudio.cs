
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgVoiceAudio : ITypedProtobuf<CMsgVoiceAudio>
{
  static CMsgVoiceAudio ITypedProtobuf<CMsgVoiceAudio>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgVoiceAudioImpl(handle, isManuallyAllocated);


  public VoiceDataFormat_t Format { get; set; }


  public byte[] VoiceData { get; set; }


  public int SequenceBytes { get; set; }


  public uint SectionNumber { get; set; }


  public uint SampleRate { get; set; }


  public uint UncompressedSampleOffset { get; set; }


  public uint NumPackets { get; set; }


  public IProtobufRepeatedFieldValueType<uint> PacketOffsets { get; }


  public float VoiceLevel { get; set; }

}
