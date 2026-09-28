using SwiftlyS2.Core.EntitySystem;
using SwiftlyS2.Core.SchemaDefinitions;
using SwiftlyS2.Shared.Events;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.Events;

internal class OnCustomHudClickedEvent : IOnCustomHudClickedEvent
{
    public required CCSUsrMsg_CustomHudClicked Message { get; set; }
    public required int PlayerId { get; set; }

    public bool IsValid => CHandle<CCSCustomHudLayout>.FromPackedInt((int)Message.CustomHudLayout).IsValid;

    public CCSCustomHudLayout CustomHudLayout {
        get {
            var handle = CHandle<CCSCustomHudLayout>.FromPackedInt((int)Message.CustomHudLayout);
            return handle.Value!;
        }
    }

    public string ButtonId => Message.ButtonId;

}