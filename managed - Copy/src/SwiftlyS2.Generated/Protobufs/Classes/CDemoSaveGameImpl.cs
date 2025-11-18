
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CDemoSaveGameImpl : TypedProtobuf<CDemoSaveGame>, CDemoSaveGame
{
  public CDemoSaveGameImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public byte[] Data
  { get => Accessor.GetBytes("data"); set => Accessor.SetBytes("data", value); }


  public ulong SteamId
  { get => Accessor.GetUInt64("steam_id"); set => Accessor.SetUInt64("steam_id", value); }


  public ulong Signature
  { get => Accessor.GetUInt64("signature"); set => Accessor.SetUInt64("signature", value); }


  public int Version
  { get => Accessor.GetInt32("version"); set => Accessor.SetInt32("version", value); }

}
