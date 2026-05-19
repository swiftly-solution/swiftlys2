using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgAuthTicketImpl : TypedProtobuf<CMsgAuthTicket>, CMsgAuthTicket
{
    public CMsgAuthTicketImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint Estate
    { get => Accessor.GetUInt32("estate"); set => Accessor.SetUInt32("estate", value); }
    public uint Eresult
    { get => Accessor.GetUInt32("eresult"); set => Accessor.SetUInt32("eresult", value); }
    public ulong Steamid
    { get => Accessor.GetUInt64("steamid"); set => Accessor.SetUInt64("steamid", value); }
    public ulong Gameid
    { get => Accessor.GetUInt64("gameid"); set => Accessor.SetUInt64("gameid", value); }
    public uint HSteamPipe
    { get => Accessor.GetUInt32("h_steam_pipe"); set => Accessor.SetUInt32("h_steam_pipe", value); }
    public uint TicketCrc
    { get => Accessor.GetUInt32("ticket_crc"); set => Accessor.SetUInt32("ticket_crc", value); }
    public byte[] Ticket
    { get => Accessor.GetBytes("ticket"); set => Accessor.SetBytes("ticket", value); }
}