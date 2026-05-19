using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CPackageReservationStatusImpl : TypedProtobuf<CPackageReservationStatus>, CPackageReservationStatus
{
    public CPackageReservationStatusImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint Packageid
    { get => Accessor.GetUInt32("packageid"); set => Accessor.SetUInt32("packageid", value); }
    public int ReservationState
    { get => Accessor.GetInt32("reservation_state"); set => Accessor.SetInt32("reservation_state", value); }
    public int QueuePosition
    { get => Accessor.GetInt32("queue_position"); set => Accessor.SetInt32("queue_position", value); }
    public int TotalQueueSize
    { get => Accessor.GetInt32("total_queue_size"); set => Accessor.SetInt32("total_queue_size", value); }
    public string ReservationCountryCode
    { get => Accessor.GetString("reservation_country_code"); set => Accessor.SetString("reservation_country_code", value); }
    public bool Expired
    { get => Accessor.GetBool("expired"); set => Accessor.SetBool("expired", value); }
    public uint TimeExpires
    { get => Accessor.GetUInt32("time_expires"); set => Accessor.SetUInt32("time_expires", value); }
    public uint TimeReserved
    { get => Accessor.GetUInt32("time_reserved"); set => Accessor.SetUInt32("time_reserved", value); }
}