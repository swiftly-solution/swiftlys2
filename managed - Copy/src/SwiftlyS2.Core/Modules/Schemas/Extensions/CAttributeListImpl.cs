using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.SchemaDefinitions;

internal partial class CAttributeListImpl : CAttributeList
{
    public void SetOrAddAttribute(string attributeName, float value)
    {
        GameFunctions.SetOrAddAttribute(Address, attributeName, value);
    }
}