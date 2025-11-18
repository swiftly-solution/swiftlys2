namespace SwiftlyS2.Shared.SchemaDefinitions;

public partial interface CGameSceneNode
{
    /// <summary>
    /// Gets the Skeleton Instance from the node.
    /// </summary>
    public CSkeletonInstance GetSkeletonInstance();
}