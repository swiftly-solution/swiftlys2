
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CSVCMsg_PacketEntities : ITypedProtobuf<CSVCMsg_PacketEntities>, INetMessage<CSVCMsg_PacketEntities>, IDisposable
{
  static int INetMessage<CSVCMsg_PacketEntities>.MessageId => 55;
  
  static string INetMessage<CSVCMsg_PacketEntities>.MessageName => "CSVCMsg_PacketEntities";

  static CSVCMsg_PacketEntities ITypedProtobuf<CSVCMsg_PacketEntities>.Wrap(nint handle, bool isManuallyAllocated) => new CSVCMsg_PacketEntitiesImpl(handle, isManuallyAllocated);


  public int MaxEntries { get; set; }


  public int UpdatedEntries { get; set; }


  public bool LegacyIsDelta { get; set; }


  public bool UpdateBaseline { get; set; }


  public int Baseline { get; set; }


  public int DeltaFrom { get; set; }


  public byte[] EntityData { get; set; }


  public bool PendingFullFrame { get; set; }


  public uint ActiveSpawngroupHandle { get; set; }


  public uint MaxSpawngroupCreationsequence { get; set; }


  public uint LastCmdNumberExecuted { get; set; }


  public int LastCmdNumberRecvDelta { get; set; }


  public uint ServerTick { get; set; }


  public byte[] SerializedEntities { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CSVCMsg_PacketEntities_alternate_baseline_t> AlternateBaselines { get; }


  public uint HasPvsVisBitsDeprecated { get; set; }


  public IProtobufRepeatedFieldValueType<int> CmdRecvStatus { get; }


  public CSVCMsg_PacketEntities_non_transmitted_entities_t NonTransmittedEntities { get; }


  public uint CqStarvedCommandTicks { get; set; }


  public uint CqDiscardedCommandTicks { get; set; }


  public CSVCMsg_PacketEntities_outofpvs_entity_updates_t OutofpvsEntityUpdates { get; }


  public byte[] DevPadding { get; set; }

}
