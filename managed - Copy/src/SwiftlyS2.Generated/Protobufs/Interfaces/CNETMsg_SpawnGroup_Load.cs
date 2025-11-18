
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CNETMsg_SpawnGroup_Load : ITypedProtobuf<CNETMsg_SpawnGroup_Load>, INetMessage<CNETMsg_SpawnGroup_Load>, IDisposable
{
  static int INetMessage<CNETMsg_SpawnGroup_Load>.MessageId => 8;
  
  static string INetMessage<CNETMsg_SpawnGroup_Load>.MessageName => "CNETMsg_SpawnGroup_Load";

  static CNETMsg_SpawnGroup_Load ITypedProtobuf<CNETMsg_SpawnGroup_Load>.Wrap(nint handle, bool isManuallyAllocated) => new CNETMsg_SpawnGroup_LoadImpl(handle, isManuallyAllocated);


  public string Worldname { get; set; }


  public string Entitylumpname { get; set; }


  public string Entityfiltername { get; set; }


  public uint Spawngrouphandle { get; set; }


  public uint Spawngroupownerhandle { get; set; }


  public Vector WorldOffsetPos { get; set; }


  public QAngle WorldOffsetAngle { get; set; }


  public byte[] Spawngroupmanifest { get; set; }


  public uint Flags { get; set; }


  public int Tickcount { get; set; }


  public bool Manifestincomplete { get; set; }


  public string Localnamefixup { get; set; }


  public string Parentnamefixup { get; set; }


  public int Manifestloadpriority { get; set; }


  public uint Worldgroupid { get; set; }


  public uint Creationsequence { get; set; }


  public string Savegamefilename { get; set; }


  public uint Spawngroupparenthandle { get; set; }


  public bool Leveltransition { get; set; }


  public string Worldgroupname { get; set; }

}
