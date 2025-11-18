using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.SchemaDefinitions;

internal partial class CBaseModelEntityImpl : CBaseModelEntity
{
    public void SetModel(string model)
    {
        GameFunctions.SetModel(Address, model);
    }

    public void SetBodygroupByName(string group, int value)
    {
        AcceptInput("SetBodygroup", $"{group},{value}");
    }

    public void SetScale(float scale)
    {
        var skeletonInstance = CBodyComponent?.SceneNode?.GetSkeletonInstance();
        if (skeletonInstance == null) return;

        skeletonInstance.Scale = scale;
        AcceptInput("SetScale", scale);
        CBodyComponentUpdated();
    }
}