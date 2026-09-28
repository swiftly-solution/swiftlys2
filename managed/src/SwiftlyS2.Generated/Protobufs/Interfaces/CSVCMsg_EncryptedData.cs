using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CSVCMsg_EncryptedData : ITypedProtobuf<CSVCMsg_EncryptedData>, INetMessage<CSVCMsg_EncryptedData>, IDisposable
{
    static int INetMessage<CSVCMsg_EncryptedData>.MessageId => 78;

    static string INetMessage<CSVCMsg_EncryptedData>.MessageName => "CSVCMsg_EncryptedData";

    static CSVCMsg_EncryptedData ITypedProtobuf<CSVCMsg_EncryptedData>.Wrap(nint handle, bool isManuallyAllocated) => new CSVCMsg_EncryptedDataImpl(handle, isManuallyAllocated);

    public byte[] Encrypted { get; set; }
    public int KeyType { get; set; }
}