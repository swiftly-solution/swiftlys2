
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSVCMsg_Sounds_sounddata_tImpl : TypedProtobuf<CSVCMsg_Sounds_sounddata_t>, CSVCMsg_Sounds_sounddata_t
{
  public CSVCMsg_Sounds_sounddata_tImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int OriginX
  { get => Accessor.GetInt32("origin_x"); set => Accessor.SetInt32("origin_x", value); }


  public int OriginY
  { get => Accessor.GetInt32("origin_y"); set => Accessor.SetInt32("origin_y", value); }


  public int OriginZ
  { get => Accessor.GetInt32("origin_z"); set => Accessor.SetInt32("origin_z", value); }


  public uint Volume
  { get => Accessor.GetUInt32("volume"); set => Accessor.SetUInt32("volume", value); }


  public float DelayValue
  { get => Accessor.GetFloat("delay_value"); set => Accessor.SetFloat("delay_value", value); }


  public int SequenceNumber
  { get => Accessor.GetInt32("sequence_number"); set => Accessor.SetInt32("sequence_number", value); }


  public int EntityIndex
  { get => Accessor.GetInt32("entity_index"); set => Accessor.SetInt32("entity_index", value); }


  public int Channel
  { get => Accessor.GetInt32("channel"); set => Accessor.SetInt32("channel", value); }


  public int Pitch
  { get => Accessor.GetInt32("pitch"); set => Accessor.SetInt32("pitch", value); }


  public int Flags
  { get => Accessor.GetInt32("flags"); set => Accessor.SetInt32("flags", value); }


  public uint SoundNum
  { get => Accessor.GetUInt32("sound_num"); set => Accessor.SetUInt32("sound_num", value); }


  public uint SoundNumHandle
  { get => Accessor.GetUInt32("sound_num_handle"); set => Accessor.SetUInt32("sound_num_handle", value); }


  public int SpeakerEntity
  { get => Accessor.GetInt32("speaker_entity"); set => Accessor.SetInt32("speaker_entity", value); }


  public int RandomSeed
  { get => Accessor.GetInt32("random_seed"); set => Accessor.SetInt32("random_seed", value); }


  public int SoundLevel
  { get => Accessor.GetInt32("sound_level"); set => Accessor.SetInt32("sound_level", value); }


  public bool IsSentence
  { get => Accessor.GetBool("is_sentence"); set => Accessor.SetBool("is_sentence", value); }


  public bool IsAmbient
  { get => Accessor.GetBool("is_ambient"); set => Accessor.SetBool("is_ambient", value); }


  public uint Guid
  { get => Accessor.GetUInt32("guid"); set => Accessor.SetUInt32("guid", value); }


  public ulong SoundResourceId
  { get => Accessor.GetUInt64("sound_resource_id"); set => Accessor.SetUInt64("sound_resource_id", value); }

}
