namespace SwiftlyS2.Shared.SchemaDefinitions;

public partial interface CAttributeList
{
    /// <summary>
    /// Sets or adds an attribute to the attribute list.
    /// </summary>
    public void SetOrAddAttribute(string attributeName, float value);
}