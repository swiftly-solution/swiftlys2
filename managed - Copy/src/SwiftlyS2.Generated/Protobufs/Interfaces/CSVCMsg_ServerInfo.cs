
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CSVCMsg_ServerInfo : ITypedProtobuf<CSVCMsg_ServerInfo>, INetMessage<CSVCMsg_ServerInfo>, IDisposable
{
  static int INetMessage<CSVCMsg_ServerInfo>.MessageId => 40;
  
  static string INetMessage<CSVCMsg_ServerInfo>.MessageName => "CSVCMsg_ServerInfo";

  static CSVCMsg_ServerInfo ITypedProtobuf<CSVCMsg_ServerInfo>.Wrap(nint handle, bool isManuallyAllocated) => new CSVCMsg_ServerInfoImpl(handle, isManuallyAllocated);


  public int Protocol { get; set; }


  public int ServerCount { get; set; }


  public bool IsDedicated { get; set; }


  public bool IsHltv { get; set; }


  public int COs { get; set; }


  public int MaxClients { get; set; }


  public int MaxClasses { get; set; }


  public int PlayerSlot { get; set; }


  public float TickInterval { get; set; }


  public string GameDir { get; set; }


  public string MapName { get; set; }


  public string SkyName { get; set; }


  public string HostName { get; set; }


  public string AddonName { get; set; }


  public CSVCMsg_GameSessionConfiguration GameSessionConfig { get; }


  public byte[] GameSessionManifest { get; set; }

}
