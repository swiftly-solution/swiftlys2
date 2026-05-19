using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPackageReservationStatus : ITypedProtobuf<CPackageReservationStatus>
{
    static CPackageReservationStatus ITypedProtobuf<CPackageReservationStatus>.Wrap(nint handle, bool isManuallyAllocated) => new CPackageReservationStatusImpl(handle, isManuallyAllocated);

    public uint Packageid { get; set; }
    public int ReservationState { get; set; }
    public int QueuePosition { get; set; }
    public int TotalQueueSize { get; set; }
    public string ReservationCountryCode { get; set; }
    public bool Expired { get; set; }
    public uint TimeExpires { get; set; }
    public uint TimeReserved { get; set; }
}