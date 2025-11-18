
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CDemoFileHeader : ITypedProtobuf<CDemoFileHeader>
{
  static CDemoFileHeader ITypedProtobuf<CDemoFileHeader>.Wrap(nint handle, bool isManuallyAllocated) => new CDemoFileHeaderImpl(handle, isManuallyAllocated);


  public string DemoFileStamp { get; set; }


  public int PatchVersion { get; set; }


  public string ServerName { get; set; }


  public string ClientName { get; set; }


  public string MapName { get; set; }


  public string GameDirectory { get; set; }


  public int FullpacketsVersion { get; set; }


  public bool AllowClientsideEntities { get; set; }


  public bool AllowClientsideParticles { get; set; }


  public string Addons { get; set; }


  public string DemoVersionName { get; set; }


  public string DemoVersionGuid { get; set; }


  public int BuildNum { get; set; }


  public string Game { get; set; }


  public int ServerStartTick { get; set; }

}
