
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CNETMsg_SpawnGroup_LoadImpl : NetMessage<CNETMsg_SpawnGroup_Load>, CNETMsg_SpawnGroup_Load
{
  public CNETMsg_SpawnGroup_LoadImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public string Worldname
  { get => Accessor.GetString("worldname"); set => Accessor.SetString("worldname", value); }


  public string Entitylumpname
  { get => Accessor.GetString("entitylumpname"); set => Accessor.SetString("entitylumpname", value); }


  public string Entityfiltername
  { get => Accessor.GetString("entityfiltername"); set => Accessor.SetString("entityfiltername", value); }


  public uint Spawngrouphandle
  { get => Accessor.GetUInt32("spawngrouphandle"); set => Accessor.SetUInt32("spawngrouphandle", value); }


  public uint Spawngroupownerhandle
  { get => Accessor.GetUInt32("spawngroupownerhandle"); set => Accessor.SetUInt32("spawngroupownerhandle", value); }


  public Vector WorldOffsetPos
  { get => Accessor.GetVector("world_offset_pos"); set => Accessor.SetVector("world_offset_pos", value); }


  public QAngle WorldOffsetAngle
  { get => Accessor.GetQAngle("world_offset_angle"); set => Accessor.SetQAngle("world_offset_angle", value); }


  public byte[] Spawngroupmanifest
  { get => Accessor.GetBytes("spawngroupmanifest"); set => Accessor.SetBytes("spawngroupmanifest", value); }


  public uint Flags
  { get => Accessor.GetUInt32("flags"); set => Accessor.SetUInt32("flags", value); }


  public int Tickcount
  { get => Accessor.GetInt32("tickcount"); set => Accessor.SetInt32("tickcount", value); }


  public bool Manifestincomplete
  { get => Accessor.GetBool("manifestincomplete"); set => Accessor.SetBool("manifestincomplete", value); }


  public string Localnamefixup
  { get => Accessor.GetString("localnamefixup"); set => Accessor.SetString("localnamefixup", value); }


  public string Parentnamefixup
  { get => Accessor.GetString("parentnamefixup"); set => Accessor.SetString("parentnamefixup", value); }


  public int Manifestloadpriority
  { get => Accessor.GetInt32("manifestloadpriority"); set => Accessor.SetInt32("manifestloadpriority", value); }


  public uint Worldgroupid
  { get => Accessor.GetUInt32("worldgroupid"); set => Accessor.SetUInt32("worldgroupid", value); }


  public uint Creationsequence
  { get => Accessor.GetUInt32("creationsequence"); set => Accessor.SetUInt32("creationsequence", value); }


  public string Savegamefilename
  { get => Accessor.GetString("savegamefilename"); set => Accessor.SetString("savegamefilename", value); }


  public uint Spawngroupparenthandle
  { get => Accessor.GetUInt32("spawngroupparenthandle"); set => Accessor.SetUInt32("spawngroupparenthandle", value); }


  public bool Leveltransition
  { get => Accessor.GetBool("leveltransition"); set => Accessor.SetBool("leveltransition", value); }


  public string Worldgroupname
  { get => Accessor.GetString("worldgroupname"); set => Accessor.SetString("worldgroupname", value); }

}
