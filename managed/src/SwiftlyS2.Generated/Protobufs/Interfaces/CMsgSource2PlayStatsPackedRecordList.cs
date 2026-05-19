using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSource2PlayStatsPackedRecordList : ITypedProtobuf<CMsgSource2PlayStatsPackedRecordList>
{
    static CMsgSource2PlayStatsPackedRecordList ITypedProtobuf<CMsgSource2PlayStatsPackedRecordList>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSource2PlayStatsPackedRecordListImpl(handle, isManuallyAllocated);

    public string RecordName { get; set; }
    public IProtobufRepeatedFieldSubMessageType<CMsgSource2PlayStatsPackedRecordList_FieldDef> FieldDefs { get; }
    public uint RecordCount { get; set; }
    public IProtobufRepeatedFieldValueType<ulong> Uint64Vals { get; }
    public IProtobufRepeatedFieldValueType<uint> Uint32Vals { get; }
    public IProtobufRepeatedFieldValueType<uint> Uint16Vals { get; }
    public IProtobufRepeatedFieldValueType<uint> Uint8Vals { get; }
    public IProtobufRepeatedFieldValueType<long> Int64Vals { get; }
    public IProtobufRepeatedFieldValueType<int> Int32Vals { get; }
    public IProtobufRepeatedFieldValueType<int> Int16Vals { get; }
    public IProtobufRepeatedFieldValueType<int> Int8Vals { get; }
    public IProtobufRepeatedFieldValueType<double> Float64Vals { get; }
    public IProtobufRepeatedFieldValueType<float> Float32Vals { get; }
    public IProtobufRepeatedFieldValueType<bool> BoolVals { get; }
    public IProtobufRepeatedFieldValueType<string> StringVals { get; }
    public IProtobufRepeatedFieldValueType<string> LowCardinalityStringVals { get; }
    public IProtobufRepeatedFieldValueType<uint> UtcdatetimeVals { get; }
    public IProtobufRepeatedFieldValueType<ulong> SteamidtrustbucketVals { get; }
    public IProtobufRepeatedFieldSubMessageType<CMsgSource2PlayStatsPackedRecordList_SteamIDList> TrustbucketVals { get; }
    public IProtobufRepeatedFieldValueType<ulong> SteamidVals { get; }
}