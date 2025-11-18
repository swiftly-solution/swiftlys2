
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSVCMsg_BSPDecalImpl : NetMessage<CSVCMsg_BSPDecal>, CSVCMsg_BSPDecal
{
  public CSVCMsg_BSPDecalImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public Vector Pos
  { get => Accessor.GetVector("pos"); set => Accessor.SetVector("pos", value); }


  public int DecalTextureIndex
  { get => Accessor.GetInt32("decal_texture_index"); set => Accessor.SetInt32("decal_texture_index", value); }


  public int EntityIndex
  { get => Accessor.GetInt32("entity_index"); set => Accessor.SetInt32("entity_index", value); }


  public int ModelIndex
  { get => Accessor.GetInt32("model_index"); set => Accessor.SetInt32("model_index", value); }


  public bool LowPriority
  { get => Accessor.GetBool("low_priority"); set => Accessor.SetBool("low_priority", value); }

}
