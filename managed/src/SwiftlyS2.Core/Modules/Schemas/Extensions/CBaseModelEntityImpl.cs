using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Scheduler;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.SchemaDefinitions;

internal partial class CBaseModelEntityImpl : CBaseModelEntity
{
    public CSkeletonInstance? GetSkeletonInstance()
    {
        return CBodyComponent?.SceneNode?.GetSkeletonInstance();
    }

    public string? GetModel()
    {
        return GetSkeletonInstance()?.ModelState.ModelName;
    }

    public void SetModel(string model)
    {
        NativeBinding.ThrowIfNonMainThread();
        GameFunctions.SetModel(Address, model);
    }

    public Task SetModelAsync(string model)
    {
        return SchedulerManager.QueueOrNow(() => SetModel(model));
    }

    public void SetBodygroupByName(string group, int value)
    {
        NativeBinding.ThrowIfNonMainThread();
        AcceptInput("SetBodygroup", $"{group},{value}");
    }

    public Task SetBodygroupByNameAsync(string group, int value)
    {
        return SchedulerManager.QueueOrNow(() => SetBodygroupByName(group, value));
    }

    public ulong? GetMeshGroupMask()
    {
        return GetSkeletonInstance()?.ModelState.MeshGroupMask;
    }

    public void SetMeshGroupMask(ulong meshGroupMask)
    {
        var skeletonInstance = GetSkeletonInstance();
        if (skeletonInstance == null) return;

        skeletonInstance.ModelState.MeshGroupMask = meshGroupMask;
        CBodyComponentUpdated();
    }

    public void SetScale(float scale)
    {
        var skeletonInstance = GetSkeletonInstance();
        if (skeletonInstance == null) return;

        skeletonInstance.Scale = scale;
        AcceptInput("SetScale", scale);
        CBodyComponentUpdated();
    }

    public Task SetScaleAsync(float scale)
    {
        return SchedulerManager.QueueOrNow(() => SetScale(scale));
    }

    public void ChangeSubclass(ushort itemDefinitionIndex)
    {
        AcceptInput("ChangeSubclass", itemDefinitionIndex.ToString());
    }

    public Task ChangeSubclassAsync(ushort itemDefinitionIndex)
    {
        return SchedulerManager.QueueOrNow(() => ChangeSubclass(itemDefinitionIndex));
    }
}