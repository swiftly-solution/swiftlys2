using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMessageSendAudioImpl : NetMessage<CUserMessageSendAudio>, CUserMessageSendAudio
{
    public CUserMessageSendAudioImpl( nint handle, bool isManuallyAllocated ) : base(handle, isManuallyAllocated)
    {
    }


    public string Soundname { get => Accessor.GetString("soundname"); set => Accessor.SetString("soundname", value); }


    public bool Stop { get => Accessor.GetBool("stop"); set => Accessor.SetBool("stop", value); }

}
