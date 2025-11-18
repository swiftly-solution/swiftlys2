using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.SchemaDefinitions;

internal partial class CGameSceneNodeImpl : CGameSceneNode
{
    public CSkeletonInstance GetSkeletonInstance()
    {
        return new CSkeletonInstanceImpl(GameFunctions.GetSkeletonInstance(Address));
    }
}