
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CSVCMsg_GameSessionConfiguration : ITypedProtobuf<CSVCMsg_GameSessionConfiguration>
{
  static CSVCMsg_GameSessionConfiguration ITypedProtobuf<CSVCMsg_GameSessionConfiguration>.Wrap(nint handle, bool isManuallyAllocated) => new CSVCMsg_GameSessionConfigurationImpl(handle, isManuallyAllocated);


  public bool IsMultiplayer { get; set; }


  public bool IsLoadsavegame { get; set; }


  public bool IsBackgroundMap { get; set; }


  public bool IsHeadless { get; set; }


  public uint MinClientLimit { get; set; }


  public uint MaxClientLimit { get; set; }


  public uint MaxClients { get; set; }


  public uint TickInterval { get; set; }


  public string Hostname { get; set; }


  public string Savegamename { get; set; }


  public string S1Mapname { get; set; }


  public string Gamemode { get; set; }


  public string ServerIpAddress { get; set; }


  public byte[] Data { get; set; }


  public bool IsLocalonly { get; set; }


  public bool NoSteamServer { get; set; }


  public bool IsTransition { get; set; }


  public string Previouslevel { get; set; }


  public string Landmarkname { get; set; }

}
