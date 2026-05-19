using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSource2PlayStatsPackedRecordListImpl : TypedProtobuf<CMsgSource2PlayStatsPackedRecordList>, CMsgSource2PlayStatsPackedRecordList
{
    public CMsgSource2PlayStatsPackedRecordListImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public string RecordName
    { get => Accessor.GetString("record_name"); set => Accessor.SetString("record_name", value); }
    public IProtobufRepeatedFieldSubMessageType<CMsgSource2PlayStatsPackedRecordList_FieldDef> FieldDefs
    { get => new ProtobufRepeatedFieldSubMessageType<CMsgSource2PlayStatsPackedRecordList_FieldDef>(Accessor, "field_defs"); }
    public uint RecordCount
    { get => Accessor.GetUInt32("record_count"); set => Accessor.SetUInt32("record_count", value); }
    public IProtobufRepeatedFieldValueType<ulong> Uint64Vals
    { get => new ProtobufRepeatedFieldValueType<ulong>(Accessor, "uint64_vals"); }
    public IProtobufRepeatedFieldValueType<uint> Uint32Vals
    { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "uint32_vals"); }
    public IProtobufRepeatedFieldValueType<uint> Uint16Vals
    { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "uint16_vals"); }
    public IProtobufRepeatedFieldValueType<uint> Uint8Vals
    { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "uint8_vals"); }
    public IProtobufRepeatedFieldValueType<long> Int64Vals
    { get => new ProtobufRepeatedFieldValueType<long>(Accessor, "int64_vals"); }
    public IProtobufRepeatedFieldValueType<int> Int32Vals
    { get => new ProtobufRepeatedFieldValueType<int>(Accessor, "int32_vals"); }
    public IProtobufRepeatedFieldValueType<int> Int16Vals
    { get => new ProtobufRepeatedFieldValueType<int>(Accessor, "int16_vals"); }
    public IProtobufRepeatedFieldValueType<int> Int8Vals
    { get => new ProtobufRepeatedFieldValueType<int>(Accessor, "int8_vals"); }
    public IProtobufRepeatedFieldValueType<double> Float64Vals
    { get => new ProtobufRepeatedFieldValueType<double>(Accessor, "float64_vals"); }
    public IProtobufRepeatedFieldValueType<float> Float32Vals
    { get => new ProtobufRepeatedFieldValueType<float>(Accessor, "float32_vals"); }
    public IProtobufRepeatedFieldValueType<bool> BoolVals
    { get => new ProtobufRepeatedFieldValueType<bool>(Accessor, "bool_vals"); }
    public IProtobufRepeatedFieldValueType<string> StringVals
    { get => new ProtobufRepeatedFieldValueType<string>(Accessor, "string_vals"); }
    public IProtobufRepeatedFieldValueType<string> LowCardinalityStringVals
    { get => new ProtobufRepeatedFieldValueType<string>(Accessor, "low_cardinality_string_vals"); }
    public IProtobufRepeatedFieldValueType<uint> UtcdatetimeVals
    { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "utcdatetime_vals"); }
    public IProtobufRepeatedFieldValueType<ulong> SteamidtrustbucketVals
    { get => new ProtobufRepeatedFieldValueType<ulong>(Accessor, "steamidtrustbucket_vals"); }
    public IProtobufRepeatedFieldSubMessageType<CMsgSource2PlayStatsPackedRecordList_SteamIDList> TrustbucketVals
    { get => new ProtobufRepeatedFieldSubMessageType<CMsgSource2PlayStatsPackedRecordList_SteamIDList>(Accessor, "trustbucket_vals"); }
    public IProtobufRepeatedFieldValueType<ulong> SteamidVals
    { get => new ProtobufRepeatedFieldValueType<ulong>(Accessor, "steamid_vals"); }
}