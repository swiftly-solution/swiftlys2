using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCloud_UserFileImpl : TypedProtobuf<CCloud_UserFile>, CCloud_UserFile
{
    public CCloud_UserFileImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint Appid
    { get => Accessor.GetUInt32("appid"); set => Accessor.SetUInt32("appid", value); }
    public ulong Ugcid
    { get => Accessor.GetUInt64("ugcid"); set => Accessor.SetUInt64("ugcid", value); }
    public string Filename
    { get => Accessor.GetString("filename"); set => Accessor.SetString("filename", value); }
    public ulong Timestamp
    { get => Accessor.GetUInt64("timestamp"); set => Accessor.SetUInt64("timestamp", value); }
    public uint FileSize
    { get => Accessor.GetUInt32("file_size"); set => Accessor.SetUInt32("file_size", value); }
    public string Url
    { get => Accessor.GetString("url"); set => Accessor.SetString("url", value); }
    public ulong SteamidCreator
    { get => Accessor.GetUInt64("steamid_creator"); set => Accessor.SetUInt64("steamid_creator", value); }
}