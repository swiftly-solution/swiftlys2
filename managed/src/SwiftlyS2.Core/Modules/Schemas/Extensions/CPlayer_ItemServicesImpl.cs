using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Scheduler;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.Schemas;

namespace SwiftlyS2.Core.SchemaDefinitions;

internal partial class CPlayer_ItemServicesImpl
{
    public T GiveItem<T>() where T : ISchemaClass<T>
    {
        NativeBinding.ThrowIfNonMainThread();
        var name = T.ClassName;
        return name == null
            ? throw new ArgumentException($"Can't give item with class {typeof(T).Name}, which doesn't have a designer name.")
            : T.From(GameFunctions.CCSPlayer_ItemServices_GiveNamedItem(Address, name));
    }

    public Task<T> GiveItemAsync<T>() where T : ISchemaClass<T>
    {
        return SchedulerManager.QueueOrNow(() => GiveItem<T>());
    }

    public T GiveItem<T>( string itemDesignerName ) where T : ISchemaClass<T>
    {
        NativeBinding.ThrowIfNonMainThread();
        return T.From(GameFunctions.CCSPlayer_ItemServices_GiveNamedItem(Address, itemDesignerName));
    }

    public Task<T> GiveItemAsync<T>( string itemDesignerName ) where T : ISchemaClass<T>
    {
        return SchedulerManager.QueueOrNow(() => GiveItem<T>(itemDesignerName));
    }

    public void GiveItem( string itemDesignerName )
    {
        NativeBinding.ThrowIfNonMainThread();
        _ = GameFunctions.CCSPlayer_ItemServices_GiveNamedItem(Address, itemDesignerName);
    }

    public Task GiveItemAsync( string itemDesignerName )
    {
        return SchedulerManager.QueueOrNow(() => GiveItem(itemDesignerName));
    }

    public void RemoveItems()
    {
        NativeBinding.ThrowIfNonMainThread();
        GameFunctions.CCSPlayer_ItemServices_RemoveWeapons(Address);
    }

    public Task RemoveItemsAsync()
    {
        return SchedulerManager.QueueOrNow(() => RemoveItems());
    }

    public void DropActiveItem()
    {
        NativeBinding.ThrowIfNonMainThread();
        GameFunctions.CCSPlayer_ItemServices_DropActiveItem(Address, Vector.Zero);
    }

    public Task DropActiveItemAsync()
    {
        return SchedulerManager.QueueOrNow(() => DropActiveItem());
    }
}