
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class MLDemoHeaderImpl : TypedProtobuf<MLDemoHeader>, MLDemoHeader
{
  public MLDemoHeaderImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public string MapName
  { get => Accessor.GetString("map_name"); set => Accessor.SetString("map_name", value); }


  public int TickRate
  { get => Accessor.GetInt32("tick_rate"); set => Accessor.SetInt32("tick_rate", value); }


  public uint Version
  { get => Accessor.GetUInt32("version"); set => Accessor.SetUInt32("version", value); }


  public uint SteamUniverse
  { get => Accessor.GetUInt32("steam_universe"); set => Accessor.SetUInt32("steam_universe", value); }

}
