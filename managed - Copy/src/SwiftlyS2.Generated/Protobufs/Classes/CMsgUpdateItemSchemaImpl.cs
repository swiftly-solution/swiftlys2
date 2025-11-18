
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgUpdateItemSchemaImpl : TypedProtobuf<CMsgUpdateItemSchema>, CMsgUpdateItemSchema
{
  public CMsgUpdateItemSchemaImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public byte[] ItemsGame
  { get => Accessor.GetBytes("items_game"); set => Accessor.SetBytes("items_game", value); }


  public uint ItemSchemaVersion
  { get => Accessor.GetUInt32("item_schema_version"); set => Accessor.SetUInt32("item_schema_version", value); }


  public string ItemsGameUrl
  { get => Accessor.GetString("items_game_url"); set => Accessor.SetString("items_game_url", value); }

}
