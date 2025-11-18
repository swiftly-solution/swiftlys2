
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CNETMsg_Tick : ITypedProtobuf<CNETMsg_Tick>, INetMessage<CNETMsg_Tick>, IDisposable
{
  static int INetMessage<CNETMsg_Tick>.MessageId => 4;
  
  static string INetMessage<CNETMsg_Tick>.MessageName => "CNETMsg_Tick";

  static CNETMsg_Tick ITypedProtobuf<CNETMsg_Tick>.Wrap(nint handle, bool isManuallyAllocated) => new CNETMsg_TickImpl(handle, isManuallyAllocated);


  public uint Tick { get; set; }


  public uint HostComputationtime { get; set; }


  public uint HostComputationtimeStdDeviation { get; set; }


  public uint LegacyHostLoss { get; set; }


  public uint HostUnfilteredFrametime { get; set; }


  public uint HltvReplayFlags { get; set; }


  public uint ExpectedLongTick { get; set; }


  public string ExpectedLongTickReason { get; set; }


  public uint HostFrameDroppedPctX10 { get; set; }


  public uint HostFrameIrregularArrivalPctX10 { get; set; }

}
