using SwiftlyS2.Shared.Events;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.Events;

internal class OnEntityFireOutputHookEvent : IOnEntityFireOutputHookEvent
{
    public unsafe CEntityIOOutput* _entityIO;
    public ref CEntityIOOutput EntityIO {
        get {
            unsafe
            {
                return ref *_entityIO;
            }
        }
    }
    public required string DesignerName { get; init; }
    public required string OutputName { get; init; }
    public CEntityInstance? Activator { get; init; }
    public CEntityInstance? Caller { get; init; }

    public unsafe CVariant<CVariantDefaultAllocator>* _variant;
    public ref CVariant<CVariantDefaultAllocator> VariantValue {
        get {
            unsafe
            {
                return ref *_variant;
            }
        }
    }
    public float Delay { get; init; }
    public HookResult Result { get; set; }
}