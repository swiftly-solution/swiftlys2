using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgAuthTicket : ITypedProtobuf<CMsgAuthTicket>
{
    static CMsgAuthTicket ITypedProtobuf<CMsgAuthTicket>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgAuthTicketImpl(handle, isManuallyAllocated);

    public uint Estate { get; set; }
    public uint Eresult { get; set; }
    public ulong Steamid { get; set; }
    public ulong Gameid { get; set; }
    public uint HSteamPipe { get; set; }
    public uint TicketCrc { get; set; }
    public byte[] Ticket { get; set; }
}