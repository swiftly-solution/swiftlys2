
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CSOEconDefaultEquippedDefinitionInstanceClient : ITypedProtobuf<CSOEconDefaultEquippedDefinitionInstanceClient>
{
  static CSOEconDefaultEquippedDefinitionInstanceClient ITypedProtobuf<CSOEconDefaultEquippedDefinitionInstanceClient>.Wrap(nint handle, bool isManuallyAllocated) => new CSOEconDefaultEquippedDefinitionInstanceClientImpl(handle, isManuallyAllocated);


  public uint AccountId { get; set; }


  public uint ItemDefinition { get; set; }


  public uint ClassId { get; set; }


  public uint SlotId { get; set; }

}
