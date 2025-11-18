
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCLCMsg_ClientInfo : ITypedProtobuf<CCLCMsg_ClientInfo>, INetMessage<CCLCMsg_ClientInfo>, IDisposable
{
  static int INetMessage<CCLCMsg_ClientInfo>.MessageId => 20;
  
  static string INetMessage<CCLCMsg_ClientInfo>.MessageName => "CCLCMsg_ClientInfo";

  static CCLCMsg_ClientInfo ITypedProtobuf<CCLCMsg_ClientInfo>.Wrap(nint handle, bool isManuallyAllocated) => new CCLCMsg_ClientInfoImpl(handle, isManuallyAllocated);


  public uint SendTableCrc { get; set; }


  public uint ServerCount { get; set; }


  public bool IsHltv { get; set; }


  public uint FriendsId { get; set; }


  public string FriendsName { get; set; }

}
