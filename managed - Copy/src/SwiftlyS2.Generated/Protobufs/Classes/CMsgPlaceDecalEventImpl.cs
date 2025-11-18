
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgPlaceDecalEventImpl : NetMessage<CMsgPlaceDecalEvent>, CMsgPlaceDecalEvent
{
  public CMsgPlaceDecalEventImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public Vector Position
  { get => Accessor.GetVector("position"); set => Accessor.SetVector("position", value); }


  public Vector Normal
  { get => Accessor.GetVector("normal"); set => Accessor.SetVector("normal", value); }


  public Vector Saxis
  { get => Accessor.GetVector("saxis"); set => Accessor.SetVector("saxis", value); }


  public int Boneindex
  { get => Accessor.GetInt32("boneindex"); set => Accessor.SetInt32("boneindex", value); }


  public int Triangleindex
  { get => Accessor.GetInt32("triangleindex"); set => Accessor.SetInt32("triangleindex", value); }


  public uint Flags
  { get => Accessor.GetUInt32("flags"); set => Accessor.SetUInt32("flags", value); }


  public uint Color
  { get => Accessor.GetUInt32("color"); set => Accessor.SetUInt32("color", value); }


  public int RandomSeed
  { get => Accessor.GetInt32("random_seed"); set => Accessor.SetInt32("random_seed", value); }


  public uint DecalGroupName
  { get => Accessor.GetUInt32("decal_group_name"); set => Accessor.SetUInt32("decal_group_name", value); }


  public float SizeOverride
  { get => Accessor.GetFloat("size_override"); set => Accessor.SetFloat("size_override", value); }


  public uint Entityhandle
  { get => Accessor.GetUInt32("entityhandle"); set => Accessor.SetUInt32("entityhandle", value); }


  public ulong MaterialId
  { get => Accessor.GetUInt64("material_id"); set => Accessor.SetUInt64("material_id", value); }


  public uint SequenceName
  { get => Accessor.GetUInt32("sequence_name"); set => Accessor.SetUInt32("sequence_name", value); }

}
