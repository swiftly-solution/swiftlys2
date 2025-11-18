
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgVoiceAudioImpl : TypedProtobuf<CMsgVoiceAudio>, CMsgVoiceAudio
{
  public CMsgVoiceAudioImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public VoiceDataFormat_t Format
  { get => (VoiceDataFormat_t)Accessor.GetInt32("format"); set => Accessor.SetInt32("format", (int)value); }


  public byte[] VoiceData
  { get => Accessor.GetBytes("voice_data"); set => Accessor.SetBytes("voice_data", value); }


  public int SequenceBytes
  { get => Accessor.GetInt32("sequence_bytes"); set => Accessor.SetInt32("sequence_bytes", value); }


  public uint SectionNumber
  { get => Accessor.GetUInt32("section_number"); set => Accessor.SetUInt32("section_number", value); }


  public uint SampleRate
  { get => Accessor.GetUInt32("sample_rate"); set => Accessor.SetUInt32("sample_rate", value); }


  public uint UncompressedSampleOffset
  { get => Accessor.GetUInt32("uncompressed_sample_offset"); set => Accessor.SetUInt32("uncompressed_sample_offset", value); }


  public uint NumPackets
  { get => Accessor.GetUInt32("num_packets"); set => Accessor.SetUInt32("num_packets", value); }


  public IProtobufRepeatedFieldValueType<uint> PacketOffsets
  { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "packet_offsets"); }


  public float VoiceLevel
  { get => Accessor.GetFloat("voice_level"); set => Accessor.SetFloat("voice_level", value); }

}
