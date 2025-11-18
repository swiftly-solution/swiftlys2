
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CNETMsg_SetConVar : ITypedProtobuf<CNETMsg_SetConVar>, INetMessage<CNETMsg_SetConVar>, IDisposable
{
  static int INetMessage<CNETMsg_SetConVar>.MessageId => 6;
  
  static string INetMessage<CNETMsg_SetConVar>.MessageName => "CNETMsg_SetConVar";

  static CNETMsg_SetConVar ITypedProtobuf<CNETMsg_SetConVar>.Wrap(nint handle, bool isManuallyAllocated) => new CNETMsg_SetConVarImpl(handle, isManuallyAllocated);


  public CMsg_CVars Convars { get; }

}
