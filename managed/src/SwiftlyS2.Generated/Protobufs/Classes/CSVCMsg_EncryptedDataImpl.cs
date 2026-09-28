using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSVCMsg_EncryptedDataImpl : NetMessage<CSVCMsg_EncryptedData>, CSVCMsg_EncryptedData
{
    public CSVCMsg_EncryptedDataImpl(nint handle, bool isManuallyAllocated) : base(handle, isManuallyAllocated)
    {
    }

    public byte[] Encrypted
    { get => Accessor.GetBytes("encrypted"); set => Accessor.SetBytes("encrypted", value); }
    public int KeyType
    { get => Accessor.GetInt32("key_type"); set => Accessor.SetInt32("key_type", value); }
}