
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface C2S_CONNECT_Message : ITypedProtobuf<C2S_CONNECT_Message>
{
  static C2S_CONNECT_Message ITypedProtobuf<C2S_CONNECT_Message>.Wrap(nint handle, bool isManuallyAllocated) => new C2S_CONNECT_MessageImpl(handle, isManuallyAllocated);


  public uint HostVersion { get; set; }


  public uint AuthProtocol { get; set; }


  public uint ChallengeNumber { get; set; }


  public ulong ReservationCookie { get; set; }


  public bool LowViolence { get; set; }


  public byte[] EncryptedPassword { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CCLCMsg_SplitPlayerConnect> Splitplayers { get; }


  public byte[] AuthSteam { get; set; }


  public string ChallengeContext { get; set; }


  public C2S_CONNECT_SameProcessCheck LocalhostSameProcessCheck { get; }

}
