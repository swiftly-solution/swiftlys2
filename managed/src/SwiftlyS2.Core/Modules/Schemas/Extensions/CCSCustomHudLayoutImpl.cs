using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.SchemaDefinitions;

internal partial class CCSCustomHudLayoutImpl : CCSCustomHudLayout
{
    public void SetDialogVariableStringForPlayer( int playerId, string panelId, string variableName, string value )
    {
        NativeBinding.ThrowIfNonMainThread();
        GameFunctions.CCSCustomHudLayout_SetDialogVariableStringForPlayer(
            Address,
            playerId,
            panelId,
            variableName,
            value
        );
    }

    public void RemoveDialogVariableStringForPlayer( int playerId, string panelId, string variableName )
    {
        NativeBinding.ThrowIfNonMainThread();
        GameFunctions.CCSCustomHudLayout_RemoveDialogVariableStringForPlayer(
            Address,
            playerId,
            panelId,
            variableName
        );
    }

    public void SetDialogVariableString( string panelId, string variableName, string value )
    {
        NativeBinding.ThrowIfNonMainThread();
        GameFunctions.CCSCustomHudLayout_SetDialogVariableString(
            Address,
            panelId,
            variableName,
            value
        );
    }

    public void SetHasClassForPlayer(
        int playerId,
        string panelId,
        string className,
        EHudPanelClassStatus_t classStatus
    )
    {
        NativeBinding.ThrowIfNonMainThread();
        GameFunctions.CCSCustomHudLayout_SetHasClassForPlayer(
            Address,
            playerId,
            panelId,
            className,
            classStatus
        );
    }

    public void SetHasClass( string panelId, string className, EHudPanelClassStatus_t classStatus )
    {
        NativeBinding.ThrowIfNonMainThread();
        GameFunctions.CCSCustomHudLayout_SetHasClass(
            Address,
            panelId,
            className,
            classStatus
        );
    }
}
